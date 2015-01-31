using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remonduk.Physics;

namespace TestSuite
{
	[TestClass]
	public class ForceTest
	{
		[TestMethod]
		public void ForceTest1()
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
	}
}