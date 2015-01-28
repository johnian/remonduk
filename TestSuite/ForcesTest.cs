using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remonduk.Physics;

namespace TestSuite
{
	[TestClass]
	public class ForcesTest
	{
		[TestMethod]
		public void ForceTest()
		{
			Circle first = new Circle();
			Circle second = new Circle();
			Force force = new Force(
				delegate(Circle one, Circle two)
				{
					return new OrderedPair(5.0, 3.0);
				}
			);
			Test.AreEqual(new OrderedPair(5.0, 3.0), force.Calculate(first, second));
			Test.AreEqual(new OrderedPair(5.0, 3.0), force.Calculate(second, first));

			first = new Circle(1, 2, 3);
			second = new Circle(6, 5, 4);
			force = new Force(
				delegate(Circle one, Circle two)
				{
					return new OrderedPair(one.Px - two.Px, one.Py - two.Py);
				}
			);
			Test.AreEqual(new OrderedPair(-3.0, -1.0), force.Calculate(first, second));
			Test.AreEqual(new OrderedPair(3.0, 1.0), force.Calculate(second, first));

			first = new Circle(1, 2, 3);
			second = new Circle(6, 5, 4);
			force = new Force(
				delegate(Circle one, Circle two)
				{
					return new OrderedPair(one.Px - two.Px + one.Radius, one.Py - two.Py + two.Radius);
				}
			);
			Test.AreEqual(new OrderedPair(-2.0, 5.0), force.Calculate(first, second));
			Test.AreEqual(new OrderedPair(9.0, 2.0), force.Calculate(second, first));
		}

		[TestMethod]
		public void GravityTest1()
		{
			double earthRadius = 6371000;
			double earthMass = 5.972 * Math.Pow(10, 24);
			Circle earth = new Circle(earthRadius, -1 * earthRadius, 0, earthMass);
			Circle person = new Circle(10, 0, 0, 60);

			Gravity gravity = new Gravity(.0000000000667384);
			Test.AreClose(new OrderedPair(60 * 9.81, 0.0), gravity.Calculate(earth, person));
			Test.AreClose(new OrderedPair(-60 * 9.81, 0.0), gravity.Calculate(person, earth));
		}

		[TestMethod]
		public void GravityTest2()
		{
			Circle one = new Circle();
			Circle two = new Circle();

			Gravity gravity = new Gravity();
			Test.AreEqual(new OrderedPair(0, 1), gravity.Calculate(one, two));
			Test.AreEqual(new OrderedPair(0, 1), gravity.Calculate(two, one));

			gravity = new Gravity(12, 13);
			Test.AreEqual(new OrderedPair(12, 13), gravity.Calculate(one, two));
			Test.AreEqual(new OrderedPair(12, 13), gravity.Calculate(two, one));
		}

		[TestMethod]
		public void SpringTest2()
		{
			Circle one = new Circle(1);
			Circle two = new Circle(3);
			Spring spring = new Spring();
			Test.AreEqual(new OrderedPair(-4 * Spring.K, 0), spring.Calculate(one, two));
			Test.AreEqual(new OrderedPair(4 * Spring.K, 0), spring.Calculate(two, one));
		}

		[TestMethod]
		public void SpringTest3()
		{
			Test.AreEqual(true, false);
		}

		[TestMethod]
		public void SpringTest4()
		{
			Test.AreEqual(true, false);
		}
	}
}