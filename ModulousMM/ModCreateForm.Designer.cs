namespace ModulousMM
{
    partial class ModCreateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModCreateForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.author_name_text_box = new wmgCMS.WaterMarkTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numeric_version_select = new System.Windows.Forms.NumericUpDown();
            this.game_combo_box = new System.Windows.Forms.ComboBox();
            this.name_text_box = new wmgCMS.WaterMarkTextBox();
            this.select_lua_button = new System.Windows.Forms.Button();
            this.select_mod_root_button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.save_button = new System.Windows.Forms.Button();
            this.brawlex_checkbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_version_select)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.author_name_text_box);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numeric_version_select);
            this.groupBox1.Controls.Add(this.game_combo_box);
            this.groupBox1.Controls.Add(this.name_text_box);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 74);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data";
            // 
            // author_name_text_box
            // 
            this.author_name_text_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.author_name_text_box.Location = new System.Drawing.Point(107, 20);
            this.author_name_text_box.Name = "author_name_text_box";
            this.author_name_text_box.Size = new System.Drawing.Size(100, 20);
            this.author_name_text_box.TabIndex = 5;
            this.author_name_text_box.WaterMarkColor = System.Drawing.Color.Gray;
            this.author_name_text_box.WaterMarkText = "Author";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Version";
            // 
            // numeric_version_select
            // 
            this.numeric_version_select.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numeric_version_select.Location = new System.Drawing.Point(45, 44);
            this.numeric_version_select.Name = "numeric_version_select";
            this.numeric_version_select.Size = new System.Drawing.Size(100, 20);
            this.numeric_version_select.TabIndex = 3;
            this.numeric_version_select.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // game_combo_box
            // 
            this.game_combo_box.AutoCompleteCustomSource.AddRange(new string[] {
            "Brawl",
            "ProjectM"});
            this.game_combo_box.FormattingEnabled = true;
            this.game_combo_box.Items.AddRange(new object[] {
            "Brawl",
            "ProjectM"});
            this.game_combo_box.Location = new System.Drawing.Point(151, 44);
            this.game_combo_box.Name = "game_combo_box";
            this.game_combo_box.Size = new System.Drawing.Size(100, 21);
            this.game_combo_box.TabIndex = 2;
            // 
            // name_text_box
            // 
            this.name_text_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.name_text_box.Location = new System.Drawing.Point(1, 20);
            this.name_text_box.Name = "name_text_box";
            this.name_text_box.Size = new System.Drawing.Size(100, 20);
            this.name_text_box.TabIndex = 1;
            this.name_text_box.WaterMarkColor = System.Drawing.Color.Gray;
            this.name_text_box.WaterMarkText = "Name";
            // 
            // select_lua_button
            // 
            this.select_lua_button.Location = new System.Drawing.Point(141, 19);
            this.select_lua_button.Name = "select_lua_button";
            this.select_lua_button.Size = new System.Drawing.Size(100, 21);
            this.select_lua_button.TabIndex = 3;
            this.select_lua_button.Text = "Select Lua script";
            this.select_lua_button.UseVisualStyleBackColor = true;
            this.select_lua_button.Click += new System.EventHandler(this.select_lua_button_Click);
            // 
            // select_mod_root_button
            // 
            this.select_mod_root_button.Location = new System.Drawing.Point(6, 19);
            this.select_mod_root_button.Name = "select_mod_root_button";
            this.select_mod_root_button.Size = new System.Drawing.Size(129, 21);
            this.select_mod_root_button.TabIndex = 4;
            this.select_mod_root_button.Text = "Select Mod Root Folder";
            this.select_mod_root_button.UseVisualStyleBackColor = true;
            this.select_mod_root_button.Click += new System.EventHandler(this.select_mod_root_button_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.select_lua_button);
            this.groupBox2.Controls.Add(this.select_mod_root_button);
            this.groupBox2.Location = new System.Drawing.Point(12, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 49);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Files";
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(12, 171);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(75, 23);
            this.save_button.TabIndex = 6;
            this.save_button.Text = "Save";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // brawlex_checkbox
            // 
            this.brawlex_checkbox.AutoSize = true;
            this.brawlex_checkbox.Location = new System.Drawing.Point(13, 148);
            this.brawlex_checkbox.Name = "brawlex_checkbox";
            this.brawlex_checkbox.Size = new System.Drawing.Size(175, 17);
            this.brawlex_checkbox.TabIndex = 7;
            this.brawlex_checkbox.Text = "Requires BrawlEX/ProjectM EX";
            this.brawlex_checkbox.UseVisualStyleBackColor = true;
            // 
            // ModCreateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 203);
            this.Controls.Add(this.brawlex_checkbox);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModCreateForm";
            this.Text = "ModCreateForm";
            this.Load += new System.EventHandler(this.ModCreateForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_version_select)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private wmgCMS.WaterMarkTextBox name_text_box;
        private System.Windows.Forms.ComboBox game_combo_box;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numeric_version_select;
        private System.Windows.Forms.Button select_lua_button;
        private System.Windows.Forms.Button select_mod_root_button;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.CheckBox brawlex_checkbox;
        private wmgCMS.WaterMarkTextBox author_name_text_box;
    }
}