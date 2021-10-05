using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools;

namespace PWA.Shared.Models
{
    public class CheckData
    {
        public class Checker{
            public int Number;
            //题号
            public String OriginalProblem;
            //原始题目
            public List<Unit> Problem;
            //后缀表达式形式的题目
            public String OriginalAnswer;
            //原始答案
            public Unit Answer;
            //Unit形式的答案

            public Checker()
            {
                Problem = null;
            }

            public Checker(int number, string originalProblem, string originalAnswer)
            {
                Number = number;
                OriginalProblem = originalProblem;
                Problem = Expression.InfixToPostfix(Expression.StringToExpression(OriginalProblem));
                OriginalAnswer = originalAnswer;
                List<Unit> AnswerTemp = Expression.StringToExpression(OriginalAnswer);
                if (AnswerTemp == null || AnswerTemp.Count != 1)
                {
                    Answer = null;
                }//答案不合法或无法解析
                else
                {
                    Answer = AnswerTemp[0];
                    Answer.ChangeType();
                    //规格化答案, 防止因为 UnitType 不同导致判断出错
                }
            }
        }

        public List<Checker> Exercises = new List<Checker>();
        //习题集
        public List<Checker> Correct = new List<Checker>();
        //正确习题
        public List<Checker> Wrong = new List<Checker>();
        //错误习题
        public Dictionary<String, Checker> Expressions = new Dictionary<String, Checker>();
        //查重
        public Dictionary<Checker, Checker> Repeat = new Dictionary<Checker, Checker>();
        //重复习题
    }
}
