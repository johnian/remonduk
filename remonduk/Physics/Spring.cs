using System;

namespace Remonduk.Physics
{
	public class Spring : Force
	{
		/// <summary>
		/// Default k value.
		/// </summary>
		public const double K = .002;
		/// <summary>
		/// Default c value.
		/// </summary>
		public const double C = K / 2;

		/// <summary>
		/// Constructor that creates a spring that holds two circles
		/// at a distance such that they are just barely touching.
		/// </summary>
		/// <param name="k">The value that determines the spring's strength.</param>
		/// <param name="c">The value that determines the damping strength.</param>
		public Spring(double k = K, double c = C)
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

		/// <summary>
		/// Constructor that creates a spring that holds two circles
		/// at a distance specified by the equilibrium value.
		/// </summary>
		/// <param name="k">The value that determines the spring's strength.</param>
		/// <param name="c">The value that determines the damping strength.</param>
		/// <param name="equilibrium">The distance to hold the two circles at.</param>
		public Spring(double k, double c, double equilibrium)
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

		/// <summary>
		/// Constructor that creates a spring that holds two circles
		/// at a distance specified by the equilibrium value,
		/// and breaks when the distance between the two circles
		/// exceeds the max value.
		/// </summary>
		/// <param name="k">The value that determines the spring's strength.</param>
		/// <param name="c">The value that determines the damping strength.</param>
		/// <param name="equilibrium">The distance to hold the two circles at.</param>
		/// <param name="max">The max distance the spring can be stretched before breaking.</param>
		public Spring(double k, double c, double equilibrium, double max)
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
