#include "StdAfx.h"
#include "SqlAceCli.h"
#include "TKeyPoints.h"


DBKeyPoints::DBKeyPoints(void)
{
}


DBKeyPoints::~DBKeyPoints(void)
{
}

VEC_KEYPOINT DBKeyPoints::GetKeyPointsTable(vector<int> nTypes)
{
	VEC_KEYPOINT keyPTList;

	HRESULT hr;
	CTableKeyPoints table;

	hr = table.OpenDataSource();
	if (FAILED(hr))
	{
		return keyPTList;
	}


	/*hr = table.OpenAll();
	if (FAILED(hr))
	{
		CoUninitialize();
		return keyPTList;
	}*/
	CString strSQL;
	if (nTypes.size() > 0)
	{
		strSQL.Format(L"Select * from dbo.KeyPoints where "); //Type = '%d'", 
		CString strTmp = L"";
		int nLoop = 0;
		for (auto it = nTypes.cbegin(); it != nTypes.cend(); ++it)
		{
			int nType = *it;
			strTmp.Format(L"(Type = '%d') OR ", nType);
			strSQL += strTmp;
		}
		strSQL = strSQL.Left(strSQL.GetLength() - 4);
	}
	else
	{
		strSQL.Format(L"Select * from dbo.KeyPoints");
	}
	
	hr = table.Open(table.m_session, strSQL);
	if (FAILED(hr))
	{
		return keyPTList;
	}

	while(table.MoveNext() != DB_S_ENDOFROWSET)
	{
		KeyPointItem item;
		item.uPosition = table.m_Position;
		item.uType = table.m_Type;
		item.uSpeedRate = table.m_SpeedRate;
		item.uTeachMode = table.m_TeachMode;
		item.uOHT_ID = table.m_OHT_ID;
		item.uLane_ID = table.m_Lane_ID;
		item.uPrev = table.m_Prev;
		item.uNext = table.m_Next;
		item.strName = table.m_Name;
		keyPTList.push_back(item);
	}
	table.CloseAll();

	return keyPTList;
}

int DBKeyPoints::SetKeyPointbyOHTTeach(int nOHT_ID, int nPOS, int nType, int nSpeedRate)
{
	HRESULT hr;
	int nFoup = 0;

	CTableKeyPoints table;
	hr = table.OpenDataSource();
	if (FAILED(hr))
	{
		cout << "Open KeyPoints Failed." << endl;
		return -1;
	}
	CDBPropSet propset(DBPROPSET_ROWSET);
	table.GetRowsetProperties(&propset);

	CString strSQL = L"";
	strSQL.Format(L"Select * from dbo.KeyPoints where (Position = %d)", nPOS);
	hr = table.Open(table.m_session, strSQL, &propset);
	if (FAILED(hr))
	{
		cout << "Open KeyPoints Failed." << endl;
		return -1;
	}

	if(table.MoveFirst() != DB_S_ENDOFROWSET)
	{
		// update recoard
		table.m_dwIdStatus = DBSTATUS_S_IGNORE;
		table.m_Type = nType;
		table.m_SpeedRate = nSpeedRate;
		table.m_TeachMode = 1;
		table.m_OHT_ID = nOHT_ID;

		//table.m_dwPositionStatus = DBSTATUS_S_IGNORE;
		hr = table.SetData();
		if (FAILED(hr))
		{
			LOG_ERROR("update keypoints table failed.");
		}
		table.CloseAll();
		return -1;
	}
	table.CloseAll();

	hr = table.OpenAll();
	if (FAILED(hr))
	{
		return -1;
	}

	//Position, \
	//	Type, \
	//	SpeedRate, \
	//	TeachMode, \
	//	OHT_ID, \
	//	Rail_ID, \
	//	Prev, \
	//	Next \
	////// insert record
	table.m_Position = nPOS;
	table.m_Type = nType;
	table.m_SpeedRate = nSpeedRate;
	table.m_TeachMode = 1;
	table.m_OHT_ID = nOHT_ID;
	table.m_Lane_ID = 0;
	table.m_Prev = 0;
	table.m_Next = 0;

	table.m_dwIdStatus = DBSTATUS_S_IGNORE;
	table.m_dwPositionStatus = DBSTATUS_S_OK;
	table.m_dwTypeStatus = DBSTATUS_S_OK;
	table.m_dwSpeedRateStatus = DBSTATUS_S_OK;
	table.m_dwTeachModeStatus = DBSTATUS_S_OK;
	table.m_dwOHT_IDStatus = DBSTATUS_S_OK;
	table.m_dwLane_IDStatus = DBSTATUS_S_OK;
	table.m_dwPrevStatus = DBSTATUS_S_OK;
	table.m_dwNextStatus = DBSTATUS_S_OK;
	table.m_dwNameStatus = DBSTATUS_S_IGNORE;
	table.m_dwrefXStatus = DBSTATUS_S_IGNORE;
	table.m_dwrefYStatus = DBSTATUS_S_IGNORE;

	hr = table.Insert();

	int nRet = 0;
	if (FAILED(hr))
	{
		LOG_ERROR("Insert keypoints table failed.");
		nRet = -1;
	}
	else
	{
		table.UpdateAll();
	}
	table.CloseAll();

	return nRet;
}
