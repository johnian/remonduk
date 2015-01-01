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
		/// <summary>
		/// Default value for a circle's radius.
		/// </summary>
		public const double RADIUS = 1;
		/// <summary>
		/// Default value for a circle's mass.
		/// </summary>
		public const double MASS = 1;

		/// <summary>
		/// Default value for the x component of a circle's position.
		/// </summary>
		public const double PX = 0;
		/// <summary>
		/// Default value for the y component of a circle's position.
		/// </summary>
		public const double PY = 0;
		/// <summary>
		/// Default value for the x component of a circle's velocity.
		/// </summary>
		public const double VX = 0;
		/// <summary>
		/// Default value for the y component of a circle's velocity.
		/// </summary>
		public const double VY = 0;
		/// <summary>
		/// Default value for the x component of a circle's acceleration.
		/// </summary>
		public const double AX = 0;
		/// <summary>
		/// Default value for the y component of a circle's acceleration.
		/// </summary>
		public const double AY = 0;

		public bool EXISTS = true;

		/// <summary>
		/// Default value for a circle's target. Used for non-physics interactions.
		/// </summary>
		public const Circle TARGET = null;
		/// <summary>
		/// Default value for the minimum distance to follow the target at.
		/// </summary>
		public const double MIN_DIST = 0;
		/// <summary>
		/// Default value for the maximum distance to follow the target at.
		/// </summary>
		public const double MAX_DIST = 0;

		/// <summary>
		/// Default value for a circle's color.
		/// </summary>
		//Need to rework this for loading / saving.  Save to string and load from there
		public Color COLOR = Color.Chartreuse;

		/// <summary>
		/// The circle's radius and mass.
		/// </summary>
		public double radius, mass;
		/// <summary>
		/// The circle's position, velocity, and acceleration vectors.
		/// </summary>
		public OrderedPair position, velocity, acceleration;
		public double px { get { return position.x; } set { position.x = value; } }
		public double py { get { return position.y; } set { position.y = value; } }
		public double vx { get { return velocity.x; } set { velocity.x = value; } }
		public double vy { get { return velocity.y; } set { velocity.y = value; } }
		public double ax { get { return acceleration.x; } set { acceleration.x = value; } }
		public double ay { get { return acceleration.y; } set { acceleration.y = value; } }

		/// <summary>
		/// The circle's target. Used for non-physics interactions.
		/// </summary>
		public Circle target;
		/// <summary>
		/// The minimum distance this circle will follow the target to.
		/// </summary>
		public double min_dist;
		/// <summary>
		/// The maximum distance this circle will follow the target from.
		/// </summary>
		public double max_dist;

		/// <summary>
		/// The circle's color.
		/// </summary>
		public Color color;

		/// <summary>
		/// If this circle exists (if it should interact with other objects in the physical system).
		/// </summary>
		bool exists;

		public QuadTreeTest.QuadTreePositionItem<Circle> q_tree_pos;

		/// <summary>
		/// Empty constructor. No values are set.
		/// </summary>
		public Circle() { }

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="other">The other circle to copy.</param>
		public Circle(Circle that) :
			this(that.radius, that.position.x, that.position.y,
                that.velocity.x, that.velocity.y, that.acceleration.x, that.acceleration.y, that.mass) { }

		public Circle(double radius) :
			this(radius, MASS) { }

		public Circle(double radius, double mass = MASS) :
			this(radius, PX, PY, mass) { }

		public Circle(double radius, double px, double py, double mass = MASS) :
			this(radius, px, py, VX, VY, mass) { }

		public Circle(double radius, double px, double py, double vx, double vy, double mass = MASS) :
			this(radius, px, py, vx, vy, AX, AY, mass) { }

		public Circle(double radius, double px, double py,
			double vx, double vy, double ax, double ay, double mass = MASS)
		{
			setRadius(radius);
			setMass(mass);

			position = new OrderedPair(px, py);
			velocity = new OrderedPair(vx, vy);
			acceleration = new OrderedPair(ax, ay);
            Out.WriteLine("CIRCLE ACCEL MAG: " + acceleration.magnitude());

			//setPosition(px, py);
			//setVelocity(vx, vy);
			//setAcceleration(ax, ay);

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

		public void setVelocity(double vx, double vy)
		{
			velocity.x = vx;
			velocity.y = vy;
		}

		public void faceTarget()
		{
			velocity.set(OrderedPair.angle(target.py, target.px));
		}

		/// <summary>
		/// Sets this circles acceleration.  Recalculates ax and ay values.
		/// </summary>
		/// <param name="acceleration">The new acceleration vector's magnitude.</param>
		/// <param name="acceleration_angle">The new acceleration vector's angle.</param>

		public void setAcceleration(double ax, double ay)
		{
			// maybe check for close to 0 precision rounding
			acceleration.x = ax;
			acceleration.y = ay;
		}

		/// <summary>
		/// Sets the target circle for this circle to follow.  Uses default min and max distances.
		/// Somethings a little weird - we should talk about this one.
		/// </summary>
		/// <param name="target">The target circle for this circle to follow.</param>
		public void follow(Circle target = null)
		{
			if (target == null || target == this)
			{
				this.target = null;
				this.min_dist = MIN_DIST;
				this.max_dist = MAX_DIST;
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
		/// <param name="min_dist">The minimum distance this circle will start following at.</param>
		/// <param name="max_dist">The maximum distance this circle will start following at.</param>
		public void follow(Circle target, double min_dist, double max_dist)
		{
			if (target == null || target == this)
			{
				follow();
			}
			else
			{
				this.target = target;
				this.min_dist = min_dist;
				this.max_dist = max_dist;
			}
		}
		
		/// <summary>
		/// Updates this circles velocity by adding its acceleration values to velocity values.  Occurs once per frame.
		/// </summary>
		public void updateVelocity(double time)
		{
			vx += ax * time;
			vy += ay * time;
		}

		/// <summary>
		/// Updates this circles position based on current velocity and acceleration values.
		/// </summary>
		public void updatePosition(double time)
		{
			px += ax * time * time / 2 + vx * time;
			py += ay * time * time / 2 + vy * time;
			q_tree_pos.Position = new Tuple<double, double>(position.x, position.y);
			updateVelocity(time);
		}

		public OrderedPair potentialPosition()
		{
			double potential_x = ax / 2 + vx + px;
			double potential_y = ay / 2 + vy + py;
			return new OrderedPair(potential_x, potential_y);
		}

		/// <summary>
		/// The beginings of elastic collisions.
		/// </summary>
		/// <param name="that">The other circle this circle is colliding with.</param>
		/// <returns></returns>
		//public void elastic(double that_mass, double that_vx, double that_vy, double elasticity = 1)
		//{
		//	// would like to figure out how to determine elasticity of a collision
		//	// wikipedia has an article for calculating the coefficient of restitution
		//	// but what actually determines that -- average hardness of the two objects? etc.
		//	// can we just use our own bastardized version where we give all circles an "elasticity" coefficient
		//	// and take the average of the two colliding bodies
		//	//if (!that.exists)
		//	//	return false;
		//	vx = (vx * (mass - that_mass) + 2 * that_mass * that_vx) / (mass + that_mass);
		//	vy = (vy * (mass - that_mass) + 2 * that_mass * that_vy) / (mass + that_mass);
		//	//double this_vx = (that.vx * (this.mass - that.mass) + 2 * that.mass * that.vx) / (this.mass + that.mass);
		//	//double that_vx = (this.vx * (this.mass - that.mass) + 2 * this.mass * this.vx) / (this.mass + that.mass);
		//	//double this_vy = (that.vy * (this.mass - that.mass) + 2 * that.mass * that.vy) / (this.mass + that.mass);
		//	//double that_vy = (this.vy * (this.mass - that.mass) + 2 * this.mass * this.vy) / (this.mass + that.mass);
		//	//this.setVelocity(Circle.magnitude(this_vx, this_vy), Circle.angle(this_vy, this_vx));
		//	//that.setVelocity(Circle.magnitude(that_vx, that_vy), Circle.angle(that_vy, that_vx));
		//	//return true;
		//}

		/// <summary>
		/// Determines if this circle is colliding with that circle.
		/// </summary>
		/// <param name="that">The circle to check for collision with.</param>
		/// <returns>Returns the angle of impact if colliding.  -1 if not.  </returns>
		public double colliding(Circle that, double time)
		{
			// double center = OrderedPair.angle(that.position.y - position.y, that.position.x - position.x);
			// use squared instead of square root for efficiency
			if (distanceSquared(that.position) <= (that.radius + radius) * (that.radius + radius))
			{
				//Out.WriteLine("overlapping");
				if (that != this)
				{
					//return 0;
				}
				return Double.PositiveInfinity;
				//return -1;
			}
			return crossing(that, time);
		}

		public bool overlapping(Circle that) {
			return (distanceSquared(that.position) <= (that.radius + radius) * (that.radius + radius));
		}

		/// <summary>
		/// Treats that circle as stationary and the frame of perspective as this.
		/// </summary>
		/// <param name="that">The circle to check if this circle is crossing.</param>
		/// <returns>The center of this circle when the two circles collide.</returns>
		public double crossing(Circle that, double time)
		{
			double this_vx = acceleration.x * time / 2 + velocity.x;
			double this_vy = acceleration.y * time / 2 + velocity.y;
			double that_vx = that.acceleration.x * time / 2 + that.velocity.x;
			double that_vy = that.acceleration.y * time / 2 + that.velocity.y;
			double reference_vx = this_vx - that_vx;
			double reference_vy = this_vy - that_vy;
			//Out.WriteLine("");
			//Out.WriteLine("reference_vx: " + reference_vx);
			//Out.WriteLine("reference_vy: " + reference_vy);

			OrderedPair point = closestPoint(that.position.x, that.position.y, reference_vx, reference_vy);
			if (point == null)
			{
				//Out.WriteLine("point is null");
				return Double.PositiveInfinity;
				//return null;
			}
			double distance_from_that = OrderedPair.magnitude(point.x - that.position.x, point.y - that.position.y);
			double radii_sum = that.radius + radius;

			//Out.WriteLine("distance_from_that: " + distance_from_that);
			//Out.WriteLine("radii_sum: " + radii_sum);
			if (distance_from_that > radii_sum)
			{
				//Out.WriteLine("too far");
				return Double.PositiveInfinity;
				//return null;
			}
			else
			{
				double distance_from_collision_squared = radii_sum * radii_sum - distance_from_that * distance_from_that;
				double distance_from_collision = Math.Sqrt(distance_from_collision_squared);

				double reference_velocity = OrderedPair.magnitude(reference_vx, reference_vy);
				double collision_x = point.x - distance_from_collision * reference_vx / reference_velocity;
				double collision_y = point.y - distance_from_collision * reference_vy / reference_velocity;
				double time_x = collision_x / reference_vx;
				double time_y = collision_y / reference_vy;
				if (time_x >= 0 && time_x <= time && time_x == time_y)
				{
					return time_x;
				}
				else
				{
					return Double.PositiveInfinity;
				}
				//return Tuple.Create(collision_x, collision_y);
			}
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
			double x = position.x;
			double y = position.y;
			double constant_1 = reference_vy * x - reference_vx * y;
			double constant_2 = reference_vx * that_x + reference_vy * that_y;

			double determinant = reference_vx * reference_vx + reference_vy * reference_vy;

			double intersection_x;
			double intersection_y;
			if (determinant == 0)
			{
				intersection_x = x;
				intersection_y = y;
			}
			else
			{
				intersection_x = (constant_1 * reference_vy + constant_2 * reference_vx) / determinant;
				intersection_y = (constant_2 * reference_vy - constant_1 * reference_vx) / determinant;
			}

			//Out.WriteLine("intersection_x: " + intersection_x);
			//Out.WriteLine("intersection_y: " + intersection_y);

			double delta_x = intersection_x - x;
			double delta_y = intersection_y - y;
			if (OrderedPair.magnitude(intersection_x - x, intersection_y - y) >
				OrderedPair.magnitude(reference_vx, reference_vy))
			{
				//Out.WriteLine("too far");
				return null;
			}
			return new OrderedPair(intersection_x, intersection_y);
		}
		
		public OrderedPair collideWith(List<Circle> circles)
		{
			double total_mass = 0;
			double total_vx = 0;
			double total_vy = 0;
			//double average_elasticity = elasticity;
			foreach (Circle that in circles)
			{
				if (that == this) {
					continue;
				}
				//total_mass += circle.mass;
				//total_vx += circle.velocity.x;
				//total_vy += circle.velocity.y;
				// average_elasticity += circle.elasticity;

				total_vx += (that.vx * (mass - that.mass) + 2 * that.mass * that.vx) / (mass + that.mass);
				total_vy += (that.vy * (mass - that.mass) + 2 * that.mass * that.vy) / (mass + that.mass);
				// modify for elasticity
				// use average elasticity between two colliding objects for simplicity
				// try to derive a formula for it
			}

			// average_elasticity /= (circles.count + 1);

			//total_vx = (total_vx * (mass - total_mass) + 2 * total_mass * total_vx) / (mass + total_mass);
			//total_vy = (total_vy * (mass - total_mass) + 2 * total_mass * total_vy) / (mass + total_mass);

			return new OrderedPair(total_vx, total_vy);
		}

		/// <summary>
		/// This circles move method.  Updates the velocity, checks for collisions then updates the (x,y) coordinates.
		/// </summary>
		/// <param name="time">For some reason a silly keycollection of circles, change me.</param>
		public void move(double time)
		{
			if (target != null)
			{
				 faceTarget();
			}
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
			//Out.WriteLine("Drawing in circle " + position);
            
			g.FillEllipse(brush, (float)(position.x - radius), (float)(position.y - radius), (float)(2 * radius), (float)(2 * radius));
		}

		/// <summary>
		/// This circles to string method.
		/// </summary>
		/// <returns>
		/// (523, 316) radius: 5 mass: 1
		/// velocity: 0 (0, 0): 0
		/// acceleration: 0 (0, 0): 0
		/// target: [0, 0
		/// color: Color [Chartreuse]
		/// </returns>
		public String toString()
		{
			return "radius: " + radius + ", mass: " + mass + "\n" +
				"position: " + position + "\n" +
				"velocity: " + velocity + "\n" +
				"acceleration: " + acceleration + "\n" +
				"target: " + target + "[" + min_dist + ", " + max_dist + "]\n" +
				"color: " + color;
		}
	}
}

// need to handle following differently
// make crossing check if following target first, if so, update velocity first
