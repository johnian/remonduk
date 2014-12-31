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
using remonduk.QuadTreeTest;

namespace remonduk
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

            selected_circle = null;
            selected_interaction = null;

            frame_count = 0;
            Out.WriteLine("MAIN WINDOW CREATED");
        }

        void HandleApplicationIdle(object sender, EventArgs e)
        {
            while (IsApplicationIdle())
            {
                this.CreateGraphics().Clear(System.Drawing.Color.Gray);
				
				ps.update();
				//ps.updateNetForces();

                foreach (Circle c in ps.netForces.Keys)
                {
                    if (!pause)
                    {
                        List<QuadTreePositionItem<Circle>> cs = new List<QuadTreePositionItem<Circle>>();
                        ps.tree.GetItems(new FRect(c.py-50, c.px-50, c.py + 50, c.px + 50), ref cs);
						//c.update(cs);
                    }
                    c.draw(this.CreateGraphics());
                }

                drawHUD(this.CreateGraphics());

                selected_interaction = psdw.selected_interaction;
                selected_circle = psdw.selected_circle;

                frame_count++;

                System.Threading.Thread.Sleep(50);
            }
        }

        void drawInteractions(Graphics g)
        {
            foreach(Interaction i in ps.interactions)
            {
                if (i == psdw.selected_interaction && psdw.Visible)
                {

                    i.draw(g, Color.CornflowerBlue);
                }
                else
                {
                    i.draw(g, Color.Red);
                }
            }
        }

        void drawHUD(Graphics g)
        {
            if(cdw.Visible && selected_circle != null)
            {
                cdw.update_circle(selected_circle, ps.interactions);
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
            foreach(Circle c in ps.netForces.Keys)
            {
                c.q_tree_pos.Rect.draw(g);
            }
            drawNewCircleAngles(g);
            drawInteractions(g);
        }

        void drawSelected(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            g.DrawEllipse(pen, (float)selected_circle.px-25, (float)selected_circle.py-25, 50, 50);
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
            if (e.Button == MouseButtons.Right)
                return; //will implement soon

            drag = false;

            Point pos = this.PointToClient(Control.MousePosition);

            Circle click = new Circle(5, pos.X, pos.Y);

            bool found = false;

            if (drag && selected_circle != null)
            {
				selected_circle.setPosition(pos.X, pos.Y);
				//selected_circle.x = pos.X;
				//selected_circle.y = pos.Y;
            }


            foreach (Circle c in ps.netForces.Keys)
            {
                double collide = click.colliding(c, 0);

                if(collide != -1 && selected_circle != null && tethering)
                {
                    Tether t = new Tether(.002, 50);
                    Interaction i = new Interaction(selected_circle, c, t);
                    ps.addInteraction(i);
                    found = true;
                }
                else if (collide >= 0)
                {
                    selected_circle = c;
                    psdw.selected_circle = c;
                    psdw.circle_list.SelectedItem = c;

                    cdw.update_circle(selected_circle, ps.interactions);

                    new_circle_acceleration_angle_up_down.Value = (Decimal)(selected_circle.acceleration.angle() * 180.0 / Math.PI);
                    new_circle_acceleration_up_down.Value = (Decimal)selected_circle.acceleration.magnitude();
                    new_circle_velocity_angle_up_down.Value = (Decimal)(selected_circle.velocity.angle() * 180.0 / Math.PI);
                    new_circle_velocity_up_down.Value = (Decimal)selected_circle.velocity.magnitude();
                    circle_radius_up_down.Value = (Decimal)selected_circle.radius;

                    found = true;
                }
            }

            if (!found)
            {
                click.radius = (float)circle_radius_up_down.Value;
                click.setVelocity((float)new_circle_velocity_up_down.Value, 
                    ((double)new_circle_velocity_angle_up_down.Value) * Math.PI / 180.0);

				/*
				 * FIX ME!! update acceleration is no longer a thing
				 * */

				//click.updateAcceleration((float)new_circle_acceleration_up_down.Value, 
				//	((double)new_circle_acceleration_angle_up_down.Value) * Math.PI / 180.0);
                ps.addCircle(click);
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
            if (e.Button == MouseButtons.Right)
                return; //will implement soon

            if(selected_circle != null)
            {
                Point pos = this.PointToClient(Control.MousePosition);

                Circle click = new Circle(pos.X, pos.Y, 5);

                if(click.colliding(selected_circle, 0) >= 0)
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
            sfd.DefaultExt = ".circles";
            sfd.Filter = "Circles|.circles";
            sfd.ShowDialog();
            using (var writer = new System.IO.StreamWriter(sfd.FileName))
            {
                var serializer = new XmlSerializer(typeof(HashSet<Circle>));
                serializer.Serialize(writer, ps.netForces.Keys);
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
                    ps.addCircle(c);
                }
                //Out.WriteLine(circles.Count);
                //Circle c1 = circles.ElementAt(0);
                //c1.draw(this.CreateGraphics());
                //Out.WriteLine("X: " + c1.x + " Y: " + c1.y);
                //Out.WriteLine("R: " + c1.radius);
            }
        }

        private void circle_radius_up_down_ValueChanged(object sender, EventArgs e)
        {
            if(selected_circle != null)
            {
                selected_circle.radius = (float)circle_radius_up_down.Value;
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
                cdw = new GUI.Circle_Detail_Window(selected_circle, ps.interactions);
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
    }
}
