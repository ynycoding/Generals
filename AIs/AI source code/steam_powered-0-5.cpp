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
	int n=20, mp[3][N][N], (*pcol)[N]=mp[0], (*force)[N]=mp[1], (*type)[N]=mp[2], cx, cy, kx, ky;
	int army[N], land[N], ter[N][N], stamp[N][N], col[N][N], pf[N][N], tf[N][N], rev[N][N], dds[N][N];
	int revking, revtow;
	inline bool chkpos(int x, int y) { return x>0&&y>0&&x<=n&&y<=n; }
	inline bool chkvis(int x, int y)
	{
		if(pcol[x][y]==color) return 1;
		for(int a:{-1, 0, 1}) for(int b:{-1, 0, 1}) if(chkpos(x+a, y+b)&&pcol[x+a][y+b]==color) return 1;
		return 0;
	}
	int tt=10;
	int tad[N][N];
	inline void rd(void)
	{
		++T;
		revking=revtow=0;
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
				++land[col[i][j]], ter[i][j]=type[i][j], stamp[i][j]=T;
				col[i][j]=pcol[i][j], pf[i][j]=force[i][j];
				if(!rev[i][j])
				{
					if(ter[i][j]==3) revking=1;
					else if(ter[i][j]==2) revtow=1;
				}
				rev[i][j]=1;
			}
			else if(pcol[i][j])
			{
				if(ter[i][j]==0&&T%25==0) ++pf[i][j];
				if(ter[i][j]&2) ++pf[i][j];
			}
			if(!rev[i][j]) ter[i][j]=type[i][j];
			if(pcol[i][j]==color&&type[i][j]==3) kx=i, ky=j;
			army[col[i][j]]+=pf[i][j];
			if(!rev[i][j]) ter[i][j]=type[i][j];
			tf[i][j]=(col[i][j]==color?pf[i][j]:-pf[i][j]);
			if((ter[i][j]&2)&&col[i][j]&&col[i][j]!=color) tf[i][j]-=n*1.5;
			// if(col[i][j]&&col[i][j]!=color&&type[i][j]!=3) tf[i][j]-=army[color]/4;
		}
		// for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j)
		// {
			// if(col[i][j]!=color&&ter[i][j]==0&&army[color]>500) tf[i][j]*=2;
		// }
		// while(tt)
		// {
		// 	--tt;
		// 	int x=rnd()%n+1, y=rnd()%n+1;
		// 	if(ter[x][y]==0) tad[x][y]+=5000000;
		// }
	}
	queue<int> qx, qy;
	int dis[N][N];
//	inline int dis(int a, int b) { return abs(a-kx)+abs(b-ky); }
	inline void pq(int x, int y, int d)
	{ if(!chkpos(x, y)||dis[x][y]>=0||ter[x][y]==1) return; dis[x][y]=d, qx.push(x), qy.push(y); }
	inline void pq1(int x, int y, int d)
	{ if(!chkpos(x, y)||dds[x][y]>=0||ter[x][y]==1) return; dds[x][y]=d, qx.push(x), qy.push(y); }
	inline void gdis(int x, int y)
	{
		for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) dis[i][j]=-0x3f3f3f;
		// for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if(col[i][j]==color) dis[i][j]=1, qx.push(i), qy.push(j);
		qx.push(x), qy.push(y);
		// memset(dis, 0x3f, sizeof(dis));
		dis[x][y]=0;
		while(!qx.empty())
		{
			x=qx.front(), y=qy.front();
			qx.pop(), qy.pop();
			for(int t=0; t<4; ++t) pq(x+dx[t], y+dy[t], dis[x][y]+1);
		}
	}
	inline void gdds(void)
	{
		for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) dds[i][j]=-0x3f3f3f;
		for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if(col[i][j]==color) dds[i][j]=1, qx.push(i), qy.push(j);
		// qx.push(x), qy.push(y);
		// memset(dis, 0x3f, sizeof(dis));
		// dis[x][y]=0;
		while(!qx.empty())
		{
			int x=qx.front(), y=qy.front();
			qx.pop(), qy.pop();
			for(int t=0; t<4; ++t) pq1(x+dx[t], y+dy[t], dds[x][y]+1);
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
	{ ERR("fa %d %d %d %d\n", x, y, dir, cf); fflush(stderr); cout<<x-1<<" "<<y-1<<" "<<dir<<" "<<(cf==-1?force[x][y]-1:cf)<<endl; }

	int cad;
	inline int gval(int x, int y)
	{
		if(ter[x][y]==1) return -0x3f3f3f;
		if(col[x][y]==color) return 0;
		int ret=dis[x][y]*10+cad;
		ret+=tad[x][y];
		if(col[x][y])
		{
			ret+=10000/dds[x][y]/dds[x][y];
			ret+=100000/dis[x][y]/dis[x][y]/dis[x][y]/dis[x][y]/dis[x][y];
			// ret+=tf[x][y]*100/dds[x][y]/dds[x][y];
		}
		if(ter[x][y]==0) ret+=1000;
		else
		{
			ret+=col[x][y]?4000000:2000000;
		}
		if(ter[x][y]==3) ret+=10000000;
		// if(col[x][y]) ret+=200000/dis[x][y]/dis[x][y]/dis[x][y]/dis[x][y];
		// if(pcol[x][y]&&col[x][y]!=color&&dis[x][y]<=5) ret+=10000;
		if(col[x][y]) ret+=100;
		return ret;
	}
	int in[N][N], in1[N][N], cur, sum, sumf, rsum, mx;
	int ccin[N][N], csum, cval, ctim=-100;
	int pin[N][N], pcur, psum, psumf;
	inline bool chkadj(int x, int y)
	{
		for(int d:{0, 1, 2, 3}) if(chkpos(x+dx[d], y+dy[d])&&in[x+dx[d]][y+dy[d]]) return 1;
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
	int isr;
	int is[N][N], rem[N][N], lim=150;
	void getsol(int x, int y)
	{
		isr=0;
		memset(in, 0, sizeof(in));
		memset(in1, 0, sizeof(in1));
		cur=mx=0;
		in[x][y]=1;
		sum=0;
		{
			queue<pair<int, int> > q;
			q.push({x, y});
			in[x][y]=1;
			while(!q.empty())
			{
				auto [x, y]=q.front();
				sum+=tf[x][y]-1;
				q.pop();
				// break;
		// ERR("in %d %d\n", x, y);
		// fflush(stderr);
				for(int d:{0, 1, 2, 3})
				{
					int nx=x+dx[d], ny=y+dy[d];
					if(chkpos(nx, ny)&&col[nx][ny]==color&&!in[nx][ny]) q.push({nx, ny}), in[nx][ny]=1;
				}
			}
		}
		sumf=sum;
		// ERR("%d %d %d %d\n", tf[x][y], x, y, type[x][y]);
		// fflush(stderr);
		static pair<int, int> pos[N*N];
		int top=0;
		for(int ct:{0, 1})
		{
		memcpy(in, in1, sizeof(in1));
		sum=sumf=0;
		for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if(in[i][j])
		{
			sum+=tf[i][j]-1;
			if(col[i][j]==color) sumf+=tf[i][j]-1;
		}
		ld T=100000;
		while(T>1e-6)
		{
			pcur=cur, psum=sum, psumf=sumf;
			memcpy(pin, in, sizeof(in));
			int tot=0;
			top=0;
			for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if(in[i][j]) pos[++top]={i, j}, tot+=(col[i][j]==color);
			int pval=cur/pow(tot+1, 0.2);///sqrt(tot);///(sumf-sum+tot+1);
			int ptot=tot;
			tot=top;
				// ERR("T %Lf\n", T);
				// fflush(stderr);
			if((rnd()%2)&&top)
			{
				// ERR("A\n");
				// fflush(stderr);
				memset(dfn, 0, sizeof(dfn));
				memset(iscut, 0, sizeof(iscut));
				cnt=0;
				tarjan(pos[1].x, pos[1].y, 0, 0);
				int mn=0x3f3f3f3f;
				for(int i=1; i<=top; ++i) if(!iscut[pos[i].x][pos[i].y]&&(!ct||col[pos[i].x][pos[i].y]==color)) mn=std::min(mn, tf[pos[i].x][pos[i].y]-1);
				// ERR("SA\n");
				fflush(stderr);
				if(mn>sum) goto out;
				// ERR("SA %d\n", top);
				// fflush(stderr);
				int t;
				re:;
				// fflush(stderr);
				t=rnd()%top+1;
				int cx=pos[t].x, cy=pos[t].y;
				// ERR("hh %d %d %d %d\n", tf[cx][cy]-1, iscut[cx][cy], mn, sumf);
				// fflush(stderr);
				if((ct&&col[cx][cy]!=color)||!in[cx][cy]||iscut[cx][cy]||tf[cx][cy]-1>sum) goto re;
				// ERR("SAha %d %d %d\n", cx, cy, in[cx][cy]);
				// fflush(stderr);
				in[cx][cy]=0;
				cur-=gval(cx, cy);
				// ERR("ga\n");
				// fflush(stderr);
				sum-=tf[cx][cy]-1;
				if(tf[cx][cy]>0) sumf-=tf[cx][cy]-1;
				if(col[cx][cy]==color) --tot;
			}
			else
			{
				out:;
				// ERR("B\n");
				// fflush(stderr);
				top=0;
				for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if(!in[i][j]&&((!tot&&col[i][j]==color)||chkadj(i, j))&&sum+tf[i][j]-1>=0&&(ct||col[i][j]==color))
					pos[++top]={i, j};
				// ERR("%d %d %d\n", tot, top, sumf);
				// fflush(stderr);
				if(!top) { T*=0.999; continue; }
				int t=rnd()%top+1;
				int cx=pos[t].x, cy=pos[t].y;
				in[cx][cy]=1;
				if(col[cx][cy]==color) ++tot;
				cur+=gval(cx, cy);
				sum+=tf[cx][cy]-1;
				if(tf[cx][cy]>0) sumf+=tf[cx][cy]-1;
				// ERR("%d %d\n", cx, cy);
				// fflush(stderr);
			}
			int val=cur/(pow(tot+1, 0.2));///sqrt(tot);///(sumf-sum+tot*2+1);
			if(tot>lim) val=-0x3f3f;
			// ERR("sum %d %d\n", sumf-sum+tot+1, val);
			// fflush(stderr);
			if(val>mx) memcpy(in1, in, sizeof(in)), mx=val, rsum=sum;
			else if(val<pval&&rnd()%100>=exp((val-pval)*10/T)*100)
			{
				cur=pcur, sum=psum, sumf=psumf, tot=ptot;
				memcpy(in, pin, sizeof(pin));
			}
			T*=0.999;
			// ERR("sum %d\n", sumf-sum+tot+1);
			// fflush(stderr);
		}
		}
		sum=rsum;
			// ERR("sum %d %d %d\n", sumf, sum, mx);
			// fflush(stderr);
		csum=cval=0;
		int csumf=0, ctot=0;
		for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if(ccin[i][j])
		{
			if(col[i][j]==color) ++ctot;
			if(col[i][j]!=color&&ter[i][j]==2) tf[i][j]+=std::min(30, ::T-ctim);
			csum+=tf[i][j]-1;
			if(rem[i][j]<0) cval+=gval(i, j);
			if(tf[i][j]-1>0) csumf+=tf[i][j]-1;
		}
		cval/=(pow(std::max(0, ctot-(::T-ctim)), 0.2)+1);
		// if(csum>=0) cval/=sqrt(ctot);//(csumf-csum+ctot*2+1);
		// ERR("val %d %d\n", csum, cval);
		if(csum>=0&&cval>0&&::T-ctim<=1000&&cval*10>=mx&&ctot<=lim)
		{
			isr=1;
			memcpy(in1, ccin, sizeof(in1));
			sum=csum;
		}
		else
		{
			ctim=::T;
			memcpy(ccin, in1, sizeof(ccin));
			cval=mx;
		}
		memcpy(in, in1, sizeof(in1));
	}
	int dfs(int x, int y)
	{
		in[x][y]=0;
		int ret=rem[x][y];
		for(int d:{0, 1, 2, 3})
		{
			int nx=x+dx[d], ny=y+dy[d];
			if(!chkpos(nx, ny)||!in[nx][ny]) continue;
			int v=dfs(nx, ny);
			if(v<0) is[nx][ny]=1;
			if(v>0) is[x][y]=1;
			ret+=v;
		}
		return ret;
	}
	int rok;
	int dfs1(int x, int y)
	{
		// ERR("cur %d %d %d\n", x, y, is[x][y]);
		fflush(stderr);
		in[x][y]=0;
		int ret=rem[x][y];
		for(int d:{0, 1, 2, 3})
		{
			int nx=x+dx[d], ny=y+dy[d];
			if(!chkpos(nx, ny)||!in[nx][ny]) continue;
			int v=dfs1(nx, ny);
		// ERR("nx %d %d %d\n", nx, ny, v);
		fflush(stderr);
			if(rok) return 0;
			if(v>0)
			{
				if(!is[nx][ny])
				{
					rok=1;
					in[nx][ny]=0;
					move(nx, ny, d^1, v);
					rem[nx][ny]-=v;
					rem[x][y]+=v;
					return 0;
				}
			}
			else if(v<0)
			{
				if(!is[x][y])
				{
		// ERR("haha %d %d %d\n", nx, ny, v);
		// fflush(stderr);
					rok=1;
					in[nx][ny]=0;
					move(x, y, d, -v);
					rem[nx][ny]+=-v;
					rem[x][y]-=-v;
					return 0;
				}
			}
			ret+=v;
		}
		return ret;
	}
	inline void proc(void)
	{
		int rx=0, ry=0;
		for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if(in[i][j]) { rx=i, ry=j; break; }
		if(!rx) { move(0, 0, 0); return; }
		ERR("rx %d %d %d\n", rx ,ry, sum);
		fflush(stderr);
		rok=0;
		if(!isr)
		{
			ERR("clr");
			fflush(stderr);
			memset(rem, 0, sizeof(rem));
			for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if(in[i][j])
			{
				int t=std::max(0, std::min(sum, tf[i][j]-1));
				rem[i][j]=tf[i][j]-1-t, sum-=t;
			}
		}
		memset(is, 0, sizeof(is));
		dfs(rx, ry);
		// ERR("rx %d %d\n", rx ,ry);
		// fflush(stderr);
		memcpy(in, in1, sizeof(in));
		dfs1(rx, ry);
		if(!rok) move(-1, -1, -1);
		ERR("fax %d %d %d\n", rx ,ry, rok);
		fflush(stderr);
	}
	inline void solve(void)
	{
		ERR("RT %d %d\n", T, color);
		rd();
		ERR("T %d %d\n", T, color);
		ERR("%d %d\n", kx, ky);
		fflush(stderr);
		gdis(kx, ky);
		gdds();
		lim=50;
		ERR("plim %d %d %d\n", lim, kx, ky);
		fflush(stderr);
		for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if(col[i][j]!=color&&col[i][j]&&dis[i][j]<=4&&dis[i][j]>=0)
		{
			lim=std::min(lim, dis[i][j]*5);
			if(lim==5*dis[i][j]) ERR("ha %d %d %d\n", i, j, dis[i][j]);
		}
		ERR("lim %d\n", lim);
		fflush(stderr);
		int ad=0x3f3f3f3f;
		cad=0;
		for(int i=1; i<=n; ++i) for(int j=1; j<=n; ++j) if(col[i][j]!=color&&chkvis(i, j)) ad=std::min(ad, -gval(i, j)-tf[i][j]+2);
		ad=std::max(ad, 0);
		cad=ad;
		ERR("ga%d\n", T);
		for(int i=1; i<=n; ++i, ERR("\n")) for(int j=1; j<=n; ++j) ERR("%5d ", std::max(-100, gval(j, i)));
		ERR("\n");
		fflush(stderr);
		// for(int i=1; i<=n; ++i, ERR("\n")) for(int j=1; j<=n; ++j) ERR("%5d ", dis[i][j]);
		// ERR("\n");
		// fflush(stderr);
		// for(int i=1; i<=n; ++i, ERR("\n")) for(int j=1; j<=n; ++j) ERR("%5d ", tf[j][i]);
		// fflush(stderr);
		// ERR("ga%d\n", T);
		// fflush(stderr);
		getsol(kx, ky);
		// for(int i=1; i<=n; ++i, ERR("\n"))
		// {
		// 	ERR("L:%3d ", i);
		// 	for(int j=1; j<=n; ++j) ERR("%3d ", ccin[j][i]);
		// }
		// ERR("\n");
		// fflush(stderr);
		// for(int i=1; i<=n; ++i, ERR("\n")) 
		// {
		// 	ERR("L:%3d ", i);
		// 	for(int j=1; j<=n; ++j) ERR("%3d ", rem[j][i]);
		// }
		// ERR("\n");
		// fflush(stderr);
		// for(int i=1; i<=n; ++i, ERR("\n"))
		// {
		// 	ERR("L:%3d ", i);
		// 	for(int j=1; j<=n; ++j) ERR("%3d ", tf[j][i]);
		// }
		// ERR("\n");
		// fflush(stderr);
		int rtot=0;
		// for(int i=1; i<=n; ++i, ERR("\n")) for(int j=1; j<=n; ++j) ERR("%5d ", (in1[j][i]?-1:1)*(type[j][i]+1)), rtot+=(in1[i][j]&&col[i][j]!=color);
		// ERR("rtot %d\n", rtot);
		// fflush(stderr);
		// move(0, 0, 0);
		// return;
		ERR("ha %d\n", T);
		fflush(stderr);
		proc();
		// memcpy(ccin, in, sizeof(in));
	}
}
int main() {
	freopen("debug.txt", "w", stderr);
	// for(int i=0; i<T::N; ++i) for(int j=0; j<T::N; ++j) T::rev[i][j]=-1;
	// memset(T::terrain, -1, sizeof(T::terrain));
	cin>>color>>Game::n;
	for(;true;) Game::solve();
}