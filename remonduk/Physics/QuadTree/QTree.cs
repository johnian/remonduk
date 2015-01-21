using Remonduk;
using Remonduk.Physics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remonduk.Physics.QuadTree
{
	public class QTree
	{
        /// <summary>
        /// The default value for the maximum count of circles before splitting.
        /// </summary>
		public const int MAX_COUNT = 8;
        /// <summary>
        /// The default value for the maximum number of levels this tree will split to.
        /// </summary>
        public const int MAX_LEVEL = 6;

		/// <summary>
		/// The starting node for this Quad Tree
		/// </summary>
		public QTreeNode HeadNode;
		/// <summary>
		/// All of the nodes that make up this Quad Tree
		/// </summary>
		public HashSet<QTreeNode> Nodes;
        /// <summary>
        /// All of the circles that are contained in this quad tree.
        /// </summary>
		public HashSet<Circle> Circles;

		/// <summary>
		/// This quad tree's position and dimensions.
		/// </summary>
		OrderedPair Pos, Dim;

        /// <summary>
        /// The maximum count of circles a node can have before splitting.
        /// </summary>
		public int MaxCount;

        /// <summary>
        /// The maximum number of levels the nodes will split to.
        /// </summary>
        public int MaxLevel;

		/// <summary>
		/// Default constructor...silly values.
		/// </summary>
		public QTree()
		{
			Pos = new OrderedPair(0, 0);
            Dim = new OrderedPair(100, 100);
            MaxLevel = 6;
			HeadNode = new QTreeNode(Pos, Dim, this, "Head", 0);
			Nodes = new HashSet<QTreeNode>();
			Circles = new HashSet<Circle>();
            MaxCount = 16;

            //Nodes.Add(HeadNode); Just remember we're NOT doing this.
		}

		/// <summary>
		/// Quad Tree Constructor
		/// </summary>
		/// <param name="pos">This quad tree's position.</param>
		/// <param name="dim">This quad tree's dimensions.</param>
		/// <param name="maxCount">The max count before splitting</param>
        /// <param name="maxLevel">The max number of levels this tree will split to.</param>
        public QTree(OrderedPair pos, OrderedPair dim, int maxCount = MAX_COUNT, int maxLevel = MAX_LEVEL)
		{
			Pos = pos;
			Dim = dim;
			MaxCount = maxCount;
            HeadNode = new QTreeNode(Pos, Dim, this, "Head", 0);
            MaxLevel = maxLevel;
			Nodes = new HashSet<QTreeNode>();
            Circles = new HashSet<Circle>();

            //Nodes.Add(HeadNode); Just remember we're NOT doing this.
		}

        /// <summary>
        /// Quad Tree Constructor assuming (0,0) position.
        /// </summary>
        /// <param name="dim">This quad tree's dimensions.</param>
        /// <param name="maxCount">The max count before splitting</param>
        /// <param name="maxLevel">The max number of levels this tree will split to.</param>
		public QTree(OrderedPair dim, int maxCount = MAX_COUNT, int maxLevel = MAX_LEVEL)
		{
			Pos = new OrderedPair(0, 0);
			Dim = dim;
            MaxCount = maxCount;
            MaxLevel = maxLevel;
			HeadNode = new QTreeNode(Pos, Dim, this, "Head", 0);
			Nodes = new HashSet<QTreeNode>();
            Circles = new HashSet<Circle>();

            //Nodes.Add(HeadNode); Just remember we're NOT doing this.
		}

		/// <summary>
		/// Inserts a circle into this quad tree.
		/// </summary>
		/// <param name="c">The circle to insert into this quad tree.</param>
		/// <returns>All of the nodes this circle was inserted into.</returns>
		public List<QTreeNode> Insert(Circle c)
		{
			List<QTreeNode> Nodes = HeadNode.Insert(c);
			Circles.Add(c);
			return Nodes;
		}

        /// <summary>
        /// Removes a circle from this quad tree.
        /// </summary>
        /// <param name="c">The circles to remove from this quad tree.</param>
        /// <returns>All of the nodes that had this circle removed from them.</returns>
		public List<QTreeNode> Remove(Circle c)
		{
			Circles.Remove(c);
			return HeadNode.Remove(c);
		}

		/// <summary>
		/// Gets all of the nodes that has the given circle.
		/// </summary>
		/// <param name="c">The circle to get all nodes for.</param>
		/// <returns>A list of nodes that has the given circle.</returns>
		public List<QTreeNode> GetNodes(Circle c)
		{
			return HeadNode.GetAllNodes(c);
		}

		/// <summary>
		/// Check the QTreeNode resize comment...very costly.
		/// </summary>
		/// <param name="c">The circle that was inserted that doesn't fit</param>
		/// <returns>The new dimensions for this quad tree</returns>
		public OrderedPair Resize(Circle c)
		{
			OrderedPair NewSize = new OrderedPair(0, 0);

			return NewSize;
		}

        /// <summary>
        /// Updates a circles position after it has moved.
        /// </summary>
        /// <param name="c">The circle to reinsert</param>
        /// <returns>A list of nodes this circle was inserted into.</returns>
		public List<QTreeNode> Move(Circle c)
		{
            //Maybe check to see if we cross boundaries before removing and inserting.
			HeadNode.Remove(c);
			List<QTreeNode> nodes = Insert(c);
			return nodes;
		}

        /// <summary>
        /// Draws this quad tree.
        /// </summary>
        /// <param name="g">The graphics object to draw on.</param>
		public void Draw(Graphics g)
		{
			HeadNode.Draw(g);
		}

        /// <summary>
        /// Creates a list of all the possible circles the given circle could collide with given a certain time step.
        /// </summary>
        /// <param name="circle">The circle to check for collisions for.</param>
        /// <param name="time">The time step to use for the check.</param>
        /// <returns></returns>
		public List<Circle> Possible(Circle circle, double time)
		{
			OrderedPair start = circle.Position;
			OrderedPair end = circle.NextPosition(time);

			return HeadNode.Possible(start, end);
		}
	}
}
