using Microsoft.VisualStudio.TestTools.UnitTesting;
using remonduk.QuadTreeTest;
using Remonduk;
using Remonduk.Physics;
using Remonduk.QuadTreeTest;
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
            Test.AreEqual(tree.circles.Contains(c), true);

            Circle c2 = new Circle(5, 120, 50);
            tree.Insert(c2);
            Test.AreEqual(tree.circles.Contains(c2), true);

            //Not sure if this is actually how we want it to work...
            //See QTree and QTreeNode Resize() methods for more.
            Circle c3 = new Circle(5, 750, 400);
            tree.Insert(c3);
            Test.AreEqual(tree.circles.Contains(c3), false);
        }

        [TestMethod]
        public void InsertToQuadTreeManyTest()
        {
            QTree tree = new QTree(new OrderedPair(0, 0), new OrderedPair(600, 600), 4);
            Random rand = new Random();
            List<Circle> CirclesIn = new List<Circle>();
            for(int i = 0; i < 75; i++)
            {
                Circle c = new Circle(5, 600 * rand.NextDouble(), 600 * rand.NextDouble());
                CirclesIn.Add(c);
                tree.Insert(c);
            }

            HashSet<Circle> CirclesOut = tree.circles;

            Test.AreEqual(true, CirclesIn.Count == CirclesOut.Count);
            for(int i = 0; i < CirclesIn.Count; i++)
            {
                Test.AreEqual(true, CirclesIn.ElementAt(i) == CirclesOut.ElementAt(i));
            }
            Out.WriteLine("" + tree.nodes.Count);
        }

        [TestMethod]
        public void QuadTreeSplitTest()
        {
            QTree tree = new QTree(new OrderedPair(0, 0), new OrderedPair(600, 600), 4);
            Circle c1 = new Circle(5, 100, 100);
            Circle c2 = new Circle(5, 400, 100);
            Circle c3 = new Circle(5, 100, 400);
            Circle c4 = new Circle(5, 400, 400);
            tree.Insert(c1);
            tree.Insert(c2);
            tree.Insert(c3);
            tree.Insert(c4);

            //Splits automatically at 4, see constructor
            //tree.HeadNode.Split();

            Test.AreEqual(5, tree.nodes.Count);

            List<QTreeNode> nodes = new List<QTreeNode>();

            nodes = tree.getNodes(c1);
            Test.AreEqual(0, tree.HeadNode.NorthWest.pos.X);
            Test.AreEqual(0, tree.HeadNode.NorthWest.pos.Y);
            Test.AreEqual(300, tree.HeadNode.NorthWest.dim.X);
            Test.AreEqual(300, tree.HeadNode.NorthWest.dim.Y);
            Test.AreEqual(1, nodes.Count);
            Test.AreEqual(nodes.ElementAt(0) == tree.HeadNode.NorthWest, true);

            nodes.Clear();

            nodes = tree.getNodes(c2);
            Test.AreEqual(300, tree.HeadNode.NorthEast.pos.X);
            Test.AreEqual(0, tree.HeadNode.NorthEast.pos.Y);
            Test.AreEqual(300, tree.HeadNode.NorthEast.dim.X);
            Test.AreEqual(300, tree.HeadNode.NorthEast.dim.Y);
            Test.AreEqual(1, nodes.Count);
            Test.AreEqual(nodes.ElementAt(0) == tree.HeadNode.NorthEast, true);

            nodes.Clear();

            nodes = tree.getNodes(c3);
            Test.AreEqual(0, tree.HeadNode.SouthWest.pos.X);
            Test.AreEqual(300, tree.HeadNode.SouthWest.pos.Y);
            Test.AreEqual(300, tree.HeadNode.SouthWest.dim.X);
            Test.AreEqual(300, tree.HeadNode.SouthWest.dim.Y);
            Test.AreEqual(1, nodes.Count);
            Test.AreEqual(nodes.ElementAt(0) == tree.HeadNode.SouthWest, true);

            nodes.Clear();

            nodes = tree.getNodes(c4);
            Test.AreEqual(300, tree.HeadNode.SouthEast.pos.X);
            Test.AreEqual(300, tree.HeadNode.SouthEast.pos.Y);
            Test.AreEqual(300, tree.HeadNode.SouthEast.dim.X);
            Test.AreEqual(300, tree.HeadNode.SouthEast.dim.Y);
            Test.AreEqual(1, nodes.Count);
            Test.AreEqual(nodes.ElementAt(0) == tree.HeadNode.SouthEast, true);
        }

        [TestMethod]
        public void QuadTreeSplitTestDeep()
        {
            QTree tree = new QTree(new OrderedPair(0, 0), new OrderedPair(600, 600), 4);
            Circle c1 = new Circle(1, 100, 100);
            Circle c2 = new Circle(1, 400, 100);
            Circle c3 = new Circle(1, 100, 400);
            Circle c4 = new Circle(1, 400, 400);
            tree.Insert(c1);
            tree.Insert(c2);
            tree.Insert(c3);
            tree.Insert(c4);

            Test.AreEqual(5, tree.nodes.Count);

            List<QTreeNode> nodes = new List<QTreeNode>();

            Circle c5 = new Circle(1, 147, 148);
            tree.Insert(c5);
            Circle c6 = new Circle(1, 75, 90);
            tree.Insert(c6);
            Circle c7 = new Circle(1, 100, 125);
            tree.Insert(c7);
            Circle c8 = new Circle(1, 5, 5);
            tree.Insert(c8);

            Test.AreEqual(9, tree.nodes.Count);

            nodes = tree.getNodes(c5);
            Test.AreEqual(1, nodes.Count);
            Test.AreEqual(false, nodes.Contains(tree.HeadNode.NorthWest));
            Test.AreEqual(true, nodes.Contains(tree.HeadNode.NorthWest.NorthWest));



        }

        [TestMethod]
        public void QuadTreeNodeEdgeTest()
        {
            QTree tree = new QTree(new OrderedPair(0, 0), new OrderedPair(600, 600), 2);
            Circle c1 = new Circle(5, 300, 300);
            Circle c2 = new Circle(5, 300, 100);
            tree.Insert(c1);
            tree.Insert(c2);

            List<QTreeNode> nodes = new List<QTreeNode>();

            nodes = tree.getNodes(c1);
            Test.AreEqual(4, nodes.Count);
            Test.AreEqual(true, nodes.Contains(tree.HeadNode.NorthWest));
            Test.AreEqual(true, nodes.Contains(tree.HeadNode.NorthEast));
            Test.AreEqual(true, nodes.Contains(tree.HeadNode.SouthWest));
            Test.AreEqual(true, nodes.Contains(tree.HeadNode.SouthEast));

            nodes.Clear();
            nodes = tree.getNodes(c2);
            Test.AreEqual(2, nodes.Count);
            Test.AreEqual(true, nodes.Contains(tree.HeadNode.NorthWest));
            Test.AreEqual(true, nodes.Contains(tree.HeadNode.NorthEast));
        }



    }
}
