namespace remonduk
{
    partial class MainWindow
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
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_velocity_up_down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_acceleration_up_down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_velocity_angle_up_down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_acceleration_angle_up_down)).BeginInit();
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
            this.new_circle_velocity_up_down.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.new_circle_velocity_up_down_KeyPress);
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
            this.new_circle_velocity_label.Click += new System.EventHandler(this.label1_Click);
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
            this.new_circle_acceleration_up_down.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.new_circle_acceleration_up_down_KeyPress);
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
            this.new_circle_velocity_angle_up_down.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.new_circle_velocity_angle_up_down_KeyPress);
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
            this.new_circle_acceleration_angle_up_down.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.new_circle_acceleration_angle_up_down_KeyPress);
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
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(791, 606);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.new_circle_acceleration_angle_up_down);
            this.Controls.Add(this.new_circle_velocity_angle_up_down);
            this.Controls.Add(this.new_circle_acceleration_label);
            this.Controls.Add(this.new_circle_acceleration_up_down);
            this.Controls.Add(this.new_circle_velocity_label);
            this.Controls.Add(this.new_circle_velocity_up_down);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainWindow_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_velocity_up_down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_acceleration_up_down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_velocity_angle_up_down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.new_circle_acceleration_angle_up_down)).EndInit();
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
    }
}

