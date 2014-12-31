﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace remonduk.GUI
{
    public partial class Circle_Detail_Window : Form
    {
        Circle c;

        public Circle_Detail_Window()
        {
            InitializeComponent();
        }

        public Circle_Detail_Window(Circle c, List<Interaction> interactions)
        {
            InitializeComponent();
            this.c = c;
            color_button.BackColor = c.color;
            update_circle(c, interactions);
            this.Paint += new PaintEventHandler(this.drawNewCircleAngles);
            
        }

        void drawNewCircleAngles(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Color.Black);
            double theta = (double)velocity_angle_up_down.Value;
            float x1 = 52.5F;
            float y1 = 127.5F;
            float x2 = (float)Math.Cos(theta * (Math.PI / 180.0)) * 25 + x1;
            float y2 = (float)Math.Sin(theta * (Math.PI / 180.0)) * 25 + y1;
            g.DrawLine(pen, x1, y1, x2, y2);
            g.DrawEllipse(pen, 15, 90, 75, 75);


            theta = (double)acceleration_angle_up_down.Value;
            //Out.WriteLine(new_circle_acceleration_angle_up_down.Value);
            x1 = 162.5F;
            y1 = 127.5F;
            x2 = (float)Math.Cos(theta * (Math.PI / 180.0)) * 25 + x1;
            y2 = (float)Math.Sin(theta * (Math.PI / 180.0)) * 25 + y1;
            g.DrawLine(pen, x1, y1, x2, y2);
            g.DrawEllipse(pen, 125, 90, 75, 75);
        }

        public void update_circle(Circle c, List<Interaction> interactions)
        {
            acceleration_up_down.Value = (Decimal)c.acceleration.magnitude();
            velocity_up_down.Value = (Decimal)c.velocity.magnitude();
            acceleration_angle_up_down.Value = (Decimal)(c.acceleration.angle() * 180.0/Math.PI);
            velocity_angle_up_down.Value = (Decimal)(c.velocity.angle() * 180.0/Math.PI);
            List<Interaction> real = new List<Interaction>();
            foreach(Interaction interaction in interactions)
            {
                if(interaction.first == c || interaction.second == c)
                {
                    real.Add(interaction);
                }
            }
            this.interactions_list.DataSource = real;
            this.interactions_list.DisplayMember = "Force";
            color_button.BackColor = c.color;
            this.c = c;
            this.Invalidate();
        }

        private void color_button_Click(object sender, EventArgs e)
        {
            circle_color_dialog.ShowDialog();
            c.color = circle_color_dialog.Color;
            color_button.BackColor = circle_color_dialog.Color;
        }
    }
}
