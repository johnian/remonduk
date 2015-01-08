using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remonduk;
using Remonduk.Physics;

namespace TestSuite
{
	public partial class CircleTest
	{
		[TestMethod]
		public void NextVelocityTest()
		{
			Circle circle = new Circle(1);
			double time = 0;
			Test.AreEqual(new OrderedPair(0, 0), circle.NextVelocity(time));

			circle.SetVelocity(1, 2);
			Test.AreEqual(new OrderedPair(1, 2), circle.NextVelocity(time));

			circle.SetAcceleration(3, 4);
			Test.AreEqual(new OrderedPair(1, 2), circle.NextVelocity(time));

			circle = new Circle(1);
			time = .5;
			Test.AreEqual(new OrderedPair(0, 0), circle.NextVelocity(time));

			circle.SetVelocity(1, 2);
			Test.AreEqual(new OrderedPair(1, 2), circle.NextVelocity(time));

			circle.SetAcceleration(3, 4);
			Test.AreEqual(new OrderedPair(2.5, 4), circle.NextVelocity(time));

			circle = new Circle(1);
			time = 1;
			Test.AreEqual(new OrderedPair(0, 0), circle.NextVelocity(time));

			circle.SetVelocity(1, 2);
			Test.AreEqual(new OrderedPair(1, 2), circle.NextVelocity(time));

			circle.SetAcceleration(3, 4);
			Test.AreEqual(new OrderedPair(4, 6), circle.NextVelocity(time));
		}

		[TestMethod]
		public void NextPositionTest0()
		{
			Circle circle = new Circle(1);
			double time = 1;
			Test.AreEqual(new OrderedPair(0, 0), circle.NextPosition(time));

			circle.SetVelocity(1, 2);
			Test.AreEqual(new OrderedPair(1, 2), circle.NextPosition(time));

			circle.SetAcceleration(3, 4);
			Test.AreEqual(new OrderedPair(3 / 2.0 + 1,
				4 / 2.0 + 2), circle.NextPosition(time));

			circle.SetPosition(5, 6);
			Test.AreEqual(new OrderedPair(3 / 2.0 + 1 + 5,
				4 / 2.0 + 2 + 6), circle.NextPosition(time));
		}

		[TestMethod]
		public void NextPositionTest1()
		{
			Circle circle = new Circle(1);
			double time = 0;
			Test.AreEqual(new OrderedPair(0, 0), circle.NextPosition(time));

			circle.SetVelocity(1, 2);
			Test.AreEqual(new OrderedPair(0, 0), circle.NextPosition(time));

			circle.SetAcceleration(3, 4);
			Test.AreEqual(new OrderedPair(0, 0), circle.NextPosition(time));

			circle.SetPosition(5, 6);
			Test.AreEqual(new OrderedPair(5, 6), circle.NextPosition(time));

			circle = new Circle(1);
			time = .5;
			Test.AreEqual(new OrderedPair(0, 0), circle.NextPosition(time));

			circle.SetVelocity(1, 2);
			Test.AreEqual(new OrderedPair(.5, 1), circle.NextPosition(time));

			circle.SetAcceleration(3, 4);
			Test.AreEqual(new OrderedPair(3 * .5 * .5 / 2 + 1 * .5,
				4 * .5 * .5 / 2 + 2 * .5), circle.NextPosition(time));

			circle.SetPosition(5, 6);
			Test.AreEqual(new OrderedPair(3 * .5 * .5 / 2 + 1 * .5 + 5,
				4 * .5 * .5 / 2 + 2 * .5 + 6), circle.NextPosition(time));

			circle = new Circle(1);
			time = 1;
			Test.AreEqual(new OrderedPair(0, 0), circle.NextPosition(time));

			circle.SetVelocity(1, 2);
			Test.AreEqual(new OrderedPair(1, 2), circle.NextPosition(time));

			circle.SetAcceleration(3, 4);
			Test.AreEqual(new OrderedPair(3 / 2.0 + 1,
				4 / 2.0 + 2), circle.NextPosition(time));

			circle.SetPosition(5, 6);
			Test.AreEqual(new OrderedPair(3 / 2.0 + 1 + 5,
				4 / 2.0 + 2 + 6), circle.NextPosition(time));
		}

		[TestMethod]
		public void DistanceSquared()
		{
			Circle one = new Circle(1, 0, 0);
			Circle two = new Circle(1, 3, 4);
			Test.AreEqual(25, one.DistanceSquared(two));
			Test.AreEqual(25, two.DistanceSquared(one));

			one = new Circle(1, 1, 2);
			two = new Circle(1, 7, 10);
			Test.AreEqual(100, one.DistanceSquared(two));
			Test.AreEqual(100, two.DistanceSquared(one));
		}

		[TestMethod]
		public void Distance()
		{
			Circle one = new Circle(1, 0, 0);
			Circle two = new Circle(1, 3, 4);
			Test.AreEqual(5, one.Distance(two));
			Test.AreEqual(5, two.Distance(one));

			one = new Circle(1, 1, 2);
			two = new Circle(1, 7, 10);
			Test.AreEqual(10, one.Distance(two));
			Test.AreEqual(10, two.Distance(one));
		}

		[TestMethod]
		public void ReferenceVelocity() {
			Circle one = new Circle(1);
			Circle two = new Circle(2);
			double time = 0;
			Test.AreEqual(new OrderedPair(0, 0), one.ReferenceVelocity(two, time));
			Test.AreEqual(new OrderedPair(0, 0), two.ReferenceVelocity(one, time));

			time = .5;
			Test.AreEqual(new OrderedPair(0, 0), one.ReferenceVelocity(two, time));
			Test.AreEqual(new OrderedPair(0, 0), two.ReferenceVelocity(one, time));

			time = 1;
			Test.AreEqual(new OrderedPair(0, 0), one.ReferenceVelocity(two, time));
			Test.AreEqual(new OrderedPair(0, 0), two.ReferenceVelocity(one, time));

			one = new Circle(1, 1, 2, 1, 2);
			two = new Circle(3, 5, 8, 5, 8);
			time = 0;
			Test.AreEqual(new OrderedPair(-4, -6), one.ReferenceVelocity(two, time));
			Test.AreEqual(new OrderedPair(4, 6), two.ReferenceVelocity(one, time));

			time = .5;
			Test.AreEqual(new OrderedPair(-4, -6), one.ReferenceVelocity(two, time));
			Test.AreEqual(new OrderedPair(4, 6), two.ReferenceVelocity(one, time));

			time = 1;
			Test.AreEqual(new OrderedPair(-4, -6), one.ReferenceVelocity(two, time));
			Test.AreEqual(new OrderedPair(4, 6), two.ReferenceVelocity(one, time));

			one = new Circle(1, 1, 2, 1, 2, 3, 5);
			two = new Circle(3, 5, 8, 5, 8, 13, 21);
			time = 0;
			Test.AreEqual(new OrderedPair(-4, -6), one.ReferenceVelocity(two, time));
			Test.AreEqual(new OrderedPair(4, 6), two.ReferenceVelocity(one, time));

			time = .5;
			Test.AreEqual(new OrderedPair(-9, -14), one.ReferenceVelocity(two, time));
			Test.AreEqual(new OrderedPair(9, 14), two.ReferenceVelocity(one, time));

			time = 1;
			Test.AreEqual(new OrderedPair(-14, -22), one.ReferenceVelocity(two, time));
			Test.AreEqual(new OrderedPair(14, 22), two.ReferenceVelocity(one, time));
		}

		[TestMethod]
		public void ClosestPointTest()
		{
			Circle one = new Circle(1);
			Circle two = new Circle(1);
			double time = 1;
			OrderedPair referenceV = one.ReferenceVelocity(two, time);
			Test.AreEqual(new OrderedPair(0, 0), one.ClosestPoint(two, referenceV));
			referenceV = two.ReferenceVelocity(one, time);
			Test.AreEqual(new OrderedPair(0, 0), two.ClosestPoint(one, referenceV));

			one = new Circle(1, 1, 2, 3, 5);
			two = new Circle(4, 8, 13, 0, 0);

			referenceV = one.ReferenceVelocity(two, time);
			Test.AreEqual(new OrderedPair(7.70588235294118, 13.1764705882353), one.ClosestPoint(two, referenceV));
			referenceV = two.ReferenceVelocity(one, time);
			Test.AreEqual(new OrderedPair(1.29411764705882, 1.82352941176471), two.ClosestPoint(one, referenceV));
		}

		[TestMethod]
		public void collidingTestT()
		{
			Test.AreEqual(true, false);
		}

		[TestMethod]
		public void collidingTestTT()
		{
			Circle one = new Circle(1, 0, 0, 0, 0, 0, 0);
			Circle two = new Circle(1, 0, 0, 0, 0, 0, 0);

			double time = 1;
			Test.AreEqual(true, Double.IsInfinity(one.Colliding(one, time)));
			Test.AreEqual(true, Double.IsInfinity(two.Colliding(two, time)));

			Test.AreEqual(0, one.Colliding(two, time));
			Test.AreEqual(0, two.Colliding(one, time));

			two = new Circle(1, 0, 2);

			Test.AreEqual(0, one.Colliding(two, time));
			Test.AreEqual(0, two.Colliding(one, time));

			two = new Circle(1, 2, 2);
			Test.AreEqual(true, Double.IsInfinity(one.Colliding(two, time)));
			Test.AreEqual(true, Double.IsInfinity(two.Colliding(one, time)));

			two.SetVelocity(2, 0);
			Test.AreEqual(true, Double.IsInfinity(one.Colliding(two, time)));
			Test.AreEqual(true, Double.IsInfinity(two.Colliding(one, time)));

			two.SetVelocity(0, 2);
			Test.AreEqual(true, Double.IsInfinity(one.Colliding(two, time)));
			Test.AreEqual(true, Double.IsInfinity(two.Colliding(one, time)));

			two.SetVelocity(-2, 0);
			Test.AreEqual(1, one.Colliding(two, time));
			Test.AreEqual(1, two.Colliding(one, time));

			two.SetVelocity(0, -2);
			Test.AreEqual(1, one.Colliding(two, time));
			Test.AreEqual(1, two.Colliding(one, time));

			two.SetVelocity(1.42, 1.42);
			Test.AreEqual(true, Double.IsInfinity(one.Colliding(two, time)));
			Test.AreEqual(true, Double.IsInfinity(two.Colliding(one, time)));

			two.SetVelocity(1.42, -1.42);
			Test.AreEqual(true, Double.IsInfinity(one.Colliding(two, time)));
			Test.AreEqual(true, Double.IsInfinity(two.Colliding(one, time)));

			two.SetVelocity(-1.42, 1.42);
			Test.AreEqual(true, Double.IsInfinity(one.Colliding(two, time)));
			Test.AreEqual(true, Double.IsInfinity(two.Colliding(one, time)));

			two.SetVelocity(2 * Math.Sqrt(2) - 4, 2 * Math.Sqrt(2) - 4);
			Test.AreEqual(.5, one.Colliding(two, time));
			Test.AreEqual(.5, two.Colliding(one, time));

			one.SetVelocity(0, 2);
			two.SetVelocity(0, 0);
			Test.AreEqual(1, one.Colliding(two, time));
			Test.AreEqual(1, two.Colliding(one, time));

			one.SetVelocity(2 - Math.Sqrt(2), 2 - Math.Sqrt(2));
			two.SetVelocity(Math.Sqrt(2) - 2, Math.Sqrt(2) - 2);
			Test.AreEqual(.5, one.Colliding(two, time));
			Test.AreEqual(.5, two.Colliding(one, time));
		}

		[TestMethod]
		public void collideWithTest()
		{
			Test.AreEqual(true, false);
		}

		[TestMethod]
		public void crossingTest()
		{
			Test.AreEqual(true, false);
		}
		
		//[TestMethod]
		//public void magnitude()
		//{
		//	Test.AreEqual(10, OrderedPair.Magnitude(6.0, 8.0));
		//}

		//[TestMethod]
		//public void angle()
		//{
		//	Test.AreEqual(0.0, OrderedPair.Angle(0.0, 1.0));
		//	Test.AreEqual(Math.PI / 4, OrderedPair.Angle(1.0, 1.0));
		//	Test.AreEqual(2 * Math.PI / 4, OrderedPair.Angle(1.0, 0.0));
		//	Test.AreEqual(3 * Math.PI / 4, OrderedPair.Angle(1.0, -1.0));
		//	Test.AreEqual(4 * Math.PI / 4, OrderedPair.Angle(0.0, -1.0));
		//	Test.AreEqual(5 * Math.PI / 4, OrderedPair.Angle(-1.0, -1.0));
		//	Test.AreEqual(6 * Math.PI / 4, OrderedPair.Angle(-1.0, 0.0));
		//	Test.AreEqual(7 * Math.PI / 4, OrderedPair.Angle(-1.0, 1.0));
		//}
	}
}
