using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using remonduk.QuadTreeTest;

namespace remonduk
{
	/// <summary>
	/// Building blocks.
	/// </summary>
	public class Circle
	{
		////
		// Constants
		////
		
		public const int PRECISION = 8;

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
		public const Circle TARGET = null;
		/// <summary>
		/// Default MinDistance.
		/// </summary>
		public const double MIN_DISTANCE = 0;
		/// <summary>
		/// Default MaxDistance.
		/// </summary>
		public const double MAX_DISTANCE = 0;

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
		public Circle Target;
		/// <summary>
		/// The minimum distance this circle will Follow the target to.
		/// </summary>
		public double MinDistance;
		/// <summary>
		/// The maximum distance this circle will Follow the target from.
		/// </summary>
		public double MaxDistance;

		/// <summary>
		/// The circle's color.
		/// </summary>
		public Color Color;

		public QuadTreeTest.QuadTreePositionItem<Circle> q_tree_pos;

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
			Follow(TARGET);

			Color = COLOR;
			q_tree_pos = new QuadTreeTest.QuadTreePositionItem<Circle>(this, new Tuple<double, double>(Position.X, Position.Y), new Tuple<double, double>(radius, radius));
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
			Position.X = px;
			Position.Y = py;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="vx"></param>
		/// <param name="vy"></param>
		public void SetVelocity(double vx, double vy)
		{
			Velocity.X = vx;
			Velocity.Y = vy;
			if (Target != null)
			{
				FaceTarget();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ax"></param>
		/// <param name="ay"></param>
		public void SetAcceleration(double ax, double ay)
		{
			Acceleration.X = ax;
			Acceleration.Y = ay;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="target"></param>
		public void Follow(Circle target = null)
		{
			if (target == null || target == this)
			{
				Target = null;
				MinDistance = MIN_DISTANCE;
				MaxDistance = MAX_DISTANCE;
			}
			else
			{
				Follow(target, target.Radius + Radius, target.Radius + Radius);
			}
		}

		/// <summary>
		/// Sets a circle for this circle to Follow.
		/// </summary>
		/// <param name="target">The target circle this circle should Follow.</param>
		/// <param name="min_distance">The minimum distance this circle will start following at.</param>
		/// <param name="max_distance">The maximum distance this circle will start following at.</param>
		public void Follow(Circle target, double min_distance, double max_distance)
		{
			if (target == null || target == this)
			{
				Follow();
			}
			else
			{
				Target = target;
				MinDistance = min_distance;
				MaxDistance = max_distance;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void FaceTarget()
		{
			Velocity.Set(OrderedPair.Angle(Target.Py - Py, Target.Px - Px));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public void UpdateVelocity(double time)
		{
			SetVelocity(Ax * time + Vx, Ay * time + Vy);
			if (Target != null)
			{
				FaceTarget();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public void UpdatePosition(double time)
		{
			SetPosition(Ax * time * time / 2 + Vx * time + Px,
				Ay * time * time / 2 + Vy * time + Py);
			q_tree_pos.Position = new Tuple<double, double>(Position.X, Position.Y);
			UpdateVelocity(time);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public OrderedPair PotentialPosition()
		{
			double potential_x = Ax / 2 + Vx + Px;
			double potential_y = Ay / 2 + Vy + Py;
			return new OrderedPair(potential_x, potential_y);
		}

		/// <summary>
		/// Calculates the closest point on the this' movement vector to that.
		/// </summary>
		/// <param name="that_x">That circles x value.</param>
		/// <param name="that_y">That circles y value</param>
		/// <param name="reference_vx">This circles reference vx.</param>
		/// <param name="reference_vy">This circles reference vy.</param>
		/// <returns>The closest point on this' movement vector to that_x and that_y</returns>
		public OrderedPair ClosestPoint(double that_x, double that_y,
			double reference_vx, double reference_vy)
		{
			double constant_1 = reference_vy * Px - reference_vx * Py;
			double constant_2 = reference_vx * that_x + reference_vy * that_y;

			double determinant = reference_vx * reference_vx + reference_vy * reference_vy;

			double intersection_x;
			double intersection_y;
			if (determinant == 0)
			{
				intersection_x = Px;
				intersection_y = Py;
			}
			else
			{
				intersection_x = (constant_1 * reference_vy + constant_2 * reference_vx) / determinant;
				intersection_y = (constant_2 * reference_vy - constant_1 * reference_vx) / determinant;
			}
			return new OrderedPair(intersection_x, intersection_y);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public double Crossing(Circle that, double time)
		{
			double this_vx = Acceleration.X * time / 2 + Velocity.X;
			double this_vy = Acceleration.Y * time / 2 + Velocity.Y;
			double that_vx = that.Acceleration.X * time / 2 + that.Velocity.X;
			double that_vy = that.Acceleration.Y * time / 2 + that.Velocity.Y;
			double reference_vx = this_vx - that_vx;
			double reference_vy = this_vy - that_vy;

			OrderedPair point = ClosestPoint(that.Position.X, that.Position.Y, reference_vx, reference_vy);
			if (point == null)
			{
				return Double.PositiveInfinity;
			}
			double distance_from_that = OrderedPair.Magnitude(point.X - that.Position.X, point.Y - that.Position.Y);
			double radii_sum = that.Radius + Radius;

			if (distance_from_that > radii_sum)
			{
				return Double.PositiveInfinity;
			}
			else
			{
				double distance_from_collision_squared = radii_sum * radii_sum - distance_from_that * distance_from_that;
				double distance_from_collision = Math.Sqrt(distance_from_collision_squared);

				double reference_velocity = OrderedPair.Magnitude(reference_vx, reference_vy);
				double collision_x = point.X - distance_from_collision * reference_vx / reference_velocity;
				double collision_y = point.Y - distance_from_collision * reference_vy / reference_velocity;
				double time_x;
				if (reference_vx == 0)
				{
					if (collision_x == Px) time_x = 0;
					else time_x = Double.PositiveInfinity;
				}
				else
				{
					time_x = (collision_x - Px) / reference_vx;
				}
				double time_y;
				if (reference_vy == 0)
				{
					if (collision_y == Py) time_y = 0;
					else time_y = Double.PositiveInfinity;
				}
				else
				{
					time_y = (collision_y - Py) / reference_vy;
				}
				if (time_x >= 0 && time_x <= time &&
					time_y >= 0 && time_y <= time)
				{
					return Math.Round((time_x > time_y) ? time_x : time_y, PRECISION);
				}
				else
				{
					return Double.PositiveInfinity;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public double Colliding(Circle that, double time)
		{
			if (DistanceSquared(that.Position) <= (that.Radius + Radius) * (that.Radius + Radius))
			{
				if (that != this)
				{
					double old_distance = Distance(that);

					double this_px = Ax / 2 + Vx + Px;
					double this_py = Ay / 2 + Vy + Py;
					double that_px = that.Ax / 2 + that.Vx + that.Px;
					double that_py = that.Ay / 2 + that.Vy + that.Py;
					double new_distance = OrderedPair.Magnitude(that_px - this_px, that_py - this_py);

					if (new_distance < old_distance)
					{
						return 0;
					}
				}
				return Double.PositiveInfinity;
			}
			return Crossing(that, time);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		public bool Colliding(Circle that)
		{
			return (DistanceSquared(that.Position) <= (that.Radius + Radius) * (that.Radius + Radius));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="circles"></param>
		/// <returns></returns>
		public OrderedPair CollideWith(List<Circle> circles)
		{
			double total_vx = 0;
			double total_vy = 0;
			foreach (Circle that in circles)
			{
				if (that == this)
				{
					continue;
				}

				total_vx += (Vx * (Mass - that.Mass) + 2 * that.Vx * that.Mass) / (Mass + that.Mass);
				total_vy += (Vy * (Mass - that.Mass) + 2 * that.Vy * that.Mass) / (Mass + that.Mass);
				// modify for elasticity
				// use average elasticity between two Colliding objects for simplicity
				// try to derive a formula for it
			}
			return new OrderedPair(total_vx, total_vy);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public void Update(double time)
		{
			UpdatePosition(time);
		}

		/// <summary>
		/// Calculates the distance from this circle to that circle.
		/// </summary>
		/// <param name="other">The other circle to calculate distance to.</param>
		/// <returns>The distance to the other circle.</returns>
		public double Distance(Circle other)
		{
			return Position.Magnitude(other.Position);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		public double DistanceSquared(OrderedPair that)
		{
			double distx = that.X - Position.X;
			double disty = that.Y - Position.Y;
			return distx * distx + disty * disty;
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

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public String ToString()
		{
			return "radius: " + Radius + ", mass: " + Mass + "\n" +
				"position: " + Position + "\n" +
				"velocity: " + Velocity + "\n" +
				"acceleration: " + Acceleration + "\n" +
				"target: " + Target + "[" + MinDistance + ", " + MaxDistance + "]\n" +
				"color: " + Color;
		}
	}
}