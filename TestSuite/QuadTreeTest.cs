using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remonduk;
using Remonduk.Physics;
using Remonduk.Physics.CollisionPruning;

namespace TestSuite
{
	[TestClass]
	public class QuadTreeTest
	{
		[TestMethod]
		public void QuadTreeTest3()
		{

		}

		[TestMethod]
		public void InsertTest()
		{
			QuadTree tree = new QuadTree(new PhysicalSystem());
			List<QuadTreeNode> nodes;

			Circle c1 = new Circle(5, 25, 25);
			Circle c2 = new Circle(5, 25, 25);
			Circle third = new Circle(5, 750, 400);
			nodes = tree.Insert(c1);
			Test.AreEqual(true, tree.Circles.Contains(c1));
			Test.AreEqual(1, tree.Circles.Count());
			//Test.AreEqual(1, nodes.Count); // check this in node tests

			nodes = tree.Insert(c2);
			Test.AreEqual(true, tree.Circles.Contains(c2));
			Test.AreEqual(2, tree.Circles.Count());
			//Test.AreEqual(1, nodes.Count);
			
			nodes = tree.Insert(third);
			Test.AreEqual(true, tree.Circles.Contains(third));
			Test.AreEqual(3, tree.Circles.Count());
			//Test.AreEqual(1, nodes.Count);

			////

			tree = new QuadTree(new PhysicalSystem());
			Random rand = new Random();
			List<Circle> CirclesIn = new List<Circle>();
			for (int i = 0; i < 100; i++)
			{
				Circle c = new Circle(5, tree.Dimensions.X * rand.NextDouble(), tree.Dimensions.Y * rand.NextDouble());
				CirclesIn.Add(c);
				tree.Insert(c);
			}

			HashSet<Circle> CirclesOut = tree.Circles;

			Test.AreEqual(CirclesIn.Count, CirclesOut.Count);
			for (int i = 0; i < CirclesIn.Count; i++)
			{
				Test.AreEqual(true, CirclesIn.ElementAt(i) == CirclesOut.ElementAt(i));
			}

			// add a circle that sits on a boundary
		}

		[TestMethod]
		public void RemoveTest()
		{

		}

		[TestMethod]
		public void GetNodesTest()
		{

		}

		[TestMethod]
		public void MoveTest()
		{

		}

		[TestMethod]
		public void PossibleTest()
		{
			PhysicalSystem physicalSystem = new PhysicalSystem();
			QuadTree quadTree = new QuadTree(physicalSystem, 1);
			List<Circle> circles = new List<Circle>();

			Random random = new Random();
			for (int i = 0; i < 100; i++) {
				circles.Add(
					new Circle(
						random.Next(1, 5),
						random.Next(0, (int)physicalSystem.Dimensions.X),
						random.Next(0, (int)physicalSystem.Dimensions.Y),
						random.Next(0, (int)physicalSystem.MaxSpeed),
						random.Next(0, (int)physicalSystem.MaxSpeed)
					)
				);
			}
			List<Circle> possibleCollisions = quadTree.Possible(new Circle(), 1);
			String line = "";
			foreach(Circle c in possibleCollisions) {
				line += c.ToString() + ", ";
			}
			Test.AreEqual("things", line);
			Test.AreEqual(quadTree.MaxCount, possibleCollisions.Count());

			//quadTree = new QuadTree(new PhysicalSystem());
			//for (int i = 0; i <= quadTree.MaxCount; i++) {
			//	Circle circle = new Circle();
			//	quadTree.Insert(circle);
			//}
			//possibleCollisions = quadTree.Possible(new Circle(), 1);
			//Test.AreEqual(quadTree.MaxCount + 1, possibleCollisions.Count());

		}

		//[TestMethod]
		//public void InsertToQuadTreeManyTest()
		//{
			//Tree tree = new Tree(new OrderedPair(0, 0), new OrderedPair(600, 600), 4);
			//Random rand = new Random();
			//List<Circle> CirclesIn = new List<Circle>();
			//for (int i = 0; i < 15; i++)
			//{
			//	Circle c = new Circle(5, 600 * rand.NextDouble(), 600 * rand.NextDouble());
			//	CirclesIn.Add(c);
			//	tree.Insert(c);
			//}

			//HashSet<Circle> CirclesOut = tree.Circles;

			//Test.AreEqual(CirclesIn.Count, CirclesOut.Count);
			//for (int i = 0; i < CirclesIn.Count; i++)
			//{
			//	Test.AreEqual(true, CirclesIn.ElementAt(i) == CirclesOut.ElementAt(i));
			//}
		//}

		//[TestMethod]
		//public void QuadTreeSplitTest()
		//{
		//	Tree tree = new Tree(new OrderedPair(0, 0), new OrderedPair(100, 100), 4);
		//	Circle c1 = new Circle(5, 10, 10);
		//	Circle c2 = new Circle(5, 90, 10);
		//	Circle c3 = new Circle(5, 10, 90);
		//	Circle c4 = new Circle(5, 90, 90);

		//	tree.Insert(c1);
		//	Test.AreEqual(1, tree.Circles.Count);
		//	Test.AreEqual(0, tree.Nodes.Count);
		//	Test.AreEqual(false, tree.HeadNode.Split);

		//	tree.Insert(c2);
		//	Test.AreEqual(2, tree.Circles.Count);
		//	Test.AreEqual(0, tree.Nodes.Count);
		//	Test.AreEqual(false, tree.HeadNode.Split);

		//	tree.Insert(c3);
		//	Test.AreEqual(3, tree.Circles.Count);
		//	Test.AreEqual(0, tree.Nodes.Count);
		//	Test.AreEqual(false, tree.HeadNode.Split);

		//	tree.Insert(c4);
		//	Test.AreEqual(4, tree.Circles.Count);
		//	Test.AreEqual(4, tree.Nodes.Count);
		//	Test.AreEqual(true, tree.HeadNode.Split);

		//	List<Node> nodes = new List<Node>();

		//	nodes = tree.GetNodes(c1);
		//	Test.AreEqual(0, tree.HeadNode.NorthWest.Pos.X);
		//	Test.AreEqual(0, tree.HeadNode.NorthWest.Pos.Y);
		//	Test.AreEqual(50, tree.HeadNode.NorthWest.Dim.X);
		//	Test.AreEqual(50, tree.HeadNode.NorthWest.Dim.Y);
		//	Test.AreEqual(1, nodes.Count);
		//	Out.WriteLine(nodes.ElementAt(0).ToString());
		//	Test.AreEqual(true, nodes.ElementAt(0).Name.Equals(tree.HeadNode.NorthWest.Name));

		//	nodes.Clear();

		//	nodes = tree.GetNodes(c2);
		//	Test.AreEqual(50, tree.HeadNode.NorthEast.Pos.X);
		//	Test.AreEqual(0, tree.HeadNode.NorthEast.Pos.Y);
		//	Test.AreEqual(50, tree.HeadNode.NorthEast.Dim.X);
		//	Test.AreEqual(50, tree.HeadNode.NorthEast.Dim.Y);
		//	Test.AreEqual(1, nodes.Count);
		//	Out.WriteLine(nodes.ElementAt(0).ToString());
		//	Test.AreEqual(true, nodes.ElementAt(0).Name.Equals(tree.HeadNode.NorthEast.Name));

		//	nodes.Clear();

		//	nodes = tree.GetNodes(c3);
		//	Test.AreEqual(0, tree.HeadNode.SouthWest.Pos.X);
		//	Test.AreEqual(50, tree.HeadNode.SouthWest.Pos.Y);
		//	Test.AreEqual(50, tree.HeadNode.SouthWest.Dim.X);
		//	Test.AreEqual(50, tree.HeadNode.SouthWest.Dim.Y);
		//	Test.AreEqual(1, nodes.Count);
		//	Out.WriteLine(nodes.ElementAt(0).ToString());
		//	Test.AreEqual(true, nodes.ElementAt(0).Name.Equals(tree.HeadNode.SouthWest.Name));

		//	nodes.Clear();

		//	nodes = tree.GetNodes(c4);
		//	Test.AreEqual(50, tree.HeadNode.SouthEast.Pos.X);
		//	Test.AreEqual(50, tree.HeadNode.SouthEast.Pos.Y);
		//	Test.AreEqual(50, tree.HeadNode.SouthEast.Dim.X);
		//	Test.AreEqual(50, tree.HeadNode.SouthEast.Dim.Y);
		//	Test.AreEqual(1, nodes.Count);
		//	Out.WriteLine(nodes.ElementAt(0).ToString());
		//	Test.AreEqual(true, nodes.ElementAt(0).Name.Equals(tree.HeadNode.SouthEast.Name));

		//}

		//[TestMethod]
		//public void QuadTreeSplitTestDeep()
		//{
		//	Tree tree = new Tree(new OrderedPair(0, 0), new OrderedPair(100, 100), 4);
		//	Circle c1 = new Circle(1, 10, 10);
		//	Circle c2 = new Circle(1, 75, 10);
		//	Circle c3 = new Circle(1, 10, 75);
		//	Circle c4 = new Circle(1, 75, 75);
		//	Circle c5 = new Circle(1, 5, 5);
		//	Circle c6 = new Circle(1, 30, 30);
		//	Circle c7 = new Circle(1, 25, 25);

		//	tree.Insert(c1);
		//	tree.Insert(c2);
		//	tree.Insert(c3);
		//	tree.Insert(c4);
		//	Test.AreEqual(4, tree.Nodes.Count);
		//	Test.AreEqual(true, tree.HeadNode.Split);

		//	tree.Insert(c5);
		//	tree.Insert(c6);
		//	List<Node> node7 = tree.Insert(c7);
		//	Test.AreEqual(8, tree.Nodes.Count);
		//	Test.AreEqual(true, tree.HeadNode.NorthWest.Split);

		//	List<Node> nodes = new List<Node>();
		//	nodes = tree.GetNodes(c5);
		//	Test.AreEqual(1, nodes.Count);
		//	Test.AreEqual(true, nodes.Contains(tree.HeadNode.NorthWest.NorthWest));

		//	Test.AreEqual(4, node7.Count);
		//	node7 = tree.GetNodes(c7);
		//	Test.AreEqual(4, node7.Count);
		//	Test.AreEqual(true, node7.Contains(tree.HeadNode.NorthWest.NorthWest));
		//	Test.AreEqual(true, node7.Contains(tree.HeadNode.NorthWest.NorthEast));
		//	Test.AreEqual(true, node7.Contains(tree.HeadNode.NorthWest.SouthWest));
		//	Test.AreEqual(true, node7.Contains(tree.HeadNode.NorthWest.SouthEast));
		//}

		//[TestMethod]
		//public void QuadTreeNodeEdgeTest()
		//{
		//	Tree tree = new Tree(new OrderedPair(0, 0), new OrderedPair(600, 600), 3);
		//	Circle c1 = new Circle(5, 300, 300);
		//	Circle c2 = new Circle(5, 350, 500);
		//	Circle c3 = new Circle(5, 10, 10);

		//	List<Node> nodes1 = tree.Insert(c1);
		//	List<Node> nodes2 = tree.Insert(c2);
		//	Test.AreEqual(false, tree.HeadNode.Split);

		//	List<Node> nodes3 = tree.Insert(c3);
		//	Test.AreEqual(true, tree.HeadNode.Split);

		//	nodes1 = tree.GetNodes(c1);
		//	Test.AreEqual(4, nodes1.Count);
		//	Out.WriteLine("Checking Northwest");
		//	Out.WriteLine(nodes1.ElementAt(0).Name);
		//	Test.AreEqual(true, nodes1.Contains(tree.HeadNode.NorthWest));
		//	Out.WriteLine("Checking Northeast");
		//	Out.WriteLine(nodes1.ElementAt(1).Name);
		//	Test.AreEqual(true, nodes1.Contains(tree.HeadNode.NorthEast));
		//	Out.WriteLine("Checking Southwest");
		//	Out.WriteLine(nodes1.ElementAt(2).Name);
		//	Test.AreEqual(true, nodes1.Contains(tree.HeadNode.SouthWest));
		//	Out.WriteLine("Checking Southeast");
		//	Out.WriteLine(nodes1.ElementAt(3).Name);
		//	Test.AreEqual(true, nodes1.Contains(tree.HeadNode.SouthEast));
		//}

		//[TestMethod]
		//public void QuadTreeRemoveTest()
		//{
		//	Tree tree = new Tree(new OrderedPair(0, 0), new OrderedPair(100, 100), 4);
		//	Circle c1 = new Circle(1, 10, 10);
		//	tree.Insert(c1);
		//	List<Node> nodes = tree.Remove(c1);
		//	Test.AreEqual(1, nodes.Count);
		//	Test.AreEqual(0, tree.Circles.Count);
		//	Test.AreEqual(false, tree.HeadNode.HasA(c1));


		//	Circle c2 = new Circle(1, 75, 10);
		//	Circle c3 = new Circle(1, 10, 75);
		//	Circle c4 = new Circle(1, 75, 75);
		//	tree.Insert(c1);
		//	tree.Insert(c2);
		//	tree.Insert(c3);
		//	tree.Insert(c4);

		//	Test.AreEqual(true, tree.HeadNode.Split);

		//	tree.Remove(c2);
		//	Test.AreEqual(false, tree.Circles.Contains(c2));
		//	Test.AreEqual(0, tree.GetNodes(c2).Count);
		//	Test.AreEqual(false, tree.HeadNode.Split);
		//}

		//[TestMethod]
		//public void QuadTreeMoveTest()
		//{
		//	Tree tree = new Tree(new OrderedPair(0, 0), new OrderedPair(100, 100), 2);
		//	Circle c1 = new Circle(1, 10, 10);
		//	Circle c2 = new Circle(1, 75, 10);
		//	tree.Insert(c1);
		//	tree.Insert(c2);
		//	Test.AreEqual(true, tree.HeadNode.NorthWest.Contains(c1));

		//	c1.SetPosition(75, 75);
		//	tree.Move(c1);
		//	Test.AreEqual(true, tree.HeadNode.SouthEast.Contains(c1));
		//	Test.AreEqual(false, tree.HeadNode.NorthWest.Contains(c1));
		//}

		//[TestMethod]
		//public void QuadTreePossibleTest()
		//{
		//	Tree tree = new Tree(new OrderedPair(0, 0), new OrderedPair(100, 100), 2);
		//	Circle c1 = new Circle(1, 10, 10);
		//	Circle c2 = new Circle(1, 90, 10);
		//	Circle c3 = new Circle(1, 10, 90);
		//	c1.SetVelocity(50, 0);
		//	tree.Insert(c1);
		//	tree.Insert(c2);
		//	tree.Insert(c3);

		//	List<Circle> circles = tree.Possible(c1, 1.0);
		//	Test.AreEqual(true, circles.Contains(c2));
		//	Test.AreEqual(false, circles.Contains(c3));
		//}

	}
}
