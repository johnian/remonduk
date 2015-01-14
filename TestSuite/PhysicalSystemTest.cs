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
			Test.AreEqual(0, world.Forces.Count);
			Test.AreEqual(0, world.Interactions.Count);
			Test.AreEqual(0, world.InteractionMap.Count);

			Test.AreEqual(PhysicalSystem.TIME_STEP, world.TimeStep);
			Test.AreEqual(new OrderedPair(PhysicalSystem.WIDTH, PhysicalSystem.HEIGHT), world.Dimensions);
			Test.AreEqual(false, world.Tree == null);
		}

		[TestMethod]
		public void PhysicalSystemTest1()
		{
			double width = 400;
			PhysicalSystem world = new PhysicalSystem(width);
			Test.AreEqual(0, world.Circles.Count);
			Test.AreEqual(0, world.Forces.Count);
			Test.AreEqual(0, world.Interactions.Count);
			Test.AreEqual(0, world.InteractionMap.Count);

			Test.AreEqual(PhysicalSystem.TIME_STEP, world.TimeStep);
			Test.AreEqual(new OrderedPair(width, PhysicalSystem.HEIGHT), world.Dimensions);
			Test.AreEqual(false, world.Tree == null);
		}

		[TestMethod]
		public void PhysicalSystemTest2()
		{
			double width = 400;
			double height = 400;
			PhysicalSystem world = new PhysicalSystem(width, height);
			Test.AreEqual(0, world.Circles.Count);
			Test.AreEqual(0, world.Forces.Count);
			Test.AreEqual(0, world.Interactions.Count);
			Test.AreEqual(0, world.InteractionMap.Count);

			Test.AreEqual(PhysicalSystem.TIME_STEP, world.TimeStep);
			Test.AreEqual(new OrderedPair(width, height), world.Dimensions);
			Test.AreEqual(false, world.Tree == null);
		}

		[TestMethod]
		public void PhysicalSystemTest3()
		{
			double width = 400;
			double height = 400;
			double timeStep = 23;
			PhysicalSystem world = new PhysicalSystem(width, height, timeStep);
			Test.AreEqual(0, world.Circles.Count);
			Test.AreEqual(0, world.Forces.Count);
			Test.AreEqual(0, world.Interactions.Count);
			Test.AreEqual(0, world.InteractionMap.Count);

			Test.AreEqual(timeStep, world.TimeStep);
			Test.AreEqual(new OrderedPair(width, height), world.Dimensions);
			Test.AreEqual(false, world.Tree == null);
		}

		[TestMethod]
		public void AddCircleTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			for (int i = 1; i <= 10; i++)
			{
				Circle circle = new Circle();
				world.AddCircle(circle);
				Test.AreEqual(true, world.Circles.Contains(circle));
				Test.AreEqual(i, world.Circles.Count);
				Test.AreEqual(true, world.Tree.Circles.Contains(circle));
				Test.AreEqual(i, world.Tree.Circles.Count);
			}
		}

		[TestMethod]
		public void RemoveCircleTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			List<Circle> circles = new List<Circle>();
			int count = 10;
			for (int i = 1; i <= count; i++)
			{
				Circle circle = new Circle();
				world.AddCircle(circle);
				circles.Add(circle);
			}
			Test.AreEqual(false, world.RemoveCircle(new Circle()));
			for (int i = count - 1; i >= 0; i--)
			{
				Circle circle = circles[i];
				world.RemoveCircle(circle);
				Test.AreEqual(false, world.Circles.Contains(circle));
				Test.AreEqual(i, world.Circles.Count);
				Test.AreEqual(false, world.Tree.Circles.Contains(circle));
				Test.AreEqual(i, world.Tree.Circles.Count);
			}
		}

		[TestMethod]
		public void AddInteractionTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			Circle one = new Circle();
			Circle two = new Circle();
			world.AddCircle(one);
			world.AddCircle(two);
			Interaction interaction = new Interaction(one, two, new Gravity(0, 9.8));
			world.AddInteraction(interaction);
			Test.AreEqual(1, world.Interactions.Count);
			Test.AreEqual(1, world.InteractionMap[one].Count);
			Test.AreEqual(1, world.InteractionMap[two].Count);
			Test.AreEqual(interaction, world.Interactions[0]);
			Test.AreEqual(interaction, world.InteractionMap[one][0]);
			Test.AreEqual(interaction, world.InteractionMap[two][0]);
			Test.AreEqual(1, world.Interactions.Count);
			Test.AreEqual(1, world.InteractionMap[one].Count);
			Test.AreEqual(1, world.InteractionMap[two].Count);

			interaction = new Interaction(two, one, new Gravity(0, 9.8));
			world.AddInteraction(interaction);
			Test.AreEqual(2, world.Interactions.Count);
			Test.AreEqual(2, world.InteractionMap[one].Count);
			Test.AreEqual(2, world.InteractionMap[two].Count);
			Test.AreEqual(interaction, world.Interactions[1]);
			Test.AreEqual(interaction, world.InteractionMap[one][1]);
			Test.AreEqual(interaction, world.InteractionMap[two][1]);
		}

		[TestMethod]
		public void RemoveInteractionTest()
		{
			PhysicalSystem world = new PhysicalSystem();
			Circle one = new Circle();
			Circle two = new Circle();
			world.AddCircle(one);
			world.AddCircle(two);
			Interaction interaction1 = new Interaction(one, two, new Gravity(0, 9.8));
			world.AddInteraction(interaction1);
			Interaction interaction2 = new Interaction(two, one, new Gravity(0, 9.8));
			world.AddInteraction(interaction2);

			Interaction interaction3 =new Interaction(two, one, new Gravity(0, 9.8));
			Test.AreEqual(false,  world.RemoveInteraction(interaction3));

			Test.AreEqual(true, world.RemoveInteraction(interaction1));
			Test.AreEqual(1, world.Interactions.Count);
			Test.AreEqual(1, world.InteractionMap[one].Count);
			Test.AreEqual(1, world.InteractionMap[two].Count);
			Test.AreEqual(interaction2, world.Interactions[0]);
			Test.AreEqual(interaction2, world.InteractionMap[one][0]);
			Test.AreEqual(interaction2, world.InteractionMap[two][0]);

			Test.AreEqual(true, world.RemoveInteraction(interaction2));
			Test.AreEqual(0, world.Interactions.Count);
			Test.AreEqual(0, world.InteractionMap[one].Count);
			Test.AreEqual(0, world.InteractionMap[two].Count);
		}

		[TestMethod]
		public void UpdateVelocitiesTest()
		{

		}

		[TestMethod]
		public void UpdateCirclesTest()
		{

		}

		[TestMethod]
		public void UpdateCollisionMapTest()
		{

		}

		[TestMethod]
		public void NewCollisionMapTest()
		{

		}

		[TestMethod]
		public void FirstCollisionTest()
		{

		}

		[TestMethod]
		public void CheckCollisionsTest()
		{

		}

		[TestMethod]
		public void UpdateAccelerationTest()
		{

		}

		[TestMethod]
		public void UpdateAccelerationsTest()
		{

		}

		[TestMethod]
		public void UpdatePositions()
		{

		}
	}
}
