using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remonduk;
using Remonduk.Physics;

namespace TestSuite
{
	[TestClass]
	public class OrderedPairTest
	{
		[TestMethod]
		public void OrderedPairTest0()
		{
			OrderedPair orderedPair = new OrderedPair();
			Test.AreEqual(0, orderedPair.X);
			Test.AreEqual(0, orderedPair.Y);
		}

		[TestMethod]
		public void OrderedPairTest2()
		{
			OrderedPair orderedPair = new OrderedPair(0, 0);
			Test.AreEqual(0, orderedPair.X);
			Test.AreEqual(0, orderedPair.Y);

			orderedPair = new OrderedPair(3, 4);
			Test.AreEqual(3, orderedPair.X);
			Test.AreEqual(4, orderedPair.Y);

			orderedPair = new OrderedPair(-12.3, -45.67);
			Test.AreEqual(-12.3, orderedPair.X);
			Test.AreEqual(-45.67, orderedPair.Y);
		}

		[TestMethod]
		public void SetXYTest2()
		{
			OrderedPair orderedPair = new OrderedPair(0, 0);
			orderedPair.SetXY(123, 456);
			Test.AreEqual(123, orderedPair.X);
			Test.AreEqual(456, orderedPair.Y);

			orderedPair.SetXY(-12.3, -45.6);
			Test.AreEqual(-12.3, orderedPair.X);
			Test.AreEqual(-45.6, orderedPair.Y);

			orderedPair.SetXY(0, 0);
			Test.AreEqual(0, orderedPair.X);
			Test.AreEqual(0, orderedPair.Y);
		}

		[TestMethod]
		public void SetXYTest1()
		{
			OrderedPair orderedPair = new OrderedPair(3, 4);
			orderedPair.SetXY(0);
			Test.AreEqual(5, orderedPair.X);
			Test.AreEqual(0, orderedPair.Y);

			orderedPair.SetXY(-Math.PI / 4);
			Test.AreEqual(5 * Math.Sqrt(2) / 2, orderedPair.X);
			Test.AreEqual(-5 * Math.Sqrt(2) / 2, orderedPair.Y);

			orderedPair.SetXY(Math.PI / 2);
			Test.AreEqual(0, orderedPair.X);
			Test.AreEqual(5, orderedPair.Y);
		}

		[TestMethod]
		public void MagnitudeSquaredTest2()
		{

		}

		[TestMethod]
		public void MagnitudeSquaredTest1()
		{

		}

		[TestMethod]
		public void MagnitudeTest2()
		{
			Test.AreEqual(10, OrderedPair.Magnitude(6.0, 8.0));
		}

		[TestMethod]
		public void MagnitudeTest1()
		{

		}

		[TestMethod]
		public void MagnitudeTest0()
		{

		}

		[TestMethod]
		public void AngleTest2()
		{
			Test.AreEqual(0, OrderedPair.Angle(0, 0));
			Test.AreEqual(0, OrderedPair.Angle(0.0, 1.0));
			Test.AreEqual(1 * Math.PI / 4, OrderedPair.Angle(1.0, 1.0));
			Test.AreEqual(2 * Math.PI / 4, OrderedPair.Angle(1.0, 0.0));
			Test.AreEqual(3 * Math.PI / 4, OrderedPair.Angle(1.0, -1.0));
			Test.AreEqual(4 * Math.PI / 4, OrderedPair.Angle(0.0, -1.0));
			Test.AreEqual(5 * Math.PI / 4, OrderedPair.Angle(-1.0, -1.0));
			Test.AreEqual(6 * Math.PI / 4, OrderedPair.Angle(-1.0, 0.0));
			Test.AreEqual(7 * Math.PI / 4, OrderedPair.Angle(-1.0, 1.0));
		}

		[TestMethod]
		public void AngleTest1()
		{

		}

		[TestMethod]
		public void AngleTest0()
		{

		}

		[TestMethod]
		public void EqualsTest()
		{

		}
	}
}
