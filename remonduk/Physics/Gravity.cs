using System;

namespace Remonduk.Physics
{
	/// <summary>
	/// Sub class of force that emulates gravity.
	/// </summary>
	public class Gravity : Force
	{
		public const double GRAVITY = 9.8;
		public const double ANGLE = Math.PI / 2;
		public const double G = .0000000000667384;

		public Gravity(double fx, double fy)
			: base(
				delegate(Circle first, Circle second)
				{
					return new OrderedPair(fx, fy);
				}
			)
		{ }

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
	}
}