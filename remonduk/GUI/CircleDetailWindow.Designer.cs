namespace Remonduk.GUI
{
	partial class CircleDetailWindow
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
			this.ok_button = new System.Windows.Forms.Button();
			this.cancel_button = new System.Windows.Forms.Button();
			this.interactions_list = new System.Windows.Forms.ListBox();
			this.interactions_list_label = new System.Windows.Forms.Label();
			this.group_list = new System.Windows.Forms.ComboBox();
			this.group_list_label = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.acceleration_angle_up_down = new System.Windows.Forms.NumericUpDown();
			this.velocity_angle_up_down = new System.Windows.Forms.NumericUpDown();
			this.new_circle_acceleration_label = new System.Windows.Forms.Label();
			this.acceleration_up_down = new System.Windows.Forms.NumericUpDown();
			this.new_circle_velocity_label = new System.Windows.Forms.Label();
			this.velocity_up_down = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.circle_radius_up_down = new System.Windows.Forms.NumericUpDown();
			this.circle_color_dialog = new System.Windows.Forms.ColorDialog();
			this.color_button = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.acceleration_angle_up_down)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.velocity_angle_up_down)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.acceleration_up_down)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.velocity_up_down)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.circle_radius_up_down)).BeginInit();
			this.SuspendLayout();
			// 
			// ok_button
			// 
			this.ok_button.Location = new System.Drawing.Point(440, 207);
			this.ok_button.Name = "ok_button";
			this.ok_button.Size = new System.Drawing.Size(78, 27);
			this.ok_button.TabIndex = 0;
			this.ok_button.Text = "Ok";
			this.ok_button.UseVisualStyleBackColor = true;
			// 
			// cancel_button
			// 
			this.cancel_button.Location = new System.Drawing.Point(558, 207);
			this.cancel_button.Name = "cancel_button";
			this.cancel_button.Size = new System.Drawing.Size(78, 27);
			this.cancel_button.TabIndex = 1;
			this.cancel_button.Text = "cancel";
			this.cancel_button.UseVisualStyleBackColor = true;
			// 
			// interactions_list
			// 
			this.interactions_list.Font = new System.Drawing.Font("Colonna MT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.interactions_list.FormattingEnabled = true;
			this.interactions_list.Items.AddRange(new object[] {
            "Interaction 1",
            "Interaction 2",
            "Interaction 3",
            "NOT IMPLEMENTED!"});
			this.interactions_list.Location = new System.Drawing.Point(466, 38);
			this.interactions_list.Name = "interactions_list";
			this.interactions_list.Size = new System.Drawing.Size(170, 147);
			this.interactions_list.TabIndex = 2;
			// 
			// interactions_list_label
			// 
			this.interactions_list_label.AutoSize = true;
			this.interactions_list_label.Font = new System.Drawing.Font("Colonna MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.interactions_list_label.Location = new System.Drawing.Point(463, 22);
			this.interactions_list_label.Name = "interactions_list_label";
			this.interactions_list_label.Size = new System.Drawing.Size(86, 13);
			this.interactions_list_label.TabIndex = 3;
			this.interactions_list_label.Text = "Interactions";
			// 
			// group_list
			// 
			this.group_list.FormattingEnabled = true;
			this.group_list.Items.AddRange(new object[] {
            "Group 1",
            "Group 2",
            "Group 3",
            "NOT IMPLEMENTED"});
			this.group_list.Location = new System.Drawing.Point(348, 38);
			this.group_list.Name = "group_list";
			this.group_list.Size = new System.Drawing.Size(112, 21);
			this.group_list.TabIndex = 4;
			// 
			// group_list_label
			// 
			this.group_list_label.AutoSize = true;
			this.group_list_label.Font = new System.Drawing.Font("Colonna MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.group_list_label.Location = new System.Drawing.Point(296, 43);
			this.group_list_label.Name = "group_list_label";
			this.group_list_label.Size = new System.Drawing.Size(46, 13);
			this.group_list_label.TabIndex = 5;
			this.group_list_label.Text = "Group";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point(106, 216);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 13);
			this.label2.TabIndex = 20;
			this.label2.Text = "Acceleration Angle";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(6, 216);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 13);
			this.label1.TabIndex = 19;
			this.label1.Text = "Velocity Angle";
			// 
			// acceleration_angle_up_down
			// 
			this.acceleration_angle_up_down.Location = new System.Drawing.Point(122, 180);
			this.acceleration_angle_up_down.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
			this.acceleration_angle_up_down.Name = "acceleration_angle_up_down";
			this.acceleration_angle_up_down.Size = new System.Drawing.Size(70, 20);
			this.acceleration_angle_up_down.TabIndex = 18;
			// 
			// velocity_angle_up_down
			// 
			this.velocity_angle_up_down.Location = new System.Drawing.Point(12, 180);
			this.velocity_angle_up_down.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
			this.velocity_angle_up_down.Name = "velocity_angle_up_down";
			this.velocity_angle_up_down.Size = new System.Drawing.Size(70, 20);
			this.velocity_angle_up_down.TabIndex = 17;
			// 
			// new_circle_acceleration_label
			// 
			this.new_circle_acceleration_label.AutoSize = true;
			this.new_circle_acceleration_label.BackColor = System.Drawing.Color.Transparent;
			this.new_circle_acceleration_label.Location = new System.Drawing.Point(18, 58);
			this.new_circle_acceleration_label.Name = "new_circle_acceleration_label";
			this.new_circle_acceleration_label.Size = new System.Drawing.Size(69, 13);
			this.new_circle_acceleration_label.TabIndex = 16;
			this.new_circle_acceleration_label.Text = "Acceleration:";
			// 
			// acceleration_up_down
			// 
			this.acceleration_up_down.DecimalPlaces = 3;
			this.acceleration_up_down.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.acceleration_up_down.Location = new System.Drawing.Point(95, 56);
			this.acceleration_up_down.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.acceleration_up_down.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
			this.acceleration_up_down.Name = "acceleration_up_down";
			this.acceleration_up_down.ReadOnly = true;
			this.acceleration_up_down.Size = new System.Drawing.Size(70, 20);
			this.acceleration_up_down.TabIndex = 15;
			// 
			// new_circle_velocity_label
			// 
			this.new_circle_velocity_label.AutoSize = true;
			this.new_circle_velocity_label.BackColor = System.Drawing.Color.Transparent;
			this.new_circle_velocity_label.Location = new System.Drawing.Point(18, 24);
			this.new_circle_velocity_label.Name = "new_circle_velocity_label";
			this.new_circle_velocity_label.Size = new System.Drawing.Size(47, 13);
			this.new_circle_velocity_label.TabIndex = 14;
			this.new_circle_velocity_label.Text = "Velocity:";
			// 
			// velocity_up_down
			// 
			this.velocity_up_down.DecimalPlaces = 3;
			this.velocity_up_down.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.velocity_up_down.Location = new System.Drawing.Point(95, 22);
			this.velocity_up_down.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.velocity_up_down.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
			this.velocity_up_down.Name = "velocity_up_down";
			this.velocity_up_down.ReadOnly = true;
			this.velocity_up_down.Size = new System.Drawing.Size(70, 20);
			this.velocity_up_down.TabIndex = 13;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(296, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 13);
			this.label3.TabIndex = 22;
			this.label3.Text = "R(but really D)";
			// 
			// circle_radius_up_down
			// 
			this.circle_radius_up_down.Location = new System.Drawing.Point(400, 65);
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
			this.circle_radius_up_down.Size = new System.Drawing.Size(60, 20);
			this.circle_radius_up_down.TabIndex = 21;
			this.circle_radius_up_down.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// circle_color_dialog
			// 
			this.circle_color_dialog.AnyColor = true;
			// 
			// color_button
			// 
			this.color_button.Location = new System.Drawing.Point(299, 91);
			this.color_button.Name = "color_button";
			this.color_button.Size = new System.Drawing.Size(161, 23);
			this.color_button.TabIndex = 23;
			this.color_button.Text = "Circle Color";
			this.color_button.UseVisualStyleBackColor = true;
			this.color_button.Click += new System.EventHandler(this.color_button_Click);
			// 
			// CircleDetailWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(659, 244);
			this.Controls.Add(this.color_button);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.circle_radius_up_down);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.acceleration_angle_up_down);
			this.Controls.Add(this.velocity_angle_up_down);
			this.Controls.Add(this.new_circle_acceleration_label);
			this.Controls.Add(this.acceleration_up_down);
			this.Controls.Add(this.new_circle_velocity_label);
			this.Controls.Add(this.velocity_up_down);
			this.Controls.Add(this.group_list_label);
			this.Controls.Add(this.group_list);
			this.Controls.Add(this.interactions_list_label);
			this.Controls.Add(this.interactions_list);
			this.Controls.Add(this.cancel_button);
			this.Controls.Add(this.ok_button);
			this.Name = "CircleDetailWindow";
			this.Text = "Circle - Detailed View";
			((System.ComponentModel.ISupportInitialize)(this.acceleration_angle_up_down)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.velocity_angle_up_down)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.acceleration_up_down)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.velocity_up_down)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.circle_radius_up_down)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button ok_button;
		private System.Windows.Forms.Button cancel_button;
		private System.Windows.Forms.ListBox interactions_list;
		private System.Windows.Forms.Label interactions_list_label;
		private System.Windows.Forms.ComboBox group_list;
		private System.Windows.Forms.Label group_list_label;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown acceleration_angle_up_down;
		private System.Windows.Forms.NumericUpDown velocity_angle_up_down;
		private System.Windows.Forms.Label new_circle_acceleration_label;
		private System.Windows.Forms.NumericUpDown acceleration_up_down;
		private System.Windows.Forms.Label new_circle_velocity_label;
		private System.Windows.Forms.NumericUpDown velocity_up_down;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown circle_radius_up_down;
		private System.Windows.Forms.ColorDialog circle_color_dialog;
		private System.Windows.Forms.Button color_button;
	}
}