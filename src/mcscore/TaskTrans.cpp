#include "StdAfx.h"
#include "TaskTrans.h"
#include "../SqlAceCli/SqlAceCli.h"
#include "PathProductor.h"
using namespace MCS;


CTaskTrans::CTaskTrans(int nID, int nFoupBarCode, int nTarget)
	: m_nID(nID)
	, m_nFoupBarCode(nFoupBarCode)
	, m_nTarget(nTarget)
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
	
	// find FOUP Location
	DBFoup dbFoup;
	int nFoupID = dbFoup.FindFoup(m_nFoupBarCode);
	FoupLocation locFoup = {0};
	if (nFoupID > 0)
	{
		dbFoup.GetFoupLocation(nFoupID, locFoup);
	}

	if(locFoup.nLocType == dbcli::loctypeStocker)
	{
		TranCommand tran;
		tran.m_strCommand = "Place";
		tran.nDevType = dbcli::loctypeStocker;
		tran.nDevID = locFoup.nCarrier;
		tran.nPort = locFoup.nPort;
		m_cmdList.push_back(tran);
	}
	else 
	{
		//TODO: other device
	}
	
	// find FOUP keypoints
	DBFoupDevice dbFoupDevice;
	ItemFoupDevie itemFoupDev;
	int posFoup = 0;
	itemFoupDev = dbFoupDevice.GetItem(locFoup.nLocType, locFoup.nCarrier, locFoup.nPort);
	if(itemFoupDev.nID > 0)
	{
		DBKeyPoints dbKP;
		KeyPointItem kpoint;
		kpoint = dbKP.GetKeyPointByID(itemFoupDev.nDevPortKP);
		if(kpoint.nID > 0)
		{
			posFoup = kpoint.uPosition;
			// TODO: OHT Move to posFoup
			{
				TranCommand tranCmd;
				tranCmd.m_strCommand = "Move";
				tranCmd.nDevType = dbcli::loctypeOHT;
				tranCmd.nTo = posFoup;
				m_cmdList.push_back(tranCmd);
			}

			// TODO: OHT Pick Foup
			{
				TranCommand tranCmd;
				tranCmd.m_strCommand = "Pick";
				tranCmd.nDevType = dbcli::loctypeOHT;
				tranCmd.nPosition = posFoup;
				m_cmdList.push_back(tranCmd);
			}
		}
	}

	//TODO: OHT Move to Target
	{
		TranCommand tranCmd;
		tranCmd.m_strCommand = "Move";
		tranCmd.nDevType = dbcli::loctypeOHT;
		tranCmd.nFrom = posFoup;
		tranCmd.nTo = m_nTarget;
		m_cmdList.push_back(tranCmd);
	}

	// TODO: Target Type
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
		tranCmd.nTo = m_nTarget;
		m_cmdList.push_back(tranCmd);
	}

	// TODO: Target Operation
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
