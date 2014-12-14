using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace remonduk
{
    public partial class MainWindow : Form
    {
        Circle leader;
        Circle sheep;

        public MainWindow()
        {
            InitializeComponent();
            Application.Idle += HandleApplicationIdle;  //adds the HandleApplicationIdle method to the Application.Idle event
            leader = new Circle(200.0F, 150.0F, 10.0F);
            sheep = new Circle(100.0F, 500.0F, 10.0F);
            sheep.follow(leader);
        }

        //this method is added to the Idle event in the constructor
        void HandleApplicationIdle(object sender, EventArgs e)
        {
            while (IsApplicationIdle()) //keep going while we're still idle. idle event is fired once when the queue is emptied
            {
                this.CreateGraphics().Clear(System.Drawing.Color.Gray);
                leader.setV(6.0F, 0);
                leader.update();
                leader.draw(this.CreateGraphics());
                sheep.setV(5.0F, 0);
                sheep.update();
                sheep.draw(this.CreateGraphics());
                System.Threading.Thread.Sleep(50);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage
        {
            public IntPtr Handle;
            public uint Message;
            public IntPtr WParameter;
            public IntPtr LParameter;
            public uint Time;
            public Point Location;
        }

        //check to see the next message in queue returns 0 if empty 
        [DllImport("user32.dll")]
        public static extern int PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);

        //check to see if there's anything in the queue
        bool IsApplicationIdle()
        {
            NativeMessage result;
            return PeekMessage(out result, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0;
        }
    }
}
