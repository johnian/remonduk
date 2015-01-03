using System;
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
    public partial class Interaction_Detail_Window : Form
    {
        Interaction interaction;

        public Interaction_Detail_Window()
        {
            InitializeComponent();
            interaction = null;

        }
        public Interaction_Detail_Window(Interaction interaction)
        {
            InitializeComponent();
            this.interaction = interaction;
            update_interaction(interaction);
        }

        public void update_interaction(Interaction interaction)
        {
            this.interaction = interaction;
            if (interaction != null)
            {
                circle_1_acceleration_angle_up_down.Value =
                    (Decimal)(interaction.First.Acceleration.Angle() * 180.0 / Math.PI);
                circle_1_acceleration_up_down.Value =
                    (Decimal)interaction.First.Acceleration.Magnitude();
                circle_1_velocity_angle_up_down.Value =
                    (Decimal)(interaction.First.Velocity.Angle() * 180.0 / Math.PI);
                circle_1_velocity_up_down.Value =
                    (Decimal)interaction.First.Velocity.Magnitude();

                circle_2_acceleration_angle_updown.Value =
                    (Decimal)(interaction.Second.Acceleration.Angle() * 180.0 / Math.PI);
                circle_2_acceleration_updown.Value =
                    (Decimal)interaction.Second.Acceleration.Magnitude();
                circle_2_velocity_angle_up_down.Value =
                    (Decimal)(interaction.Second.Velocity.Angle() * 180.0 / Math.PI);
                circle_2_velocity_updown.Value =
                    (Decimal)interaction.Second.Velocity.Magnitude();

                Graphics g = this.CreateGraphics();
                Pen pen = new Pen(Color.Black);
                double theta = (double)interaction.First.Velocity.Angle();
                float x1 = 42.5F;
                float y1 = 167.5F;
                float x2 = (float)Math.Cos(theta) * 25 + x1;
                float y2 = (float)Math.Sin(theta) * 25 + y1;
                g.DrawLine(pen, x1, y1, x2, y2);
                g.DrawEllipse(pen, 5, 130, 75, 75);


                theta = (double)interaction.First.Acceleration.Angle();
                //Out.WriteLine(new_circle_acceleration_angle_up_down.Value);
                x1 = 152.5F;
                y1 = 167.5F;
                x2 = (float)Math.Cos(theta) * 25 + x1;
                y2 = (float)Math.Sin(theta) * 25 + y1;
                g.DrawLine(pen, x1, y1, x2, y2);
                g.DrawEllipse(pen, 115, 130, 75, 75);

                theta = (double)interaction.Second.Velocity.Angle();
                x1 = 237.5F;
                y1 = 167.5F;
                x2 = (float)Math.Cos(theta) * 25 + x1;
                y2 = (float)Math.Sin(theta) * 25 + y1;
                g.DrawLine(pen, x1, y1, x2, y2);
                g.DrawEllipse(pen, 200, 130, 75, 75);


                theta = (double)interaction.Second.Acceleration.Angle();
                //Out.WriteLine(new_circle_acceleration_angle_up_down.Value);
                x1 = 357.5F;
                y1 = 167.5F;
                x2 = (float)Math.Cos(theta) * 25 + x1;
                y2 = (float)Math.Sin(theta) * 25 + y1;
                g.DrawLine(pen, x1, y1, x2, y2);
                g.DrawEllipse(pen, 310, 130, 75, 75);
            }
            Invalidate();
        }
    }
}
