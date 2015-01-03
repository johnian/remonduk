using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remonduk;
using Remonduk.Physics;

namespace TestSuite
{
	[TestClass]
	public class TetherTest
	{
		int PRECISION = 6;
		double EPSILON = .0001;

		//[TestMethod]
		//public void tether1Test()
		//{
		//	Circle c1 = new Circle(0, 0, 2, 0, 0, 0, 0);
		//	Circle c2 = new Circle(200, 0, 2, 0, 0, 0, 0);
		//	double max_dist = 20;
		//	double k = .001;

		//	Tether t = new Tether(c1, c2, max_dist, k);

		//	AreEqual(c1, t.c1);
		//	AreEqual(c2, t.c2);
		//	AreEqual(max_dist, t.max_dist);
		//	AreEqual(k, t.k);

		//	t.pull();

		//	//AreEqual(t.c1.acceleration_angle, 0);
		//	//AreEqual(t.c2.acceleration_angle, Math.PI);
		//	//AreEqual(t.c1.acceleration, .18);
		//	//AreEqual(t.c2.acceleration, .18);

		//	t.c1.update(new HashSet<Circle>());
		//	t.c2.update(new HashSet<Circle>());

		//	AreEqual(t.c1.velocity_angle, 0);
		//	AreEqual(t.c2.velocity_angle, Math.PI);
		//	AreEqual(t.c1.velocity, .18);
		//	AreEqual(t.c2.velocity, .18);
		//	AreEqual(t.c1.x, .18);
		//	AreEqual(t.c2.x, 199.82);
		//}

		//[TestMethod]
		//public void tether2Test()
		//{
		//	Circle c1 = new Circle(0, 0, 2, 0, 0, 0, 0);
		//	Circle c2 = new Circle(200, 0, 2, 0, 0, 0, 0);
		//	Circle c3 = new Circle(0, 200, 2, 0, 0, 0, 0);
		//	double max_dist = 20;
		//	double k = .001;

		//	Tether t = new Tether(c1, c2, max_dist, k);
		//	Tether t2 = new Tether(c1, c3, max_dist, k);

		//	t.pull();
		//	t2.pull();

		//	AreEqual(Math.PI / 4.0, c1.velocity_angle);
		//	AreEqual(Math.PI, c2.velocity_angle);
		//	AreEqual(3 * Math.PI / 2, c3.velocity_angle);
		//	AreEqual(.18, c2.velocity);
		//	AreEqual(.18, c3.velocity);
		//	AreEqual(.36, c1.velocity);
		//}
	}
}