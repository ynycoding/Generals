#include <cstdio>
#include <queue>
#include <cmath>
#include <cstring>
#include <cstdlib>
#include <iostream>
#include <algorithm>
using std::cin;
using std::cout;
using std::endl;
using std::queue;
int color, n;
int dx[]={0,0,-1,1};
int dy[]={-1,1,0,0};
namespace T{
	int mp[3][55][55], (*col)[55]=mp[0], (*force)[55]=mp[1], (*type)[55]=mp[2], cx, cy, c, kx, ky;
	int cnt[9], cnt1[9], ene[55][55], dds[55][55], tower[55][55], terrain[55][55], allied[55][55];
	inline void rd(void)
	{
		for(int k=0;k<3;k++) {
			for(int i=0;i<n;i++) {
				for(int j=0;j<n;j++) {
					cin>>mp[k][i][j];
				}
			}
		}
		for(int i=0; i<n; ++i) for(int j=0; j<n; ++j) cnt[col[i][j]]+=force[i][j];
		for(int i=0; i<n; ++i) for(int j=0; j<n; ++j) if(type[i][j]!=-1&&type[i][j]!=1)
		++cnt1[col[i][j]], terrain[i][j]=1;
		for(int i=0; i<n; ++i) for(int j=0; j<n; ++j) if(col[i][j]==color&&type[i][j]==3) kx=i, ky=j;
		for(int i=0; i<n; ++i) for(int j=0; j<n; ++j) tower[i][j]|=(type[i][j]==2);
		for(int i=0; i<n; ++i) for(int j=0; j<n; ++j) if(type[i][j]==2||type[i][j]==3)
		{
			tower[i][j]=type[i][j]==3?100000:10;
		}
		for(int i=0; i<n; ++i) for(int j=0; j<n; ++j) tower[i][j]|=(type[i][j]==0);
	}
	queue<int> qx, qy;
//	inline int dis(int a, int b) { return abs(a-kx)+abs(b-ky); }
	inline void pq(int x, int y, int d)
	{ if(x<0||y<0||x>=n||y>=n||dds[x][y]<=d||terrain[x][y]==-1) return; dds[x][y]=d, qx.push(x), qy.push(y); }
	inline void gdis(int x, int y)
	{
		qx.push(x), qy.push(y);
		memset(dds, 0x3f, sizeof(dds));
		dds[x][y]=1;
		while(!qx.empty())
		{
			x=qx.front(), y=qy.front();
			qx.pop(), qy.pop();
			for(int t=0; t<4; ++t) pq(x+dx[t], y+dy[t], dds[x][y]+1);
		}
	}
	inline void debug(void)
	{
		FILE *p=fopen("debug.txt", "w");
		for(int i=0; i<n; ++i, fprintf(p, "\n")) for(int j=0; j<n; ++j)
		fprintf(p, "%d ", allied[i][j]);
		fclose(p);
	}
	inline void addene(int x, int y, int cf)
	{
		gdis(x, y);
		for(int i=0; i<n; ++i) for(int j=0; j<n; ++j)
		ene[i][j]+=1.0*(cf+50)/(dds[i][j])*(1000.0/dds[kx][ky]/dds[kx][ky]);
	}
	inline void addallied(int x, int y, int cf)
	{
		if(!cf) return;
		gdis(x, y);
		for(int i=0; i<n; ++i) for(int j=0; j<n; ++j)
		allied[i][j]+=10.0*(cf-1)/(dds[i][j]*dds[i][j]);
	}
	inline void addtower(int x, int y, int val)
	{
		gdis(x, y);
		for(int i=0; i<n; ++i) for(int j=0; j<n; ++j) allied[i][j]+=1.0*val/(1.0*dds[i][j]*dds[i][j]);
	}
//	inline int accept(int a, int b) { return a>b; }
//	inline int accept(int a, int b) { return a+rand()%(cnt[color]/100+1)>b+rand()%(cnt[color]/100+1); }
	inline void findmax(void)
	{
		cx=cy=c=-1;
		int cur;
		for(int i=0; i<n; ++i) for(int j=0; j<n; ++j)
		if(col[i][j]==color&&(cur=allied[i][j])>c) cx=i, cy=j, c=cur;
//		if(col[i][j]==color&&(cur=(force[i][j]+ene[i][j]/300))>c) cx=i, cy=j, c=cur;
	}
	inline void move(int x, int y, int dir)
	{ cout<<x<<" "<<y<<" "<<dir<<" "<<force[x][y]-1<<endl; }
	inline void procene(void)
	{
		memset(ene, 0, sizeof(ene));
		for(int i=0; i<n; ++i) for(int j=0; j<n; ++j) if(col[i][j]!=color&&col[i][j]) addene(i, j, force[i][j]);
	}
	inline void proctow(void)
	{
		for(int i=0; i<n; ++i) for(int j=0; j<n; ++j) if(col[i][j]!=color&&tower[i][j]&&force[i][j]<force[cx][cy]-2)
		addtower(i, j, tower[i][j]);
	}
	inline void procallied(void)
	{
		memset(allied, 0, sizeof(allied));
		for(int i=0; i<n; ++i) for(int j=0; j<n; ++j) if(col[i][j]==color)
		addallied(i, j, force[i][j]);
	}
	inline int gval(int x, int y, int cf)
	{
		if(x<0||y<0||x>=n||y>=n||type[x][y]==-1||type[x][y]==1) return -0x3f3f3f3f;
		int ret=0;
		if(col[x][y]!=color)
		{
//			ret=10000/(dds[x][y]*dds[x][y]);
//			ret+=1.0*cnt1[color]/cnt1[col[x][y]]*(col[x][y]?100:200);
			ret+=(col[x][y]?200:350);
			if(type[x][y]==2)
			{
				if(!col[x][y]&&cf<=force[x][y]) return -0x3f3f3f3f;
				ret+=cnt[color]/n+200;
			}
			if(type[x][y]==3) return 0x3f3f3f3f;
			if(cf<=force[x][y]) return -0x3f3f3f3f;
			ret+=cf-force[x][y];
//			ret*=2;
		}
		else ret+=force[x][y];
		ret+=ene[x][y]+allied[x][y];
		return ret;
	}
	inline int gnx(int x, int y)
	{
		int d=0, c=-100, cf=force[x][y];
		for(int i=0; i<4; ++i) if(gval(x+dx[i], y+dy[i], cf)>c) c=gval(x+dx[i], y+dy[i], cf), d=i;
		return d;
	}
	inline void solve(void)
	{
		rd();
		procene();
		procallied();
		proctow();
		findmax();
//		debug();
		gdis(kx,  ky);
		int d=gnx(cx, cy);
		move(cx, cy, d);
	}
}
int main() {
	memset(T::terrain, -1, sizeof(T::terrain));
	cin>>color>>n;
	for(;true;) T::solve();
}
