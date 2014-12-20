using System;

namespace remonduk
{
	public class Interaction
	{
		public Circle first;
		public Circle second;
		public Force force;
		public double scalar;

		public Interaction(Circle first, Circle second, Force force, double scalar = 1) {
			this.first = first;
			this.second = second;
			this.force = force;
			this.scalar = scalar;
		}

		public Tuple<double, double> forceOnFirst() {
			Tuple<double, double> f = force.calculate(second, first);
			return Tuple.Create(scalar * f.Item1, scalar * f.Item2);
		}

		public Tuple<double, double> forceOnSecond() {
			return force.calculate(first, second);
		}
	}
}