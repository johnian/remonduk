using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using remonduk;

namespace TestSuite
{
	[TestClass]
	public class CircleTest
	{
		int PRECISION = 6;
		[TestMethod]
		public void ConstructorTest()
		{
			// check that all other fields are set correctly
			Circle circle = new Circle(2, 3, 5);

			Assert.AreEqual(2, circle.x);
			Assert.AreEqual(3, circle.y);
			Assert.AreEqual(5, circle.r);

			Assert.AreEqual(0, circle.velocity);
			Assert.AreEqual(0, circle.velocity_angle);
			Assert.AreEqual(0, circle.vx);
			Assert.AreEqual(0, circle.vy);

			Assert.AreEqual(0, circle.acceleration);
			Assert.AreEqual(0, circle.acceleration_angle);
			Assert.AreEqual(0, circle.ax);
			Assert.AreEqual(Constants.GRAVITY, circle.ay);

			circle = new Circle(2, 3, 5, 8, Math.PI / 4);
			
			Assert.AreEqual(2, circle.x);
			Assert.AreEqual(3, circle.y);
			Assert.AreEqual(5, circle.r);

			Assert.AreEqual(8, circle.velocity);
			Assert.AreEqual(Math.PI / 4, circle.velocity_angle);
			AreClose(4 * Math.Sqrt(2), circle.vx);
			AreClose(4 * Math.Sqrt(2), circle.vy);

			Assert.AreEqual(0, circle.acceleration);
			Assert.AreEqual(0, circle.acceleration_angle);
			Assert.AreEqual(0, circle.ax);
			Assert.AreEqual(Constants.GRAVITY, circle.ay);

			circle = new Circle(2, 3, 5, 8, Math.PI / 4, 13, 1 * Math.PI / 4);

			Assert.AreEqual(2, circle.x);
			Assert.AreEqual(3, circle.y);
			Assert.AreEqual(5, circle.r);

			Assert.AreEqual(8, circle.velocity);
			Assert.AreEqual(circle.velocity_angle, Math.PI / 4);
			AreClose(4 * Math.Sqrt(2), circle.vx);
			AreClose(4 * Math.Sqrt(2), circle.vy);

			Assert.AreEqual(13, circle.acceleration);
			Assert.AreEqual(Math.PI / 4, circle.acceleration_angle);
			AreClose(6.5 * Math.Sqrt(2), circle.ax);
			AreClose(-1 * 6.5 * Math.Sqrt(2) + Constants.GRAVITY, circle.ay);
		}
		[TestMethod]
		public void leashTest() {

		}

		[TestMethod]
		public void followTest() {

		}

		[TestMethod]
		public void setATest() {

		}

		[TestMethod]
		public void setVTest() {

		}

		[TestMethod]
		public void updatePosition() {

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

		private void AreClose(double expected, double actual) {
			Assert.AreEqual(round(expected), round(actual));
		}

		private double round(double value) {
			return Math.Round(value, PRECISION);
		}
	}
}