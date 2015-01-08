using Remonduk.QuadTreeTest;
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
		/// <param name="that"></param>
		/// <returns></returns>
		public double DistanceSquared(Circle that)
		{
			return Position.MagnitudeSquared(that.Position);
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
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public OrderedPair ReferenceVelocity(Circle that, double time)
		{
			OrderedPair thisV = NextVelocity(time);
			OrderedPair thatV = that.NextVelocity(time);
			double referenceVx = thisV.X - thatV.X;
			double referenceVy = thisV.Y - thatV.Y;
			return new OrderedPair(referenceVx, referenceVy);
		}

		/// <summary>
		/// Calculates the closest point on the this' movement vector to that.
		/// </summary>
		/// <param name="thatX">That circles x value.</param>
		/// <param name="thatY">That circles y value</param>
		/// <param name="referenceVx">This circles reference vx.</param>
		/// <param name="referenceVy">This circles reference vy.</param>
		/// <returns>The closest point on this' movement vector to thatX and thatY</returns>
		public OrderedPair ClosestPoint(Circle that, OrderedPair referenceV)
		{
			double thisConstant = referenceV.Y * Px - referenceV.X * Py;
			double thatConstant = referenceV.X * that.Px + referenceV.Y * that.Py;
			double determinant = referenceV.X * referenceV.X + referenceV.Y * referenceV.Y;

			if (determinant == 0)
			{
				return new OrderedPair(Px, Py);
			}
			double intersectionX = (thisConstant * referenceV.Y + thatConstant * referenceV.X) / determinant;
			double intersectionY = (thatConstant * referenceV.Y - thisConstant * referenceV.X) / determinant;
			return new OrderedPair(intersectionX, intersectionY);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="referenceVelocity"></param>
		/// <param name="distance"></param>
		/// <returns></returns>
		public double TimeTo(double referenceV, double distance)
		{
			if (referenceV == 0)
			{
				if (Math.Round(distance, Constants.PRECISION) == 0)
				{
					return 0;
				}
				return Double.PositiveInfinity;
			}
			return distance / referenceV;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="distanceFromCollision"></param>
		/// <param name="referenceVx"></param>
		/// <param name="referenceVy"></param>
		/// <param name="point"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public double CollisionTime(double distanceFromCollision,
			OrderedPair referenceV, OrderedPair point, double time)
		{
			double referenceVelocity = OrderedPair.Magnitude(referenceV.X, referenceV.Y);
			double collisionX = point.X - referenceV.X * distanceFromCollision / referenceVelocity;
			double collisionY = point.Y - referenceV.X * distanceFromCollision / referenceVelocity;

			double timeX = TimeTo(referenceV.X, collisionX - Px);
			double timeY = TimeTo(referenceV.Y, collisionY - Py);

			if (timeX >= 0 && timeX <= time &&
				timeY >= 0 && timeY <= time)
			{
				//Out.WriteLine("(" + timeX + ", " + timeY + ")");
				timeX = Math.Round(timeX, Constants.PRECISION);
				timeY = Math.Round(timeY, Constants.PRECISION);
				return (timeX > timeY) ? timeX : timeY;
			}
			return Double.PositiveInfinity;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public double Crossing(Circle that, double time)
		{
			OrderedPair referenceV = ReferenceVelocity(that, time);
			OrderedPair point = ClosestPoint(that, referenceV);

			if (point == null)
			{
				return Double.PositiveInfinity;
			}
			//double distanceFromThat = OrderedPair.Magnitude(point.X - that.Px, point.Y - that.Py);
			double distanceFromThat = point.Magnitude(that.Position);
			double radiiSum = that.Radius + Radius;
			double distanceFromCollision = Math.Sqrt(radiiSum * radiiSum - distanceFromThat * distanceFromThat);

			return CollisionTime(distanceFromCollision, referenceV, point, time);
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
					OrderedPair thisNext = NextPosition(time);
					OrderedPair thatNext = that.NextPosition(time);
					double newDistance = thisNext.Magnitude(thatNext);

					if (newDistance < oldDistance)
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

		public bool Intersects(FRect rect)
		{
			return (rect.Left < Px + Radius &&
					rect.Right > Px - Radius &&
					rect.Top < Py + Radius &&
					rect.Bottom > Py - Radius);
		}
	}
}
