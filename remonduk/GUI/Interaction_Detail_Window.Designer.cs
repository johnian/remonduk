namespace Remonduk.GUI
{
    partial class Interaction_Detail_Window
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.circle_1_acceleration_angle_up_down = new System.Windows.Forms.NumericUpDown();
            this.circle_1_velocity_angle_up_down = new System.Windows.Forms.NumericUpDown();
            this.circle_1__acceleration_label = new System.Windows.Forms.Label();
            this.circle_1_acceleration_up_down = new System.Windows.Forms.NumericUpDown();
            this.circle_1_velocity_label = new System.Windows.Forms.Label();
            this.circle_1_velocity_up_down = new System.Windows.Forms.NumericUpDown();
            this.circle_1_label = new System.Windows.Forms.Label();
            this.circle_2_label = new System.Windows.Forms.Label();
            this.circle_2_acceleration_angle = new System.Windows.Forms.Label();
            this.circle_2_acceleration_updown = new System.Windows.Forms.NumericUpDown();
            this.circle_2_velocity_label = new System.Windows.Forms.Label();
            this.circle_2_velocity_updown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.circle_2_acceleration_angle_updown = new System.Windows.Forms.NumericUpDown();
            this.circle_2_velocity_angle_up_down = new System.Windows.Forms.NumericUpDown();
            this.k_updown = new System.Windows.Forms.NumericUpDown();
            this.eq_updown = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.circle_1_acceleration_angle_up_down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circle_1_velocity_angle_up_down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circle_1_acceleration_up_down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circle_1_velocity_up_down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circle_2_acceleration_updown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circle_2_velocity_updown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circle_2_acceleration_angle_updown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circle_2_velocity_angle_up_down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.k_updown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eq_updown)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(102, 271);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Acceleration Angle";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(2, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Velocity Angle";
            // 
            // circle_1_acceleration_angle_up_down
            // 
            this.circle_1_acceleration_angle_up_down.Location = new System.Drawing.Point(118, 235);
            this.circle_1_acceleration_angle_up_down.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.circle_1_acceleration_angle_up_down.Name = "circle_1_acceleration_angle_up_down";
            this.circle_1_acceleration_angle_up_down.Size = new System.Drawing.Size(70, 20);
            this.circle_1_acceleration_angle_up_down.TabIndex = 22;
            // 
            // circle_1_velocity_angle_up_down
            // 
            this.circle_1_velocity_angle_up_down.Location = new System.Drawing.Point(8, 235);
            this.circle_1_velocity_angle_up_down.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.circle_1_velocity_angle_up_down.Name = "circle_1_velocity_angle_up_down";
            this.circle_1_velocity_angle_up_down.Size = new System.Drawing.Size(70, 20);
            this.circle_1_velocity_angle_up_down.TabIndex = 21;
            // 
            // circle_1__acceleration_label
            // 
            this.circle_1__acceleration_label.AutoSize = true;
            this.circle_1__acceleration_label.BackColor = System.Drawing.Color.Transparent;
            this.circle_1__acceleration_label.Location = new System.Drawing.Point(8, 101);
            this.circle_1__acceleration_label.Name = "circle_1__acceleration_label";
            this.circle_1__acceleration_label.Size = new System.Drawing.Size(69, 13);
            this.circle_1__acceleration_label.TabIndex = 28;
            this.circle_1__acceleration_label.Text = "Acceleration:";
            // 
            // circle_1_acceleration_up_down
            // 
            this.circle_1_acceleration_up_down.DecimalPlaces = 3;
            this.circle_1_acceleration_up_down.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.circle_1_acceleration_up_down.Location = new System.Drawing.Point(118, 94);
            this.circle_1_acceleration_up_down.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.circle_1_acceleration_up_down.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.circle_1_acceleration_up_down.Name = "circle_1_acceleration_up_down";
            this.circle_1_acceleration_up_down.ReadOnly = true;
            this.circle_1_acceleration_up_down.Size = new System.Drawing.Size(70, 20);
            this.circle_1_acceleration_up_down.TabIndex = 27;
            // 
            // circle_1_velocity_label
            // 
            this.circle_1_velocity_label.AutoSize = true;
            this.circle_1_velocity_label.BackColor = System.Drawing.Color.Transparent;
            this.circle_1_velocity_label.Location = new System.Drawing.Point(8, 67);
            this.circle_1_velocity_label.Name = "circle_1_velocity_label";
            this.circle_1_velocity_label.Size = new System.Drawing.Size(47, 13);
            this.circle_1_velocity_label.TabIndex = 26;
            this.circle_1_velocity_label.Text = "Velocity:";
            // 
            // circle_1_velocity_up_down
            // 
            this.circle_1_velocity_up_down.DecimalPlaces = 3;
            this.circle_1_velocity_up_down.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.circle_1_velocity_up_down.Location = new System.Drawing.Point(118, 65);
            this.circle_1_velocity_up_down.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.circle_1_velocity_up_down.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.circle_1_velocity_up_down.Name = "circle_1_velocity_up_down";
            this.circle_1_velocity_up_down.ReadOnly = true;
            this.circle_1_velocity_up_down.Size = new System.Drawing.Size(70, 20);
            this.circle_1_velocity_up_down.TabIndex = 25;
            // 
            // circle_1_label
            // 
            this.circle_1_label.AutoSize = true;
            this.circle_1_label.Location = new System.Drawing.Point(72, 26);
            this.circle_1_label.Name = "circle_1_label";
            this.circle_1_label.Size = new System.Drawing.Size(42, 13);
            this.circle_1_label.TabIndex = 29;
            this.circle_1_label.Text = "Circle 1";
            // 
            // circle_2_label
            // 
            this.circle_2_label.AutoSize = true;
            this.circle_2_label.Location = new System.Drawing.Point(272, 26);
            this.circle_2_label.Name = "circle_2_label";
            this.circle_2_label.Size = new System.Drawing.Size(42, 13);
            this.circle_2_label.TabIndex = 38;
            this.circle_2_label.Text = "Circle 2";
            // 
            // circle_2_acceleration_angle
            // 
            this.circle_2_acceleration_angle.AutoSize = true;
            this.circle_2_acceleration_angle.BackColor = System.Drawing.Color.Transparent;
            this.circle_2_acceleration_angle.Location = new System.Drawing.Point(211, 101);
            this.circle_2_acceleration_angle.Name = "circle_2_acceleration_angle";
            this.circle_2_acceleration_angle.Size = new System.Drawing.Size(69, 13);
            this.circle_2_acceleration_angle.TabIndex = 37;
            this.circle_2_acceleration_angle.Text = "Acceleration:";
            // 
            // circle_2_acceleration_updown
            // 
            this.circle_2_acceleration_updown.DecimalPlaces = 3;
            this.circle_2_acceleration_updown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.circle_2_acceleration_updown.Location = new System.Drawing.Point(321, 99);
            this.circle_2_acceleration_updown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.circle_2_acceleration_updown.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.circle_2_acceleration_updown.Name = "circle_2_acceleration_updown";
            this.circle_2_acceleration_updown.ReadOnly = true;
            this.circle_2_acceleration_updown.Size = new System.Drawing.Size(70, 20);
            this.circle_2_acceleration_updown.TabIndex = 36;
            // 
            // circle_2_velocity_label
            // 
            this.circle_2_velocity_label.AutoSize = true;
            this.circle_2_velocity_label.BackColor = System.Drawing.Color.Transparent;
            this.circle_2_velocity_label.Location = new System.Drawing.Point(211, 67);
            this.circle_2_velocity_label.Name = "circle_2_velocity_label";
            this.circle_2_velocity_label.Size = new System.Drawing.Size(47, 13);
            this.circle_2_velocity_label.TabIndex = 35;
            this.circle_2_velocity_label.Text = "Velocity:";
            // 
            // circle_2_velocity_updown
            // 
            this.circle_2_velocity_updown.DecimalPlaces = 3;
            this.circle_2_velocity_updown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.circle_2_velocity_updown.Location = new System.Drawing.Point(321, 67);
            this.circle_2_velocity_updown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.circle_2_velocity_updown.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.circle_2_velocity_updown.Name = "circle_2_velocity_updown";
            this.circle_2_velocity_updown.ReadOnly = true;
            this.circle_2_velocity_updown.Size = new System.Drawing.Size(70, 20);
            this.circle_2_velocity_updown.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(305, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Acceleration Angle";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(205, 271);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "Velocity Angle";
            // 
            // circle_2_acceleration_angle_updown
            // 
            this.circle_2_acceleration_angle_updown.Location = new System.Drawing.Point(321, 235);
            this.circle_2_acceleration_angle_updown.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.circle_2_acceleration_angle_updown.Name = "circle_2_acceleration_angle_updown";
            this.circle_2_acceleration_angle_updown.Size = new System.Drawing.Size(70, 20);
            this.circle_2_acceleration_angle_updown.TabIndex = 31;
            // 
            // circle_2_velocity_angle_up_down
            // 
            this.circle_2_velocity_angle_up_down.Location = new System.Drawing.Point(211, 235);
            this.circle_2_velocity_angle_up_down.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.circle_2_velocity_angle_up_down.Name = "circle_2_velocity_angle_up_down";
            this.circle_2_velocity_angle_up_down.Size = new System.Drawing.Size(70, 20);
            this.circle_2_velocity_angle_up_down.TabIndex = 30;
            // 
            // k_updown
            // 
            this.k_updown.Location = new System.Drawing.Point(491, 67);
            this.k_updown.Name = "k_updown";
            this.k_updown.Size = new System.Drawing.Size(87, 20);
            this.k_updown.TabIndex = 39;
            // 
            // eq_updown
            // 
            this.eq_updown.Location = new System.Drawing.Point(491, 99);
            this.eq_updown.Name = "eq_updown";
            this.eq_updown.Size = new System.Drawing.Size(87, 20);
            this.eq_updown.TabIndex = 40;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(413, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 13);
            this.label9.TabIndex = 42;
            this.label9.Text = "EQUILIBRIUM";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(413, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 13);
            this.label10.TabIndex = 41;
            this.label10.Text = "K";
            // 
            // Interaction_Detail_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 288);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.eq_updown);
            this.Controls.Add(this.k_updown);
            this.Controls.Add(this.circle_2_label);
            this.Controls.Add(this.circle_2_acceleration_angle);
            this.Controls.Add(this.circle_2_acceleration_updown);
            this.Controls.Add(this.circle_2_velocity_label);
            this.Controls.Add(this.circle_2_velocity_updown);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.circle_2_acceleration_angle_updown);
            this.Controls.Add(this.circle_2_velocity_angle_up_down);
            this.Controls.Add(this.circle_1_label);
            this.Controls.Add(this.circle_1__acceleration_label);
            this.Controls.Add(this.circle_1_acceleration_up_down);
            this.Controls.Add(this.circle_1_velocity_label);
            this.Controls.Add(this.circle_1_velocity_up_down);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.circle_1_acceleration_angle_up_down);
            this.Controls.Add(this.circle_1_velocity_angle_up_down);
            this.Name = "Interaction_Detail_Window";
            this.Text = "Interaction_Detail_Window";
            ((System.ComponentModel.ISupportInitialize)(this.circle_1_acceleration_angle_up_down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circle_1_velocity_angle_up_down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circle_1_acceleration_up_down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circle_1_velocity_up_down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circle_2_acceleration_updown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circle_2_velocity_updown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circle_2_acceleration_angle_updown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circle_2_velocity_angle_up_down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.k_updown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eq_updown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown circle_1_acceleration_angle_up_down;
        private System.Windows.Forms.NumericUpDown circle_1_velocity_angle_up_down;
        private System.Windows.Forms.Label circle_1__acceleration_label;
        private System.Windows.Forms.NumericUpDown circle_1_acceleration_up_down;
        private System.Windows.Forms.Label circle_1_velocity_label;
        private System.Windows.Forms.NumericUpDown circle_1_velocity_up_down;
        private System.Windows.Forms.Label circle_1_label;
        private System.Windows.Forms.Label circle_2_label;
        private System.Windows.Forms.Label circle_2_acceleration_angle;
        private System.Windows.Forms.NumericUpDown circle_2_acceleration_updown;
        private System.Windows.Forms.Label circle_2_velocity_label;
        private System.Windows.Forms.NumericUpDown circle_2_velocity_updown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown circle_2_acceleration_angle_updown;
        private System.Windows.Forms.NumericUpDown circle_2_velocity_angle_up_down;
        private System.Windows.Forms.NumericUpDown k_updown;
        private System.Windows.Forms.NumericUpDown eq_updown;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}