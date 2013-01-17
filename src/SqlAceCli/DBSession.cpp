#include "StdAfx.h"
#include "SqlAceCli.h"
#include "TLinkSession.h"

DBSession::DBSession(void)
{
	m_pmapRole = new Map_Int();
}


DBSession::~DBSession(void)
{
	if (NULL != m_pmapRole)
	{
		delete m_pmapRole;
		m_pmapRole = NULL;
	}
}

int DBSession::GetLoginSession(int nUserID, int nRight, 
	const string& strConnection, bool bLastLimit)
{
	int nSession = 0;

	HRESULT hr;
	CTableLinkSession linkSession;
	hr = linkSession.OpenAll();
	if (FAILED(hr))
	{
		return -1;
	}

	linkSession.m_UserID = nUserID;
	linkSession.m_OrginRight = nRight;
	linkSession.m_RealRight = nRight;
	CString strCon;
	strCon = strConnection.c_str();
	wcscpy_s(linkSession.m_ConnectInfo, strCon);
	linkSession.m_dwConnectInfoLength = strCon.GetLength()*2;
	
	COleDateTime t = COleDateTime::GetCurrentTime();
	DBTIMESTAMP ts;
	t.GetAsDBTIMESTAMP( ts ); // retrieves the time in t into the ts structure

	linkSession.m_AccessTime = ts;
	linkSession.m_UserStatus = 1;

	linkSession.m_dwSessionIDStatus = DBSTATUS_S_IGNORE;
	linkSession.m_dwUserIDStatus = DBSTATUS_S_OK;
	linkSession.m_dwOrginRightStatus = DBSTATUS_S_OK;
	linkSession.m_dwRealRightStatus = DBSTATUS_S_OK;
	linkSession.m_dwConnectInfoStatus = DBSTATUS_S_OK;
	linkSession.m_dwAccessTimeStatus = DBSTATUS_S_OK;
	linkSession.m_dwUserStatusStatus = DBSTATUS_S_OK;
	hr = linkSession.Insert();
	if (FAILED(hr))
	{
		linkSession.CloseAll();
		return -1;
	}
	linkSession.UpdateAll();
	if (linkSession.MoveLast() != DB_S_ENDOFROWSET)
	{
		nSession = linkSession.m_SessionID;
	}
	linkSession.CloseAll();

	return nSession;
}

int DBSession::LoginOut(int nSession)
{
	int nRetrun = -1;
	HRESULT hr;
	CTableLinkSession linkSession;
	hr = linkSession.OpenDataSource();
	if (FAILED(hr))
	{
		return nRetrun;
	}
	CDBPropSet propset(DBPROPSET_ROWSET);
	linkSession.GetRowsetProperties(&propset);

	CString strSQL;
	strSQL.Format(L"Select * from dbo.LinkSession where (SessionID = %d)", 
		nSession);
	hr = linkSession.Open(linkSession.m_session, strSQL, &propset);
	if (FAILED(hr))
	{
		return nRetrun;
	}

	if (linkSession.MoveFirst() != DB_S_ENDOFROWSET)
	{
		//linkSession.m_dwSessionIDStatus = DBSTATUS_S_IGNORE;
		//linkSession.m_UserStatus = 2;
		//hr = linkSession.SetData();
		hr = linkSession.Delete();
		if (!FAILED(hr))
		{
			m_pmapRole->erase(nSession);
		}
	}
	linkSession.CloseAll();

	return nRetrun;
}

int DBSession::SetRealRight(int nSession, int nRealRight)
{
	int nRetrun = -1;
	HRESULT hr;
	CTableLinkSession linkSession;
	hr = linkSession.OpenDataSource();
	if (FAILED(hr))
	{
		return nRetrun;
	}
	CDBPropSet propset(DBPROPSET_ROWSET);
	linkSession.GetRowsetProperties(&propset);

	CString strSQL;
	strSQL.Format(L"Select * from dbo.LinkSession where (SessionID = %d)", 
		nSession);
	hr = linkSession.Open(linkSession.m_session, strSQL, &propset);
	if (FAILED(hr))
	{
		return nRetrun;
	}

	if (linkSession.MoveFirst() != DB_S_ENDOFROWSET)
	{
		linkSession.m_dwSessionIDStatus = DBSTATUS_S_IGNORE;
		linkSession.m_RealRight = nRealRight;
		hr = linkSession.SetData();
		if (!FAILED(hr))
		{
			(*m_pmapRole)[nSession] = nRealRight;
		}
	}
	linkSession.CloseAll();

	return nRetrun;
}

int DBSession::GetRealRight(int nSession)
{
	int nRight = -1;
	Map_Int::iterator it = m_pmapRole->find(nSession);
	if (it != m_pmapRole->end())
	{
		nRight = it->second;
		return nRight;
	}
	else
	{
		HRESULT hr;
		CTableLinkSession linkSession;
		hr = linkSession.OpenDataSource();
		if (FAILED(hr))
		{
			return nRight;
		}

		CString strSQL;
		strSQL.Format(L"Select * from dbo.LinkSession where (SessionID = %d)", 
			nSession);
		hr = linkSession.Open(linkSession.m_session, strSQL);
		if (FAILED(hr))
		{
			return nRight;
		}
	
		if (linkSession.MoveFirst() != DB_S_ENDOFROWSET)
		{
			nRight = linkSession.m_RealRight;
		}
		linkSession.CloseAll();
		(*m_pmapRole)[nSession] = nRight;
	}

	return nRight;
}