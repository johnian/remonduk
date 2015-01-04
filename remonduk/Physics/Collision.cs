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
		private const int PRECISION = 1;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public OrderedPair NextVelocity(double time)
		{
			double thisVx = Ax * time + Vx;
			double thisVy = Ay * time + Vy;
			return new OrderedPair(thisVx, thisVy);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public OrderedPair NextPosition(double time)
		{
			double potentialX = Ax * time * time / 2 + Vx * time + Px;
			double potentialY = Ay * time * time / 2 + Vy * time + Py;
			return new OrderedPair(potentialX, potentialY);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public OrderedPair NextPosition()
		{
			double potentialX = Ax / 2 + Vx + Px;
			double potentialY = Ay / 2 + Vy + Py;
			return new OrderedPair(potentialX, potentialY);
		}

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
		/// <param name="that">The other circle to calculate distance to.</param>
		/// <returns>The distance to the other circle.</returns>
		public double Distance(Circle that)
		{
			return Position.Magnitude(that.Position);
		}

		/// <summary>
		/// Calculates the closest point on the this' movement vector to that.
		/// </summary>
		/// <param name="thatX">That circles x value.</param>
		/// <param name="thatY">That circles y value</param>
		/// <param name="referenceVx">This circles reference vx.</param>
		/// <param name="referenceVy">This circles reference vy.</param>
		/// <returns>The closest point on this' movement vector to thatX and thatY</returns>
		public OrderedPair ClosestPoint(double thatX, double thatY,
			double referenceVx, double referenceVy)
		{
			double thisConstant = referenceVy * Px - referenceVx * Py;
			double thatConstant = referenceVx * thatX + referenceVy * thatY;
			double determinant = referenceVx * referenceVx + referenceVy * referenceVy;

			if (determinant == 0)
			{
				return new OrderedPair(Px, Py);
			}
			else
			{
				double intersectionX = (thisConstant * referenceVy + thatConstant * referenceVx) / determinant;
				double intersectionY = (thatConstant * referenceVy - thisConstant * referenceVx) / determinant;
				return new OrderedPair(intersectionX, intersectionY);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public double Crossing(Circle that, double time)
		{
			OrderedPair thisV = NextVelocity(time);
			OrderedPair thatV = that.NextVelocity(time);
			double referenceVx = thisV.X - thatV.X;
			double referenceVy = thisV.X - thatV.Y;

			OrderedPair point = ClosestPoint(that.Px, that.Py, referenceVx, referenceVy);
			if (point == null)
			{
				return Double.PositiveInfinity;
			}
			//double distanceFromThat = OrderedPair.Magnitude(point.X - that.Px, point.Y - that.Py);
			double distanceFromThat = point.Magnitude(that.Position);
			double radiiSum = that.Radius + Radius;
			double distanceFromCollision = Math.Sqrt(radiiSum * radiiSum - distanceFromThat * distanceFromThat);

			return CollisionTime(distanceFromCollision, referenceVx, referenceVy, point, time);
		}

		private double CollisionTime(double distanceFromCollision,
			double referenceVx, double referenceVy, OrderedPair point, double time)
		{
			double referenceVelocity = OrderedPair.Magnitude(referenceVx, referenceVy);
			double collisionX = point.X - referenceVx * distanceFromCollision / referenceVelocity;
			double collisionY = point.Y - referenceVy * distanceFromCollision / referenceVelocity;

			double timeX = TimeTo(referenceVx, collisionX - Px);
			double timeY = TimeTo(referenceVy, collisionY - Py);

			if (timeX >= 0 && timeX <= time &&
				timeY >= 0 && timeY <= time)
			{
				//Out.WriteLine("(" + timeX + ", " + timeY + ")");
				timeX = Math.Round(timeX, PRECISION);
				timeY = Math.Round(timeY, PRECISION);
				return (timeX > timeY) ? timeX : timeY;
			}
			return Double.PositiveInfinity;
		}

		private double TimeTo(double referenceVelocity, double distance)
		{
			if (referenceVelocity == 0)
			{
				if (Math.Round(distance, PRECISION) == 0)
				{
					return 0;
				}
				return Double.PositiveInfinity;
			}
			else
			{
				return distance / referenceVelocity;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="circles"></param>
		/// <returns></returns>
		public OrderedPair CollideWith(List<Circle> circles)
		{
			double totalVx = 0;
			double totalVy = 0;
			foreach (Circle that in circles)
			{
				if (that == this) continue;

				totalVx += (Vx * (Mass - that.Mass) + 2 * that.Vx * that.Mass) / (Mass + that.Mass);
				totalVy += (Vy * (Mass - that.Mass) + 2 * that.Vy * that.Mass) / (Mass + that.Mass);
				// modify for elasticity
				// use average elasticity between two Colliding objects for simplicity
				// try to derive a formula for it
			}
			return new OrderedPair(totalVx, totalVy);
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
					double oldDistance = Distance(that);

					double thisPx = Ax / 2 + Vx + Px;
					double thisPy = Ay / 2 + Vy + Py;
					double thatPx = that.Ax / 2 + that.Vx + that.Px;
					double thatPy = that.Ay / 2 + that.Vy + that.Py;
					double newDistance = OrderedPair.Magnitude(thatPx - thisPx, thatPy - thisPy);

					if (newDistance < oldDistance)
					{
						//Out.WriteLine("overlapping collision: " + GetHashCode() + " ~ " + that.GetHashCode());
						return 0;
					}
					return 0;
				}
				return Double.PositiveInfinity;
			}
			double value = Crossing(that, time);
			if (!Double.IsInfinity(value)) {
				//Out.WriteLine("colliding collision: " + GetHashCode() + " ~ " + that.GetHashCode());
			}
			//return Crossing(that, time);
			return value;
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
	}
}
