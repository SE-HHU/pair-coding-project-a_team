using NUnit.Framework;
using System;
using Tools;

namespace ToolsUnitTest
{
    public class UnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestInRange()
        {
            Settings.IntegerMinimize = 0;
            Settings.IntegerMaximum = 100;
            Settings.DenominationMaximum = 100;

            Unit unit = new Unit();

            Assert.IsTrue(unit.InRange());

            unit = new Unit(UnitType.Fraction, new Fraction(10, 1000), null);
            Assert.IsTrue(unit.InRange());

            unit = new Unit(UnitType.Operator, new Fraction(10, 1000), null);
            Assert.IsFalse(unit.InRange());

            unit = new Unit(UnitType.Fraction, new Fraction(10009, 1000), null);
            Assert.IsFalse(unit.InRange());

            unit = new Unit(UnitType.Fraction, new Fraction(1010009, 99), null);
            Assert.IsFalse(unit.InRange());
        }

        [Test]
        public void TestCompareTo()
        {
            Unit unit1 = new Unit();
            Unit unit2 = null;

            Assert.IsTrue(unit1.CompareTo(unit2) > 0);

            unit2 = new Unit(UnitType.Fraction, new Fraction(), null);
            Assert.IsTrue(unit1.CompareTo(unit2) < 0);
            Assert.IsTrue(unit2.CompareTo(unit1) > 0);

            unit2 = new Unit(UnitType.Operator, new Fraction(), new Operator());
            Assert.IsTrue(unit1.CompareTo(unit2) < 0);

            unit1 = new Unit(UnitType.Operator, new Fraction(), new Operator('+', 1, 1));
            Assert.IsTrue(unit1.CompareTo(unit2) > 0);

            unit2 = new Unit(UnitType.Operator, new Fraction(), new Operator('+', 1, 1));
            Assert.IsTrue(unit1.CompareTo(unit2) == 0);

            unit1 = new Unit(UnitType.Integer, new Fraction(10, 1), null);
            unit2 = new Unit(UnitType.Integer, new Fraction(1, 1), null);
            Assert.IsTrue(unit1.CompareTo(unit2) > 0);
            Assert.IsTrue(unit2.CompareTo(unit1) < 0);

            unit1 = new Unit(UnitType.Fraction, new Fraction(10, 11), null);
            unit2 = new Unit(UnitType.Fraction, new Fraction(1, 1), null);
            Assert.IsTrue(unit1.CompareTo(unit2) > 0);
            Assert.IsTrue(unit2.CompareTo(unit1) < 0);

            unit2 = new Unit(UnitType.Fraction, new Fraction(1, 11), null);
            Assert.IsTrue(unit1.CompareTo(unit2) > 0);
            Assert.IsTrue(unit2.CompareTo(unit1) < 0);
        }
        
        [Test]
        public void TestChangeType()
        {
            Unit unit = new Unit(UnitType.Fraction, new Fraction(1, 1), null);
            unit.ChangeType();
            Assert.IsTrue(unit.UnitType == UnitType.Integer);

            unit = new Unit(UnitType.Fraction, new Fraction(0, 10086), null);
            unit.ChangeType();
            Assert.IsTrue(unit.UnitType == UnitType.Integer
                && unit.Fraction.Denomination == 1);

            unit = new Unit(UnitType.Operator, new Fraction(1, 21), new Operator());
            unit.ChangeType();
            Assert.IsTrue(unit.UnitType == UnitType.Operator);
        }

        [Test]
        public void TestToString()
        {
            Unit unit = new Unit(UnitType.Fraction, new Fraction(1, 1), null);

            Assert.AreEqual(unit.ToString(), "1");

            unit = new Unit(UnitType.Integer, new Fraction(15, 15), null);
            Assert.AreEqual(unit.ToString(), "15");

            unit = new Unit(UnitType.Fraction, new Fraction(16, 3), null);
            Assert.AreEqual(unit.ToString(), "5U_1~3");

            unit = new Unit(UnitType.Operator, null, new Operator('+', 1, 1));
            Assert.AreEqual(unit.ToString(), "+");

            unit = new Unit((UnitType)4, null, new Operator('+', 1, 1));
            Assert.IsTrue(unit.ToString() == null);
        }

        [Test]
        public void TestToHTML()
        {
            Unit unit = new Unit(UnitType.Fraction, new Fraction(1, 1), null);

            Assert.AreEqual(unit.ToHTML(), "1");

            unit = new Unit(UnitType.Integer, new Fraction(15, 15), null);
            Assert.AreEqual(unit.ToHTML(), "15");

            unit = new Unit(UnitType.Fraction, new Fraction(16, 3), null);
            Assert.AreEqual(unit.ToHTML(),
                "5<span class=\"texthidden\">U_</span><sup>1</sup>~<sub>3</sub>");

            unit = new Unit(UnitType.Operator, null, new Operator('+', 1, 1));
            Assert.AreEqual(unit.ToHTML(), "+");

            unit = new Unit((UnitType)4, null, new Operator('+', 1, 1));
            Assert.IsTrue(unit.ToHTML() == null);
        }

        [Test]
        public void TestAddition()
        {
            Unit unit1 = new Unit();
            Unit unit2 = new Unit(UnitType.Fraction, new Fraction(2, 7), null);

            unit1 = unit1 + unit2;
            Assert.IsTrue(unit1.Fraction.Numerator == 9 && unit1.Fraction.Denomination == 7);

            unit1 = new Unit(UnitType.Fraction, new Fraction(-2, 7), null);
            unit1 = unit1 + unit2;
            Assert.IsTrue(unit1.Fraction.Numerator == 0 && unit1.Fraction.Denomination == 1);

            unit1 = new Unit(UnitType.Integer, new Fraction(-10, 7), null);
            unit2 = new Unit(UnitType.Fraction, new Fraction(1, 7), null);
            unit1 = unit1 + unit2;
            Assert.IsTrue(unit1.Fraction.Numerator == -69 && unit1.Fraction.Denomination == 7);
        }

        [Test]
        public void TestSubscribe()
        {
            Unit unit1 = new Unit();
            Unit unit2 = new Unit(UnitType.Fraction, new Fraction(2, 7), null);

            unit1 = unit1 - unit2;
            Assert.IsTrue(unit1.Fraction.Numerator == 5 && unit1.Fraction.Denomination == 7);

            unit1 = new Unit(UnitType.Fraction, new Fraction(-2, 7), null);
            unit1 = unit1 - unit2;
            Assert.IsTrue(unit1.Fraction.Numerator == -4 && unit1.Fraction.Denomination == 7);

            unit1 = new Unit(UnitType.Integer, new Fraction(10, 7), null);
            unit2 = new Unit(UnitType.Integer, new Fraction(110, 7), null);
            unit1 = unit1 - unit2;
            Assert.IsTrue(unit1.Fraction.Numerator == -100 && unit1.Fraction.Denomination == 1);
        }

        [Test]
        public void TestMultiply()
        {
            Unit unit1 = new Unit();
            Unit unit2 = new Unit(UnitType.Fraction, new Fraction(2, 7), null);

            unit1 = unit1 * unit2;
            Assert.IsTrue(unit1.Fraction.Numerator == 2 && unit1.Fraction.Denomination == 7);

            unit1 = new Unit(UnitType.Fraction, new Fraction(-2, 7), null);
            unit1 = unit1 * unit2;
            Assert.IsTrue(unit1.Fraction.Numerator == -4 && unit1.Fraction.Denomination == 49);

            unit1 = new Unit(UnitType.Integer, new Fraction(10, 7), null);
            unit2 = new Unit(UnitType.Integer, new Fraction(110, 7), null);
            unit1 = unit1 * unit2;
            Assert.IsTrue(unit1.Fraction.Numerator == 1100 && unit1.Fraction.Denomination == 1);

            unit1 = new Unit(UnitType.Fraction, new Fraction(10, 7), null);
            unit2 = new Unit(UnitType.Fraction, new Fraction(7, -10), null);
            unit1 = unit1 * unit2;
            Assert.IsTrue(unit1.Fraction.Numerator == -1 && unit1.Fraction.Denomination == 1);
        }

        [Test]
        public void TestDivid()
        {
            Unit unit1 = new Unit();
            Unit unit2 = new Unit(UnitType.Fraction, new Fraction(2, 7), null);

            unit1 = unit1 / unit2;
            Assert.IsTrue(unit1.Fraction.Numerator == 7 && unit1.Fraction.Denomination == 2);

            unit2 = new Unit(UnitType.Fraction, new Fraction(0, 7), null);
            Exception exception = null;
            try
            {
                unit1 = unit1 / unit2;
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.AreEqual(typeof(DivideByZeroException), exception.GetType());

            unit1 = new Unit(UnitType.Fraction, new Fraction(9, -1), null);
            unit2 = new Unit(UnitType.Fraction, new Fraction(1, 9), null);
            unit1 = unit1 / unit2;
            Assert.IsTrue(unit1.Fraction.Numerator == -81 && unit1.Fraction.Denomination == 1);
        }
    }
}
