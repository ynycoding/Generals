namespace Generals
{
	partial class AI_Manager
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
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(50, 101);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(583, 316);
			this.listBox1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("宋体", 12F);
			this.button1.Location = new System.Drawing.Point(50, 43);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(175, 52);
			this.button1.TabIndex = 3;
			this.button1.Text = "Add AI Executable";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("宋体", 12F);
			this.button2.Location = new System.Drawing.Point(277, 43);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(151, 52);
			this.button2.TabIndex = 4;
			this.button2.Text = "Remove Selected";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("宋体", 12F);
			this.button3.Location = new System.Drawing.Point(512, 43);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(121, 52);
			this.button3.TabIndex = 5;
			this.button3.Text = "Clear Items";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// AI_Manager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(748, 448);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.listBox1);
			this.Name = "AI_Manager";
			this.Text = "AI Manager";
			this.Load += new System.EventHandler(this.AI_Manager_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
	}
}