using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using remonduk;

namespace TestSuite
{
	[TestClass]
	public class CircleTest
	{
		Constants constants = Constants.Instance;

		[TestMethod]
		public void CircleTestXYR()
		{
			// check that all other fields are set correctly
			Circle circle = new Circle(2, 3, 5);

			Test.AreEqual(2, circle.radius);
			Test.AreEqual(3, circle.px);
			Test.AreEqual(5, circle.py);
			Test.AreEqual(Circle.MASS, circle.mass);

			Test.AreEqual(Circle.VX, circle.vx);
			Test.AreEqual(Circle.VY, circle.vy);

			Test.AreEqual(Circle.AX, circle.ax);
			Test.AreEqual(Circle.AY, circle.ay);

			Test.AreEqual(Circle.TARGET, circle.target);
			Test.AreEqual(Circle.MIN_DIST, circle.min_dist);
			Test.AreEqual(Circle.MAX_DIST, circle.max_dist);

			circle = new Circle(2, 3, 5, 8);
			Test.AreEqual(8, circle.mass);
		}

		[TestMethod]
		public void CircleTestXYRV()
		{
			Circle circle = new Circle(2, 3, 5, 8, 13);

			Test.AreEqual(2, circle.radius);
			Test.AreEqual(3, circle.px);
			Test.AreEqual(5, circle.py);
			Test.AreEqual(Circle.MASS, circle.mass);

			Test.AreEqual(8, circle.vx);
			Test.AreEqual(13, circle.vy);

			Test.AreEqual(Circle.AX, circle.ax);
			Test.AreEqual(Circle.AY, circle.ay);

			Test.AreEqual(Circle.TARGET, circle.target);
			Test.AreEqual(Circle.MIN_DIST, circle.min_dist);
			Test.AreEqual(Circle.MAX_DIST, circle.max_dist);

			circle = new Circle(2, 3, 5, 8, 13, 21);
			Test.AreEqual(21, circle.mass);
		}

		[TestMethod]
		public void CircleTestXYRVA()
		{
			Circle circle = new Circle(2, 3, 5, 8, 13, 21, 34);

			Test.AreEqual(2, circle.radius);
			Test.AreEqual(3, circle.px);
			Test.AreEqual(5, circle.py);
			Test.AreEqual(Circle.MASS, circle.mass);

			Test.AreEqual(8, circle.vx);
			Test.AreEqual(13, circle.vy);

			Test.AreEqual(21, circle.ax);
			Test.AreEqual(34, circle.ay);

			Test.AreEqual(Circle.TARGET, circle.target);
			Test.AreEqual(Circle.MIN_DIST, circle.min_dist);
			Test.AreEqual(Circle.MAX_DIST, circle.max_dist);

			circle = new Circle(2, 3, 5, 8, 13, 21, 34, 55);
			Test.AreEqual(55, circle.mass);
		}

		[TestMethod]
		public void setRadiusTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.setRadius(10);
			Test.AreEqual(10, circle.radius);
			circle.setRadius(1);
			Test.AreEqual(1, circle.radius);

			try
			{
				circle.setRadius(.99);
				Assert.Fail();
			}
			catch (ArgumentException e)
			{
				Test.AreEqual("radius: 0.99", e.Message);
			}
			try
			{
				circle.setRadius(0);
				Assert.Fail();
			}
			catch (ArgumentException e)
			{
				Test.AreEqual("radius: 0", e.Message);
			}
			try
			{
				circle.setRadius(-1.0);
				Assert.Fail();
			}
			catch (ArgumentException e)
			{
				Test.AreEqual("radius: -1", e.Message);
			}
		}

		[TestMethod]
		public void setMassTest()
		{
			Circle circle = new Circle(1, 1, 2);
			Test.AreEqual(Circle.MASS, circle.mass);
			circle.setMass(10);
			Test.AreEqual(10, circle.mass);
			circle.setMass(0);
			Test.AreEqual(0, circle.mass);
			circle.setMass(-1);
			Test.AreEqual(-1, circle.mass);
		}

		[TestMethod]
		public void setVelocityTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.setVelocity(3, 5);
			Test.AreEqual(3, circle.vx);
			Test.AreEqual(5, circle.vy);

			circle.setVelocity(8, 13);
			Test.AreEqual(8, circle.vx);
			Test.AreEqual(13, circle.vy);
		}

		[TestMethod]
		public void setAccelerationTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.setAcceleration(3, 5);
			Test.AreEqual(3, circle.ax);
			Test.AreEqual(5, circle.ay);

			circle.setAcceleration(8, 13);
			Test.AreEqual(8, circle.ax);
			Test.AreEqual(13, circle.ay);
		}

		[TestMethod]
		public void followTestT()
		{
			Circle leader = new Circle(1, 1, 2);
			Circle sheep = new Circle(3, 5, 8);

			sheep.follow(leader);
			Test.AreEqual(Circle.TARGET, leader.target);
			Test.AreEqual(Circle.MIN_DIST, leader.min_dist);
			Test.AreEqual(Circle.MAX_DIST, leader.max_dist);

			Test.AreEqual(leader, sheep.target);
			Test.AreEqual(10, sheep.min_dist);
			Test.AreEqual(10, sheep.max_dist);

			sheep.follow();
			Test.AreEqual(null, leader.target);
			Test.AreEqual(0, leader.min_dist);
			Test.AreEqual(0, leader.max_dist);

			Test.AreEqual(null, sheep.target);
			Test.AreEqual(0, sheep.min_dist);
			Test.AreEqual(0, sheep.max_dist);
		}

		[TestMethod]
		public void followTestTMM()
		{
			Circle leader = new Circle(1, 1, 2);
			Circle sheep = new Circle(3, 5, 8);

			sheep.follow(leader, 13, 21);
			Test.AreEqual(Circle.TARGET, leader.target);
			Test.AreEqual(Circle.MIN_DIST, leader.min_dist);
			Test.AreEqual(Circle.MAX_DIST, leader.max_dist);

			Test.AreEqual(leader, sheep.target);
			Test.AreEqual(13, sheep.min_dist);
			Test.AreEqual(21, sheep.max_dist);

			sheep.follow(null, 34, 55);
			Test.AreEqual(null, leader.target);
			Test.AreEqual(0, leader.min_dist);
			Test.AreEqual(0, leader.max_dist);

			Test.AreEqual(null, sheep.target);
			Test.AreEqual(0, sheep.min_dist);
			Test.AreEqual(0, sheep.max_dist);
		}

		//[TestMethod]
		//public void updateAccelerationTest()
		//{
		//	Circle circle = new Circle(1, 1, 2, 0, 0, 0, 0);

		//	circle.updateAcceleration(-1 * constants.GRAVITY, constants.GRAVITY_ANGLE);
		//	Test.AreEqual(constants.GRAVITY, circle.acceleration);
		//	if (constants.GRAVITY_ANGLE + Math.PI > 2 * Math.PI)
		//	{
		//		Test.AreEqual(constants.GRAVITY_ANGLE - Math.PI, circle.acceleration_angle);
		//	}
		//	else
		//	{
		//		Test.AreEqual(constants.GRAVITY_ANGLE + Math.PI, circle.acceleration_angle);
		//	}
		//	Test.AreEqual(0, circle.ax);
		//	Test.AreEqual(-1 * constants.GRAVITY, circle.ay);

		//	double expected_ax = -1 * constants.GRAVITY * Math.Sqrt(2) / 2;
		//	double expected_ay = -1 * constants.GRAVITY * Math.Sqrt(2) / 2 - constants.GRAVITY;
		//	circle.updateAcceleration(-1 * constants.GRAVITY, Math.PI / 4);
		//	Test.AreEqual(Circle.magnitude(expected_ax, expected_ay), circle.acceleration);
		//	Test.AreEqual(Circle.angle(expected_ay, expected_ax), circle.acceleration_angle);
		//	Test.AreEqual(expected_ax, circle.ax);
		//	Test.AreEqual(expected_ay, circle.ay);
		//}

		[TestMethod]
		public void updateVelocity()
		{
			Circle circle = new Circle(1, 1, 2, 0, 0, 0, 0);

			double time = 1;
			for (int i = 1; i <= 10; i++)
			{
				circle.updateVelocity(time);
				Test.AreEqual(0, circle.vx);
				Test.AreEqual(0, circle.vy);
			}

			double gravity = constants.GRAVITY;
			double gravity_angle = Math.PI / 4;
			double gx = gravity * Math.Cos(gravity_angle);
			double gy = gravity * Math.Sin(gravity_angle);

			circle.setAcceleration(gx, gy);
			for (int i = 1; i <= 10; i++)
			{
				circle.updateVelocity(time);
				Test.AreEqual(gx * i, circle.vx);
				Test.AreEqual(gy * i, circle.vy);
			}

			circle.setAcceleration(-gx, -gy);
			for (int i = 9; i >= 0; i--)
			{
				circle.updateVelocity(time);
				Test.AreEqual(gx * i, circle.vx);
				Test.AreEqual(gy * i, circle.vy);
			}

			// do the same but with different time step
			time = .5;
		}

		[TestMethod]
		public void updatePosition()
		{
			Circle circle = new Circle(0, 0, 2, 0, 0, 0, 0);

			double time = 1;
			for (int i = 1; i <= 10; i++)
			{
				circle.updatePosition(time);
				Test.AreEqual(0, circle.px);
				Test.AreEqual(0, circle.py);
			}

			double velocity = 1;
			double velocity_angle = Math.PI / 4;
			double vx = velocity * Math.Cos(velocity_angle);
			double vy = velocity * Math.Sin(velocity_angle);
			circle.setVelocity(vx, vy);
			for (int i = 1; i <= 10; i++)
			{
				circle.updatePosition(time);
				Test.AreEqual(i * vx, circle.px);
				Test.AreEqual(i * vy, circle.py);
			}

			double gravity = constants.GRAVITY;
			double gravity_angle = Math.PI / 4;
			double gx = gravity * Math.Cos(gravity_angle);
			double gy = gravity * Math.Sin(gravity_angle);
			circle.setAcceleration(gx, gy);
			for (int i = 1; i <= 10; i++)
			{
				circle.updatePosition(time);
				Test.AreEqual(gx * i * i / 2 + vx * i + 10 * vx, circle.px);
				Test.AreEqual(gy * i * i / 2 + vy * i + 10 * vy, circle.py);
			}
		}

		[TestMethod]
		public void collidingTest()
		{
			Circle one = new Circle(1, 0, 0, 0, 0, 0, 0);
			Circle two = new Circle(1, 0, 0, 0, 0, 0, 0);

			double time = 1;
			Test.AreEqual(true, Double.IsInfinity(one.colliding(one, time)));
			Test.AreEqual(true, Double.IsInfinity(two.colliding(two, time)));

			Test.AreEqual(false, Double.IsInfinity(one.colliding(two, time)));
			Test.AreEqual(false, Double.IsInfinity(two.colliding(one, time)));

			two = new Circle(0, 1, 1);
			Test.AreEqual(false, Double.IsInfinity(one.colliding(two, time)));
			Test.AreEqual(false, Double.IsInfinity(two.colliding(one, time)));

			two = new Circle(2, 2, 1);
			Test.AreEqual(true, Double.IsInfinity(one.colliding(two, time)));
			Test.AreEqual(true, Double.IsInfinity(two.colliding(one, time)));

			two.setVelocity(2, Math.PI / 4);
			Test.AreEqual(true, Double.IsInfinity(one.colliding(two, time)));
			Test.AreEqual(true, Double.IsInfinity(two.colliding(one, time)));

			two.setVelocity(2, Math.PI / 2);
			Test.AreEqual(true, Double.IsInfinity(one.colliding(two, time)));
			Test.AreEqual(true, Double.IsInfinity(two.colliding(one, time)));

			two.setVelocity(2, 3 * Math.PI / 4);
			Test.AreEqual(true, Double.IsInfinity(one.colliding(two, time)));
			Test.AreEqual(true, Double.IsInfinity(two.colliding(one, time)));

			two.setVelocity(2, Math.PI);
			Test.AreEqual(false, Double.IsInfinity(one.colliding(two, time)));
			Test.AreEqual(false, Double.IsInfinity(two.colliding(one, time)));

			two.setVelocity(2, 5 * Math.PI / 4);
			Test.AreEqual(true, Double.IsInfinity(one.colliding(two, time)));
			Test.AreEqual(true, Double.IsInfinity(two.colliding(one, time)));

			two.setVelocity(2, 3 * Math.PI / 2);
			Test.AreEqual(false, Double.IsInfinity(one.colliding(two, time)));
			Test.AreEqual(false, Double.IsInfinity(two.colliding(one, time)));

			two.setVelocity(2, 7 * Math.PI / 4);
			Test.AreEqual(true, Double.IsInfinity(one.colliding(two, time)));
			Test.AreEqual(true, Double.IsInfinity(two.colliding(one, time)));

			two.setVelocity(0, 0);
			one.setVelocity(1, Math.PI / 4);
			Test.AreEqual(true, Double.IsInfinity(one.colliding(two, time)));
			Test.AreEqual(true, Double.IsInfinity(two.colliding(one, time)));

			one.setVelocity(2, Math.PI / 2);
			Test.AreEqual(false, Double.IsInfinity(one.colliding(two, time)));
			Test.AreEqual(false, Double.IsInfinity(two.colliding(one, time)));
		}

		//[TestMethod]
		//public void moveTest()
		//{
		//	Circle one = new Circle(1, 1, 1, 1, 1);
		//	Circle two = new Circle(3, 3, 3);

		//	one.follow(two);
		//	one.move(null);
		//	Test.AreEqual(1.0, one.velocity);
		//	Test.AreEqual(Math.PI / 4, one.velocity_angle);
		//	Test.AreEqual(0.0, two.velocity);
		//	Test.AreEqual(0.0, two.velocity_angle);
		//}

		[TestMethod]
		public void distanceTest()
		{
			Circle one = new Circle(0, 0, 1);
			Circle two = new Circle(0, 0, 1);

			Test.AreEqual(0.0, one.distance(two));
			Test.AreEqual(0.0, two.distance(one));

			two = new Circle(3, 4, 1);

			Test.AreEqual(5.0, one.distance(two));
			Test.AreEqual(5.0, two.distance(one));
		}

		[TestMethod]
		public void magnitude()
		{
			Test.AreEqual(10, OrderedPair.magnitude(6.0, 8.0));
		}

		[TestMethod]
		public void angle()
		{
			Test.AreEqual(0.0, OrderedPair.angle(0.0, 1.0));
			Test.AreEqual(Math.PI / 4, OrderedPair.angle(1.0, 1.0));
			Test.AreEqual(2 * Math.PI / 4, OrderedPair.angle(1.0, 0.0));
			Test.AreEqual(3 * Math.PI / 4, OrderedPair.angle(1.0, -1.0));
			Test.AreEqual(4 * Math.PI / 4, OrderedPair.angle(0.0, -1.0));
			Test.AreEqual(5 * Math.PI / 4, OrderedPair.angle(-1.0, -1.0));
			Test.AreEqual(6 * Math.PI / 4, OrderedPair.angle(-1.0, 0.0));
			Test.AreEqual(7 * Math.PI / 4, OrderedPair.angle(-1.0, 1.0));
		}
	}
}