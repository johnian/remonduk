namespace Remonduk.GUI
{
    partial class Physical_System_Detail_Window
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
            this.circle_list = new System.Windows.Forms.ListBox();
            this.circle_list_label = new System.Windows.Forms.Label();
            this.interactions_label = new System.Windows.Forms.Label();
            this.interactions_list = new System.Windows.Forms.ListBox();
            this.gravity_checkbox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.interactions_blank_button = new System.Windows.Forms.Button();
            this.interactions_remove_button = new System.Windows.Forms.Button();
            this.interactions_edit_button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // circle_list
            // 
            this.circle_list.FormattingEnabled = true;
            this.circle_list.Location = new System.Drawing.Point(469, 49);
            this.circle_list.Name = "circle_list";
            this.circle_list.Size = new System.Drawing.Size(174, 212);
            this.circle_list.TabIndex = 0;
            this.circle_list.MouseClick += new System.Windows.Forms.MouseEventHandler(this.circle_list_MouseClick);
            // 
            // circle_list_label
            // 
            this.circle_list_label.AutoSize = true;
            this.circle_list_label.Font = new System.Drawing.Font("Constantia", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.circle_list_label.Location = new System.Drawing.Point(464, 17);
            this.circle_list_label.Name = "circle_list_label";
            this.circle_list_label.Size = new System.Drawing.Size(91, 29);
            this.circle_list_label.TabIndex = 1;
            this.circle_list_label.Text = "Circles";
            // 
            // interactions_label
            // 
            this.interactions_label.AutoSize = true;
            this.interactions_label.Font = new System.Drawing.Font("Constantia", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.interactions_label.Location = new System.Drawing.Point(279, 17);
            this.interactions_label.Name = "interactions_label";
            this.interactions_label.Size = new System.Drawing.Size(151, 29);
            this.interactions_label.TabIndex = 3;
            this.interactions_label.Text = "Interactions";
            // 
            // interactions_list
            // 
            this.interactions_list.FormattingEnabled = true;
            this.interactions_list.Location = new System.Drawing.Point(284, 49);
            this.interactions_list.Name = "interactions_list";
            this.interactions_list.Size = new System.Drawing.Size(174, 212);
            this.interactions_list.TabIndex = 2;
            this.interactions_list.SelectedIndexChanged += new System.EventHandler(this.interactions_list_SelectedIndexChanged);
            // 
            // gravity_checkbox
            // 
            this.gravity_checkbox.AutoSize = true;
            this.gravity_checkbox.Font = new System.Drawing.Font("Constantia", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gravity_checkbox.Location = new System.Drawing.Point(12, 19);
            this.gravity_checkbox.Name = "gravity_checkbox";
            this.gravity_checkbox.Size = new System.Drawing.Size(113, 33);
            this.gravity_checkbox.TabIndex = 4;
            this.gravity_checkbox.Text = "Gravity";
            this.gravity_checkbox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Constantia", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "Value:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 3;
            this.numericUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDown1.Location = new System.Drawing.Point(97, 59);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(56, 20);
            this.numericUpDown1.TabIndex = 6;
            // 
            // interactions_blank_button
            // 
            this.interactions_blank_button.Location = new System.Drawing.Point(284, 267);
            this.interactions_blank_button.Name = "interactions_blank_button";
            this.interactions_blank_button.Size = new System.Drawing.Size(58, 23);
            this.interactions_blank_button.TabIndex = 7;
            this.interactions_blank_button.UseVisualStyleBackColor = true;
            // 
            // interactions_remove_button
            // 
            this.interactions_remove_button.Location = new System.Drawing.Point(400, 267);
            this.interactions_remove_button.Name = "interactions_remove_button";
            this.interactions_remove_button.Size = new System.Drawing.Size(58, 23);
            this.interactions_remove_button.TabIndex = 8;
            this.interactions_remove_button.Text = "remove";
            this.interactions_remove_button.UseVisualStyleBackColor = true;
            this.interactions_remove_button.Click += new System.EventHandler(this.interactions_remove_button_Click);
            // 
            // interactions_edit_button
            // 
            this.interactions_edit_button.Location = new System.Drawing.Point(348, 267);
            this.interactions_edit_button.Name = "interactions_edit_button";
            this.interactions_edit_button.Size = new System.Drawing.Size(46, 23);
            this.interactions_edit_button.TabIndex = 9;
            this.interactions_edit_button.Text = "edit";
            this.interactions_edit_button.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(533, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "edit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(585, 267);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(58, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "remove";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(469, 267);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(58, 23);
            this.button3.TabIndex = 10;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Physical_System_Detail_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 305);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.interactions_edit_button);
            this.Controls.Add(this.interactions_remove_button);
            this.Controls.Add(this.interactions_blank_button);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gravity_checkbox);
            this.Controls.Add(this.interactions_label);
            this.Controls.Add(this.interactions_list);
            this.Controls.Add(this.circle_list_label);
            this.Controls.Add(this.circle_list);
            this.Name = "Physical_System_Detail_Window";
            this.Text = "Physical System Detail Window";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label circle_list_label;
        private System.Windows.Forms.Label interactions_label;
        private System.Windows.Forms.ListBox interactions_list;
        private System.Windows.Forms.CheckBox gravity_checkbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button interactions_blank_button;
        private System.Windows.Forms.Button interactions_remove_button;
        private System.Windows.Forms.Button interactions_edit_button;
        public System.Windows.Forms.ListBox circle_list;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}