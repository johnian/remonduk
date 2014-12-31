using System;
using System.Linq;
using System.Collections.Generic;
using remonduk.QuadTreeTest;

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

        public QuadTree<Circle> tree;

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
            tree = new QuadTree<Circle>(new FRect(0, 0, 600, 600), 10);
			gravity = GRAVITY;
		}

        /// <summary>
        /// Adds a new circle to this physical system.
        /// </summary>
        /// <param name="circle">The new circle to add.</param>
		public void addCircle(Circle circle)
		{
			netForces.Add(circle, Tuple.Create(0.0, 0.0));
            tree.Insert(circle.q_tree_pos);
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
				circle.setAcceleration(OrderedPair.magnitude(ax, ay), OrderedPair.angle(ay, ax));
			}
		}
		
		public void update()
		{
			updateNetForces();
			updatePositions();
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

		public void updatePositions()
		{
			double time = 1;
			Dictionary<Circle, Tuple<Double, Double>>.KeyCollection circles = netForces.Keys;
			Dictionary<Circle, List<Circle>> collisionMap = new Dictionary<Circle, List<Circle>>();;
			while (time > 0)
			{
				double min = time;
				foreach (Circle circle in circles) {
					foreach (Circle that in circles) {
						// use quad tree to get the list of circles to check against
						// for now, just use circles
						double value = circle.colliding(that, time);
						if (value < min) {
							min = value;
							collisionMap = new Dictionary<Circle, List<Circle>>();
							List<Circle> collisions = new List<Circle>();
							collisions.Add(that);
							collisionMap.Add(circle, collisions);
						}
						else if (value == min)
						{
							collisionMap[circle].Add(that);
							//collisionMap.Add(circle, collisions);
						}
					}
				}
				foreach (Circle circle in circles)
				{
					circle.update(min);
				}
				updateVelocities(collisionMap);
				time -= min;
			}
		}

		public void updateVelocities(Dictionary<Circle, List<Circle>> collisionMap)
		{
			Dictionary<Circle, OrderedPair> velocityMap = new Dictionary<Circle, OrderedPair>();
			foreach (Circle circle in collisionMap.Keys)
			{
				velocityMap.Add(circle, circle.collideWith(collisionMap[circle]));
			}
			foreach (Circle circle in velocityMap.Keys)
			{
				circle.setVelocity(velocityMap[circle].x, velocityMap[circle].y);
			}
		}
	}
}