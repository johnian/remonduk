using System;
using System.Linq;
using System.Collections.Generic;

namespace remonduk
{
    /// <summary>
    /// The physical system represents the current existence the interactions are taking place in.  
    /// It contains all of the circles and interactions.
    /// </summary>
	public class PhysicalSystem
	{
        /// <summary>
        /// This physical system's dictionary of net forces.
        /// Looking up a circle will give the netforce exerted on that circle from within the physical system.
        /// </summary>
		public Dictionary<Circle, Tuple<double, double>> netForces;
		
        //public Dictionary<Circle, List<Interaction>> interactions;
		/// <summary>
		/// A list of all of the interactions in this physical system.
		/// </summary>
        public List<Interaction> interactions;

        /// <summary>
        /// Default value for gravity.
        /// </summary>
		public bool GRAVITY = true;
        /// <summary>
        /// Global flag for gravity in this physical system.
        /// </summary>
		public bool gravity;

        /// <summary>
        /// No-arg constructor to create a new physical system.  Inits lists, sets gravity to default.
        /// </summary>
		public PhysicalSystem()
		{
			netForces = new Dictionary<Circle, Tuple<double, double>>();
			//interactions = new Dictionary<Circle, List<Interaction>>();
			interactions = new List<Interaction>();
			gravity = GRAVITY;
		}

        /// <summary>
        /// Adds a new circle to this physical system.
        /// </summary>
        /// <param name="circle">The new circle to add.</param>
		public void addCircle(Circle circle)
		{
			netForces.Add(circle, Tuple.Create(0.0, 0.0));
			// could make this add gravity as a default based on a flag
            //you mean SHOULD, there is a grav flag here and in constants
		}

        /// <summary>
        /// Removes a circle from this physical system.
        /// </summary>
        /// <param name="circle">The circle to remove from this system.</param>
		public void removeCircle(Circle circle)
		{
			netForces.Remove(circle);
			// have this also remove from the interaction list
            // we should address this, can delete circles but tethers remain
            // should also return something here to indicate if the circle did exist in the physical system or not
		}

        /// <summary>
        /// Adds an interaction to this physical system.
        /// </summary>
        /// <param name="interaction">The interaction to add.</param>
		public void addInteraction(Interaction interaction)
		{
			interactions.Add(interaction);
		}

        /// <summary>
        /// Removes an interaction from this physical system.
        /// </summary>
        /// <param name="interaction">The interaction to remove.</param>
		public void removeInteraction(Interaction interaction)
		{
			interactions.Remove(interaction);
		}

        /// <summary>
        /// Updates circle velocities based on the netforces in the dictionary.
        /// </summary>
		public void updateNetForces()
		{
			for (int i = 0; i < netForces.Count; i++)
			{
				Circle circle = netForces.ElementAt(i).Key;
				netForces[circle] = updateNetForceOn(circle);
				double ax = netForces[circle].Item1 / circle.mass;
				double ay = netForces[circle].Item2 / circle.mass;
				circle.setAcceleration(Circle.magnitude(ax, ay), Circle.angle(ay, ax));
			}
		}

        /// <summary>
        /// Calculates the net forces acting on a circle.
        /// </summary>
        /// <param name="circle">The circle to calculate net forces for.</param>
        /// <returns>The net forces on the given circle.</returns>
		public Tuple<double, double> updateNetForceOn(Circle circle)
		{
			double fx = 0;
			double fy = 0;
			foreach (Interaction interaction in interactions)
			{
				if (interaction.first == circle)
				{
					fx += interaction.forceOnFirst().Item1;
					fy += interaction.forceOnFirst().Item2;
				}
				if (interaction.second == circle)
				{
					fx += interaction.forceOnSecond().Item1;
					fy += interaction.forceOnSecond().Item2;
				}
			}
			return Tuple.Create(fx, fy);
		}

        //// remove this -- don't need, just grab array directly -- less overhead
        //public Tuple<double, double> netForceOn(Circle circle)
        //{
        //    return netForces[circle];
        //}
	}
}