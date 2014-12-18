using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using remonduk;

namespace TestSuite
{
	[TestClass]
	public class CircleTest
	{
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

			AreEqual(0, circle.velocity);
			AreEqual(0, circle.velocity_angle);
			AreEqual(0, circle.vx);
			AreEqual(0, circle.vy);

			AreEqual(Constants.GRAVITY, circle.acceleration);
			AreEqual(Math.PI / 2, circle.acceleration_angle);
			AreEqual(0, circle.ax);
			AreEqual(Constants.GRAVITY, circle.ay);

			AreEqual(null, circle.target);
			AreEqual(0F, circle.min_dist);
			AreEqual(0F, circle.max_dist);

			circle = new Circle(2, 3, 5, 8F);
			AreEqual(8F, circle.mass);
		}

		[TestMethod]
		public void CircleTestXYRV() {
			Circle circle = new Circle(2, 3, 5, 8, Math.PI / 4);
			
			AreEqual(2, circle.x);
			AreEqual(3, circle.y);
			AreEqual(5, circle.r);
			AreEqual(Circle.MASS, circle.mass);

			AreEqual(8F, circle.velocity);
			AreEqual(Math.PI / 4, circle.velocity_angle);
			AreEqual(4 * Math.Sqrt(2), circle.vx);
			AreEqual(4 * Math.Sqrt(2), circle.vy);

			AreEqual(Constants.GRAVITY, circle.acceleration);
			AreEqual(Math.PI / 2, circle.acceleration_angle);
			AreEqual(0, circle.ax);
			AreEqual(Constants.GRAVITY, circle.ay);

			AreEqual(null, circle.target);
			AreEqual(0F, circle.min_dist);
			AreEqual(0F, circle.max_dist);

			circle = new Circle(2, 3, 5, 8, Math.PI / 4, 13F);
			AreEqual(13F, circle.mass);
		}
		
		[TestMethod]
		public void CircleTestXYRVA() {
			Circle circle = new Circle(2, 3, 5, 8, Math.PI / 4, 13, 1 * Math.PI / 4);

			AreEqual(2, circle.x);
			AreEqual(3, circle.y);
			AreEqual(5, circle.r);
			AreEqual(Circle.MASS, circle.mass);

			AreEqual(8F, circle.velocity);
			AreEqual(Math.PI / 4, circle.velocity_angle);
			AreEqual(4 * Math.Sqrt(2), circle.vx);
			AreEqual(4 * Math.Sqrt(2), circle.vy);

			AreEqual(13, circle.acceleration);
			AreEqual(Math.PI / 4, circle.acceleration_angle);
			AreEqual(6.5 * Math.Sqrt(2), circle.ax);
			AreEqual(6.5 * Math.Sqrt(2), circle.ay);

			AreEqual(null, circle.target);
			AreEqual(0F, circle.min_dist);
			AreEqual(0F, circle.max_dist);

			circle = new Circle(2, 3, 5, 8, Math.PI / 4, 13, 1 * Math.PI / 4, 21F);
			AreEqual(21F, circle.mass);
		}

		[TestMethod]
		public void setRadiusTest() {
			Circle circle = new Circle(1, 1, 2);
			circle.setRadius(10F);
			AreEqual(10F, circle.r);
			circle.setRadius(1F);
			AreEqual(1F, circle.r);

			try {
				circle.setRadius(.99F);
				Assert.Fail();
			}
			catch (ArgumentException e) {
				AreEqual("radius: 0.99", e.Message);
			}
			try {
				circle.setRadius(0F);
				Assert.Fail();
			}
			catch (ArgumentException e) {
				AreEqual("radius: 0", e.Message);
			}
			try {
				circle.setRadius(-1.0F);
				Assert.Fail();
			}
			catch (ArgumentException e) {
				AreEqual("radius: -1", e.Message);
			}
		}

		[TestMethod]
		public void setMassTest() {
			Circle circle = new Circle(1, 1, 2);
			circle.setMass(10F);
			AreEqual(10F, circle.mass);
			circle.setMass(0F);
			AreEqual(0F, circle.mass);
			circle.setMass(-1F);
			AreEqual(-1F, circle.mass);
		}

		[TestMethod]
		public void setVelocityTest() {
			Circle circle = new Circle(1, 1, 2);
			circle.setVelocity(10, 0);
			AreEqual(10F, circle.velocity);
			AreEqual(0, circle.velocity_angle);
			AreEqual(10, circle.vx);
			AreEqual(0, circle.vy);

			circle.setVelocity(20, Math.PI / 2);
			AreEqual(20F, circle.velocity);
			AreEqual(Math.PI / 2, circle.velocity_angle);
			AreEqual(0, circle.vx);
			AreEqual(20, circle.vy);

			circle.setVelocity(30, Math.PI / 4);
			AreEqual(30F, circle.velocity);
			AreEqual(Math.PI / 4, circle.velocity_angle);
			AreEqual(15 * Math.Sqrt(2), circle.vx);
			AreEqual(15 * Math.Sqrt(2), circle.vy);

			circle.setVelocity(40, 5 * Math.PI / 4);
			AreEqual(40F, circle.velocity);
			AreEqual(5 * Math.PI / 4, circle.velocity_angle);
			AreEqual(-20 * Math.Sqrt(2), circle.vx);
			AreEqual(-20 * Math.Sqrt(2), circle.vy);
		}

		[TestMethod]
		public void setAccelerationTest() {
			Circle circle = new Circle(1, 1, 2);
			circle.setAcceleration(10, 0);
			AreEqual(10F, circle.acceleration);
			AreEqual(0, circle.acceleration_angle);
			AreEqual(10, circle.ax);
			AreEqual(0, circle.ay);

			circle.setAcceleration(20, Math.PI / 2);
			AreEqual(20F, circle.acceleration);
			AreEqual(Math.PI / 2, circle.acceleration_angle);
			AreEqual(0, circle.ax);
			AreEqual(20, circle.ay);

			circle.setAcceleration(30, Math.PI / 4);
			AreEqual(30F, circle.acceleration);
			AreEqual(Math.PI / 4, circle.acceleration_angle);
			AreEqual(15 * Math.Sqrt(2), circle.ax);
			AreEqual(15 * Math.Sqrt(2), circle.ay);

			circle.setAcceleration(40, 5 * Math.PI / 4);
			AreEqual(40F, circle.acceleration);
			AreEqual(5 * Math.PI / 4, circle.acceleration_angle);
			AreEqual(-20 * Math.Sqrt(2), circle.ax);
			AreEqual(-20 * Math.Sqrt(2), circle.ay);
		}

		[TestMethod]
		public void followTestT() {
			Circle leader = new Circle(1, 1, 2);
			Circle sheep = new Circle(3, 5, 8);

			sheep.follow(leader);
			AreEqual(null, leader.target);
			AreEqual(0F, leader.min_dist);
			AreEqual(0F, leader.max_dist);

			AreEqual(leader, sheep.target);
			AreEqual(10F, sheep.min_dist);
			AreEqual(10F, sheep.max_dist);

			sheep.follow();
			AreEqual(null, leader.target);
			AreEqual(0F, leader.min_dist);
			AreEqual(0F, leader.max_dist);

			AreEqual(null, sheep.target);
			AreEqual(0F, sheep.min_dist);
			AreEqual(0F, sheep.max_dist);
		}

		[TestMethod]
		public void followTestTMM() {
			Circle leader = new Circle(1, 1, 2);
			Circle sheep = new Circle(3, 5, 8);

			sheep.follow(leader, 13, 21);
			AreEqual(null, leader.target);
			AreEqual(0F, leader.min_dist);
			AreEqual(0F, leader.max_dist);

			AreEqual(leader, sheep.target);
			AreEqual(13F, sheep.min_dist);
			AreEqual(21F, sheep.max_dist);

			sheep.follow(null, 34, 55);
			AreEqual(null, leader.target);
			AreEqual(0F, leader.min_dist);
			AreEqual(0F, leader.max_dist);

			AreEqual(null, sheep.target);
			AreEqual(0F, sheep.min_dist);
			AreEqual(0F, sheep.max_dist);
		}	

		[TestMethod]
		public void updateAccelerationTest() {
			Circle circle = new Circle(1, 1, 2);
			HashSet<Circle> circles = new HashSet<Circle>();
			
			AreEqual(9.8F, circle.acceleration);

			circle.updateAcceleration(-1 * Constants.GRAVITY, Constants.GRAVITY_ANGLE);
			AreEqual(0, circle.acceleration);
			AreEqual(0, circle.acceleration_angle);
			AreEqual(0, circle.ax);
			AreEqual(0, circle.ay);

			circle.updateAcceleration(-1 * Constants.GRAVITY, Math.PI / 4);
			AreEqual(Constants.GRAVITY, circle.acceleration);
			AreEqual(-3 * Math.PI / 4, circle.acceleration_angle);
			AreEqual(-1 * Constants.GRAVITY * Math.Sqrt(2) / 2, circle.ax);
			AreEqual(-1 * Constants.GRAVITY * Math.Sqrt(2) / 2, circle.ay);

			//circle.updateAcceleration(-1 * Constants.GRAVITY, circle.acceleration_angle);
			//for (int i = 9; i >= 0; i--) {
			//	circle.update(circles);
			//	AreEqual(0, circle.vx);
			//	AreEqual(i * Constants.GRAVITY, circle.vy);
			//	AreEqual(0, circle.ax);
			//	AreEqual(-1 * Constants.GRAVITY, circle.ay);
			//}
		}

		[TestMethod]
		public void updateVelocity() {
			Circle circle = new Circle(1, 1, 2);
			HashSet<Circle> circles = new HashSet<Circle>();

			for (int i = 1; i <= 10; i++) {
				circle.update(circles);
				AreEqual(0, circle.vx);
				AreEqual(i * Constants.GRAVITY, circle.vy);
				AreEqual(0, circle.ax);
				AreEqual(Constants.GRAVITY, circle.ay);
			}

			circle.updateAcceleration(-1 * Constants.GRAVITY, circle.acceleration_angle);
			for (int i = 10; i > 0; i--) {
				circle.update(circles);
				AreEqual(0, circle.vx);
				AreEqual(10 * Constants.GRAVITY, circle.vy);
				AreEqual(0, circle.ax);
				AreEqual(0, circle.ay);
			}

			//circle.updateAcceleration(-1 * Constants.GRAVITY, circle.acceleration_angle);
			//for (int i = 9; i >= 0; i--) {
			//	circle.update(circles);
			//	AreEqual(0, circle.vx);
			//	AreEqual(i * Constants.GRAVITY, circle.vy);
			//	AreEqual(0, circle.ax);
			//	AreEqual(-1 * Constants.GRAVITY, circle.ay);
			//}
		}

		[TestMethod]
		public void updatePosition() {
			// need to check position, velocity, and acceleration
		}

		[TestMethod]
		public void collideTest() {

		}

		[TestMethod]

		public void moveTest() {

		}

		[TestMethod]
		public void updateTest() {

		}

		[TestMethod]
		public void drawTest() {
			// is this even testable?
		}

		[TestMethod]
		public void distanceTest() {
			// gonna have to make the method public to do this
		}

		//private void AreEqual(double expected, double actual) {
		//	Assert.AreEqual(expected, actual);
		//}

		private void AreEqual(double expected, double actual) {
			System.Diagnostics.Debug.WriteLine("expected: " + expected + " actual: " + actual);
			AreEqual(true, Math.Abs(expected - actual) < EPSILON);
		}

		private void AreEqual(bool expected, bool actual) {
			Assert.AreEqual(expected, actual);
		}

		private void AreEqual(Object expected, Object actual) {
			Assert.AreEqual(expected, actual);
		}

		//private void AreEqual(String expected, String actual) {
		//	Assert.AreEqual(expected, actual);
		//}

		private double round(double value) {
			return Math.Round(value, PRECISION);
		}
	}
}