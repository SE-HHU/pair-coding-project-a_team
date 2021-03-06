using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tools
{
    public class Expression
    {

        public static readonly Unit[] Operator = new Unit[] {
            new(UnitType.Operator, new(), new('(', 0, 0)),
            new(UnitType.Operator, new(), new(')', 1, 0)),
            new(UnitType.Operator, new(), new('+', 2, 1)),
            new(UnitType.Operator, new(), new('-', 3, 1)),
            new(UnitType.Operator, new(), new('*', 4, 2)),
            new(UnitType.Operator, new(), new('/', 5, 2)),
        };

        public static List<Unit> CanUseOperatorUnits;

        private static readonly Random random = new Random();

        /// <summary>
        /// 根据设置生成可用的运算符列表
        /// </summary>
        public static void FillCanUseOperators()
        {
            CanUseOperatorUnits = new List<Unit>();
            if (Settings.AllowPlus)
            {
                CanUseOperatorUnits.Add(Operator[2]);
            }//允许加法
            if (Settings.AllowSubscribe)
            {
                CanUseOperatorUnits.Add(Operator[3]);
            }//允许减法
            if (Settings.AllowMultiply)
            {
                CanUseOperatorUnits.Add(Operator[4]);
            }//允许乘法
            if (Settings.AllowDivide)
            {
                CanUseOperatorUnits.Add(Operator[5]);
            }//允许除法
        }

        /// <summary>
        /// 获取随机表达式, 仅保证形式合法性
        /// </summary>
        /// <returns>随机表达式</returns>
        public static List<Unit> GetRandomExpression()
        {
            List<Unit> infix = new List<Unit>(2 * Settings.OperatorsNumber + 3);
            Unit pre = Unit.GetRandomOperand();
            //为了处理除法, 先生成一个操作数, 此后以一个操作符和一个操作数为一组生成
            int leftParentheses = -2;
            int rightParentheses = -2;
            //为了避免出现不配对的括号, 位置应当小于-1
            if (Settings.AllowParentheses)
            {
                leftParentheses = random.Next(0, Settings.OperatorsNumber);
                rightParentheses = random.Next(leftParentheses + 1, Settings.OperatorsNumber + 1);
                if (leftParentheses == 0 && rightParentheses == Settings.OperatorsNumber)
                {
                    leftParentheses = -2;
                    rightParentheses = -2;
                }//包括整个表达式的无效括号
            }//允许括号
            if (leftParentheses == 0)
            {
                infix.Add(Operator[0]);
            }//括号出现在表达式首部
            infix.Add(pre);
            for (int j = 0; j < Settings.OperatorsNumber; j++)
            {
                Unit @operator = CanUseOperatorUnits[random.Next(0, CanUseOperatorUnits.Count)];
                Unit now = Unit.GetRandomOperand();
                if (@operator.Operator.Value == '/')
                {

                    while (pre.CompareTo(now) >= 0)
                    {
                        now = Unit.GetRandomOperand();
                    }
                }//除法
                infix.Add(@operator);
                if (leftParentheses == j + 1)
                {
                    infix.Add(Operator[0]);
                }//左括号
                infix.Add(now);
                if (rightParentheses == j + 1)
                {
                    infix.Add(Operator[1]);
                }//右括号
                pre = now;
            }

            return infix;
        }

        /// <summary>
        /// 从字符串解析表达式
        /// </summary>
        /// <param name="expression">表达式的字符串</param>
        /// <returns>解析后的表达式, null或空字符串或空白字符串将返回null</returns>
        public static List<Unit> StringToExpression(String expression)
        {
            if (String.IsNullOrWhiteSpace(expression))
            {
                return null;
            }
            expression = Regex.Replace(expression, @"\s", "");
            //去除空白字符方便处理
            List<Unit> list = new List<Unit>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (Char.IsDigit(expression[i]))
                {
                    UnitType unitType = UnitType.Integer;
                    long value = 0;
                    for (; i < expression.Length && Char.IsDigit(expression[i]); i++)
                    {
                        value *= 10;
                        value += long.Parse(expression[i].ToString());
                    }
                    i--;
                    list.Add(new Unit(unitType, new Fraction(value, 1), new Operator()));
                }//数字
                else if (expression[i] == 'U')
                {
                    long integer = list[list.Count - 1].Fraction.Numerator;
                    long sign = (integer < 0) ? -1 : 1;
                    integer = Math.Abs(integer);
                    //整数部分
                    list.RemoveAt(list.Count - 1);
                    i += 2;
                    long Numerator = 0;
                    for (; i < expression.Length && Char.IsDigit(expression[i]); i++)
                    {
                        Numerator *= 10;
                        Numerator += long.Parse(expression[i].ToString());
                    }
                    //分子部分
                    long Denomination = 0;
                    for (i++; i < expression.Length && Char.IsDigit(expression[i]); i++)
                    {
                        Denomination *= 10;
                        Denomination += long.Parse(expression[i].ToString());
                    }
                    //分母部分
                    i--;
                    Numerator += integer * Denomination;
                    Numerator *= sign;
                    list.Add(new Unit(UnitType.Fraction,
                        new Fraction(Numerator, Denomination), new Operator()));
                }//字符'U'
                else if (expression[i] == '_')
                {
                    long Numerator = 0;
                    for (i++; i < expression.Length && Char.IsDigit(expression[i]); i++)
                    {
                        Numerator *= 10;
                        Numerator += long.Parse(expression[i].ToString());
                    }
                    //分子部分
                    long Denomination = 0;
                    for (i++; i < expression.Length && Char.IsDigit(expression[i]); i++)
                    {
                        Denomination *= 10;
                        Denomination += long.Parse(expression[i].ToString());
                    }
                    //分母部分
                    i--;
                    list.Add(new Unit(UnitType.Fraction,
                        new Fraction(Numerator, Denomination), new Operator()));
                }//字符'_'
                else if (expression[i] == '+' || expression[i] == '-')
                {
                    if (i == 0 || (!Char.IsDigit(expression[i - 1]) && expression[i - 1] != ')'))
                    {//若前一个字符是右括号, 则应当认定为运算符, 判断右括号不可缺少
                        long sign = (expression[i] == '+') ? 1 : -1;
                        UnitType unitType = UnitType.Integer;
                        long value = 0;
                        for (i++; i < expression.Length && Char.IsDigit(expression[i]); i++)
                        {
                            value *= 10;
                            value += long.Parse(expression[i].ToString());
                        }
                        i--;
                        value *= sign;
                        list.Add(new Unit(unitType, new Fraction(value, 1), new Operator()));
                    }//作为符号解析
                    else
                    {
                        list.Add(Operator[expression[i] == '+' ? 2 : 3]);
                    }//作为运算符解析
                }//字符'+'或'-'
                else if (expression[i] == '*' || expression[i] == '/')
                {
                    list.Add(Operator[expression[i] == '*' ? 4 : 5]);
                }//字符'*'或'/'
                else
                {
                    list.Add(Operator[expression[i] == '(' ? 0 : 1]);
                }//字符'('或')'
            }

            return list;
        }

        /// <summary>
        /// 输出表达式的字符串形式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>表达式的字符串形式, null或空表达式将返回null</returns>
        public static String ExpressionToString(List<Unit> expression)
        {
            if (expression == null || expression.Count == 0)
            {
                return null;
            }//空表达式
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Unit unit in expression)
            {
                stringBuilder.Append(unit.ToString());
                stringBuilder.Append(' ');
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 输出表达式的HTML形式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>表达式的HTML形式, null或空表达式将返回null</returns>
        public static String ExpressionToHTML(List<Unit> expression)
        {
            if (expression == null || expression.Count == 0)
            {
                return null;
            }//空表达式
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Unit unit in expression)
            {
                stringBuilder.Append(unit.ToHTML());
                stringBuilder.Append(' ');
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 基于调度场算法将中缀表达式转换为后缀表达式
        /// </summary>
        /// <param name="infix">中缀表达式</param>
        /// <returns>后缀表达式, null或空表达式将返回null</returns>
        public static List<Unit> InfixToPostfix(List<Unit> infix)
        {
            if (infix == null || infix.Count == 0)
            {
                return null;
            }//空表达式
            Stack<Unit> stack = new Stack<Unit>();
            List<Unit> postfix = new List<Unit>(infix.Count);
            foreach (Unit unit in infix)
            {
                if (unit.UnitType == UnitType.Operator)
                {
                    if (stack.Count == 0)
                    {
                        stack.Push(unit);
                    }//栈内为空
                    else
                    {
                        if (unit.Operator.Value == '(')
                        {
                            stack.Push(unit);
                        }//左括号
                        else if (unit.Operator.Value == ')')
                        {
                            while (stack.Peek().Operator.Value != '(')
                            {
                                postfix.Add(stack.Pop());
                            }
                            stack.Pop();
                        }//右括号
                        else
                        {
                            while (stack.Count != 0 &&
                                unit.Operator.Propriety <= stack.Peek().Operator.Propriety)
                            {
                                postfix.Add(stack.Pop());
                            }
                            stack.Push(unit);
                        }//其他运算符
                    }//栈内非空
                }//unit是运算符
                else
                {
                    postfix.Add(unit);
                }//unit是操作数
            }
            while (stack.Count != 0)
            {
                postfix.Add(stack.Pop());
            }
            return postfix;
        }

        /// <summary>
        /// 计算后缀表达式
        /// </summary>
        /// <param name="postfix">后缀表达式</param>
        /// <returns>计算结果</returns>
        /// <exception cref="ArgumentException">表达式不合法</exception>
        /// <exception cref="ArgumentNullException">表达式为空</exception>
        /// <exception cref="ArgumentOutOfRangeException">运算结果超出范围限制</exception>
        /// <exception cref="DivideByZeroException">除零错误</exception>
        /// <exception cref="NotSupportedException">存在未定义的运算符</exception>
        public static Unit CalculatePostfix(List<Unit> postfix)
        {
            if (postfix == null || postfix.Count == 0)
            {
                throw new ArgumentNullException();
            }//空表达式
            else
            {
                Stack<Unit> stack = new Stack<Unit>();
                foreach (Unit unit in postfix)
                {
                    if (unit.UnitType == UnitType.Operator)
                    {
                        if (stack.Count < 2)
                        {
                            throw new ArgumentException();
                        }//栈内少于两个元素
                        Unit B = stack.Pop();
                        Unit A = stack.Pop();
                        Unit value; //运算结果
                        switch (unit.Operator.Number)
                        {
                            case 2:
                                value = A + B;
                                break;
                            case 3:
                                value = A - B;
                                break;
                            case 4:
                                value = A * B;
                                break;
                            case 5:
                                try
                                {
                                    value = A / B;
                                }
                                catch
                                {
                                    throw;
                                }
                                break;
                            default:
                                throw new NotSupportedException();
                        }
                        if (!value.InRange())
                        {
                            throw new ArgumentOutOfRangeException();
                        }//结果不在限制范围内
                        stack.Push(value);
                    }
                    else
                    {
                        stack.Push(unit);
                    }
                }
                if (stack.Count != 1)
                {
                    throw new ArgumentException();
                }//运算完成后栈内多于一个元素
                else
                {
                    Unit answer = stack.Pop();
                    answer.ChangeType();

                    return answer;
                }//运算正常完成
            }
        }

        /// <summary>
        /// 参照设计文档中的伪代码, 根据后缀表达式生成表达式树
        /// </summary>
        /// <param name="postfix">后缀表达式</param>
        /// <returns>表达式树的根节点</returns>
        /// <exception cref="ArgumentNullException">传入的是null或空表达式</exception>
        /// <exception cref="ArgumentException">传入的是非法表达式</exception>
        public static Node PostfixToTree(List<Unit> postfix)
        {
            if (postfix == null || postfix.Count == 0)
            {
                throw new ArgumentNullException();
            }
            Stack<Node> stack = new Stack<Node>();
            foreach (Unit unit in postfix)
            {
                if (unit.UnitType == UnitType.Operator)
                {
                    if (unit.Operator.Value == '+' || unit.Operator.Value == '*')
                    {
                        Node root = new Node(unit);
                        Node B = stack.Pop();
                        Node A = stack.Pop();
                        if (A.CompareTo(B) >= 0)
                        {
                            root.LeftChild = A;
                            root.RightChild = B;
                        }
                        else
                        {
                            root.LeftChild = B;
                            root.RightChild = A;
                        }
                        stack.Push(root);
                    }//加减法
                    else
                    {
                        Node root = new Node(unit);
                        Node B = stack.Pop();
                        Node A = stack.Pop();
                        root.LeftChild = A;
                        root.RightChild = B;
                        stack.Push(root);
                    }//其他运算符
                }//unit是运算符
                else
                {
                    Node root = new Node(unit);
                    stack.Push(root);
                }//unit是操作数
            }
            if (stack.Count != 1)
            {
                throw new ArgumentException();
            }//建树完成后栈内元素数量不为一

            return stack.Pop();
        }
    }
}
