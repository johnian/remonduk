using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remonduk;
using Remonduk.Physics;

namespace TestSuite
{
	public static class Test
	{
		public const int PRECISION = 6;
		public const double EPSILON = .0001;

		public static void AreEqual(OrderedPair expected, OrderedPair actual)
		{
			AreEqual(expected.X, actual.X);
			AreEqual(expected.Y, actual.Y);
            
		}

		public static void AreClose(OrderedPair expected, OrderedPair actual)
		{
			AreEqual(Math.Round(expected.X), Math.Round(actual.X));
			AreEqual(Math.Round(expected.Y), Math.Round(actual.Y));
		}

		public static void AreEqual(double expected, double actual)
		{
			Out.WriteLine("expected: " + expected + " actual: " + actual);
			AreEqual(true, Math.Abs(expected - actual) < EPSILON);
		}

		public static void AreEqual(int expected, int actual)
		{
			Assert.AreEqual(expected, actual);
		}

		public static void AreEqual(bool expected, bool actual)
		{
			Assert.AreEqual(expected, actual);
		}

		public static void AreEqual(Object expected, Object actual)
		{
			Assert.AreEqual(expected, actual);
		}

		public static double round(double value)
		{
			return Math.Round(value, PRECISION);
		}
	}
}