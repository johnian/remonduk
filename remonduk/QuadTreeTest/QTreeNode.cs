using Remonduk.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace remonduk.QuadTreeTest
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
        bool split;
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

        /// <summary>
        /// QTreeNode Constructor.
        /// </summary>
        /// <param name="pos">The position for this node (top left)</param>
        /// <param name="dim">The dimensions for this node (width, height)</param>
        /// <param name="parent">The Quad Tree this node belongs to.</param>
        public QTreeNode(OrderedPair pos, OrderedPair dim, QTree parent)
        {
            this.pos = pos;
            this.dim = dim;
            this.parent = parent;
            this.split = false;
            this.circles = new HashSet<Circle>();
        }

        /// <summary>
        /// Splits this node into 4 quadrants.  Called when the number of circles is greater than limit.
        /// </summary>
        public void Split()
        {
            //Can probably do this a little cleaner
            OrderedPair MidPoint = new OrderedPair(pos.X + (dim.X / 2.0), pos.Y + (dim.Y / 2.0) );
            OrderedPair NewDim = new OrderedPair(dim.X / 2.0, dim.Y / 2.0);
            
            NorthWest = new QTreeNode(pos, NewDim, parent);
            parent.nodes.Add(NorthWest);
            NorthEast = new QTreeNode(new OrderedPair(pos.X + NewDim.X, pos.Y), NewDim, parent);
            parent.nodes.Add(NorthEast);
            SouthWest = new QTreeNode(new OrderedPair(pos.X, pos.Y + NewDim.Y), NewDim, parent);
            parent.nodes.Add(SouthWest);
            SouthEast = new QTreeNode(MidPoint, NewDim, parent);
            parent.nodes.Add(SouthEast);
            
            split = true;
            
            //I'd like for this to not be needed...but I'm not seeing how.  Spliting can/will be costly.
            foreach (Circle c in circles)
            {
                Insert(c);
            }
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
                    circles.Add(c);
                }
                nodes.Add(this);
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
                    nodes.Add(NorthWest);
                }
                if(NorthEast.HasA(c))
                {
                    nodes.Add(NorthEast);
                }
                if(SouthWest.HasA(c))
                {
                    nodes.Add(SouthWest);
                }
                if(SouthEast.HasA(c))
                {
                    nodes.Add(SouthEast);
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
    }
}
