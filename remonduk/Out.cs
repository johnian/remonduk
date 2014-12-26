using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace remonduk
{
    public static class Out
    {
        public static bool on = true;

        public static void Write(String sin)
        {
            if(on)
                System.Diagnostics.Debug.Write(sin);
        }

        public static void WriteLine(String sin)
        {
            if(on)
                System.Diagnostics.Debug.WriteLine(sin);
        }
    }
}
