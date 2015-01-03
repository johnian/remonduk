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
					return new OrderedPair(5.0, 3.0);
				}
			);
			Interaction interaction = new Interaction(first, second, force);
			Test.AreEqual(new OrderedPair(5.0, 3.0), interaction.ForceOnFirst());
			Test.AreEqual(new OrderedPair(5.0, 3.0), interaction.ForceOnSecond());

			interaction = new Interaction(first, second, force, 0);
			Test.AreEqual(new OrderedPair(0.0, 0.0), interaction.ForceOnFirst());
			Test.AreEqual(new OrderedPair(5.0, 3.0), interaction.ForceOnSecond());
		}

		[TestMethod]
		public void gravityTest()
		{
			double earth_radius = 6371000;
			double earth_mass = 5.972 * Math.Pow(10, 24);
			Circle earth = new Circle(earth_radius, -1 * earth_radius, 0, earth_mass);
			Circle person = new Circle(10, 0, 0, 60);

			Gravity gravity = new Gravity(Gravity.G);
			Interaction interaction = new Interaction(earth, person, gravity);
			Test.AreClose(new OrderedPair(60 * 9.81, 0.0), interaction.ForceOnFirst());
			Test.AreClose(new OrderedPair(-60 * 9.81, 0.0), interaction.ForceOnSecond());
		}

		[TestMethod]
		public void tetherTest()
		{
			Circle one = new Circle(2, 1, 2, 3);
			Circle two = new Circle(13, 5, 6, 21);

			Tether tether = new Tether();
			Interaction interaction = new Interaction(one, two, tether);

			double force = 2 * (Math.Sqrt(32) - 3);
			Test.AreEqual(new OrderedPair(force * Math.Cos(Math.PI / 4), force * Math.Sin(Math.PI / 4)), interaction.ForceOnFirst());
			Test.AreEqual(new OrderedPair(force * Math.Cos(5 * Math.PI / 4), force * Math.Sin(5 * Math.PI / 4)), interaction.ForceOnSecond());
		}

		[TestMethod]
		public void netForce() {
			Circle one = new Circle(2, 1, 2, 3);
			Circle two = new Circle(13, 5, 6, 21);

			Force gravity = new Gravity(0, Gravity.GRAVITY);
			Force tether = new Tether(2.0, 3.0);

			Interaction gravityOn12 = new Interaction(one, two, gravity);
			Interaction tetherOn12 = new Interaction(one, two, tether);

			PhysicalSystem world = new PhysicalSystem();
			world.AddCircle(one);
			world.AddCircle(two);
			Test.AreEqual(new OrderedPair(0.0, 0.0), world.NetForces[one]);
			Test.AreEqual(new OrderedPair(0.0, 0.0), world.NetForces[two]);

			world.AddInteraction(gravityOn12);
			world.AddInteraction(tetherOn12);

			// 8, 8
			double value = (Math.Sqrt(32) - 3) * Math.Sqrt(2);
			world.UpdateNetForces();
			Test.AreEqual(new OrderedPair(value, Gravity.GRAVITY + value), world.NetForces[one]);
			Test.AreEqual(new OrderedPair(-value, Gravity.GRAVITY - value), world.NetForces[two]);

			world.UpdateNetForces();
			Test.AreEqual(new OrderedPair(value, Gravity.GRAVITY + value), world.NetForces[one]);
			Test.AreEqual(new OrderedPair(-value, Gravity.GRAVITY - value), world.NetForces[two]);

			world.RemoveInteraction(gravityOn12);
			world.UpdateNetForces();
			Test.AreEqual(new OrderedPair(value, value), world.NetForces[one]);
			Test.AreEqual(new OrderedPair(-value, -value), world.NetForces[two]);

			world.RemoveInteraction(tetherOn12);
			world.UpdateNetForces();
			Test.AreEqual(new OrderedPair(0.0, 0.0), world.NetForces[one]);
			Test.AreEqual(new OrderedPair(0.0, 0.0), world.NetForces[two]);
		}
	}
}