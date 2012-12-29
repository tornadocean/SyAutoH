// VirualAMHS.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "VirtualAMHS.h"
#include "VirtualOHT.h"
#include "VirtualStocker.h"
#include "../shared/ThreadLock.h"
// 这是已导出类的构造函数。
// 有关类定义的信息，请参阅 VirualAMHS.h
const int OHT_START_POS = 1000;


CVirtualAMHS::CVirtualAMHS()
{
	m_mapOHT = new MAP_VOHT();
	m_mapSTK = new MAP_VSTK();
	return;
}

rwmutex g_rwLOHT;


rwmutex g_rwLStocker;

CVirtualAMHS::~CVirtualAMHS()
{
	MAP_VOHT::iterator it;

	WLock(g_rwLOHT)
	{
		it = m_mapOHT->begin();
		while(it != m_mapOHT->end())
		{
			delete it->second;
			++it;
		}
		delete m_mapOHT;
		m_mapOHT = NULL;
	}

	MAP_VSTK::iterator it_stk;
	it_stk = m_mapSTK->begin();
	while(it_stk != m_mapSTK->end())
	{
		delete it_stk->second;
		++it_stk;
	}
	delete m_mapSTK;
}

int CVirtualAMHS::Stocker_Auth(int nIndex, const char* sIP)
{
	MAP_VSTK::iterator it;
	it = m_mapSTK->find(nIndex);
	if (it != m_mapSTK->end())
	{
		VirtualStocker* stocker = it->second;
		if (stocker->Online() == false)
		{
			delete stocker;
			m_mapSTK->erase(it);
		}
		else
		{
			printf("Stocker %d already online. \n", stocker->DeviceID());
			return 0;
		}
	}

	VirtualStocker* stocker = new VirtualStocker();
	stocker->DeviceID(nIndex);
	stocker->Connect("127.0.0.1", 9999);
	stocker->Auth(sIP);
	WLock(g_rwLStocker)
	{
		(*m_mapSTK)[nIndex] = stocker;
	}
	return 0;
}

int CVirtualAMHS::OHT_Auth(int nIndex, DWORD nPos, int nHand)
{
	MAP_VOHT::iterator it;
	it = m_mapOHT->find(nIndex);
	if (it != m_mapOHT->end())
	{
		VirtualOHT* oht = it->second;
		if (oht->Online() == false)
		{
			delete oht;
			m_mapOHT->erase(it);
		}
		else
		{
			printf("OHT %d already online. \n", oht->DeviceID());
			return 0;
		}
	}

	VirtualOHT* oht = new VirtualOHT();
	oht->DeviceID(nIndex);
	oht->Connect("127.0.0.1", 9999);
	oht->Auth(nPos, nHand);
	oht->m_nHand = nHand;
	oht->m_nPos = nPos;
	WLock(g_rwLOHT)
	{
		(*m_mapOHT)[nIndex] = oht;
	}
	return 0;
}
int CVirtualAMHS::OHT_Init(int nIndex, int posTime, int statusTime)
{
	MAP_VOHT::iterator it;
	it = m_mapOHT->find(nIndex);
	if(it == m_mapOHT->end())
		return 0;
	it->second->m_nPosUpdateTimeSet = posTime;
	it->second->m_nStatusUpdateTimeSet = statusTime;
	return 0;
}
int CVirtualAMHS::OHT_SetConstSpeed(int nSpeed,int nIndex)
{
	MAP_VOHT::iterator it;
	if(nIndex >= 0)
	{
		it = m_mapOHT->find(nIndex);
		it->second->m_nSpeed = nSpeed;
		return 0;
	}
	else
	{
		for(it = m_mapOHT->begin();it != m_mapOHT->end();it++)
		{
			it->second->m_nSpeed = nSpeed;
		}
		return 0;
	}
}
int CVirtualAMHS::OHT_Offline(int nIndex)
{
	MAP_VOHT::iterator it;
	it = m_mapOHT->find(nIndex);
	if (it != m_mapOHT->end())
	{
		VirtualOHT* oht = it->second;
		if (oht->Online() == true)
		{
			oht->DestoryTimer();
			oht->Close();
		}
		else
		{
			printf("OHT %d not closed. \n", oht->DeviceID());
			return 0;
		}
	}
	return 0;
}

int CVirtualAMHS::Stocker_Offline(int nIndex)
{
	MAP_VSTK::iterator it;
	it = m_mapSTK->find(nIndex);
	if (it != m_mapSTK->end())
	{
		VirtualStocker* stocker = it->second;
		if (stocker->Online() == true)
		{
			stocker->DestoryTimer();
			stocker->Close();
		}
		else
		{
			printf("Stocker %d not closed. \n", stocker->DeviceID());
			return 0;
		}
	}
	return 0;
}

LIST_FOUP CVirtualAMHS::Stocker_GetFoupsStatus(int nStocker)
{
	LIST_FOUP list;
	

	return list;
}

int CVirtualAMHS::SetTeachPosition(int nID, int nPos, int nType, int nSpeedRate)
{
	RLock(g_rwLOHT)
	{
		MAP_VOHT::iterator it = m_mapOHT->find(nID);
		if (it != m_mapOHT->end())
		{
			it->second->SetTeachPosition(nPos, nType, nSpeedRate);
		}
	}
	
	return 0;
}

LIST_OHT CVirtualAMHS::OHT_GetStatus()
{
	LIST_OHT list;
	RLock(g_rwLOHT)
	{
		for (MAP_VOHT::iterator it = m_mapOHT->begin(); 
			it != m_mapOHT->end(); ++it)
		{
			VirtualOHT *vOht = it->second;
			ItemOHT item;
			item.nID = vOht->DeviceID();
			if (vOht->Online() == true)
			{
				item.nOnline = 1;
			}
			else
			{
				item.nOnline = 0;
			}
			item.nHandStatus = vOht->m_nHand;
			item.nPosition = vOht->m_nPos;
			item.nPosTime = vOht->m_nPosUpdateTimeSet;
			item.nStatusTime = vOht->m_nStatusUpdateTimeSet;
			list.push_back(item);
		}
	}
	return list;
}
LIST_STOCKER CVirtualAMHS::Stocker_GetInfo()
{
	LIST_STOCKER list;
	RLock(g_rwLOHT)
	{
		for(MAP_VSTK::iterator it = m_mapSTK->begin();
			it != m_mapSTK->end();++it)
		{
			VirtualStocker *vStocker = it->second;
			ItemStocker item;
			item.nID = vStocker->DeviceID();
			item.nContain = vStocker->m_nContain;
			if (vStocker->Online() == true)
			{
				item.nOnline = 1;
			}
			else
			{
				item.nOnline = 0;
			}
			item.nContain = vStocker->m_nContain;
			list.push_back(item);
		}
	}
	return list;
}
/*
int CVirtualAMHS::STK_SetFoupNum(int nIndex,int nContain)
{
	MAP_VSTK::iterator it;
	it = m_mapSTK->find(nIndex);
	if(it != m_mapSTK->end())
	{
		it->second->m_nContain = nContain;
	}
	return 0;
}

int CVirtualAMHS::STK_GetFoup(int nSTK_ID,int nFoupID,int nBatchID)
{
	MAP_VSTK::iterator it;
	it = m_mapSTK->find(nSTK_ID);
	if(it != m_mapSTK->end())
	{
		it->second->FoupIntoRoom(nFoupID,nBatchID);
	}
	return 0;
}
*/
int CVirtualAMHS::STK_GetRoomID(int nSTK_ID,int nFoupID)
{
	MAP_VSTK::iterator it;
	it = m_mapSTK->find(nSTK_ID);
	if(it != m_mapSTK->end())
	{
		int roomID = it->second->GetRoomID(nFoupID);
		return roomID;
	}
	else
		return -1;
}

int CVirtualAMHS::STK_FoupChangeType(int nStockerID)
{
	MAP_VSTK::iterator it;
	it = m_mapSTK->find(nStockerID);
	if(it != m_mapSTK->end())
	{
		int nType = it->second->m_nFoupChange;
		it->second->m_nFoupChange = 0;
		return nType;
	}
	else
		return 0;
}

int CVirtualAMHS::STK_SetContain(int nStockerID,int nContain)
{
	MAP_VSTK::iterator it;
	it = m_mapSTK->find(nStockerID);
	if(it != m_mapSTK->end())
	{
		it->second->m_nContain = nContain;
	}
	return 0;
}

int CVirtualAMHS::STK_GetSTKID()
{
	MAP_VSTK::iterator it;
	for(it = m_mapSTK->begin();it != m_mapSTK->end();it++)
	{
		if(it->second->m_nFoupChange != 0)
			return it->second->DeviceID();
	}
	return -1;
}

ItemFoup CVirtualAMHS::STK_GetChangedFoup(int nStockerID)
{
	MAP_VSTK::iterator it;
	it = m_mapSTK->find(nStockerID);
	ItemFoup item;
	if(it != m_mapSTK->end())
	{
		
		item.nID = it->second->CFoup.nID;
		item.nBatchID = it->second->CFoup.nBatchID;
		item.nRoomID = it->second->CFoup.nRoomID;
		item.nProcessStatus = 0;
		return item;
	}
	item.nBatchID = 0;
	return item;
}

int CVirtualAMHS::STK_History(int nStocker)
{
	MAP_VSTK::iterator it;
	it = m_mapSTK->find(nStocker);
	if (it != m_mapSTK->end())
	{
		VirtualStocker* stocker = it->second;
		stocker->History();
	}
	return 0;
}

int CVirtualAMHS::STK_AuthFoup(int nStocker)
{
	MAP_VSTK::iterator it;
	it = m_mapSTK->find(nStocker);
	if(it != m_mapSTK->end())
	{
		VirtualStocker* stocker = it->second;
		stocker->UpdateFoupInfo();
	}
	return 0;
}

int CVirtualAMHS::Stocker_ManualInputFoup(int nStocker,const TCHAR* sFoupID,int nBatchID)
{
	MAP_VSTK::iterator it;
	it = m_mapSTK->find(nStocker);
	if (it != m_mapSTK->end())
	{
		VirtualStocker* stocker = it->second;
		int nFoupID = _wtoi(sFoupID);
		stocker->ManualInputFoup(nFoupID,nBatchID);
	}
	else
	{
		printf("Stocker %d is offline \n", nStocker);
		return -1;
	}
	return 0;
}

int CVirtualAMHS::Stocker_ManualOutputFoup(int nStocker,const TCHAR* sFoupID)
{
	MAP_VSTK::iterator it;
	it = m_mapSTK->find(nStocker);
	if (it != m_mapSTK->end())
	{
		VirtualStocker* stocker = it->second;
		int nFoupID = _wtoi(sFoupID);
		stocker->ManualOutputFoup(nFoupID);
	}
	else
	{
		printf("Stocker %d is offline \n", nStocker);
		return -1;
	}
	return 0;
}

int CVirtualAMHS::STK_FoupInitRoom(int nStockerID,ItemFoup *pFoup)
{
	MAP_VSTK::iterator it;
	it = m_mapSTK->find(nStockerID);
	if(it != m_mapSTK->end())
	{
		it->second->InitRoom(pFoup->nID,pFoup->nBatchID,pFoup->nRoomID);
		it->second->AuthFoup(pFoup->nID,0);
	}
	return 0;
}