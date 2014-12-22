using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using remonduk;

namespace TestSuite
{
	[TestClass]
	public class ForceTest
	{
		Constants constants = Constants.Instance;
		
		[TestMethod]
		public void forceTest()
		{
			Circle first = new Circle();
			Circle second = new Circle();
			Force force = new Force(
				delegate(Circle one, Circle two)
				{
					return Tuple.Create(5.0, 3.0);
				}
			);
			Interaction interaction = new Interaction(first, second, force);
			Test.AreEqual(Tuple.Create(5.0, 3.0), interaction.forceOnFirst());
			Test.AreEqual(Tuple.Create(5.0, 3.0), interaction.forceOnSecond());

			interaction = new Interaction(first, second, force, 0);
			Test.AreEqual(Tuple.Create(0.0, 0.0), interaction.forceOnFirst());
			Test.AreEqual(Tuple.Create(5.0, 3.0), interaction.forceOnSecond());
		}

		[TestMethod]
		public void gravityTest()
		{
			double earth_radius = 6371000;
			double earth_mass = 5.972 * Math.Pow(10, 24);
			Circle earth = new Circle(-1 * earth_radius, 0, earth_radius, earth_mass);
			Circle person = new Circle(0, 0, 10, 60);

			Gravity gravity = new Gravity(Gravity.G);
			Interaction interaction = new Interaction(earth, person, gravity);
			Test.AreClose(Tuple.Create(60 * 9.81, 0.0), interaction.forceOnFirst());
			Test.AreClose(Tuple.Create(-60 * 9.81, 0.0), interaction.forceOnSecond());
		}

		[TestMethod]
		public void tetherTest()
		{
			Circle one = new Circle(1, 2, 2, 3);
			Circle two = new Circle(5, 6, 13, 21);

			Tether tether = new Tether();
			Interaction interaction = new Interaction(one, two, tether);

			double force = 2 * (Math.Sqrt(32) - 3);
			Test.AreEqual(Tuple.Create(force * Math.Cos(Math.PI / 4), force * Math.Sin(Math.PI / 4)), interaction.forceOnFirst());
			Test.AreEqual(Tuple.Create(force * Math.Cos(5 * Math.PI / 4), force * Math.Sin(5 * Math.PI / 4)), interaction.forceOnSecond());
		}

		[TestMethod]
		public void netForce() {
			Circle one = new Circle(1, 2, 2, 3);
			Circle two = new Circle(5, 6, 13, 21);

			Force gravity = new Gravity(Gravity.GRAVITY, Gravity.ANGLE);
			Force tether = new Tether(2.0, 3.0);

			Interaction gravityOn12 = new Interaction(one, two, gravity);
			Interaction tetherOn12 = new Interaction(one, two, tether);

			PhysicalSystem world = new PhysicalSystem();
			world.addCircle(one);
			world.addCircle(two);
			Test.AreEqual(Tuple.Create(0.0, 0.0), world.netForceOn(one));
			Test.AreEqual(Tuple.Create(0.0, 0.0), world.netForceOn(two));

			world.addInteraction(gravityOn12);
			world.addInteraction(tetherOn12);

			// 8, 8
			double value = (Math.Sqrt(32) - 3) * Math.Sqrt(2);
			world.updateNetForces();
			Test.AreEqual(Tuple.Create(value, Gravity.GRAVITY + value), world.netForceOn(one));
			Test.AreEqual(Tuple.Create(-value, Gravity.GRAVITY - value), world.netForceOn(two));

			world.updateNetForces();
			Test.AreEqual(Tuple.Create(value, Gravity.GRAVITY + value), world.netForceOn(one));
			Test.AreEqual(Tuple.Create(-value, Gravity.GRAVITY - value), world.netForceOn(two));

			world.removeInteraction(gravityOn12);
			world.updateNetForces();
			Test.AreEqual(Tuple.Create(value, value), world.netForceOn(one));
			Test.AreEqual(Tuple.Create(-value, -value), world.netForceOn(two));

			world.removeInteraction(tetherOn12);
			world.updateNetForces();
			Test.AreEqual(Tuple.Create(0.0, 0.0), world.netForceOn(one));
			Test.AreEqual(Tuple.Create(0.0, 0.0), world.netForceOn(two));
		}
	}
}