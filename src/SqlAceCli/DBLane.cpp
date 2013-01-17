#include "StdAfx.h"
#include "SqlAceCli.h"
#include "TLane.h"

DBLane::DBLane(void)
{
}


DBLane::~DBLane(void)
{
}

VEC_LANE DBLane::GetLaneTable(int nMapID)
{
	VEC_LANE listLane;

	HRESULT hr;
	CTableLane table;
	hr = table.OpenAll();
	if (FAILED(hr))
	{
		return listLane;
	}

	while(table.MoveNext() != DB_S_ENDOFROWSET)
	{
		ItemLane item;
		item.nID = table.m_id;
		item.nStart = table.m_Start;
		item.nEnd = table.m_Finish;
		item.nPrevLane = table.m_Prev;
		item.nNextLane = table.m_Next;
		item.nNextFork = table.m_Next_Frok;
		item.nLength = table.m_Length;
		item.nType = table.m_Type;
		item.bEnable = (table.m_Enable != TRUE);

		listLane.push_back(item);
	}
	table.CloseAll();

	return listLane;
}