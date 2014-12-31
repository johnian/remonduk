using System;

namespace remonduk
{
	public class Gravity : Force
	{
		public const double GRAVITY = 9.8;
		public const double ANGLE = Math.PI / 2;

		public const double G = .0000000000667384;

		
		public Gravity(double gravity, double angle)
			: base(
				delegate(Circle first, Circle second)
				{
					double ax = gravity * Math.Cos(angle);
					double ay = gravity * Math.Sin(angle);
					return new OrderedPair(ax, ay);
				}
			)
		{ }

		public Gravity(double g)
			: base(
				delegate(Circle first, Circle second)
				{
					double delta_x = second.px - first.px;
					double delta_y = second.py - first.py;
					double r = OrderedPair.magnitude(delta_x, delta_y);
					double angle = OrderedPair.angle(delta_y, delta_x);

					
					double f = g * first.mass * second.mass / (r * r);
					double fx = f * Math.Cos(angle);
					double fy = f * Math.Sin(angle);
					return new OrderedPair(fx, fy);
				}
			)
		{ }
	}
}