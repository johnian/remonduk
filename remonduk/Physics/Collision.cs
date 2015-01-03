using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remonduk.Physics
{
	public partial class Circle
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		public double DistanceSquared(Circle that)
		{
			double deltaX = that.Px - Px;
			double deltaY = that.Py - Py;
			return OrderedPair.MagnitudeSquared(deltaX, deltaY);
		}

		/// <summary>
		/// Calculates the distance from this circle to that circle.
		/// </summary>
		/// <param name="other">The other circle to calculate distance to.</param>
		/// <returns>The distance to the other circle.</returns>
		public double Distance(Circle that)
		{
			return Position.Magnitude(that.Position);
		}

		/// <summary>
		/// Calculates the closest point on the this' movement vector to that.
		/// </summary>
		/// <param name="that_x">That circles x value.</param>
		/// <param name="that_y">That circles y value</param>
		/// <param name="reference_vx">This circles reference vx.</param>
		/// <param name="reference_vy">This circles reference vy.</param>
		/// <returns>The closest point on this' movement vector to that_x and that_y</returns>
		public OrderedPair ClosestPoint(double that_x, double that_y,
			double reference_vx, double reference_vy)
		{
			double constant_1 = reference_vy * Px - reference_vx * Py;
			double constant_2 = reference_vx * that_x + reference_vy * that_y;

			double determinant = reference_vx * reference_vx + reference_vy * reference_vy;

			double intersection_x;
			double intersection_y;
			if (determinant == 0)
			{
				intersection_x = Px;
				intersection_y = Py;
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
		public double Crossing(Circle that, double time)
		{
			double this_vx = Acceleration.X * time / 2 + Velocity.X;
			double this_vy = Acceleration.Y * time / 2 + Velocity.Y;
			double that_vx = that.Acceleration.X * time / 2 + that.Velocity.X;
			double that_vy = that.Acceleration.Y * time / 2 + that.Velocity.Y;
			double reference_vx = this_vx - that_vx;
			double reference_vy = this_vy - that_vy;

			OrderedPair point = ClosestPoint(that.Position.X, that.Position.Y, reference_vx, reference_vy);
			if (point == null)
			{
				return Double.PositiveInfinity;
			}
			double distance_from_that = OrderedPair.Magnitude(point.X - that.Position.X, point.Y - that.Position.Y);
			double radii_sum = that.Radius + Radius;

			if (distance_from_that > radii_sum)
			{
				return Double.PositiveInfinity;
			}
			else
			{
				double distance_from_collision_squared = radii_sum * radii_sum - distance_from_that * distance_from_that;
				double distance_from_collision = Math.Sqrt(distance_from_collision_squared);

				double reference_velocity = OrderedPair.Magnitude(reference_vx, reference_vy);
				double collision_x = point.X - distance_from_collision * reference_vx / reference_velocity;
				double collision_y = point.Y - distance_from_collision * reference_vy / reference_velocity;
				double time_x;
				if (reference_vx == 0)
				{
					if (collision_x == Px) time_x = 0;
					else time_x = Double.PositiveInfinity;
				}
				else
				{
					time_x = (collision_x - Px) / reference_vx;
				}
				double time_y;
				if (reference_vy == 0)
				{
					if (collision_y == Py) time_y = 0;
					else time_y = Double.PositiveInfinity;
				}
				else
				{
					time_y = (collision_y - Py) / reference_vy;
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
		public double Colliding(Circle that, double time)
		{
			if (DistanceSquared(that) <= (that.Radius + Radius) * (that.Radius + Radius))
			{
				if (that != this)
				{
					double old_distance = Distance(that);

					double this_px = Ax / 2 + Vx + Px;
					double this_py = Ay / 2 + Vy + Py;
					double that_px = that.Ax / 2 + that.Vx + that.Px;
					double that_py = that.Ay / 2 + that.Vy + that.Py;
					double new_distance = OrderedPair.Magnitude(that_px - this_px, that_py - this_py);

					if (new_distance < old_distance)
					{
						return 0;
					}
				}
				return Double.PositiveInfinity;
			}
			return Crossing(that, time);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		public bool Colliding(Circle that)
		{
			return (DistanceSquared(that) <= (that.Radius + Radius) * (that.Radius + Radius));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="circles"></param>
		/// <returns></returns>
		public OrderedPair CollideWith(List<Circle> circles)
		{
			double total_vx = 0;
			double total_vy = 0;
			foreach (Circle that in circles)
			{
				if (that == this)
				{
					continue;
				}

				total_vx += (Vx * (Mass - that.Mass) + 2 * that.Vx * that.Mass) / (Mass + that.Mass);
				total_vy += (Vy * (Mass - that.Mass) + 2 * that.Vy * that.Mass) / (Mass + that.Mass);
				// modify for elasticity
				// use average elasticity between two Colliding objects for simplicity
				// try to derive a formula for it
			}
			return new OrderedPair(total_vx, total_vy);
		}
	}
}
