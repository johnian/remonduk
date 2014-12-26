﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace remonduk
{
	public class Tether : Force
	{
		public const double K = 2;
		public const double EQUILIBRIUM = 3;

		public Tether()
			: this(K, EQUILIBRIUM) { }

		public Tether(double k, double equilibrium)
			: base(
				delegate(Circle first, Circle second)
				{
					double dist = first.distance(second);
					if (dist > equilibrium)
					{
						dist -= equilibrium;
						double f = k * dist;

						double delta_x = second.x - first.x;
						double delta_y = second.y - first.y;
						double angle = Circle.angle(delta_y, delta_x);

						double fx = f * Math.Cos(angle);
						double fy = f * Math.Sin(angle);
						return Tuple.Create(fx, fy);
					}
					else
					{
						return Tuple.Create(0.0, 0.0);
					}
				}
			)
		{ }
	}

	//public class Tether
	//{
	//	public Circle c1, c2;
	//	public double max_dist;
	//	public double k;

	//	public Tether(Circle c1, Circle c2, double max_dist, double k)
	//	{
	//		this.c1 = c1;
	//		this.c2 = c2;
	//		this.max_dist = max_dist;
	//		this.k = k;
	//	}

	//	public void pull()
	//	{
	//		double delta_x = c1.x - c2.x;
	//		double delta_y = c1.y - c2.y;
	//		double dist = Math.Sqrt(delta_x * delta_x + delta_y * delta_y);
    //		Out.WriteLine("Distance = " + dist);
	//		double accel1 = k * (dist - max_dist) - c1.acceleration;
	//		double accel2 = k * (dist - max_dist) - c2.acceleration;
	//		if(dist > max_dist)
	//		{
	//			double theta1 = Math.Atan2(-delta_y, -delta_x);
	//			double theta2 = Math.Atan2(delta_y, delta_x);

	//			//tc1.setAcceleration(k * (dist - max_dist), theta1);
	//			//tc2.setAcceleration(k * (dist - max_dist), theta2);
	//			if (accel1 < 0)
	//			{
	//				c1.vx += Math.Cos(theta2) * Math.Abs(k * (dist - max_dist));
	//				c1.vy += Math.Sin(theta2) * Math.Abs(k * (dist - max_dist));
	//				c1.velocity_angle = theta2;
	//				//c1.updateAcceleration(Math.Abs(k * (dist - max_dist)), theta2);
	//			}
	//			else
	//			{
	//				c1.vx += Math.Cos(theta1) * Math.Abs(k * (dist - max_dist));
	//				c1.vy += Math.Sin(theta1) * Math.Abs(k * (dist - max_dist));
	//				c1.velocity_angle = theta1;
	//				//c1.updateAcceleration(Math.Abs(k * (dist - max_dist)), theta1);
	//			}

	//			if (accel2 < 0)
	//			{
	//				c2.vx += Math.Cos(theta1) * Math.Abs(k * (dist - max_dist));
	//				c2.vy += Math.Sin(theta1) * Math.Abs(k * (dist - max_dist));
	//				c2.velocity_angle = theta1;
	//				//c2.updateAcceleration(Math.Abs(k * (dist - max_dist)), theta1);
	//			}
	//			else
	//			{
	//				c2.vx += Math.Cos(theta2) * Math.Abs(k * (dist - max_dist));
	//				c2.vy += Math.Sin(theta2) * Math.Abs(k * (dist - max_dist));
	//				c2.velocity_angle = theta2;
	//				//c2.updateAcceleration(Math.Abs(k * (dist - max_dist)), theta2);
	//			}

    //			Out.WriteLine("TETHERING");
	//		}
	//		else
	//		{
	//			c1.setAcceleration(0, 0);
	//			c2.setAcceleration(0, 0);
	//		}
	//	}
	//}
}
