﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remonduk;
using Remonduk.Physics;

namespace TestSuite
{
	public partial class CircleTest
	{
		[TestMethod]
		public void DistanceSquaredTest()
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
		public void DistanceTest()
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
		public void ReferenceVelocityTest()
		{
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

			one = new Circle(1, 1, 2);
			two = new Circle(3, 5, 8);
			referenceV = one.ReferenceVelocity(two, time);
			Test.AreEqual(new OrderedPair(1, 2), one.ClosestPoint(two, referenceV));
			referenceV = two.ReferenceVelocity(one, time);
			Test.AreEqual(new OrderedPair(5, 8), two.ClosestPoint(one, referenceV));

			one = new Circle(1, 1, 1, 1, 2);
			two = new Circle(1, 3, 3, 0, 0);
			referenceV = one.ReferenceVelocity(two, time);
			Test.AreEqual(new OrderedPair(2.2, 3.4), one.ClosestPoint(two, referenceV));
			referenceV = two.ReferenceVelocity(one, time);
			Test.AreEqual(new OrderedPair(1.8, .6), two.ClosestPoint(one, referenceV));

			one = new Circle(4, 1, 1, 1, 2);
			two = new Circle(4, 3, 3, 0, 0);
			referenceV = one.ReferenceVelocity(two, time);
			Test.AreEqual(new OrderedPair(2.2, 3.4), one.ClosestPoint(two, referenceV));
			referenceV = two.ReferenceVelocity(one, time);
			Test.AreEqual(new OrderedPair(1.8, .6), two.ClosestPoint(one, referenceV));

			one = new Circle(4, 1, 1, 2, 1);
			two = new Circle(4, 3, 3, 1, -1);
			referenceV = one.ReferenceVelocity(two, time);
			Test.AreEqual(new OrderedPair(2.2, 3.4), one.ClosestPoint(two, referenceV));
			referenceV = two.ReferenceVelocity(one, time);
			Test.AreEqual(new OrderedPair(1.8, .6), two.ClosestPoint(one, referenceV));
		}

		[TestMethod]
		public void CollisionPointTest()
		{
			// shouldn't have to worry about overlapping circles
			double time = 1;

			Circle one = new Circle(1, 0, 0, 1, 2);
			Circle two = new Circle(2, 4, 4, 0, 0);
			OrderedPair referenceV = one.ReferenceVelocity(two, time);
			OrderedPair closest = one.ClosestPoint(two, referenceV);
			OrderedPair collision = one.CollisionPoint(closest, referenceV, two);
			double collisionTimeX = (collision.X - one.Px) / referenceV.X;
			double collisionTimeY = (collision.Y - one.Py) / referenceV.Y;
			Test.AreEqual(collisionTimeX, collisionTimeY);
			Test.AreEqual(one.Radius + two.Radius, two.Position.Magnitude(collision));

			referenceV = two.ReferenceVelocity(one, time);
			closest = two.ClosestPoint(one, referenceV);
			collision = two.CollisionPoint(closest, referenceV, one);
			collisionTimeX = (collision.X - two.Px) / referenceV.X;
			collisionTimeY = (collision.Y - two.Py) / referenceV.Y;
			Test.AreEqual(collisionTimeX, collisionTimeY);
			Test.AreEqual(one.Radius + two.Radius, one.Position.Magnitude(collision));

			one = new Circle(1, 0, 0, 12, 21);
			two = new Circle(2, 4, 4, 4, 5);
			referenceV = one.ReferenceVelocity(two, time);
			closest = one.ClosestPoint(two, referenceV);
			collision = one.CollisionPoint(closest, referenceV, two);
			collisionTimeX = (collision.X - one.Px) / referenceV.X;
			collisionTimeY = (collision.Y - one.Py) / referenceV.Y;
			Test.AreEqual(collisionTimeX, collisionTimeY);
			Test.AreEqual(one.Radius + two.Radius, two.Position.Magnitude(collision));

			referenceV = two.ReferenceVelocity(one, time);
			closest = two.ClosestPoint(one, referenceV);
			collision = two.CollisionPoint(closest, referenceV, one);
			collisionTimeX = (collision.X - two.Px) / referenceV.X;
			collisionTimeY = (collision.Y - two.Py) / referenceV.Y;
			Test.AreEqual(collisionTimeX, collisionTimeY);
			Test.AreEqual(one.Radius + two.Radius, one.Position.Magnitude(collision));

			one = new Circle(3, 0, 0, 12, 21);
			two = new Circle(4, 4, 4, 4, 5);
			referenceV = one.ReferenceVelocity(two, time);
			closest = one.ClosestPoint(two, referenceV);
			collision = one.CollisionPoint(closest, referenceV, two);
			collisionTimeX = (collision.X - one.Px) / referenceV.X;
			collisionTimeY = (collision.Y - one.Py) / referenceV.Y;
			Test.AreEqual(collisionTimeX, collisionTimeY);
			Test.AreEqual(one.Radius + two.Radius, two.Position.Magnitude(collision));

			referenceV = two.ReferenceVelocity(one, time);
			closest = two.ClosestPoint(one, referenceV);
			collision = two.CollisionPoint(closest, referenceV, one);
			collisionTimeX = (collision.X - two.Px) / referenceV.X;
			collisionTimeY = (collision.Y - two.Py) / referenceV.Y;
			Test.AreEqual(collisionTimeX, collisionTimeY);
			Test.AreEqual(one.Radius + two.Radius, one.Position.Magnitude(collision));
		}

		[TestMethod]
		public void TimeToTest()
		{
			Circle circle = new Circle(1);

			OrderedPair collisionPoint = new OrderedPair(0, 0);
			OrderedPair referenceV = new OrderedPair(0, 0);
			Test.AreEqual(0, circle.TimeTill(collisionPoint, referenceV));

			collisionPoint = new OrderedPair(0, 3);
			referenceV = new OrderedPair(0, 0);
			Test.AreEqual(true, Double.IsInfinity(circle.TimeTill(collisionPoint, referenceV)));

			collisionPoint = new OrderedPair(3, 0);
			referenceV = new OrderedPair(0, 0);
			Test.AreEqual(true, Double.IsInfinity(circle.TimeTill(collisionPoint, referenceV)));

			collisionPoint = new OrderedPair(0, 0);
			referenceV = new OrderedPair(0, 3);
			Test.AreEqual(0, circle.TimeTill(collisionPoint, referenceV));

			collisionPoint = new OrderedPair(0, 0);
			referenceV = new OrderedPair(3, 0);
			Test.AreEqual(0, circle.TimeTill(collisionPoint, referenceV));

			collisionPoint = new OrderedPair(3, 3);
			referenceV = new OrderedPair(0, 0);
			Test.AreEqual(true, Double.IsInfinity(circle.TimeTill(collisionPoint, referenceV)));

			collisionPoint = new OrderedPair(0, 0);
			referenceV = new OrderedPair(3, 3);
			Test.AreEqual(0, circle.TimeTill(collisionPoint, referenceV));

			collisionPoint = new OrderedPair(3, 0);
			referenceV = new OrderedPair(3, 0);
			Test.AreEqual(1, circle.TimeTill(collisionPoint, referenceV));

			collisionPoint = new OrderedPair(0, 3);
			referenceV = new OrderedPair(0, 3);
			Test.AreEqual(1, circle.TimeTill(collisionPoint, referenceV));

			collisionPoint = new OrderedPair(3, 0);
			referenceV = new OrderedPair(3, 3);
			Test.AreEqual(1, circle.TimeTill(collisionPoint, referenceV));

			collisionPoint = new OrderedPair(0, 3);
			referenceV = new OrderedPair(3, 3);
			Test.AreEqual(1, circle.TimeTill(collisionPoint, referenceV));

			collisionPoint = new OrderedPair(3, 3);
			referenceV = new OrderedPair(0, 3);
			Test.AreEqual(true, Double.IsInfinity(circle.TimeTill(collisionPoint, referenceV)));

			collisionPoint = new OrderedPair(3, 3);
			referenceV = new OrderedPair(3, 0);
			Test.AreEqual(true, Double.IsInfinity(circle.TimeTill(collisionPoint, referenceV)));

			collisionPoint = new OrderedPair(3, 3);
			referenceV = new OrderedPair(3, 3);
			Test.AreEqual(1, circle.TimeTill(collisionPoint, referenceV));
		}

		[TestMethod]
		public void CollisionTimeTest()
		{
			// this returns weird values for overlapping circles

			double time = 1;
			Circle one = new Circle(1);
			Circle two = new Circle(2);
			OrderedPair referenceV = one.ReferenceVelocity(two, time);
			OrderedPair closest = one.ClosestPoint(two, referenceV);
			Test.AreEqual(Double.PositiveInfinity, one.CollisionTime(closest, referenceV, two, time));

			one = new Circle(1, 0, 0, 1, 1);
			two = new Circle(2, 0, 0, 0, 0);
			referenceV = one.ReferenceVelocity(two, time);
			closest = one.ClosestPoint(two, referenceV);
			Test.AreEqual(Double.PositiveInfinity, one.CollisionTime(closest, referenceV, two, time));

			one = new Circle(1, 0, 0, 1, 1);
			two = new Circle(2, 0, 0, 1, 1);
			referenceV = one.ReferenceVelocity(two, time);
			closest = one.ClosestPoint(two, referenceV);
			Test.AreEqual(Double.PositiveInfinity, one.CollisionTime(closest, referenceV, two, time));

			one = new Circle(1, 0, 0, 1, 1);
			two = new Circle(1, 2, 2, 0, 0);
			referenceV = one.ReferenceVelocity(two, time);
			closest = one.ClosestPoint(two, referenceV);
			double collisionTime = one.CollisionTime(closest, referenceV, two, time);
			one.UpdatePosition(collisionTime);
			two.UpdatePosition(collisionTime);
			Test.AreEqual(one.Radius + two.Radius, one.Distance(two));

			one = new Circle(1, 0, 0, 3, 3);
			two = new Circle(2, 3, 3, 0, 0);
			referenceV = one.ReferenceVelocity(two, time);
			closest = one.ClosestPoint(two, referenceV);
			collisionTime = one.CollisionTime(closest, referenceV, two, time);
			one.UpdatePosition(collisionTime);
			two.UpdatePosition(collisionTime);
			Test.AreEqual(one.Radius + two.Radius, one.Distance(two));

			one = new Circle(1, 0, 0, 1.5, 1.5);
			two = new Circle(2, 3, 3, -1.5, -1.5);
			referenceV = one.ReferenceVelocity(two, time);
			closest = one.ClosestPoint(two, referenceV);
			collisionTime = one.CollisionTime(closest, referenceV, two, time);
			one.UpdatePosition(collisionTime);
			two.UpdatePosition(collisionTime);
			Test.AreEqual(one.Radius + two.Radius, one.Distance(two));
		}

		[TestMethod]
		public void CrossingTest()
		{
			double time = 1;
			Circle one = new Circle(1);
			Circle two = new Circle(2);
			Test.AreEqual(Double.PositiveInfinity, one.Crossing(two, time));

			one = new Circle(1, 0, 0, 1, 0);
			two = new Circle(1, 3, 3, 1, 0);
			Test.AreEqual(Double.PositiveInfinity, one.Crossing(two, time));
		}

		[TestMethod]
		public void CollidingTest2()
		{
			Circle one = new Circle(1, 0, 0, 0, 0, 0, 0);
			Circle two = new Circle(1, 0, 0, 0, 0, 0, 0);
			double time = 1;

			Test.AreEqual(Double.PositiveInfinity, one.Colliding(one, time));
			Test.AreEqual(Double.PositiveInfinity, two.Colliding(two, time));

			Test.AreEqual(Double.PositiveInfinity, one.Colliding(two, time));
			Test.AreEqual(Double.PositiveInfinity, two.Colliding(one, time));

			two = new Circle(1, 1, 1);
			Test.AreEqual(Double.PositiveInfinity, one.Colliding(two, time));
			Test.AreEqual(Double.PositiveInfinity, two.Colliding(one, time));

			two = new Circle(1, 0, 2);
			Test.AreEqual(Double.PositiveInfinity, one.Colliding(two, time));
			Test.AreEqual(Double.PositiveInfinity, two.Colliding(one, time));

			two = new Circle(1, 1, 1, -1, -1);
			Test.AreEqual(0, one.Colliding(two, time));
			Test.AreEqual(0, two.Colliding(one, time));

			two = new Circle(1, 1, 1, 1, 1);
			Test.AreEqual(Double.PositiveInfinity, one.Colliding(two, time));
			Test.AreEqual(Double.PositiveInfinity, two.Colliding(one, time));

			two = new Circle(1, 2, 2);
			Test.AreEqual(Double.PositiveInfinity, one.Colliding(two, time));
			Test.AreEqual(Double.PositiveInfinity, two.Colliding(one, time));

			two.SetVelocity(2, 0);
			Test.AreEqual(Double.PositiveInfinity, one.Colliding(two, time));
			Test.AreEqual(Double.PositiveInfinity, two.Colliding(one, time));

			two.SetVelocity(0, 2);
			Test.AreEqual(Double.PositiveInfinity, one.Colliding(two, time));
			Test.AreEqual(Double.PositiveInfinity, two.Colliding(one, time));

			two.SetVelocity(-2, 0);
			Test.AreEqual(1, one.Colliding(two, time));
			Test.AreEqual(1, two.Colliding(one, time));

			two.SetVelocity(0, -2);
			Test.AreEqual(1, one.Colliding(two, time));
			Test.AreEqual(1, two.Colliding(one, time));

			two.SetVelocity(1.42, 1.42);
			Test.AreEqual(Double.PositiveInfinity, one.Colliding(two, time));
			Test.AreEqual(Double.PositiveInfinity, two.Colliding(one, time));

			two.SetVelocity(1.42, -1.42);
			Test.AreEqual(Double.PositiveInfinity, one.Colliding(two, time));
			Test.AreEqual(Double.PositiveInfinity, two.Colliding(one, time));

			two.SetVelocity(-1.42, 1.42);
			Test.AreEqual(Double.PositiveInfinity, one.Colliding(two, time));
			Test.AreEqual(Double.PositiveInfinity, two.Colliding(one, time));

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
		public void CollidingTest1()
		{
			Circle one = new Circle(1);
			Circle two = new Circle(1);
			
			Test.AreEqual(false, one.Colliding(one));
			Test.AreEqual(false, two.Colliding(two));

			Test.AreEqual(true, one.Colliding(two));
			Test.AreEqual(true, two.Colliding(one));
			
			two = new Circle(2);
			Test.AreEqual(true, one.Colliding(two));
			Test.AreEqual(true, two.Colliding(one));

			two = new Circle(1, 1, 1);
			Test.AreEqual(true, one.Colliding(two));
			Test.AreEqual(true, two.Colliding(one));

			two = new Circle(1, 1, 1, 1, 1);
			Test.AreEqual(true, one.Colliding(two));
			Test.AreEqual(true, two.Colliding(one));

			two = new Circle(1, 2, 2);
			Test.AreEqual(false, one.Colliding(two));
			Test.AreEqual(false, two.Colliding(one));

			two = new Circle(1, 1.42, 1.42);
			Test.AreEqual(false, one.Colliding(two));
			Test.AreEqual(false, two.Colliding(one));

			two = new Circle(1, Math.Sqrt(2), Math.Sqrt(2));
			Test.AreEqual(true, one.Colliding(two));
			Test.AreEqual(true, two.Colliding(one));
		}

		[TestMethod]
		public void CollideWithTest()
		{
			Circle one = new Circle(1, 0, 0);
			Circle two = new Circle(3, 6, 6);
			Circle three = new Circle(1, -2, -2, 8, 8);
			Circle four = new Circle(2, 8, 8, -16, -16);
			List<Circle> circles = new List<Circle>();
			OrderedPair velocity;

			circles.Add(one);
			velocity = one.CollideWith(circles);
			Test.AreEqual(new OrderedPair(0, 0), velocity);

			circles.Add(two);
			velocity = one.CollideWith(circles);
			Test.AreEqual(new OrderedPair(0, 0), velocity);

			velocity = two.CollideWith(circles);
			Test.AreEqual(new OrderedPair(0, 0), velocity);

			circles.Add(three);
			velocity = one.CollideWith(circles);
			Test.AreEqual(new OrderedPair(8, 8), velocity);

			velocity = two.CollideWith(circles);
			Test.AreEqual(new OrderedPair(8, 8), velocity);

			velocity = three.CollideWith(circles);
			Test.AreEqual(new OrderedPair(0, 0), velocity);

			circles.Add(four);
			velocity = one.CollideWith(circles);
			Test.AreEqual(new OrderedPair(-8, -8), velocity);

			velocity = two.CollideWith(circles);
			Test.AreEqual(new OrderedPair(-8, -8), velocity);

			velocity = three.CollideWith(circles);
			Test.AreEqual(new OrderedPair(-16, -16), velocity);

			velocity = four.CollideWith(circles);
			Test.AreEqual(new OrderedPair(8, 8), velocity);

			circles = new List<Circle>();
			one = new Circle(2, 4, 3, 0, 0, 2);
			two = new Circle(3, 0, 0, 10, 20, 5);
			circles.Add(one);
			circles.Add(two);

			velocity = one.CollideWith(circles);
			Out.WriteLine("" + velocity);
			Test.AreEqual(new OrderedPair(44.7213595499958, 33.54101966242969), velocity);

			velocity = two.CollideWith(circles);
			Test.AreEqual(new OrderedPair(0, 0), velocity);

			circles = new List<Circle>();
			one = new Circle(1, 0, 0, 15, 5, 2);
			two = new Circle(1, 3, 4, -10, -20, 5);
			circles.Add(one);
			circles.Add(two);

			double before = one.Velocity.Magnitude() * one.Mass + two.Velocity.Magnitude() * two.Mass;
			velocity = one.CollideWith(circles);
			double after = velocity.Magnitude() * one.Mass;
			Test.AreEqual(two.Velocity.Magnitude() * two.Mass, velocity.Magnitude() * one.Mass);

			velocity = two.CollideWith(circles);
			after += velocity.Magnitude() * two.Mass;
			Test.AreEqual(one.Velocity.Magnitude() * one.Mass, velocity.Magnitude() * two.Mass);

			Test.AreEqual(before, after);
		}
	}
}
