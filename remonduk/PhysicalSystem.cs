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
		public Dictionary<Circle, OrderedPair> netForces;
		public List<Circle> circles;
		/// <summary>
		/// A list of all of the interactions in this physical system.
		/// </summary>
		public List<Interaction> interactions; // ideally, i'd like to index into this by circle to get interactions associated with that circle
		public Dictionary<Circle, List<Interaction>> interactionMap;

		public Dictionary<String, Force> forces;

		public QuadTree<Circle> tree;

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
			circles = new List<Circle>();
			netForces = new Dictionary<Circle, OrderedPair>();
			interactions = new List<Interaction>();
			interactionMap = new Dictionary<Circle, List<Interaction>>();

			forces = new Dictionary<String, Force>();

			tree = new QuadTree<Circle>(new FRect(0, 0, 600, 600), 10);

			gravity = GRAVITY;
			forces.Add("Gravity", new Gravity(Gravity.GRAVITY, Gravity.ANGLE));
		}

		/// <summary>
		/// Adds a new circle to this physical system.
		/// </summary>
		/// <param name="circle">The new circle to add.</param>
		public void addCircle(Circle circle)
		{
			circles.Add(circle);
			// need to figure out how to handle gravity
			if (false /*gravity*/) {
				interactions.Add(new Interaction(circle, null, forces["Gravity"]));
			}

			netForces.Add(circle, new OrderedPair(0, 0));
			
			interactionMap.Add(circle, new List<Interaction>());
			tree.Insert(circle.q_tree_pos);
		}

		/// <summary>
		/// Removes a circle from this physical system.
		/// </summary>
		/// <param name="circle">The circle to remove from this system.</param>
		public bool removeCircle(Circle circle)
		{
			if (circles.Remove(circle)) {
				netForces.Remove(circle);
				List<Interaction> associations = interactionMap[circle];
				foreach (Interaction interaction in associations) {
					Circle other = interaction.getOther(circle);
					interactionMap[other].Remove(interaction);
					interactions.Remove(interaction);
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Adds an interaction to this physical system.
		/// </summary>
		/// <param name="interaction">The interaction to add.</param>
		public void addInteraction(Interaction interaction)
		{
			interactions.Add(interaction);
			interactionMap[interaction.first].Add(interaction);
			interactionMap[interaction.second].Add(interaction);
		}

		/// <summary>
		/// Removes an interaction from this physical system.
		/// </summary>
		/// <param name="interaction">The interaction to remove.</param>
		public void removeInteraction(Interaction interaction)
		{
			interactions.Remove(interaction);
			interactionMap[interaction.first].Remove(interaction);
			interactionMap[interaction.second].Remove(interaction);
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
				double ax = netForces[circle].x / circle.mass;
				double ay = netForces[circle].y / circle.mass;
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
		public OrderedPair updateNetForceOn(Circle circle)
		{
			double fx = 0;
			double fy = 0;
			foreach (Interaction interaction in interactions)
			{
				if (interaction.first == circle)
				{
					fx += interaction.forceOnFirst().x;
					fy += interaction.forceOnFirst().y;
				}
				if (interaction.second == circle)
				{
					fx += interaction.forceOnSecond().x;
					fy += interaction.forceOnSecond().y;
				}
			}
			return new OrderedPair(fx, fy);
		}

		public void updatePositions()
		{
			double time = 1;
			Dictionary<Circle, OrderedPair>.KeyCollection circles = netForces.Keys;
			Dictionary<Circle, List<Circle>> collisionMap = new Dictionary<Circle, List<Circle>>(); ;
			while (time > 0)
			{
				double min = time;
				foreach (Circle circle in circles)
				{
					foreach (Circle that in circles)
					{
						// use quad tree to get the list of circles to check against
						// for now, just use circles
						double value = circle.colliding(that, time);
						if (value < min)
						{
							min = value;
							collisionMap = new Dictionary<Circle, List<Circle>>();
							List<Circle> collisions = new List<Circle>();
							collisions.Add(that);
							collisionMap.Add(circle, collisions);
						}
						else if (value == min)
						{
							collisionMap[circle].Add(that);
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