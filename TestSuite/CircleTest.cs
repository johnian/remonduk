using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using remonduk;

namespace TestSuite
{
	[TestClass]
	public class CircleTest
	{
		[TestMethod]
		public void ConstructorTest()
		{
			Circle circle = new Circle(2, 3, 5);

			Assert.AreEqual(circle.x, 2);
			Assert.AreEqual(circle.y, 3);
			Assert.AreEqual(circle.r, 5);

			Assert.AreEqual(circle.velocity, 0);
			Assert.AreEqual(circle.vx, 0);
			Assert.AreEqual(circle.vy, 0);
			Assert.AreEqual(circle.velocity_angle, 0);

			Assert.AreEqual(circle.acceleration, 0);
			Assert.AreEqual(circle.ax, 0);
			Assert.AreEqual(circle.ay, Circle.GRAVITY_CONSTANT);
			Assert.AreEqual(circle.acceleration_angle, 0);
		}
	}
}
