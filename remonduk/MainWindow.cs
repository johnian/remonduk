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

        //Label new_circle_velocity_label;
        //float new_circle_velocity_value = 0;
        //Label new_circle_velocity_angle_label;
        //double new_circle_velocity_angle_value = 0;
        //Label new_circle_acceleration_label;
        //float new_circle_acceleration_value = 0;
        //Label new_circle_acceleration_angle_label;
        //double new_circle_acceleration_angle_value = 0;
        //Label new_circle_radius_label;
        //float new_circle_radius_value = 5;

        bool pause;
        public MainWindow()
        {
            InitializeComponent();

            //new_circle_velocity_label = new Label();
            //new_circle_velocity_label.Text = "Velocity: " + new_circle_velocity_value;
            //new_circle_velocity_label.Location = new Point(15, 15);
            //this.Controls.Add(new_circle_velocity_label);
            //new_circle_velocity_label.

            //new_circle_velocity_angle_label = new Label();
            //new_circle_velocity_angle_label.Text = "Velocity Angle: " + new_circle_velocity_angle_value;
            //new_circle_velocity_angle_label.Location = new Point(15, 45);
            //this.Controls.Add(new_circle_velocity_angle_label);

            //new_circle_acceleration_label = new Label();
            //new_circle_acceleration_label.Text = "acceleration: " + new_circle_acceleration_value;
            //new_circle_acceleration_label.Location = new Point(15, 75);
            //this.Controls.Add(new_circle_acceleration_label);

            //new_circle_acceleration_angle_label = new Label();
            //new_circle_acceleration_angle_label.Text = "acceleration Angle: " + new_circle_acceleration_angle_value;
            //new_circle_acceleration_angle_label.Location = new Point(15, 105);
            //this.Controls.Add(new_circle_acceleration_angle_label);
            //new_circle_radius_label = new Label();
            //new_circle_radius_label.Text = "Radius: " + new_circle_radius_value;
            //new_circle_radius_label.Location = new Point(15, 135);
            //this.Controls.Add(new_circle_radius_label);


            Application.Idle += HandleApplicationIdle;  //adds the HandleApplicationIdle method to the Application.Idle event

            pause = false;

            Point pos = Control.MousePosition;
            pos = this.PointToClient(pos);
            mouse = new Circle(pos.X - 5, pos.Y - 5, 10.0F);
            leader = new Circle(200.0F, 150.0F, 10.0F, 12.0F, 0F, .1F, Math.PI / 2.0);
            sheep = new Circle(100.0F, 500.0F, 10.0F, 10.0F, 0F, .01F, Math.PI / 2.0);

            circles.Add(mouse);
            circles.Add(leader);
            circles.Add(sheep);

            leader.follow(mouse);

            //sheep.leash(mouse);
            sheep.follow(leader);


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

                foreach(Circle c in circles)
                {
                    if (!pause)
                    {
                        c.update(circles);
                    }
                    c.draw(this.CreateGraphics());
                }
                if (pause)
                {
                    drawPause(this.CreateGraphics());
                }
                else
                {
                    drawPlay(this.CreateGraphics());
                }
                drawNewCircleAngles(this.CreateGraphics());
                //mouse.draw(this.CreateGraphics());
                //leader.update(circles);
                //leader.draw(this.CreateGraphics());

                //sheep.update(circles);
                //sheep.draw(this.CreateGraphics());

                System.Threading.Thread.Sleep(50);
            }
        }

        void drawPause(Graphics g)
        {
            Brush brush = new SolidBrush(Color.Red);
            g.FillRectangle(brush, new Rectangle(this.Size.Width / 2 - 10, 25, 5, 25));
            g.FillRectangle(brush, new Rectangle(this.Size.Width / 2 + 10, 25, 5, 25));
        }

        void drawNewCircleAngles(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            double theta = (double)new_circle_velocity_angle_up_down.Value;
            System.Diagnostics.Debug.WriteLine(new_circle_velocity_angle_up_down.Value);
            float x1 = 52.5F;
            float y1 = 127.5F;
            float x2 = (float)Math.Cos(theta * (Math.PI / 180.0)) * 25 + x1;
            float y2 = (float)Math.Sin(theta * (Math.PI / 180.0)) * 25 + y1;
            g.DrawLine(pen, x1, y1, x2, y2);
            g.DrawEllipse(pen, 15, 90, 75, 75);
        }

        void drawPlay(Graphics g)
        {
            Brush brush = new SolidBrush(Color.Green);
            Point[] p = new Point[3];
            p[0] = new Point(this.Size.Width/2 - 15, 24);
            p[1] = new Point(this.Size.Width/2 - 15, 50);
            p[2] = new Point(this.Size.Width/2 + 15, 37);
            g.FillPolygon(brush,p);
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

        private void MainWindow_MouseClick(object sender, MouseEventArgs e)
        {
            Point pos = Control.MousePosition;
            pos = this.PointToClient(pos);
            Circle click = new Circle(pos.X - 5, pos.Y - 5, 10);
            click.setV((float)new_circle_velocity_up_down.Value, ((double)new_circle_velocity_angle_up_down.Value)*Math.PI/180.0);
            circles.Add(click);
        }

        private void MainWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == ' ')
            {
                pause = !pause;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void new_circle_velocity_up_down_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                pause = !pause;
            }
        }

        private void new_circle_acceleration_up_down_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                pause = !pause;
            }
        }

        private void new_circle_velocity_angle_up_down_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                pause = !pause;
            }
        }

        private void new_circle_acceleration_angle_up_down_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                pause = !pause;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
