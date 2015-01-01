using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using remonduk;

namespace TestSuite
{
	[TestClass]
	public class PhysicalSystemTest
	{
		[TestMethod]
		public void PhysicalSystemTestNoArg()
		{
			PhysicalSystem world = new PhysicalSystem();
			Test.AreEqual(0, world.circles.Count);
			Test.AreEqual(0, world.netForces.Keys.Count);
			Test.AreEqual(0, world.interactions.Count);
			Test.AreEqual(0, world.interactionMap.Keys.Count);
			Test.AreEqual(1, world.forces.Keys.Count);
			Test.AreEqual(true, world.forces.ContainsKey("Gravity"));
			Test.AreEqual(true, false);

			//Test.AreEqual(quad tree);
			//Test.AreEqual(world.GRAVITY, world.gravity);
		} 

		[TestMethod]
		public void addCircleTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			Circle circle = new Circle();
			world.addCircle(circle);
			Test.AreEqual(true, world.circles.Contains(circle));
			Test.AreEqual(new OrderedPair(0, 0), world.netForces[circle]);
		}

		[TestMethod]
		public void removeCircleTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			Circle circle = new Circle();
			world.addCircle(circle);
			world.removeCircle(circle);
			Test.AreEqual(false, world.circles.Contains(circle));
			Test.AreEqual(false, world.netForces.ContainsKey(circle));
			Test.AreEqual(true, false);
			// add the interactions and check interactions are updated properly
		}

		[TestMethod]
		public void addInteractionTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			Circle one = new Circle();
			Circle two = new Circle();
			world.addCircle(one);
			world.addCircle(two);
			Interaction interaction = new Interaction(one, two, world.forces["Gravity"]);
			world.addInteraction(interaction);
			Test.AreEqual(interaction, world.interactions[0]);
			Test.AreEqual(interaction, world.interactionMap[one][0]);
			Test.AreEqual(interaction, world.interactionMap[two][0]);
		}

		[TestMethod]
		public void removeInteractionTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			Circle one = new Circle();
			Circle two = new Circle();
			world.addCircle(one);
			world.addCircle(two);
			Interaction interaction = new Interaction(one, two, world.forces["Gravity"]);
			world.addInteraction(interaction);
			world.removeInteraction(interaction);
			Test.AreEqual(0, world.interactions.Count);
			Test.AreEqual(0, world.interactionMap[one].Count);
			Test.AreEqual(0, world.interactionMap[two].Count);
		}

		[TestMethod]
		public void updateNetForceOnTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			Circle one = new Circle();
			Circle two = new Circle();
			Circle three = new Circle();
			world.addCircle(one);
			world.addCircle(two);
			world.addCircle(three);
			Gravity gravity = new Gravity(0, 9.8);
			Interaction interaction1 = new Interaction(one, two, gravity);
			Interaction interaction2 = new Interaction(one, two, gravity);

			world.addInteraction(interaction1);
			world.addInteraction(interaction2);
			world.updateNetForceOn(one);
			Test.AreEqual(new OrderedPair(0, 19.6), world.netForces[one]);
			Test.AreEqual(new OrderedPair(0, 0), world.netForces[two]);
			Test.AreEqual(new OrderedPair(0, 0), world.netForces[three]);
		}

		[TestMethod]
		public void updateNetForcesTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			Circle one = new Circle();
			Circle two = new Circle();
			Circle three = new Circle();
			world.addCircle(one);
			world.addCircle(two);
			world.addCircle(three);
			Gravity gravity = new Gravity(0, 9.8);
			Interaction interaction1 = new Interaction(one, two, gravity);
			Interaction interaction2 = new Interaction(one, two, gravity);

			world.addInteraction(interaction1);
			world.addInteraction(interaction2);
			world.updateNetForces();
			Test.AreEqual(new OrderedPair(0, 19.6), world.netForces[one]);
			Test.AreEqual(new OrderedPair(0, 19.6), world.netForces[two]);
			Test.AreEqual(new OrderedPair(0, 0), world.netForces[three]);
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
