namespace ModulousMM
{
    partial class DebugForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.update_mod_from_file_button = new System.Windows.Forms.Button();
            this.Alloc_console_button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modulous Mod Manager DEBUG menu";
            // 
            // update_mod_from_file_button
            // 
            this.update_mod_from_file_button.Location = new System.Drawing.Point(15, 25);
            this.update_mod_from_file_button.Name = "update_mod_from_file_button";
            this.update_mod_from_file_button.Size = new System.Drawing.Size(144, 29);
            this.update_mod_from_file_button.TabIndex = 1;
            this.update_mod_from_file_button.Text = "Run script in update mode";
            this.update_mod_from_file_button.UseVisualStyleBackColor = true;
            this.update_mod_from_file_button.Click += new System.EventHandler(this.update_mod_from_file_button_Click);
            // 
            // Alloc_console_button
            // 
            this.Alloc_console_button.Location = new System.Drawing.Point(165, 25);
            this.Alloc_console_button.Name = "Alloc_console_button";
            this.Alloc_console_button.Size = new System.Drawing.Size(104, 29);
            this.Alloc_console_button.TabIndex = 2;
            this.Alloc_console_button.Text = "AllocConsole";
            this.Alloc_console_button.UseVisualStyleBackColor = true;
            this.Alloc_console_button.Click += new System.EventHandler(this.Alloc_console_button_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 29);
            this.button1.TabIndex = 3;
            this.button1.Text = "createform";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 321);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Alloc_console_button);
            this.Controls.Add(this.update_mod_from_file_button);
            this.Controls.Add(this.label1);
            this.Name = "DebugForm";
            this.Text = "DebugForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button update_mod_from_file_button;
        private System.Windows.Forms.Button Alloc_console_button;
        private System.Windows.Forms.Button button1;
    }
}