namespace ModulousMM
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mods_list_view = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.author = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.game = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.dolphin_tool_strip_button = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.settings_strip_button = new System.Windows.Forms.ToolStripButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.download_button = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mods_list_view
            // 
            this.mods_list_view.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mods_list_view.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.author,
            this.game});
            this.mods_list_view.FullRowSelect = true;
            this.mods_list_view.GridLines = true;
            this.mods_list_view.Location = new System.Drawing.Point(50, 43);
            this.mods_list_view.Name = "mods_list_view";
            this.mods_list_view.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mods_list_view.Size = new System.Drawing.Size(1214, 472);
            this.mods_list_view.TabIndex = 1;
            this.mods_list_view.UseCompatibleStateImageBehavior = false;
            this.mods_list_view.View = System.Windows.Forms.View.Details;
            this.mods_list_view.SizeChanged += new System.EventHandler(this.mods_list_view_SizeChanged);
            this.mods_list_view.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mods_list_view_MouseDoubleClick);
            // 
            // name
            // 
            this.name.Tag = "1";
            this.name.Text = "Name";
            this.name.Width = 168;
            // 
            // author
            // 
            this.author.Tag = "1";
            this.author.Text = "Author";
            this.author.Width = 215;
            // 
            // game
            // 
            this.game.Tag = "1";
            this.game.Text = "Game";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(489, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dolphin_tool_strip_button
            // 
            this.dolphin_tool_strip_button.Image = ((System.Drawing.Image)(resources.GetObject("dolphin_tool_strip_button.Image")));
            this.dolphin_tool_strip_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dolphin_tool_strip_button.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.dolphin_tool_strip_button.Name = "dolphin_tool_strip_button";
            this.dolphin_tool_strip_button.Size = new System.Drawing.Size(135, 40);
            this.dolphin_tool_strip_button.Text = "Launch Dolphin";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.dolphin_tool_strip_button,
            this.settings_strip_button});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1264, 43);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // settings_strip_button
            // 
            this.settings_strip_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settings_strip_button.Image = ((System.Drawing.Image)(resources.GetObject("settings_strip_button.Image")));
            this.settings_strip_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settings_strip_button.Name = "settings_strip_button";
            this.settings_strip_button.Size = new System.Drawing.Size(44, 40);
            this.settings_strip_button.Text = "toolStripButton1";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(50, 515);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1214, 165);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.download_button);
            this.panel1.Location = new System.Drawing.Point(0, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(50, 638);
            this.panel1.TabIndex = 5;
            // 
            // download_button
            // 
            this.download_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.download_button.FlatAppearance.BorderSize = 0;
            this.download_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.download_button.Location = new System.Drawing.Point(3, 3);
            this.download_button.Name = "download_button";
            this.download_button.Size = new System.Drawing.Size(44, 44);
            this.download_button.TabIndex = 6;
            this.download_button.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mods_list_view);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Modulous Mod Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView mods_list_view;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader author;
        private System.Windows.Forms.ColumnHeader game;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripButton dolphin_tool_strip_button;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton settings_strip_button;
        private System.Windows.Forms.Button download_button;


    }
}

