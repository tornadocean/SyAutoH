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
	CoInitialize(NULL);
	HRESULT hr;

	CTableTransfer table;
	hr = table.OpenDataSource();
	if (FAILED(hr))
	{
		cout << "Open Transfer Failed." << endl;
		CoUninitialize();
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
		CoUninitialize();
		return -1;
	}

	if(table.MoveFirst() != DB_S_ENDOFROWSET)
	{
		table.CloseAll();
		CoUninitialize();
		return -1;
	}

	table.CloseAll();

	hr = table.OpenAll();
	if (FAILED(hr))
	{
		CoUninitialize();
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
	CoUninitialize();

	return nRet;
}

VEC_TRANS DBTransfer::GetTransferAll()
{
	VEC_TRANS transList;

	CoInitialize(NULL);
	HRESULT hr;

	CTableTransfer table;
	hr = table.OpenAll();
	if (FAILED(hr))
	{
		CoUninitialize();
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
	CoUninitialize();
	return transList;
}

VEC_TRANS DBTransfer::GetTransferNoFinished(void)
{
	VEC_TRANS transList;

	CoInitialize(NULL);
	HRESULT hr;

	CTableTransfer table;
	hr = table.OpenDataSource();
	if (FAILED(hr))
	{
		cout << "Open Transfer Failed." << endl;
		CoUninitialize();
		return transList;
	}

	CString strFind = L"";
	strFind.Format(L"SELECT * From Transfer where Status != 'Finished'");
	hr = table.Open(table.m_session, strFind);
	if (FAILED(hr))
	{
		cout << "Open Transfer Failed." << endl;
		CoUninitialize();
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
	CoUninitialize();

	return transList;
}
