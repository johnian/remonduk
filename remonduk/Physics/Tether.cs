using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remonduk.Physics
{
	public class Tether : Force
	{
		public const double K = 2;
		public const double EQUILIBRIUM = 20;

		public Tether()
			: this(K, EQUILIBRIUM) { }

		public Tether(double k, double equilibrium)
			: base(
				delegate(Circle first, Circle second)
				{
					equilibrium = first.Radius + second.Radius;
					double dist = first.Distance(second);
					if (dist > equilibrium)
					{
						dist -= equilibrium;
						double f = k * dist;

						double delta_x = second.Px - first.Px;
						double delta_y = second.Py - first.Py;
						double angle = OrderedPair.Angle(delta_y, delta_x);

						double fx = f * Math.Cos(angle);
						double fy = f * Math.Sin(angle);
						return new OrderedPair(fx, fy);
					}
					else
					{
						return new OrderedPair(0.0, 0.0);
					}
				}
			)
		{ }
	}
}
