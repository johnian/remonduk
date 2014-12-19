using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using remonduk;

namespace TestSuite
{
	[TestClass]
	public class CircleTest
	{
		Constants constants = Constants.Instance;

		int PRECISION = 6;
		double EPSILON = .0001;
		[TestMethod]
		public void CircleTestXYR()
		{
			// check that all other fields are set correctly
			Circle circle = new Circle(2, 3, 5);

			AreEqual(2, circle.x);
			AreEqual(3, circle.y);
			AreEqual(5, circle.r);
			AreEqual(Circle.MASS, circle.mass);

			AreEqual(Circle.VELOCITY, circle.velocity);
			AreEqual(Circle.VELOCITY_ANGLE, circle.velocity_angle);
			AreEqual(Circle.VELOCITY * Math.Cos(Circle.VELOCITY_ANGLE), circle.ax);
			AreEqual(Circle.VELOCITY * Math.Sin(Circle.VELOCITY_ANGLE), circle.ay);

			AreEqual(Circle.ACCELERATION, circle.acceleration);
			AreEqual(Circle.ACCELERATION_ANGLE, circle.acceleration_angle);
			AreEqual(Circle.ACCELERATION * Math.Cos(Circle.ACCELERATION_ANGLE), circle.ax);
			AreEqual(Circle.ACCELERATION * Math.Sin(Circle.ACCELERATION_ANGLE), circle.ay);

			AreEqual(null, circle.target);
			AreEqual(0, circle.min_dist);
			AreEqual(0, circle.max_dist);

			circle = new Circle(2, 3, 5, 8);
			AreEqual(8, circle.mass);
		}

		[TestMethod]
		public void CircleTestXYRV()
		{
			Circle circle = new Circle(2, 3, 5, 8, Math.PI / 4);

			AreEqual(2, circle.x);
			AreEqual(3, circle.y);
			AreEqual(5, circle.r);
			AreEqual(Circle.MASS, circle.mass);

			AreEqual(8, circle.velocity);
			AreEqual(Math.PI / 4, circle.velocity_angle);
			AreEqual(4 * Math.Sqrt(2), circle.vx);
			AreEqual(4 * Math.Sqrt(2), circle.vy);

			AreEqual(Circle.ACCELERATION, circle.acceleration);
			AreEqual(Circle.ACCELERATION_ANGLE, circle.acceleration_angle);
			AreEqual(Circle.ACCELERATION * Math.Cos(Circle.ACCELERATION_ANGLE), circle.ax);
			AreEqual(Circle.ACCELERATION * Math.Sin(Circle.ACCELERATION_ANGLE), circle.ay);

			AreEqual(null, circle.target);
			AreEqual(0, circle.min_dist);
			AreEqual(0, circle.max_dist);

			circle = new Circle(2, 3, 5, 8, Math.PI / 4, 13);
			AreEqual(13, circle.mass);
		}

		[TestMethod]
		public void CircleTestXYRVA()
		{
			Circle circle = new Circle(2, 3, 5, 8, Math.PI / 4, 13, 1 * Math.PI / 4);

			AreEqual(2, circle.x);
			AreEqual(3, circle.y);
			AreEqual(5, circle.r);
			AreEqual(Circle.MASS, circle.mass);

			AreEqual(8, circle.velocity);
			AreEqual(Math.PI / 4, circle.velocity_angle);
			AreEqual(4 * Math.Sqrt(2), circle.vx);
			AreEqual(4 * Math.Sqrt(2), circle.vy);

			AreEqual(13, circle.acceleration);
			AreEqual(Math.PI / 4, circle.acceleration_angle);
			AreEqual(6.5 * Math.Sqrt(2), circle.ax);
			AreEqual(6.5 * Math.Sqrt(2), circle.ay);

			AreEqual(null, circle.target);
			AreEqual(0, circle.min_dist);
			AreEqual(0, circle.max_dist);

			circle = new Circle(2, 3, 5, 8, Math.PI / 4, 13, 1 * Math.PI / 4, 21);
			AreEqual(21, circle.mass);
		}

		[TestMethod]
		public void setRadiusTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.setRadius(10);
			AreEqual(10, circle.r);
			circle.setRadius(1);
			AreEqual(1, circle.r);

			try
			{
				circle.setRadius(.99);
				Assert.Fail();
			}
			catch (ArgumentException e)
			{
				AreEqual("radius: 0.99", e.Message);
			}
			try
			{
				circle.setRadius(0);
				Assert.Fail();
			}
			catch (ArgumentException e)
			{
				AreEqual("radius: 0", e.Message);
			}
			try
			{
				circle.setRadius(-1.0);
				Assert.Fail();
			}
			catch (ArgumentException e)
			{
				AreEqual("radius: -1", e.Message);
			}
		}

		[TestMethod]
		public void setMassTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.setMass(10);
			AreEqual(10, circle.mass);
			circle.setMass(0);
			AreEqual(0, circle.mass);
			circle.setMass(-1);
			AreEqual(-1, circle.mass);
		}

		[TestMethod]
		public void setVelocityTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.setVelocity(10, 0);
			AreEqual(10, circle.velocity);
			AreEqual(0, circle.velocity_angle);
			AreEqual(10, circle.vx);
			AreEqual(0, circle.vy);

			circle.setVelocity(20, Math.PI / 2);
			AreEqual(20, circle.velocity);
			AreEqual(Math.PI / 2, circle.velocity_angle);
			AreEqual(0, circle.vx);
			AreEqual(20, circle.vy);

			circle.setVelocity(30, Math.PI / 4);
			AreEqual(30, circle.velocity);
			AreEqual(Math.PI / 4, circle.velocity_angle);
			AreEqual(15 * Math.Sqrt(2), circle.vx);
			AreEqual(15 * Math.Sqrt(2), circle.vy);

			circle.setVelocity(40, 5 * Math.PI / 4);
			AreEqual(40, circle.velocity);
			AreEqual(5 * Math.PI / 4, circle.velocity_angle);
			AreEqual(-20 * Math.Sqrt(2), circle.vx);
			AreEqual(-20 * Math.Sqrt(2), circle.vy);
		}

		[TestMethod]
		public void setAccelerationTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.setAcceleration(10, 0);
			AreEqual(10, circle.acceleration);
			AreEqual(0, circle.acceleration_angle);
			AreEqual(10, circle.ax);
			AreEqual(0, circle.ay);

			circle.setAcceleration(20, Math.PI / 2);
			AreEqual(20, circle.acceleration);
			AreEqual(Math.PI / 2, circle.acceleration_angle);
			AreEqual(0, circle.ax);
			AreEqual(20, circle.ay);

			circle.setAcceleration(30, Math.PI / 4);
			AreEqual(30, circle.acceleration);
			AreEqual(Math.PI / 4, circle.acceleration_angle);
			AreEqual(15 * Math.Sqrt(2), circle.ax);
			AreEqual(15 * Math.Sqrt(2), circle.ay);

			circle.setAcceleration(40, 5 * Math.PI / 4);
			AreEqual(40, circle.acceleration);
			AreEqual(5 * Math.PI / 4, circle.acceleration_angle);
			AreEqual(-20 * Math.Sqrt(2), circle.ax);
			AreEqual(-20 * Math.Sqrt(2), circle.ay);
		}

		[TestMethod]
		public void followTestT()
		{
			Circle leader = new Circle(1, 1, 2);
			Circle sheep = new Circle(3, 5, 8);

			sheep.follow(leader);
			AreEqual(null, leader.target);
			AreEqual(0, leader.min_dist);
			AreEqual(0, leader.max_dist);

			AreEqual(leader, sheep.target);
			AreEqual(10, sheep.min_dist);
			AreEqual(10, sheep.max_dist);

			sheep.follow();
			AreEqual(null, leader.target);
			AreEqual(0, leader.min_dist);
			AreEqual(0, leader.max_dist);

			AreEqual(null, sheep.target);
			AreEqual(0, sheep.min_dist);
			AreEqual(0, sheep.max_dist);
		}

		[TestMethod]
		public void followTestTMM()
		{
			Circle leader = new Circle(1, 1, 2);
			Circle sheep = new Circle(3, 5, 8);

			sheep.follow(leader, 13, 21);
			AreEqual(null, leader.target);
			AreEqual(0, leader.min_dist);
			AreEqual(0, leader.max_dist);

			AreEqual(leader, sheep.target);
			AreEqual(13, sheep.min_dist);
			AreEqual(21, sheep.max_dist);

			sheep.follow(null, 34, 55);
			AreEqual(null, leader.target);
			AreEqual(0, leader.min_dist);
			AreEqual(0, leader.max_dist);

			AreEqual(null, sheep.target);
			AreEqual(0, sheep.min_dist);
			AreEqual(0, sheep.max_dist);
		}

		[TestMethod]
		public void updateAccelerationTest()
		{
			Circle circle = new Circle(1, 1, 2, 0, 0, 0, 0);

			circle.updateAcceleration(-1 * constants.GRAVITY, constants.GRAVITY_ANGLE);
			AreEqual(constants.GRAVITY, circle.acceleration);
			if (constants.GRAVITY_ANGLE + Math.PI > 2 * Math.PI)
			{
				AreEqual(constants.GRAVITY_ANGLE - Math.PI, circle.acceleration_angle);
			}
			else
			{
				AreEqual(constants.GRAVITY_ANGLE + Math.PI, circle.acceleration_angle);
			}
			AreEqual(0, circle.ax);
			AreEqual(-1 * constants.GRAVITY, circle.ay);

			double expected_ax = -1 * constants.GRAVITY * Math.Sqrt(2) / 2;
			double expected_ay = -1 * constants.GRAVITY * Math.Sqrt(2) / 2 - constants.GRAVITY;
			circle.updateAcceleration(-1 * constants.GRAVITY, Math.PI / 4);
			AreEqual(circle.magnitude(expected_ax, expected_ay), circle.acceleration);
			AreEqual(circle.angle(expected_ay, expected_ax), circle.acceleration_angle);
			AreEqual(expected_ax, circle.ax);
			AreEqual(expected_ay, circle.ay);
		}

		[TestMethod]
		public void updateVelocity()
		{
			Circle circle = new Circle(1, 1, 2, 0, 0, 0, 0);

			for (int i = 1; i <= 10; i++)
			{
				circle.updateVelocity();
				AreEqual(0, circle.velocity);
				AreEqual(0, circle.velocity_angle);
				AreEqual(0, circle.vx);
				AreEqual(0, circle.vy);
			}

			double gravity = constants.GRAVITY;
			double gravity_angle = Math.PI / 4;
			double gx = gravity * Math.Cos(gravity_angle);
			double gy = gravity * Math.Sin(gravity_angle);

			circle.updateAcceleration(gravity, gravity_angle);
			for (int i = 1; i <= 10; i++)
			{
				circle.updateVelocity();
				AreEqual(gravity * i, circle.velocity);
				AreEqual(circle.angle(gy * i, gx * i), circle.velocity_angle);
				AreEqual(gx * i, circle.vx);
				AreEqual(gy * i, circle.vy);
			}

			circle.updateAcceleration(2 * gravity, gravity_angle + Math.PI);
			for (int i = 9; i >= 0; i--)
			{
				circle.updateVelocity();
				AreEqual(gravity * i, circle.velocity);
				AreEqual(circle.angle(gy * i, gx * i), circle.velocity_angle);
				AreEqual(gx * i, circle.vx);
				AreEqual(gy * i, circle.vy);
			}
		}

		[TestMethod]
		public void updatePosition()
		{
			Circle circle = new Circle(0, 0, 2, 0, 0, 0, 0);

			for (int i = 1; i <= 10; i++)
			{
				circle.updatePosition();
				AreEqual(0, circle.x);
				AreEqual(0, circle.y);
			}

			double velocity = 1;
			double velocity_angle = Math.PI / 4;
			double vx = velocity * Math.Cos(velocity_angle);
			double vy = velocity * Math.Sin(velocity_angle);
			circle.setVelocity(velocity, velocity_angle);
			for (int i = 1; i <= 10; i++)
			{
				circle.updatePosition();
				AreEqual(i * vx, circle.x);
				AreEqual(i * vy, circle.y);
			}

			double gravity = constants.GRAVITY;
			double gravity_angle = Math.PI / 4;
			double gx = gravity * Math.Cos(gravity_angle);
			double gy = gravity * Math.Sin(gravity_angle);
			circle.updateAcceleration(gravity, gravity_angle);
			for (int i = 1; i <= 10; i++)
			{
				circle.updatePosition();
				AreEqual(gx * i * i / 2 + vx * i + 10 * vx, circle.x);
				AreEqual(gy * i * i / 2 + vy * i + 10 * vy, circle.y);
			}
		}

        [TestMethod]
        public void tether1Test()
        {
            Circle c1 = new Circle(0, 0, 2, 0, 0, 0, 0);
            Circle c2 = new Circle(200, 0, 2, 0, 0, 0, 0);
            double max_dist = 20;
            double k = .001;

            Tether t = new Tether(c1, c2, max_dist, k);

            AreEqual(c1, t.c1);
            AreEqual(c2, t.c2);
            AreEqual(max_dist, t.max_dist);
            AreEqual(k, t.k);

            t.pull();

            AreEqual(t.c1.acceleration_angle, 0);
            AreEqual(t.c2.acceleration_angle, Math.PI);
            AreEqual(t.c1.acceleration, .18);
            AreEqual(t.c2.acceleration, .18);

            t.c1.update(new HashSet<Circle>());
            t.c2.update(new HashSet<Circle>());

            AreEqual(t.c1.velocity_angle, 0);
            AreEqual(t.c2.velocity_angle, Math.PI);
            AreEqual(t.c1.velocity, .18);
            AreEqual(t.c2.velocity, .18);
            AreEqual(t.c1.x, .09);
            AreEqual(t.c2.x, 199.91);
        }

        [TestMethod]
        public void tether2Test()
        {
            Circle c1 = new Circle(0, 0, 2, 0, 0, 0, 0);
            Circle c2 = new Circle(200, 0, 2, 0, 0, 0, 0);
            Circle c3 = new Circle(0, 200, 2, 0, 0, 0, 0);
            double max_dist = 20;
            double k = .001;

            Tether t = new Tether(c1, c2, max_dist, k);
            Tether t2 = new Tether(c1, c3, max_dist, k);

            t.pull();
            t2.pull();

            AreEqual(Math.PI / 4.0, c1.acceleration_angle);
            AreEqual(Math.PI, c2.acceleration_angle);
            AreEqual(3 * Math.PI / 2, c3.acceleration_angle);
            AreEqual(.18, c2.acceleration);
            AreEqual(.18, c3.acceleration);
            AreEqual(.36, c1.acceleration);
        }

		[TestMethod]
		public void collidingTest()
		{

		}

		[TestMethod]

		public void moveTest()
		{

		}

		[TestMethod]
		public void updateTest()
		{

		}

		[TestMethod]
		public void drawTest()
		{
			// is this even testable?
		}

		[TestMethod]
		public void distanceTest()
		{
			// gonna have to make the method public to do this
		}

		//private void AreEqual(double expected, double actual) {
		//	Assert.AreEqual(expected, actual);
		//}

		private void AreEqual(double expected, double actual)
		{
			System.Diagnostics.Debug.WriteLine("expected: " + expected + " actual: " + actual);
			AreEqual(true, Math.Abs(expected - actual) < EPSILON);
		}

		private void AreEqual(bool expected, bool actual)
		{
			Assert.AreEqual(expected, actual);
		}

		private void AreEqual(Object expected, Object actual)
		{
			Assert.AreEqual(expected, actual);
		}

		//private void AreEqual(String expected, String actual) {
		//	Assert.AreEqual(expected, actual);
		//}

		private double round(double value)
		{
			return Math.Round(value, PRECISION);
		}
	}
}