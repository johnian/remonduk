using System;
using System.Collections.Generic;
using System.Drawing;

namespace Remonduk.Physics.QuadTree
{
	public class QTreeNode
	{
		/// <summary>
		/// The quadrants of this node.  Are null until this node has been split.
		/// </summary>
		public QTreeNode NorthWest, NorthEast, SouthWest, SouthEast;
		/// <summary>
		/// If this node has been split.  Turns some single rectangle into 4.
		/// </summary>
		public bool Split;
		/// <summary>
		/// This nodes position and dimensions.
		/// </summary>
		public OrderedPair Pos, Dim;
		/// <summary>
		/// The list of circles in this node.
		/// </summary>
		HashSet<Circle> Circles;
		/// <summary>
		/// The QuadTree this node belongs to.
		/// </summary>
		QTree Parent;

        /// <summary>
        /// This quad tree's name - Head.NorthWest.SouthEast
        /// </summary>
		public String Name;

        /// <summary>
        /// The maximum count of circles this node can have before splitting.
        /// </summary>
		public int MaxCount;

        /// <summary>
        /// The level of this node - how many splits it represents.
        /// </summary>
        int Level;

		/// <summary>
		/// QTreeNode Constructor.
		/// </summary>
		/// <param name="pos">The position for this node (top left)</param>
		/// <param name="dim">The dimensions for this node (width, height)</param>
		/// <param name="parent">The Quad Tree this node belongs to.</param>
		/// <param name="name">The name for this quad tree.</param>
        /// <param name="level">The level of this quad tree.</param>
		public QTreeNode(OrderedPair pos, OrderedPair dim, QTree parent, String name, int level)
		{
			Pos = pos;
			Dim = dim;
			Parent = parent;
			Split = false;
			Circles = new HashSet<Circle>();
			this.MaxCount = parent.MaxCount;
			Name = name;
            Level = level;
		}

		/// <summary>
		/// Splits this node into 4 quadrants.  Called when the number of circles is greater than limit.
		/// </summary>
		public void SplitNode()
		{
			if (!Split)
			{
				//Can probably do this a little cleaner
				OrderedPair NewDim = new OrderedPair(Dim.X / 2.0, Dim.Y / 2.0);

				NorthWest = new QTreeNode(Pos, NewDim, Parent, Name + ".NorthWest", Level + 1);
				Parent.Nodes.Add(NorthWest);
				NorthEast = new QTreeNode(new OrderedPair(Pos.X + NewDim.X, Pos.Y), NewDim, Parent, Name + ".NorthEast", Level + 1);
				Parent.Nodes.Add(NorthEast);
				SouthWest = new QTreeNode(new OrderedPair(Pos.X, Pos.Y + NewDim.Y), NewDim, Parent, Name + ".SouthWest", Level + 1);
				Parent.Nodes.Add(SouthWest);
				SouthEast = new QTreeNode(new OrderedPair(Pos.X + NewDim.X, Pos.Y + NewDim.Y), NewDim, Parent, Name + ".SouthEast", Level + 1);
				Parent.Nodes.Add(SouthEast);

				Split = true;

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
			Split = false;
			Parent.Nodes.Remove(NorthWest);
			Parent.Nodes.Remove(NorthEast);
			Parent.Nodes.Remove(SouthWest);
			Parent.Nodes.Remove(SouthEast);
		}

		/// <summary>
		/// Inserts a circle into this node.  If split will insert into quadrants.
		/// </summary>
		/// <param name="c">The circle to be inserted.</param>
		/// <returns>Returns a list of nodes this circle was inserted into.</returns>
		public List<QTreeNode> Insert(Circle c)
		{
			List<QTreeNode> nodes = new List<QTreeNode>();
			if (Split)
			{
				nodes.AddRange(NorthWest.Insert(c));
				nodes.AddRange(NorthEast.Insert(c));
				nodes.AddRange(SouthWest.Insert(c));
				nodes.AddRange(SouthEast.Insert(c));
			}
			else
			{
				if (Contains(c))
				{
					if (Circles.Count + 1 >= MaxCount && Level < Parent.MaxLevel)
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

			return nodes;
		}

        /// <summary>
        /// Removes a circle from this node.
        /// </summary>
        /// <param name="c">The circle to remove</param>
        /// <returns>A list of nodes this circle was removed from.</returns>
		public List<QTreeNode> Remove(Circle c)
		{
			List<QTreeNode> nodes = new List<QTreeNode>();
			if (Split)
			{
				if (HasA(c))
				{
					if (Circles.Count - 1 < MaxCount && Split)
					{
						Circles.Remove(c);
						UnSplit();
						nodes.AddRange(Remove(c));
					}
				}
				else
				{
					nodes.AddRange(NorthWest.Remove(c));
					nodes.AddRange(NorthEast.Remove(c));
					nodes.AddRange(SouthWest.Remove(c));
					nodes.AddRange(SouthEast.Remove(c));
				}
			}
			else
			{
				if (HasA(c))
				{
					Circles.Remove(c);
					nodes.Add(this);
				}
			}
			return nodes;
		}

		/// <summary>
		/// Resizes this node.  This will be very costly...will only happen if a circle is inserted outside of the area of the quad tree
		/// We can make sure this never actually happens but probably would be safe to have this.  Unless we decide that if it ever does happen
		/// something is horribly wrong.
		/// </summary>
		/// <param name="pos">The new position.</param>
		/// <param name="dim">The new dimensions.</param>
		public void Resize(OrderedPair pos, OrderedPair dim)
		{
            //Pretty sure we decided we weren't going to do this.
		}

		/// <summary>
		/// If this node has the given circle in it's list of circles.
		/// </summary>
		/// <param name="c">The circle to check.</param>
		/// <returns>True if this circle is contained in this node.  False if not.</returns>
		public bool HasA(Circle c)
		{
			if (Split)
			{
				return (NorthWest.HasA(c) || NorthEast.HasA(c) ||
						SouthWest.HasA(c) || SouthEast.HasA(c));
			}
			return Circles.Contains(c);
		}

		/// <summary>
		/// If this node contains the given circle.
		/// </summary>
		/// <param name="c">The circle to check.</param>
		/// <returns></returns>
		public bool Contains(Circle c)
		{
			return (Pos.X <= c.Px + c.Radius &&
					Pos.Y <= c.Py + c.Radius &&
					Pos.X + Dim.X >= c.Px - c.Radius &&
					Pos.Y + Dim.Y >= c.Py - c.Radius);
		}

		/// <summary>
		/// Gets all of the nodes that have a specified circle.
		/// </summary>
		/// <param name="c">The circle to check.</param>
		/// <returns>The nodes containing this circle.</returns>
		public List<QTreeNode> GetAllNodes(Circle c)
		{
			List<QTreeNode> nodes = new List<QTreeNode>();
			if (Split)
			{
				if (NorthWest.HasA(c))
				{
					nodes.AddRange(NorthWest.GetAllNodes(c));
				}
				if (NorthEast.HasA(c))
				{
					nodes.AddRange(NorthEast.GetAllNodes(c));
				}
				if (SouthWest.HasA(c))
				{
					nodes.AddRange(SouthWest.GetAllNodes(c));
				}
				if (SouthEast.HasA(c))
				{
					nodes.AddRange(SouthEast.GetAllNodes(c));
				}
			}
			else
			{
				if (HasA(c))
				{
					nodes.Add(this);
				}
			}
			return nodes;
		}

        /// <summary>
        /// Draws this quad tree.
        /// </summary>
        /// <param name="g">The graphics object to draw this quad tree on.</param>
		public void Draw(Graphics g)
		{
			if (Split)
			{
				NorthWest.Draw(g);
				NorthEast.Draw(g);
				SouthWest.Draw(g);
				SouthEast.Draw(g);
			}
			else
			{
				Pen pen = new Pen(Color.Black);
				g.DrawRectangle(pen, (float)Pos.X, (float)Pos.Y, (float)Dim.X, (float)Dim.Y);
			}
		}

		public List<Circle> Possible(OrderedPair start, OrderedPair end)
		{
			List<Circle> possible = new List<Circle>();

			if (Split)
			{
				possible.AddRange(NorthWest.Possible(start, end));
				possible.AddRange(NorthEast.Possible(start, end));
				possible.AddRange(SouthWest.Possible(start, end));
				possible.AddRange(SouthEast.Possible(start, end));
			}
			else
			{
				double MinX = start.X;
				double MaxX = end.X;
				if (MinX > end.X)
				{
					MinX = end.X;
					MaxX = start.X;
				}
				double MinY = start.Y;
				double MaxY = end.Y;
				if (MinY > end.Y)
				{
					MinY = end.Y;
					MaxY = start.Y;
				}
				if (MinX <= Pos.X + Dim.X &&
					MinY <= Pos.Y + Dim.Y &&
					MaxX >= Pos.X && MaxY >= Pos.Y)
				{
					possible.AddRange(Circles);
				}
			}
			return possible;
		}

		public override String ToString()
		{
			return Name;
		}
	}
}
