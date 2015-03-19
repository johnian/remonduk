using System;
using System.Collections.Generic;
using System.Drawing;

namespace Remonduk.Physics.CollisionPruning
{
	public class QuadTreeNode
	{
		public const int NODE_COUNT = 4;

		/// <summary>
		/// The QuadTree this node belongs to.
		/// </summary>
		public QuadTree Tree;
		/// <summary>
		/// The depth of this node - how many splits it represents.
		/// </summary>
		public int Depth;
		/// <summary>
		/// The position of the node.
		/// </summary>
		public OrderedPair Position;
		/// <summary>
		/// The dimensions of the node.
		/// </summary>
		public OrderedPair Dimensions;

		/// <summary>
		/// The list of the nodes.
		/// </summary>
		public QuadTreeNode[] Nodes;
		public QuadTreeNode NW
		{
			set { Nodes[0] = value; }
			get { return Nodes[0]; }
		}
		public QuadTreeNode NE
		{
			set { Nodes[1] = value; }
			get { return Nodes[1]; }
		}
		public QuadTreeNode SW
		{
			set { Nodes[2] = value; }
			get { return Nodes[2]; }
		}
		public QuadTreeNode SE
		{
			set { Nodes[3] = value; }
			get { return Nodes[3]; }
		}

		/// <summary>
		/// The list of circles in this node.
		/// </summary>
		public HashSet<Circle> Circles;

		/// <summary>
		/// The quadrants of this node.  Are null until this node has been split.
		/// </summary>
		//public QTreeNode NorthWest, NorthEast, SouthWest, SouthEast;

		/// <summary>
		/// This quad tree's name - Head.NorthWest.SouthEast
		/// </summary>
		//public String Name;

		/// <summary>
		/// The maximum count of circles this node can have before splitting.
		/// </summary>
		public int MaxCount;

		/// <summary>
		/// QTreeNode Constructor.
		/// </summary>
		/// <param name="position">The position for this node (top left)</param>
		/// <param name="dimensions">The dimensions for this node (width, height)</param>
		/// <param name="tree">The Quad Tree this node belongs to.</param>
		/// <param name="name">The name for this quad tree.</param>
		/// <param name="depth">The depth of this quad tree.</param>
		public QuadTreeNode(QuadTree tree, int depth, OrderedPair position, OrderedPair dimensions)
		{
			Tree = tree;
			Depth = depth;
			Position = position;
			Dimensions = dimensions;

			Nodes = new QuadTreeNode[NODE_COUNT];
			Circles = new HashSet<Circle>();

			MaxCount = Tree.MaxCount;
			//Name = name;
		}

		/// <summary>
		/// Splits this node into 4 quadrants.  Called when the number of circles is greater than limit.
		/// </summary>
		public void SplitNode()
		{
			if (NotSplit())
			{
				//Can probably do this a little cleaner
				OrderedPair NewDim = new OrderedPair(Dimensions.X / 2.0, Dimensions.Y / 2.0);

				NW = new QuadTreeNode(Tree, Depth + 1, Position, NewDim);
				NE = new QuadTreeNode(Tree, Depth + 1,
					new OrderedPair(Position.X + NewDim.X, Position.Y), NewDim);
				SW = new QuadTreeNode(Tree, Depth + 1,
					new OrderedPair(Position.X, Position.Y + NewDim.Y), NewDim);
				SE = new QuadTreeNode(Tree, Depth + 1,
					new OrderedPair(Position.X + NewDim.X, Position.Y + NewDim.Y), NewDim);
				foreach (QuadTreeNode node in Nodes)
				{
					Tree.Nodes.Add(node);
				}
				//NorthWest = new QTreeNode(Position, NewDim, Tree, Name + ".NorthWest", Depth + 1);
				//Tree.Nodes.Add(NorthWest);
				//NorthEast = new QTreeNode(new OrderedPair(Position.X + NewDim.X, Position.Y), NewDim, Tree, Name + ".NorthEast", Depth + 1);
				//Tree.Nodes.Add(NorthEast);
				//SouthWest = new QTreeNode(new OrderedPair(Position.X, Position.Y + NewDim.Y), NewDim, Tree, Name + ".SouthWest", Depth + 1);
				//Tree.Nodes.Add(SouthWest);
				//SouthEast = new QTreeNode(new OrderedPair(Position.X + NewDim.X, Position.Y + NewDim.Y), NewDim, Tree, Name + ".SouthEast", Depth + 1);
				//Tree.Nodes.Add(SouthEast);

				foreach (Circle c in Circles)
				{
					Insert(c);
				}
			}
		}

		/// <summary>
		/// Returns this node to an unsplit state.  Removes all child nodes.
		/// </summary>
		public void UnSplit()
		{
			//Out.WriteLine("UNSPLITTING");
			for (int i = 0; i < Nodes.Length; i++)
			{
				Tree.Nodes.Remove(Nodes[i]);
				Nodes[i] = null;
			}

			//foreach(QTreeNode node in Nodes) {
			//	Tree.Nodes.Remove(node);
			//}
			//Tree.Nodes.Remove(NorthWest);
			//Tree.Nodes.Remove(NorthEast);
			//Tree.Nodes.Remove(SouthWest);
			//Tree.Nodes.Remove(SouthEast);
		}

		public bool NotSplit()
		{
			return (NW == null && NE == null &&
				SW == null && SE == null);
		}

		/// <summary>
		/// Inserts a circle into this node.  If split will insert into quadrants.
		/// </summary>
		/// <param name="c">The circle to be inserted.</param>
		/// <returns>Returns a list of nodes this circle was inserted into.</returns>
		public List<QuadTreeNode> Insert(Circle c)
		{
			List<QuadTreeNode> nodes = new List<QuadTreeNode>();
			if (NotSplit())
			{
				if (Contains(c))
				{
					if (Circles.Count + 1 > MaxCount && Depth < Tree.MaxDepth)
					{
						SplitNode();
						////
						// this causes an infinite loop when inserting circles at the same point
						////
						nodes.AddRange(Insert(c));
					}
					else
					{
						Circles.Add(c);
						nodes.Add(this);
					}
				}
			}
			else
			{
				foreach (QuadTreeNode n in Nodes)
				{
					nodes.AddRange(n.Insert(c));
				}
				//nodes.AddRange(NorthWest.Insert(c));
				//nodes.AddRange(NorthEast.Insert(c));
				//nodes.AddRange(SouthWest.Insert(c));
				//nodes.AddRange(SouthEast.Insert(c));
			}
			return nodes;
		}

		/// <summary>
		/// Removes a circle from this node.
		/// </summary>
		/// <param name="c">The circle to remove</param>
		/// <returns>A list of nodes this circle was removed from.</returns>
		public List<QuadTreeNode> Remove(Circle c)
		{
			List<QuadTreeNode> nodes = new List<QuadTreeNode>();
			if (NotSplit())
			{
				if (HasA(c))
				{
					Circles.Remove(c);
					nodes.Add(this);
				}
			}
			else
			{
				if (HasA(c))
				{
					if (Circles.Count - 1 <= MaxCount)
					{
						Circles.Remove(c);
						UnSplit();
						nodes.AddRange(Remove(c));
					}
				}
				else
				{
					foreach (QuadTreeNode n in Nodes)
					{
						nodes.AddRange(n.Remove(c));
					}
					//nodes.AddRange(NorthWest.Remove(c));
					//nodes.AddRange(NorthEast.Remove(c));
					//nodes.AddRange(SouthWest.Remove(c));
					//nodes.AddRange(SouthEast.Remove(c));
				}
			}
			return nodes;
		}

		/// <summary>
		/// Resizes this node.  This will be very costly...will only happen if a circle is inserted outside of the area of the quad tree
		/// We can make sure this never actually happens but probably would be safe to have this.  Unless we decide that if it ever does happen
		/// something is horribly wrong.
		/// </summary>
		/// <param name="position">The new position.</param>
		/// <param name="dimensions">The new dimensions.</param>
		//public void Resize(OrderedPair position, OrderedPair dimensions)
		//{
		//	//Pretty sure we decided we weren't going to do this.
		//}

		/// <summary>
		/// If this node has the given circle in it's list of circles.
		/// </summary>
		/// <param name="c">The circle to check.</param>
		/// <returns>True if this circle is contained in this node.  False if not.</returns>
		public bool HasA(Circle c)
		{
			if (NotSplit())
			{
				return Circles.Contains(c);
			}
			return (NW.HasA(c) || NE.HasA(c) ||
				SE.HasA(c) || SE.HasA(c));
		}

		/// <summary>
		/// If this node contains the given circle.
		/// </summary>
		/// <param name="c">The circle to check.</param>
		/// <returns></returns>
		public bool Contains(Circle c)
		{
			return (Position.X <= c.Px + c.Radius &&
					Position.Y <= c.Py + c.Radius &&
					Position.X + Dimensions.X >= c.Px - c.Radius &&
					Position.Y + Dimensions.Y >= c.Py - c.Radius);
		}

		/// <summary>
		/// Gets all of the nodes that have a specified circle.
		/// </summary>
		/// <param name="c">The circle to check.</param>
		/// <returns>The nodes containing this circle.</returns>
		public List<QuadTreeNode> GetAllNodes(Circle c)
		{
			List<QuadTreeNode> nodes = new List<QuadTreeNode>();
			if (NotSplit())
			{
				if (HasA(c))
				{
					nodes.Add(this);
				}
			}
			else
			{
				foreach (QuadTreeNode n in Nodes)
				{
					if (n.HasA(c))
					{
						nodes.AddRange(n.GetAllNodes(c));
					}
				}
				//if (NorthWest.HasA(c))
				//{
				//	nodes.AddRange(NorthWest.GetAllNodes(c));
				//}
				//if (NorthEast.HasA(c))
				//{
				//	nodes.AddRange(NorthEast.GetAllNodes(c));
				//}
				//if (SouthWest.HasA(c))
				//{
				//	nodes.AddRange(SouthWest.GetAllNodes(c));
				//}
				//if (SouthEast.HasA(c))
				//{
				//	nodes.AddRange(SouthEast.GetAllNodes(c));
				//}
			}
			return nodes;
		}

		public List<Circle> Possible(OrderedPair start, OrderedPair end)
		{
			List<Circle> possible = new List<Circle>();
			if (NotSplit())
			{
				//double MinX = start.X;
				//double MaxX = end.X;
				//if (MinX > end.X)
				//{
				//	MinX = end.X;
				//	MaxX = start.X;
				//}
				//double MinY = start.Y;
				//double MaxY = end.Y;
				//if (MinY > end.Y)
				//{
				//	MinY = end.Y;
				//	MaxY = start.Y;
				//}
				if (start.X <= Position.X + Dimensions.X &&
					start.Y <= Position.Y + Dimensions.Y &&
					end.X >= Position.X && end.Y >= Position.Y)
				{
					possible.AddRange(Circles);
				}
			}
			else
			{
				foreach (QuadTreeNode n in Nodes)
				{
					possible.AddRange(n.Possible(start, end));
				}
				//possible.AddRange(NorthWest.Possible(start, end));
				//possible.AddRange(NorthEast.Possible(start, end));
				//possible.AddRange(SouthWest.Possible(start, end));
				//possible.AddRange(SouthEast.Possible(start, end));
			}
			return possible;
		}

		public override String ToString()
		{
			return "";
			//return Name;
		}

		/// <summary>
		/// Draws this quad tree.
		/// </summary>
		/// <param name="g">The graphics object to draw this quad tree on.</param>
		public void Draw(Graphics g)
		{

			if (NotSplit())
			{
				Pen pen = new Pen(Color.Black);
				g.DrawRectangle(pen, (float)Position.X, (float)Position.Y, (float)Dimensions.X, (float)Dimensions.Y);
			}
			else
			{
				foreach (QuadTreeNode n in Nodes)
				{
					n.Draw(g);
				}
				//NorthWest.Draw(g);
				//NorthEast.Draw(g);
				//SouthWest.Draw(g);
				//SouthEast.Draw(g);
			}
		}
	}
}