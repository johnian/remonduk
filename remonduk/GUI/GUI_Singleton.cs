using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace remonduk.GUI
{
    public class GUI_Singleton
    {
        public static GUI_Singleton instance;

        public Circle_Detail_Window cdw = new Circle_Detail_Window();
        public Physical_System_Detail_Window psdw = new Physical_System_Detail_Window();
        public Interaction_Detail_Window idw = new Interaction_Detail_Window();

        public GUI_Singleton()
        {
            System.Diagnostics.Debug.WriteLine("SINGLETON CONSTRUCTOR");
        }

        public static GUI_Singleton Instance
        {
            get 
            {
                if (instance == null)
                {
                    System.Diagnostics.Debug.WriteLine("INSTANCE == NULL");
                    instance = new GUI_Singleton();
                }
                return instance;
            }
        }
    }
}
