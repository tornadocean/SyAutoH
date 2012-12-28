#include "StdAfx.h"
#include "GuiDataHubI.h"
#include "../AMHSDrive/AMHSDrive.h"
#include "../shared/Util.h"
#include "../shared/Log.h"
#include "../SqlAceCli/SqlAceCli.h"
#include "IceUtil/Unicode.h"
#include "iConstDef.h"
#include <MMSystem.h>
#pragma comment(lib, "winmm.lib")

GuiDataHubI::GuiDataHubI(void)
	: m_nTimerPeriord(200),
	m_nTimerID(0)
{
	// for oht
	m_mapHandles.insert(std::make_pair(GuiHub::OhtPosTime, 
		&GuiDataHubI::OHT_SetPositionBackTime));
	m_mapHandles.insert(std::make_pair(GuiHub::OhtStatusTime, 
		&GuiDataHubI::OHT_SetStatusBackTime));
	m_mapHandles.insert(std::make_pair(GuiHub::OhtGetPosTable, 
		&GuiDataHubI::OHT_GetPositionTable));
	m_mapHandles.insert(std::make_pair(GuiHub::OhtFoupHanding, 
		&GuiDataHubI::OHT_FoupHanding));
	m_mapHandles.insert(std::make_pair(GuiHub::OhtSetPath, 
		&GuiDataHubI::OHT_SetPath));
	m_mapHandles.insert(std::make_pair(GuiHub::OhtMove, 
		&GuiDataHubI::OHT_Move));


	m_mapHandles.insert(std::make_pair(GuiHub::OhtPathTest, 
		&GuiDataHubI::OHT_PathTest));
	m_mapHandles.insert(std::make_pair(GuiHub::OhtMoveTest, 
		&GuiDataHubI::OHT_MoveTest));
	m_mapHandles.insert(std::make_pair(GuiHub::OhtFoupTest, 
		&GuiDataHubI::OHT_FoupTest));

	// for stocker
	m_mapHandles.insert(std::make_pair(GuiHub::StkStatusTime,
		&GuiDataHubI::STK_SetStatusBackTime));
	m_mapHandles.insert(std::make_pair(GuiHub::StkSetFoupInfoBackTime,
		&GuiDataHubI::STK_SetFoupInfoBackTime));
	m_mapHandles.insert(std::make_pair(GuiHub::StkHandFoup,
		&GuiDataHubI::STK_FoupHanding));
	m_mapHandles.insert(std::make_pair(GuiHub::StkInquiryStatus,
		&GuiDataHubI::STK_StockerStatus));
	m_mapHandles.insert(std::make_pair(GuiHub::StkInquiryRoom,
		&GuiDataHubI::STK_StockerRoom));
	m_mapHandles.insert(std::make_pair(GuiHub::StkInquiryStorage,
		&GuiDataHubI::STK_StockerFoupStorage));
	m_mapHandles.insert(std::make_pair(GuiHub::StkInquiryInputStatus,
		&GuiDataHubI::STK_InputStatus));
	m_mapHandles.insert(std::make_pair(GuiHub::StkFoupHistory,
		&GuiDataHubI::STK_History));
	m_mapHandles.insert(std::make_pair(GuiHub::StkAlarmHistory,
		&GuiDataHubI::STK_Alarms));
	m_mapHandles.insert(std::make_pair(GuiHub::StkGetFoupInSys,
		&GuiDataHubI::STK_GetFoupInSys));

	//////////////////////////////////////////////////////////////////////////
	// Push Data

	// for OHT
	m_mapMakePush.insert(std::make_pair(GuiHub::upOhtInfo,
		&GuiDataHubI::Push_OHT_DevInfo));
	m_mapMakePush.insert(std::make_pair(GuiHub::upOhtPos,
		&GuiDataHubI::Push_OHT_Position));

	// for stocker
	m_mapMakePush.insert(std::make_pair(GuiHub::upStkInfo,
		&GuiDataHubI::Push_STK_DevInfo));
	m_mapMakePush.insert(std::make_pair(GuiHub::upStkFoupsInfo,
		&GuiDataHubI::Push_STK_FoupInfo));
	m_mapMakePush.insert(std::make_pair(GuiHub::upStkLastOptFoup,
		&GuiDataHubI::Push_STK_LastOptFoup));
	m_mapMakePush.insert(std::make_pair(GuiHub::upStkStatus,
		&GuiDataHubI::Push_STK_Status));
	m_mapMakePush.insert(std::make_pair(GuiHub::upStkInputStatus,
		&GuiDataHubI::Push_STK_InputStatus));

	SetTimer();
}


GuiDataHubI::~GuiDataHubI(void)
{
	StopTimer();
}

std::string GuiDataHubI::ReadData(MCS::GuiHub::GuiCommand enumCmd, Ice::Int nSession, const Ice::Current &)
{
	return "readData";
}

Ice::Int GuiDataHubI::WriteData(MCS::GuiHub::GuiCommand enumCmd, 
	const std::string &strVal, Ice::Int nSession, const Ice::Current & current)
{
	HANDLE_MAP::iterator it = m_mapHandles.find(enumCmd);
	if (it != m_mapHandles.end())
	{
		(this->*it->second)(strVal, current);
		return 0;
	}
	else
	{
		return -1;
	}
}

int GuiDataHubI::SetPushCmd(const ::MCS::GuiDataUpdaterPrx& guicli, 
	const ::MCS::GuiHub::GuiPushDataList& pushlist, ::Ice::Int, const ::Ice::Current& )
{
	WLock(m_rwUpdaterSet)
	{
		MAP_UPDATER::iterator it = m_mapUpdater.find(guicli);
		if (it != m_mapUpdater.end())
		{
			it->second->m_listPushData = pushlist;
		}
	}
	return 0;
}

int GuiDataHubI::SetPushTimer(::Ice::Int nPeriord, ::Ice::Int, const ::Ice::Current& )
{
	WLock(m_rwTimerSet)
	{
		m_nTimerPeriord = nPeriord;
		StopTimer();
		SetTimer();
	}

	return 0;
}

void GuiDataHubI::SetTimer()
{
	if (0 == m_nTimerID)
	{
		m_nTimerID = timeSetEvent(
			m_nTimerPeriord,
			50,
			&GuiDataHubI::TimerHandler,
			(DWORD)this,
			TIME_PERIODIC);
	}
	
}

void GuiDataHubI::StopTimer()
{
	if (m_nTimerID > 0)
	{
		timeKillEvent(m_nTimerID);
		m_nTimerID = 0;
	}
	
}
void CALLBACK GuiDataHubI::TimerHandler(UINT id, UINT msg, DWORD dwUser, DWORD dw1, DWORD dw2)
{
	GuiDataHubI *pHub = reinterpret_cast<GuiDataHubI*>(dwUser);
	pHub->OnTimer();
}

void GuiDataHubI::OnTimer()
{
	RLock(m_rwUpdaterSet)
	{
		for(MAP_UPDATER::iterator p = m_mapUpdater.begin(); p != m_mapUpdater.end(); ++p)
		{
			GuiHub::GuiPushDataList list = p->second->m_listPushData;
			GuiDataUpdaterPrx guicli = p->second->client;
			for (GuiHub::GuiPushDataList::iterator it = list.begin();
				it != list.end(); ++it)
			{
				GuiDataItem dPush;
				MAP_MAKEPUSH::iterator itMakePush = m_mapMakePush.find(*it);
				if (itMakePush != m_mapMakePush.end())
				{
					dPush = (this->*itMakePush->second)();
				}
				else
				{
					continue;
				}
				PushDatatoCli(guicli, dPush.enumTag, dPush.sVal);
			}
		}
	}
}

void GuiDataHubI::SetDataUpdater(const ::MCS::GuiDataUpdaterPrx& updater, const ::Ice::Current& current/* = ::Ice::Current */)
{
	WLock(m_rwUpdaterSet)
	{
		MAP_UPDATER::iterator it = m_mapUpdater.find(updater);
		if (it != m_mapUpdater.end())
		{
			return;
		}

		ClientInfo* pInfo = new ClientInfo();
		pInfo->client = updater;
		pInfo->m_ptrCon = current.con;
		m_mapUpdater.insert(std::make_pair(updater, pInfo));
	}
}

void GuiDataHubI::EraseDataUpdater(const ::MCS::GuiDataUpdaterPrx& updater, const ::Ice::Current& )
{
	removeUpdater(updater);
}

void GuiDataHubI::removeUpdater(const ::MCS::GuiDataUpdaterPrx& updater)
{
	WLock(m_rwUpdaterSet)
	{
		if (m_mapUpdater.size() <= 0)
		{
			return;
		}

		MAP_UPDATER::iterator it = m_mapUpdater.find(updater);
		if (it != m_mapUpdater.end())
		{
			delete it->second;
			m_mapUpdater.erase(it);
		}
	}
}
void GuiDataHubI::PushDatatoCli(const ::MCS::GuiDataUpdaterPrx& guicli, MCS::GuiHub::PushData eTag, const std::string &sVal)
{
	try
	{
		UpdateCallbackPtr cb = new UpdateCallback();
		cb->client = guicli;
		cb->m_view = this;
		GuiDataItem item;
		item.enumTag =eTag;
		item.sVal = sVal;
		__time64_t time;
		_time64(&time);
				
		guicli->begin_UpdateData(time, item,
			newCallback_GuiDataUpdater_UpdateData(cb, &UpdateCallback::response, 
			&UpdateCallback::exception));
	}
	catch(const Ice::Exception& ex)
	{
		cout << "Update data ex: " << ex.ice_name() << endl;
	}
}

void GuiDataHubI::UpdateDataAll(MCS::GuiHub::PushData nTag, const std::string &sVal)
{
	RLock(m_rwUpdaterSet)
	{
		for(MAP_UPDATER::iterator p = m_mapUpdater.begin(); p != m_mapUpdater.end(); ++p)
		{
			PushDatatoCli(p->second->client, nTag, sVal);
		}
	}
}

void GuiDataHubI::UpdateDataOne(const Ice::ConnectionPtr ptrCon, MCS::GuiHub::PushData nTag, const std::string &sVal)
{
	RLock(m_rwUpdaterSet)
	{
		for(MAP_UPDATER::iterator p = m_mapUpdater.begin(); p != m_mapUpdater.end(); ++p)
		{
			if (p->second->m_ptrCon.get() == ptrCon.get())
			{
				PushDatatoCli(p->second->client, nTag, sVal);
			}
		}
	}
}
