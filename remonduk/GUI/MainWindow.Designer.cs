namespace Remonduk
{
    public partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.new_circle_velocity_up_down = new System.Windows.Forms.NumericUpDown();
            this.new_circle_velocity_label = new System.Windows.Forms.Label();
            this.new_circle_acceleration_label = new System.Windows.Forms.Label();
            this.new_circle_acceleration_up_down = new System.Windows.Forms.NumericUpDown();
            this.new_circle_velocity_angle_up_down = new System.Windows.Forms.NumericUpDown();
            this.new_circle_acceleration_angle_up_down = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.draw_menu_item = new System.Windows.Forms.ToolStripMenuItem();
            this.groupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shape_menu_item = new System.Windows.Forms.ToolStripMenuItem();
            this.circle_menu_item = new System.Windows.Forms.ToolStripMenuItem();
            this.worldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gravityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gravity_toggle_menu_item = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.interactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.system_menu_item = new System.Windows.Forms.ToolStripMenuItem();
            this.load_menu_item = new System.Windows.Forms.ToolStripMenuItem();
            this.save_menu_item = new System.Windows.Forms.ToolStripMenuItem();
            this.new_menu_item = new System.Windows.Forms.ToolStripMenuItem();
            this.circle_radius_up_down = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_velocity_up_down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_acceleration_up_down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_velocity_angle_up_down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_acceleration_angle_up_down)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.circle_radius_up_down)).BeginInit();
            this.SuspendLayout();
            // 
            // new_circle_velocity_up_down
            // 
            this.new_circle_velocity_up_down.DecimalPlaces = 3;
            this.new_circle_velocity_up_down.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.new_circle_velocity_up_down.Location = new System.Drawing.Point(98, 12);
            this.new_circle_velocity_up_down.Name = "new_circle_velocity_up_down";
            this.new_circle_velocity_up_down.Size = new System.Drawing.Size(70, 20);
            this.new_circle_velocity_up_down.TabIndex = 0;
            this.new_circle_velocity_up_down.ValueChanged += new System.EventHandler(this.new_circle_velocity_up_down_ValueChanged);
            this.new_circle_velocity_up_down.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.new_circle_velocity_up_down.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainWindow_KeyPress);
            this.new_circle_velocity_up_down.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyUp);
            // 
            // new_circle_velocity_label
            // 
            this.new_circle_velocity_label.AutoSize = true;
            this.new_circle_velocity_label.BackColor = System.Drawing.Color.Transparent;
            this.new_circle_velocity_label.Location = new System.Drawing.Point(21, 14);
            this.new_circle_velocity_label.Name = "new_circle_velocity_label";
            this.new_circle_velocity_label.Size = new System.Drawing.Size(47, 13);
            this.new_circle_velocity_label.TabIndex = 1;
            this.new_circle_velocity_label.Text = "Velocity:";
            // 
            // new_circle_acceleration_label
            // 
            this.new_circle_acceleration_label.AutoSize = true;
            this.new_circle_acceleration_label.BackColor = System.Drawing.Color.Transparent;
            this.new_circle_acceleration_label.Location = new System.Drawing.Point(21, 48);
            this.new_circle_acceleration_label.Name = "new_circle_acceleration_label";
            this.new_circle_acceleration_label.Size = new System.Drawing.Size(69, 13);
            this.new_circle_acceleration_label.TabIndex = 3;
            this.new_circle_acceleration_label.Text = "Acceleration:";
            // 
            // new_circle_acceleration_up_down
            // 
            this.new_circle_acceleration_up_down.DecimalPlaces = 3;
            this.new_circle_acceleration_up_down.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.new_circle_acceleration_up_down.Location = new System.Drawing.Point(98, 46);
            this.new_circle_acceleration_up_down.Name = "new_circle_acceleration_up_down";
            this.new_circle_acceleration_up_down.Size = new System.Drawing.Size(70, 20);
            this.new_circle_acceleration_up_down.TabIndex = 2;
            this.new_circle_acceleration_up_down.ValueChanged += new System.EventHandler(this.new_circle_acceleration_up_down_ValueChanged);
            this.new_circle_acceleration_up_down.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.new_circle_acceleration_up_down.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainWindow_KeyPress);
            this.new_circle_acceleration_up_down.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyUp);
            // 
            // new_circle_velocity_angle_up_down
            // 
            this.new_circle_velocity_angle_up_down.Location = new System.Drawing.Point(15, 170);
            this.new_circle_velocity_angle_up_down.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.new_circle_velocity_angle_up_down.Name = "new_circle_velocity_angle_up_down";
            this.new_circle_velocity_angle_up_down.Size = new System.Drawing.Size(70, 20);
            this.new_circle_velocity_angle_up_down.TabIndex = 4;
            this.new_circle_velocity_angle_up_down.ValueChanged += new System.EventHandler(this.new_circle_velocity_angle_up_down_ValueChanged);
            this.new_circle_velocity_angle_up_down.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.new_circle_velocity_angle_up_down.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainWindow_KeyPress);
            this.new_circle_velocity_angle_up_down.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyUp);
            // 
            // new_circle_acceleration_angle_up_down
            // 
            this.new_circle_acceleration_angle_up_down.Location = new System.Drawing.Point(125, 170);
            this.new_circle_acceleration_angle_up_down.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.new_circle_acceleration_angle_up_down.Name = "new_circle_acceleration_angle_up_down";
            this.new_circle_acceleration_angle_up_down.Size = new System.Drawing.Size(70, 20);
            this.new_circle_acceleration_angle_up_down.TabIndex = 5;
            this.new_circle_acceleration_angle_up_down.ValueChanged += new System.EventHandler(this.new_circle_acceleration_angle_up_down_ValueChanged);
            this.new_circle_acceleration_angle_up_down.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.new_circle_acceleration_angle_up_down.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainWindow_KeyPress);
            this.new_circle_acceleration_angle_up_down.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(9, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Velocity Angle";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(109, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Acceleration Angle";
            // 
            // menu
            // 
            this.menu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.draw_menu_item,
            this.worldToolStripMenuItem,
            this.system_menu_item});
            this.menu.Location = new System.Drawing.Point(0, 582);
            this.menu.Name = "menu";
            this.menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menu.Size = new System.Drawing.Size(791, 24);
            this.menu.TabIndex = 8;
            this.menu.Text = "menuStrip1";
            // 
            // draw_menu_item
            // 
            this.draw_menu_item.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.groupToolStripMenuItem,
            this.shape_menu_item,
            this.circle_menu_item});
            this.draw_menu_item.Name = "draw_menu_item";
            this.draw_menu_item.Size = new System.Drawing.Size(44, 20);
            this.draw_menu_item.Text = "Draw";
            // 
            // groupToolStripMenuItem
            // 
            this.groupToolStripMenuItem.Name = "groupToolStripMenuItem";
            this.groupToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.G)));
            this.groupToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.groupToolStripMenuItem.Text = "Group";
            this.groupToolStripMenuItem.Click += new System.EventHandler(this.groupToolStripMenuItem_Click);
            // 
            // shape_menu_item
            // 
            this.shape_menu_item.Name = "shape_menu_item";
            this.shape_menu_item.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.shape_menu_item.Size = new System.Drawing.Size(152, 22);
            this.shape_menu_item.Text = "Shape";
            // 
            // circle_menu_item
            // 
            this.circle_menu_item.Name = "circle_menu_item";
            this.circle_menu_item.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.circle_menu_item.Size = new System.Drawing.Size(152, 22);
            this.circle_menu_item.Text = "Circle";
            // 
            // worldToolStripMenuItem
            // 
            this.worldToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gravityToolStripMenuItem,
            this.detailsToolStripMenuItem,
            this.interactionsToolStripMenuItem});
            this.worldToolStripMenuItem.Name = "worldToolStripMenuItem";
            this.worldToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.worldToolStripMenuItem.Text = "World";
            // 
            // gravityToolStripMenuItem
            // 
            this.gravityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gravity_toggle_menu_item});
            this.gravityToolStripMenuItem.Name = "gravityToolStripMenuItem";
            this.gravityToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.gravityToolStripMenuItem.Text = "Gravity";
            // 
            // gravity_toggle_menu_item
            // 
            this.gravity_toggle_menu_item.CheckOnClick = true;
            this.gravity_toggle_menu_item.Name = "gravity_toggle_menu_item";
            this.gravity_toggle_menu_item.Size = new System.Drawing.Size(88, 22);
            this.gravity_toggle_menu_item.Text = "On";
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            this.detailsToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.detailsToolStripMenuItem.Text = "Details";
            this.detailsToolStripMenuItem.Click += new System.EventHandler(this.detailsToolStripMenuItem_Click);
            // 
            // interactionsToolStripMenuItem
            // 
            this.interactionsToolStripMenuItem.Name = "interactionsToolStripMenuItem";
            this.interactionsToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.interactionsToolStripMenuItem.Text = "Interactions";
            this.interactionsToolStripMenuItem.Click += new System.EventHandler(this.interactionsToolStripMenuItem_Click);
            // 
            // system_menu_item
            // 
            this.system_menu_item.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.load_menu_item,
            this.save_menu_item,
            this.new_menu_item});
            this.system_menu_item.Name = "system_menu_item";
            this.system_menu_item.Size = new System.Drawing.Size(54, 20);
            this.system_menu_item.Text = "System";
            // 
            // load_menu_item
            // 
            this.load_menu_item.Name = "load_menu_item";
            this.load_menu_item.ShortcutKeyDisplayString = "";
            this.load_menu_item.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.load_menu_item.Size = new System.Drawing.Size(137, 22);
            this.load_menu_item.Text = "Load";
            this.load_menu_item.Click += new System.EventHandler(this.load_menu_item_Click);
            // 
            // save_menu_item
            // 
            this.save_menu_item.Name = "save_menu_item";
            this.save_menu_item.ShortcutKeyDisplayString = "";
            this.save_menu_item.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.save_menu_item.Size = new System.Drawing.Size(137, 22);
            this.save_menu_item.Text = "Save";
            this.save_menu_item.Click += new System.EventHandler(this.save_menu_item_Click);
            // 
            // new_menu_item
            // 
            this.new_menu_item.Name = "new_menu_item";
            this.new_menu_item.ShortcutKeyDisplayString = "";
            this.new_menu_item.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.new_menu_item.Size = new System.Drawing.Size(137, 22);
            this.new_menu_item.Text = "New";
            // 
            // circle_radius_up_down
            // 
            this.circle_radius_up_down.Location = new System.Drawing.Point(92, 253);
            this.circle_radius_up_down.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.circle_radius_up_down.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.circle_radius_up_down.Name = "circle_radius_up_down";
            this.circle_radius_up_down.Size = new System.Drawing.Size(46, 20);
            this.circle_radius_up_down.TabIndex = 9;
            this.circle_radius_up_down.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.circle_radius_up_down.ValueChanged += new System.EventHandler(this.circle_radius_up_down_ValueChanged);
            this.circle_radius_up_down.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.circle_radius_up_down.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainWindow_KeyPress);
            this.circle_radius_up_down.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 255);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "R(but really D)";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(791, 606);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.circle_radius_up_down);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.new_circle_acceleration_angle_up_down);
            this.Controls.Add(this.new_circle_velocity_angle_up_down);
            this.Controls.Add(this.new_circle_acceleration_label);
            this.Controls.Add(this.new_circle_acceleration_up_down);
            this.Controls.Add(this.new_circle_velocity_label);
            this.Controls.Add(this.new_circle_velocity_up_down);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainWindow";
            this.Text = "Circle_Detail_Window";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainWindow_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_velocity_up_down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_acceleration_up_down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_velocity_angle_up_down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_acceleration_angle_up_down)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.circle_radius_up_down)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown new_circle_velocity_up_down;
        private System.Windows.Forms.Label new_circle_velocity_label;
        private System.Windows.Forms.Label new_circle_acceleration_label;
        private System.Windows.Forms.NumericUpDown new_circle_acceleration_up_down;
        private System.Windows.Forms.NumericUpDown new_circle_velocity_angle_up_down;
        private System.Windows.Forms.NumericUpDown new_circle_acceleration_angle_up_down;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem draw_menu_item;
        private System.Windows.Forms.ToolStripMenuItem shape_menu_item;
        private System.Windows.Forms.ToolStripMenuItem circle_menu_item;
        private System.Windows.Forms.ToolStripMenuItem system_menu_item;
        private System.Windows.Forms.ToolStripMenuItem load_menu_item;
        private System.Windows.Forms.ToolStripMenuItem save_menu_item;
        private System.Windows.Forms.ToolStripMenuItem new_menu_item;
        private System.Windows.Forms.ToolStripMenuItem groupToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown circle_radius_up_down;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem worldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gravityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gravity_toggle_menu_item;
        private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem interactionsToolStripMenuItem;
    }
}

