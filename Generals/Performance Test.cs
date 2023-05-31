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
	public partial class Performance_Test : Form
	{
		public Performance_Test()
		{
			InitializeComponent();
			game.label1 = label2;
		}
		int cnt;
		public Game game = new Game();
		private void Performance_Test_Load(object sender, EventArgs e)
		{
			foreach (var p in Settings.Default.AI)
			{
				string[] pv = p.Split('\\');
				string name = pv[pv.Length - 1];
				name = name.Substring(0, name.Length - 4);
				var item = new ListViewItem();
				item.Text = name;
				item.SubItems.Add("0");
				listView1.Items.Add(item);
			}
		}
		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\n') cnt= Convert.ToInt32(textBox1.Text);
			if (e.KeyChar != '\b' && (e.KeyChar < '0' || e.KeyChar > '9'))
			{
				e.Handled = true;
			}
			if (textBox1.Text.Length == 0) textBox1.Text = "0";
			int x = Convert.ToInt32(textBox1.Text);
			x = Math.Min(x, 1000);
			textBox1.Text = x.ToString();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			cnt = Convert.ToInt32(textBox1.Text);
		}
		int[] win = new int[100];
		int n;
		public void End_Game(int id)
		{
			++win[id];
			//if (listView1 == null) return;
			if (id > 0) listView1.Items[id - 1].SubItems[1].Text = win[id].ToString();
			run();
		}
		void run()
		{
			if (cnt > 0)
			{
				game.LoadFromGenerator();
				game.Start(End_Game, null);
				game.gameplay.Enabled = true;
				--cnt;
				textBox1.Text = cnt.ToString();
			}
		}
		private void button2_Click(object sender, EventArgs e)
		{
			button2.Hide();
			n = Settings.Default.AI.Count;
			game.hidden = true;
			if (game.gameplay == null)
			{
				game.gameplay = new System.Windows.Forms.Timer();
				game.gameplay.Interval = 1;
				game.gameplay.Tick += new EventHandler(game.Round);
			}
			run();
		}

		private void Performance_Test_FormClosed(object sender, FormClosedEventArgs e)
		{
			cnt = -1;
			game.gameplay.Enabled = false;
			game.finished = true;
			game.Dispose();
		}
	}
}
