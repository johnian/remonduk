using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Remonduk.Physics;

namespace Remonduk.GUI
{
	public partial class PhysicalSystemDetailWindow : Form
	{
		public PhysicalSystem ps;
		public Interaction SelectedInteraction;
		public Circle SelectedCircle;
		public int LastInteractionCount;
		public int LastCircleCount;

		public PhysicalSystemDetailWindow()
		{
			InitializeComponent();
		}

		public PhysicalSystemDetailWindow(PhysicalSystem ps)
		{
			InitializeComponent();
			this.ps = ps;
			SelectedInteraction = null;
			SelectedCircle = null;
			update_ps(ps);
		}

		public void update_ps(PhysicalSystem ps)
		{
			if (LastInteractionCount != ps.Interactions.Count)
			{
				interactions_list.DataSource = null;
				interactions_list.DataSource = ps.Interactions;
				this.Invalidate();
				LastInteractionCount = ps.Interactions.Count;
			}
			//if(LastCircleCount != ps.NetForces.Keys.Count)
			//{
			//	circle_list.DataSource = null;
			//	circle_list.DataSource = ps.NetForces.Keys.ToArray();
			//	this.Invalidate();
			//	LastCircleCount = ps.NetForces.Keys.Count;
			//}
			if (LastCircleCount != ps.Circles.Count)
			{
				circle_list.DataSource = null;
				circle_list.DataSource = ps.Circles.ToArray();
				this.Invalidate();
				LastCircleCount = ps.Circles.Count;
			}
			if (interactions_list.SelectedItem != null)
			{
				SelectedInteraction = (Interaction)interactions_list.SelectedItem;
			}
			if (circle_list.SelectedItem != null)
			{
				SelectedCircle = (Circle)circle_list.SelectedItem;
			}
		}

		private void interactions_remove_button_Click(object sender, EventArgs e)
		{
			ps.RemoveInteraction(SelectedInteraction);
		}

		private void circle_list_MouseDoubleClick(object sender, MouseEventArgs e)
		{


		}

		private void circle_list_MouseClick(object sender, MouseEventArgs e)
		{
			SelectedCircle = (Circle)circle_list.SelectedItem;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			ps.RemoveCircle(SelectedCircle);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (SelectedCircle != null)
			{
				GUI.CircleDetailWindow cdw = GUISingleton.Instance.Cdw;
				cdw.update_circle(SelectedCircle, ps.Interactions);
				cdw.Visible = true;
				cdw.Focus();
			}
		}

		private void interactions_list_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
