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
using Remonduk.Physics;
using Remonduk.Physics.QuadTree;
using System.IO;
using Remonduk.Properties;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace Remonduk
{
    public partial class MainWindow : Form
    {
        HashSet<Group> groups = new HashSet<Group>();

        PhysicalSystem ps = new PhysicalSystem();

        //Detail Windows
        GUI.Circle_Detail_Window cdw;
        GUI.Physical_System_Detail_Window psdw;
        GUI.Interaction_Detail_Window idw;

        //selected_group may not be need, should verify
        Circle selected_circle;
        Interaction selected_interaction;

        int frame_count;

        //"states"
        bool pause;
        bool tethering;
        bool drag;
        bool GroupDrawing;

        QTree tree;

        public MainWindow()
        {
            InitializeComponent();

            Application.Idle += HandleApplicationIdle;

            cdw = GUI.GUI_Singleton.Instance.cdw;
            psdw = GUI.GUI_Singleton.Instance.psdw;
            idw = GUI.GUI_Singleton.Instance.idw;

            groups.Add(new Group());

            pause = false;
            drag = false;
            tethering = false;
            GroupDrawing = false;

            selected_circle = null;
            selected_interaction = null;

            frame_count = 0;
            Out.WriteLine("MAIN WINDOW CREATED");
            tree = new QTree(new OrderedPair(0, 0), new OrderedPair(800, 800), 4);
        }

        void HandleApplicationIdle(object sender, EventArgs e)
        {
            while (IsApplicationIdle())
            {
                this.CreateGraphics().Clear(System.Drawing.Color.Gray);
				
				if (!pause) {
					ps.Update();
				}
				//ps.updateNetForces();

                foreach (Circle c in ps.Circles)
                {
                    if (!pause)
                    {
                        List<Circle> cs = new List<Circle>();
                        tree.Move(c);
						//c.update(cs);
                    }
					//Out.WriteLine("Drawing");
                    c.Draw(this.CreateGraphics());
                }

                drawHUD(this.CreateGraphics());
                //ps.Tree.draw(this.CreateGraphics());
                selected_interaction = psdw.selected_interaction;
                selected_circle = psdw.selected_circle;
                ps.Tree.Draw(this.CreateGraphics());
                frame_count++;

                System.Threading.Thread.Sleep(50);
            }
        }

        void drawInteractions(Graphics g)
        {
            foreach(Interaction i in ps.Interactions)
            {
                if (i == psdw.selected_interaction && psdw.Visible)
                {

                    i.Draw(g, Color.CornflowerBlue);
                }
                else
                {
                    i.Draw(g, Color.Red);
                }
            }
        }

        void drawHUD(Graphics g)
        {
            if(cdw.Visible && selected_circle != null)
            {
                cdw.update_circle(selected_circle, ps.Interactions);
            }

            if(psdw.Visible)
            {
                psdw.update_ps(ps);
                selected_circle = psdw.selected_circle;
            }

            if(idw.Visible)
            {
                idw.update_interaction(selected_interaction);
            }

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
            drawInteractions(g);
        }

        void drawSelected(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            g.DrawEllipse(pen, (float)selected_circle.Px-25, (float)selected_circle.Py-25, 50, 50);
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
            if(GroupDrawing == true)
            {

                return;
            }
            if (e.Button == MouseButtons.Right)
                return; //will implement soon

            drag = false;

            Point pos = this.PointToClient(Control.MousePosition);
			//Out.WriteLine("Original Click Pos: " + pos.ToString());
            Circle click = new Circle(5, pos.X, pos.Y);

            bool found = false;

            if (drag && selected_circle != null)
            {
				selected_circle.SetPosition(pos.X, pos.Y);
				//selected_circle.x = pos.X;
				//selected_circle.y = pos.Y;
            }


            foreach (Circle c in ps.Circles)
            {
				//double collide = click.Colliding(c, 0);
				bool collide = click.Colliding(c);

                if(collide/*collide < 1*/ && selected_circle != null && tethering)
                {
                    Spring t = new Spring();
                    Interaction i = new Interaction(selected_circle, c, t);
                    ps.AddInteraction(i);
                    found = true;
                }
                else if (collide /*!Double.IsInfinity(collide)*/)
                {
					Out.WriteLine("selected: [" + c.GetHashCode() + "]");
                    selected_circle = c;
                    psdw.selected_circle = c;
                    psdw.circle_list.SelectedItem = c;

                    cdw.update_circle(selected_circle, ps.Interactions);

                    new_circle_acceleration_angle_up_down.Value = (Decimal)(selected_circle.Acceleration.Angle() * 180.0 / Math.PI);
                    new_circle_acceleration_up_down.Value = (Decimal)selected_circle.Acceleration.Magnitude();
                    new_circle_velocity_angle_up_down.Value = (Decimal)(selected_circle.Velocity.Angle() * 180.0 / Math.PI);
                    new_circle_velocity_up_down.Value = (Decimal)selected_circle.Velocity.Magnitude();
                    circle_radius_up_down.Value = (Decimal)selected_circle.Radius;

                    found = true;
                }
            }

            if (!found)
            {
                click.Radius = (float)circle_radius_up_down.Value;
                //click.SetVelocity((float)new_circle_velocity_up_down.Value, 
                    //((double)new_circle_velocity_angle_up_down.Value) * Math.PI / 180.0);

				/*
				 * FIX ME!! update acceleration is no longer a thing
				 * */

				//click.updateAcceleration((float)new_circle_acceleration_up_down.Value, 
				//	((double)new_circle_acceleration_angle_up_down.Value) * Math.PI / 180.0);
				//Out.WriteLine("Click Pos: " + click.Position);
				Out.WriteLine("new circle: " + click.GetHashCode());
                ps.AddCircle(click);
                tree.Insert(click);
            }
        }

        private void MainWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                pause = !pause;
            }
        }

        private void new_circle_velocity_up_down_ValueChanged(object sender, EventArgs e)
        {
            if(selected_circle != null)
            {
                selected_circle.SetVelocity((float)new_circle_velocity_up_down.Value, ((double)new_circle_velocity_angle_up_down.Value) * Math.PI / 180);
            }
        }

        private void new_circle_acceleration_up_down_ValueChanged(object sender, EventArgs e)
        {
            if(selected_circle != null)
            {
                selected_circle.SetAcceleration((float)new_circle_acceleration_up_down.Value, ((double)new_circle_acceleration_angle_up_down.Value) * Math.PI / 180.0);
            }
        }

        private void new_circle_velocity_angle_up_down_ValueChanged(object sender, EventArgs e)
        {
            if(selected_circle != null)
            {
                selected_circle.SetVelocity((float)new_circle_velocity_up_down.Value, ((double)new_circle_velocity_angle_up_down.Value) * Math.PI / 180);
            }
        }

        private void new_circle_acceleration_angle_up_down_ValueChanged(object sender, EventArgs e)
        {
            if (selected_circle != null)
            {
                selected_circle.SetAcceleration((float)new_circle_acceleration_up_down.Value, ((double)new_circle_acceleration_angle_up_down.Value) * Math.PI / 180.0);
            }
        }

        //actually on mouseclick
        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return; //will implement soon

            if(selected_circle != null)
            {
                Point pos = this.PointToClient(Control.MousePosition);

                Circle click = new Circle(5, pos.X, pos.Y);

                if(click.Colliding(selected_circle)/*click.Colliding(selected_circle, 0) >= 0*/)
                {
                    drag = true;
                }
            }
        }

        private void save_menu_item_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "SAVE YOUR CIRCLES FOR FUTURE CIRCLES TO BE THE SAME AS YOUR CURRENT CIRCLES";
            sfd.AddExtension = true;
			// LOOK AT ME
			// DOES THIS NEED TO BE CHANGED TO .Circles??
            sfd.DefaultExt = ".circles";
            sfd.Filter = "Circles|.circles";
            sfd.ShowDialog();
            using (var writer = new System.IO.StreamWriter(sfd.FileName))
            {
                var serializer = new XmlSerializer(typeof(HashSet<Circle>));
				serializer.Serialize(writer, ps.Circles);
				//serializer.Serialize(writer, ps.NetForces.Keys);
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
                HashSet<Circle> circles = serializer.Deserialize(stream) as HashSet<Circle>;
                foreach(Circle c in circles)
                {
                    ps.AddCircle(c);
                }
                //Out.WriteLine(circles.Count);
                //Circle c1 = circles.ElementAt(0);
                //c1.Draw(this.CreateGraphics());
                //Out.WriteLine("X: " + c1.x + " Y: " + c1.y);
                //Out.WriteLine("R: " + c1.Radius);
            }
        }

        private void circle_radius_up_down_ValueChanged(object sender, EventArgs e)
        {
            if(selected_circle != null)
            {
                selected_circle.Radius = (float)circle_radius_up_down.Value;
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.ShiftKey)
            {
                //Out.WriteLine("TETHER ON");
                tethering = true;
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.ShiftKey)
            {
                tethering = false;
            }
            if(e.KeyCode == Keys.Q && selected_circle != null)
            {
                cdw = new GUI.Circle_Detail_Window(selected_circle, ps.Interactions);
                cdw.Show();
            }
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            psdw = new GUI.Physical_System_Detail_Window(ps);
            psdw.Show();
        }

        private void interactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            idw = new GUI.Interaction_Detail_Window(selected_interaction);
            idw.Show();
        }

        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Load Image";

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string sep = "";

            foreach (ImageCodecInfo codec in codecs)
            {
                string codecName = codec.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                ofd.Filter = String.Format("{0}{1}{2} ({3})|{3}", ofd.Filter, sep, codecName, codec.FilenameExtension);
                sep = "|";
            }

            ofd.Filter = String.Format("{0}{1}{2} ({3})|{3}", ofd.Filter, sep, "All Files", "*.*");

            ofd.ShowDialog();

            Bitmap image = (Bitmap)Bitmap.FromFile(ofd.FileName);
            Out.WriteLine("Image loaded..." + image.ToString());
            Out.WriteLine("Dim: (" + image.Height + "," + image.Width + ")");

            HashSet<OrderedPair> points = new HashSet<OrderedPair>();
            List<Circle> ImageCircles = new List<Circle>();

            float minR = 1F;
            float maxR = 7F;


            Color bgColor = Color.FromArgb(255, 0, 0, 0);
            Graphics g = Graphics.FromImage(image);

            for(int x = 0; x < image.Width; x++)
            {
                for(int y = 0; y < image.Height; y++)
                {
                    points.Add(new OrderedPair(x+200, y+50));
                }
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (points.Count > 0)
            {
                Random rand = new Random();
                bool add = true;
                float radius = (float)rand.NextDouble() * maxR + minR;
                OrderedPair point = points.ElementAt(rand.Next(points.Count));
                Color pointColor = image.GetPixel((int)point.X - 200, (int)point.Y - 50);
                if(pointColor == bgColor)
                {
                   add = false;
                }
                if (add)
                {
                    foreach (Circle circle in ps.Tree.Possible(new Circle(radius, point.X, point.Y), 0))
                    {
                        double dist = point.Magnitude(circle.Position);
                        while (dist < radius + circle.Radius && radius > minR + .5F)
                        {
                            radius -= .5F;
                        }
                        if (dist < radius + circle.Radius)
                        {
                            add = false;
                            break;
                        }
                    }
                }
                if (add)
                {
                    Circle newcircle = new Circle(radius, point.X, point.Y);
                    newcircle.COLOR = pointColor;

                    Brush brush = new SolidBrush(bgColor);
                    g.FillEllipse(brush, (float)point.X-200 - radius, (float)point.Y-50 - radius, radius*2, radius*2);

                    ps.AddCircle(newcircle);
                }
                points.Remove(point);
                //Out.WriteLine("Points left " + points.Count);
            }
            image.Save("out.bmp");
            timer.Stop();
            TimeSpan ts = timer.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Out.WriteLine("Time taken: " + elapsedTime);


        }
    }
}
