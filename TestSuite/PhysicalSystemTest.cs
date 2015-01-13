using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remonduk;
using Remonduk.Physics;

namespace TestSuite
{
	[TestClass]
	public class PhysicalSystemTest
	{
		[TestMethod]
		public void PhysicalSystemTest0()
		{
			PhysicalSystem world = new PhysicalSystem();
			Test.AreEqual(0, world.Circles.Count);
			Test.AreEqual(0, world.NetForces.Keys.Count);

			Test.AreEqual(0, world.Interactions.Count);
			Test.AreEqual(0, world.InteractionMap.Keys.Count);

			Test.AreEqual(1, world.Forces.Keys.Count);
			//if (world.GravityOn)
			//{
				Test.AreEqual(true, world.Forces.ContainsKey("Gravity"));
				//Test.AreEqual(new Force(), world.Forces["Gravity"].Force);
			//}

			Test.AreEqual(false, world.Tree == null);
		}

		[TestMethod]
		public void AddCircleTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			Circle circle = new Circle();
			world.AddCircle(circle);
			Test.AreEqual(true, world.Circles.Contains(circle));
			Test.AreEqual(new OrderedPair(0, 0), world.NetForces[circle]);
		}

		[TestMethod]
		public void RemoveCircleTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			Circle circle = new Circle();
			world.AddCircle(circle);
			world.RemoveCircle(circle);
			Test.AreEqual(false, world.Circles.Contains(circle));
			Test.AreEqual(false, world.NetForces.ContainsKey(circle));
			//Test.AreEqual(true, false);
			// add the interactions and check interactions are updated properly
		}

		[TestMethod]
		public void AddInteractionTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			Circle one = new Circle();
			Circle two = new Circle();
			world.AddCircle(one);
			world.AddCircle(two);
			Interaction interaction = new Interaction(one, two, world.Forces["Gravity"]);
			world.AddInteraction(interaction);
			Test.AreEqual(interaction, world.Interactions[0]);
			Test.AreEqual(interaction, world.InteractionMap[one][0]);
			Test.AreEqual(interaction, world.InteractionMap[two][0]);
		}

		[TestMethod]
		public void RemoveInteractionTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			Circle one = new Circle();
			Circle two = new Circle();
			world.AddCircle(one);
			world.AddCircle(two);
			Interaction interaction = new Interaction(one, two, world.Forces["Gravity"]);
			world.AddInteraction(interaction);
			world.RemoveInteraction(interaction);
			Test.AreEqual(0, world.Interactions.Count);
			Test.AreEqual(0, world.InteractionMap[one].Count);
			Test.AreEqual(0, world.InteractionMap[two].Count);
		}

		[TestMethod]
		public void updateNetForceOnTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			Circle one = new Circle();
			Circle two = new Circle();
			Circle three = new Circle();
			world.AddCircle(one);
			world.AddCircle(two);
			world.AddCircle(three);
			Gravity gravity = new Gravity(0, 9.8);
			Interaction interaction1 = new Interaction(one, two, gravity);
			Interaction interaction2 = new Interaction(one, two, gravity);

			world.AddInteraction(interaction1);
			world.AddInteraction(interaction2);
			//world.UpdateNetForceOn(one);
			Test.AreEqual(new OrderedPair(0, 19.6), world.NetForces[one]);
			Test.AreEqual(new OrderedPair(0, 0), world.NetForces[two]);
			Test.AreEqual(new OrderedPair(0, 0), world.NetForces[three]);
		}

		[TestMethod]
		public void updateNetForcesTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			Circle one = new Circle();
			Circle two = new Circle();
			Circle three = new Circle();
			world.AddCircle(one);
			world.AddCircle(two);
			world.AddCircle(three);
			Gravity gravity = new Gravity(0, 9.8);
			Interaction interaction1 = new Interaction(one, two, gravity);
			Interaction interaction2 = new Interaction(one, two, gravity);

			world.AddInteraction(interaction1);
			world.AddInteraction(interaction2);
			//world.UpdateNetForces();
			Test.AreEqual(new OrderedPair(0, 19.6), world.NetForces[one]);
			Test.AreEqual(new OrderedPair(0, 19.6), world.NetForces[two]);
			Test.AreEqual(new OrderedPair(0, 0), world.NetForces[three]);
		}

		[TestMethod]
		public void updateVelocitiesTest()
		{
			//Dictionary<Circle, List<Circle>> collisionMap = new Dictionary<Circle, List<Circle>>();
			//Dictionary<Circle, OrderedPair> velocityMap = new Dictionary<Circle, OrderedPair>();
			//foreach (Circle circle in collisionMap.Keys)
			//{
			//	velocityMap.Add(circle, circle.collideWith(collisionMap[circle]));
			//	//Out.WriteLine("updated velocities" + velocityMap[circle]);
			//}
			//foreach (Circle circle in velocityMap.Keys)
			//{
			//	circle.setVelocity(velocityMap[circle].x, velocityMap[circle].y);
			//}



		}

		[TestMethod]
		public void updatePositionsTest()
		{

		}

		[TestMethod]
		public void updateTest()
		{
			// nothing to do for this really
		}
	}
}
