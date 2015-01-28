using System;

namespace Remonduk.Physics
{
	/// <summary>
	/// Sub class of force that emulates gravity.
	/// </summary>
	public class Gravity : Force
	{
		/// <summary>
		/// Default value for the g constant in the force formula.
		/// </summary>
		public const double G = .0000000000667384;

		/// <summary>
		/// Default x component of gravity force.
		/// </summary>
		public const double FX = 0;
		/// <summary>
		/// Default y component of gravity force.
		/// </summary>
		public const double FY = 1;

		/// <summary>
		/// Default constructor that calls the two arg constructor with defaults.
		/// </summary>
		public Gravity() : this(FX, FY) { }

		/// <summary>
		/// Constructor for creating a gravity force that changes with distance.
		/// </summary>
		/// <param name="g"></param>
		public Gravity(double g)
			: base(
				delegate(Circle first, Circle second)
				{
					double delta_x = second.Px - first.Px;
					double delta_y = second.Py - first.Py;
					double r = OrderedPair.Magnitude(delta_x, delta_y);
					double angle = OrderedPair.Angle(delta_x, delta_y);


					double f = g * first.Mass * second.Mass / (r * r);
					double fx = f * Math.Cos(angle);
					double fy = f * Math.Sin(angle);
					return new OrderedPair(fx, fy);
				}
			)
		{ }

		/// <summary>
		/// Constructor for creating a constant gravity force.
		/// </summary>
		/// <param name="fx">The x component of the gravity force.</param>
		/// <param name="fy">The y component of the gravity force.</param>
		public Gravity(double fx, double fy)
			: base(
				delegate(Circle first, Circle second)
				{
					return new OrderedPair(fx, fy);
				}
			)
		{ }
	}
}