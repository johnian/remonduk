﻿using System;
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
        //HashSet<Circle> circles = new HashSet<Circle>();
        HashSet<Group> groups = new HashSet<Group>();

        PhysicalSystem ps = new PhysicalSystem();
        GUI.Circle_Detail_Window cdw;
        GUI.Physical_System_Detail_Window psdw;
        GUI.Interaction_Detail_Window idw;
        Circle selected_circle;
        Group selected_group;
        Interaction selected_interaction;

        int frame_count;

        bool pause;
        bool tethering;
        bool drag;

        public MainWindow()
        {
            InitializeComponent();
            cdw = GUI.GUI_Singleton.Instance.cdw;
            psdw = GUI.GUI_Singleton.Instance.psdw;
            idw = GUI.GUI_Singleton.Instance.idw;
            Application.Idle += HandleApplicationIdle;
            groups.Add(new Group());
            pause = false;
            selected_circle = null;
            selected_interaction = null;
            drag = false;
            frame_count = 0;
            tethering = false;
            System.Diagnostics.Debug.WriteLine("HERE");
        }

        void HandleApplicationIdle(object sender, EventArgs e)
        {
            while (IsApplicationIdle())
            {
                this.CreateGraphics().Clear(System.Drawing.Color.Gray);

                groups.ElementAt(0).update();
             

                ps.updateNetForces();
                foreach (Circle c in ps.netForces.Keys)
                {
                    if (!pause)
                    {
                        c.update(ps.netForces.Keys);
                    }
                    c.draw(this.CreateGraphics());
                }

                drawHUD(this.CreateGraphics());
                selected_interaction = psdw.selected_interaction;
				//System.Diagnostics.Debug.WriteLine("Frame Count: " + frame_count);
				//System.Diagnostics.Debug.WriteLine(circles.Count);
                frame_count++;
                System.Threading.Thread.Sleep(50);
            }
        }

        void drawInteractions(Graphics g)
        {
            Pen pen = new Pen(Color.Red);
            Pen select_pen = new Pen(Color.CornflowerBlue);
            foreach(Interaction i in ps.interactions)
            {
                Point p1 = new Point((int)i.first.x, (int)i.first.y);
                Point p2 = new Point((int)i.second.x, (int)i.second.y);
                if (i == psdw.selected_interaction && psdw.Visible)
                {
                    
                    g.DrawLine(select_pen, p1, p2);
                }
                else
                {
                    g.DrawLine(pen, p1, p2);
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
            drawNewCircleAngles(g);
            //groups.ElementAt(0).draw(g);
            drawInteractions(g);
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
            if (e.Button == MouseButtons.Right)
                return;
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
            foreach (Circle c in ps.netForces.Keys)
            {
                if(click.colliding(c) && selected_circle != null && tethering)
                {
                    //selected_circle.follow(c, 25, 100);
                    Tether t = new Tether(.0002, 50);
                    Interaction i = new Interaction(selected_circle, c, t);
                    ps.addInteraction(i);
					//groups.ElementAt(0).tethers.Add(t);
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
                    psdw.selected_circle = c;
                    psdw.circle_list.SelectedItem = c;
                    found = true;
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
                ps.addCircle(click);
                groups.ElementAt(0).group.Add(click);

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
            if (e.Button == MouseButtons.Right)
                return;
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
                //foreach(Circle c in circles)
                //{
                //    c.updateAcceleration(Constants.Instance.GRAVITY, Constants.Instance.GRAVITY_ANGLE);
                //}
                //Constants.Instance.GRAVITY_ACTIVE = true;
            }
            else
            {
                //foreach (Circle c in circles)
                //{
                //    c.updateAcceleration(Constants.Instance.GRAVITY, -1*Constants.Instance.GRAVITY_ANGLE);
                //}
                //Constants.Instance.GRAVITY_ACTIVE = false;
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.ShiftKey)
            {
                System.Diagnostics.Debug.WriteLine("TETHER ON");
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
