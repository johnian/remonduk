using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remonduk.Physics;

namespace TestSuite
{
	[TestClass]
	public class GravityTest
	{
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
	}
}
