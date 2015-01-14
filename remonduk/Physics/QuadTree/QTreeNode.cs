using Remonduk;
using Remonduk.Physics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remonduk.QuadTreeTest
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
        public bool split;
        /// <summary>
        /// This nodes position and dimensions.
        /// </summary>
        public OrderedPair pos, dim;
        /// <summary>
        /// The list of circles in this node.
        /// </summary>
        HashSet<Circle> circles;
        /// <summary>
        /// The QuadTree this node belongs to.
        /// </summary>
        QTree parent;
        public String name;

        public int MaxCount;

        /// <summary>
        /// QTreeNode Constructor.
        /// </summary>
        /// <param name="pos">The position for this node (top left)</param>
        /// <param name="dim">The dimensions for this node (width, height)</param>
        /// <param name="parent">The Quad Tree this node belongs to.</param>
        public QTreeNode(OrderedPair pos, OrderedPair dim, QTree parent, String name)
        {
            this.pos = pos;
            this.dim = dim;
            this.parent = parent;
            this.split = false;
            this.circles = new HashSet<Circle>();
            this.MaxCount = parent.MaxCount;
            this.name = name;
        }

        /// <summary>
        /// Splits this node into 4 quadrants.  Called when the number of circles is greater than limit.
        /// </summary>
        public void Split()
        {
            if (!split)
            {
                //Can probably do this a little cleaner
                OrderedPair NewDim = new OrderedPair(dim.X / 2.0, dim.Y / 2.0);

                NorthWest = new QTreeNode(pos, NewDim, parent, name + ".NorthWest");
                parent.Nodes.Add(NorthWest);
                NorthEast = new QTreeNode(new OrderedPair(pos.X + NewDim.X, pos.Y), NewDim, parent, name + ".NorthEast");
                parent.Nodes.Add(NorthEast);
                SouthWest = new QTreeNode(new OrderedPair(pos.X, pos.Y + NewDim.Y), NewDim, parent, name + ".SouthWest");
                parent.Nodes.Add(SouthWest);
                SouthEast = new QTreeNode(new OrderedPair(pos.X + NewDim.X, pos.Y + NewDim.Y), NewDim, parent, name + ".SouthEast");
                parent.Nodes.Add(SouthEast);

                split = true;

                foreach (Circle c in circles)
                {
                    Insert(c);
                }
            }
        }

        public void UnSplit()
        {
			//Out.WriteLine("UNSPLITTING");
            split = false;
            parent.Nodes.Remove(NorthWest);
            parent.Nodes.Remove(NorthEast);
            parent.Nodes.Remove(SouthWest);
            parent.Nodes.Remove(SouthEast);
        }

        /// <summary>
        /// Inserts a circle into this node.  If split will insert into quadrants.
        /// </summary>
        /// <param name="c">The circle to be inserted.</param>
        /// <returns>Returns a list of nodes this circle was inserted into.</returns>
        public List<QTreeNode> Insert(Circle c)
        {
            List<QTreeNode> nodes = new List<QTreeNode>();
            if(split)
            {
                nodes.AddRange(NorthWest.Insert(c));
                nodes.AddRange(NorthEast.Insert(c));
                nodes.AddRange(SouthWest.Insert(c));
                nodes.AddRange(SouthEast.Insert(c));
            }
            else
            {
                if(Contains(c))
                {
                    if (circles.Count + 1 >= MaxCount)
                    {
                        Split();
                        nodes.AddRange(Insert(c));
                    }
                    else
                    {
                        circles.Add(c);
                        nodes.Add(this);
                    }

                }
            }

            return nodes;
        }

        public List<QTreeNode> Remove(Circle c)
        {
            List<QTreeNode> nodes = new List<QTreeNode>();
            if(split)
            {
                if (HasA(c))
                {
                    if (circles.Count - 1 < MaxCount && split)
                    {
                        circles.Remove(c);
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
                if(HasA(c))
                {
                    circles.Remove(c);
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
        /// <param name="pos"></param>
        /// <param name="dim"></param>
        public void Resize(OrderedPair pos, OrderedPair dim)
        {

        }

        /// <summary>
        /// If this node has the given circle in it's list of circles.
        /// </summary>
        /// <param name="c">The circle to check.</param>
        /// <returns></returns>
        public bool HasA(Circle c)
        {
            if(split)
            {
                return (NorthWest.HasA(c) || NorthEast.HasA(c) ||
                        SouthWest.HasA(c) || SouthEast.HasA(c));
            }
            return circles.Contains(c);
        }

        /// <summary>
        /// If this node contains the given circle.
        /// </summary>
        /// <param name="c">The circle to check.</param>
        /// <returns></returns>
        public bool Contains(Circle c)
        {
            return (pos.X <= c.Px + c.Radius &&
                    pos.Y <= c.Py + c.Radius &&
                    pos.X + dim.X >= c.Px - c.Radius &&
                    pos.Y + dim.Y >= c.Py - c.Radius);
        }

        /// <summary>
        /// Gets all of the nodes that have a specified circle.
        /// </summary>
        /// <param name="c">The circle to check.</param>
        /// <returns>The nodes containing this circle.</returns>
        public List<QTreeNode> GetAllNodes(Circle c)
        {
            List<QTreeNode> nodes = new List<QTreeNode>();
            if(split)
            {
                if(NorthWest.HasA(c))
                {
                    nodes.AddRange(NorthWest.GetAllNodes(c));
                }
                if(NorthEast.HasA(c))
                {
                    nodes.AddRange(NorthEast.GetAllNodes(c));
                }
                if(SouthWest.HasA(c))
                {
                    nodes.AddRange(SouthWest.GetAllNodes(c));
                }
                if(SouthEast.HasA(c))
                {
                    nodes.AddRange(SouthEast.GetAllNodes(c));
                }
            }
            else
            {
                if(HasA(c))
                {
                    nodes.Add(this);
                }
            }
            return nodes;
        }

        public void draw(Graphics g)
        {
            if(split)
            {
                NorthWest.draw(g);
                NorthEast.draw(g);
                SouthWest.draw(g);
                SouthEast.draw(g);
            }
            else
            {
                Pen pen = new Pen(Color.Black);
                g.DrawRectangle(pen, (float)pos.X, (float)pos.Y, (float)dim.X, (float)dim.Y);
            }
        }

        public List<Circle> Possible(OrderedPair start, OrderedPair end)
        {
            List<Circle> possible = new List<Circle>();

            if(split)
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
                if(MinX > end.X)
                {
                    MinX = end.X;
                    MaxX = start.X;
                }
                double MinY = start.Y;
                double MaxY = end.Y;
                if(MinY > end.Y)
                {
                    MinY = end.Y;
                    MaxY = start.Y;
                }
                if (MinX <= pos.X + dim.X &&
                    MinY <= pos.Y + dim.Y &&
                    MaxX >= pos.X && MaxY >= pos.Y)
                {
                    possible.AddRange(circles);
                }
            }
            return possible;
        }

        public String ToString()
        {
            return name;
        }
    }
}
