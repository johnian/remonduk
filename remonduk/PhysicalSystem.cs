using System;
using System.Linq;
using System.Collections.Generic;

namespace remonduk
{
	public class PhysicalSystem
	{
		public Dictionary<Circle, Tuple<double, double>> netForces;
		//public Dictionary<Circle, List<Interaction>> interactions;
		public List<Interaction> interactions;

		public bool GRAVITY = true;
		public bool gravity;

		public PhysicalSystem()
		{
			netForces = new Dictionary<Circle, Tuple<double, double>>();
			//interactions = new Dictionary<Circle, List<Interaction>>();
			interactions = new List<Interaction>();
			gravity = GRAVITY;
		}

		public void addCircle(Circle circle)
		{
			netForces.Add(circle, Tuple.Create(0.0, 0.0));
			// could make this add gravity as a default based on a flag
		}

		public void removeCircle(Circle circle)
		{
			netForces.Remove(circle);
		}

		public void addInteraction(Interaction interaction)
		{
			interactions.Add(interaction);
		}

		public void removeInteraction(Interaction interaction)
		{
			interactions.Remove(interaction);
		}

		public void updateNetForces() {
			//List<Circle> circles = netForces.Keys;
			//for (int i = 0; i < circles.Count; i++) {
			//	netForces[circles[i]] = updateNetForceOn(circles[i]);
			//}
			//foreach(KeyValuePair<Circle, Tuple<double, double>> entry in netForces) {
			//	netForces[entry.Key] = updateNetForceOn(entry.Key);
			//}
			for (int i = 0; i < netForces.Count; i++) {
				Circle circle = netForces.ElementAt(i).Key;
				netForces[circle] = updateNetForceOn(circle);
			}
		}
		
		public Tuple<double, double> updateNetForceOn(Circle circle)
		{
			double fx = 0;
			double fy = 0;
			foreach(Interaction interaction in interactions) {
				if (interaction.first == circle) {
					fx += interaction.forceOnFirst().Item1;
					fy += interaction.forceOnFirst().Item2;
				}
				if (interaction.second == circle) {
					fx += interaction.forceOnSecond().Item1;
					fy += interaction.forceOnSecond().Item2;
				}
			}
			return Tuple.Create(fx, fy);
		}

		public Tuple<double, double> netForceOn(Circle circle) {
			return netForces[circle];
		}
	}
}