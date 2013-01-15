#pragma once
#include "../shared/Singleton.h"
#pragma comment(lib, "shared.lib")

#include "../SqlAceCli/SqlAceCli.h"

#include <boost/graph/astar_search.hpp>
#include <boost/graph/adjacency_list.hpp>

#include <vector>
#include <list>
#include <math.h>    // for sqrt
#include <set>
using namespace boost;
using namespace std;

// auxiliary types
struct location
{
	float y, x; // lat, long
};
typedef float cost;

// euclidean distance heuristic
template <class Graph, class CostType, class LocMap>
class distance_heuristic : public astar_heuristic<Graph, CostType>
{
public:
	typedef typename graph_traits<Graph>::vertex_descriptor Vertex;
	distance_heuristic(LocMap l, Vertex goal)
		: m_location(l), m_goal(goal) 
	{
		//m_location = l;
	}
	CostType operator()(Vertex u)
	{
		CostType dx = m_location[m_goal].x - m_location[u].x;
		CostType dy = m_location[m_goal].y - m_location[u].y;
		return ::sqrt(dx * dx + dy * dy);
	}
private:
	LocMap m_location;
	Vertex m_goal;
};


struct found_goal {}; // exception for termination

// visitor that terminates when we find the goal
template <class Vertex>
class astar_goal_visitor : public boost::default_astar_visitor
{
public:
	astar_goal_visitor(Vertex goal) : m_goal(goal) {}
	template <class Graph>
	void examine_vertex(Vertex u, Graph& g) {
		if(u == m_goal)
			throw found_goal();
	}
private:
	Vertex m_goal;
};

//
// specify some types
typedef adjacency_list<listS, vecS, directedS, no_property,
	property<edge_weight_t, cost> > mygraph_t;
typedef property_map<mygraph_t, edge_weight_t>::type WeightMap;
typedef mygraph_t::vertex_descriptor lane_vertex;
typedef mygraph_t::edge_descriptor edge_descriptor;
typedef mygraph_t::vertex_iterator vertex_iterator;
typedef std::pair<int, int> lane_edge;

typedef vector<int> vec_int;
typedef vector<location> vec_location;
typedef vector<lane_edge> vec_edge;
typedef vector<cost> vec_cost;


class CPathProductor;
class CPathProductor  : public Singleton<CPathProductor>
{
public:
	CPathProductor(void);
	~CPathProductor(void);

private:
	VEC_LANE m_vecLane;
	map<int, ItemLane> mapLane;
	map<int, int> mapPoint;

	vec_int m_arrayBarCode;
	vec_location m_arrayLocation;
	vec_edge m_arrayEdge;
	vec_cost m_arrayWeights;
	vec_int m_arrayPath;

	int m_nFrom;
	int m_nTo;

public:
	void GetLaneData(void);
	vec_int ProductPath(int nFrom, int nTo);
	
protected:
	void InitGraph(void);
	int AStarFind(void);

};

#define sPathProductor CPathProductor::getSingleton()