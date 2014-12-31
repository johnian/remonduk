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
}
