using Remonduk.Physics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remonduk.QuadTreeTest
{
	public class QTree
	{
		public const int MAX_COUNT = 16;
		/// <summary>
		/// The starting node for this Quad Tree
		/// </summary>
		public QTreeNode HeadNode;
		/// <summary>
		/// All of the nodes that make up this Quad Tree
		/// </summary>
		public HashSet<QTreeNode> Nodes;

		public HashSet<Circle> Circles;
		/// <summary>
		/// This quad tree's position and dimensions.
		/// </summary>
		OrderedPair Pos, Dim;

		public int MaxCount;

		/// <summary>
		/// Default constructor...silly values.
		/// </summary>
		public QTree()
		{
			Pos = new OrderedPair(0, 0);
			Dim = new OrderedPair(100, 100);
			HeadNode = new QTreeNode(Pos, Dim, this, "Head");
			Nodes = new HashSet<QTreeNode>();
			//Nodes.Add(HeadNode);
			Circles = new HashSet<Circle>();
			MaxCount = 16;
		}

		/// <summary>
		/// Quad Tree Constructor
		/// </summary>
		/// <param name="Pos">This quad tree's position.</param>
		/// <param name="Dim">This quad tree's dimensions.</param>
		/// <param name="MaxCount">The max count before splitting</param>
		public QTree(OrderedPair pos, OrderedPair dim, int maxCount = MAX_COUNT)
		{
			Pos = pos;
			Dim = dim;
			MaxCount = maxCount;
			HeadNode = new QTreeNode(Pos, Dim, this, "Head");
			Nodes = new HashSet<QTreeNode>();
			//Nodes.Add(HeadNode);
			Circles = new HashSet<Circle>();
		}

		public QTree(OrderedPair dim, int maxCount = MAX_COUNT)
		{
			Pos = new OrderedPair(0, 0);
			Dim = dim;
			MaxCount = maxCount;
			HeadNode = new QTreeNode(Pos, Dim, this, "Head");
			Nodes = new HashSet<QTreeNode>();
			//Nodes.Add(HeadNode);
			Circles = new HashSet<Circle>();
		}

		/// <summary>
		/// Inserts a circle into this quad tree.
		/// </summary>
		/// <param name="c">The circle to insert into this quad tree.</param>
		/// <returns></returns>
		public List<QTreeNode> Insert(Circle c)
		{
			List<QTreeNode> Nodes = HeadNode.Insert(c);
			Circles.Add(c);
			return Nodes;
		}

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

		public List<QTreeNode> Move(Circle c)
		{
			HeadNode.Remove(c);
			List<QTreeNode> nodes = Insert(c);
			return nodes;
		}

		public void Draw(Graphics g)
		{
			HeadNode.draw(g);
		}

		public List<Circle> Possible(Circle circle, double time)
		{
			OrderedPair start = circle.Position;
			OrderedPair end = circle.NextPosition(time);

			return HeadNode.Possible(start, end);
		}
	}
}
