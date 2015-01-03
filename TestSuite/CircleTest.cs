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

			Test.AreEqual(2, circle.Radius);
			Test.AreEqual(3, circle.Px);
			Test.AreEqual(5, circle.Py);
			Test.AreEqual(Circle.MASS, circle.Mass);

			Test.AreEqual(Circle.VX, circle.Vx);
			Test.AreEqual(Circle.VY, circle.Vy);

			Test.AreEqual(Circle.AX, circle.Ax);
			Test.AreEqual(Circle.AY, circle.Ay);

			Test.AreEqual(Circle.TARGET, circle.Target);
			Test.AreEqual(Circle.MIN_DISTANCE, circle.MinDistance);
			Test.AreEqual(Circle.MAX_DISTANCE, circle.MaxDistance);

			circle = new Circle(2, 3, 5, 8);
			Test.AreEqual(8, circle.Mass);
		}

		[TestMethod]
		public void CircleTestXYRV()
		{
			Circle circle = new Circle(2, 3, 5, 8, 13);

			Test.AreEqual(2, circle.Radius);
			Test.AreEqual(3, circle.Px);
			Test.AreEqual(5, circle.Py);
			Test.AreEqual(Circle.MASS, circle.Mass);

			Test.AreEqual(8, circle.Vx);
			Test.AreEqual(13, circle.Vy);

			Test.AreEqual(Circle.AX, circle.Ax);
			Test.AreEqual(Circle.AY, circle.Ay);

			Test.AreEqual(Circle.TARGET, circle.Target);
			Test.AreEqual(Circle.MIN_DISTANCE, circle.MinDistance);
			Test.AreEqual(Circle.MAX_DISTANCE, circle.MaxDistance);

			circle = new Circle(2, 3, 5, 8, 13, 21);
			Test.AreEqual(21, circle.Mass);
		}

		[TestMethod]
		public void CircleTestXYRVA()
		{
			Circle circle = new Circle(2, 3, 5, 8, 13, 21, 34);

			Test.AreEqual(2, circle.Radius);
			Test.AreEqual(3, circle.Px);
			Test.AreEqual(5, circle.Py);
			Test.AreEqual(Circle.MASS, circle.Mass);

			Test.AreEqual(8, circle.Vx);
			Test.AreEqual(13, circle.Vy);

			Test.AreEqual(21, circle.Ax);
			Test.AreEqual(34, circle.Ay);

			Test.AreEqual(Circle.TARGET, circle.Target);
			Test.AreEqual(Circle.MIN_DISTANCE, circle.MinDistance);
			Test.AreEqual(Circle.MAX_DISTANCE, circle.MaxDistance);

			circle = new Circle(2, 3, 5, 8, 13, 21, 34, 55);
			Test.AreEqual(55, circle.Mass);
		}

		[TestMethod]
		public void setRadiusTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.SetRadius(10);
			Test.AreEqual(10, circle.Radius);
			circle.SetRadius(1);
			Test.AreEqual(1, circle.Radius);

			try
			{
				circle.SetRadius(.99);
				Assert.Fail();
			}
			catch (ArgumentException e)
			{
				Test.AreEqual("radius: 0.99", e.Message);
			}
			try
			{
				circle.SetRadius(0);
				Assert.Fail();
			}
			catch (ArgumentException e)
			{
				Test.AreEqual("radius: 0", e.Message);
			}
			try
			{
				circle.SetRadius(-1.0);
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
			Test.AreEqual(Circle.MASS, circle.Mass);
			circle.SetMass(10);
			Test.AreEqual(10, circle.Mass);
			circle.SetMass(0);
			Test.AreEqual(0, circle.Mass);
			circle.SetMass(-1);
			Test.AreEqual(-1, circle.Mass);
		}

		[TestMethod]
		public void setVelocityTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.SetVelocity(3, 5);
			Test.AreEqual(3, circle.Vx);
			Test.AreEqual(5, circle.Vy);

			circle.SetVelocity(8, 13);
			Test.AreEqual(8, circle.Vx);
			Test.AreEqual(13, circle.Vy);
		}

		[TestMethod]
		public void setAccelerationTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.SetAcceleration(3, 5);
			Test.AreEqual(3, circle.Ax);
			Test.AreEqual(5, circle.Ay);

			circle.SetAcceleration(8, 13);
			Test.AreEqual(8, circle.Ax);
			Test.AreEqual(13, circle.Ay);
		}

		[TestMethod]
		public void followTestT()
		{
			Circle leader = new Circle(1, 1, 2);
			Circle sheep = new Circle(3, 5, 8);

			sheep.Follow(leader);
			Test.AreEqual(Circle.TARGET, leader.Target);
			Test.AreEqual(Circle.MIN_DISTANCE, leader.MinDistance);
			Test.AreEqual(Circle.MAX_DISTANCE, leader.MaxDistance);

			Test.AreEqual(leader, sheep.Target);
			Test.AreEqual(leader.Radius + sheep.Radius, sheep.MinDistance);
			Test.AreEqual(leader.Radius + sheep.Radius, sheep.MaxDistance);

			sheep.Follow();
			Test.AreEqual(null, leader.Target);
			Test.AreEqual(0, leader.MinDistance);
			Test.AreEqual(0, leader.MaxDistance);

			Test.AreEqual(null, sheep.Target);
			Test.AreEqual(0, sheep.MinDistance);
			Test.AreEqual(0, sheep.MaxDistance);
		}

		[TestMethod]
		public void followTestTMM()
		{
			Circle leader = new Circle(1, 1, 2);
			Circle sheep = new Circle(3, 5, 8);

			sheep.Follow(leader, 13, 21);
			Test.AreEqual(Circle.TARGET, leader.Target);
			Test.AreEqual(Circle.MIN_DISTANCE, leader.MinDistance);
			Test.AreEqual(Circle.MAX_DISTANCE, leader.MaxDistance);

			Test.AreEqual(leader, sheep.Target);
			Test.AreEqual(13, sheep.MinDistance);
			Test.AreEqual(21, sheep.MaxDistance);

			sheep.Follow(null, 34, 55);
			Test.AreEqual(null, leader.Target);
			Test.AreEqual(0, leader.MinDistance);
			Test.AreEqual(0, leader.MaxDistance);

			Test.AreEqual(null, sheep.Target);
			Test.AreEqual(0, sheep.MinDistance);
			Test.AreEqual(0, sheep.MaxDistance);
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
		//	Test.AreEqual(0, circle.Ax);
		//	Test.AreEqual(-1 * constants.GRAVITY, circle.Ay);

		//	double expected_ax = -1 * constants.GRAVITY * Math.Sqrt(2) / 2;
		//	double expected_ay = -1 * constants.GRAVITY * Math.Sqrt(2) / 2 - constants.GRAVITY;
		//	circle.updateAcceleration(-1 * constants.GRAVITY, Math.PI / 4);
		//	Test.AreEqual(Circle.Magnitude(expected_ax, expected_ay), circle.acceleration);
		//	Test.AreEqual(Circle.Angle(expected_ay, expected_ax), circle.acceleration_angle);
		//	Test.AreEqual(expected_ax, circle.Ax);
		//	Test.AreEqual(expected_ay, circle.Ay);
		//}

		[TestMethod]
		public void updateVelocity()
		{
			Circle circle = new Circle(1, 1, 2, 0, 0, 0, 0);

			double time = 1;
			for (int i = 1; i <= 10; i++)
			{
				circle.UpdateVelocity(time);
				Test.AreEqual(0, circle.Vx);
				Test.AreEqual(0, circle.Vy);
			}

			double gravity = constants.GRAVITY;
			double gravity_angle = Math.PI / 4;
			double gx = gravity * Math.Cos(gravity_angle);
			double gy = gravity * Math.Sin(gravity_angle);

			circle.SetAcceleration(gx, gy);
			for (int i = 1; i <= 10; i++)
			{
				circle.UpdateVelocity(time);
				Test.AreEqual(gx * i, circle.Vx);
				Test.AreEqual(gy * i, circle.Vy);
			}

			circle.SetAcceleration(-gx, -gy);
			for (int i = 9; i >= 0; i--)
			{
				circle.UpdateVelocity(time);
				Test.AreEqual(gx * i, circle.Vx);
				Test.AreEqual(gy * i, circle.Vy);
			}

			// do the same but with different time step
			time = .5;
		}

		[TestMethod]
		public void updatePosition()
		{
			Circle circle = new Circle(2, 0, 0, 0, 0, 0, 0);

			double time = 1;
			for (int i = 1; i <= 10; i++)
			{
				circle.UpdatePosition(time);
				Test.AreEqual(0, circle.Px);
				Test.AreEqual(0, circle.Py);
			}

			double velocity = 1;
			double velocity_angle = Math.PI / 4;
			double vx = velocity * Math.Cos(velocity_angle);
			double vy = velocity * Math.Sin(velocity_angle);
			circle.SetVelocity(vx, vy);
			for (int i = 1; i <= 10; i++)
			{
				circle.UpdatePosition(time);
				Test.AreEqual(i * vx, circle.Px);
				Test.AreEqual(i * vy, circle.Py);
			}

			double gravity = constants.GRAVITY;
			double gravity_angle = Math.PI / 4;
			double gx = gravity * Math.Cos(gravity_angle);
			double gy = gravity * Math.Sin(gravity_angle);
			circle.SetAcceleration(gx, gy);
			for (int i = 1; i <= 10; i++)
			{
				circle.UpdatePosition(time);
				Test.AreEqual(gx * i * i / 2 + vx * i + 10 * vx, circle.Px);
				Test.AreEqual(gy * i * i / 2 + vy * i + 10 * vy, circle.Py);
			}
		}
		
		[TestMethod]
		public void collidingTestT() {
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
		public void crossingTest() {
			Test.AreEqual(true, false);
		}

		[TestMethod]
		public void closestPointTest() {
			Test.AreEqual(true, false);
		}


		[TestMethod]
		public void moveTest()
		{
			Circle one = new Circle(1, 1, 1, 1, 1);
			Circle two = new Circle(3, 3, 3);

			Test.AreEqual(true, false);
			//one.Follow(two);
			//one.move(null);
			//Test.AreEqual(1.0, one.velocity);
			//Test.AreEqual(Math.PI / 4, one.velocity_angle);
			//Test.AreEqual(0.0, two.velocity);
			//Test.AreEqual(0.0, two.velocity_angle);
		}

		[TestMethod]
		public void distanceTest()
		{
			Circle one = new Circle(1, 0, 0);
			Circle two = new Circle(1, 0, 0);

			Test.AreEqual(0.0, one.Distance(two));
			Test.AreEqual(0.0, two.Distance(one));

			two = new Circle(1, 3, 4);

			Test.AreEqual(5.0, one.Distance(two));
			Test.AreEqual(5.0, two.Distance(one));
		}

		[TestMethod]
		public void distanceSquaredTest() {
			Test.AreEqual(true, false);
		}

		[TestMethod]
		public void magnitude()
		{
			Test.AreEqual(10, OrderedPair.Magnitude(6.0, 8.0));
		}

		[TestMethod]
		public void angle()
		{
			Test.AreEqual(0.0, OrderedPair.Angle(0.0, 1.0));
			Test.AreEqual(Math.PI / 4, OrderedPair.Angle(1.0, 1.0));
			Test.AreEqual(2 * Math.PI / 4, OrderedPair.Angle(1.0, 0.0));
			Test.AreEqual(3 * Math.PI / 4, OrderedPair.Angle(1.0, -1.0));
			Test.AreEqual(4 * Math.PI / 4, OrderedPair.Angle(0.0, -1.0));
			Test.AreEqual(5 * Math.PI / 4, OrderedPair.Angle(-1.0, -1.0));
			Test.AreEqual(6 * Math.PI / 4, OrderedPair.Angle(-1.0, 0.0));
			Test.AreEqual(7 * Math.PI / 4, OrderedPair.Angle(-1.0, 1.0));
		}
	}
}