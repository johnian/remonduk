using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remonduk;
using Remonduk.Physics;

namespace TestSuite
{
	[TestClass]
	public class OrderedPairTest
	{
		
		[TestMethod]
		public void Magnitude()
		{
			Test.AreEqual(10, OrderedPair.Magnitude(6.0, 8.0));
		}

		[TestMethod]
		public void Angle()
		{
			Test.AreEqual(0, OrderedPair.Angle(0, 0));
			Test.AreEqual(0, OrderedPair.Angle(0.0, 1.0));
			Test.AreEqual(1 * Math.PI / 4, OrderedPair.Angle(1.0, 1.0));
			Test.AreEqual(2 * Math.PI / 4, OrderedPair.Angle(1.0, 0.0));
			Test.AreEqual(3 * Math.PI / 4, OrderedPair.Angle(1.0, -1.0));
			Test.AreEqual(4 * Math.PI / 4, OrderedPair.Angle(0.0, -1.0));
			Test.AreEqual(5 * Math.PI / 4, OrderedPair.Angle(-1.0, -1.0));
			Test.AreEqual(6 * Math.PI / 4, OrderedPair.Angle(-1.0, 0.0));
			Test.AreEqual(7 * Math.PI / 4, OrderedPair.Angle(-1.0, 1.0));
		}
	}
}
