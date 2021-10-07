using NUnit.Framework;
using System;
using Tools;

namespace ToolsUnitTest
{
    public class FractionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGCD()
        {
            long a = 0;
            long b = 0;
            Assert.AreEqual(Fraction.GCD(a, b), 0);

            a = 1;
            b = 0;
            Assert.AreEqual(Fraction.GCD(a, b), 1);

            a = 1;
            b = 1;
            Assert.AreEqual(Fraction.GCD(a, b), 1);

            a = -1;
            b = 0;
            Assert.AreEqual(Fraction.GCD(a, b), -1);

            a = 0;
            b = -1;
            Assert.AreEqual(Fraction.GCD(a, b), -1);

            a = -15;
            b = 3;
            Assert.AreEqual(Fraction.GCD(a, b), 3);

            a = -9;
            b = 7;
            Assert.AreEqual(Fraction.GCD(a, b), 1);
        }

        [Test]
        public void TestReduce()
        {
            Fraction fraction = new Fraction(16, 78);

            fraction.Reduce();
            Assert.IsTrue(fraction.Numerator == 8 && fraction.Denomination == 39);

            fraction = new Fraction(0, 78);
            fraction.Reduce();
            Assert.IsTrue(fraction.Numerator == 0 && fraction.Denomination == 1);

            fraction = new Fraction(-16, -78);
            fraction.Reduce();
            Assert.IsTrue(fraction.Numerator == 8 && fraction.Denomination == 39);

            fraction = new Fraction(16, -78);
            fraction.Reduce();
            Assert.IsTrue(fraction.Numerator == -8 && fraction.Denomination == 39);

            fraction = new Fraction(-16, 78);
            fraction.Reduce();
            Assert.IsTrue(fraction.Numerator == -8 && fraction.Denomination == 39);
        }

        [Test]
        public void TestAddition()
        {
            Fraction fraction1 = new Fraction();
            Fraction fraction2 = new Fraction(2, 7);

            fraction1 = fraction1 + fraction2;
            Assert.IsTrue(fraction1.Numerator == 9 && fraction1.Denomination == 7);

            fraction1 = new Fraction(-2, 7);
            fraction1 = fraction1 + fraction2;
            Assert.IsTrue(fraction1.Numerator == 0 && fraction1.Denomination == 1);

            fraction1 = new Fraction(1, -9);
            fraction2 = new Fraction(1, 9);
            fraction1 = fraction1 + fraction2;
            Assert.IsTrue(fraction1.Numerator == 0 && fraction1.Denomination == 1);

            fraction1 = new Fraction(1, 7);
            fraction2 = new Fraction(7, 9);
            fraction1 = fraction1 + fraction2;
            Assert.IsTrue(fraction1.Numerator == 58 && fraction1.Denomination == 63);
        }

        [Test]
        public void TestSubscribe()
        {
            Fraction fraction1 = new Fraction();
            Fraction fraction2 = new Fraction(2, 7);

            fraction1 = fraction1 - fraction2;
            Assert.IsTrue(fraction1.Numerator == 5 && fraction1.Denomination == 7);

            fraction1 = new Fraction(2, 7);
            fraction1 = fraction1 - fraction2;
            Assert.IsTrue(fraction1.Numerator == 0 && fraction1.Denomination == 1);

            fraction1 = new Fraction(1, -9);
            fraction2 = new Fraction(1, 9);
            fraction1 = fraction1 - fraction2;
            Assert.IsTrue(fraction1.Numerator == -2 && fraction1.Denomination == 9);

            fraction1 = new Fraction(1, 7);
            fraction2 = new Fraction(7, 9);
            fraction1 = fraction1 - fraction2;
            Assert.IsTrue(fraction1.Numerator == -40 && fraction1.Denomination == 63);

            fraction1 = new Fraction(1, 7);
            fraction2 = new Fraction(-1, 7);
            fraction1 = fraction1 - fraction2;
            Assert.IsTrue(fraction1.Numerator == 2 && fraction1.Denomination == 7);
        }

        [Test]
        public void TestMultiply()
        {
            Fraction fraction1 = new Fraction();
            Fraction fraction2 = new Fraction(2, 7);

            fraction1 = fraction1 * fraction2;
            Assert.IsTrue(fraction1.Numerator == 2 && fraction1.Denomination == 7);

            fraction1 = new Fraction(0, 7);
            fraction1 = fraction1 * fraction2;
            Assert.IsTrue(fraction1.Numerator == 0 && fraction1.Denomination == 1);

            fraction1 = new Fraction(9, -1);
            fraction2 = new Fraction(1, 9);
            fraction1 = fraction1 * fraction2;
            Assert.IsTrue(fraction1.Numerator == -1 && fraction1.Denomination == 1);

            fraction1 = new Fraction(-1, 7);
            fraction2 = new Fraction(7, 9);
            fraction1 = fraction1 * fraction2;
            Assert.IsTrue(fraction1.Numerator == -1 && fraction1.Denomination == 9);
        }

        [Test]
        public void TestDivid()
        {
            Fraction fraction1 = new Fraction();
            Fraction fraction2 = new Fraction(2, 7);

            fraction1 = fraction1 / fraction2;
            Assert.IsTrue(fraction1.Numerator == 7 && fraction1.Denomination == 2);

            fraction2 = new Fraction(0, 7);
            Exception exception = null;
            try
            {
                fraction1 = fraction1 / fraction2;
            }
            catch (Exception e)
            {
                exception = e;
            }
            Assert.AreEqual(typeof(DivideByZeroException), exception.GetType());

            fraction1 = new Fraction(9, -1);
            fraction2 = new Fraction(1, 9);
            fraction1 = fraction1 / fraction2;
            Assert.IsTrue(fraction1.Numerator == -81 && fraction1.Denomination == 1);

            fraction1 = new Fraction(-1, 7);
            fraction2 = new Fraction(7, 9);
            fraction1 = fraction1 / fraction2;
            Assert.IsTrue(fraction1.Numerator == -9 && fraction1.Denomination == 49);
        }

        [Test]
        public void TestToString()
        {
            Fraction fraction1 = new Fraction();
            Assert.AreEqual(fraction1.ToString(), "1");

            fraction1 = new Fraction(6, -66);
            Assert.AreEqual(fraction1.ToString(), "_-1~11");

            fraction1 = new Fraction(18, -5);
            Assert.AreEqual(fraction1.ToString(), "-3U_3~5");

            fraction1 = new Fraction(18, 5);
            Assert.AreEqual(fraction1.ToString(), "3U_3~5");
        }

        [Test]
        public void TestToHTML()
        {
            Fraction fraction1 = new Fraction();
            Assert.AreEqual(fraction1.ToHTML(), "1");

            fraction1 = new Fraction(6, -66);
            Assert.AreEqual(fraction1.ToHTML(),
                "<span class=\"texthidden\">_</span><sup>-1</sup>~<sub>11</sub>");

            fraction1 = new Fraction(18, -5);
            Assert.AreEqual(fraction1.ToHTML(),
                "-3<span class=\"texthidden\">U_</span><sup>3</sup>~<sub>5</sub>");

            fraction1 = new Fraction(18, 5);
            Assert.AreEqual(fraction1.ToHTML(), 
                "3<span class=\"texthidden\">U_</span><sup>3</sup>~<sub>5</sub>");
        }
    }
}