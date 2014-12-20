namespace remonduk.GUI
{
    partial class Form1
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
            this.position_label = new System.Windows.Forms.Label();
            this.velocity_label = new System.Windows.Forms.Label();
            this.tether_list_label = new System.Windows.Forms.Label();
            this.tether_list = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.interactions_list.Location = new System.Drawing.Point(558, 38);
            this.interactions_list.Name = "interactions_list";
            this.interactions_list.Size = new System.Drawing.Size(78, 147);
            this.interactions_list.TabIndex = 2;
            // 
            // interactions_list_label
            // 
            this.interactions_list_label.AutoSize = true;
            this.interactions_list_label.Font = new System.Drawing.Font("Colonna MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.interactions_list_label.Location = new System.Drawing.Point(550, 22);
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
            this.group_list.Location = new System.Drawing.Point(340, 38);
            this.group_list.Name = "group_list";
            this.group_list.Size = new System.Drawing.Size(112, 21);
            this.group_list.TabIndex = 4;
            // 
            // group_list_label
            // 
            this.group_list_label.AutoSize = true;
            this.group_list_label.Font = new System.Drawing.Font("Colonna MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group_list_label.Location = new System.Drawing.Point(337, 22);
            this.group_list_label.Name = "group_list_label";
            this.group_list_label.Size = new System.Drawing.Size(46, 13);
            this.group_list_label.TabIndex = 5;
            this.group_list_label.Text = "Group";
            // 
            // position_label
            // 
            this.position_label.AutoSize = true;
            this.position_label.Font = new System.Drawing.Font("Constantia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.position_label.Location = new System.Drawing.Point(337, 62);
            this.position_label.Name = "position_label";
            this.position_label.Size = new System.Drawing.Size(111, 23);
            this.position_label.TabIndex = 6;
            this.position_label.Text = "Pos: (120,54)";
            // 
            // velocity_label
            // 
            this.velocity_label.AutoSize = true;
            this.velocity_label.Font = new System.Drawing.Font("Constantia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.velocity_label.Location = new System.Drawing.Point(336, 85);
            this.velocity_label.Name = "velocity_label";
            this.velocity_label.Size = new System.Drawing.Size(73, 23);
            this.velocity_label.TabIndex = 7;
            this.velocity_label.Text = "velocity";
            // 
            // tether_list_label
            // 
            this.tether_list_label.AutoSize = true;
            this.tether_list_label.Font = new System.Drawing.Font("Colonna MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tether_list_label.Location = new System.Drawing.Point(458, 22);
            this.tether_list_label.Name = "tether_list_label";
            this.tether_list_label.Size = new System.Drawing.Size(56, 13);
            this.tether_list_label.TabIndex = 9;
            this.tether_list_label.Text = "Tethers";
            // 
            // tether_list
            // 
            this.tether_list.Font = new System.Drawing.Font("Colonna MT", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tether_list.FormattingEnabled = true;
            this.tether_list.Items.AddRange(new object[] {
            "Tether 1",
            "Tether 2",
            "Tether 3",
            "NOT IMPLEMENTED"});
            this.tether_list.Location = new System.Drawing.Point(466, 38);
            this.tether_list.Name = "tether_list";
            this.tether_list.Size = new System.Drawing.Size(78, 147);
            this.tether_list.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Constantia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(336, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 23);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Constantia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(336, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Constantia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(336, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 23);
            this.label3.TabIndex = 12;
            this.label3.Text = "label3";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 244);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tether_list_label);
            this.Controls.Add(this.tether_list);
            this.Controls.Add(this.velocity_label);
            this.Controls.Add(this.position_label);
            this.Controls.Add(this.group_list_label);
            this.Controls.Add(this.group_list);
            this.Controls.Add(this.interactions_list_label);
            this.Controls.Add(this.interactions_list);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.Name = "Form1";
            this.Text = "Circle - Detailed View";
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
        private System.Windows.Forms.Label position_label;
        private System.Windows.Forms.Label velocity_label;
        private System.Windows.Forms.Label tether_list_label;
        private System.Windows.Forms.ListBox tether_list;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}