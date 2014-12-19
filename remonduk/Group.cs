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
   
            for (int i = 0; i < tethers.Count; i++ )
            {
                System.Diagnostics.Debug.WriteLine("PULLING");
                tethers.ElementAt(i).pull();
            }
        }
    }
}
