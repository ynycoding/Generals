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
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;


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

		public Game game=new Game();

		private void Form2_Load(object sender, EventArgs e)
		{
			game.label1 = label1;
			game.finished = true;
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
			if (!game.finished) return;
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
				if (!pause)
				{
					//game.Display(pictureBox1);
					game.Start(End_Game, listView1);
				}
				pause = false;
				if (game.gameplay==null)
				{
					game.gameplay = new System.Windows.Forms.Timer();
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

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			game.MouseDown(e);
		}

		private void Form2_Resize(object sender, EventArgs e)
		{
			int len = Math.Min(Height - pictureBox1.Location.Y-50, listView1.Location.X - 5);
			pictureBox1.Size = new Size(len, len);
		}

		private void Form2_KeyDown(object sender, KeyEventArgs e)
		{
			game.KeyDown(e);
		}

		private void Form2_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		private void button3_Click(object sender, EventArgs e)
		{
			game.gameplay.Enabled = false;
			game.finished = true;
			game.Dispose();
			End_Game(-1);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			DialogResult ok = MessageBox.Show("Are you sure to surrender?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
			if (ok == DialogResult.OK)
			{
				game.Surrender();
			}
		}

		private void loadFromGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!game.finished) return;
			game.LoadFromGenerator();
			game.Dispose();
			game.Display(pictureBox1);
			pause = false;
			button1.Text = "Start Game";
		}
	}
	public class Map
	{
		public const int N = 205;
		public int n, cnt;
		public bool loaded, finished=false;
		public int[,] terrain = new int[N, N], force = new int[N, N];
		public Tuple<int, int>[] kingpos = new Tuple<int, int>[N*N];
		public bool checkpos(int x, int y)
		{
			return (x > 0 && y > 0 && x <= n && y <= n);
		}
		public void LoadFromGenerator()
		{
			string file = Settings.Default.Generator;
			string s = Settings.Default.GenArg;
			if (!File.Exists(file)) return;
			Process gen = new Process();
			var start = new ProcessStartInfo();
			start.FileName = file;
			start.UseShellExecute = false;
			start.RedirectStandardInput = true;
			start.RedirectStandardOutput = true;
			start.CreateNoWindow = true;
			gen.StartInfo = start;
			try
			{
				gen.Start();
				gen.StandardInput.WriteLine(s);
				LoadFromStream(gen.StandardOutput);
				if (!gen.HasExited) gen.Kill();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, file);
			}
		}
		public void LoadFromStream(StreamReader p)
		{
			try
			{
				string line = p.ReadLine();
				n = Convert.ToInt32(line);
				cnt = 0;
				bool rule = Settings.Default.ViewAsKing;
				for (int i = 1; i <= n; ++i)
				{
					line = p.ReadLine();
					string[] num = line.Split();
					for (int j = 1; j <= n; ++j)
					{
						terrain[i, j] = Convert.ToInt32(num[j - 1]);
						if (terrain[i, j] == 2 && rule) terrain[i, j] = 3;
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
		}
		public bool Load(string path)
		{
			if (!File.Exists(path))
			{
				MessageBox.Show("Map File does not exist", "Error");
				return false;
			}
			StreamReader p = new StreamReader(path);
			LoadFromStream(p);
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
		public Label label1;
		public bool hidden;
		const int M = 17;
		public int[,] col = new int[N, N];
		public string[] name = new string[N];
		public List<string> errors;
		SolidBrush[] brushes = new SolidBrush[18];
		public System.Windows.Forms.Timer gameplay;
		public bool issandbox=false;
		int[] dx = new int[4] { 0, 0, -1, 1 }, dy = new int[4] { -1, 1, 0, 0 };
		Process[] AI = new Process[M];
		string[] AInames=new string[M];
		//AnonymousPipeServerStream[] Pipes = new AnonymousPipeServerStream[M];
		int AI_cnt, size, len, rlen;
		Random rnd = new Random();
		bool[] defeated=new bool[M];
		PictureBox pictureBox1;
		ListView list;
		Graphics g;
		Image canvas;
		public bool isplayer=false;
		int playerid = -1, rule;
		int[] typ=new int [N];
		public int cx = 0, cy = 0;
		Queue<Tuple<int, int, int, int>> operation=new Queue<Tuple<int, int, int, int>>();
		~Game()
		{
			Dispose();
		}
		bool chk(int i, int j, int t)
		{
			for (int a=-1; a<=1; ++a) for(int b=-1; b<=1; ++b)
			{
					if (col[i + a, j + b] == t) return true;
			}
			return false;
		}
		public void Surrender()
		{
			if (playerid > 0)
			{
				int t = playerid;
				defeated[playerid] = true;
				for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if (col[i, j] == t)
				{
					col[i, j] = 0;
					if (terrain[i, j] == 3) terrain[i, j] = 2;
				}
				playerid = -1;
				operation.Clear();
				Drawgrid();
			}
		}
		void DrawOperation()
		{
			foreach (var cur in operation)
			{
				int x = cur.Item1, y = cur.Item2, d = cur.Item3;
				Point start = new Point(x * len - len / 2, y * len - len / 2);
				Point end = new Point((x + dx[d]) * len - len / 2, (y+dy[d]) * len - len / 2);
				var arrowcap = new AdjustableArrowCap(6, 6, true);
				Pen arrow = new Pen(Color.White, 4);
				arrow.CustomEndCap = arrowcap;
				g.DrawLine(arrow, start, end);
			}
		}
		void Relocate(int x, int y)
		{
			if (cx > 0) render(cx, cy, playerid, true, false);
			cx = x;
			cy = y;
			//}
			render(cx, cy, playerid, true, true);
			DrawOperation();
			pictureBox1.Image = canvas;
		}
		public void MouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && pictureBox1 != null)
			{
				Point p = e.Location;
				//p.Offset(new Point(-pictureBox1.Location.X, -pictureBox1.Location.Y));
				//rlen = pictureBox1.Height / n;
				Relocate(p.X * n / pictureBox1.Height + 1, p.Y * n / pictureBox1.Height + 1);
				//if (x % rlen > 0 && x % rlen < rlen - 1 && y % rlen > 0 && y % rlen < rlen - 1)
				//{
			}
		}
		[DllImport("user32.dll")]
		static extern short GetAsyncKeyState(int vkey);
		public void KeyDown(KeyEventArgs e)
		{
			if (playerid>0 && e.KeyValue == 'Q')
			{
				operation.Clear();
				Drawgrid();
			}
			if (playerid>0 && cx > 0 && pictureBox1 != null)
			{
				int d = -1;
				switch (e.KeyValue)
				{
					case 'A':
						d = 2;
						break;
					case 'D':
						d = 3;
						break;
					case 'W':
						d = 0;
						break;
					case 'S':
						d = 1;
						break;
				}
				if (d != -1)
				{
					bool ishalf = GetAsyncKeyState(17) != 0;
					operation.Enqueue(new Tuple<int, int, int, int>(cx, cy, d, (ishalf ? 1 : 0)));
					Relocate(cx + dx[d], cy + dy[d]);
				}
			}
		}
		public void Dispose()
		{
			operation.Clear();
			cx = 0;
			cy = 0;
			//if(gameplay!=null) gameplay.Dispose();
			finished = false;
			for (int i = 1; i <= AI_cnt; ++i)
			{
				defeated[i] = false;
				if (typ[i]==0&&!AI[i].HasExited)
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
		bool Operate(int u, int x, int y, int d, int num, int round = 0)
		{
			++x;
			++y;
			if (!checkpos(x, y) || col[x, y] != u || d < 0 || d > 3)
			{
				//if(col[x, y] != u) MessageBox.Show("fa");
				//add errors
				return false;
			}
			num = Math.Min(num, force[x, y] - 1);
			if (num < 0) return false;
			int nx = x + dx[d], ny = y + dy[d];
			if (!checkpos(nx, ny) || terrain[nx, ny] == 1)
			{
				//gameplay.Enabled = false;
				//MessageBox.Show(d.ToString() + "\n" + x.ToString() + " " + y.ToString() + "\n" + nx.ToString() + " " + ny.ToString(), "", MessageBoxButtons.OK);
				//gameplay.Enabled = true;
				return false;
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
						terrain[nx, ny] = 2;
						if(def==playerid)
						{
							playerid = -1;
							Drawgrid();
							Thread t = new Thread(() => MessageBox.Show("You are Defeated!"));
							t.Start();
						}
						else AI[def].Kill();
						defeated[def] = true;
						for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if (col[i, j] == def)
						{
							col[i, j] = u;
							render(i, j, playerid);
						}
					}
					force[nx, ny] *= -1;
					col[nx, ny] = u;
				}
			}
			if (playerid == -1)
			{
				render(x, y, playerid);
				render(nx, ny, playerid);
			}
			return true;
		}
		void Drawgrid()
		{
			if (hidden) return;
			//g.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(1, 1, size, size));
			var pen = new Pen(Brushes.Black);
			pen.Width = 4;
			for (int i = 0; i <= n; ++i)
			{
				g.DrawLine(pen, i * len, 0, i * len, n * len);
				g.DrawLine(pen, 0, i * len, n * len, i * len);
			}
			for (int i = 1; i <= n; ++i) for (int j = 1; j <= n; ++j)
					render(i, j, playerid);
		}
		void DrawSelected()
		{
			if (hidden) return;
			if (cx > 0) render(cx, cy, playerid, true, true);
		}
		public void Display(PictureBox rpictureBox1)
		{
			if (hidden) return;
			pictureBox1 = rpictureBox1;
			canvas = new Bitmap(80*n, 80*n);
			g = Graphics.FromImage(canvas);
			brushes[0] = new SolidBrush(Color.Gray);
			brushes[1] = new SolidBrush(Color.Blue);
			brushes[2] = new SolidBrush(Color.Red);
			brushes[3] = new SolidBrush(Color.Purple);
			brushes[4] = new SolidBrush(Color.DarkBlue);
			brushes[5] = new SolidBrush(Color.DarkOrange);
			brushes[7] = new SolidBrush(Color.DarkCyan);
			brushes[8] = new SolidBrush(Color.Violet);
			size = 80*n;
			len = size / n;
			rlen = pictureBox1.Height / n;
			//g.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(1, 1, size, size));
			Drawgrid();
			//for (int i = 1; i <= n; ++i) for (int j = 1; j <= n; ++j)
			//	{
			//		render(i, j);
			//	}
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBox1.Image = canvas;
		}
		public void render(int i, int j, int u = -1, bool typ = false, bool typ1=false)
		{
			if (hidden) return;
			if (!checkpos(i, j)) return;
			int cc = -1, cf = 0, ter = Math.Min(terrain[i, j], rule);
			if (u==-1||chk(i, j, u))
			{
				cc = col[i, j];
				cf = force[i, j];
				ter = terrain[i, j];
			}
			SolidBrush bru = new SolidBrush(brushes[cc == -1 ? 0 : cc].Color);
			if (cc == -1)
			{
				cc = 0;
				bru.Color = Color.FromArgb(80, 80, 80);
			}
				//	if (u == -1 || chk(i, j, u)) bru.Color = Color.Gray;
				//}
			g.FillRectangle(bru, new Rectangle((i - 1) * len + 2, (j - 1) * len + 2, len - 4, len - 4));
			//else g.FillRectangle(brushes[col[i, j]], new Rectangle((i - 1) * len + 2, (j - 1) * len + 2, len-4, 10));
			switch (ter)
			{
				case 1:
					g.DrawImage(Properties.Resources.mountain, new Rectangle((i - 1) * len + 3, (j - 1) * len + 3, len - 5, len - 5));
					break;
				case 2:
					g.DrawImage(Properties.Resources.tower, new Rectangle((i - 1) * len + 3, (j - 1) * len + 3, len - 5, len - 5));
					break;
				case 3:
					g.DrawImage(Properties.Resources.king, new Rectangle((i - 1) * len + 3, (j - 1) * len + 3, len - 5, len - 5));
					break;
			}
			if (cf>0)
			{
				string x = cf.ToString();
				if(x.Length>4)
				{
					x = x.Substring(0, 4) + "...";
				}
				var format = new StringFormat();
				format.Alignment = StringAlignment.Center;
				g.DrawString(cf.ToString(), new Font("Consolas", 16), Brushes.White, 
					new Rectangle((i - 1) * len + 2, (j - 1) * len + 10, len - 3, len - 3), format);
			}
			if (typ)
			{
				var pen = new Pen((typ1 ? Color.White : Color.Black), 4);
				g.DrawRectangle(pen, new Rectangle((i - 1) * len, (j - 1) * len, len, len));
			}
		}
		int rndcnt=0;
		public delegate void Game_End_handler(int id);
		Game_End_handler endgame;
		int[,] listcnt = new int[M, 3];
		bool[,] vis = new bool[N, N];
		public void Round(object sender, EventArgs e)
		{
			if (finished) return;
			Stopwatch watch = new Stopwatch();
			watch.Start();
			++rndcnt;
			label1.Text = "Round: " + rndcnt;
			//Drawgrid();
			for (int i = 1; i <= AI_cnt; ++i) for (int j = 0; j < 3; ++j) listcnt[i, j] = 0;
			//var pen = new Pen(Brushes.Black, 6);
			//for (int i = 0; i <= n; ++i)
			//{
			//	g.DrawLine(pen, i * len, 0, i * len, n * len);
			//	g.DrawLine(pen, 0, i * len, n * len, i * len);
			//}
			for (int i = 1; i <= n; ++i) for (int j = 1; j<=n; ++j)
			{
				if (col[i, j] > 0)
				{
					listcnt[col[i, j], 0] += force[i, j];
					if (terrain[i, j] >= 2)
					{
						if(rndcnt % 2 == 0) ++force[i, j];
						++listcnt[col[i, j], 2];
						render(i, j, playerid);
					}
					else if (terrain[i, j] == 0 && rndcnt % 50 == 0)
					{
						++force[i, j];
						render(i, j, playerid);
					}
					if (terrain[i, j] == 0) ++listcnt[col[i, j], 1];
				}
				vis[i, j] = (playerid == -1 || chk(i, j, playerid));
			}
			DrawSelected();
			DrawOperation();
			if(!hidden) pictureBox1.Image = canvas;
			//list.BeginUpdate();
			if(!hidden) for (int i=1; i<=AI_cnt; ++i)
			{
				for (int j=0; j<3; ++j)
				{
					list.Items[i-1].SubItems[j+1].Text = listcnt[i, j].ToString();
				}
				list.Items[i - 1].BackColor = brushes[i].Color;
				list.Items[i - 1].ForeColor = Color.White;//Color.FromArgb(brushes[i].Color.ToArgb() ^ 0xffffff);
			}
			//list.EndUpdate();
			int remcnt = 0;
			for (int t = 1; t <= AI_cnt; ++t) if (!defeated[t]) ++remcnt;
			if(remcnt<=1&&!issandbox)
			{
				int id = 0;
				for (int t = 1; t <= AI_cnt; ++t) if (!defeated[t]) id = t;
				if (!hidden) g.DrawString("Winner is "+brushes[id].Color.ToString(), new Font("宋体", 60), Brushes.White, size/8, size/2);
				gameplay.Enabled = false;
				finished = true;
				Dispose();
				endgame(id);
				return;
			}
			for(int t=1; t<=AI_cnt; ++t) if(!defeated[t])
			{
				if (t == playerid)
				{
					while (operation.Count > 0)
					{
						var cur = operation.Dequeue();
						int x = cur.Item1, y = cur.Item2, d = cur.Item3, cf = (cur.Item4 == 1 ? force[x, y] / 2 : force[x, y]);
						render(x, y);
						render(x + dx[d], y + dy[d]);
						if (Operate(t, x-1, y-1, d, cf)) break;
					}
					Drawgrid();
					DrawSelected();
					DrawOperation();
					continue;
				}
				string A="", B="", C="";
				for (int i = 1; i <= n; ++i)
				{
					for (int j = 1; j <= n; ++j)
					{
						bool ok = chk(i, j, t);
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
							//C += "-1 ";
							C += Math.Min(terrain[i, j], rule).ToString()+" ";
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
						gameplay.Enabled = false;
						finished = true;
						Dispose();
						endgame(0);
						MessageBox.Show(AI[t].StartInfo.FileName, "AI stopped unexpectedly. ", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
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
						MessageBox.Show("An error has occured when running : " + AI[t].StartInfo.FileName +" : "+e1.Message);
					AI[t].Kill();
					defeated[t] = true;
						//gameplay.Enabled = true;
						endgame(0);
				}
			}
			for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j)
			{
				bool t = (playerid == -1 || chk(i, j, playerid));
				if(t!=vis[i,j]) render(i, j, playerid);
			}
			DrawOperation();
			watch.Stop();
			if(!hidden) gameplay.Interval = Math.Max(Settings.Default.Gap - watch.Elapsed.Milliseconds, 1);
		}
		public void Start(Game_End_handler end_Handler, ListView list1)
		{
			rndcnt = 0;
			label1.Text = "Round: " + rndcnt;
			Drawgrid();
			rule = Settings.Default.Rule;
			if ((rule & 1) == 0) rule = 0;
			if (rule == 0) rule = -1;
			rule = Math.Min(rule, 2);
			if (list != null)
			{
				//list.BeginUpdate();
				list.Items.Clear();
				//list.EndUpdate();
			}
			list = list1;
			endgame = end_Handler;
			AI_cnt = 0;
			finished = false;
			if (Settings.Default.AI == null) return;
			if (isplayer)
			{
				typ[++AI_cnt] = 1;
				playerid = AI_cnt;
				string name = "Player";
				AInames[AI_cnt] = name;
				var item = new ListViewItem();
				item.Text = name;
				item.SubItems.Add("0");
				item.SubItems.Add("0");
				item.SubItems.Add("0");
				item.SubItems.Add("1");
				item.ForeColor = Color.White;
				item.BackColor = brushes[AI_cnt].Color;
				list.Items.Add(item);
			}
			foreach (var p in Settings.Default.AI)
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

				if(!hidden)
				{
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
				}

				AInames[AI_cnt] = name;
				typ[AI_cnt] = 0;
				start.FileName = p;
				start.UseShellExecute = false;
				start.RedirectStandardInput = true;
				start.RedirectStandardOutput = true;
				start.CreateNoWindow = true;
				AI[AI_cnt].StartInfo = start;
				try
				{
					AI[AI_cnt].Start();
					int t = AI_cnt;
					if (AI[t].HasExited)
					{
						MessageBox.Show(AI[t].StartInfo.FileName, "AI failed to start. ", MessageBoxButtons.OK, MessageBoxIcon.Error);
						gameplay.Enabled = false;
						finished = true;
						Dispose();
						endgame(0);
						return;
					}
					AI[AI_cnt].StandardInput.WriteLine(AI_cnt.ToString());
					if(!Settings.Default.Legacy) AI[AI_cnt].StandardInput.WriteLine(n.ToString());
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
			Spawn();
		}
		int[,] dis = new int[N, N];
		void bfs(int sx, int sy)
		{
			for (int i = 1; i <= n; ++i) for (int j = 1; j <= n; ++j) dis[i, j] = -1;
			Queue<Tuple<int, int>> q=new Queue<Tuple<int, int>>();
			q.Enqueue(new Tuple<int, int>(sx,sy));
			dis[sx, sy] = 0;
			while (q.Count() > 0)
			{
				var t = q.Dequeue();
				int x = t.Item1, y = t.Item2;
				for(int d=0; d<4; ++d)
				{
					int nx = x + dx[d], ny = y + dy[d];
					if(checkpos(nx, ny)&&dis[nx, ny]==-1&&terrain[nx,ny]!=1)
					{
						dis[nx, ny] = dis[x, y] + 1;
						q.Enqueue(new Tuple<int, int>(nx, ny));
					}
				}
			}
		}
		void Spawn()
		{
			int n = cnt;
			int[] id = new int[n+2];
			int[,] kdis = new int[n + 2, n + 2];
			for(int i=1; i<=n; ++i)
			{
				id[i] = i;
				bfs(kingpos[i].Item1, kingpos[i].Item2);
				for (int j = 1; j <= n; ++j) kdis[i, j] = dis[kingpos[j].Item1, kingpos[j].Item2];
			}
			int lim = 2*n;
			while (true)
			{
				int T = 1000;
				bool ok = true;
				while (T > 0)
				{
					T--;
					for (int i = n; i > 0; --i)
					{
						int p = rnd.Next() % i + 1;
						int t = id[p];
						id[p] = id[i];
						id[i] = t;
					}
					bool fl = false;
					for(int i=1; i<=AI_cnt&&!fl; ++i) for(int j=i+1; j<=AI_cnt&&!fl; ++j)
					{
						if (kdis[id[i], id[j]] < lim) fl = true;
					}
					ok = fl;
					if (!fl) break;
				}
				if (!ok) break;
				lim--;
			}
			//MessageBox.Show(lim.ToString());
			for (int i = 1; i <= AI_cnt; ++i)
			{
				var p = kingpos[id[i]];
				col[p.Item1, p.Item2] = i;
				force[p.Item1, p.Item2] = 1;
				render(p.Item1, p.Item2);
			}
			for (int i = AI_cnt + 1; i <= cnt; ++i)
			{
				var p = kingpos[id[i]];
				terrain[p.Item1, p.Item2] = 2;
				force[p.Item1, p.Item2] = 50;
				render(p.Item1, p.Item2);
			}
		}
	}
}