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
		/// Default vx.
		/// </summary>
		public const double VX = 0;
		/// <summary>
		/// Default vy.
		/// </summary>
		public const double VY = 0;
		/// <summary>
		/// Default ax.
		/// </summary>
		public const double AX = 0;
		/// <summary>
		/// Default ay.
		/// </summary>
		public const double AY = 0;

		/// <summary>
		/// Default exists.
		/// </summary>
		public const bool EXISTS = true;
		/// <summary>
		/// Default target.
		/// </summary>
		public const Circle TARGET = null;
		/// <summary>
		/// Default min_distance.
		/// </summary>
		public const double MIN_DISTANCE = 0;
		/// <summary>
		/// Default max_distance.
		/// </summary>
		public const double MAX_DISTANCE = 0;

		/// <summary>
		/// Default color.
		/// </summary>
		public Color COLOR = Color.Chartreuse;	//Need to rework this for loading / saving.  Save to string and load from there

		////
		// Fields
		////		

		/// <summary>
		/// The circle's radius.
		/// </summary>
		public double radius;
		/// <summary>
		/// The circle's mass.
		/// </summary>
		public double mass;
		/// <summary>
		/// The circle's position vector.
		/// </summary>
		public OrderedPair position;
		/// <summary>
		/// The circle's velocity vector.
		/// </summary>
		public OrderedPair velocity;
		/// <summary>
		/// The circle's acceleration vector.
		/// </summary>
		public OrderedPair acceleration;
		/// <summary>
		/// Shorthand for position.x.
		/// </summary>
		public double px { get { return position.x; } }
		/// <summary>
		/// Shorthand for position.y.
		/// </summary>
		public double py { get { return position.y; } }
		/// <summary>
		/// Shorthand for velocity.x.
		/// </summary>
		public double vx { get { return velocity.x; } }
		/// <summary>
		/// Shorthand for velocity.y.
		/// </summary>
		public double vy { get { return velocity.y; } }
		/// <summary>
		/// Shorthand for acceleration.x.
		/// </summary>
		public double ax { get { return acceleration.x; } }
		/// <summary>
		/// Shorthand for acceleration.y.
		/// </summary>
		public double ay { get { return acceleration.y; } }

		
		/// <summary>
		/// Whether this circle exists (if other objects in the physical system can interact with it).
		/// </summary>
		public bool exists;
		/// <summary>
		/// The circle's target. Used for non-physics interactions.
		/// </summary>
		public Circle target;
		/// <summary>
		/// The minimum distance this circle will follow the target to.
		/// </summary>
		public double min_distance;
		/// <summary>
		/// The maximum distance this circle will follow the target from.
		/// </summary>
		public double max_distance;

		/// <summary>
		/// The circle's color.
		/// </summary>
		public Color color;

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
			this(that.radius, that.position.x, that.position.y,
				that.velocity.x, that.velocity.y, that.acceleration.x, that.acceleration.y, that.mass) { }

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
			setRadius(radius);
			setMass(mass);

			position = new OrderedPair(px, py);
			velocity = new OrderedPair(vx, vy);
			acceleration = new OrderedPair(ax, ay);

			exists = EXISTS;
			follow(TARGET);

			color = COLOR;
			q_tree_pos = new QuadTreeTest.QuadTreePositionItem<Circle>(this, new Tuple<double, double>(position.x, position.y), new Tuple<double, double>(radius, radius));
		}

		/// <summary>
		/// Sets the radius.
		/// </summary>
		/// <param name="radius">The new value for this circle's radius.</param>
		public void setRadius(double radius)
		{
			if (radius < RADIUS)
			{
				throw new ArgumentException("radius: " + radius);
			}
			this.radius = radius;
		}

		/// <summary>
		/// Set this circles mass.
		/// </summary>
		/// <param name="mass">The value to set this circles mass to.</param>
		public void setMass(double mass)
		{
			this.mass = mass;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="px"></param>
		/// <param name="py"></param>
		public void setPosition(double px, double py)
		{
			position.x = px;
			position.y = py;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="vx"></param>
		/// <param name="vy"></param>
		public void setVelocity(double vx, double vy)
		{
			velocity.x = vx;
			velocity.y = vy;
			if (target != null)
			{
				faceTarget();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ax"></param>
		/// <param name="ay"></param>
		public void setAcceleration(double ax, double ay)
		{
			acceleration.x = ax;
			acceleration.y = ay;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="target"></param>
		public void follow(Circle target = null)
		{
			if (target == null || target == this)
			{
				this.target = null;
				this.min_distance = MIN_DISTANCE;
				this.max_distance = MAX_DISTANCE;
			}
			else
			{
				follow(target, target.radius + radius, target.radius + radius);
			}
		}

		/// <summary>
		/// Sets a circle for this circle to follow.
		/// </summary>
		/// <param name="target">The target circle this circle should follow.</param>
		/// <param name="min_distance">The minimum distance this circle will start following at.</param>
		/// <param name="max_distance">The maximum distance this circle will start following at.</param>
		public void follow(Circle target, double min_distance, double max_distance)
		{
			if (target == null || target == this)
			{
				follow();
			}
			else
			{
				this.target = target;
				this.min_distance = min_distance;
				this.max_distance = max_distance;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void faceTarget()
		{
			velocity.set(OrderedPair.angle(target.py - py, target.px - px));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public void updateVelocity(double time)
		{
			setVelocity(ax * time + vx, ay * time + vy);
			if (target != null)
			{
				faceTarget();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public void updatePosition(double time)
		{
			setPosition(ax * time * time / 2 + vx * time + px,
				ay * time * time / 2 + vy * time + py);
			q_tree_pos.Position = new Tuple<double, double>(position.x, position.y);
			updateVelocity(time);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public OrderedPair potentialPosition()
		{
			double potential_x = ax / 2 + vx + px;
			double potential_y = ay / 2 + vy + py;
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
		public OrderedPair closestPoint(double that_x, double that_y,
			double reference_vx, double reference_vy)
		{
			double constant_1 = reference_vy * px - reference_vx * py;
			double constant_2 = reference_vx * that_x + reference_vy * that_y;

			double determinant = reference_vx * reference_vx + reference_vy * reference_vy;

			double intersection_x;
			double intersection_y;
			if (determinant == 0)
			{
				intersection_x = px;
				intersection_y = py;
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
		public double crossing(Circle that, double time)
		{
			double this_vx = acceleration.x * time / 2 + velocity.x;
			double this_vy = acceleration.y * time / 2 + velocity.y;
			double that_vx = that.acceleration.x * time / 2 + that.velocity.x;
			double that_vy = that.acceleration.y * time / 2 + that.velocity.y;
			double reference_vx = this_vx - that_vx;
			double reference_vy = this_vy - that_vy;

			OrderedPair point = closestPoint(that.position.x, that.position.y, reference_vx, reference_vy);
			if (point == null)
			{
				return Double.PositiveInfinity;
			}
			double distance_from_that = OrderedPair.magnitude(point.x - that.position.x, point.y - that.position.y);
			double radii_sum = that.radius + radius;

			if (distance_from_that > radii_sum)
			{
				return Double.PositiveInfinity;
			}
			else
			{
				double distance_from_collision_squared = radii_sum * radii_sum - distance_from_that * distance_from_that;
				double distance_from_collision = Math.Sqrt(distance_from_collision_squared);

				double reference_velocity = OrderedPair.magnitude(reference_vx, reference_vy);
				double collision_x = point.x - distance_from_collision * reference_vx / reference_velocity;
				double collision_y = point.y - distance_from_collision * reference_vy / reference_velocity;
				double time_x;
				if (reference_vx == 0)
				{
					if (collision_x == px) time_x = 0;
					else time_x = Double.PositiveInfinity;
				}
				else
				{
					time_x = (collision_x - px) / reference_vx;
				}
				double time_y;
				if (reference_vy == 0)
				{
					if (collision_y == py) time_y = 0;
					else time_y = Double.PositiveInfinity;
				}
				else
				{
					time_y = (collision_y - py) / reference_vy;
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
		public double colliding(Circle that, double time)
		{
			if (distanceSquared(that.position) <= (that.radius + radius) * (that.radius + radius))
			{
				if (that != this)
				{
					double old_distance = distance(that);

					double this_px = ax / 2 + vx + px;
					double this_py = ay / 2 + vy + py;
					double that_px = that.ax / 2 + that.vx + that.px;
					double that_py = that.ay / 2 + that.vy + that.py;
					double new_distance = OrderedPair.magnitude(that_px - this_px, that_py - this_py);

					if (new_distance < old_distance)
					{
						return 0;
					}
				}
				return Double.PositiveInfinity;
			}
			return crossing(that, time);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		public bool colliding(Circle that)
		{
			return (distanceSquared(that.position) <= (that.radius + radius) * (that.radius + radius));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="circles"></param>
		/// <returns></returns>
		public OrderedPair collideWith(List<Circle> circles)
		{
			double total_vx = 0;
			double total_vy = 0;
			foreach (Circle that in circles)
			{
				if (that == this)
				{
					continue;
				}

				total_vx += (vx * (mass - that.mass) + 2 * that.vx * that.mass) / (mass + that.mass);
				total_vy += (vy * (mass - that.mass) + 2 * that.vy * that.mass) / (mass + that.mass);
				// modify for elasticity
				// use average elasticity between two colliding objects for simplicity
				// try to derive a formula for it
			}
			return new OrderedPair(total_vx, total_vy);
		}

		/// <summary>
		/// This circles move method.  Updates the velocity, checks for collisions then updates the (x,y) coordinates.
		/// </summary>
		/// <param name="time">For some reason a silly keycollection of circles, change me.</param>
		public void move(double time)
		{
			//if (target != null)
			//{
			//	faceTarget();
			//}
			updatePosition(time);
		}

		/// <summary>
		/// Calculates the distance from this circle to that circle.
		/// </summary>
		/// <param name="other">The other circle to calculate distance to.</param>
		/// <returns>The distance to the other circle.</returns>
		public double distance(Circle other)
		{
			return position.magnitude(other.position);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		public double distanceSquared(OrderedPair that)
		{
			double distx = that.x - position.x;
			double disty = that.y - position.y;
			return distx * distx + disty * disty;
		}

		/// <summary>
		/// This circles update method.  See move()
		/// </summary>
		/// <param name="circles">Some silly keycollection...change me</param>
		//revisit List for refactorization!!!!! rar i like my keyboard this
		public void update(double time)
		{
			move(time);
		}

		/// <summary>
		/// Draws this circle.
		/// </summary>
		/// <param name="g">The graphics object to draw this circle on.</param>
		public void draw(Graphics g)
		{
			Brush brush = new SolidBrush(Color.Chartreuse);
			g.FillEllipse(brush, (float)(position.x - radius), (float)(position.y - radius), (float)(2 * radius), (float)(2 * radius));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public String ToString()
		{
			return "radius: " + radius + ", mass: " + mass + "\n" +
				"position: " + position + "\n" +
				"velocity: " + velocity + "\n" +
				"acceleration: " + acceleration + "\n" +
				"target: " + target + "[" + min_distance + ", " + max_distance + "]\n" +
				"color: " + color;
		}
	}
}