using System;

namespace remonduk {
	public class Force {
		public Circle m1, m2; // can be gravity, another circle
		public double constant;
		public Func<Circle, Circle, double> calculate;

		// if it's gravity, give it a constant force
		// or give a function to calculate

		public Force(Circle m1, Circle m2, Func<Circle, Circle, double> function) {
			this.m1 = m1;
			this.m2 = m2;
			this.calculate = function;			
		}

		public double calculateForce() {
			return calculate(m1, m2);
		}
	}
}