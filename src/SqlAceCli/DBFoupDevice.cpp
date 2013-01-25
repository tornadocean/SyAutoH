#include "StdAfx.h"
#include "SqlAceCli.h"
#include "TFoupDevice.h"

DBFoupDevice::DBFoupDevice(void)
{
}


DBFoupDevice::~DBFoupDevice(void)
{
}

ItemFoupDevie DBFoupDevice::GetItem(int nType, int nDevID, int nPort)
{
	ItemFoupDevie item = {0};

	HRESULT hr;
	CTableFoupDevice table;

	hr = table.OpenDataSource();
	if (FAILED(hr))
	{
		return item;
	}

	CString strSQL = _T("");
	strSQL.Format(_T("Select * from dbo.FoupDevice WHERE")
		_T(" DevType = %d and DevID = %d and DevPortID = %d"),
		nType, nDevID, nPort);

	hr = table.Open(table.m_session, strSQL);
	if (FAILED(hr))
	{
		return item;
	}

	while(table.MoveNext() != DB_S_ENDOFROWSET)
	{
		item.nID = table.m_Id;
		item.nDevID = table.m_DevID;
		item.nDevType = table.m_DevType;
		item.nDevPortID = table.m_DevPortID;
		item.nDevPortKP = table.m_DevPortKP;
	}
	table.CloseAll();

	return item;
}