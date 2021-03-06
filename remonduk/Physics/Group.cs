﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Remonduk.Physics
{
	public class Group
	{
		public HashSet<Circle> group;
		public HashSet<Circle> anchors;

		public double x_min, x_min_y;
		public double x_max, x_max_y;
		public double y_min, y_min_x;
		public double y_max, y_max_x;

		//public HashSet<Tether> tethers;

		public Group()
		{
			this.group = new HashSet<Circle>();
			this.anchors = new HashSet<Circle>();
			//this.tethers = new HashSet<Tether>();
		}

		public void tether(Circle c1, Circle c2, double max_dist, double k)
		{
			group.Add(c1);
			group.Add(c2);
			//tethers.Add(new Tether(c1,c2,max_dist,k));
		}

		public void draw(Graphics g)
		{
			Pen pen = new Pen(Color.Black);
			Point[] points = new Point[4];
			points[0] = new Point((int)x_min, (int)x_min_y);
			points[1] = new Point((int)x_max, (int)x_max_y);
			points[2] = new Point((int)y_min_x, (int)y_min);
			points[3] = new Point((int)y_max_x, (int)y_max);
			g.DrawRectangle(pen, (int)x_min, (int)y_min, (int)(x_max - x_min), (int)(y_max - y_min));

		}

		public void update()
		{
			if (group.Count > 0)
			{
				x_min = group.ElementAt(0).Px;
				x_max = x_min;
				y_min = group.ElementAt(0).Py;
				y_max = y_min;
				y_min_x = x_min;
				y_max_x = x_max;
				x_max_y = y_max;
				x_min_y = y_min;
				foreach (Circle c in group)
				{
					if (c.Px > x_max)
					{
						x_max = c.Px;
						x_max_y = c.Py;
					}
					if (c.Px < x_min)
					{
						x_min = c.Px;
						x_min_y = c.Py;
					}
					if (c.Py > y_max)
					{
						y_max = c.Py;
						y_max_x = c.Px;
					}
					if (c.Py < y_min)
					{
						y_min = c.Py;
						y_min_x = c.Px;
					}
				}
			}
			//for (int i = 0; i < tethers.Count; i++ )
			//{
			//    Out.WriteLine("PULLING");
			//    tethers.ElementAt(i).pull();
			//}
		}
	}
}
