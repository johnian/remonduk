using System;

namespace Remonduk.Physics
{
	public class Tether : Force
	{
		public const double K = .002;
		public const double C = K / 2;

		public Tether(double k = K, double c = C)
			: base(
				delegate(Circle first, Circle second)
				{
					double equilibrium = first.Radius + second.Radius;
					double angle = first.Position.Angle(second.Position);
					double ex = equilibrium * Math.Cos(angle);
					double ey = equilibrium * Math.Sin(angle);

					double fx = k * (second.Px - first.Px - ex) - c * (first.Vx - second.Vx);
					double fy = k * (second.Py - first.Py - ey) - c * (first.Vy - second.Vy);
					return new OrderedPair(fx, fy);
				}
			)
		{ }

		public Tether(double k, double c, double equilibrium)
			: base(
				delegate(Circle first, Circle second)
				{
					double angle = first.Position.Angle(second.Position);
					double ex = equilibrium * Math.Cos(angle);
					double ey = equilibrium * Math.Sin(angle);

					double fx = k * (second.Px - first.Px - ex) - c * (first.Vx - second.Vx);
					double fy = k * (second.Py - first.Py - ey) - c * (first.Vy - second.Vy);
					return new OrderedPair(fx, fy);
				}
			)
		{ }

		public Tether(double k, double c, double equilibrium, double max)
			: base(
				delegate(Circle first, Circle second)
				{
					if (first.Distance(second) > max)
					{
						return null;
					}
					double angle = first.Position.Angle(second.Position);
					double ex = equilibrium * Math.Cos(angle);
					double ey = equilibrium * Math.Sin(angle);

					double fx = k * (second.Px - first.Px - ex) - c * (first.Vx - second.Vx);
					double fy = k * (second.Py - first.Py - ey) - c * (first.Vy - second.Vy);
					return new OrderedPair(fx, fy);
				}
			)
		{ }
	}
}
