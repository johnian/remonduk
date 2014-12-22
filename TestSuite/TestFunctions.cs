using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSuite
{
	public static class Test
	{
		public const int PRECISION = 6;
		public const double EPSILON = .0001;

		public static void AreEqual(Tuple<double, double> expected, Tuple<double, double> actual)
		{
			AreEqual(expected.Item1, actual.Item1);
			AreEqual(expected.Item2, actual.Item2);
		}

		public static void AreClose(Tuple<double, double> expected, Tuple<double, double> actual)
		{
			AreEqual(Math.Round(expected.Item1), Math.Round(actual.Item1));
			AreEqual(Math.Round(expected.Item2), Math.Round(actual.Item2));
		}

		public static void AreEqual(double expected, double actual)
		{
			System.Diagnostics.Debug.WriteLine("expected: " + expected + " actual: " + actual);
			AreEqual(true, Math.Abs(expected - actual) < EPSILON);
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