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

			AreEqual(0, circle.acceleration);
			AreEqual(0, circle.acceleration_angle);
			AreEqual(0, circle.ax);
			AreEqual(0, circle.ay);

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

			AreEqual(8, circle.velocity);
			AreEqual(Math.PI / 4, circle.velocity_angle);
			AreEqual(4 * Math.Sqrt(2), circle.vx);
			AreEqual(4 * Math.Sqrt(2), circle.vy);

			AreEqual(0, circle.acceleration);
			AreEqual(0, circle.acceleration_angle);
			AreEqual(0, circle.ax);
			AreEqual(0, circle.ay);

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

			AreEqual(8, circle.velocity);
			AreEqual(circle.velocity_angle, Math.PI / 4);
			AreEqual(4 * Math.Sqrt(2), circle.vx);
			AreEqual(4 * Math.Sqrt(2), circle.vy);

			AreEqual(13, circle.acceleration);
			AreEqual(Math.PI / 4, circle.acceleration_angle);
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
		public void setATest() {
			Circle circle = new Circle(1, 1, 2);
			circle.setA(10, 0);
			AreEqual(10, circle.acceleration);
			AreEqual(0, circle.acceleration_angle);
			AreEqual(10, circle.ax);
			AreEqual(0, circle.ay);

			circle.setA(20, Math.PI / 2);
			AreEqual(20, circle.acceleration);
			AreEqual(Math.PI / 2, circle.acceleration_angle);
			AreEqual(0, circle.ax);
			AreEqual(20, circle.ay);

			circle.setA(30, Math.PI / 4);
			AreEqual(30, circle.acceleration);
			AreEqual(Math.PI / 4, circle.acceleration_angle);
			AreEqual(15 * Math.Sqrt(2), circle.ax);
			AreEqual(15 * Math.Sqrt(2), circle.ay);

			circle.setA(40, 5 * Math.PI / 4);
			AreEqual(40, circle.acceleration);
			AreEqual(5 * Math.PI / 4, circle.acceleration_angle);
			AreEqual(-20 * Math.Sqrt(2), circle.ax);
			AreEqual(-20 * Math.Sqrt(2), circle.ay);
		}

		[TestMethod]
		public void setVTest() {
			Circle circle = new Circle(1, 1, 2);
			circle.setV(10, 0);
			AreEqual(10, circle.velocity);
			AreEqual(0, circle.velocity_angle);
			AreEqual(10, circle.vx);
			AreEqual(0, circle.vy);

			circle.setV(20, Math.PI / 2);
			AreEqual(20, circle.velocity);
			AreEqual(Math.PI / 2, circle.velocity_angle);
			AreEqual(0, circle.vx);
			AreEqual(20, circle.vy);

			circle.setV(30, Math.PI / 4);
			AreEqual(30, circle.velocity);
			AreEqual(Math.PI / 4, circle.velocity_angle);
			AreEqual(15 * Math.Sqrt(2), circle.vx);
			AreEqual(15 * Math.Sqrt(2), circle.vy);

			circle.setV(40, 5 * Math.PI / 4);
			AreEqual(40, circle.velocity);
			AreEqual(5 * Math.PI / 4, circle.velocity_angle);
			AreEqual(-20 * Math.Sqrt(2), circle.vx);
			AreEqual(-20 * Math.Sqrt(2), circle.vy);
		}

		[TestMethod]
		public void gravityTest() {
			Circle circle = new Circle(1, 1, 2);
			List<Circle> circles = new List<Circle>();
			circle.update(circles);
			
			AreEqual(0, circle.acceleration);
			AreEqual(0, circle.ax);
			AreEqual(0, circle.ay);

			AreEqual(0, circle.velocity);
			AreEqual(0, circle.vx);
			AreEqual(0, circle.vy);

			//circle.setGravity();
			AreEqual(Constants.GRAVITY, circle.acceleration);
			AreEqual(0, circle.ax);
			AreEqual(Constants.GRAVITY, circle.ay);

			AreEqual(0, circle.velocity);
			AreEqual(0, circle.vx);
			AreEqual(0, circle.vy);

			circle.update(circles);
			AreEqual(Constants.GRAVITY, circle.acceleration);
			AreEqual(0, circle.ax);
			AreEqual(Constants.GRAVITY, circle.ay);

			AreEqual(Constants.GRAVITY, circle.velocity);
			AreEqual(0, circle.vx);
			AreEqual(Constants.GRAVITY, circle.vy);
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