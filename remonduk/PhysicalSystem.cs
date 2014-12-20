using System;
using System.Collections.Generic;

namespace remonduk
{
	public class PhysicalSystem
	{
		public Dictionary<Circle, Tuple<double, double>> netForces;
		public List<Interaction> interactions;

		public bool GRAVITY = true;
		public bool gravity;
		
		// if we have a separate class that manages all the forces
		// iterate over it, building a map of circle to net forces
		// at the end, iterate over the map, updating accelerations of all circles

		public PhysicalSystem() {
			netForces = new Dictionary<Circle, Tuple<double, double>>();
			interactions = new List<Interaction>();
			gravity = GRAVITY;
		}

		public void addCircle(Circle circle) {
			netForces.Add(circle, Tuple.Create(0.0, 0.0));
			// could make this add gravity as a default based on a flag
		}
		
		public void removeCircle(Circle circle) {
			netForces.Remove(circle);
		}

		public void addInteraction(Interaction interaction) {
			interactions.Add(interaction);
		}

		public void removeInteraction(Interaction interaction) {
			interactions.Remove(interaction);
		}

		public void updateNetForce(Circle circle) {
			// update dictionary reference for the given circle
		}
	}
}