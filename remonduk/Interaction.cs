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

		public double forceOnFirst() {
			return force.calculate(first, second);
		}

		public double forceOnSecond() {
			return scalar * force.calculate(first, second);
		}

		// need functions to calculate ax and ay
		//public void update() {

		//	first.updateAcceleration(ax1, ay1);
		//	second.updateAcceleration(ax2, ay2);
		//}
	}
}