using NUnit.Framework;
using System;
using System.Collections.Generic;
using Tools;

namespace ToolsUnitTest
{
    public class ExpressionTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestStringToExpression()
        {
            String text = "-17-4U_1~2 * 7 / 9 - (-4U_1~3 + _1~7)";
            List<Unit> list = Expression.StringToExpression(null);
            Assert.AreEqual(list, null);

            list = Expression.StringToExpression(text);
            Assert.IsTrue(list.Count == 13);
            Assert.IsTrue(list[0].CompareTo(new Unit(UnitType.Integer,
                new Fraction(-17, 1), null)) == 0);
            Assert.IsTrue(list[1].CompareTo(new Unit(UnitType.Operator,
                null, new Operator('-', 3, 1))) == 0);
            Assert.IsTrue(list[2].CompareTo(new Unit(UnitType.Fraction,
                new Fraction(9, 2), null)) == 0);
            Assert.IsTrue(list[3].CompareTo(new Unit(UnitType.Operator,
                null, new Operator('*', 4, 2))) == 0);
            Assert.IsTrue(list[4].CompareTo(new Unit(UnitType.Integer,
                new Fraction(7, 1), null)) == 0);
            Assert.IsTrue(list[5].CompareTo(new Unit(UnitType.Operator,
                null, new Operator('/', 5, 2))) == 0);
            Assert.IsTrue(list[6].CompareTo(new Unit(UnitType.Integer,
                new Fraction(9, 1), null)) == 0);
            Assert.IsTrue(list[7].CompareTo(new Unit(UnitType.Operator,
                null, new Operator('-', 3, 1))) == 0);
            Assert.IsTrue(list[8].CompareTo(new Unit(UnitType.Operator,
                null, new Operator('(', 0, 0))) == 0);
            Assert.IsTrue(list[9].CompareTo(new Unit(UnitType.Fraction,
                new Fraction(-13, 3), null)) == 0);
            Assert.IsTrue(list[10].CompareTo(new Unit(UnitType.Operator,
                null, new Operator('+', 2, 1))) == 0);
            Assert.IsTrue(list[11].CompareTo(new Unit(UnitType.Fraction,
                new Fraction(1, 7), null)) == 0);
            Assert.IsTrue(list[12].CompareTo(new Unit(UnitType.Operator,
                null, new Operator(')', 1, 0))) == 0);
        }

        [Test]
        public void TestExpressionToString()
        {
            List<Unit> list = null;
            Assert.AreEqual(Expression.ExpressionToString(list), null);
            list = new List<Unit>();
            Assert.AreEqual(Expression.ExpressionToString(list), null);
            String text = "-17-4U_1~2 * 7 / 9 - (-4U_1~3 + _1~7)";
             list = Expression.StringToExpression(text);
            Assert.AreEqual(Expression.ExpressionToString(list),
                "-17 - 4U_1~2 * 7 / 9 - ( -4U_1~3 + _1~7 ) ");
        }

        [Test]
        public void TestExpressionToHTML()
        {
            List<Unit> list = null;
            Assert.AreEqual(Expression.ExpressionToHTML(list), null);
            list = new List<Unit>();
            Assert.AreEqual(Expression.ExpressionToHTML(list), null);
            String text = "-17-4U_1~2 * 7 / 9 - (-4U_1~3 + _1~7)";
            list = Expression.StringToExpression(text);
            Assert.AreEqual(Expression.ExpressionToHTML(list),
                "-17 - 4<span class=\"texthidden\">U_</span><sup>1</sup>~<sub>2</sub> "
                + "* 7 / 9 - ( -4<span class=\"texthidden\">U_</span><sup>1</sup>~<sub>3</sub>"
                + " + <span class=\"texthidden\">_</span><sup>1</sup>~<sub>7</sub> ) ");
        }

        [Test]
        public void TestInfixToPostfix()
        {
            List<Unit> infix = null;
            Assert.AreEqual(Expression.InfixToPostfix(infix), null);
            infix = new List<Unit>();
            Assert.AreEqual(Expression.InfixToPostfix(infix), null);
            String text = "-17-4U_1~2 * 7 / 9 - (-4U_1~3 + _1~7)";
            infix = Expression.StringToExpression(text);
            List<Unit> postfix = Expression.InfixToPostfix(infix);
            Assert.AreEqual(postfix.Count, 11);
            Assert.IsTrue(postfix[0].CompareTo(new Unit(UnitType.Integer,
                new Fraction(-17, 1), null)) == 0);
            Assert.IsTrue(postfix[1].CompareTo(new Unit(UnitType.Fraction,
                new Fraction(9, 2), null)) == 0);
            Assert.IsTrue(postfix[2].CompareTo(new Unit(UnitType.Integer,
                new Fraction(7, 1), null)) == 0);
            Assert.IsTrue(postfix[3].CompareTo(new Unit(UnitType.Operator,
                null, new Operator('*', 4, 2))) == 0);
            Assert.IsTrue(postfix[4].CompareTo(new Unit(UnitType.Integer,
                new Fraction(9, 1), null)) == 0);
            Assert.IsTrue(postfix[5].CompareTo(new Unit(UnitType.Operator,
                null, new Operator('/', 5, 2))) == 0);
            Assert.IsTrue(postfix[6].CompareTo(new Unit(UnitType.Operator,
                null, new Operator('-', 3, 1))) == 0);
            Assert.IsTrue(postfix[7].CompareTo(new Unit(UnitType.Fraction,
                new Fraction(-13, 3), null)) == 0);
            Assert.IsTrue(postfix[8].CompareTo(new Unit(UnitType.Fraction,
                new Fraction(1, 7), null)) == 0);
            Assert.IsTrue(postfix[9].CompareTo(new Unit(UnitType.Operator,
                null, new Operator('+', 2, 1))) == 0);
            Assert.IsTrue(postfix[10].CompareTo(new Unit(UnitType.Operator,
                null, new Operator('-', 3, 1))) == 0);
        }

        [Test]
        public void TestCalculatePostfix()
        {
            Settings.IntegerMaximum = int.MaxValue;
            Settings.IntegerMinimize = int.MinValue;
            Settings.DenominationMaximum = int.MaxValue;
            String text = "-17-4U_1~2 * 7 / 9 - (-4U_1~3 + _1~7)";
            List<Unit> infix = Expression.StringToExpression(text);
            List<Unit> postfix = Expression.InfixToPostfix(infix);
            Unit answer = Expression.CalculatePostfix(postfix);
            Assert.IsTrue((new Unit(UnitType.Fraction, new Fraction(-685, 42),
                new Operator())).CompareTo(answer) == 0);

            postfix = Expression.StringToExpression("/17");
            Exception exception = null;
            try
            {
                Expression.CalculatePostfix(postfix);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.AreEqual(typeof(ArgumentException), exception.GetType());

            postfix = new List<Unit> {
                new Unit(UnitType.Integer, new Fraction(6, 1), null),
                new Unit(UnitType.Integer, new Fraction(6, 1), null),
                new Unit(UnitType.Operator, null, new Operator('+', 2, 1)),
                new Unit(UnitType.Integer, new Fraction(6, 1), null),
            };
            exception = null;
            try
            {
                Expression.CalculatePostfix(postfix);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.AreEqual(typeof(ArgumentException), exception.GetType());

            postfix = new List<Unit> {
                new Unit(UnitType.Integer, new Fraction(6, 1), null),
                new Unit(UnitType.Integer, new Fraction(0, 1), null),
                new Unit(UnitType.Operator, null, new Operator('/', 5, 2)),
            };
            exception = null;
            try
            {
                Expression.CalculatePostfix(postfix);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.AreEqual(typeof(DivideByZeroException), exception.GetType());

            postfix = null;
            exception = null;
            try
            {
                Expression.CalculatePostfix(postfix);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.AreEqual(typeof(ArgumentNullException), exception.GetType());

            postfix = new List<Unit>();
            exception = null;
            try
            {
                Expression.CalculatePostfix(postfix);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.AreEqual(typeof(ArgumentNullException), exception.GetType());

            postfix = new List<Unit> {
                new Unit(UnitType.Integer, new Fraction(6, 1), null),
                new Unit(UnitType.Integer, new Fraction(6, 1), null),
                new Unit(UnitType.Operator, null, new Operator('(', 0, 0)),
            };
            exception = null;
            try
            {
                Expression.CalculatePostfix(postfix);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.AreEqual(typeof(NotSupportedException), exception.GetType());

            Settings.IntegerMaximum = 0;
            Settings.IntegerMinimize = 10;
            Settings.DenominationMaximum = 10;
            postfix = new List<Unit> {
                new Unit(UnitType.Integer, new Fraction(6, 1), null),
                new Unit(UnitType.Integer, new Fraction(6, 1), null),
                new Unit(UnitType.Operator, null, new Operator('+', 2, 1)),
            };
            exception = null;
            try
            {
                Expression.CalculatePostfix(postfix);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.AreEqual(typeof(ArgumentOutOfRangeException), exception.GetType());
        }

        [Test]
        public void TestPostfixToTree()
        {
            List<Unit> postfix1 = Expression.InfixToPostfix(
                Expression.StringToExpression(
                    "3+4+5+9*7/6"));
            Node tree1 = Expression.PostfixToTree(postfix1);
            Assert.AreEqual("+(/(*(9()())(7()()))(6()()))(+(+(4()())(3()()))(5()()))",
                tree1.ToString());
            
            List<Unit> postfix2 = Expression.InfixToPostfix(
                Expression.StringToExpression(
                    "4+3+(9+5)"));
            Node tree2 = Expression.PostfixToTree(postfix2);
            Assert.AreEqual("+(+(9()())(5()()))(+(4()())(3()()))", tree2.ToString());

            postfix2 = Expression.InfixToPostfix(
                Expression.StringToExpression(
                    "4+3+5+7*9/6"));
            tree2 = Expression.PostfixToTree(postfix2);
            Assert.AreEqual(tree1.ToString(), tree2.ToString());

            postfix1 = new List<Unit>();
            Exception exception = null;
            try
            {
                Expression.PostfixToTree(postfix1);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.AreEqual(typeof(ArgumentNullException), exception.GetType());

            postfix1 = null;
            exception = null;
            try
            {
                Expression.PostfixToTree(postfix1);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.AreEqual(typeof(ArgumentNullException), exception.GetType());

            postfix1 = new List<Unit>
            {
                new Unit(UnitType.Integer, new Fraction(15, 0), null),
                new Unit(UnitType.Fraction, new Fraction(7, 6), null),
                new Unit(UnitType.Operator, null, new Operator('*', 4, 2)),
                new Unit(UnitType.Integer, new Fraction(), null),
            };
            exception = null;
            try
            {
                Expression.PostfixToTree(postfix1);
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        }
    }
}
