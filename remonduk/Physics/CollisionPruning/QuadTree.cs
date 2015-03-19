using Remonduk;
using Remonduk.Physics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remonduk.Physics.CollisionPruning
{
	/// <summary>
	/// 
	/// </summary>
	public class QuadTree
	{
		/// <summary>
		/// The default value for the maximum count of circles before splitting.
		/// </summary>
		public const int MAX_COUNT = 8;
		/// <summary>
		/// The default value for the maximum number of levels this tree will split to.
		/// </summary>
		public const int MAX_DEPTH = 4;

		/// <summary>
		/// The root node of the Quad Tree.
		/// </summary>
		public QuadTreeNode Root;

		/// <summary>
		/// All of the nodes contained in the Quad Tree.
		/// Used as an efficient way to gather the list of nodes.
		/// </summary>
		public HashSet<QuadTreeNode> Nodes;
		/// <summary>
		/// All of the circles contained in the Quad Tree.
		/// Used as an efficient way to gather the list of circles.
		/// </summary>
		public HashSet<Circle> Circles;

		/// <summary>
		/// The dimensions of the Quad Tree.
		/// </summary>
		public OrderedPair Dimensions;
		/// <summary>
		/// The max speed a circle in the Quad Tree can travel.
		/// </summary>
		public double MaxSpeed;

		/// <summary>
		/// The maximum count of circles a node can have before splitting.
		/// </summary>
		public int MaxCount;
		/// <summary>
		/// The maximum number of levels the nodes will split to.
		/// </summary>
		public int MaxDepth;


		/// <summary>
		/// Default constructor...silly values.
		/// </summary>
		//public QTree()
		//{
		//	Pos = new OrderedPair(0, 0);
		//	Dim = new OrderedPair(100, 100);
		//	MaxDepth = 6;
		//	Root = new QTreeNode(Pos, Dim, this, "Head", 0);
		//	Nodes = new HashSet<QTreeNode>();
		//	Circles = new HashSet<Circle>();
		//	MaxCount = 16;

		//	//Nodes.Add(Root); Just remember we're NOT doing this.
		//}

		/// <summary>
		/// Quad Tree Constructor
		/// </summary>
		/// <param name="pos">This quad tree's position.</param>
		/// <param name="dim">This quad tree's dimensions.</param>
		/// <param name="maxCount">The max count before splitting</param>
		/// <param name="maxDepth">The max number of levels this tree will split to.</param>
		public QuadTree(PhysicalSystem physicalSystem, int maxCount = MAX_COUNT, int maxDepth = MAX_DEPTH)
		{
			Dimensions = physicalSystem.Dimensions;
			MaxSpeed = physicalSystem.MaxSpeed;

			Nodes = new HashSet<QuadTreeNode>();
			Circles = new HashSet<Circle>();

			MaxCount = maxCount;
			MaxDepth = maxDepth;

			Root = new QuadTreeNode(this, 0, new OrderedPair(), Dimensions);
			//Root = new QTreeNode(new OrderedPair(), Dimensions, this, "Head", 0);
			Nodes.Add(Root); // might be removing this
		}

		/// <summary>
		/// Quad Tree Constructor assuming (0,0) position.
		/// </summary>
		/// <param name="dim">This quad tree's dimensions.</param>
		/// <param name="maxCount">The max count before splitting</param>
		/// <param name="maxDepth">The max number of levels this tree will split to.</param>
		//public QTree(OrderedPair dim, int maxCount = MAX_COUNT, int maxDepth = MAX_DEPTH)
		//{
		//	Pos = new OrderedPair(0, 0);
		//	Dim = dim;
		//	MaxCount = maxCount;
		//	MaxDepth = maxDepth;
		//	Root = new QTreeNode(Pos, Dim, this, "Head", 0);
		//	Nodes = new HashSet<QTreeNode>();
		//	Circles = new HashSet<Circle>();

		//	//Nodes.Add(Root); Just remember we're NOT doing this.
		//}

		/// <summary>
		/// Insert a circle into the Quad Tree.
		/// </summary>
		/// <param name="c">The circle to insert into the quad tree.</param>
		/// <returns>All of the nodes this circle was inserted into.</returns>
		public List<QuadTreeNode> Insert(Circle c)
		{
			// do we want to insert circles into multiple nodes
			List<QuadTreeNode> nodes = Root.Insert(c);
			if (nodes.Count > 0)
			{
				Circles.Add(c);
			}
			return nodes;
		}

		/// <summary>
		/// Removes a circle from this quad tree.
		/// </summary>
		/// <param name="c">The circles to remove from this quad tree.</param>
		/// <returns>All of the nodes that had this circle removed from them.</returns>
		public List<QuadTreeNode> Remove(Circle c)
		{
			if (Circles.Remove(c))
			{
				return Root.Remove(c);
			}
			return new List<QuadTreeNode>();
		}

		/// <summary>
		/// Gets all of the nodes that has the given circle.
		/// </summary>
		/// <param name="c">The circle to get all nodes for.</param>
		/// <returns>A list of nodes that has the given circle.</returns>
		public List<QuadTreeNode> GetNodes(Circle c)
		{
			return Root.GetAllNodes(c);
		}

		/// <summary>
		/// Check the QTreeNode resize comment...very costly.
		/// </summary>
		/// <param name="c">The circle that was inserted that doesn't fit</param>
		/// <returns>The new dimensions for this quad tree</returns>
		//public OrderedPair Resize(Circle c)
		//{
		//	OrderedPair NewSize = new OrderedPair(0, 0);

		//	return NewSize;
		//}

		/// <summary>
		/// Updates a circle's position after it has moved.
		/// </summary>
		/// <param name="c">The circle to move</param>
		/// <returns>A list of nodes this circle was inserted into.</returns>
		public List<QuadTreeNode> Move(Circle c)
		{
			// Maybe check to see if we cross boundaries before removing and inserting.
			// is there a better way to do this?
			Root.Remove(c);
			List<QuadTreeNode> nodes = Insert(c);
			return nodes;
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
			double[] x = new double[2];
			double[] y = new double[2];

			x[0] = start.X;
			x[1] = end.X;
			Array.Sort(x);
			//Out.WriteLine(x[0] + " " + x[1]);
			//Out.WriteLine(y[0] + " " + y[1]);

			y[0] = start.Y;
			y[1] = end.Y;
			Array.Sort(y);

			x[0] -= MaxSpeed - circle.Radius;
			y[0] -= MaxSpeed - circle.Radius;

			x[1] += MaxSpeed + circle.Radius;
			y[1] += MaxSpeed + circle.Radius;

			return Root.Possible(new OrderedPair(x[0], y[0]), new OrderedPair(x[1], y[1]));
		}

		/// <summary>
		/// Draws this quad tree.
		/// </summary>
		/// <param name="g">The graphics object to draw on.</param>
		public void Draw(Graphics g)
		{
			Root.Draw(g);
		}
	}
}
