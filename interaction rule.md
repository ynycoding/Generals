# generals交互方式

最开始两个数字表示你的颜色和地图大小

对于每一轮：

- 你应该读入 $3$ 个大小 $n\times n$ 的矩阵，分别为目前所属颜色，该点兵力，节点格式
- 对于节点格式：`-1` 是不可视，`0` 是空地，`1` 是山， `2` 是塔，`3` 是王塔
- 你应该输出 $4$ 个整数，分别为 `x y d num` ，`x y` 为要移动的点的坐标$x,y\in [0,20)$，`d` 是方向 $d\in [0,3]$,`num` 是你要移动的兵力
- 对于方向：`0` 为向上，`1` 为向下，`2` 为向左，`3` 为向右。

### Simple Example

```cpp
#include<bits/stdc++.h>
using namespace std;
int n, color;
int dx[]={0,0,-1,1};
int dy[]={-1,1,0,0};
inline void work() {
	static int mp[55][55][3];
	for(int k=0;k<3;k++) {
		for(int i=0;i<n;i++) {
			for(int j=0;j<n;j++) {
				cin>>mp[i][j][k];
			}
		}
	}
	vector<pair<int,int> >mynodeset;
	for(int i=0;i<n;i++) {
		for(int j=0;j<n;j++) {
			if(mp[i][j][0]==color) mynodeset.emplace_back(i,j);
		}
	}int rak=rand()%mynodeset.size();
	static int ps[4];
	for(int i=0;i<4;i++) ps[i]=i;
	random_shuffle(ps,ps+4);
	for(int i=0;i<4;i++) {
		int nx=mynodeset[rak].first+dx[ps[i]],ny=mynodeset[rak].second+dy[ps[i]];
		if(nx<0||ny<0||nx>=n||ny>=n||mp[nx][ny][2]==1) continue ;
		if(mp[mynodeset[rak].first][mynodeset[rak].second][1]>1) cout<<mynodeset[rak].first<<' '<<mynodeset[rak].second<<' '<<ps[i]<<' '<<(rand()%(mp[mynodeset[rak].first][mynodeset[rak].second][1]-1)+1)<<endl;
		else cout<<mynodeset[rak].first<<' '<<mynodeset[rak].second<<' '<<ps[i]<<' '<<0<<endl;
		return ;
	}
}
int main() {
	cin>>color>>n;
	for(;true;) work();
}
```

