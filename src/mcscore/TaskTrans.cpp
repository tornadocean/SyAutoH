#include "StdAfx.h"
#include "TaskTrans.h"
#include "../SqlAceCli/SqlAceCli.h"
#include "PathProductor.h"
using namespace MCS;


CTaskTrans::CTaskTrans(int nID, int nFoupBarCode, int nTarget)
	: m_nID(nID)
	, m_nFoupBarCode(nFoupBarCode)
	, m_nTarget(nTarget)
	, m_amhsDrive(NULL)
	, m_nFoupKeyPoint(0)
	, m_locFoupSource()
{
}


CTaskTrans::~CTaskTrans(void)
{
}


void CTaskTrans::Run(void)
{
	LOG_DEBUG("ID:%d, FOUP:%d, TARGET:%d", 
		m_nID, m_nFoupBarCode, m_nTarget);

	sLog.outDebug("Make Commands.");
	
	_findFoupSource();

	_prepareFoup(); // stocker output
	
	_findFoupPos();

	_ohtMoveToFoupSource();

	_ohtPickFoup();

	_ohtMoveToTarget();

	_ohtPlaceFoup();

	_targetOperate(); // stocker input
	

	// output command list
	for(auto it = m_cmdList.cbegin();
		it != m_cmdList.cend(); ++it)
	{
		sLog.outBasic("Cmd: %s, DevType: %d, Dev: %d, From: %d, To: %d, Pos: %d",
			it->m_strCommand.c_str(), it->nDevType, 
			it->nDevID, it->nFrom, it->nTo, it->nPosition);
	}

	sLog.outDebug("Check Commands.");
	
	// run commands
	sLog.outDebug("Run Commands.");
	Sleep(2000);

}


void CTaskTrans::_findFoupSource(void)
{
	DBFoup dbFoup;
	int nFoupID = dbFoup.FindFoup(m_nFoupBarCode);
	if (nFoupID > 0)
	{
		dbFoup.GetFoupLocation(nFoupID, m_locFoupSource);
	}
}


void CTaskTrans::_prepareFoup(void)
{
	if(m_locFoupSource.nLocType == dbcli::loctypeStocker)
	{
		TranCommand tran;
		tran.m_strCommand = "Place";
		tran.nDevType = dbcli::loctypeStocker;
		tran.nDevID = m_locFoupSource.nCarrier;
		tran.nPort = m_locFoupSource.nPort;
		m_cmdList.push_back(tran);
	}
	else 
	{
		//TODO: other device
	}
}


void CTaskTrans::_findFoupPos(void)
{
	DBFoupDevice dbFoupDevice;
	ItemFoupDevie itemFoupDev;
	int posFoup = 0;
	itemFoupDev = dbFoupDevice.GetItem(
		m_locFoupSource.nLocType, 
		m_locFoupSource.nCarrier, 
		m_locFoupSource.nPort);
	if(itemFoupDev.nID > 0)
	{
		DBKeyPoints dbKP;
		KeyPointItem kpoint;
		kpoint = dbKP.GetKeyPointByID(itemFoupDev.nDevPortKP);
		if(kpoint.nID > 0)
		{
			m_nFoupKeyPoint = kpoint.uPosition;
		}
	}
}


void CTaskTrans::_ohtMoveToFoupSource(void)
{
	TranCommand tranCmd;
	tranCmd.m_strCommand = "Move";
	tranCmd.nDevType = dbcli::loctypeOHT;
	tranCmd.nTo = m_nFoupKeyPoint;
	m_cmdList.push_back(tranCmd);
}


void CTaskTrans::_ohtPickFoup(void)
{
	TranCommand tranCmd;
	tranCmd.m_strCommand = "Pick";
	tranCmd.nDevType = dbcli::loctypeOHT;
	tranCmd.nPosition = m_nFoupKeyPoint;
	m_cmdList.push_back(tranCmd);
}


void CTaskTrans::_ohtMoveToTarget(void)
{
		TranCommand tranCmd;
		tranCmd.m_strCommand = "Move";
		tranCmd.nDevType = dbcli::loctypeOHT;
		tranCmd.nFrom = m_nFoupKeyPoint;
		tranCmd.nTo = m_nTarget;
		tranCmd.nPosition = m_nFoupKeyPoint;
		m_cmdList.push_back(tranCmd);
}


void CTaskTrans::_ohtPlaceFoup(void)
{
	DBKeyPoints kptDB;
	KeyPointItem target = 
		kptDB.GetKeyPointByPos(m_nTarget);
	bool CanPlace = false;
	if(target.nID > 0)
	{
		switch (target.uType)
		{
		case 0x80:
		case 0x40:
		case 0x20:
			CanPlace = true;
		default:
			break;
		}
	}
	

	// TODO: OHT Place Foup
	if(true == CanPlace)
	{
		TranCommand tranCmd;
		tranCmd.m_strCommand = "Place";
		tranCmd.nDevType = dbcli::loctypeOHT;
		tranCmd.nPosition = m_nTarget;
		m_cmdList.push_back(tranCmd);
	}
}


void CTaskTrans::_targetOperate(void)
{
	DBKeyPoints kptDB;
		KeyPointItem target = 
			kptDB.GetKeyPointByPos(m_nTarget);
		if(target.nID > 0)
		{
			switch (target.uType)
			{
			case 0x80:
				// TODO: stocker pick
				{
					TranCommand tran;
					tran.m_strCommand = "Pick";
					tran.nDevType = dbcli::loctypeStocker;
					tran.nPosition = m_nTarget;
					m_cmdList.push_back(tran);
				}
				break;
			case 0x40:
				// todo: process ready
				{
					TranCommand tran;
					tran.m_strCommand = "Ready";
					tran.nDevType = dbcli::loctypeProcessDev;
					tran.nPosition = m_nTarget;
					m_cmdList.push_back(tran);
				}
				break;
			case 0x20:
				break;
			default:
				break;
			}
		}
}
