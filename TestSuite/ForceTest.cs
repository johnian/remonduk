using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using remonduk;

namespace TestSuite
{
	[TestClass]
	public class ForceTest
	{
		Constants constants = Constants.Instance;
		int PRECISION = 6;
		double EPSILON = .0001;

		[TestMethod]
		public void forceTest()
		{
			Force force = new Force(new Circle(), new Circle(), delegate(Circle one, Circle two) {
					return 5.0; 
				}
			);
			AreEqual(5, force.calculateForce());
		}

		[TestMethod]
		public void gravityTest() {
			double g = 6.67384 * Math.Pow(10, -11);
			Circle earth = new Circle(0, 0, 6371000, 5.972 * Math.Pow(10, 24));
			Circle person = new Circle(0, 0, 10, 60);
			Force gravity = new Force(earth, person, delegate(Circle planet, Circle body) {
					double planet_pos = planet.r;
					double body_pos = body.magnitude(body.x, body.y);
					double r_squared = (planet_pos + body_pos) * (planet_pos + body_pos);
					return g * planet.mass * body.mass / r_squared;
				}
			);
			person.addForce(gravity);
			AreEqual(60 * 9.8, gravity.calculateForce());

			//AreEqual(10, person.getForce().calculateForce());
		}

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

		private double round(double value)
		{
			return Math.Round(value, PRECISION);
		}
	}
}