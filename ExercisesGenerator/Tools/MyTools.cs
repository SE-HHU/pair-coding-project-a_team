using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MyTools
{
    public static class Maps
    {
        public static Dictionary<char, int> operators = new Dictionary<char, int> {
            { '(', 0 },
            { ')', 1 },
            { '+', 2 },
            { '-', 3 },
            { '*', 4 },
            { '/', 5 },
            { '^', 6 }
        };

        public static Dictionary<int, char> getOperators = new Dictionary<int, char> {
            { 0, '(' },
            { 1, ')' },
            { 2, '+' },
            { 3, '-' },
            { 4, '*' },
            { 5, '/' },
            { 6, '^' }
        };

        public static Dictionary<char, int> priporoty = new Dictionary<char, int> {
            { '(', 0 },
            { '+', 1 },
            { '-', 1 },
            { '*', 2 },
            { '/', 2 },
            { '^', 3 }
        };
    }
    public enum UnitType
    {
        Number = 0,
        Operator = 1
    }
    public struct Unit
    {
        public UnitType unitType;
        public int value;
        public Unit(int unitType, int value)
        {
            this.unitType = (UnitType)unitType;
            this.value = value;
        }
        public Unit(UnitType unitType, int value)
        {
            this.unitType = unitType;
            this.value = value;
        }
        public override string ToString()
        {
            if (unitType == UnitType.Number)
            {
                if (value < 0)
                {
                    return "(" + value.ToString() + ")";
                }
                else
                {
                    return value.ToString();
                }
            }
            else
            {
                return Maps.getOperators[value].ToString();
            }
        }
    }
    public class Tools
    {
        public static int? QuickPower(int baseNumber, int index)
        {
            if ((index < 0) || (baseNumber == 0 && index == 0))
            {
                return null;
            }
            else
            {
                int value = 1, temp = baseNumber;
                while (index != 0)
                {
                    if ((index & 1) == 1)
                    {
                        value *= temp;
                    }
                    temp *= temp;
                    index >>= 1;
                }
                return value;
            }
        }

        public static List<Unit> StringToList(String s)
        {
            if (String.IsNullOrWhiteSpace(s))
            {
                return null;
            }
            s = Regex.Replace(s, @"\s", "");
            List<Unit> list = new List<Unit>();
            for (int i = 0; i < s.Length; i++)
            {
                if (Char.IsDigit(s[i]))
                {
                    UnitType unitType = UnitType.Number;
                    int value = 0;
                    for (; i < s.Length && Char.IsDigit(s[i]); i++)
                    {
                        value *= 10;
                        value += int.Parse(s[i].ToString());
                    }
                    i--;
                    list.Add(new Unit(unitType, value));
                }
                else if (s[i] == '+' || s[i] == '-')
                {
                    if (i == 0 || !Char.IsDigit(s[i - 1]))
                    {
                        int sign = (s[i] == '+') ? 1 : -1;
                        UnitType unitType = UnitType.Number;
                        int value = 0;
                        for (i++; i < s.Length && Char.IsDigit(s[i]); i++)
                        {
                            value *= 10;
                            value += int.Parse(s[i].ToString());
                        }
                        i--;
                        value *= sign;
                        list.Add(new Unit(unitType, value));
                    }
                    else
                    {
                        UnitType unitType = UnitType.Operator;
                        int value = Maps.operators[s[i]];
                        list.Add(new Unit(unitType, value));
                    }
                }
                else
                {
                    UnitType unitType = UnitType.Operator;
                    int value = Maps.operators[s[i]];
                    list.Add(new Unit(unitType, value));
                }
            }
            return list;
        }

        public static String ListToString(List<Unit> list)
        {
            if (list == null || list.Count == 0)
            {
                return null;
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Unit unit in list)
            {
                stringBuilder.Append(unit.ToString());
                stringBuilder.Append(" ");
            }
            String s = stringBuilder.ToString();
            return s;
        }

        public static List<Unit> InfixToPostfix(List<Unit> infix)
        {
            if (infix == null || infix.Count == 0)
            {
                return null;
            }
            Stack<Unit> stack = new Stack<Unit>();
            List<Unit> postfix = new List<Unit>();
            foreach (Unit unit in infix)
            {
                if (unit.unitType == UnitType.Number)
                {
                    postfix.Add(unit);
                }
                else
                {
                    if (stack.Count == 0)
                    {
                        stack.Push(unit);
                    }
                    else
                    {
                        if (unit.value == 0){
                            stack.Push(unit);
                        }
                        else if (unit.value == 1){
                            while (stack.Peek().value != 0){
                                postfix.Add(stack.Pop());
                            }
                            stack.Pop();
                        }
                        //right associative
                        else if (unit.value == 6)
                        {
                            stack.Push(unit);
                        }
                        //left associative
                        else
                        {
                            while (stack.Count != 0 && 
                                (Maps.priporoty[Maps.getOperators[unit.value]]
                                    <= Maps.priporoty[Maps.getOperators[stack.Peek().value]]))
                            {
                                postfix.Add(stack.Pop());
                            }
                            stack.Push(unit);
                        }
                    }
                }
            }
            while (stack.Count != 0)
            {
                postfix.Add(stack.Pop());
            }
            return postfix;
        }

        public static int? CalculatePostfix(List<Unit> postfix, int? min = null, int? max = null)
        {
            if (postfix == null || postfix.Count == 0)
            {
                return null;
            }
            else
            {
                Stack<int> stack = new Stack<int>();
                foreach(Unit unit in postfix)
                {
                    if (unit.unitType == UnitType.Number)
                    {
                        stack.Push(unit.value);
                    }
                    else
                    {
                        if (stack.Count < 2)
                        {
                            return null;
                        }
                        int B = stack.Pop();
                        int A = stack.Pop();
                        long? value;
                        switch (unit.value)
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
                                if (B == 0)
                                {
                                    value = null;
                                }
                                else
                                {
                                    value = A / B;
                                }
                                break;
                            case 6:
                                value = QuickPower(A, B);
                                break;
                            default:
                                value = null;
                                break;
                        }
                        if (value == null)
                        {
                            return null;
                        }
                        if ((long)value < (min ?? int.MinValue + 1) || (long)value > (max ?? int.MaxValue - 1))
                        {
                            return null;
                        }
                        stack.Push((int)value);
                    }
                }
                if (stack.Count != 1)
                {
                    return null;
                }
                else
                {
                    return stack.Pop();
                }
            }
        }
    }
}
