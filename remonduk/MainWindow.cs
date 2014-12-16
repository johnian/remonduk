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
        List<Circle> circles = new List<Circle>();
        Circle mouse;

        public MainWindow()
        {
            InitializeComponent();
            Application.Idle += HandleApplicationIdle;  //adds the HandleApplicationIdle method to the Application.Idle event
            leader = new Circle(200.0F, 150.0F, 10.0F,12.0F);
            sheep = new Circle(100.0F, 500.0F, 10.0F, 10.0F);
            Point pos = Control.MousePosition;
            pos = this.PointToClient(pos);
            mouse = new Circle(pos.X-5, pos.Y-5, 10.0F);
            circles.Add(leader);
            circles.Add(sheep);
            sheep.leash(mouse);
            sheep.follow(mouse);
            sheep.setA(.5F, 0);
        }

        //this method is added to the Idle event in the constructor
        void HandleApplicationIdle(object sender, EventArgs e)
        {
            while (IsApplicationIdle()) //keep going while we're still idle. idle event is fired once when the queue is emptied
            {
                this.CreateGraphics().Clear(System.Drawing.Color.Gray);

                Point pos = Control.MousePosition;
                pos = this.PointToClient(pos);
                mouse.x = pos.X - 5;
                mouse.y = pos.Y - 5;
                mouse.draw(this.CreateGraphics());
                leader.update(circles);
                leader.draw(this.CreateGraphics());
                leader.follow(sheep);
                sheep.update(circles);
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
