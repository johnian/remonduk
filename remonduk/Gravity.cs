using System;

namespace remonduk
{
	public class Gravity : Force
	{
		public const double G = .0000000000667384;
		//public const double G = 6.67384 * Math.Pow(10, -11);

		public const double GRAVITY = 9.8;
		public const double ANGLE = Math.PI / 2;
		
		public Gravity(double gravity, double angle)
			: base(
				delegate(Circle first, Circle second)
				{
					double ax = gravity * Math.Cos(angle);
					double ay = gravity * Math.Sin(angle);
					return Tuple.Create(ax, ay);
				}
			)
		{ }

		public Gravity(double g)
			: base(
				delegate(Circle first, Circle second)
				{
					double delta_x = second.x - first.x;
					double delta_y = second.y - first.y;
					double r = Circle.magnitude(delta_x, delta_y);
					double angle = Circle.angle(delta_y, delta_x);

					
					double f = g * first.mass * second.mass / (r * r);
					double fx = f * Math.Cos(angle);
					double fy = f * Math.Sin(angle);
					return Tuple.Create(fx, fy);
				}
			)
		{ }
	}
}