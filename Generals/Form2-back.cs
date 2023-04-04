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
using System.IO.Pipes;
using System.Diagnostics;




namespace Generals
{
	using System.Configuration;
	public partial class Form2 : Form
	{
		const int N = 205;
		public Form2()
		{
			InitializeComponent();
		}

		Game game=new Game();

		private void Form2_Load(object sender, EventArgs e)
		{
			//Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			//AppSettingsSection mapsconfig = (AppSettingsSection)config.GetSection("Maps");
			//foreach(var t in mapsconfig.Settings)
			//{
			//	loadRecentMapsToolStripMenuItem
			//}
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}

		private void loadMapFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string s = game.selectfile();
			if (s.Length > 0)
			{
				game.Dispose();
				if (game.Load(s))
				{
					game.Display(pictureBox1);
				}
				pause = false;
				button1.Text = "Start Game";
			}
		}
		bool pause = false;
		public void End_Game(int id)
		{
			button1.Text = "Play Again";
			pause = false;
		}
		private void button1_Click(object sender, EventArgs e)
		{
			if(!game.loaded)
			{
				MessageBox.Show("Please choose a map.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (game.gameplay==null||!game.gameplay.Enabled)
			{
				game.Display(pictureBox1);
				if (!pause)
				{
					game.Start(End_Game, listView1);
				}
				pause = false;
				if (game.gameplay==null)
				{
					game.gameplay = new Timer();
					game.gameplay.Interval = Settings.Default.Gap;
					game.gameplay.Tick += new EventHandler(game.Round);
				}
				game.gameplay.Enabled = true;
				//game.Round(sender, e);
				button1.Text="Pause Game";
			}
			else
			{
				game.gameplay.Enabled = false;
				pause = true;
				button1.Text = "Resume Game";
			}
		}

		private void Form2_FormClosed(object sender, FormClosedEventArgs e)
		{
			game.Dispose();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
	public class Map
	{
		public const int N = 205;
		public int n, cnt;
		public bool loaded, finished=false;
		public int[,] terrain = new int[N, N], force = new int[N, N];
		public Tuple<int, int>[] kingpos = new Tuple<int, int>[N];
		public bool checkpos(int x, int y)
		{
			return (x > 0 && y > 0 && x <= n && y <= n);
		}
		public bool Load(string path)
		{
			if (!File.Exists(path))
			{
				MessageBox.Show("Map File does not exist", "Error");
				return false;
			}
			StreamReader p = new StreamReader(path);
			try
			{
				string line = p.ReadLine();
				n = Convert.ToInt32(line);
				cnt = 0;
				for (int i = 1; i <= n; ++i)
				{
					line = p.ReadLine();
					string[] num = line.Split();
					for (int j = 1; j <= n; ++j)
					{
						terrain[i, j] = Convert.ToInt32(num[j - 1]);
						if (terrain[i, j] == 3) kingpos[++cnt] = new Tuple<int, int>(i, j);
					}
				}
				for (int i = 1; i <= n; ++i)
				{
					line = p.ReadLine();
					string[] num = line.Split();
					for (int j = 1; j <= n; ++j)
					{
						force[i, j] = Convert.ToInt32(num[j - 1]);
					}
				}
				loaded = true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Map invalid");
			}
			return true;
		}
		public string selectfile()
		{
			OpenFileDialog file = new OpenFileDialog();
			file.Multiselect = false;
			file.Title = "Load Map";
			file.Filter = "config|*.conf";
			if (file.ShowDialog() == DialogResult.OK)
			{
				return file.FileName;
			}
			return "";
		}
		public Map()
		{
		}
	}
	public class Game : Map
	{
		const int M = 17;
		public int[,] col = new int[N, N];
		public string[] name = new string[N];
		public List<string> errors;
		SolidBrush[] brushes = new SolidBrush[18];
		public Timer gameplay;
		int[] dx = new int[4] { 0, 0, -1, 1 }, dy = new int[4] { -1, 1, 0, 0 };
		Process[] AI = new Process[M];
		string[] AInames=new string[M];
		//AnonymousPipeServerStream[] Pipes = new AnonymousPipeServerStream[M];
		int AI_cnt, size, len;
		Random rnd = new Random();
		bool[] defeated=new bool[M];
		PictureBox pictureBox1;
		ListView list;
		Graphics g;
		~Game()
		{
			Dispose();
		}
		public void Dispose()
		{
			//if(gameplay!=null) gameplay.Dispose();
			if (list != null)
			{
				//list.BeginUpdate();
				list.Items.Clear();
				//list.EndUpdate();
			}
			finished = false;
			for (int i = 1; i <= AI_cnt; ++i)
			{
				defeated[i] = false;
				if (!AI[i].HasExited)
				{
					AI[i].Kill();
				}
			}
			AI_cnt = 0;
			for (int i = 1; i <= n; ++i) for (int j = 1; j <= n; ++j)
				{
					col[i, j] = 0;
					if (terrain[i, j] == 2) force[i, j] = 50;
					else force[i, j] = 0;
				}
			for (int i = 1; i <= cnt; ++i)
			{
				var p = kingpos[i];
				terrain[p.Item1, p.Item2] = 3;
				force[p.Item1, p.Item2] = 1;
			}
		}
		void Operate(int u, int x, int y, int d, int num, int round = 0)
		{
			++x;
			++y;
			if (col[x, y] != u || d < 0 || d > 3 || !checkpos(x, y))
			{
				//add errors
				return;
			}
			num = Math.Min(num, force[x, y] - 1);
			if (num < 0) return;
			int nx = x + dx[d], ny = y + dy[d];
			if (!checkpos(nx, ny) || terrain[nx, ny] == 1)
			{
				//gameplay.Enabled = false;
				//MessageBox.Show(d.ToString()+"\n"+ x.ToString()+" "+y.ToString()+"\n"+nx.ToString()+" "+ny.ToString(), "", MessageBoxButtons.OK);
				//gameplay.Enabled = true;
				return;
			}
			force[x, y] -= num;
			if (col[nx, ny] == u) force[nx, ny] += num;
			else
			{
				force[nx, ny] -= num;
				if(force[nx,ny]<0)
				{
					if (terrain[nx, ny] == 3)
					{
						int def = col[nx, ny];
						defeated[def] = true;
						AI[def].Kill();
						for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if (col[i, j] == def)
								{
									col[i, j] = u;
									render(i, j);
								}
					}
					force[nx, ny] *= -1;
					col[nx, ny] = u;
				}
			}
			render(x, y);
			render(nx, ny);
		}
		void Drawgrid()
		{
			//g.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(1, 1, size, size));
			var pen = new Pen(Brushes.Black);
			pen.Width = 3;
			for (int i = 0; i <= n; ++i)
			{
				g.DrawLine(pen, i * len, 0, i * len, n * len);
				g.DrawLine(pen, 0, i * len, n * len, i * len);
			}
		}
		public void Display(PictureBox rpictureBox1)
		{
			pictureBox1 = rpictureBox1;
			g = pictureBox1.CreateGraphics();
			brushes[0] = new SolidBrush(Color.Gray);
			brushes[1] = new SolidBrush(Color.Blue);
			brushes[2] = new SolidBrush(Color.Red);
			brushes[3] = new SolidBrush(Color.Purple);
			brushes[4] = new SolidBrush(Color.DarkBlue);
			brushes[5] = new SolidBrush(Color.DarkOrange);
			size = (int)Math.Min(pictureBox1.Width, pictureBox1.Height);
			len = size / n;
			g.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(1, 1, size, size));
			Drawgrid();
			for (int i = 1; i <= n; ++i) for (int j = 1; j <= n; ++j)
				{
					render(i, j);
				}
		}
		public void render(int i, int j, bool typ = false)
		{
			if(!typ) g.FillRectangle(brushes[col[i, j]], new Rectangle((i - 1) * len + 2, (j - 1) * len + 2, len - 3, len - 3));
			else g.FillRectangle(brushes[col[i, j]], new Rectangle((i - 1) * len + 2, (j - 1) * len + 2, len-4, 10));
			switch (terrain[i, j])
			{
				case 1:
					g.DrawImage(Properties.Resources.mountain, new Rectangle((i - 1) * len + 2, (j - 1) * len + 2, len - 4, len - 4));
					break;
				case 2:
					g.DrawImage(Properties.Resources.tower, new Rectangle((i - 1) * len + 2, (j - 1) * len + 2, len - 4, len - 4));
					break;
				case 3:
					g.DrawImage(Properties.Resources.king, new Rectangle((i - 1) * len + 2, (j - 1) * len + 2, len - 4, len - 4));
					break;
			}
			if (force[i, j]>0)
			{
				string x = force[i, j].ToString();
				if(x.Length>4)
				{
					x = x.Substring(0, 4) + "...";
				}
				g.DrawString(force[i, j].ToString(), new Font("宋体", 8), Brushes.White, (i - 1) * len + 4, (j - 1) * len + 3);
			}
		}
		int rndcnt=0;
		public delegate void Game_End_handler(int id);
		Game_End_handler endgame;
		int[,] listcnt = new int[M, 3];
		public void Round(object sender, EventArgs e)
		{
			if (finished) return;
			++rndcnt;
			//Drawgrid();
			var pen = new Pen(Brushes.Black);
			for (int i = 1; i <= AI_cnt; ++i) for (int j = 0; j < 3; ++j) listcnt[i, j] = 0;
			for (int i = 0; i <= n; ++i)
			{
				g.DrawLine(pen, i * len, 0, i * len, n * len);
				g.DrawLine(pen, 0, i * len, n * len, i * len);
			}
			for (int i = 1; i <= n; ++i) for (int j = 1; j<=n; ++j) if (col[i, j] > 0)
					{
						listcnt[col[i, j], 0] += force[i, j];
						if (terrain[i, j] >= 2)
						{
							++force[i, j];
							++listcnt[col[i, j], 2];
							render(i, j, true);
						}
						else if (terrain[i, j] == 0 && rndcnt % 25 == 0)
						{
							++force[i, j];
							render(i, j, true);
						}
						if (terrain[i, j] == 0) ++listcnt[col[i, j], 1];
					}
			//list.BeginUpdate();
			for(int i=1; i<=AI_cnt; ++i)
			{
				for (int j=0; j<3; ++j)
				{
					list.Items[i-1].SubItems[j+1].Text = listcnt[i, j].ToString();
				}
				list.Items[i - 1].BackColor = brushes[i].Color;
			}
			//list.EndUpdate();
			int remcnt = 0;
			for (int t = 1; t <= AI_cnt; ++t) if (!defeated[t]) ++remcnt;
			if(remcnt<=1)
			{
				int id = 0;
				for (int t = 1; t <= AI_cnt; ++t) if (!defeated[t]) id = t;
				g.DrawString("Winner is "+brushes[id].Color.ToString(), new Font("宋体", 30), Brushes.White, size/8, size/2);
				gameplay.Enabled = false;
				finished = true;
				Dispose();
				endgame(id);
				return;
			}
			for(int t=1; t<=AI_cnt; ++t) if(!defeated[t])
			{
				string A="", B="", C="";
				for (int i = 1; i <= n; ++i)
				{
					for (int j = 1; j <= n; ++j)
					{
						bool ok = (col[i, j] == t);
						for(int d=0; d<4; ++d)
						{
							if (col[i + dx[d], j + dy[d]] == t) ok = true;
						}
						if(ok)
						{
							A += col[i, j].ToString() + " ";
							B += force[i, j].ToString() + " ";
							C += terrain[i, j].ToString() + " ";
						}
						else
						{
							A += "0 ";
							B += "0 ";
							C += "-1 ";
						}
					}
					A += "\n";
					B += "\n";
					C += "\n";
				}
				try
				{
					AI[t].StandardInput.WriteLine(A);
					AI[t].StandardInput.WriteLine(B);
					AI[t].StandardInput.WriteLine(C);
					string s = AI[t].StandardOutput.ReadLine();
					if (AI[t].HasExited)
					{
						MessageBox.Show(AI[t].StartInfo.FileName, "AI stopped unexpectedly. ", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}


					string[] vs = s.Split(' ');
					Operate(t, Convert.ToInt32(vs[0]), Convert.ToInt32(vs[1]), Convert.ToInt32(vs[2]), Convert.ToInt32(vs[3]));
						//{
						//	gameplay.Enabled = false;
						//	MessageBox.Show(s, "Debug", MessageBoxButtons.OK);
						//	gameplay.Enabled = true;
						//}
				}
				catch (Exception e1)
				{
					gameplay.Enabled = false;
					MessageBox.Show(e1.Message, "An error has occured when running : "+AI[t].StartInfo.FileName);
					AI[t].Kill();
					defeated[t] = true;
					gameplay.Enabled = true;
				}
			}
		}
		public void Start(Game_End_handler end_Handler, ListView list1)
		{
			list = list1;
			endgame = end_Handler;
			AI_cnt = 0;
			finished = false;
			if (Settings.Default.AI == null) return;
			foreach(var p in Settings.Default.AI)
			{
				//if (AI[AI_cnt + 1] != null && !AI[AI_cnt + 1].HasExited)
				//{
				//	MessageBox.Show("fafa", "fa", MessageBoxButtons.OK);
				//}
				AI[++AI_cnt] = new Process();
				var start = new ProcessStartInfo();

				string[] pv = p.Split('\\');
				string name = pv[pv.Length - 1];
				name = name.Substring(0, name.Length - 4);

				var item = new ListViewItem();
				item.Text = name;
				item.SubItems.Add("0");
				item.SubItems.Add("0");
				item.SubItems.Add("0");
				item.SubItems.Add("1");
				item.BackColor = brushes[AI_cnt].Color;
				//list.BeginUpdate();
				list.Items.Add(item);
				//list.EndUpdate();

				AInames[AI_cnt] = name;
				start.FileName = p;
				start.UseShellExecute = false;
				start.RedirectStandardInput = true;
				start.RedirectStandardOutput = true;
				start.CreateNoWindow = true;
				AI[AI_cnt].StartInfo = start;
				try
				{
					AI[AI_cnt].Start();
					AI[AI_cnt].StandardInput.WriteLine(AI_cnt.ToString());
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, p);
				}
			}
			if (AI_cnt > cnt)
			{
				var buttons = MessageBoxButtons.OK;
				MessageBox.Show("AI Count can not be greater than King Tower count. AI Count is now set to King Tower Count.",
					"Warining", buttons, MessageBoxIcon.Warning);
				AI_cnt = cnt;
			}
			for(int i=cnt; i>0; --i)
			{
				int p = rnd.Next() % i + 1;
				var t = kingpos[p];
				kingpos[p] = kingpos[i];
				kingpos[i] = t;
			}
			for(int i=1; i<=AI_cnt; ++i)
			{
				var p = kingpos[i];
				col[p.Item1, p.Item2]=i;
				render(p.Item1, p.Item2);
			}
			for (int i = AI_cnt+1; i <= cnt; ++i)
			{
				var p = kingpos[i];
				terrain[p.Item1, p.Item2] = 2;
				force[p.Item1, p.Item2] = 50;
				render(p.Item1, p.Item2);
			}
		}
	}
}
