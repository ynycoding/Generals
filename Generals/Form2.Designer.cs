namespace Generals
{
	partial class Form2
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
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadRecentMapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadMapFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadFromGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.button1 = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.Column1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Column2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Column3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Column4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1227, 25);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// filesToolStripMenuItem
			// 
			this.filesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRecentMapsToolStripMenuItem,
            this.loadMapFromFileToolStripMenuItem,
            this.loadFromGeneratorToolStripMenuItem});
			this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
			this.filesToolStripMenuItem.Size = new System.Drawing.Size(45, 21);
			this.filesToolStripMenuItem.Text = "Files";
			// 
			// loadRecentMapsToolStripMenuItem
			// 
			this.loadRecentMapsToolStripMenuItem.Name = "loadRecentMapsToolStripMenuItem";
			this.loadRecentMapsToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
			this.loadRecentMapsToolStripMenuItem.Text = "Load recent maps";
			// 
			// loadMapFromFileToolStripMenuItem
			// 
			this.loadMapFromFileToolStripMenuItem.Name = "loadMapFromFileToolStripMenuItem";
			this.loadMapFromFileToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
			this.loadMapFromFileToolStripMenuItem.Text = "Load map from file";
			this.loadMapFromFileToolStripMenuItem.Click += new System.EventHandler(this.loadMapFromFileToolStripMenuItem_Click);
			// 
			// loadFromGeneratorToolStripMenuItem
			// 
			this.loadFromGeneratorToolStripMenuItem.Name = "loadFromGeneratorToolStripMenuItem";
			this.loadFromGeneratorToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
			this.loadFromGeneratorToolStripMenuItem.Text = "Load from generator";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Font = new System.Drawing.Font("宋体", 12F);
			this.button1.Location = new System.Drawing.Point(797, 630);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(187, 59);
			this.button1.TabIndex = 3;
			this.button1.Text = "Start Game";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(817, 28);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(398, 234);
			this.listView1.TabIndex = 4;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// Column1
			// 
			this.Column1.Text = "Name";
			this.Column1.Width = 205;
			// 
			// Column2
			// 
			this.Column2.Text = "Army";
			// 
			// Column3
			// 
			this.Column3.Text = "Lands";
			// 
			// Column4
			// 
			this.Column4.Text = "Towers";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Gray;
			this.pictureBox1.Location = new System.Drawing.Point(12, 28);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(750, 750);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Font = new System.Drawing.Font("宋体", 12F);
			this.button2.Location = new System.Drawing.Point(1005, 521);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(187, 59);
			this.button2.TabIndex = 5;
			this.button2.Text = "Surrender";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.Font = new System.Drawing.Font("宋体", 12F);
			this.button3.Location = new System.Drawing.Point(1005, 630);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(187, 59);
			this.button3.TabIndex = 6;
			this.button3.Text = "Teminate";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1227, 800);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.menuStrip1);
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form2";
			this.Text = "New Game";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
			this.Load += new System.EventHandler(this.Form2_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form2_KeyPress);
			this.Resize += new System.EventHandler(this.Form2_Resize);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadRecentMapsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadMapFromFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadFromGeneratorToolStripMenuItem;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader Column1;
		private System.Windows.Forms.ColumnHeader Column2;
		private System.Windows.Forms.ColumnHeader Column3;
		private System.Windows.Forms.ColumnHeader Column4;
		private System.ComponentModel.BackgroundWorker backgroundWorker2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
	}
}