using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remonduk.Physics;

namespace TestSuite
{
	[TestClass]
	public class SpringTest
	{
		[TestMethod]
		public void SpringTest0()
		{
			Circle one = new Circle(1);
			Circle two = new Circle(3);
			Spring spring = new Spring();
			Test.AreEqual(new OrderedPair(0, 0), spring.Calculate(one, two));
			Test.AreEqual(new OrderedPair(0, 0), spring.Calculate(two, one));

			one = new Circle(1, 2, 3);
			two = new Circle(1, 5, 6);
			Test.AreEqual(
				new OrderedPair(
					Spring.K * (3 - Math.Sqrt(2)) / 2,
					Spring.K * (3 - Math.Sqrt(2)) / 2),
				spring.Calculate(one, two)
			);
			Test.AreEqual(
				new OrderedPair(
					-Spring.K * (3 - Math.Sqrt(2)) / 2,
					-Spring.K * (3 - Math.Sqrt(2)) / 2),
				spring.Calculate(two, one)
			);

			one = new Circle(1, 2, 3);
			two = new Circle(4, 5, 6);
			Test.AreEqual(
				new OrderedPair(
					Spring.K * (3 - 5 * Math.Sqrt(2) / 2) / 2,
					Spring.K * (3 - 5 * Math.Sqrt(2) / 2) / 2),
				spring.Calculate(one, two)
			);
			Test.AreEqual(
				new OrderedPair(
					-Spring.K * (3 - 5 * Math.Sqrt(2) / 2) / 2,
					-Spring.K * (3 - 5 * Math.Sqrt(2) / 2) / 2),
				spring.Calculate(two, one)
			);

			one = new Circle(1, 2, 3, 7, 8);
			two = new Circle(4, 5, 6, 9, 10);
			Test.AreEqual(
				new OrderedPair(
					Spring.K * (3 - 5 * Math.Sqrt(2) / 2) / 2 - Spring.C * -2,
					Spring.K * (3 - 5 * Math.Sqrt(2) / 2) / 2 - Spring.C * -2),
				spring.Calculate(one, two)
			);
			Test.AreEqual(
				new OrderedPair(
					-Spring.K * (3 - 5 * Math.Sqrt(2) / 2) / 2 - Spring.C * 2,
					-Spring.K * (3 - 5 * Math.Sqrt(2) / 2) / 2 - Spring.C * 2),
				spring.Calculate(two, one)
			);
		}

		[TestMethod]
		public void SpringTest1()
		{
			Spring spring = new Spring(2);
			Circle one = new Circle(1, 2, 3, 7, 8);
			Circle two = new Circle(4, 5, 6, 9, 10);
			Test.AreEqual(
				new OrderedPair(
					2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - Spring.C * -2,
					2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - Spring.C * -2),
				spring.Calculate(one, two)
			);
			Test.AreEqual(
				new OrderedPair(
					-2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - Spring.C * 2,
					-2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - Spring.C * 2),
				spring.Calculate(two, one)
			);
		}

		[TestMethod]
		public void SpringTest2()
		{
			Spring spring = new Spring(2, 3);
			Circle one = new Circle(1, 2, 3, 7, 8);
			Circle two = new Circle(4, 5, 6, 9, 10);
			Test.AreEqual(
				new OrderedPair(
					2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - 3 * -2,
					2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - 3 * -2),
				spring.Calculate(one, two)
			);
			Test.AreEqual(
				new OrderedPair(
					-2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - 3 * 2,
					-2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - 3 * 2),
				spring.Calculate(two, one)
			);
		}

		[TestMethod]
		public void SpringTest3()
		{
			Test.AreEqual(true, false);
			Spring spring = new Spring(2, 3, 5);
			Circle one = new Circle(1, 2, 3, 7, 8);
			Circle two = new Circle(4, 5, 6, 9, 10);
			Test.AreEqual(
				new OrderedPair(
					2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - 5 * -2,
					2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - 5 * -2),
				spring.Calculate(one, two)
			);
			Test.AreEqual(
				new OrderedPair(
					-2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - 5 * 2,
					-2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - 5 * 2),
				spring.Calculate(two, one)
			);
		}

		[TestMethod]
		public void SpringTest4()
		{
			Test.AreEqual(true, false);
			Spring spring = new Spring(2, 3, 5, 8);
			Circle one = new Circle(1, 2, 3, 7, 8);
			Circle two = new Circle(4, 5, 6, 9, 10);
			Test.AreEqual(
				new OrderedPair(
					2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - 5 * -2,
					2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - 5 * -2),
				spring.Calculate(one, two)
			);
			Test.AreEqual(
				new OrderedPair(
					-2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - 5 * 2,
					-2 * (3 - 5 * Math.Sqrt(2) / 2) / 2 - 5 * 2),
				spring.Calculate(two, one)
			);
		}
	}
}
