using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remonduk.GUI
{
    public class GUISingleton
    {
        private static GUISingleton instance;

        public CircleDetailWindow Cdw = new CircleDetailWindow();
        public PhysicalSystemDetailWindow Psdw = new PhysicalSystemDetailWindow();
        public InteractionDetailWindow Idw = new InteractionDetailWindow();

        public GUISingleton()
        {
            Out.WriteLine("SINGLETON CONSTRUCTOR");
        }

        public static GUISingleton Instance
        {
            get 
            {
                if (instance == null)
                {
                    Out.WriteLine("INSTANCE == NULL");
                    instance = new GUISingleton();
                }
                return instance;
            }
        }
    }
}
