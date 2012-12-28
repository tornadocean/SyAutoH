#include "StdAfx.h"
#include "Logger.h"

#ifdef WINCE
	#include <IceE/Unicode.h>
#endif

#include <atlbase.h>
#include <time.h>
#include <sstream>
#include <iostream>

#include "../common/LogDataDb.h"
#include "LogDef.h"

void LogTrace(LPCTSTR pszFormat, ...)  
{  
	va_list pArgs;  

	TCHAR szMessageBuffer[16380]={0};  
	va_start( pArgs, pszFormat );  
	_vsntprintf( szMessageBuffer, 16380, pszFormat, pArgs );  
	va_end( pArgs );  

	OutputDebugString(szMessageBuffer);  
}  

CLogger::CLogger(void)
{
	m_nDupMark = 0;
	m_nOverBuf = 0;
	m_strFileName = "";
	m_strModule = "";
	logger = NULL;
	m_bConnected = false;
	m_dwTick = 0;
	m_hThreadDbStore = NULL;
	m_hThreadUpLoad = NULL;
	m_eventStore = NULL;
	m_eventUpLoad = NULL;

	communicator = NULL;

	//Init();
	LogTrace(_T("-----------------------------CLogger::CLogger ------------------------------\r\n"));
}

CLogger::~CLogger(void)
{
	End();
}

int CLogger::InitICE(void)
{
	//return 0;
	std::string strPath = GetProcessPath();
	
	try
	{
		//EndICE();
		Ice::InitializationData initData;
		initData.properties = Ice::createProperties();

		//
		// Set a default value for "Hello.Proxy" so that the demo will
		// run without a configuration file.
		//
		initData.properties->setProperty("ICLog.Proxy", "ICLog:tcp -p 18888");

		//
		// Now, load the configuration file if present. Under WinCE we
		// use "config.txt" since it can be edited with pocket word.
		//
		std::string strConfigfile = "";
		try
		{
			strConfigfile = strPath + "../config/IceClientConfig.txt";
			initData.properties->load(strConfigfile);
		}
		catch(const Ice::FileException& ex)
		{
			std::cout<< ex.what() << endl;
			std::cout<< "Maybe cannot find the config file: " << strConfigfile << endl;
			Sleep(1000);
		}

		int argc = 0;
		communicator = Ice::initialize(argc, 0, initData);

		logger =
			LogPrx::uncheckedCast(communicator->stringToProxy(
			initData.properties->getProperty("ICLog.Proxy")));

		logger->ice_timeout(3000);

		//if(!logger)
		//{
		//	MessageBox(NULL, L"invalid proxy", L"Minimal Client", MB_ICONEXCLAMATION | MB_OK);
		//	return EXIT_FAILURE;
		//}
	}
	catch(const Ice::Exception& ex)
	{
		TCHAR wtext[1024] = {0};

#ifdef WINCE
		string err = ex.toString();
		mbstowcs(wtext, err.c_str(), err.size());
#else
		ostringstream ostr;
		ostr << ex;
		string s = ostr.str();
		mbstowcs(wtext, s.c_str(), s.size());
#endif

		//MessageBox(NULL, wtext, L"Error", MB_ICONEXCLAMATION | MB_OK);

		//status = EXIT_FAILURE;
		return -1;
	}

	return 0;
}

int CLogger::Init(void)
{
	if(m_hThreadDbStore != NULL && m_hThreadUpLoad != NULL)
	{
		return 0;
	}
	
	std::string strPath = GetProcessPath();

	m_strModule = GetProcessName();
	TCHAR wstrPath[1024] = {0};
	std::string strDataPath;
	strDataPath = strPath + "..\\data\\";
	mbstowcs(wstrPath,strDataPath.c_str(), strDataPath.size());
	CreateDirectory(wstrPath,NULL);

	strDataPath = strPath + "..\\data\\log\\";
	mbstowcs(wstrPath,strDataPath.c_str(), strDataPath.size());
	CreateDirectory(wstrPath,NULL);
	
	m_strFileName = strPath + "..\\data\\log\\" +m_strModule + ".logdb";

	bool ret = false;
	m_eventStore = CreateEvent(NULL, true, false, NULL);
	
#ifdef WINCE
	m_hThreadDbStore = CreateThread(NULL, 0, __DataDaseStore, (LPVOID)this, 0, NULL);
#else
	m_hThreadDbStore = (HANDLE)_beginthreadex(NULL, 0, __DataDaseStore, (LPVOID)this, 0, NULL);
#endif

	if (m_hThreadDbStore)
	{
		SetThreadPriority(m_hThreadDbStore, THREAD_PRIORITY_BELOW_NORMAL);
		ret = true;
	}
	else
	{
		ret = false;
	}

	m_eventUpLoad = CreateEvent(NULL, true, false, NULL);

#ifdef WINCE
	m_hThreadUpLoad =  CreateThread(NULL, 0, __DataUpLoad, (LPVOID)this, 0, NULL);
#else
	m_hThreadUpLoad =  (HANDLE)_beginthreadex(NULL, 0, __DataUpLoad, (LPVOID)this, 0, NULL);
#endif

	if (m_hThreadUpLoad)
	{
		SetThreadPriority(m_hThreadUpLoad, THREAD_PRIORITY_BELOW_NORMAL);
		ret = true;
	}
	else
	{
		ret = false;
	}

	return 0;
}

int CLogger::EndICE(void)
{
	if(communicator)
		{
			try
			{
				communicator->shutdown();
				//communicator->destroy();
				return 0;
			}
			catch(const Ice::Exception& ex)
			{
				TCHAR wtext[1024] = {0};

#ifdef WINCE
				string err = ex.toString();
				mbstowcs(wtext, err.c_str(), err.size());
#else
				ostringstream ostr;
				ostr << ex;
				string s = ostr.str();
				mbstowcs(wtext, s.c_str(), s.size());
#endif
			}
		}

	return -1;
}

int CLogger::End(void)
{
	if (m_hThreadDbStore)
	{
		SetEvent(m_eventStore);
		WaitForSingleObject(m_hThreadDbStore, 2000);  
		CloseHandle(m_hThreadDbStore);
		m_hThreadDbStore = NULL;
		CloseHandle(m_eventStore);
	}

	if (m_hThreadUpLoad)
	{
		SetEvent(m_eventUpLoad);
		WaitForSingleObject(m_hThreadUpLoad, 2000);  
		CloseHandle(m_hThreadUpLoad);
		m_hThreadUpLoad= NULL;
		CloseHandle(m_eventUpLoad);
	}
	

	LogMsg* msg = NULL;
	while(!IsEventBufferEmpty())
	{
		msg = Pop();
		delete msg;
	}
	EndICE();

	return 0;
}

__int64 CLogger::GetTime()
{
	SYSTEMTIME time;
	GetLocalTime(&time);

	__int64 _64Time = 0;
	
	char buf[24] = {0};
	sprintf(buf, "%04d%02d%02d%02d%02d%02d%03d", time.wYear, 
		time.wMonth, time.wDay, time.wHour, time.wMinute,
		time.wSecond, time.wMilliseconds);

	_64Time = _atoi64(buf);

	return _64Time;
}

void CLogger::Log(int nID, UCHAR type, const CHAR* strUnit, const CHAR* strMsg)
{
	__int64 _64Time = GetTime();
	
	LogMsg logData;
	logData.nEventID = nID;
	logData.dParam1 = 0;
	logData.dParam2 = 0;
	logData.nParam1 = 0;
	logData.nParam2 = 0;
	//memset(&logData, 0, sizeof(LogMsg));
	logData.lTime = _64Time;
	

	logData.nID = nID;
	logData.nType = type;

	std::string sStr = "";
	
	USES_CONVERSION;
	std::wstring s = A2T(strUnit);
	sStr = IceUtil::wstringToString(s);
	logData.strUnit = sStr;
	//logData.strUnit = strUnit;

	std::wstring s2 = A2T(strMsg);
	sStr = IceUtil::wstringToString(s2);
	logData.strMsg = sStr;
	//logData.strMsg = strMsg;

	Push(logData);
}

Gemma::LogMsg* CLogger::Pop()
{

	LogMsg* data = NULL;


	if(!IsEventBufferEmpty())
	{  	
		WLock(m_MutexBuf)
		{
			//memcpy(&data, &m_logList[m_nListBegin], sizeof(data));
			data = m_logBuf[m_nListBegin];
			m_logBuf[m_nListBegin] = NULL; 

			//memset(&m_logList[m_nListBegin], 0, sizeof(data));
			m_nListBegin = (m_nListBegin + 1) % LogListCount; 
		}
	}

	return data;
}
//int n=0;
//int np=0;
//int nz = 0;
void CLogger::Push(const ::Gemma::LogMsg & data)
{
	if (IsLogBufferFull())
	{
		m_nOverBuf++;
		return;
	}

	if(data.lTime - logLast.lTime < 5)
	{
		if((logLast.nType == data.nType)
			&& (logLast.nEventID == data.nEventID)
			&&(logLast.strUnit.compare(data.strUnit) == 0)
			&&(logLast.strMsg.compare(data.strMsg) == 0))
		{
			m_nDupMark++;
			return;
		}
	}
	else
	{
		//TRACE("LOGZ %d\n", nz++);

		if (m_nDupMark > 0)
		{
			std::ostringstream   ostr;
			ostr << " | continuous "<<m_nDupMark << " times";
			Gemma::LogMsg *pMsg = new Gemma::LogMsg;
			*pMsg = logLast;
			pMsg->strMsg += ostr.str();
			pMsg->lTime = data.lTime;
			WLock(m_MutexBuf)
			{
				m_logBuf[m_nListEnd] = pMsg;
				m_nListEnd = (m_nListEnd + 1) % LogListCount; 
			}
			m_nDupMark = 0;
		}
	}

	Gemma::LogMsg *pMsg = new Gemma::LogMsg;
	*pMsg = data;
	logLast = data;

	if (m_nOverBuf>0)
	{
		std::ostringstream   ostr;
		ostr << " | lost "<<m_nOverBuf << " logs";
		pMsg->strMsg += ostr.str();
		//m_nOverBuf=0;
	}

	WLock(m_MutexBuf)
	{
		m_logBuf[m_nListEnd] = pMsg;
		m_nListEnd = (m_nListEnd + 1) % LogListCount; 
	}
}

std::string CLogger::GetProcessName()
{
	TCHAR buf[256] = {0};
	GetModuleFileName(NULL, buf, 256);
	std::wstring wstr;
	wstr = buf;
	std::string str = "";
	str =  IceUtil::wstringToString(wstr);

	size_t nBar = str.find_last_of('\\') + 1;
	str = str.substr(nBar, str.length() - nBar);

	size_t nCm = str.find_last_of('.');
	str = str.substr(0, nCm);

	return str;
}

std::string CLogger::GetProcessPath()
{
	TCHAR buf[256] = {0};
	GetModuleFileName(NULL, buf, 256);
	std::wstring wstr;
	wstr = buf;
	std::string str = "";
	str =  IceUtil::wstringToString(wstr);

	size_t nBar = str.find_last_of('\\') + 1;
	str = str.substr(0, nBar);

	//int nCm = str.find_last_of('.');
	//str = str.substr(0, nCm);

	return str;
}

int CLogger::UpLoadtoServer(void)
{
	try
	{
		logger->ice_ping();
		m_bConnected = true;
	}
	catch (Ice::Exception& ex)
	{
		ex.what();
		m_bConnected = false;
		return -1;
	}

	LogList listUpdate;
	int nCount = GetMsg(listUpdate);

	if (nCount > 0)
	{
		try
		{
			m_bConnected = false;
			logger->sendLog(listUpdate);
			m_bConnected = true;
			DeleteMsg(listUpdate);
		}
		catch (Ice::Exception& ex)
		{
			ex.what();
		}
	}
	
	return 0;
}

int CLogger::GetLastType()
{
	int nRet = -1;
	if (true == m_bConnected)
	{
		int nID = logger->GetLastID();
		IDList list;
		IDList tyList;
		list.push_back(-1);
		LogList lMsg = logger->GetLog(nID, 1, tyList, list, ""); // 0 length for tyList means ALL
		if (lMsg.size() > 0)
		{
			nRet = lMsg[0].nType;
		}
	}
	else
	{
		nRet = -1;
	}

	return nRet;
}

int CLogger::GetAlarmCount()
{
	int nRet = 0;
	if (true == m_bConnected)
	{
		nRet = logger->GetAlarmCount();
	}
	else
	{
		nRet = -1;
	}

	return nRet;
}

int CLogger::WritetoDb(void)
{

	if (IsEventBufferEmpty())
	{
		return -1;
	}

	WLock(m_MutexDB)
	{
		if (!OpenDB())
		{
			return -1;
		}
		LogMsg* msg = NULL;
		m_logTable.BeginTrans();
		while(!IsEventBufferEmpty())
		{
			msg = Pop();
			if (NULL == msg)
			{
				continue;
			}
			m_logTable.AddNew();
			m_logTable.m_nEventID = msg->nEventID;
			m_logTable.m_strMsg =msg->strMsg.c_str();
			m_logTable.m_llTime = msg->lTime;
			m_logTable.m_nType = msg->nType;
			m_logTable.m_strUnit = msg->strUnit.c_str();
			m_logTable.m_nParam1 = msg->nParam1;
			m_logTable.m_nParam2 = msg->nParam2;
			m_logTable.m_dParam1 = msg->dParam1;
			m_logTable.m_dparam2 = msg->dParam2;
			m_logTable.Update();

			delete msg;
		}
		m_logTable.EndTrans();

		CloseDB();
	}
	
	return 0;
}

#ifdef WINCE
	DWORD CLogger::__DataDaseStore(LPVOID lp)
#else
	unsigned CLogger::__DataDaseStore(LPVOID lp)
#endif
{
	CLogger* pLog = static_cast<CLogger*>(lp);

	while(1)
	{
		pLog->WritetoDb();
		//Sleep(50);
		DWORD dwRet = WaitForSingleObject(pLog->m_eventStore, 50);
		if (WAIT_OBJECT_0 == dwRet)
		{
			break;
		}
	}
	return 0;
}

#ifdef WINCE
	DWORD CLogger::__DataUpLoad(LPVOID lp)
#else
	unsigned int CLogger::__DataUpLoad(LPVOID lp)
#endif

{
	CLogger* pLog = static_cast<CLogger*>(lp);

	pLog->InitICE();

	while(1)
	{
		pLog->UpLoadtoServer();
		//Sleep(50);
		DWORD dwRet = WaitForSingleObject(pLog->m_eventUpLoad, 50);
		if(WAIT_OBJECT_0 == dwRet)
		{
			break;
		}
	}

	return 0;
}

bool CLogger::OpenDB(void)
{
	BOOL bConnect =m_logDB.ConnectDb(m_strFileName.c_str());
	if (FALSE == bConnect)
	{
		return false;
	}

	BOOL bOpen = m_logTable.Open(m_logDB.GetDBPtr());
	if (FALSE == bOpen)
	{
		return false;
	}

	return true;
}

void CLogger::CloseDB(bool bVacuum)
{
	if (true == bVacuum)
	{
		DWORD dwTick = GetTickCount();
		if ((dwTick - m_dwTick) > VacuumTime )
		{
			m_logTable.Compact();
			m_dwTick = dwTick;
		}
	}

	//m_logTable.Close();
	m_logDB.Close();
}

int CLogger::DeleteMsg(Gemma::LogList& msgList)
{
	WLock(m_MutexDB)
	{
		if (OpenDB() == false)
		{
			return -1;
		}

		m_logTable.SetSort("[id]");
		m_logTable.SetLimit((int)msgList.size());
		m_logTable.Query();
		int count = m_logTable.GetCount();
		if (count > 0)
		{
			m_logTable.BeginTrans();
			while(!m_logTable.IsEOF())
			{
				m_logTable.Delete();
				m_logTable.MoveNext();
			}
			m_logTable.EndTrans();
		}

		CloseDB();
	}

	return (int)msgList.size();
}

int CLogger::GetMsg(Gemma::LogList& msgList)
{
	RLock(m_MutexDB)
	{
		if (OpenDB() == false)
		{
			return -1;
		}

		int nLimitCount = 0;
		if (true == m_bConnected)
		{
			nLimitCount = UpLoadListCount;
		}
		else
		{
			nLimitCount = 10;
		}

		m_logTable.SetSort("[id]");
		m_logTable.SetLimit(nLimitCount);
		m_logTable.Query();
		int count = m_logTable.GetCount();
		if (count > 0)
		{
			LogMsg msg;
			msg.nEventID = 0;
			msg.dParam1 = 0;
			msg.dParam2 = 0;
			msg.lTime = 0;
			msg.nID = 0;
			msg.nParam1 = 0;
			msg.nParam2 = 0;
			msg.nType = 0;
			msg.nUnit = 0;

			while(!m_logTable.IsEOF())
			{
				msg.nEventID = m_logTable.m_nEventID;
				msg.lTime =m_logTable.m_llTime;
				msg.nType = (short)m_logTable.m_nType;
				msg.strUnit = m_logTable.m_strUnit;
				msg.strMsg = m_logTable.m_strMsg;
				msg.dParam1 = m_logTable.m_dParam1;
				msg.dParam2 = m_logTable.m_dparam2;
				msg.nParam1 = m_logTable.m_nParam1;
				msg.nParam2 = m_logTable.m_nParam2;

				msgList.push_back(msg);
				m_logTable.MoveNext();
			}
		}

		CloseDB();
	}

	return (int)msgList.size();
}