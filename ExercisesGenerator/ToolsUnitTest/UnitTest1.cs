using NUnit.Framework;
using System.Collections.Generic;
using MyTools;
using System;
using System.Windows.Forms;

namespace ToolsUnitTest
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestQuickPower()
        {
            Assert.AreEqual(Tools.QuickPower(0, 0), null);
            Assert.AreEqual(Tools.QuickPower(2, -1), null);
            Assert.AreEqual(Tools.QuickPower(0, 1), 0);
            Assert.AreEqual(Tools.QuickPower(2, 0), 1);
            Assert.AreEqual(Tools.QuickPower(2, 10), 1024);
            Assert.AreEqual(Tools.QuickPower(-10, 3), -1000);
        }
        [Test]
        public void TestListToString()
        {
            //null
            Assert.AreEqual(Tools.ListToString(null), null);
            Assert.AreEqual(Tools.ListToString(new List<Unit> { }), null);

            List<Unit> list = new List<Unit>
            {
                new Unit(UnitType.Number, -12),
                new Unit(UnitType.Operator, 2),
                new Unit(UnitType.Number, 2)
            };
            Assert.AreEqual(Tools.ListToString(list), "(-12) + 2 ");

            List<Unit> list2 = new List<Unit>
            {
                new Unit(0, -12),//-12
                new Unit(1, 2),//+
                new Unit(1, 0),//(
                new Unit(0, 13),//13
                new Unit(1, 2),//+
                new Unit(0, -13),//-13
                new Unit(1, 6),//^
                new Unit(0, 2),//2
                new Unit(1, 1),//)
                new Unit(1, 4),//*
                new Unit(0, 12),//12
            };
            Assert.AreEqual(Tools.ListToString(list2), "(-12) + ( 13 + (-13) ^ 2 ) * 12 ");
        }
        [Test]
        public void TestStringToList()
        {
            //null
            Assert.AreEqual(Tools.StringToList(null), null);
            Assert.AreEqual(Tools.StringToList(""), null);
            Assert.AreEqual(Tools.StringToList("  "), null);

            List<Unit> list = new List<Unit>
            {
                new Unit(UnitType.Number, -12),
                new Unit(UnitType.Operator, 2),
                new Unit(UnitType.Number, 2)
            };
            Assert.AreEqual(Tools.StringToList("-12+2"), list);

            List<Unit> list2 = new List<Unit>
            {
                new Unit(0, -12),//-12
                new Unit(1, 2),//+
                new Unit(1, 0),//(
                new Unit(0, 13),//13
                new Unit(1, 2),//+
                new Unit(0, -13),//-13
                new Unit(1, 6),//^
                new Unit(0, 2),//2
                new Unit(1, 1),//)
                new Unit(1, 4),//*
                new Unit(0, 12),//12
            };
            Assert.AreEqual(Tools.StringToList("-12+(1 3+-13^2) *+12"), list2);
        }
        [Test]
        public void TestInfixToPostfix()
        {
            //null
            Assert.AreEqual(Tools.InfixToPostfix(null), null);
            Assert.AreEqual(Tools.InfixToPostfix(new List<Unit> { }), null);


            List<Unit> infix = new List<Unit>
            {
                new Unit(UnitType.Number, -12),
                new Unit(UnitType.Operator, 4),
                new Unit(UnitType.Number, 2),
                new Unit(UnitType.Operator, 2),
                new Unit(UnitType.Number, 2)
            };
            List<Unit> postfix = new List<Unit>
            {
                new Unit(UnitType.Number, -12),
                new Unit(UnitType.Number, 2),
                new Unit(UnitType.Operator, 4),
                new Unit(UnitType.Number, 2),
                new Unit(UnitType.Operator, 2)
            };
            Assert.AreEqual(Tools.InfixToPostfix(infix), postfix);

            //3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3
            List<Unit> infix2 = new List<Unit>
            {
                new Unit(0, 3),// 3
                new Unit(1, 2),// +
                new Unit(0, 4),// 4
                new Unit(1, 4),// *
                new Unit(0, 2),// 2
                new Unit(1, 5),// /
                new Unit(1, 0),// (
                new Unit(0, 1),// 1
                new Unit(1, 3),// -
                new Unit(0, 5),// 5
                new Unit(1, 1),// )
                new Unit(1, 6),// ^
                new Unit(0, 2),// 2
                new Unit(1, 6),// ^
                new Unit(0, 3)// 3
            };
            //3 4 2 * 1 5 - 2 3 ^ ^ / +
            List<Unit> postfix2 = new List<Unit>
            {
                new Unit(0, 3),// 3
                new Unit(0, 4),// 4
                new Unit(0, 2),// 2
                new Unit(1, 4),// *
                new Unit(0, 1),// 1
                new Unit(0, 5),// 5
                new Unit(1, 3),// -
                new Unit(0, 2),// 2
                new Unit(0, 3),// 3
                new Unit(1, 6),// ^
                new Unit(1, 6),// ^
                new Unit(1, 5),// /
                new Unit(1, 2)// +
            };
            Assert.AreEqual(Tools.InfixToPostfix(infix2), postfix2);

            //3 + 4 * 2 / ( 1 - 5 ) ^ 2 ^ 3
            List<Unit> infix3 = new List<Unit>
            {
                new Unit(0, 3),// 3
                new Unit(1, 2),// +
                new Unit(0, 4),// 4
                new Unit(1, 4),// *
                new Unit(0, 2),// 2
                new Unit(1, 5),// /
                new Unit(1, 0),// (
                new Unit(0, 1),// 1
                new Unit(1, 3),// -
                new Unit(0, 5),// 5
                new Unit(1, 1),// )
                new Unit(1, 6),// ^
                new Unit(0, 2),// 2
                new Unit(1, 6),// ^
                new Unit(0, 3)// 3
            };
            //3 4 2 * 1 5 - 2 3 ^ ^ / +
            List<Unit> postfix3 = new List<Unit>
            {
                new Unit(0, 3),// 3
                new Unit(0, 4),// 4
                new Unit(0, 2),// 2
                new Unit(1, 4),// *
                new Unit(0, 1),// 1
                new Unit(0, 5),// 5
                new Unit(1, 3),// -
                new Unit(0, 2),// 2
                new Unit(0, 3),// 3
                new Unit(1, 6),// ^
                new Unit(1, 6),// ^
                new Unit(1, 5),// /
                new Unit(1, 2)// +
            };
            Assert.AreEqual(Tools.InfixToPostfix(infix3), postfix3);
        }
        [Test]
        public void TestCalculatePostfix()
        {
            //null
            Assert.AreEqual(Tools.CalculatePostfix(null, null, null), null);
            Assert.AreEqual(Tools.CalculatePostfix(new List<Unit>(), null, null), null);
            Assert.AreEqual(Tools.CalculatePostfix(new List<Unit> { new Unit(1, 2) }, null, null), null);

            //3 4 2 * 1 5 - 2 3 ^ ^ * +
            List<Unit> postfix = new List<Unit>
            {
                new Unit(0, 3),// 3
                new Unit(0, 4),// 4
                new Unit(0, 2),// 2
                new Unit(1, 4),// *
                new Unit(0, 1),// 1
                new Unit(0, 5),// 5
                new Unit(1, 3),// -
                new Unit(0, 2),// 2
                new Unit(0, 3),// 3
                new Unit(1, 6),// ^
                new Unit(1, 6),// ^
                new Unit(1, 4),// *
                new Unit(1, 2)// +
            };
            Assert.AreEqual(Tools.CalculatePostfix(postfix, null, null), 524291);
            Assert.AreEqual(Tools.CalculatePostfix(postfix, 524300, null), null);
            Assert.AreEqual(Tools.CalculatePostfix(postfix, null, 100), null);
            Assert.AreEqual(Tools.CalculatePostfix(postfix, 0, 10), null);

            // 1 0 /
            Assert.AreEqual(Tools.CalculatePostfix(new List<Unit> { 
                        new Unit(0, 1), new Unit(0, 0), new Unit(1, 5)
                    }, 
                null, null), null);

            // 1 1 / 3 + 4 - 6 | 1 / 1 + 3 - 4 6
            Assert.AreEqual(Tools.CalculatePostfix(new List<Unit> {
                        new Unit(0, 1), new Unit(0, 1), new Unit(1, 5),
                        new Unit(0, 3), new Unit(1, 2), new Unit(0, 4),
                        new Unit(1, 3), new Unit(0, 6)
                    },
                null, null), null);

            // 1 1 / 3 + 4 - | 1 / 1 + 3 - 4 = 0
            Assert.AreEqual(Tools.CalculatePostfix(new List<Unit> {
                        new Unit(0, 1), new Unit(0, 1), new Unit(1, 5),
                        new Unit(0, 3), new Unit(1, 2), new Unit(0, 4),
                        new Unit(1, 3)
                    },
                null, null), 0);

            // 1 0 (
            Assert.AreEqual(Tools.CalculatePostfix(new List<Unit> {
                        new Unit(0, 1), new Unit(0, 0), new Unit(1, 1)
                    },
                null, null), null);

            // 1 1 + 
            Assert.AreEqual(Tools.CalculatePostfix(new List<Unit> {
                        new Unit(0, 2), new Unit(0, 2), new Unit(1, 2)
                    },
                0, 3), null);

            // 2 -6 ^
            Assert.AreEqual(Tools.CalculatePostfix(new List<Unit> {
                        new Unit(0, 2), new Unit(0, -6), new Unit(1, 6)
                    },
                null, null), null);
        }
    }
}