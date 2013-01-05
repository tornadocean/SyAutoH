#include "StdAfx.h"
#include "GuiDataHubI.h"
#include "../AMHSDrive/AMHSDrive.h"
#include "../shared/Util.h"
#include "../shared/Log.h"
#include "../SqlAceCli/SqlAceCli.h"
#include "IceUtil/Unicode.h"
#include "iConstDef.h"

GuiDataItem GuiDataHubI::Push_STK_DevInfo()
{
	GuiDataItem item;
	item.enumTag = GuiHub::upStkInfo;

	DR_STK_LIST stk_list = m_pAMHSDrive->GetStkList();
	string strGuiData = "";
	char buf[256] ="";
	for (DR_STK_LIST::iterator it = stk_list.begin(); 
		it != stk_list.end(); ++it)
	{
		sprintf_s(buf, 256, "<%d,%d,%d>", it->nID, 0, 0);
		strGuiData += buf;
	}

	item.sVal = strGuiData;

	return item;
}

GuiDataItem GuiDataHubI::Push_STK_FoupInfo()
{
	GuiDataItem item;
	item.enumTag = GuiHub::upStkFoupsInfo;
	string strGuiData="";
	char buf[256]="";

	DR_STK_LIST stk_list = m_pAMHSDrive->GetStkList();
	for (DR_STK_LIST::iterator it = stk_list.begin(); 
		it != stk_list.end(); ++it)
	{
		DR_FOUP_LIST foup_list=m_pAMHSDrive->GetStkFoupList(it->nID);
		for(DR_FOUP_LIST::iterator itFoup = foup_list.begin();
			itFoup != foup_list.end(); ++itFoup)
		{
			sprintf_s(buf, 256, "<%d,%d,%d,%d,%d,%d>",
				it->nID,itFoup->nBarCode, itFoup->nfoupRoom, itFoup->nLot, itFoup->nChaned, 0);
			strGuiData += buf;
		}
	}
	item.sVal=strGuiData;
	return item;
}

GuiDataItem GuiDataHubI::Push_STK_LastOptFoup()
{
	GuiDataItem item;
	item.enumTag=GuiHub::upStkLastOptFoup;
	string strGuiData="";
	char buf[256]="";

	DR_STK_LIST stk_list = m_pAMHSDrive->GetStkList();
	for (DR_STK_LIST::iterator it = stk_list.begin(); 
		it != stk_list.end(); ++it)
	{
		DR_FOUP_LIST foup_list=m_pAMHSDrive->GetStkLastOptFoup(it->nID);
		for(DR_FOUP_LIST::iterator itFoup = foup_list.begin();
			itFoup != foup_list.end(); ++itFoup)
		{
			sprintf_s(buf, 256, "<%d, %d, %d, %d, %d, %d, %d, %d, %d, %d, %d, %d>",
				it->nID,itFoup->nBarCode, itFoup->nfoupRoom, itFoup->nLot, itFoup->nChaned, itFoup->nInput, 
				it->last_opt_foup_time.wYear, it->last_opt_foup_time.wMonth, it->last_opt_foup_time.wDay, 
				it->last_opt_foup_time.wHour, it->last_opt_foup_time.wMinute, it->last_opt_foup_time.wSecond);
			strGuiData += buf;
		}
	}
	item.sVal=strGuiData;
	return item;
}

GuiDataItem GuiDataHubI::Push_STK_Status()
{
	GuiDataItem item;
	item.enumTag = GuiHub::upStkStatus;
	string strGuiData="";
	char buf[256]="";

	DR_STK_LIST stk_list = m_pAMHSDrive->GetStkList();
	for (DR_STK_LIST::iterator it = stk_list.begin(); 
		it != stk_list.end(); ++it)
	{
		sprintf_s(buf, 256, "<%d,%d>", it->nID, it->nStatus);
		strGuiData += buf;
	}
	item.sVal = strGuiData;
	return item;
}

GuiDataItem GuiDataHubI::Push_STK_InputStatus()
{
	GuiDataItem item;
	item.enumTag=GuiHub::upStkInputStatus;
	string strGuiData="";
	char buf[256]="";

	DR_STK_LIST stk_list = m_pAMHSDrive->GetStkList();
	for (DR_STK_LIST::iterator it = stk_list.begin(); 
		it != stk_list.end(); ++it)
	{
		sprintf_s(buf, 256, "<%d,%d,%d>", it->nID, it->nAuto, it->nManu);
		strGuiData += buf;
	}
	item.sVal = strGuiData;
	return item;
}

GuiDataItem GuiDataHubI::Push_STK_GetRoom()
{
	GuiDataItem item;
	item.enumTag=GuiHub::upStkRoomStatus;
	string strGuiData="";
	char buf[100]="";

	DR_STK_LIST stk_list = m_pAMHSDrive->GetStkList();
	for (DR_STK_LIST::iterator it = stk_list.begin(); 
		it != stk_list.end(); ++it)
	{
		vector<int> room_vec=m_pAMHSDrive->GetStkRoom(it->nID);
		strGuiData += "<";
		strGuiData += itoa(it->nID,buf,10);
		for(vector<int>::iterator it_vec=room_vec.begin();
			it_vec!=room_vec.end(); ++it_vec)
		{
			strGuiData += ",";
			strGuiData += itoa(*it_vec,buf,10);
		}
		strGuiData += ">";
	}
	item.sVal += strGuiData;
	return item;
}

void GuiDataHubI::STK_SetStatusBackTime(const std::string& strVal, const ::Ice::Current&)
{
	STR_VEC vecStr = GetVecStrings(strVal);
	for(STR_VEC::iterator it = vecStr.begin();
		it != vecStr.end(); ++it)
	{
		string strE = *it;
		STR_VEC Params = SplitString(*it, ",");
		if (Params.size() == 2)
		{
			int nID = atoi(Params[0].c_str());
			int nTime = atoi(Params[1].c_str());
			m_pAMHSDrive->STKStatusBackTime(nID, nTime);
		}
	}
}

void GuiDataHubI::STK_SetFoupInfoBackTime(const std::string& strVal, const ::Ice::Current&)
{
	STR_VEC vecStr = GetVecStrings(strVal);
	for(STR_VEC::iterator it=vecStr.begin();
		it!=vecStr.end();++it)
	{
		string strE=*it;
		STR_VEC Params=SplitString(*it,",");
		if(Params.size()==2)
		{
			int nID=atoi(Params[0].c_str());
			int nTime=atoi(Params[1].c_str());
			m_pAMHSDrive->STKFoupInfoBackTime(nID,nTime);
		}
	}
}

void GuiDataHubI::STK_FoupHanding(const std::string& strVal, const ::Ice::Current&)
{
	STR_VEC vecStr=GetVecStrings(strVal);
	for(STR_VEC::iterator it=vecStr.begin();
		it!=vecStr.end();++it)
	{
		string strE = *it;
		STR_VEC Params=SplitString(*it,",");
		if(Params.size()==4)
		{
			int nID=atoi(Params[0].c_str());
			int nOpt=atoi(Params[1].c_str());
			int nMode=atoi(Params[2].c_str());
			int nData=atoi(Params[3].c_str());
			m_pAMHSDrive->STKFoupHand(nID, nOpt, nMode, nData);
		}
	}
}

void GuiDataHubI::STK_StockerStatus(const std::string& strVal, const ::Ice::Current&)
{
	STR_VEC vecStr=GetVecStrings(strVal);
	if(vecStr.size()==1)
	{
		int nID=atoi(vecStr[0].c_str());
		m_pAMHSDrive->STKStockerStatus(nID);
	}
}

void GuiDataHubI::STK_StockerRoom(const std::string& strVal, const ::Ice::Current&)
{
	STR_VEC vecStr=GetVecStrings(strVal);
	if(vecStr.size()==1)
	{
		int nID=atoi(vecStr[0].c_str());
		m_pAMHSDrive->STKStockerRoom(nID);
	}
}

void GuiDataHubI::STK_StockerFoupStorage(const std::string& strVal, const ::Ice::Current&)
{
	STR_VEC vecStr=GetVecStrings(strVal);
	if(vecStr.size()==1)
	{
		int nID=atoi(vecStr[0].c_str());
		m_pAMHSDrive->STKFoupStorage(nID);
	}
}

void GuiDataHubI::STK_InputStatus(const std::string& strVal, const ::Ice::Current&)
{
	STR_VEC vecStr=GetVecStrings(strVal);
	if(vecStr.size()==1)
	{
		int nID=atoi(vecStr[0].c_str());
		m_pAMHSDrive->STKInputStatus(nID);
	}
}

void GuiDataHubI::STK_History(const std::string& strVal, const ::Ice::Current&)
{
	STR_VEC vecStr=GetVecStrings(strVal);
	for(STR_VEC::iterator it=vecStr.begin();
		it!=vecStr.end();++it)
	{
		string strE= *it;
		STR_VEC Params=SplitString(*it,",");
		if(Params.size()==3)
		{
			int nID=atoi(Params[0].c_str());
			SYSTEMTIME timeStart=ToTime(Params[1]);
			SYSTEMTIME timeEnd=ToTime(Params[2]);
			m_pAMHSDrive->STKHistory(nID,timeStart,timeEnd);
		}
	}
}

void GuiDataHubI::STK_Alarms(const std::string& strVal, const ::Ice::Current&)
{
	STR_VEC vecStr=GetVecStrings(strVal);
	for(STR_VEC::iterator it=vecStr.begin();
		it!=vecStr.end();++it)
	{
		string strE= *it;
		STR_VEC Params=SplitString(*it,",");
		if(Params.size()==3)
		{
			int nID=atoi(Params[0].c_str());
			SYSTEMTIME timeStart=ToTime(Params[1]);
			SYSTEMTIME timeEnd=ToTime(Params[2]);
			m_pAMHSDrive->STKAlarms(nID, timeStart, timeEnd);
		}
	}
}

void GuiDataHubI::STK_GetRoomStatus(const std::string&, const ::Ice::Current& current)
{
	string strVal = "";
	char buf[100]="";

	DR_STK_LIST stk_list = m_pAMHSDrive->GetStkList();
	for (DR_STK_LIST::iterator it = stk_list.begin(); 
		it != stk_list.end(); ++it)
	{
		vector<int> room_vec=m_pAMHSDrive->GetStkRoom(it->nID);
		
		strVal += "<";
		strVal += itoa(it->nID,buf,10);
		for(vector<int>::iterator it_vec=room_vec.begin();
			it_vec!=room_vec.end(); ++it_vec)
		{
			strVal += ",";
			strVal += itoa(*it_vec,buf,10);
		}
		strVal += ">";
	}

	UpdateDataOne(current.con, GuiHub::upStkRoomStatus, strVal);
}

SYSTEMTIME GuiDataHubI::ToTime(std::string& strVal)
{
	std::string strTime=strVal;
	SYSTEMTIME time;
	int nYear=0;
	int nMonth=0;
	int nDay=0;
	int nHour=0;
	int nMin=0;
	int nSec=0;

	sscanf(strVal.c_str(),"%d-%d-%d %d:%d:%d",&nYear,&nMonth,&nDay,&nHour,&nMin,&nSec);

	time.wYear=nYear;
	time.wMonth=nMonth;
	time.wDay=nDay;
	time.wHour=nHour;
	time.wMinute=nMin;
	time.wSecond=nSec;
	return time;
}