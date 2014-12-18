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
using System.Xml.Serialization;

namespace remonduk
{
    public partial class MainWindow : Form
    {
        HashSet<Circle> circles = new HashSet<Circle>();
        Circle selected;

        bool pause;

        bool drag;

        public MainWindow()
        {
            InitializeComponent();

            Application.Idle += HandleApplicationIdle;  //adds the HandleApplicationIdle method to the Application.Idle event

            pause = false;
            selected = null;
            drag = false;
        }

        //this method is added to the Idle event in the constructor
        void HandleApplicationIdle(object sender, EventArgs e)
        {
            while (IsApplicationIdle()) //keep going while we're still idle. idle event is fired once when the queue is emptied
            {
                this.CreateGraphics().Clear(System.Drawing.Color.Gray);

                foreach (Circle c in circles)
                {
                    if (!pause)
                    {
                        c.update(circles);
                    }
                    c.draw(this.CreateGraphics());
                }

                drawHUD(this.CreateGraphics());

                System.Threading.Thread.Sleep(50);
            }
        }

        void drawHUD(Graphics g)
        {
            if(pause)
            {
                drawPause(g);
            }
            else
            {
                drawPlay(g);
            }
            if(selected != null)
            {
                drawSelected(g);
            }
            drawNewCircleAngles(g);
        }

        void drawSelected(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            g.DrawEllipse(pen, selected.x-25, selected.y-25, 50, 50);
        }

        void drawPause(Graphics g)
        {
            Brush brush = new SolidBrush(Color.Red);
            g.FillRectangle(brush, new Rectangle(this.Size.Width / 2 - 10, 25, 5, 25));
            g.FillRectangle(brush, new Rectangle(this.Size.Width / 2 + 10, 25, 5, 25));
        }

        void drawPlay(Graphics g)
        {
            Brush brush = new SolidBrush(Color.Green);
            Point[] p = new Point[3];
            p[0] = new Point(this.Size.Width / 2 - 15, 24);
            p[1] = new Point(this.Size.Width / 2 - 15, 50);
            p[2] = new Point(this.Size.Width / 2 + 15, 37);
            g.FillPolygon(brush, p);
        }

        void drawNewCircleAngles(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            double theta = (double)new_circle_velocity_angle_up_down.Value;
            float x1 = 52.5F;
            float y1 = 127.5F;
            float x2 = (float)Math.Cos(theta * (Math.PI / 180.0)) * 25 + x1;
            float y2 = (float)Math.Sin(theta * (Math.PI / 180.0)) * 25 + y1;
            g.DrawLine(pen, x1, y1, x2, y2);
            g.DrawEllipse(pen, 15, 90, 75, 75);


            theta = (double)new_circle_acceleration_angle_up_down.Value;
            //System.Diagnostics.Debug.WriteLine(new_circle_acceleration_angle_up_down.Value);
            x1 = 162.5F;
            y1 = 127.5F;
            x2 = (float)Math.Cos(theta * (Math.PI / 180.0)) * 25 + x1;
            y2 = (float)Math.Sin(theta * (Math.PI / 180.0)) * 25 + y1;
            g.DrawLine(pen, x1, y1, x2, y2);
            g.DrawEllipse(pen, 125, 90, 75, 75);
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

        //actually on mouseup
        private void MainWindow_MouseClick(object sender, MouseEventArgs e)
        {
            Point pos = Control.MousePosition;
            pos = this.PointToClient(pos);
            Circle click = new Circle(pos.X - 5, pos.Y - 5, 25);
            bool found = false;
            if (drag && selected != null)
            {
                selected.x = pos.X;
                selected.y = pos.Y;
            }
            drag = false;
            foreach (Circle c in circles)
            {

                if (click.collide(c) == c)
                {
                    System.Diagnostics.Debug.WriteLine("MOUSEUP");
                    System.Diagnostics.Debug.WriteLine(c.velocity + " " + c.acceleration);
                    selected = c;
                    new_circle_acceleration_angle_up_down.Value = (Decimal)(selected.acceleration_angle * 180.0 / Math.PI);
                    new_circle_acceleration_up_down.Value = (Decimal)selected.acceleration;
                    new_circle_velocity_angle_up_down.Value = (Decimal)(selected.velocity_angle * 180.0 / Math.PI);
                    new_circle_velocity_up_down.Value = (Decimal)selected.velocity;
                    found = true;
                    selected.color = Color.CornflowerBlue;
                }
            }
            if (!found)
            {
                click.r = 10;
                click.setVelocity((float)new_circle_velocity_up_down.Value, ((double)new_circle_velocity_angle_up_down.Value) * Math.PI / 180.0);
                click.updateAcceleration((float)new_circle_acceleration_up_down.Value, ((double)new_circle_acceleration_angle_up_down.Value) * Math.PI / 180.0);
                circles.Add(click);
                if(selected != null)
                {
                    selected.color = Color.Chartreuse;
                }
                selected = null;
            }
        }

        private void MainWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
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

        private void new_circle_velocity_up_down_ValueChanged(object sender, EventArgs e)
        {
            if(selected != null)
            {
                selected.velocity = ((float)new_circle_velocity_up_down.Value); 
            }
        }

        private void new_circle_acceleration_up_down_ValueChanged(object sender, EventArgs e)
        {
            if(selected != null)
            {
                selected.acceleration = (float)new_circle_acceleration_up_down.Value;
            }
        }

        private void new_circle_velocity_angle_up_down_ValueChanged(object sender, EventArgs e)
        {
            if(selected != null)
            {
                selected.velocity_angle = ((double)new_circle_velocity_angle_up_down.Value) * Math.PI / 180.0;
            }
        }

        private void new_circle_acceleration_angle_up_down_ValueChanged(object sender, EventArgs e)
        {
            if (selected != null)
            {
                selected.acceleration_angle = ((double)new_circle_acceleration_angle_up_down.Value) * Math.PI / 180.0;
            }
        }

        //actually on mouseclick
        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if(selected != null)
            {
                Point pos = Control.MousePosition;
                pos = this.PointToClient(pos);
                Circle click = new Circle(pos.X, pos.Y, 50);
                Circle easySelect = new Circle(selected.x, selected.y, 25);
                if(click.collide(easySelect) == easySelect)
                {
                    drag = true;
                    System.Diagnostics.Debug.WriteLine("SELECTED");
                    System.Diagnostics.Debug.WriteLine(String.Concat("MouseX = ", pos.X.ToString(),"  MouseY = ",pos.Y.ToString()));
                }
            }
        }

        private void save_menu_item_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "SAVE YOUR CIRCLES FOR FUTURE CIRCLES TO BE THE SAME AS YOUR CURRENT CIRCLES";
            sfd.AddExtension = true;
            sfd.DefaultExt = ".xml";
            sfd.Filter = "XML|.xml";
            sfd.ShowDialog();
            using (var writer = new System.IO.StreamWriter("out.xml"))
            {
                var serializer = new XmlSerializer(typeof(HashSet<Circle>));
                serializer.Serialize(writer, circles);
                writer.Flush();
            }


        }
    }
}
