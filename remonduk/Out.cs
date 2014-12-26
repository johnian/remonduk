using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace remonduk
{
    /// <summary>
    /// Simplified debug printing.
    /// </summary>
    public static class Out
    {
        /// <summary>
        /// Turns debug printing on and off.
        /// </summary>
        public static bool on = true;

        /// <summary>
        /// Wrapper for write.
        /// </summary>
        /// <param name="sin">The string to write.</param>
        public static void Write(String sin)
        {
            if(on)
                System.Diagnostics.Debug.Write(sin);
        }

        /// <summary>
        /// Wrapper for writeline.
        /// </summary>
        /// <param name="sin">The string to write + a newline.</param>
        public static void WriteLine(String sin)
        {
            if(on)
                System.Diagnostics.Debug.WriteLine(sin);
        }
    }
}
