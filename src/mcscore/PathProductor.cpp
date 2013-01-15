#include "StdAfx.h"
#include "PathProductor.h"

initialiseSingleton(CPathProductor);
CPathProductor::CPathProductor(void)
{
	sLog.outBasic("Path Productor is Started.");
}


CPathProductor::~CPathProductor(void)
{
}


void CPathProductor::GetLaneData(void)
{
	DBLane dbLane;
	auto vecLane = dbLane.GetLaneTable(1);
	set<int> setPoint;

	for (auto it = vecLane.cbegin();
		it != vecLane.cend(); ++it)
	{
		printf("Lane: %d, s: %d e: %d, p: %d n: %d, f: %d, t: %d, l: %d\r\n",
			it->nID, it->nStart, it->nEnd, it->nPrevLane, it->nNextLane, it->nNextFork, it->nType, 
			it->nLength);
		setPoint.insert(it->nStart);
		setPoint.insert(it->nEnd);
		mapLane[it->nID] = *it;
	}

	m_arrayBarCode.clear();
	m_arrayLocation.clear();

	int nIndex = 0;
	for(auto it = setPoint.cbegin();
		it != setPoint.cend(); ++it)
	{
		int nBarCode = *it;
		m_arrayBarCode.push_back(nBarCode);
		location loc;
		loc.x = nBarCode;
		loc.y = 10;
		m_arrayLocation.push_back(loc);
		mapPoint[nBarCode] = nIndex;
		nIndex++;
	}
	
	m_arrayEdge.clear();
	m_arrayWeights.clear();
	int nLoopNodeStart = -1;
	int nLoopNodeEnd = -1;
	for (auto it = vecLane.cbegin();
		it != vecLane.cend(); ++it)
	{
		int nIndexStart = 0;
		int nIndexEnd = 0;

		auto ms = mapPoint.find(it->nStart);
		auto me = mapPoint.find(it->nEnd);
		if (ms != mapPoint.cend() && me != mapPoint.cend())
		{
			nIndexStart = ms->second;
			nIndexEnd = me->second;
			m_arrayEdge.push_back(lane_edge(nIndexStart, nIndexEnd));
			m_arrayWeights.push_back(it->nLength);
			// todo: find nLoopNodeStart, nLoopNodeEnd
			auto itNextLane = mapLane.find(it->nNextLane);
			if ( itNextLane != mapLane.cend())
			{
				// the end point of the last lane don't equal the start point of the first lane.
				// but the two points will be the same point.
				if (it->nEnd != itNextLane->second.nStart) 
				{
					nLoopNodeStart = nIndexEnd;
				}
				// the first lane have not prev lane, the prevlane id is -1
				else if(it->nPrevLane == -1) 
				{
					nLoopNodeEnd = nIndexStart;
				}
			}
		}
	}

	// add virtual lane to complete the rail to a loop
	if (nLoopNodeStart >= 0 && nLoopNodeEnd >= 0)
	{
		m_arrayEdge.push_back(lane_edge(nLoopNodeStart, nLoopNodeEnd));
		m_arrayWeights.push_back(0);
	}
}


void CPathProductor::InitGraph(void)
{

}

vec_int CPathProductor::ProductPath(int nFrom, int nTo)
{ 
	m_nFrom = nFrom;
	m_nTo = nTo;
	
	InitGraph();
	
	if (AStarFind() > 0)
	{
		return m_arrayPath;
	}
	return vec_int();
}


int CPathProductor::AStarFind(void)
{
	m_arrayPath.clear();

	size_t num_edges =m_arrayEdge.size();

	if (num_edges <= 0)
	{
		return 0;
	}

	size_t szCount = m_arrayBarCode.size() + 1;
	
	// create graph
	mygraph_t g(szCount);
	WeightMap weightmap = get(edge_weight, g);
	for(std::size_t j = 0; j < num_edges; ++j)
	{
		edge_descriptor e; 
		bool inserted;
		boost::tie(e, inserted) = add_edge(m_arrayEdge[j].first,
			m_arrayEdge[j].second, g);
		weightmap[e] = m_arrayWeights[j];
	}

	lane_vertex start = -1;
	lane_vertex goal = -1;
	// from will be the first element
	int nIndexFrom = 0;
	for (auto it = m_arrayBarCode.cbegin();
		it != m_arrayBarCode.cend(); ++it, ++nIndexFrom)
	{
		if (*it == m_nFrom)
		{
			start = nIndexFrom;
			break;
		}
	}

	// to will be the last element
	int nIndexTo = m_arrayBarCode.size();
	for (auto it = m_arrayBarCode.crbegin();
		it != m_arrayBarCode.crend(); ++it, --nIndexTo)
	{
		if (*it == m_nTo)
		{
			--nIndexTo;
			goal = nIndexTo;
			break;
		}
	}

	if (start < 0 || goal < 0)
	{
		cout<< "Can not find Vetex for path." << endl;
		return 0;
	}


	//cout << "Start vertex: " << m_arrayBarCode[start] << endl;
	//cout << "Goal vertex: " << m_arrayBarCode[goal] << endl;

	vector<mygraph_t::vertex_descriptor> p(num_vertices(g));
	vector<cost> d(num_vertices(g));
	try 
	{
		// call astar named parameter interface
		astar_search
			(g, start,
			distance_heuristic<mygraph_t, cost, vec_location >
			(m_arrayLocation, goal),
			predecessor_map(&p[0]).distance_map(&d[0]).
			visitor(astar_goal_visitor<lane_vertex>(goal)));
	} 
	catch(found_goal /*fg*/) 
	{ 
		// found a path to the goal
		list<lane_vertex> shortest_path;
		for(auto v = goal;; v = p[v]) 
		{
			shortest_path.push_front(v);
			if(p[v] == v)
				break;
		}

		for(auto spi = shortest_path.begin(); spi != shortest_path.end(); ++spi)
		{
			int nBarCode = m_arrayBarCode[*spi];
			m_arrayPath.push_back(nBarCode);
		}
		
		int nTotalTravel = (int)d[goal];
		cout << endl << "Total travel time: " << nTotalTravel << endl;
		return nTotalTravel;
	}


	return 0;
}
