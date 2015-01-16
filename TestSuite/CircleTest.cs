using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remonduk.Physics;

namespace TestSuite
{
	[TestClass]
	public partial class CircleTest
	{
		Constants constants = Constants.Instance;

		[TestMethod]
		public void CircleTest0()
		{
			Circle circle = new Circle();
			Test.AreEqual(Circle.RADIUS, circle.Radius);
			Test.AreEqual(Circle.MASS, circle.Mass);

			Test.AreEqual(Circle.PX, circle.Px);
			Test.AreEqual(Circle.PY, circle.Py);
			Test.AreEqual(Circle.VX, circle.Vx);
			Test.AreEqual(Circle.VY, circle.Vy);
			Test.AreEqual(Circle.AX, circle.Ax);
			Test.AreEqual(Circle.AY, circle.Ay);

			Test.AreEqual(Circle.EXISTS, circle.Exists);
			//Test.AreEqual(Circle.TARGET, circle.Target);
			//Test.AreEqual(Circle.MIN_DISTANCE, circle.MinDistance);
			//Test.AreEqual(Circle.MAX_DISTANCE, circle.MaxDistance);

			Test.AreEqual(circle.COLOR, circle.Color);
			//Test.AreEqual(false, circle.QTreePos == null);
		}

		[TestMethod]
		public void CircleTest1()
		{
			Circle original = new Circle(2, 3, 5);
			Circle circle = new Circle(original);
			Test.AreEqual(2, circle.Radius);
			Test.AreEqual(Circle.MASS, circle.Mass);

			Test.AreEqual(3, circle.Px);
			Test.AreEqual(5, circle.Py);
			Test.AreEqual(Circle.VX, circle.Vx);
			Test.AreEqual(Circle.VY, circle.Vy);
			Test.AreEqual(Circle.AX, circle.Ax);
			Test.AreEqual(Circle.AY, circle.Ay);

			Test.AreEqual(Circle.EXISTS, circle.Exists);
			//Test.AreEqual(Circle.TARGET, circle.Target);
			//Test.AreEqual(Circle.MIN_DISTANCE, circle.MinDistance);
			//Test.AreEqual(Circle.MAX_DISTANCE, circle.MaxDistance);

			Test.AreEqual(circle.COLOR, circle.Color);
			//Test.AreEqual(false, circle.QTreePos == null);
		}

		[TestMethod]
		public void CircleTest2()
		{
			Circle circle = new Circle(2);

			Test.AreEqual(2, circle.Radius);
			Test.AreEqual(Circle.MASS, circle.Mass);

			Test.AreEqual(Circle.PX, circle.Px);
			Test.AreEqual(Circle.PY, circle.Py);
			Test.AreEqual(Circle.VX, circle.Vx);
			Test.AreEqual(Circle.VY, circle.Vy);
			Test.AreEqual(Circle.AX, circle.Ax);
			Test.AreEqual(Circle.AY, circle.Ay);

			Test.AreEqual(Circle.EXISTS, circle.Exists);
			//Test.AreEqual(Circle.TARGET, circle.Target);
			//Test.AreEqual(Circle.MIN_DISTANCE, circle.MinDistance);
			//Test.AreEqual(Circle.MAX_DISTANCE, circle.MaxDistance);

			Test.AreEqual(circle.COLOR, circle.Color);
			//Test.AreEqual(false, circle.QTreePos == null);

			circle = new Circle(3, 5);
			Test.AreEqual(5, circle.Mass);
		}

		[TestMethod]
		public void CircleTest4()
		{
			Circle circle = new Circle(2, 3, 5);

			Test.AreEqual(2, circle.Radius);
			Test.AreEqual(Circle.MASS, circle.Mass);

			Test.AreEqual(3, circle.Px);
			Test.AreEqual(5, circle.Py);
			Test.AreEqual(Circle.VX, circle.Vx);
			Test.AreEqual(Circle.VY, circle.Vy);
			Test.AreEqual(Circle.AX, circle.Ax);
			Test.AreEqual(Circle.AY, circle.Ay);

			Test.AreEqual(Circle.EXISTS, circle.Exists);
			//Test.AreEqual(Circle.TARGET, circle.Target);
			//Test.AreEqual(Circle.MIN_DISTANCE, circle.MinDistance);
			//Test.AreEqual(Circle.MAX_DISTANCE, circle.MaxDistance);

			Test.AreEqual(circle.COLOR, circle.Color);
			//Test.AreEqual(false, circle.QTreePos == null);

			circle = new Circle(3, 5, 8, 13);
			Test.AreEqual(13, circle.Mass);
		}

		[TestMethod]
		public void CircleTest6()
		{
			Circle circle = new Circle(2, 3, 5, 8, 13);

			Test.AreEqual(2, circle.Radius);
			Test.AreEqual(Circle.MASS, circle.Mass);

			Test.AreEqual(3, circle.Px);
			Test.AreEqual(5, circle.Py);
			Test.AreEqual(8, circle.Vx);
			Test.AreEqual(13, circle.Vy);
			Test.AreEqual(Circle.AX, circle.Ax);
			Test.AreEqual(Circle.AY, circle.Ay);

			Test.AreEqual(Circle.EXISTS, circle.Exists);
			//Test.AreEqual(Circle.TARGET, circle.Target);
			//Test.AreEqual(Circle.MIN_DISTANCE, circle.MinDistance);
			//Test.AreEqual(Circle.MAX_DISTANCE, circle.MaxDistance);

			Test.AreEqual(circle.COLOR, circle.Color);
			//Test.AreEqual(false, circle.QTreePos == null);

			circle = new Circle(3, 5, 8, 13, 21, 34);
			Test.AreEqual(34, circle.Mass);
		}

		[TestMethod]
		public void CircleTest8()
		{
			Circle circle = new Circle(2, 3, 5, 8, 13, 21, 34);

			Test.AreEqual(2, circle.Radius);
			Test.AreEqual(Circle.MASS, circle.Mass);

			Test.AreEqual(3, circle.Px);
			Test.AreEqual(5, circle.Py);
			Test.AreEqual(8, circle.Vx);
			Test.AreEqual(13, circle.Vy);
			Test.AreEqual(21, circle.Ax);
			Test.AreEqual(34, circle.Ay);

			Test.AreEqual(Circle.EXISTS, circle.Exists);
			//Test.AreEqual(Circle.TARGET, circle.Target);
			//Test.AreEqual(Circle.MIN_DISTANCE, circle.MinDistance);
			//Test.AreEqual(Circle.MAX_DISTANCE, circle.MaxDistance);

			Test.AreEqual(circle.COLOR, circle.Color);
			//Test.AreEqual(false, circle.QTreePos == null);

			circle = new Circle(3, 5, 8, 13, 21, 34, 55, 89);
			Test.AreEqual(89, circle.Mass);
		}

		[TestMethod]
		public void SetRadiusTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.SetRadius(10);
			Test.AreEqual(10, circle.Radius);
			circle.SetRadius(1);
			Test.AreEqual(1, circle.Radius);

			try
			{
				circle.SetRadius(.99);
				Assert.Fail();
			}
			catch (ArgumentException e)
			{
				Test.AreEqual("radius: 0.99", e.Message);
			}
			try
			{
				circle.SetRadius(0);
				Assert.Fail();
			}
			catch (ArgumentException e)
			{
				Test.AreEqual("radius: 0", e.Message);
			}
			try
			{
				circle.SetRadius(-1.0);
				Assert.Fail();
			}
			catch (ArgumentException e)
			{
				Test.AreEqual("radius: -1", e.Message);
			}
		}

		[TestMethod]
		public void SetMassTest()
		{
			Circle circle = new Circle(1, 1, 2);
			Test.AreEqual(Circle.MASS, circle.Mass);
			circle.SetMass(10);
			Test.AreEqual(10, circle.Mass);
			circle.SetMass(0);
			Test.AreEqual(0, circle.Mass);
			circle.SetMass(-1);
			Test.AreEqual(-1, circle.Mass);
		}

		[TestMethod]
		public void SetPositionTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.SetPosition(3, 5);
			Test.AreEqual(3, circle.Px);
			Test.AreEqual(5, circle.Py);

			circle.SetPosition(8, 13);
			Test.AreEqual(8, circle.Px);
			Test.AreEqual(13, circle.Py);
		}

		[TestMethod]
		public void SetVelocityTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.SetVelocity(3, 5);
			Test.AreEqual(3, circle.Vx);
			Test.AreEqual(5, circle.Vy);

			circle.SetVelocity(8, 13);
			Test.AreEqual(8, circle.Vx);
			Test.AreEqual(13, circle.Vy);
		}

		[TestMethod]
		public void SetAccelerationTest()
		{
			Circle circle = new Circle(1, 1, 2);
			circle.SetAcceleration(3, 5);
			Test.AreEqual(3, circle.Ax);
			Test.AreEqual(5, circle.Ay);

			circle.SetAcceleration(8, 13);
			Test.AreEqual(8, circle.Ax);
			Test.AreEqual(13, circle.Ay);
		}

		//[TestMethod]
		//public void FollowTest1()
		//{
		//	Circle leader = new Circle(1, 1, 2);
		//	Circle sheep = new Circle(3, 5, 8);

		//	sheep.Follow(leader);
		//	Test.AreEqual(Circle.TARGET, leader.Target);
		//	Test.AreEqual(Circle.MIN_DISTANCE, leader.MinDistance);
		//	Test.AreEqual(Circle.MAX_DISTANCE, leader.MaxDistance);

		//	Test.AreEqual(leader, sheep.Target);
		//	Test.AreEqual(leader.Radius + sheep.Radius, sheep.MinDistance);
		//	Test.AreEqual(leader.Radius + sheep.Radius, sheep.MaxDistance);

		//	sheep.Follow();
		//	Test.AreEqual(null, leader.Target);
		//	Test.AreEqual(0, leader.MinDistance);
		//	Test.AreEqual(0, leader.MaxDistance);

		//	Test.AreEqual(null, sheep.Target);
		//	Test.AreEqual(0, sheep.MinDistance);
		//	Test.AreEqual(0, sheep.MaxDistance);
		//}

		//[TestMethod]
		//public void FollowTest3()
		//{
		//	Circle leader = new Circle(1, 1, 2);
		//	Circle sheep = new Circle(3, 5, 8);

		//	sheep.Follow(leader, 13, 21);
		//	Test.AreEqual(Circle.TARGET, leader.Target);
		//	Test.AreEqual(Circle.MIN_DISTANCE, leader.MinDistance);
		//	Test.AreEqual(Circle.MAX_DISTANCE, leader.MaxDistance);

		//	Test.AreEqual(leader, sheep.Target);
		//	Test.AreEqual(13, sheep.MinDistance);
		//	Test.AreEqual(21, sheep.MaxDistance);

		//	sheep.Follow(null, 34, 55);
		//	Test.AreEqual(null, leader.Target);
		//	Test.AreEqual(0, leader.MinDistance);
		//	Test.AreEqual(0, leader.MaxDistance);

		//	Test.AreEqual(null, sheep.Target);
		//	Test.AreEqual(0, sheep.MinDistance);
		//	Test.AreEqual(0, sheep.MaxDistance);
		//}

		//[TestMethod]
		//public void FaceTargetTest()
		//{
		//	Circle leader = new Circle(1, 1, 2, 3, 5);
		//	Circle sheep = new Circle(8, 13, 21, 34, 55);
		//	sheep.Follow(leader);
			
		//	sheep.FaceTarget();
		//	Test.AreEqual(new OrderedPair(3, 5), leader.Velocity);
		//	double magnitude = Math.Sqrt(34 * 34 + 55 * 55);
		//	double angle = OrderedPair.Angle(2 - 21, 1 - 13);
		//	double vx = magnitude * Math.Cos(angle);
		//	double vy = magnitude * Math.Sin(angle);
		//	Out.WriteLine(sheep.Velocity.ToString());
		//	Test.AreEqual(new OrderedPair(vx, vy), sheep.Velocity);
		//}
		[TestMethod]
		public void UpdateVelocityTest()
		{
			Circle circle = new Circle(1, 1, 2, 0, 0, 0, 0);

			double time = .5;
			for (int i = 1; i <= 10; i++)
			{
				circle.UpdateVelocity(time);
				Test.AreEqual(0, circle.Vx);
				Test.AreEqual(0, circle.Vy);
			}

			double gravity = constants.GRAVITY;
			double gravityAngle = Math.PI / 4;
			double gx = gravity * Math.Cos(gravityAngle);
			double gy = gravity * Math.Sin(gravityAngle);

			circle.SetAcceleration(gx, gy);
			for (int i = 1; i <= 10; i++)
			{
				circle.UpdateVelocity(time);
				Test.AreEqual(gx * i * time, circle.Vx);
				Test.AreEqual(gy * i * time, circle.Vy);
			}

			circle.SetAcceleration(-gx, -gy);
			for (int i = 9; i >= 0; i--)
			{
				circle.UpdateVelocity(time);
				Test.AreEqual(gx * i * time, circle.Vx);
				Test.AreEqual(gy * i * time, circle.Vy);
			}

			//Circle target = new Circle(1, 3, 5);
			//circle.Follow(target);
			//circle.SetPosition(1, 2);
			//circle.SetVelocity(0, 0);
			//circle.SetAcceleration(gx, gy);
			//circle.FaceTarget();
			//for (int i = 1; i <= 10; i++)
			//{
			//	circle.UpdateVelocity(time);
			//	double velocity = OrderedPair.Magnitude(gx * i * time, gy * i * time);
			//	double angle = OrderedPair.Angle(5 - 2, 3 - 1);
			//	double vx = velocity * Math.Cos(angle);
			//	double vy = velocity * Math.Sin(angle);
			//	Test.AreEqual(vx, circle.Vx);
			//	Test.AreEqual(vy, circle.Vy);
			//}
		}

		[TestMethod]
		public void UpdatePositionTest()
		{
			Circle circle = new Circle(2, 0, 0, 0, 0, 0, 0);

			double time = 1;
			for (int i = 1; i <= 10; i++)
			{
				circle.UpdatePosition(time);
				Test.AreEqual(0, circle.Px);
				Test.AreEqual(0, circle.Py);
			}

			double velocity = 1;
			double velocity_angle = Math.PI / 4;
			double vx = velocity * Math.Cos(velocity_angle);
			double vy = velocity * Math.Sin(velocity_angle);
			circle.SetVelocity(vx, vy);
			for (int i = 1; i <= 10; i++)
			{
				circle.UpdatePosition(time);
				Test.AreEqual(i * vx, circle.Px);
				Test.AreEqual(i * vy, circle.Py);
			}

			double gravity = constants.GRAVITY;
			double gravityAngle = Math.PI / 4;
			double gx = gravity * Math.Cos(gravityAngle);
			double gy = gravity * Math.Sin(gravityAngle);
			circle.SetAcceleration(gx, gy);
			for (int i = 1; i <= 10; i++)
			{
				circle.UpdatePosition(time);
				Test.AreEqual(gx * i * i / 2 + vx * i + 10 * vx, circle.Px);
				Test.AreEqual(gy * i * i / 2 + vy * i + 10 * vy, circle.Py);
			}
		}
	}
}