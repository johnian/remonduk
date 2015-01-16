using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remonduk;
using Remonduk.Physics;
using Remonduk.Physics.QuadTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite
{
    [TestClass]
    public class QuadTreeTest
    {
        [TestMethod]
        public void InsertToQuadTreeTest()
        {
            QTree tree;
            tree = new QTree(new OrderedPair(0, 0), new OrderedPair(600, 600), 16);
            Circle c = new Circle(5, 25, 25);
            tree.Insert(c);
            Test.AreEqual(tree.Circles.Contains(c), true);

            Circle c2 = new Circle(5, 120, 50);
            tree.Insert(c2);
            Test.AreEqual(tree.Circles.Contains(c2), true);

            //Not sure if this is actually how we want it to work...
            //See QTree and QTreeNode Resize() methods for more.
            Circle c3 = new Circle(5, 750, 400);
            List<QTreeNode> nodes = tree.Insert(c3);
            Test.AreEqual(0, nodes.Count);
            Test.AreEqual(true, tree.Circles.Contains(c3));
        }

        [TestMethod]
        public void InsertToQuadTreeManyTest()
        {
            QTree tree = new QTree(new OrderedPair(0, 0), new OrderedPair(600, 600), 4);
            Random rand = new Random();
            List<Circle> CirclesIn = new List<Circle>();
            for(int i = 0; i < 15; i++)
            {
                Circle c = new Circle(5, 600 * rand.NextDouble(), 600 * rand.NextDouble());
                CirclesIn.Add(c);
                tree.Insert(c);
            }

            HashSet<Circle> CirclesOut = tree.Circles;

            Test.AreEqual(CirclesIn.Count, CirclesOut.Count);
            for(int i = 0; i < CirclesIn.Count; i++)
            {
                Test.AreEqual(true, CirclesIn.ElementAt(i) == CirclesOut.ElementAt(i));
            }
        }

        [TestMethod]
        public void QuadTreeSplitTest()
        {
            QTree tree = new QTree(new OrderedPair(0, 0), new OrderedPair(100, 100), 4);
            Circle c1 = new Circle(5, 10, 10);
            Circle c2 = new Circle(5, 90, 10);
            Circle c3 = new Circle(5, 10, 90);
            Circle c4 = new Circle(5, 90, 90);

            tree.Insert(c1);
            Test.AreEqual(1, tree.Circles.Count);
            Test.AreEqual(0, tree.Nodes.Count);
            Test.AreEqual(false, tree.HeadNode.split);

            tree.Insert(c2);
            Test.AreEqual(2, tree.Circles.Count);
            Test.AreEqual(0, tree.Nodes.Count);
            Test.AreEqual(false, tree.HeadNode.split);

            tree.Insert(c3);
            Test.AreEqual(3, tree.Circles.Count);
            Test.AreEqual(0, tree.Nodes.Count);
            Test.AreEqual(false, tree.HeadNode.split);

            tree.Insert(c4);
            Test.AreEqual(4, tree.Circles.Count);
            Test.AreEqual(4, tree.Nodes.Count);
            Test.AreEqual(true, tree.HeadNode.split);

            List<QTreeNode> nodes = new List<QTreeNode>();

            nodes = tree.GetNodes(c1);
            Test.AreEqual(0, tree.HeadNode.NorthWest.pos.X);
            Test.AreEqual(0, tree.HeadNode.NorthWest.pos.Y);
            Test.AreEqual(50, tree.HeadNode.NorthWest.dim.X);
            Test.AreEqual(50, tree.HeadNode.NorthWest.dim.Y);
            Test.AreEqual(1, nodes.Count);
            Out.WriteLine(nodes.ElementAt(0).ToString());
            Test.AreEqual(true, nodes.ElementAt(0).name.Equals(tree.HeadNode.NorthWest.name));

            nodes.Clear();

            nodes = tree.GetNodes(c2);
            Test.AreEqual(50, tree.HeadNode.NorthEast.pos.X);
            Test.AreEqual(0, tree.HeadNode.NorthEast.pos.Y);
            Test.AreEqual(50, tree.HeadNode.NorthEast.dim.X);
            Test.AreEqual(50, tree.HeadNode.NorthEast.dim.Y);
            Test.AreEqual(1, nodes.Count);
            Out.WriteLine(nodes.ElementAt(0).ToString());
            Test.AreEqual(true, nodes.ElementAt(0).name.Equals(tree.HeadNode.NorthEast.name));

            nodes.Clear();

            nodes = tree.GetNodes(c3);
            Test.AreEqual(0, tree.HeadNode.SouthWest.pos.X);
            Test.AreEqual(50, tree.HeadNode.SouthWest.pos.Y);
            Test.AreEqual(50, tree.HeadNode.SouthWest.dim.X);
            Test.AreEqual(50, tree.HeadNode.SouthWest.dim.Y);
            Test.AreEqual(1, nodes.Count);
            Out.WriteLine(nodes.ElementAt(0).ToString());
            Test.AreEqual(true, nodes.ElementAt(0).name.Equals(tree.HeadNode.SouthWest.name));

            nodes.Clear();

            nodes = tree.GetNodes(c4);
            Test.AreEqual(50, tree.HeadNode.SouthEast.pos.X);
            Test.AreEqual(50, tree.HeadNode.SouthEast.pos.Y);
            Test.AreEqual(50, tree.HeadNode.SouthEast.dim.X);
            Test.AreEqual(50, tree.HeadNode.SouthEast.dim.Y);
            Test.AreEqual(1, nodes.Count);
            Out.WriteLine(nodes.ElementAt(0).ToString());
            Test.AreEqual(true, nodes.ElementAt(0).name.Equals(tree.HeadNode.SouthEast.name));

        }

        [TestMethod]
        public void QuadTreeSplitTestDeep()
        {
            QTree tree = new QTree(new OrderedPair(0, 0), new OrderedPair(100, 100), 4);
            Circle c1 = new Circle(1, 10, 10);
            Circle c2 = new Circle(1, 75, 10);
            Circle c3 = new Circle(1, 10, 75);
            Circle c4 = new Circle(1, 75, 75);
            Circle c5 = new Circle(1, 5, 5);
            Circle c6 = new Circle(1, 30, 30);
            Circle c7 = new Circle(1, 25, 25);
            
            tree.Insert(c1);
            tree.Insert(c2);
            tree.Insert(c3);
            tree.Insert(c4);
            Test.AreEqual(4, tree.Nodes.Count);
            Test.AreEqual(true, tree.HeadNode.split);

            tree.Insert(c5);
            tree.Insert(c6);
            List<QTreeNode> node7 = tree.Insert(c7);
            Test.AreEqual(8, tree.Nodes.Count);
            Test.AreEqual(true, tree.HeadNode.NorthWest.split);

            List<QTreeNode> nodes = new List<QTreeNode>();
            nodes = tree.GetNodes(c5);
            Test.AreEqual(1, nodes.Count);
            Test.AreEqual(true, nodes.Contains(tree.HeadNode.NorthWest.NorthWest));

            Test.AreEqual(4, node7.Count);
            node7 = tree.GetNodes(c7);
            Test.AreEqual(4, node7.Count);
            Test.AreEqual(true, node7.Contains(tree.HeadNode.NorthWest.NorthWest));
            Test.AreEqual(true, node7.Contains(tree.HeadNode.NorthWest.NorthEast));
            Test.AreEqual(true, node7.Contains(tree.HeadNode.NorthWest.SouthWest));
            Test.AreEqual(true, node7.Contains(tree.HeadNode.NorthWest.SouthEast));
        }

        [TestMethod]
        public void QuadTreeNodeEdgeTest()
        {
            QTree tree = new QTree(new OrderedPair(0, 0), new OrderedPair(600, 600), 3);
            Circle c1 = new Circle(5, 300, 300);
            Circle c2 = new Circle(5, 350, 500);
            Circle c3 = new Circle(5, 10, 10);

            List<QTreeNode> nodes1 = tree.Insert(c1);
            List<QTreeNode> nodes2 = tree.Insert(c2);
            Test.AreEqual(false, tree.HeadNode.split);

            List<QTreeNode> nodes3 = tree.Insert(c3);
            Test.AreEqual(true, tree.HeadNode.split);

            nodes1 = tree.GetNodes(c1);
            Test.AreEqual(4, nodes1.Count);
            Out.WriteLine("Checking Northwest");
            Out.WriteLine(nodes1.ElementAt(0).name);
            Test.AreEqual(true, nodes1.Contains(tree.HeadNode.NorthWest));
            Out.WriteLine("Checking Northeast");
            Out.WriteLine(nodes1.ElementAt(1).name);
            Test.AreEqual(true, nodes1.Contains(tree.HeadNode.NorthEast));
            Out.WriteLine("Checking Southwest");
            Out.WriteLine(nodes1.ElementAt(2).name);
            Test.AreEqual(true, nodes1.Contains(tree.HeadNode.SouthWest));
            Out.WriteLine("Checking Southeast");
            Out.WriteLine(nodes1.ElementAt(3).name);
            Test.AreEqual(true, nodes1.Contains(tree.HeadNode.SouthEast));
        }

        [TestMethod]
        public void QuadTreeRemoveTest()
        {
            QTree tree = new QTree(new OrderedPair(0, 0), new OrderedPair(100, 100), 4);
            Circle c1 = new Circle(1, 10, 10);
            tree.Insert(c1);
            List<QTreeNode> nodes = tree.Remove(c1);
            Test.AreEqual(1, nodes.Count);
            Test.AreEqual(0, tree.Circles.Count);
            Test.AreEqual(false, tree.HeadNode.HasA(c1));


            Circle c2 = new Circle(1, 75, 10);
            Circle c3 = new Circle(1, 10, 75);
            Circle c4 = new Circle(1, 75, 75);
            tree.Insert(c1);
            tree.Insert(c2);
            tree.Insert(c3);
            tree.Insert(c4);

            Test.AreEqual(true, tree.HeadNode.split);

            tree.Remove(c2);
            Test.AreEqual(false, tree.Circles.Contains(c2));
            Test.AreEqual(0, tree.GetNodes(c2).Count);
            Test.AreEqual(false, tree.HeadNode.split);
        }

        [TestMethod]
        public void QuadTreeMoveTest()
        {
            QTree tree = new QTree(new OrderedPair(0, 0), new OrderedPair(100, 100), 2);
            Circle c1 = new Circle(1, 10, 10);
            Circle c2 = new Circle(1, 75, 10);
            tree.Insert(c1);
            tree.Insert(c2);
            Test.AreEqual(true, tree.HeadNode.NorthWest.Contains(c1));

            c1.SetPosition(75, 75);
            tree.Move(c1);
            Test.AreEqual(true, tree.HeadNode.SouthEast.Contains(c1));
            Test.AreEqual(false, tree.HeadNode.NorthWest.Contains(c1));
        }

        [TestMethod]
        public void QuadTreePossibleTest()
        {
            QTree tree = new QTree(new OrderedPair(0, 0), new OrderedPair(100, 100), 2);
            Circle c1 = new Circle(1, 10, 10);
            Circle c2 = new Circle(1, 90, 10);
            Circle c3 = new Circle(1, 10, 90);
            c1.SetVelocity(50, 0);
            tree.Insert(c1);
            tree.Insert(c2);
            tree.Insert(c3);

            List<Circle> circles = tree.Possible(c1, 1.0);
            Test.AreEqual(true, circles.Contains(c2));
            Test.AreEqual(false, circles.Contains(c3));
        }

    }
}
