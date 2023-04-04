namespace Generals
{
	partial class Form1
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gamePlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.readmeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gameRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.howToInteractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.button4 = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(273, 142);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(208, 51);
			this.button1.TabIndex = 0;
			this.button1.Text = "AI FFA";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(273, 215);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(208, 51);
			this.button2.TabIndex = 1;
			this.button2.Text = "Player vs AIs";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(273, 371);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(208, 51);
			this.button3.TabIndex = 2;
			this.button3.Text = "Load Game";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(800, 25);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aIToolStripMenuItem,
            this.gamePlayToolStripMenuItem});
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(66, 21);
			this.settingsToolStripMenuItem.Text = "Settings";
			// 
			// aIToolStripMenuItem
			// 
			this.aIToolStripMenuItem.Name = "aIToolStripMenuItem";
			this.aIToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.aIToolStripMenuItem.Text = "AI";
			this.aIToolStripMenuItem.Click += new System.EventHandler(this.aIToolStripMenuItem_Click);
			// 
			// gamePlayToolStripMenuItem
			// 
			this.gamePlayToolStripMenuItem.Name = "gamePlayToolStripMenuItem";
			this.gamePlayToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.gamePlayToolStripMenuItem.Text = "Game play";
			this.gamePlayToolStripMenuItem.Click += new System.EventHandler(this.gamePlayToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readmeToolStripMenuItem,
            this.gameRulesToolStripMenuItem,
            this.howToInteractToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// readmeToolStripMenuItem
			// 
			this.readmeToolStripMenuItem.Name = "readmeToolStripMenuItem";
			this.readmeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.readmeToolStripMenuItem.Text = "Readme";
			this.readmeToolStripMenuItem.Click += new System.EventHandler(this.readmeToolStripMenuItem_Click);
			// 
			// gameRulesToolStripMenuItem
			// 
			this.gameRulesToolStripMenuItem.Name = "gameRulesToolStripMenuItem";
			this.gameRulesToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.gameRulesToolStripMenuItem.Text = "Game Rules";
			this.gameRulesToolStripMenuItem.Click += new System.EventHandler(this.gameRulesToolStripMenuItem_Click);
			// 
			// howToInteractToolStripMenuItem
			// 
			this.howToInteractToolStripMenuItem.Name = "howToInteractToolStripMenuItem";
			this.howToInteractToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.howToInteractToolStripMenuItem.Text = "How to Interact";
			this.howToInteractToolStripMenuItem.Click += new System.EventHandler(this.howToInteractToolStripMenuItem_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(273, 292);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(208, 51);
			this.button4.TabIndex = 4;
			this.button4.Text = "Sandbox";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Launcher";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aIToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem gamePlayToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem readmeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem gameRulesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem howToInteractToolStripMenuItem;
		private System.Windows.Forms.Button button4;
	}
}

