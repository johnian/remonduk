using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace remonduk
{
    class Group
    {
        public HashSet<Circle> group;
        public HashSet<Circle> anchors;
        public HashSet<Tether> tethers;

        public Group()
        {
            this.group = new HashSet<Circle>();
            this.anchors = new HashSet<Circle>();
            this.tethers = new HashSet<Tether>();
        }

        public void tether(Circle c1, Circle c2, double max_dist, double k)
        {
            group.Add(c1);
            group.Add(c2);
            tethers.Add(new Tether(c1,c2,max_dist,k));
        }

        public void update()
        {

			HashSet<Tether> original_tethers = new HashSet<Tether>();
			foreach (Tether tether in tethers) {
				original_tethers.Add(new Tether(new Circle(tether.c1), new Circle(tether.c2), tether.max_dist, tether.k));

			}
			if (tethers.Count > 0) {
				//System.Diagnostics.Debug.WriteLine(tethers.ElementAt(0).c1 == original_tethers.ElementAt(0).c1);
			}

			for (int i = 0; i < tethers.Count; i++) {
				tethers.ElementAt(i).pull(original_tethers.ElementAt(i).c1, original_tethers.ElementAt(i).c2);
			}
        }
    }
}
