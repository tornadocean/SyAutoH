// AMHSDrive.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"

#include "Common.h"

#include "AMHS_Server.h"
#include "AMHSDrive.h"

#include "AMHSPacket.h"


// 这是已导出类的构造函数。
// 有关类定义的信息，请参阅 AMHSDrive.h
CAMHSDrive::CAMHSDrive()
{
	
}

CAMHSDrive::~CAMHSDrive()
{
	Clean();
}

int CAMHSDrive::Init()
{
	// using asio
	new AMHS_Server;
	sAmhsServer.Start(9999);

	return 0;
}

void CopySTKStruct(driveSTK& target, amhs_stocker_ptr& src)
{
	target.nID = src->nID;
	target.nStatus = src->nStatus;
	target.nAuto = src->nAuto;
	target.nManu = src->nManu;
	target.last_opt_foup_time.wYear=src->last_opt_foup_time.wYear;
	target.last_opt_foup_time.wMonth=src->last_opt_foup_time.wMonth;
	target.last_opt_foup_time.wDay=src->last_opt_foup_time.wDay;
	target.last_opt_foup_time.wHour=src->last_opt_foup_time.wHour;
	target.last_opt_foup_time.wMinute=src->last_opt_foup_time.wMinute;
	target.last_opt_foup_time.wSecond=src->last_opt_foup_time.wSecond;
}

void CopyFOUPStruct(driveFOUP& target, amhs_foup_ptr& src)
{
	target.nChaned=src->nChaned;
	target.nBarCode=src->nBarCode;
	target.nfoupRoom=src->nfoupRoom;
	target.nInput=src->nInput;
	target.nLot=src->nLot;
}

void CopyOHTStruct(driveOHT& target, amhs_oht_ptr& src)
{
	target.nID = src->nID;
	target.nPOS = src->nPOS;
	target.nHand  = src->nHand;
	target.nStatusTime = src->nStatusTime;
	target.nPosTime = src->nPosTime;
	target.nPathResult = src->nPathResult;
	target.nMoveStatus = src->nMoveStatus;
	target.nMoveAlarm = src->nMoveAlarm;
	target.nFoupOpt = src->nFoupOpt;
	target.nBackStatusMode = src->nBackStatusMode;
	target.nBackStatusMark = src->nBackStatusMark;
	target.nBackStausAlarm = src->nBackStausAlarm;
	target.bNeedPath = src->bNeedPath;
	target.strIp = src->p_participant->sIP_;
	target.uPort = src->p_participant->uPort_;
}

DR_STK_LIST CAMHSDrive::GetStkList()
{
	DR_STK_LIST list;
	amhs_stocker_vec stk_vec = sAmhsServer.GetServer()->STK_GetDataSet();
	for (amhs_stocker_vec::iterator it = stk_vec.begin();
		it != stk_vec.end(); ++it)
	{
		amhs_stocker_ptr sp_stk = *it;
		driveSTK dStk;
		CopySTKStruct(dStk, sp_stk);
		list.push_back(dStk);
	}
	return list;
}

DR_FOUP_LIST CAMHSDrive::GetStkFoupList(int nID)
{
	DR_FOUP_LIST list;
	amhs_foup_vec foup_vec = sAmhsServer.GetServer()->STK_GetFoupDataSet(nID);
	for(amhs_foup_vec::iterator it=foup_vec.begin();
		it != foup_vec.end(); ++it)
	{
		amhs_foup_ptr sp_foup= *it;
		driveFOUP dFoup;
		CopyFOUPStruct(dFoup, sp_foup);
		list.push_back(dFoup);
	}
	return list;
}

void CAMHSDrive::STKCleanLastEvent(int nID)
{
	sAmhsServer.GetServer()->STK_CleanLastEvent(nID);
}

DR_FOUP_LIST CAMHSDrive::GetStkLastOptFoup(int nID)
{
	DR_FOUP_LIST list;
	amhs_foup_vec foup_vec = sAmhsServer.GetServer()->STK_GetLastOptFoup(nID);
	for(amhs_foup_vec::iterator it=foup_vec.begin();
		it != foup_vec.end(); ++it)
	{
		amhs_foup_ptr sp_foup= *it;
		driveFOUP dFoup;
		CopyFOUPStruct(dFoup, sp_foup);
		list.push_back(dFoup);
	}

	return list;
}

vector<int> CAMHSDrive::GetStkRoom(int nID)
{
	vector<int> room_vec;
	room_vec=sAmhsServer.GetServer()->STK_GetRoom(nID);
	return room_vec;
}

DR_OHT_LIST CAMHSDrive::GetOhtList()
{
	DR_OHT_LIST list;
	amhs_oht_vec oht_vec = sAmhsServer.GetServer()->OHT_GetDataSet();
	for (amhs_oht_vec::iterator it = oht_vec.begin(); 
		it != oht_vec.end(); ++it)
	{
		amhs_oht_ptr sp_oht = *it;
		driveOHT dOht;
		CopyOHTStruct(dOht, sp_oht);
		list.push_back(dOht);
	}
	return list;
}

int CAMHSDrive::Check()
{

	return 0;
}

int CAMHSDrive::Clean()
{
	delete AMHS_Server::getSingletonPtr();
	return 0;
}

void CAMHSDrive::OHTStatusBackTime(int nID, int ms)
{
	sAmhsServer.GetServer()->OHT_Set_StatusBackTime(nID, ms);
}
void CAMHSDrive::OHTPosBackTime(int nID, int ms)
{
	sAmhsServer.GetServer()->OHT_Set_PosBackTime(nID, ms);
}
void CAMHSDrive::OHTMove(int nID, int nControl)
{
	sAmhsServer.GetServer()->OHT_Move(nID, nControl);
}
void CAMHSDrive::OHTFoup(int nID, int nDevBuf, int nOperation)
{
	sAmhsServer.GetServer()->OHT_Foup(nID, nDevBuf, nOperation);
}
void CAMHSDrive::OHTSetPath(int nID, int nType, int nStart, int nTarget, PATH_POINT_LIST& KeyPoints)
{
	//sAmhsServer.GetServer()->OHT_SetPath();
	amhs_keypoint_vec keyPts;
	for(PATH_POINT_LIST::iterator it = KeyPoints.begin();
		it != KeyPoints.end(); ++it)
	{
		amhs_keyPoint kPt;
		kPt.nPos = it->nPos;
		kPt.nType = it->nType;
		kPt.nSpeedRate = it->nSpeedRate;

		keyPts.push_back(kPt);
	}
	sAmhsServer.GetServer()->OHT_SetPath(nID, nType, nStart, nTarget, keyPts);
}

void CAMHSDrive::STKFoupHand(int nID, int nOpt, int nMode, int nData)
{
	sAmhsServer.GetServer()->STK_FOUP(nID, nOpt, nMode, nData);
}

void CAMHSDrive::STKStockerStatus(int nID)
{
	sAmhsServer.GetServer()->STK_Status(nID);
}

void CAMHSDrive::STKStockerRoom(int nID)
{
	sAmhsServer.GetServer()->STK_Room(nID);
}

void CAMHSDrive::STKFoupStorage(int nID)
{
	sAmhsServer.GetServer()->STK_Storage(nID);
}

void CAMHSDrive::STKInputStatus(int nID)
{
	sAmhsServer.GetServer()->STK_InputStatus(nID);
}

void CAMHSDrive::STKHistory(int nID, const SYSTEMTIME &timeStart, const SYSTEMTIME &timeEnd)
{
	sAmhsServer.GetServer()->STK_History(nID, timeStart, timeEnd);
}

void CAMHSDrive::STKAlarms(int nID, const SYSTEMTIME &timeStart, const SYSTEMTIME &timeEnd)
{
	sAmhsServer.GetServer()->STK_Alarms(nID, timeStart, timeEnd);
}

void CAMHSDrive::STKStatusBackTime(int nID, int ms)
{
	sAmhsServer.GetServer()->STK_Set_StatusBackTime(nID, ms);
}

void CAMHSDrive::STKFoupInfoBackTime(int nID,int ms)
{
	sAmhsServer.GetServer()->STK_Set_FoupBackTime(nID,ms);
}

int CAMHSDrive::SetOHTLocation(int nPoint)
{
	return 0;
}

