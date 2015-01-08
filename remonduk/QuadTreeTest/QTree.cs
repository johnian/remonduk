using Remonduk.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace remonduk.QuadTreeTest
{
    public class QTree
    {
        /// <summary>
        /// The starting node for this Quad Tree
        /// </summary>
        public QTreeNode HeadNode;
        /// <summary>
        /// All of the nodes that make up this Quad Tree
        /// </summary>
        public HashSet<QTreeNode> nodes;

        public HashSet<Circle> circles;
        /// <summary>
        /// This quad tree's position and dimensions.
        /// </summary>
        OrderedPair pos, dim;

        /// <summary>
        /// Default constructor...silly values.
        /// </summary>
        public QTree()
        {
            pos = new OrderedPair(0, 0);
            dim = new OrderedPair(100, 100);
            HeadNode = new QTreeNode(pos, dim, this);
            nodes = new HashSet<QTreeNode>();
            nodes.Add(HeadNode);
            circles = new HashSet<Circle>();
        }

        /// <summary>
        /// Quad Tree Constructor
        /// </summary>
        /// <param name="pos">This quad tree's position.</param>
        /// <param name="dim">This quad tree's dimensions.</param>
        public QTree(OrderedPair pos, OrderedPair dim)
        {
            this.pos = pos;
            this.dim = dim;
            HeadNode = new QTreeNode(pos, dim, this);
            nodes = new HashSet<QTreeNode>();
            nodes.Add(HeadNode);
            circles = new HashSet<Circle>();
        }

        /// <summary>
        /// Inserts a circle into this quad tree.
        /// </summary>
        /// <param name="c">The circle to insert into this quad tree.</param>
        /// <returns></returns>
        public List<QTreeNode> Insert(Circle c)
        {
            circles.Add(c);
            return HeadNode.Insert(c);
        }

        /// <summary>
        /// Gets all of the nodes that has the given circle.
        /// </summary>
        /// <param name="c">The circle to get all nodes for.</param>
        /// <returns>A list of nodes that has the given circle.</returns>
        public List<QTreeNode> getNodes(Circle c)
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


    }
}
