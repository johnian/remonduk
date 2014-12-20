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
    public partial class Physical_System_Detail_Window : Form
    {
        PhysicalSystem ps;
        public Interaction selected_interaction;
        public Circle selected_circle;
        int last_interaction_count;
        int last_circle_count;

        public Physical_System_Detail_Window()
        {
            InitializeComponent();
        }

        public Physical_System_Detail_Window(PhysicalSystem ps)
        {
            InitializeComponent();
            this.ps = ps;
            selected_interaction = null;
            selected_circle = null;
            update_ps(ps);
        }

        public void update_ps(PhysicalSystem ps)
        {
            if(last_interaction_count != ps.interactions.Count)
            {
                interactions_list.DataSource = null;
                interactions_list.DataSource = ps.interactions;
                this.Invalidate();
                last_interaction_count = ps.interactions.Count;
            }
            if(last_circle_count != ps.netForces.Keys.Count)
            {
                circle_list.DataSource = null;
                circle_list.DataSource = ps.netForces.Keys.ToArray();
                this.Invalidate();
                last_circle_count = ps.netForces.Keys.Count;
            }
            if (interactions_list.SelectedItem != null)
            {
                selected_interaction = (Interaction)interactions_list.SelectedItem;
            }
            if(circle_list.SelectedItem != null)
            {
                selected_circle = (Circle)circle_list.SelectedItem;
            }
        }

        private void interactions_remove_button_Click(object sender, EventArgs e)
        {
            ps.removeInteraction(selected_interaction);
        }

        private void circle_list_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            
        }

        private void circle_list_MouseClick(object sender, MouseEventArgs e)
        {
            selected_circle = (Circle)circle_list.SelectedItem;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ps.removeCircle(selected_circle);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selected_circle != null)
            {
                GUI.Circle_Detail_Window cdw = GUI_Singleton.Instance.cdw;
                cdw.update_circle(selected_circle, ps.interactions);
                cdw.Visible = true;
                cdw.Focus();
            }
        }
    }
}
