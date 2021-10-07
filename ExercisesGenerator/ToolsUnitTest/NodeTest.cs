using NUnit.Framework;
using System;
using System.Collections.Generic;
using Tools;

namespace ToolsUnitTest
{
    public class NodeTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestToString()
        {
            Node node = new Node(new Unit(UnitType.Integer, new Fraction(), null));
            Assert.AreEqual(node.ToString(), "1()()");
            node.RightChild = new Node(new Unit(UnitType.Fraction, new Fraction(49, 3), null));
            Assert.AreEqual(node.ToString(), "1()(16U_1~3()())");
        }

        [Test]
        public void TestCompareTo()
        {
            Node node = new Node(new Unit(UnitType.Integer, new Fraction(), null));
            Assert.IsTrue(node.CompareTo(null) > 0);

            Node node1 = new Node();
            Assert.IsTrue(node1.CompareTo(node) == 0);

            node1 = new Node(new Unit(UnitType.Integer, new Fraction(), null));
            Assert.IsTrue(node.CompareTo(node1) == 0);

            node.LeftChild = new Node(new Unit(UnitType.Operator,
                null, new Operator('+', 2, 1)));
            node.RightChild = new Node(new Unit(UnitType.Operator,
                null, new Operator('+', 2, 1)));

            node1.LeftChild = new Node(new Unit(UnitType.Operator,
                null, new Operator('+', 2, 1)));
            node1.RightChild = new Node(new Unit(UnitType.Operator,
                null, new Operator('+', 2, 1)));

            Assert.IsTrue(node.CompareTo(node1) == 0);

            node.LeftChild.LeftChild = new Node(new Unit(UnitType.Integer, new Fraction(), null));
            Assert.IsTrue(node.CompareTo(node1) > 0);

            node1.RightChild = new Node(new Unit(UnitType.Fraction,
                new Fraction(178, 499), new Operator('+', 2, 1)));
            Assert.IsTrue(node.CompareTo(node1) > 0);
        }
    }
}
