using System;
using System.Drawing;

namespace Remonduk.Physics
{
	/// <summary>
	/// Circles are the atomic pieces of the game world.
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
		/// Default color.
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
		/// Shorthand for Position.X.
		/// </summary>
		public double Px { get { return Position.X; } }
		/// <summary>
		/// Shorthand for Position.Y.
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
		/// Flag for whether this circle can physically interact with other circles.
		/// </summary>
		public bool Exists;

		/// <summary>
		/// The circle's color.
		/// </summary>
		public Color Color;

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
		/// <param name="radius">The circle's radius.</param>
		/// <param name="mass">The circle's mass.</param>
		public Circle(double radius, double mass = MASS) :
			this(radius, PX, PY, mass) { }

		/// <summary>
		/// Creates a circle with the specified radius, position, and optional mass.
		/// </summary>
		/// <param name="radius">The circle's radius.</param>
		/// <param name="px">The circle's x position.</param>
		/// <param name="py">The circle's y position.</param>
		/// <param name="mass">The circle's mass.</param>
		public Circle(double radius, double px, double py, double mass = MASS) :
			this(radius, px, py, VX, VY, mass) { }

		/// <summary>
		/// Creates a circle with the specified radius, position, velocity, and optional mass.
		/// </summary>
		/// <param name="radius">The circle's radius.</param>
		/// <param name="px">The circle's x position.</param>
		/// <param name="py">The circle's y position.</param>
		/// <param name="vx">The circle's x velocity.</param>
		/// <param name="vy">The circle's y velocity.</param>
		/// <param name="mass">The circle's mass.</param>
		public Circle(double radius, double px, double py, double vx, double vy, double mass = MASS) :
			this(radius, px, py, vx, vy, AX, AY, mass) { }

		/// <summary>
		/// Creates a circle with the specified radius, position, velocity, acceleration,
		/// and optional mass.
		/// </summary>
		/// <param name="radius">The circle's radius.</param>
		/// <param name="px">The circle's x position.</param>
		/// <param name="py">The circle's y position.</param>
		/// <param name="vx">The circle's x velocity.</param>
		/// <param name="vy">The circle's y velocity.</param>
		/// <param name="ax">The circle's x acceleration.</param>
		/// <param name="ay">The circle's y acceleration.</param>
		/// <param name="mass">The circle's mass.</param>
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
		}

		/// <summary>
		/// Sets the circle's radius.
		/// </summary>
		/// <param name="radius">The new value for the circle's radius.</param>
		public void SetRadius(double radius)
		{
			if (radius < RADIUS)
			{
				throw new ArgumentException("radius: " + radius);
			}
			Radius = radius;
		}

		/// <summary>
		/// Set the circle's mass.
		/// </summary>
		/// <param name="mass">The new value for the circle's mass.</param>
		public void SetMass(double mass)
		{
			Mass = mass;
		}

		/// <summary>
		/// Sets the circle's position.
		/// </summary>
		/// <param name="px">The new value for the circle's x position.</param>
		/// <param name="py">The new value for the circle's y position.</param>
		public void SetPosition(double px, double py)
		{
			Position.SetXY(px, py);
		}

		/// <summary>
		/// Set the circle's velocity.
		/// </summary>
		/// <param name="vx">The new value for the circle's x velocity.</param>
		/// <param name="vy">The new value for the circle's y velocity.</param>
		public void SetVelocity(double vx, double vy)
		{
			Velocity.SetXY(vx, vy);
			//if (Target != null)
			//{
			//	FaceTarget();
			//}
		}

		/// <summary>
		/// Sets the circle's acceleration.
		/// </summary>
		/// <param name="ax">The new value for the circle's x acceleration.</param>
		/// <param name="ay">The new value for the circle's y acceleration.</param>
		public void SetAcceleration(double ax, double ay)
		{
			Acceleration.SetXY(ax, ay);
		}

		/// <summary>
		/// Returns the circle's next velocity if it were to be updated by the time step.
		/// </summary>
		/// <param name="time">The time step by which to update the velocity.</param>
		/// <returns>The circle's next velocity.</returns>
		public OrderedPair NextVelocity(double time = PhysicalSystem.TIME_STEP)
		{
			double nextX = Ax * time + Vx;
			double nextY = Ay * time + Vy;
			return new OrderedPair(nextX, nextY);
		}

		/// <summary>
		/// Updates the circle's velocity by its acceleration based on the time step.
		/// </summary>
		/// <param name="time">The time step by which the velocity should update.</param>
		public void UpdateVelocity(double time)
		{
			OrderedPair nextVelocity = NextVelocity(time);
			SetVelocity(nextVelocity.X, nextVelocity.Y);
		}

		/// <summary>
		/// Returns the circle's next position if it were to be updated by the time step.
		/// </summary>
		/// <param name="time">The time step by which to update the position.</param>
		/// <returns>The circle's next position.</returns>
		public OrderedPair NextPosition(double time = PhysicalSystem.TIME_STEP)
		{
			double nextX = (Ax * time / 2 + Vx) * time + Px;
			double nextY = (Ay * time / 2 + Vy) * time + Py;
			return new OrderedPair(nextX, nextY);
		}

		/// <summary>
		/// Updates the circle's position by its velocity and
		/// acceleration based on the time step.
		/// </summary>
		/// <param name="time">The time step by which the position should update.</param>
		public void UpdatePosition(double time)
		{
			OrderedPair nextPosition = NextPosition(time);
			SetPosition(nextPosition.X, nextPosition.Y);
		}

		/// <summary>
		/// Updates the circle's position and velocity based on the time step.
		/// </summary>
		/// <param name="time">The time step by which the circle should update.</param>
		public void Update(double time)
		{
			UpdatePosition(time);
			UpdateVelocity(time);
			// UpdateVelocity
			// UpdateAcceleration
		}

		/// <summary>
		/// Draws the circle.
		/// </summary>
		/// <param name="graphics">The graphics object upon which the circle will be drawn.</param>
		public void Draw(Graphics graphics)
		{
			Brush brush = new SolidBrush(Color.Chartreuse);
			graphics.FillEllipse(brush, (float)(Px - Radius), (float)(Py - Radius), (float)(2 * Radius), (float)(2 * Radius));
		}

		//public bool Contains(OrderedPair point)
		//{
		//	return (point.X < Px + Radius &&
		//			point.X > Px - Radius &&
		//			point.Y < Py + Radius &&
		//			point.Y > Py - Radius);
		//}

		/// <summary>
		/// Returns the circle's data as a string.
		/// </summary>
		/// <returns>The circle's data.</returns>
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
		/// Returns whether two circles are the same object.
		/// </summary>
		/// <param name="obj">The circle to check.</param>
		/// <returns>Whether the circles are the same object.</returns>
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>
		/// Returns the base hash code of the object.
		/// </summary>
		/// <returns>The base hash code of the object.</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}