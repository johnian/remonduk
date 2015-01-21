using System;
using System.Collections.Generic;

namespace Remonduk.Physics
{
	/// <summary>
	/// Collisions is a part of the Circle class.
	/// Any functionality related to collisions can be found here.
	/// </summary>
	public partial class Circle
	{
		/// <summary>
		/// Returns the circle's next velocity if it were to be updated by the time step.
		/// </summary>
		/// <param name="time">The time step by which to update the velocity.</param>
		/// <returns>The circle's next velocity.</returns>
		public OrderedPair NextVelocity(double time = PhysicalSystem.TIME_STEP)
		{
			double thisVx = Ax * time + Vx;
			double thisVy = Ay * time + Vy;
			return new OrderedPair(thisVx, thisVy);
		}

		/// <summary>
		/// Returns the circle's next position if it were to be updated by the time step.
		/// </summary>
		/// <param name="time">The time step by which to update the position.</param>
		/// <returns>The circle's next position.</returns>
		public OrderedPair NextPosition(double time = PhysicalSystem.TIME_STEP)
		{
			double potentialX = Ax * time * time / 2 + Vx * time + Px;
			double potentialY = Ay * time * time / 2 + Vy * time + Py;
			return new OrderedPair(potentialX, potentialY);
		}

		/// <summary>
		/// Returns the distance squared from this circle to that.
		/// </summary>
		/// <param name="that">The circle used as the end point for the calculation.</param>
		/// <returns>The distance squared from this circle to that.</returns>
		public double DistanceSquared(Circle that)
		{
			return Position.MagnitudeSquared(that.Position);
		}

		/// <summary>
		/// Returns the distance from this circle to that.
		/// </summary>
		/// <param name="that">The circle used as the end point for the calculation.</param>
		/// <returns>The distance from this circle to that.</returns>
		public double Distance(Circle that)
		{
			return Position.Magnitude(that.Position);
		}

		/// <summary>
		/// Returns the time till the center of this circle reaches the collision point.
		/// </summary>
		/// <param name="collisionPoint">The center point of this circle when it first collides with that circle.</param>
		/// <param name="referenceV">The reference velocity of this circle.</param>
		/// <returns>The time till the center of this circle reaches the collision point.
		/// Returns positive infinity if this circle cannot reach the collision point.</returns>
		public double TimeTill(OrderedPair collisionPoint, OrderedPair referenceV)
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
			if (timeX < 0 || timeY < 0)
			{
				return Double.PositiveInfinity;
			}
			return (timeX > timeY) ? timeX : timeY;
		}

		/// <summary>
		/// Returns the center point of this circle when it first collides with that circle.
		/// </summary>
		/// <param name="closest">The closest point from that circle to this circle's movement vector.</param>
		/// <param name="referenceV">The reference velocity of this circle.</param>
		/// <param name="that">The circle that is considered motionless in reference to this.</param>
		/// <returns>The center point of this circle when it first collides with that circle.</returns>
		public OrderedPair CollisionPoint(OrderedPair closest, OrderedPair referenceV, Circle that)
		{
			//double distanceFromThat = OrderedPair.Magnitude(point.X - that.Px, point.Y - that.Py);

			double distanceFromThat = closest.Magnitude(that.Position);
			double radiiSum = that.Radius + Radius;
			double distanceFromCollision = Math.Sqrt(radiiSum * radiiSum - distanceFromThat * distanceFromThat);
			double referenceVelocity = referenceV.Magnitude();
			double collisionX = closest.X - referenceV.X * distanceFromCollision / referenceVelocity;
			double collisionY = closest.Y - referenceV.Y * distanceFromCollision / referenceVelocity;
			return new OrderedPair(collisionX, collisionY);
		}

		/// <summary>
		/// Returns the time this circle takes to collide with that circle bounded by a time step.
		/// </summary>
		/// <param name="closest">The intersection of this circle and that circle's movement vectors.</param>
		/// <param name="referenceV">The reference velocity of this circle.</param>
		/// <param name="that">The circle that this circle may be colliding with.</param>
		/// <param name="time">The time frame in which to check for a collision.</param>
		/// <returns>The time of collision between this and that.
		/// Returns Positive infinity if they do not collide in the time frame.</returns>
		public double CollisionTime(OrderedPair closest, OrderedPair referenceV,
			Circle that, double time)
		{
			OrderedPair collisionPoint = CollisionPoint(closest, referenceV, that);
			double t = TimeTill(collisionPoint, referenceV);
			return (t >= 0 && t <= time) ? t : Double.PositiveInfinity;
		}

		/// <summary>
		/// Returns the closest point from that circle to this circle's movement vector.
		/// </summary>
		/// <param name="that">The circle that is considered motionless in reference to this.</param>
		/// <param name="referenceV">The reference velocity of this circle.</param>
		/// <returns>The closest point from that circle to this circle's movement vector.</returns>
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
		/// Returns a reference velocity using this circle as a frame of reference.
		/// </summary>
		/// <param name="that">The circle that is considered motionless in reference to this.</param>
		/// <param name="time">The time step by which to update the two circles.</param>
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
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public double Crossing(Circle that, double time)
		{
			OrderedPair referenceV = ReferenceVelocity(that, time);
			if (referenceV.X == 0 && referenceV.Y == 0)
			{
				return Double.PositiveInfinity;
			}
			OrderedPair closest = ClosestPoint(that, referenceV);
			if (closest == null)
			{
				return Double.PositiveInfinity;
			}
			return CollisionTime(closest, referenceV, that, time);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public double Colliding(Circle that, double time)
		{
			if (that == this) return Double.PositiveInfinity;
			double oldDistance = DistanceSquared(that);
			if (oldDistance - (that.Radius + Radius) * (that.Radius + Radius) <= Constants.EPSILON)
			{
				OrderedPair thisNext = NextPosition(time);
				OrderedPair thatNext = that.NextPosition(time);
				double newDistance = thisNext.MagnitudeSquared(thatNext);
				if (newDistance - oldDistance > -Constants.EPSILON)
				{
					return Double.PositiveInfinity;
				}
				return 0;
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
			if (that == this) return false;
			return DistanceSquared(that) - (that.Radius + Radius) * (that.Radius + Radius) <= Constants.EPSILON;
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
				double totalV = that.Mass * that.Velocity.Magnitude() / Mass;

				double angle = that.Position.Angle(Position);
				totalVx += totalV * Math.Cos(angle);
				totalVy += totalV * Math.Sin(angle);
				// use average elasticity between two Colliding objects for simplicity
				// try to derive a formula for it
			}
			return new OrderedPair(totalVx, totalVy);
		}
	}
}
