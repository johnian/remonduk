using System;
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

						double delta_x = second.px - first.px;
						double delta_y = second.py - first.py;
						double angle = OrderedPair.angle(delta_y, delta_x);

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
