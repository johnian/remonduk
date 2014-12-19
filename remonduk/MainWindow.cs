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
        HashSet<Group> groups = new HashSet<Group>();
        
        Circle selected_circle;
        Group selected_group;

        int frame_count;

        bool pause;

        bool drag;

        bool targeting;

        public MainWindow()
        {
            InitializeComponent();

            Application.Idle += HandleApplicationIdle;  //adds the HandleApplicationIdle method to the Application.Idle event
            groups.Add(new Group());
            pause = false;
            selected_circle = null;
            drag = false;
            targeting = false;
            frame_count = 0;
        }

        //this method is added to the Idle event in the constructor
        void HandleApplicationIdle(object sender, EventArgs e)
        {
            while (IsApplicationIdle()) //keep going while we're still idle. idle event is fired once when the queue is emptied
            {
                this.CreateGraphics().Clear(System.Drawing.Color.Gray);

                groups.ElementAt(0).update();
                foreach (Circle c in circles)
                {
                    if (!pause)
                    {
                        c.update(circles);
                    }
                    c.draw(this.CreateGraphics());
                }

                drawHUD(this.CreateGraphics());

                System.Diagnostics.Debug.WriteLine("Frame Count: " + frame_count);
                System.Diagnostics.Debug.WriteLine(circles.Count);
                frame_count++;
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
            if(selected_circle != null)
            {
                drawSelected(g);
            }
            drawNewCircleAngles(g);
        }

        void drawSelected(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            g.DrawEllipse(pen, (float)selected_circle.x-25, (float)selected_circle.y-25, 50, 50);
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
            Circle click = new Circle(pos.X, pos.Y, 5);
            bool found = false;
            if (drag && selected_circle != null)
            {
                selected_circle.x = pos.X;
                selected_circle.y = pos.Y;
            }
            drag = false;
            foreach (Circle c in circles)
            {
                if(click.colliding(c) && selected_circle != null)
                {
                    //selected_circle.follow(c, 25, 100);
                    groups.ElementAt(0).tethers.Add(new Tether(selected_circle, c, 50, .002));
                    found = true;
                }
                else if (click.colliding(c))
                {
                    System.Diagnostics.Debug.WriteLine("MOUSEUP");
                    System.Diagnostics.Debug.WriteLine(c.velocity + " " + c.acceleration);
                    selected_circle = c;
                    new_circle_acceleration_angle_up_down.Value = (Decimal)(selected_circle.acceleration_angle * 180.0 / Math.PI);
                    new_circle_acceleration_up_down.Value = (Decimal)selected_circle.acceleration;
                    new_circle_velocity_angle_up_down.Value = (Decimal)(selected_circle.velocity_angle * 180.0 / Math.PI);
                    new_circle_velocity_up_down.Value = (Decimal)selected_circle.velocity;
                    circle_radius_up_down.Value = (Decimal)selected_circle.r;
                    found = true;
                    selected_circle.color = Color.CornflowerBlue;
                }
            }
            if (!found)
            {
                click.r = (float)circle_radius_up_down.Value;
                click.setVelocity((float)new_circle_velocity_up_down.Value, ((double)new_circle_velocity_angle_up_down.Value) * Math.PI / 180.0);
                click.updateAcceleration((float)new_circle_acceleration_up_down.Value, ((double)new_circle_acceleration_angle_up_down.Value) * Math.PI / 180.0);
                if (Constants.Instance.GRAVITY_ACTIVE)
                {
                    click.updateAcceleration(Constants.Instance.GRAVITY, Constants.Instance.GRAVITY_ANGLE);
                }
                circles.Add(click);
                if(selected_circle != null)
                {
                    selected_circle.color = Color.Chartreuse;
                }
                selected_circle = null;
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
            if(selected_circle != null)
            {
                selected_circle.setVelocity((float)new_circle_velocity_up_down.Value, ((double)new_circle_velocity_angle_up_down.Value) * Math.PI / 180);
            }
        }

        private void new_circle_acceleration_up_down_ValueChanged(object sender, EventArgs e)
        {
            if(selected_circle != null)
            {
                selected_circle.setAcceleration((float)new_circle_acceleration_up_down.Value, ((double)new_circle_acceleration_angle_up_down.Value) * Math.PI / 180.0);
            }
        }

        private void new_circle_velocity_angle_up_down_ValueChanged(object sender, EventArgs e)
        {
            if(selected_circle != null)
            {
                selected_circle.setVelocity((float)new_circle_velocity_up_down.Value, ((double)new_circle_velocity_angle_up_down.Value) * Math.PI / 180);
            }
        }

        private void new_circle_acceleration_angle_up_down_ValueChanged(object sender, EventArgs e)
        {
            if (selected_circle != null)
            {
                selected_circle.setAcceleration((float)new_circle_acceleration_up_down.Value, ((double)new_circle_acceleration_angle_up_down.Value) * Math.PI / 180.0);
            }
        }

        //actually on mouseclick
        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if(selected_circle != null)
            {
                Point pos = Control.MousePosition;
                pos = this.PointToClient(pos);
                Circle click = new Circle(pos.X, pos.Y, 5);
                if(click.colliding(selected_circle))
                {
                    drag = true;
                    //System.Diagnostics.Debug.WriteLine("SELECTED");
                    //System.Diagnostics.Debug.WriteLine(String.Concat("MouseX = ", pos.X.ToString(),"  MouseY = ",pos.Y.ToString()));
                }
            }
        }

        private void save_menu_item_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "SAVE YOUR CIRCLES FOR FUTURE CIRCLES TO BE THE SAME AS YOUR CURRENT CIRCLES";
            sfd.AddExtension = true;
            sfd.DefaultExt = ".circles";
            sfd.Filter = "Circles|.circles";
            sfd.ShowDialog();
            using (var writer = new System.IO.StreamWriter(sfd.FileName))
            {
                var serializer = new XmlSerializer(typeof(HashSet<Circle>));
                serializer.Serialize(writer, circles);
                writer.Flush();
            }
        }

        private void load_menu_item_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "REMEMBER YOUR CIRCLES FROM BEFORE TO MAKE THEM EXIST AGAIN!";
            ofd.ShowDialog();
            using (var stream = System.IO.File.OpenRead(ofd.FileName))
            {
                var serializer = new XmlSerializer(typeof(HashSet<Circle>));
                circles = serializer.Deserialize(stream) as HashSet<Circle>;
                //System.Diagnostics.Debug.WriteLine(circles.Count);
                //Circle c1 = circles.ElementAt(0);
                //c1.draw(this.CreateGraphics());
                //System.Diagnostics.Debug.WriteLine("X: " + c1.x + " Y: " + c1.y);
                //System.Diagnostics.Debug.WriteLine("R: " + c1.r);
            }
        }

        private void circle_radius_up_down_ValueChanged(object sender, EventArgs e)
        {
            if(selected_circle != null)
            {
                selected_circle.r = (float)circle_radius_up_down.Value;
            }
        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(gravity_toggle_menu_item.Checked)
            {
                foreach(Circle c in circles)
                {
                    c.updateAcceleration(Constants.Instance.GRAVITY, Constants.Instance.GRAVITY_ANGLE);
                }
                Constants.Instance.GRAVITY_ACTIVE = true;
            }
            else
            {
                foreach (Circle c in circles)
                {
                    c.updateAcceleration(Constants.Instance.GRAVITY, -1*Constants.Instance.GRAVITY_ANGLE);
                }
                Constants.Instance.GRAVITY_ACTIVE = false;
            }
        }
    }
}
