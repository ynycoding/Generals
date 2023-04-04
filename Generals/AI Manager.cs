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

namespace Generals
{
	public partial class AI_Manager : Form
	{
		public AI_Manager()
		{
			InitializeComponent();
		}
		//Configuration config;
		private void AI_Manager_Load(object sender, EventArgs e)
		{
			if (Settings.Default.AI == null) Settings.Default.AI = new System.Collections.Specialized.StringCollection();
			//config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			//AppSettingsSection ais = (AppSettingsSection)config.GetSection("AI");
			foreach (var t in Settings.Default.AI)
			{
				listBox1.Items.Add(t);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			for(int i=listBox1.SelectedItems.Count-1; i>=0; --i)
			{
				Settings.Default.AI.Remove(listBox1.Items[listBox1.SelectedIndices[i]].ToString());
				listBox1.Items.Remove(listBox1.Items[listBox1.SelectedIndices[i]]);
			}
			Settings.Default.Save();
			//config.Save();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			OpenFileDialog file = new OpenFileDialog();
			file.Multiselect = false;
			file.Title = "Load Map";
			file.Filter = "executable|*.exe";
			if (file.ShowDialog() == DialogResult.OK)
			{
				string path = file.FileName;
				Settings.Default.AI.Add(path);
				listBox1.Items.Add(path);
				//config.Save();
			}
			Settings.Default.Save();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();
			Settings.Default.AI.Clear();
			Settings.Default.Save();
		}
	}
}
