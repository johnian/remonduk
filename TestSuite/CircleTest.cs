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

			Test.AreEqual(2, circle.x);
			Test.AreEqual(3, circle.y);
			Test.AreEqual(5, circle.r);
			Test.AreEqual(Circle.MASS, circle.mass);

			Test.AreEqual(Circle.VELOCITY, circle.velocity);
			Test.AreEqual(Circle.VELOCITY_ANGLE, circle.velocity_angle);
			Test.AreEqual(Circle.VELOCITY * Math.Cos(Circle.VELOCITY_ANGLE), circle.ax);
			Test.AreEqual(Circle.VELOCITY * Math.Sin(Circle.VELOCITY_ANGLE), circle.ay);

			Test.AreEqual(Circle.ACCELERATION, circle.acceleration);
			Test.AreEqual(Circle.ACCELERATION_ANGLE, circle.acceleration_angle);
			Test.AreEqual(Circle.ACCELERATION * Math.Cos(Circle.ACCELERATION_ANGLE), circle.ax);
			Test.AreEqual(Circle.ACCELERATION * Math.Sin(Circle.ACCELERATION_ANGLE), circle.ay);

			Test.AreEqual(null, circle.target);
			Test.AreEqual(0, circle.min_dist);
			Test.AreEqual(0, circle.max_dist);

			circle = new Circle(2, 3, 5, 8);
			Test.AreEqual(8, circle.mass);
		}

		[TestMethod]
		public void CircleTestXYRV()
		{
			Circle circle = new Circle(2, 3, 5, 8, Math.PI / 4);

			Test.AreEqual(2, circle.x);
			Test.AreEqual(3, circle.y);
			Test.AreEqual(5, circle.r);
			Test.AreEqual(Circle.MASS, circle.mass);

			Test.AreEqual(8, circle.velocity);
			Test.AreEqual(Math.PI / 4, circle.velocity_angle);
			Test.AreEqual(4 * Math.Sqrt(2), circle.vx);
			Test.AreEqual(4 * Math.Sqrt(2), circle.vy);

			Test.AreEqual(Circle.ACCELERATION, circle.acceleration);
			Test.AreEqual(Circle.ACCELERATION_ANGLE, circle.acceleration_angle);
			Test.AreEqual(Circle.ACCELERATION * Math.Cos(Circle.ACCELERATION_ANGLE), circle.ax);
			Test.AreEqual(Circle.ACCELERATION * Math.Sin(Circle.ACCELERATION_ANGLE), circle.ay);

			Test.AreEqual(null, circle.target);
			Test.AreEqual(0, circle.min_dist);
			Test.AreEqual(0, circle.max_dist);

			circle = new Circle(2, 3, 5, 8, Math.PI / 4, 13);
			Test.AreEqual(13, circle.mass);
		}

		[TestMethod]
		public void CircleTestXYRVA()
		{
			Circle circle = new Circle(2, 3, 5, 8, Math.PI / 4, 13, 1 * Math.PI / 4);

			Test.AreEqual(2, circle.x);
			Test.AreEqual(3, circle.y);
			Test.AreEqual(5, circle.r);
			Test.AreEqual(Circle.MASS, circle.mass);

			Test.AreEqual(8, circle.velocity);
			Test.AreEqual(Math.PI / 4, circle.velocity_angle);
			Test.AreEqual(4 * Math.Sqrt(2), circle.vx);
			Test.AreEqual(4 * Math.Sqrt(2), circle.vy);

			Test.AreEqual(13, circle.acceleration);
			Test.AreEqual(Math.PI / 4, circle.acceleration_angle);
			Test.AreEqual(6.5 * Math.Sqrt(2), circle.ax);
			Test.AreEqual(6.5 * Math.Sqrt(2), circle.ay);

			Test.AreEqual(null, circle.target);
			Test.AreEqual(0, circle.min_dist);
			Test.AreEqual(0, circle.max_dist);

			circle = new Circle(2, 3, 5, 8, Math.PI / 4, 13, 1 * Math.PI / 4, 21);
			Test.AreEqual(21, circle.mass);
		}

		[TestMethod]
		public void setRadiusTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.setRadius(10);
			Test.AreEqual(10, circle.r);
			circle.setRadius(1);
			Test.AreEqual(1, circle.r);

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
			circle.setVelocity(10, 0);
			Test.AreEqual(10, circle.velocity);
			Test.AreEqual(0, circle.velocity_angle);
			Test.AreEqual(10, circle.vx);
			Test.AreEqual(0, circle.vy);

			circle.setVelocity(20, Math.PI / 2);
			Test.AreEqual(20, circle.velocity);
			Test.AreEqual(Math.PI / 2, circle.velocity_angle);
			Test.AreEqual(0, circle.vx);
			Test.AreEqual(20, circle.vy);

			circle.setVelocity(30, Math.PI / 4);
			Test.AreEqual(30, circle.velocity);
			Test.AreEqual(Math.PI / 4, circle.velocity_angle);
			Test.AreEqual(15 * Math.Sqrt(2), circle.vx);
			Test.AreEqual(15 * Math.Sqrt(2), circle.vy);

			circle.setVelocity(40, 5 * Math.PI / 4);
			Test.AreEqual(40, circle.velocity);
			Test.AreEqual(5 * Math.PI / 4, circle.velocity_angle);
			Test.AreEqual(-20 * Math.Sqrt(2), circle.vx);
			Test.AreEqual(-20 * Math.Sqrt(2), circle.vy);
		}

		[TestMethod]
		public void setAccelerationTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.setAcceleration(10, 0);
			Test.AreEqual(10, circle.acceleration);
			Test.AreEqual(0, circle.acceleration_angle);
			Test.AreEqual(10, circle.ax);
			Test.AreEqual(0, circle.ay);

			circle.setAcceleration(20, Math.PI / 2);
			Test.AreEqual(20, circle.acceleration);
			Test.AreEqual(Math.PI / 2, circle.acceleration_angle);
			Test.AreEqual(0, circle.ax);
			Test.AreEqual(20, circle.ay);

			circle.setAcceleration(30, Math.PI / 4);
			Test.AreEqual(30, circle.acceleration);
			Test.AreEqual(Math.PI / 4, circle.acceleration_angle);
			Test.AreEqual(15 * Math.Sqrt(2), circle.ax);
			Test.AreEqual(15 * Math.Sqrt(2), circle.ay);

			circle.setAcceleration(40, 5 * Math.PI / 4);
			Test.AreEqual(40, circle.acceleration);
			Test.AreEqual(5 * Math.PI / 4, circle.acceleration_angle);
			Test.AreEqual(-20 * Math.Sqrt(2), circle.ax);
			Test.AreEqual(-20 * Math.Sqrt(2), circle.ay);
		}

		[TestMethod]
		public void followTestT()
		{
			Circle leader = new Circle(1, 1, 2);
			Circle sheep = new Circle(3, 5, 8);

			sheep.follow(leader);
			Test.AreEqual(null, leader.target);
			Test.AreEqual(0, leader.min_dist);
			Test.AreEqual(0, leader.max_dist);

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
			Test.AreEqual(null, leader.target);
			Test.AreEqual(0, leader.min_dist);
			Test.AreEqual(0, leader.max_dist);

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

		[TestMethod]
		public void updateAccelerationTest()
		{
			Circle circle = new Circle(1, 1, 2, 0, 0, 0, 0);

			circle.updateAcceleration(-1 * constants.GRAVITY, constants.GRAVITY_ANGLE);
			Test.AreEqual(constants.GRAVITY, circle.acceleration);
			if (constants.GRAVITY_ANGLE + Math.PI > 2 * Math.PI)
			{
				Test.AreEqual(constants.GRAVITY_ANGLE - Math.PI, circle.acceleration_angle);
			}
			else
			{
				Test.AreEqual(constants.GRAVITY_ANGLE + Math.PI, circle.acceleration_angle);
			}
			Test.AreEqual(0, circle.ax);
			Test.AreEqual(-1 * constants.GRAVITY, circle.ay);

			double expected_ax = -1 * constants.GRAVITY * Math.Sqrt(2) / 2;
			double expected_ay = -1 * constants.GRAVITY * Math.Sqrt(2) / 2 - constants.GRAVITY;
			circle.updateAcceleration(-1 * constants.GRAVITY, Math.PI / 4);
			Test.AreEqual(Circle.magnitude(expected_ax, expected_ay), circle.acceleration);
			Test.AreEqual(Circle.angle(expected_ay, expected_ax), circle.acceleration_angle);
			Test.AreEqual(expected_ax, circle.ax);
			Test.AreEqual(expected_ay, circle.ay);
		}

		[TestMethod]
		public void updateVelocity()
		{
			Circle circle = new Circle(1, 1, 2, 0, 0, 0, 0);

			for (int i = 1; i <= 10; i++)
			{
				circle.updateVelocity();
				Test.AreEqual(0, circle.velocity);
				Test.AreEqual(0, circle.velocity_angle);
				Test.AreEqual(0, circle.vx);
				Test.AreEqual(0, circle.vy);
			}

			double gravity = constants.GRAVITY;
			double gravity_angle = Math.PI / 4;
			double gx = gravity * Math.Cos(gravity_angle);
			double gy = gravity * Math.Sin(gravity_angle);

			circle.updateAcceleration(gravity, gravity_angle);
			for (int i = 1; i <= 10; i++)
			{
				circle.updateVelocity();
				Test.AreEqual(gravity * i, circle.velocity);
				Test.AreEqual(Circle.angle(gy * i, gx * i), circle.velocity_angle);
				Test.AreEqual(gx * i, circle.vx);
				Test.AreEqual(gy * i, circle.vy);
			}

			circle.updateAcceleration(2 * gravity, gravity_angle + Math.PI);
			for (int i = 9; i >= 0; i--)
			{
				circle.updateVelocity();
				Test.AreEqual(gravity * i, circle.velocity);
				Test.AreEqual(Circle.angle(gy * i, gx * i), circle.velocity_angle);
				Test.AreEqual(gx * i, circle.vx);
				Test.AreEqual(gy * i, circle.vy);
			}
		}

		[TestMethod]
		public void updatePosition()
		{
			Circle circle = new Circle(0, 0, 2, 0, 0, 0, 0);

			for (int i = 1; i <= 10; i++)
			{
				circle.updatePosition();
				Test.AreEqual(0, circle.x);
				Test.AreEqual(0, circle.y);
			}

			double velocity = 1;
			double velocity_angle = Math.PI / 4;
			double vx = velocity * Math.Cos(velocity_angle);
			double vy = velocity * Math.Sin(velocity_angle);
			circle.setVelocity(velocity, velocity_angle);
			for (int i = 1; i <= 10; i++)
			{
				circle.updatePosition();
				Test.AreEqual(i * vx, circle.x);
				Test.AreEqual(i * vy, circle.y);
			}

			double gravity = constants.GRAVITY;
			double gravity_angle = Math.PI / 4;
			double gx = gravity * Math.Cos(gravity_angle);
			double gy = gravity * Math.Sin(gravity_angle);
			circle.updateAcceleration(gravity, gravity_angle);
			for (int i = 1; i <= 10; i++)
			{
				circle.updatePosition();
				Test.AreEqual(gx * i * i / 2 + vx * i + 10 * vx, circle.x);
				Test.AreEqual(gy * i * i / 2 + vy * i + 10 * vy, circle.y);
			}
		}

		[TestMethod]
		public void collidingTest()
		{
			Circle one = new Circle(0, 0, 1, 0, 0, 0, 0);
			Circle two = new Circle(0, 0, 1, 0, 0, 0, 0);

			Test.AreEqual(false, one.colliding(one));
			Test.AreEqual(false, two.colliding(two));

			Test.AreEqual(true, one.colliding(two));
			Test.AreEqual(true, two.colliding(one));

			two = new Circle(0, 1, 1);
			Test.AreEqual(true, one.colliding(two));
			Test.AreEqual(true, two.colliding(one));

			two = new Circle(2, 2, 1);
			Test.AreEqual(false, one.colliding(two));
			Test.AreEqual(false, two.colliding(one));

			two.setVelocity(2, Math.PI / 4);
			Test.AreEqual(false, one.colliding(two));
			Test.AreEqual(false, two.colliding(one));

			two.setVelocity(2, Math.PI / 2);
			Test.AreEqual(false, one.colliding(two));
			Test.AreEqual(false, two.colliding(one));

			two.setVelocity(2, 3 * Math.PI / 4);
			Test.AreEqual(false, one.colliding(two));
			Test.AreEqual(false, two.colliding(one));

			two.setVelocity(2, Math.PI);
			Test.AreEqual(true, one.colliding(two));
			Test.AreEqual(true, two.colliding(one));

			two.setVelocity(2, 5 * Math.PI / 4);
			Test.AreEqual(false, one.colliding(two));
			Test.AreEqual(false, two.colliding(one));

			two.setVelocity(2, 3 * Math.PI / 2);
			Test.AreEqual(true, one.colliding(two));
			Test.AreEqual(true, two.colliding(one));

			two.setVelocity(2, 7 * Math.PI / 4);
			Test.AreEqual(false, one.colliding(two));
			Test.AreEqual(false, two.colliding(one));

			two.setVelocity(0, 0);
			one.setVelocity(1, Math.PI / 4);
			Test.AreEqual(false, one.colliding(two));
			Test.AreEqual(false, two.colliding(one));

			one.setVelocity(2, Math.PI / 2);
			Test.AreEqual(true, one.colliding(two));
			Test.AreEqual(true, two.colliding(one));
		}

		[TestMethod]
		public void moveTest()
		{
			Circle one = new Circle(1, 1, 1, 1, 1);
			Circle two = new Circle(3, 3, 3);

			one.follow(two);
			one.move(null);
			Test.AreEqual(1.0, one.velocity);
			Test.AreEqual(Math.PI / 4, one.velocity_angle);
			Test.AreEqual(0.0, two.velocity);
			Test.AreEqual(0.0, two.velocity_angle);
		}

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
			Test.AreEqual(10, Circle.magnitude(6.0, 8.0));
		}

		[TestMethod]
		public void angle()
		{
			Test.AreEqual(0.0, Circle.angle(0.0, 1.0));
			Test.AreEqual(Math.PI / 4, Circle.angle(1.0, 1.0));
			Test.AreEqual(2 * Math.PI / 4, Circle.angle(1.0, 0.0));
			Test.AreEqual(3 * Math.PI / 4, Circle.angle(1.0, -1.0));
			Test.AreEqual(4 * Math.PI / 4, Circle.angle(0.0, -1.0));
			Test.AreEqual(5 * Math.PI / 4, Circle.angle(-1.0, -1.0));
			Test.AreEqual(6 * Math.PI / 4, Circle.angle(-1.0, 0.0));
			Test.AreEqual(7 * Math.PI / 4, Circle.angle(-1.0, 1.0));
		}
	}
}