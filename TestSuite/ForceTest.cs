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
			Circle first = new Circle();
			Circle second = new Circle();
			Force force = new Force(delegate(Circle one, Circle two) {
					return 5.0; 
				}
			);
			Interaction interaction = new Interaction(first, second, force);
			AreEqual(5, interaction.forceOnFirst());
			AreEqual(5, interaction.forceOnSecond());

			interaction = new Interaction(first, second, force, 0);
			AreEqual(5, interaction.forceOnFirst());
			AreEqual(0, interaction.forceOnSecond());
		}

		[TestMethod]
		public void gravityTest() {
			double earth_radius = 6371000;
			double earth_mass = 5.972 * Math.Pow(10, 24);
			Circle earth = new Circle(0, 0, earth_radius, earth_mass);
			Circle person = new Circle(0, 0, 10, 60);

			Force gravity = new Force(delegate(Circle planet, Circle body) {
					double G = 6.67384 * Math.Pow(10, -11);
					double planet_pos = planet.r;
					double body_pos = 2 * body.r;
					double r_squared = (planet_pos + body_pos) * (planet_pos + body_pos);
					return G * planet.mass * body.mass / r_squared;
				}
			);
			Interaction interaction = new Interaction(earth, person, gravity);
			AreEqual(Math.Round(60 * 9.81), Math.Round(interaction.forceOnFirst()));
			AreEqual(Math.Round(60 * 9.81), Math.Round(interaction.forceOnSecond()));
		}

		[TestMethod]
		public void elasticityTest() {
			Circle one = new Circle(1, 1, 2, 3);
			Circle two = new Circle(5, 8, 13, 21);
			
			Force elasticity = new Force(delegate(Circle first, Circle second) {
					double k = 2;
					double equilibrium = 3;
					double dist = first.distance(second); // what should this formula be
					if (dist > equilibrium) {
						dist -= equilibrium;
					}
					return k * dist;
				}
			);
			Interaction interaction = new Interaction(one, two, elasticity);
			AreEqual(2 * (Math.Sqrt(65) - 3), interaction.forceOnFirst());
			AreEqual(2 * (Math.Sqrt(65) - 3), interaction.forceOnSecond());
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