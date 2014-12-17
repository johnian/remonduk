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

			AreEqual(0, circle.vx);
			AreEqual(0, circle.vy);

			AreEqual(0, circle.ax);
			AreEqual(Constants.GRAVITY, circle.ay);

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

			AreEqual(4 * Math.Sqrt(2), circle.vx);
			AreEqual(4 * Math.Sqrt(2), circle.vy);

			AreEqual(0, circle.ax);
			AreEqual(Constants.GRAVITY, circle.ay);

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

			AreEqual(4 * Math.Sqrt(2), circle.vx);
			AreEqual(4 * Math.Sqrt(2), circle.vy);

			AreEqual(6.5 * Math.Sqrt(2), circle.ax);
			AreEqual(6.5 * Math.Sqrt(2), circle.ay);

			circle = new Circle(2, 3, 5, 8, Math.PI / 4, 13, 1 * Math.PI / 4, 21F);
			AreEqual(21F, circle.mass);
		}

		[TestMethod]
		public void followTest() {
			Circle leader = new Circle(1, 1, 2);
			Circle sheep = new Circle(3, 5, 8);

			AreEqual(false, leader.following);
			AreEqual(null, leader.target);
			AreEqual(false, sheep.following);
			AreEqual(null, sheep.target);

			sheep.follow(leader);
			AreEqual(true, sheep.following);
			AreEqual(leader, sheep.target);
			AreEqual(false, leader.following);
			AreEqual(null, leader.target);
		}

		[TestMethod]
		public void setAccelerationTest() {
			Circle circle = new Circle(1, 1, 2);
			circle.setAcceleration(10, 0);
			AreEqual(10, circle.ax);
			AreEqual(0, circle.ay);

			circle.setAcceleration(20, Math.PI / 2);
			AreEqual(0, circle.ax);
			AreEqual(20, circle.ay);

			circle.setAcceleration(30, Math.PI / 4);
			AreEqual(15 * Math.Sqrt(2), circle.ax);
			AreEqual(15 * Math.Sqrt(2), circle.ay);

			circle.setAcceleration(40, 5 * Math.PI / 4);
			AreEqual(-20 * Math.Sqrt(2), circle.ax);
			AreEqual(-20 * Math.Sqrt(2), circle.ay);
		}

		[TestMethod]
		public void setVelocityTest() {
			Circle circle = new Circle(1, 1, 2);
			circle.setVelocity(10, 0);
			AreEqual(10, circle.vx);
			AreEqual(0, circle.vy);

			circle.setVelocity(20, Math.PI / 2);
			AreEqual(0, circle.vx);
			AreEqual(20, circle.vy);

			circle.setVelocity(30, Math.PI / 4);
			AreEqual(15 * Math.Sqrt(2), circle.vx);
			AreEqual(15 * Math.Sqrt(2), circle.vy);

			circle.setVelocity(40, 5 * Math.PI / 4);
			AreEqual(-20 * Math.Sqrt(2), circle.vx);
			AreEqual(-20 * Math.Sqrt(2), circle.vy);
		}

		[TestMethod]
		public void gravityTest() {
			Circle circle = new Circle(1, 1, 2);
			List<Circle> circles = new List<Circle>();

			AreEqual(0, circle.vx);
			AreEqual(0, circle.vy);
			AreEqual(0, circle.ax);
			AreEqual(Constants.GRAVITY, circle.ay);

			for (int i = 1; i <= 10; i++) {
				circle.update(circles);
				AreEqual(0, circle.vx);
				AreEqual(i * Constants.GRAVITY, circle.vy);
				AreEqual(0, circle.ax);
				AreEqual(Constants.GRAVITY, circle.ay);
			}
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

		private double round(double value) {
			return Math.Round(value, PRECISION);
		}
	}
}