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
		/// Default offset value.
		/// </summary>
		public const double OFFSET = 0;

		/// <summary>
		/// Constructor that creates a spring that holds two circles
		/// at a distance such that they are just barely touching.
		/// </summary>
		/// <param name="k">The value that determines the spring strength.</param>
		/// <param name="c">The value that determines the damping strength.</param>
		/// <param name="offset">The distance to keep between the two circles.</param>
		public Spring(double k = K, double c = C, double offset = OFFSET)
			: base(
				delegate(Circle first, Circle second)
				{
					if (first.Position.Equals(second.Position)) {
						return new OrderedPair(0, 0);
					}
					double distance = first.Distance(second) - first.Radius - second.Radius;
					distance /= 2;
					distance -= offset;
					double angle = first.Position.Angle(second.Position);

					double fx = k * distance * Math.Cos(angle) - c * (first.Vx - second.Vx);
					double fy = k * distance * Math.Sin(angle) - c * (first.Vy - second.Vy);
					return new OrderedPair(fx, fy);
				}
			)
		{ }

		/// <summary>
		/// Constructor that creates a spring that holds two circles
		/// at a distance specified by the offset value,
		/// and breaks when the distance between the two circles
		/// exceeds the max value.
		/// </summary>
		/// <param name="k">The value that determines the spring strength.</param>
		/// <param name="c">The value that determines the damping strength.</param>
		/// <param name="offset">The distance to keep between the two circles.</param>
		/// <param name="max">The max distance the spring can be stretched before breaking.</param>
		public Spring(double k, double c, double offset, double max)
			: base(
				delegate(Circle first, Circle second)
				{
					if (first.Distance(second) > max)
					{
						return null;
					}
					if (first.Position.Equals(second.Position)) {
						return new OrderedPair(0, 0);
					}
					double distance = first.Distance(second) - first.Radius - second.Radius;
					distance /= 2;
					distance -= offset;
					double angle = first.Position.Angle(second.Position);

					double fx = k * distance * Math.Cos(angle) - c * (first.Vx - second.Vx);
					double fy = k * distance * Math.Sin(angle) - c * (first.Vy - second.Vy);
					return new OrderedPair(fx, fy);
				}
			)
		{ }
	}
}
