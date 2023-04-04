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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
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
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Gray;
			this.pictureBox1.Location = new System.Drawing.Point(12, 28);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(700, 700);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
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
			this.button1.Font = new System.Drawing.Font("宋体", 12F);
			this.button1.Location = new System.Drawing.Point(990, 669);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(187, 59);
			this.button1.TabIndex = 3;
			this.button1.Text = "Start Game";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(744, 28);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(433, 234);
			this.listView1.TabIndex = 4;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// Column1
			// 
			this.Column1.Text = "Name";
			this.Column1.Width = 225;
			// 
			// Column2
			// 
			this.Column2.Text = "Sodiers";
			// 
			// Column3
			// 
			this.Column3.Text = "Lands";
			// 
			// Column4
			// 
			this.Column4.Text = "Towers";
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1227, 800);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form2";
			this.Text = "New Game";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
			this.Load += new System.EventHandler(this.Form2_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
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
	}
}