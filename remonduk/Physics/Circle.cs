using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace Remonduk.Physics
{
	/// <summary>
	/// Building blocks.
	/// </summary>
	public partial class Circle
	{
		////
		// Constants
		////

		/// <summary>
		/// Default radius.
		/// </summary>
		public const double RADIUS = 1;
		/// <summary>
		/// Default mass.
		/// </summary>
		public const double MASS = 1;

		/// <summary>
		/// Default px.
		/// </summary>
		public const double PX = 0;
		/// <summary>
		/// Default py.
		/// </summary>
		public const double PY = 0;
		/// <summary>
		/// Default Vx.
		/// </summary>
		public const double VX = 0;
		/// <summary>
		/// Default Vy.
		/// </summary>
		public const double VY = 0;
		/// <summary>
		/// Default Ax.
		/// </summary>
		public const double AX = 0;
		/// <summary>
		/// Default Ay.
		/// </summary>
		public const double AY = 0;

		/// <summary>
		/// Default Exists.
		/// </summary>
		public const bool EXISTS = true;
		/// <summary>
		/// Default Target.
		/// </summary>
		//public const Circle TARGET = null;
		/// <summary>
		/// Default MinDistance.
		/// </summary>
		//public const double MIN_DISTANCE = 0;
		/// <summary>
		/// Default MaxDistance.
		/// </summary>
		//public const double MAX_DISTANCE = 0;

		/// <summary>
		/// Default Color.
		/// </summary>
		public Color COLOR = Color.Chartreuse;	//Need to rework this for loading / saving.  Save to string and load from there

		////
		// Fields
		////		

		/// <summary>
		/// The circle's radius.
		/// </summary>
		public double Radius;
		/// <summary>
		/// The circle's mass.
		/// </summary>
		public double Mass;
		/// <summary>
		/// The circle's position vector.
		/// </summary>
		public OrderedPair Position;
		/// <summary>
		/// The circle's velocity vector.
		/// </summary>
		public OrderedPair Velocity;
		/// <summary>
		/// The circle's acceleration vector.
		/// </summary>
		public OrderedPair Acceleration;
		/// <summary>
		/// Shorthand for position.X.
		/// </summary>
		public double Px { get { return Position.X; } }
		/// <summary>
		/// Shorthand for position.Y.
		/// </summary>
		public double Py { get { return Position.Y; } }
		/// <summary>
		/// Shorthand for Velocity.X.
		/// </summary>
		public double Vx { get { return Velocity.X; } }
		/// <summary>
		/// Shorthand for Velocity.Y.
		/// </summary>
		public double Vy { get { return Velocity.Y; } }
		/// <summary>
		/// Shorthand for Acceleration.X.
		/// </summary>
		public double Ax { get { return Acceleration.X; } }
		/// <summary>
		/// Shorthand for Acceleration.Y.
		/// </summary>
		public double Ay { get { return Acceleration.Y; } }

		/// <summary>
		/// Whether this circle exists (if other objects in the physical system can interact with it).
		/// </summary>
		public bool Exists;
		/// <summary>
		/// The circle's target. Used for non-physics interactions.
		/// </summary>
		//public Circle Target;
		/// <summary>
		/// The minimum distance this circle will Follow the target to.
		/// </summary>
		//public double MinDistance;
		/// <summary>
		/// The maximum distance this circle will Follow the target from.
		/// </summary>
		//public double MaxDistance;

		/// <summary>
		/// The circle's color.
		/// </summary>
		public Color Color;

		/// <summary>
		/// 
		/// </summary>
		//public QuadTreeTest.QuadTreePositionItem<Circle> QTreePos;


		////
		// Constructors
		////

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Circle() : this(RADIUS) { }

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="that">The circle to copy.</param>
		public Circle(Circle that) :
			this(that.Radius, that.Position.X, that.Position.Y,
				that.Velocity.X, that.Velocity.Y, that.Acceleration.X, that.Acceleration.Y, that.Mass) { }

		/// <summary>
		/// Creates a circle with the specified radius and optional mass.
		/// </summary>
		/// <param name="radius">The radius of the circle.</param>
		/// <param name="mass">The mass of the circle.</param>
		public Circle(double radius, double mass = MASS) :
			this(radius, PX, PY, mass) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="radius"></param>
		/// <param name="px"></param>
		/// <param name="py"></param>
		/// <param name="mass"></param>
		public Circle(double radius, double px, double py, double mass = MASS) :
			this(radius, px, py, VX, VY, mass) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="radius"></param>
		/// <param name="px"></param>
		/// <param name="py"></param>
		/// <param name="vx"></param>
		/// <param name="vy"></param>
		/// <param name="mass"></param>
		public Circle(double radius, double px, double py, double vx, double vy, double mass = MASS) :
			this(radius, px, py, vx, vy, AX, AY, mass) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="radius"></param>
		/// <param name="px"></param>
		/// <param name="py"></param>
		/// <param name="vx"></param>
		/// <param name="vy"></param>
		/// <param name="ax"></param>
		/// <param name="ay"></param>
		/// <param name="mass"></param>
		public Circle(double radius, double px, double py,
			double vx, double vy, double ax, double ay, double mass = MASS)
		{
			SetRadius(radius);
			SetMass(mass);

			Position = new OrderedPair(px, py);
			Velocity = new OrderedPair(vx, vy);
			Acceleration = new OrderedPair(ax, ay);

			Exists = EXISTS;
			//Follow(TARGET);

			Color = COLOR;

			//QTreePos = new QuadTreePositionItem<Circle>(this, new Tuple<double, double>(Position.X, Position.Y), new Tuple<double, double>(radius, radius));
		}

		/// <summary>
		/// Sets the radius.
		/// </summary>
		/// <param name="radius">The new value for this circle's radius.</param>
		public void SetRadius(double radius)
		{
			if (radius < RADIUS)
			{
				throw new ArgumentException("radius: " + radius);
			}
			Radius = radius;
		}

		/// <summary>
		/// Set this circles mass.
		/// </summary>
		/// <param name="mass">The value to set this circles mass to.</param>
		public void SetMass(double mass)
		{
			Mass = mass;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="px"></param>
		/// <param name="py"></param>
		public void SetPosition(double px, double py)
		{
			Position.SetXY(px, py);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="vx"></param>
		/// <param name="vy"></param>
		public void SetVelocity(double vx, double vy)
		{
			Velocity.SetXY(vx, vy);
			//if (Target != null)
			//{
			//	FaceTarget();
			//}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ax"></param>
		/// <param name="ay"></param>
		public void SetAcceleration(double ax, double ay)
		{
			Acceleration.SetXY(ax, ay);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="target"></param>
		//public void Follow(Circle target = null)
		//{
		//	if (target == null || target == this)
		//	{
		//		Target = null;
		//		MinDistance = MIN_DISTANCE;
		//		MaxDistance = MAX_DISTANCE;
		//	}
		//	else
		//	{
		//		Follow(target, target.Radius + Radius, target.Radius + Radius);
		//	}
		//}

		/// <summary>
		/// Sets a circle for this circle to Follow.
		/// </summary>
		/// <param name="target">The target circle this circle should Follow.</param>
		/// <param name="minDistance">The minimum distance this circle will start following at.</param>
		/// <param name="maxDistance">The maximum distance this circle will start following at.</param>
		//public void Follow(Circle target, double minDistance, double maxDistance)
		//{
		//	if (target == null || target == this)
		//	{
		//		Follow();
		//	}
		//	else
		//	{
		//		Target = target;
		//		MinDistance = minDistance;
		//		MaxDistance = maxDistance;
		//	}
		//}

		/// <summary>
		/// 
		/// </summary>
		//public void FaceTarget()
		//{
		//	double angle = OrderedPair.Angle(Target.Py - Py, Target.Px - Px);
		//	Velocity.SetXY(angle);
		//}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public void UpdateVelocity(double time)
		{
			//if (Target != null)
			//{
			//	FaceTarget();
			//}
			SetVelocity(Ax * time + Vx, Ay * time + Vy);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public void UpdatePosition(double time)
		{
			SetPosition((Ax * time / 2 + Vx) * time + Px,
				(Ay * time / 2 + Vy) * time + Py);
			//QTreePos.Position = new Tuple<double, double>(Position.X, Position.Y);
			UpdateVelocity(time);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public void Update(double time)
		{
			// will this ever need to do anything else??
			UpdatePosition(time);
			// UpdateVelocity
			// UpdateAcceleration
		}

		/// <summary>
		/// Draws this circle.
		/// </summary>
		/// <param name="g">The graphics object to draw this circle on.</param>
		public void Draw(Graphics g)
		{
			Brush brush = new SolidBrush(Color.Chartreuse);
			g.FillEllipse(brush, (float)(Px - Radius), (float)(Py - Radius), (float)(2 * Radius), (float)(2 * Radius));
		}

        public bool Contains(OrderedPair point)
        {
            return (point.X < Px + Radius &&
                    point.X > Px - Radius &&
                    point.Y < Py + Radius &&
                    point.Y > Py - Radius);
        }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override String ToString()
		{
			return "{" + base.ToString() + "}\n" +
				"radius: " + Radius + ", mass: " + Mass + "\n" +
				"position: " + Position + "\n" +
				"velocity: " + Velocity + "\n" +
				"acceleration: " + Acceleration + "\n" +
				/*"target: " + Target + "[" + MinDistance + ", " + MaxDistance + "]\n" +*/
				"color: " + Color;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}