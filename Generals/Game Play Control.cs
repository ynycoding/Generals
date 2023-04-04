using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generals
{
	public partial class Game_Play_Control : Form
	{
		public Game_Play_Control()
		{
			InitializeComponent();
		}

		private void Commit_Wait_Time()
		{
			Settings.Default.Gap = Convert.ToInt32(textBox1.Text);
			Settings.Default.Save();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Commit_Wait_Time();
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\n') Commit_Wait_Time();
			if (e.KeyChar != '\b' && (e.KeyChar < '0' || e.KeyChar > '9'))
			{
				e.Handled = true;
			}
			if (textBox1.Text.Length == 0) textBox1.Text = "0";
			int x = Convert.ToInt32(textBox1.Text);
			x = Math.Min(x, 2000);
			textBox1.Text = x.ToString();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Settings.Default.Save();
			this.Close();
		}

		private void Game_Play_Control_Load(object sender, EventArgs e)
		{
			textBox1.Text = Settings.Default.Gap.ToString();
			checkBox1.Checked = (Settings.Default.Rule & 1) > 0;
			checkBox2.Checked = (Settings.Default.Rule & 2) > 0;
			checkBox3.Checked = Settings.Default.ViewAsKing;
		}

		private void checkBox3_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.ViewAsKing = checkBox3.Checked;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.Rule &= 2;
			Settings.Default.Rule |= Convert.ToInt32(checkBox1.Checked);
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.Rule &= 1;
			Settings.Default.Rule |= Convert.ToInt32(checkBox2.Checked);
		}
	}
}
