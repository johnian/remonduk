using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            QuadTree<Circle> tree;
            tree = new QuadTree<Circle>(new FRect(new Tuple<double, double>(0, 0), new Tuple<double, double>(600, 600)), 16);
            Circle c = new Circle(5, 25, 25);
            tree.Insert(c);
            List<Circle> circles = new List<Circle>();
            tree.GetAllItems(ref circles);
            Test.AreEqual(circles.Contains(c), true);
        }

        [TestMethod]
        public void InsertToQuadTreeManyTest()
        {
            QuadTree<Circle> tree;
            tree = new QuadTree<Circle>(new FRect(new Tuple<double, double>(0, 0), new Tuple<double, double>(600, 600)), 4);
            Random rand = new Random();
            List<Circle> CirclesIn = new List<Circle>();
            for(int i = 0; i < 30; i++)
            {
                Circle c = new Circle(5, 600 * rand.NextDouble(), 600 * rand.NextDouble());
                CirclesIn.Add(c);
                tree.Insert(c);
            }
            
            List<Circle> CirclesOut = new List<Circle>();

            tree.GetAllItems(ref CirclesOut);

            Test.AreEqual(true, CirclesIn.Count == CirclesOut.Count);
            for(int i = 0; i < CirclesIn.Count; i++)
            {
                Test.AreEqual(true, CirclesIn.ElementAt(i) == CirclesOut.ElementAt(i));
            }
        }

        [TestMethod]
        public void QuadTreeResizeTest()
        {
            QuadTree<Circle> tree;
            tree = new QuadTree<Circle>(new FRect(new Tuple<double, double>(0, 0), new Tuple<double, double>(600, 600)), 4);

            Circle c = new Circle(5, 650, 650);
            tree.Insert(c);
            List<Circle> circles = new List<Circle>();
            tree.GetAllItems(ref circles);

            Test.AreEqual(circles.Contains(c), true);
            Test.AreEqual(0, tree.WorldRect.Left);
            Test.AreEqual(0, tree.WorldRect.Top);
            Test.AreEqual(1310, tree.WorldRect.Right);
            Test.AreEqual(1310, tree.WorldRect.Bottom);

            c = new Circle(5, -25, -25);
            tree.Insert(c);
            circles = new List<Circle>();
            tree.GetAllItems(ref circles);
            Test.AreEqual(circles.Contains(c), true);
            Test.AreEqual(-30, tree.WorldRect.Left);
            Test.AreEqual(-30, tree.WorldRect.Top);
            Test.AreEqual(2620, tree.WorldRect.Right);
            Test.AreEqual(2620, tree.WorldRect.Bottom);

            //NOT SURE THESE ARE HANDLED THE WAY THEY SHOULD BE...
            //ALL CORNERS INCREASE REGARDLESS OF WHAT SIDE SHOULD BE EXPANDED
            //WELL...BOTTOM RIGHT IS DOUBLED, TOP LEFT IS SET TO MINIMUM
            //SOMETHING TO THINK ABOUT
        }



    }
}
