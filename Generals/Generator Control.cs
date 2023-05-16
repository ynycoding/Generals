using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;

namespace Generals
{
	public partial class Generator_Control : Form
	{
		public Generator_Control()
		{
			InitializeComponent();
		}

		public string selectfile()
		{
			OpenFileDialog file = new OpenFileDialog();
			file.Multiselect = false;
			file.Title = "Load Generator";
			file.Filter = "executable|*.exe";
			if (file.ShowDialog() == DialogResult.OK)
			{
				return file.FileName;
			}
			return "";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string s=selectfile();
			if (s.Length > 0)
			{
				textBox1.Text = s;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Settings.Default.Generator = textBox1.Text;
			Settings.Default.GenArg = textBox2.Text;
			Settings.Default.Save();
			Close();
		}

		private void Generator_Control_Load(object sender, EventArgs e)
		{
			textBox1.Text = Settings.Default.Generator;
			textBox2.Text = Settings.Default.GenArg;
		}
	}
}
