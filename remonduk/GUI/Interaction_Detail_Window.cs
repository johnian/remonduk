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
                    (Decimal)(interaction.first.acceleration_angle * 180.0 / Math.PI);
                circle_1_acceleration_up_down.Value =
                    (Decimal)interaction.first.acceleration;
                circle_1_velocity_angle_up_down.Value =
                    (Decimal)(interaction.first.velocity_angle * 180.0 / Math.PI);
                circle_1_velocity_up_down.Value =
                    (Decimal)interaction.first.velocity;

                circle_2_acceleration_angle_updown.Value =
                    (Decimal)(interaction.second.acceleration_angle * 180.0 / Math.PI);
                circle_2_acceleration_updown.Value =
                    (Decimal)interaction.second.acceleration;
                circle_2_velocity_angle_up_down.Value =
                    (Decimal)(interaction.second.velocity_angle * 180.0 / Math.PI);
                circle_2_velocity_updown.Value =
                    (Decimal)interaction.second.velocity;
            }
            Invalidate();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
