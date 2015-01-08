﻿using Remonduk.QuadTreeTest;
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
		/// <param name="point"></param>
		/// <param name="referenceV"></param>
		/// <param name="distanceFromCollision"></param>
		/// <returns></returns>
		public OrderedPair CollisionPoint(OrderedPair point, OrderedPair referenceV,
			double distanceFromCollision)
		{
			double referenceVelocity = referenceV.Magnitude();
			double collisionX = point.X - referenceV.X * distanceFromCollision / referenceVelocity;
			double collisionY = point.Y - referenceV.Y * distanceFromCollision / referenceVelocity;
			return new OrderedPair(collisionX, collisionY);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="collisionPoint"></param>
		/// <param name="referenceV"></param>
		/// <returns></returns>
		public double TimeTo(OrderedPair collisionPoint, OrderedPair referenceV)
		{
			OrderedPair distance = new OrderedPair(collisionPoint.X - Px, collisionPoint.Y - Py);
			if (distance.X == 0 && distance.Y == 0)
			{
				return 0;
			}
			if ((distance.X != 0 && referenceV.X == 0) ||
				(distance.Y != 0 && referenceV.Y == 0))
			{
				return Double.PositiveInfinity;
			}
			double timeX = (referenceV.X == 0) ? 0 : distance.X / referenceV.X;
			double timeY = (referenceV.Y == 0) ? 0 : distance.Y / referenceV.Y;
			return (timeX > timeY) ? timeX : timeY;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="point"></param>
		/// <param name="referenceV"></param>
		/// <param name="distanceFromCollision"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public double CollisionTime(OrderedPair point, OrderedPair referenceV,
			double distanceFromCollision, double time)
		{
			OrderedPair collisionPoint = CollisionPoint(point, referenceV, distanceFromCollision);
			double t = TimeTo(collisionPoint, referenceV);
			if (t >= 0 && t <= time)
			{
				return t;
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

			return CollisionTime(point, referenceV, distanceFromCollision, time);
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
