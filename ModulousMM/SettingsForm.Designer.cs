namespace ModulousMM
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.sd_folder_browse_textbox = new System.Windows.Forms.TextBox();
            this.sd_folder_browse_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dolphin_location_text_box = new System.Windows.Forms.TextBox();
            this.browse_dolphin_path_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.open_mod_creator = new System.Windows.Forms.Button();
            this.enable_debug_features = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // sd_folder_browse_textbox
            // 
            this.sd_folder_browse_textbox.Enabled = false;
            this.sd_folder_browse_textbox.Location = new System.Drawing.Point(6, 30);
            this.sd_folder_browse_textbox.Name = "sd_folder_browse_textbox";
            this.sd_folder_browse_textbox.Size = new System.Drawing.Size(477, 20);
            this.sd_folder_browse_textbox.TabIndex = 0;
            // 
            // sd_folder_browse_button
            // 
            this.sd_folder_browse_button.Location = new System.Drawing.Point(489, 30);
            this.sd_folder_browse_button.Name = "sd_folder_browse_button";
            this.sd_folder_browse_button.Size = new System.Drawing.Size(75, 20);
            this.sd_folder_browse_button.TabIndex = 1;
            this.sd_folder_browse_button.Text = "Browse...";
            this.sd_folder_browse_button.UseVisualStyleBackColor = true;
            this.sd_folder_browse_button.Click += new System.EventHandler(this.sd_folder_browse_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dolphin_location_text_box);
            this.groupBox1.Controls.Add(this.browse_dolphin_path_button);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.sd_folder_browse_textbox);
            this.groupBox1.Controls.Add(this.sd_folder_browse_button);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 98);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Paths";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Dolphin";
            // 
            // dolphin_location_text_box
            // 
            this.dolphin_location_text_box.Enabled = false;
            this.dolphin_location_text_box.Location = new System.Drawing.Point(6, 68);
            this.dolphin_location_text_box.Name = "dolphin_location_text_box";
            this.dolphin_location_text_box.Size = new System.Drawing.Size(477, 20);
            this.dolphin_location_text_box.TabIndex = 3;
            // 
            // browse_dolphin_path_button
            // 
            this.browse_dolphin_path_button.Location = new System.Drawing.Point(489, 68);
            this.browse_dolphin_path_button.Name = "browse_dolphin_path_button";
            this.browse_dolphin_path_button.Size = new System.Drawing.Size(75, 20);
            this.browse_dolphin_path_button.TabIndex = 4;
            this.browse_dolphin_path_button.Text = "Browse...";
            this.browse_dolphin_path_button.UseVisualStyleBackColor = true;
            this.browse_dolphin_path_button.Click += new System.EventHandler(this.browse_dolphin_path_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "SD card";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.open_mod_creator);
            this.groupBox2.Controls.Add(this.enable_debug_features);
            this.groupBox2.Location = new System.Drawing.Point(12, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(570, 55);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Advanced";
            // 
            // open_mod_creator
            // 
            this.open_mod_creator.Location = new System.Drawing.Point(181, 19);
            this.open_mod_creator.Name = "open_mod_creator";
            this.open_mod_creator.Size = new System.Drawing.Size(149, 23);
            this.open_mod_creator.TabIndex = 1;
            this.open_mod_creator.Text = "Open Mod Creation Tools";
            this.open_mod_creator.UseVisualStyleBackColor = true;
            this.open_mod_creator.Click += new System.EventHandler(this.open_mod_creator_Click);
            // 
            // enable_debug_features
            // 
            this.enable_debug_features.Location = new System.Drawing.Point(6, 19);
            this.enable_debug_features.Name = "enable_debug_features";
            this.enable_debug_features.Size = new System.Drawing.Size(169, 23);
            this.enable_debug_features.TabIndex = 0;
            this.enable_debug_features.Text = "Load Debugging Console";
            this.enable_debug_features.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 183);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SettingsForm_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox sd_folder_browse_textbox;
        private System.Windows.Forms.Button sd_folder_browse_button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button enable_debug_features;
        private System.Windows.Forms.Button open_mod_creator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dolphin_location_text_box;
        private System.Windows.Forms.Button browse_dolphin_path_button;
    }
}