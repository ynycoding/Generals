using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Diagnostics;

namespace Generals
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		

		private void button1_Click(object sender, EventArgs e)
		{
			Form2 play=new Form2();
			play.Show();
			//this.Hide();
		}

		private void aIToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AI_Manager aI_Manager = new AI_Manager();
			aI_Manager.ShowDialog();
		}

		private void gamePlayToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var control = new Game_Play_Control();
			control.ShowDialog();
		}

		private void readmeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Copyright 2023@ynycoding.\n" +
				"This is an early access version, some functions are not available.\n" +
				"Before starting a game, please make sure you've properly set the framerate and AI executable's paths. To start a game, " +
				"you must load a map beforehand.\n", "Readme");
		}

		private void howToInteractToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("interaction rule.md");
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Form2 play = new Form2();
			play.Show();
			play.game.isplayer = true;
		}

		private void gameRulesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Press WASD to move all but one sodier. You can also move only half the army by holding CTRL when giving orders.", "Readme");
		}

		private void button4_Click(object sender, EventArgs e)
		{
			Form2 play = new Form2();
			play.Show();
			play.game.issandbox = true;
		}
	}
}
