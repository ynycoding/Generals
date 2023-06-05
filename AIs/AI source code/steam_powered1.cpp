#include <cstdio>
#include <queue>
#include <cmath>
#include <random>
#include <cstring>
#include <cmath>
#include <cstdlib>
#include <iostream>
#include <algorithm>
#define ERR(s...) fprintf(stderr, s)
#define ld long double
#define ll long long
#define x first
#define y second
using std::cin;
using std::cout;
using std::endl;
using std::queue;
using std::pair;
std::mt19937 rnd(114);
int T, color;
int dx[]={0,0,-1,1};
int dy[]={-1,1,0,0};
namespace Game{
	const int N=55;
	int n=20, mp[3][N][N], (*col)[N]=mp[0], (*force)[N]=mp[1], (*type)[N]=mp[2], cx, cy, kx, ky;
	int army[N], land[N], ter[N][N], stamp[N][N], pcol[N][N], pf[N][N], tf[N][N], rev[N][N];
	inline bool chkpos(int x, int y) { return x>0&&y>0&&x<=n&&y<=n; }
	inline bool chkvis(int x, int y)
	{
		if(col[x][y]==color) return 1;
		for(int a:{-1, 0, 1}) for(int b:{-1, 0, 1}) if(chkpos(x+a, y+b)&&col[x+a][y+b]==color) return 1;
		return 0;
	}
	inline void rd(void)
	{
		++T;
		memset(army, 0, sizeof(army));
		for(int k=0;k<3;k++) {
			for(int i=1;i<=n;i++) {
				for(int j=1;j<=n;j++) {
					cin>>mp[k][i][j];
				}
			}
		}
		for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j)
		{
			if(chkvis(i, j))
			{
				rev[i][j]=1;
				++land[col[i][j]], ter[i][j]=type[i][j], stamp[i][j]=T;
				pcol[i][j]=col[i][j], pf[i][j]=force[i][j];
			}
			else if(pcol[i][j])
			{
				if(ter[i][j]==0&&T%25==0) ++pf[i][j];
				if(ter[i][j]&2) ++pf[i][j];
			}
			if(!rev[i][j]) ter[i][j]=type[i][j];
			if(col[i][j]==color&&type[i][j]==3) kx=i, ky=j;
			army[col[i][j]]+=pf[i][j];
			if(!rev[i][j]) ter[i][j]=type[i][j];
			tf[i][j]=(col[i][j]==color?pf[i][j]:-pf[i][j]);
			// if(col[i][j]&&col[i][j]!=color&&type[i][j]!=3) tf[i][j]-=army[color]/4;
		}
	}
	queue<int> qx, qy;
	int dis[N][N];
//	inline int dis(int a, int b) { return abs(a-kx)+abs(b-ky); }
	inline void pq(int x, int y, int d)
	{ if(!chkpos(x, y)||dis[x][y]>=0||ter[x][y]==1) return; dis[x][y]=d, qx.push(x), qy.push(y); }
	inline void gdis(int x, int y)
	{
		for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) dis[i][j]=-0x3f3f3f;
		// for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if(col[i][j]==color) dis[i][j]=1, qx.push(i), qy.push(j);
		qx.push(x), qy.push(y);
		// memset(dis, 0x3f, sizeof(dis));
		dis[x][y]=1;
		while(!qx.empty())
		{
			x=qx.front(), y=qy.front();
			qx.pop(), qy.pop();
			for(int t=0; t<4; ++t) pq(x+dx[t], y+dy[t], dis[x][y]+1);
		}
	}
	inline void debug(void)
	{
		FILE *p=fopen("debug.txt", "w");
		for(int i=0; i<20; ++i, fprintf(p, "\n")) for(int j=0; j<20; ++j)
		fprintf(p, "%d ", ter[i][j]);
		fprintf(p, "\n");
		fclose(p);
	}
	inline void move(int x, int y, int dir, int cf=-1)
	{ ERR("fa %d %d %d %d\n", x, y, dir, force[x][y]-1); fflush(stderr); cout<<x-1<<" "<<y-1<<" "<<dir<<" "<<(cf==-1?force[x][y]-1:cf)<<endl; }

	int cad;
	int in[N][N], in1[N][N], cur, sum, sumf, rsum, mx;
	int ccin[N][N], csum, cval, ctim=-100;
	int pin[N][N], pcur, psum, psumf;
	inline bool chkadj(int x, int y)
	{
		for(int d:{0, 1, 2, 3}) if(chkpos(x+dx[d], y+dy[d])&&in[x+dx[d]][y+dy[d]]) return 1;
		return 0;
	}
	inline bool chkadjcol(int x, int y)
	{
		for(int d:{0, 1, 2, 3}) if(chkpos(x+dx[d], y+dy[d])&&col[x+dx[d]][y+dy[d]]==color) return 1;
		return 0;
	}
	int low[N][N], dfn[N][N], iscut[N][N], cnt;
	bool vis[N][N];
	void tarjan(int x, int y, int fx, int fy)
	{
		low[x][y]=dfn[x][y]=++cnt;
		int cnt=fx>0;
		for(int d:{0, 1, 2, 3})
		{
			int nx=x+dx[d], ny=y+dy[d];
			if(!chkpos(nx, ny)||!in[nx][ny]) continue;
			if(!dfn[nx][ny])
			{
				tarjan(nx, ny, x, y);
				if(low[nx][ny]>=dfn[x][y]) ++cnt;
				low[x][y]=std::min(low[x][y], low[nx][ny]);
			}
			else if(nx!=fx||ny!=fy)
			{
				low[x][y]=std::min(low[x][y], dfn[nx][ny]);
			}
		}
		iscut[x][y]=cnt>1;
	}
	int getsol(int x, int y, int rq)
	{
		ld T=10;
		memset(in, 0, sizeof(in));
		memset(in1, 0, sizeof(in1));
		cur=mx=0;
		in[x][y]=1;
		sum=-rq;
		{
			queue<pair<int, int> > q;
			q.push({x, y});
			in[x][y]=1;
			while(!q.empty())
			{
				auto [x, y]=q.front();
				q.pop();
		// ERR("in %d %d\n", x, y);
		// fflush(stderr);
				for(int d:{0, 1, 2, 3})
				{
					int nx=x+dx[d], ny=y+dy[d];
					if(chkpos(nx, ny)&&col[nx][ny]==color&&!in[nx][ny]) q.push({nx, ny}), in[nx][ny]=1, sum+=pf[nx][ny]-1;
				}
			}
		}
		// ERR("getsol %d %d %d %d %d\n", tf[x][y], x, y, type[x][y], sum);
		// for(int i=1; i<=n; ++i, ERR("\n")) for(int j=1; j<=n; ++j) ERR("%5d ", (in[j][i]?-1:1)*(type[j][i]+1));
		// fflush(stderr);
		if(sum<0) return -1;
		static pair<int, int> pos[N*N];
		int top=0;
		mx=n*n;
		rsum=sum;
		memcpy(in1, in, sizeof(in));
		while(T>1e-3)
		{
			psum=sum;
			memcpy(pin, in, sizeof(in));
			int tot=0;
			top=0;
			for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if(in[i][j]) pos[++top]={i, j};
			tot=top;
			int pval=tot;
			if((rnd()%5)&&top)
			{
				memset(dfn, 0, sizeof(dfn));
				memset(iscut, 0, sizeof(iscut));
				tarjan(x, y, 0, 0);
				iscut[x][y]=1;
				cnt=0;
				int mn=0x3f3f3f3f;
				for(int i=1; i<=top; ++i) if(!iscut[pos[i].x][pos[i].y]) mn=std::min(mn, tf[pos[i].x][pos[i].y]-1);
				fflush(stderr);
				if(mn>sum) goto out;
				int t;
				re:;
				t=rnd()%top+1;
				int cx=pos[t].x, cy=pos[t].y;
				if(!in[cx][cy]||iscut[cx][cy]||tf[cx][cy]-1>sum) goto re;
				in[cx][cy]=0;
				sum-=tf[cx][cy]-1;
				--tot;
			}
			else
			{
				out:;
				top=0;
				for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if(!in[i][j]&&chkadj(i, j)&&sum+tf[i][j]-1>=0&&col[i][j]==color)
					pos[++top]={i, j};
				if(!top) { T*=0.99; continue; }
				int t=rnd()%top+1;
				int cx=pos[t].x, cy=pos[t].y;
				in[cx][cy]=1;
				++tot;
				sum+=tf[cx][cy]-1;
			}
			int val=tot;
			// fprintf(stderr, "%lf\n", exp((-val+pval)*10/T));
			// fflush(stderr);
			if(val<mx) memcpy(in1, in, sizeof(in)), mx=val, rsum=sum;
			else if(val>pval&&rnd()%100+1>exp((-val+pval)*2/T)*100)
			{
				sum=psum;
				memcpy(in, pin, sizeof(pin));
			}
			T*=0.99;
			// ERR("sum %d\n", sumf-sum+tot+1);
			// fflush(stderr);
		}
			// ERR("sum %d %d\n", sum, mx);
			// fflush(stderr);
		sum=rsum;
			// ERR("sum %d %d\n", sum, mx);
			// fflush(stderr);
		memcpy(in, in1, sizeof(in1));
		return mx;
	}
	int is[N][N], rem[N][N];
	void dfs(int x, int y, int fa)
	{
		// ERR("dfs %d %d\n", x, y);
		// fflush(stderr);
		in[x][y]=0;
		// int ret=tf[x][y]-1-rem[x][y];
		// int ct=0;
		for(int d:{0, 1, 2, 3})
		{
			int nx=x+dx[d], ny=y+dy[d];
			if(!chkpos(nx, ny)||!in[nx][ny]) continue;
			dfs(nx, ny, d);
			in[x][y]=1;
			return;
			// if(v<0) is[nx][ny]=1;
			// if(v>0) is[x][y]=1;
			// ret+=v;
		}
		move(x, y, fa^1);
	}
	inline void proc(int x, int y)
	{
		// ERR("proc %d %d %d\n", x, y, col[x][y]!=color);
		// fflush(stderr);
		// for(int i=1; i<=n; ++i, ERR("\n")) for(int j=1; j<=n; ++j) ERR("%5d ", (in[j][i]?1:0)*(pf[j][i]+1)+((i==y&&j==x)?100:0));
		// for(int i=1; i<=n; ++i, ERR("\n")) for(int j=1; j<=n; ++j) ERR("%d ", in[j][i]);
		// fflush(stderr);
		dfs(x, y, 0);
	}
	inline void solve(void)
	{
		// ERR("T %d %d\n", T, color);
		// for(int i=1; i<=n; ++i, ERR("\n")) for(int j=1; j<=n; ++j) ERR("%5d ", (in[j][i]?-1:1)*(type[j][i]+1));
		// fflush(stderr);
		rd();
		int ct=0, mx=-1, avail=0;
		for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if(col[i][j]==color) ct+=in[i][j], avail+=pf[i][j]-1;
		if(ct>1) goto sout;
		gdis(kx, ky);
		// ERR("T %d %d\n", T, color);
		// fflush(stderr);
		for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if(ter[i][j]!=1&&chkadjcol(i, j))
		{
			if(col[i][j]&&col[i][j]!=color&&dis[i][j]<=2) { cx=i, cy=j; break; }
			if(col[i][j]!=color)
			{
				int val=100*(ter[i][j]==2?(25+avail/10):1)/(pf[i][j]+1);
				if(ter[i][j]==3) val=0x1f3f3f3f;
				if(col[i][j])
				{
					val*=2;
					val+=100;
					if(dis[i][j]<=4) val=0x3f3f3f;
					if(getsol(i, j, pf[i][j]+1)<0) continue;
				}
				else val/=getsol(i, j, pf[i][j]+1);
				if(val>mx) mx=val, cx=i, cy=j;
			}
		}
		{
		int cur=pf[cx][cy]+1;
		// ERR("cx %d %d %d\n", cx, cy, pf[cx][cy]+1);
		if(col[cx][cy])
		{
			if(getsol(cx, cy, std::min(avail, cur))>2)
			{
				getsol(cx, cy, std::min(avail, cur+avail/4));
			}
		}
		getsol(cx, cy, std::min(avail, pf[cx][cy]+1));
		// ERR("sum %d\n", sum);
		// fflush(stderr);
		}
		sout:;
		proc(cx, cy);
	}
}
int main() {
	freopen("debug.txt", "w", stderr);
	// for(int i=0; i<T::N; ++i) for(int j=0; j<T::N; ++j) T::rev[i][j]=-1;
	// memset(T::terrain, -1, sizeof(T::terrain));
	cin>>color;
	for(;true;) Game::solve();
}