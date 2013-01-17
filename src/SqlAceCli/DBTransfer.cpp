#include "StdAfx.h"
#include "SqlAceCli.h"
#include "TTransfer.h"

DBTransfer::DBTransfer(void)
{
}


DBTransfer::~DBTransfer(void)
{
}

int DBTransfer::AddTransfer(int nFoupID, int nTarget)
{
	HRESULT hr;

	CTableTransfer table;
	hr = table.OpenDataSource();
	if (FAILED(hr))
	{
		cout << "Open Transfer Failed." << endl;
		return -1;
	}
	CDBPropSet propset(DBPROPSET_ROWSET);
	table.GetRowsetProperties(&propset);

	CString strSQL = L"";
	strSQL.Format(L"Select * from dbo.Transfer where (FoupID = %d) AND (Destination = %d)", nFoupID, nTarget);
	hr = table.Open(table.m_session, strSQL, &propset);
	if (FAILED(hr))
	{
		cout << "Open KeyPoints Failed." << endl;
		return -1;
	}

	if(table.MoveFirst() != DB_S_ENDOFROWSET)
	{
		table.CloseAll();
		return -1;
	}

	table.CloseAll();

	hr = table.OpenAll();
	if (FAILED(hr))
	{
		return -1;
	}
	//////////////////////////////////////////////////////////////////////////
	// insert record
	table.m_FoupID = nFoupID;
	table.m_Destination = nTarget;
	table.m_Source = -1;
	table.m_Priority = 100;
	//table.m_Status
	CString strStatus = _T("Add");
	wcscpy_s(table.m_Status, strStatus);
	table.m_dwStatusLength = strStatus.GetLength()*2;

	table.m_dwidStatus = DBSTATUS_S_IGNORE;

	table.m_dwStatusStatus = DBSTATUS_S_OK;
	table.m_dwFoupIDStatus = DBSTATUS_S_OK;
	table.m_dwSourceStatus = DBSTATUS_S_OK;
	table.m_dwDestinationStatus = DBSTATUS_S_OK;
	table.m_dwPriorityStatus = DBSTATUS_S_OK;

	hr = table.Insert();

	int nRet = 0;
	if (FAILED(hr))
	{
		nRet = -1;
	}
	else
	{
		table.UpdateAll();
	}
	table.CloseAll();

	return nRet;
}

VEC_TRANS DBTransfer::GetTransferAll()
{
	VEC_TRANS transList;
	transList = GetTransfer("");
	
	return transList;
}

VEC_TRANS DBTransfer::GetTransfer(const std::string& strStatus, bool bInclude /* = true */)
{
	VEC_TRANS transList;

	HRESULT hr;

	CTableTransfer table;
	hr = table.OpenDataSource();
	if (FAILED(hr))
	{
		cout << "Open Transfer Failed." << endl;
		return transList;
	}

	CString strFind = L"";
	CString cstrStatus = L"";
	cstrStatus = strStatus.c_str();
	if (strStatus.size() < 0)
	{
		strFind.Format(L"SELECT * From Transfer");
	}
	else
	{
		if (true == bInclude)
		{
			strFind.Format(L"SELECT * From Transfer where Status = '%s'", cstrStatus);
		}
		else
		{
			strFind.Format(L"SELECT * From Transfer where Status != '%s'", cstrStatus);
		}
		
	}
	
	hr = table.Open(table.m_session, strFind);
	if (FAILED(hr))
	{
		cout << "Open Transfer Failed." << endl;
		return transList;
	}

	while(table.MoveNext() != DB_S_ENDOFROWSET)
	{
		ItemTrans iTrans;
		iTrans.nID = table.m_id;
		iTrans.nFoupID = table.m_FoupID;
		iTrans.nTarget = table.m_Destination;
		transList.push_back(iTrans);
	}

	table.CloseAll();

	return transList;
}

VEC_TRANS DBTransfer::GetTransferNoFinished(void)
{
	VEC_TRANS transList;
	transList = GetTransfer("Finished", false);

	return transList;
}


int DBTransfer::SetTransferStatus(int nID, const std::string& sStatus)
{
	HRESULT hr;

	CTableTransfer table;
	hr = table.OpenDataSource();
	if (FAILED(hr))
	{
		cout << "Open Transfer Failed." << endl;
		return -1;
	}
	CDBPropSet propset(DBPROPSET_ROWSET);
	table.GetRowsetProperties(&propset);

	CString strSQL = L"";
	strSQL.Format(L"Select * from dbo.Transfer where id = %d", nID);
	hr = table.Open(table.m_session, strSQL, &propset);
	if (FAILED(hr))
	{
		cout << "Open Transfer Failed." << endl;
		return -1;
	}

	if(table.MoveFirst() != DB_S_ENDOFROWSET)
	{
		table.m_dwidStatus = DBSTATUS_S_IGNORE;
		CString strStatus = L"";
		strStatus = sStatus.c_str();
		wcscpy_s(table.m_Status, strStatus);
		table.m_dwStatusLength = strStatus.GetLength()*2;
		hr = table.SetData();
	}

	int nRet = 0;
	if (FAILED(hr))
	{
		nRet = -1;
	}
	else
	{
		table.UpdateAll();
	}
	table.CloseAll();

	return nRet;
}