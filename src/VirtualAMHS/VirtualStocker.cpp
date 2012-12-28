#include "StdAfx.h"
#include "VirtualStocker.h"
#include <MMSystem.h>

VirtualStocker::VirtualStocker(void)
{
	//m_optHanders[STK_MCS_ACK_AUTH] = &VirtualStocker::Handle_Auth;
	m_nContain = 0;
	m_nFoupChange = 0;
    m_nWholeRoomStatus = 0;
	m_nTimerID = 0;
	m_nTimeCounter = 0;
	m_nMoveStatus = 0;
	m_nMoveStatusTimeSet = 0;
	m_nFoupInfoTimeSet = 0;
	ItemRoom InitRoom;
	InitRoom.nFoupID = 0;
	InitRoom.nStatus = 0;
	for(int i = 0;i < 142;i++)
	{
		m_mapRooms.insert(std::make_pair(i,InitRoom));
	}
	m_optHanders.insert(std::make_pair(STK_AUTH,
		 &VirtualStocker::Handle_Auth));
	m_optHanders.insert(std::make_pair(STK_MCS_FOUP,
		&VirtualStocker::Handle_FoupOperate));
	m_optHanders.insert(std::make_pair(STK_MCS_STATUS,
		&VirtualStocker::Handle_StatusQuery));
	m_optHanders.insert(std::make_pair(STK_MCS_ROOM,
		&VirtualStocker::Handle_RoomQuery));
	m_optHanders.insert(std::make_pair(STK_MCS_STORAGE,
		&VirtualStocker::Handle_StorageQuery));
	m_optHanders.insert(std::make_pair(STK_MCS_INPUT_STATUS,
		&VirtualStocker::Handle_InputStatus));
	m_optHanders.insert(std::make_pair(STK_MCS_HISTORY,
		&VirtualStocker::Handle_History));
	m_optHanders.insert(std::make_pair(STK_MCS_ALARMS,
		&VirtualStocker::Handle_Alarms));
	m_optHanders.insert(std::make_pair(STK_MCS_STATUS_BACK_TIME,
		&VirtualStocker::Handle_StatusBackTime));
	m_optHanders.insert(std::make_pair(STK_MCS_FOUP_BACK_TIME,
		&VirtualStocker::Handle_FoupBackTime));
	m_optHanders.insert(std::make_pair(STK_MCS_ACK_AUTH,
		&VirtualStocker::Handle_AuthBack));
	CreateTimer();
}

VirtualStocker::~VirtualStocker(void)
{
	DestoryTimer();
}

void VirtualStocker::HandleCommand(AMHSPacket& packet)
{
	OPT_MAP::iterator it = m_optHanders.find(packet.GetOpcode());
	if (it != m_optHanders.end())
	{
		(this->*it->second)(packet);
	}
}

int VirtualStocker::Auth( const char* sIP)
{
	AMHSPacket authPacket(STK_AUTH, 8);
	authPacket<< (uint8)(DeviceID());		// stokcer id
	uint32 uIP = 0;
	uIP = inet_addr(sIP);
	authPacket << (uint32)uIP;
	authPacket << (uint8)(142);
	authPacket << (uint8)(5);
	authPacket << (uint8)(5);

	SendPacket(authPacket);
	return 0;
}

int VirtualStocker::UpdateStatus()
{
	AMHSPacket StatusPacket(STK_ACK_STATUS,2);
	StatusPacket << uint8(DeviceID());
	StatusPacket << uint8(m_nMoveStatus);

	SendPacket(StatusPacket);
	return 0;
}

int VirtualStocker::UpdateFoupInfo()
{
	int nBityNum = m_nContain * 5 + 2;
	AMHSPacket StoragePacket(STK_ACK_STORAGE,nBityNum);
	StoragePacket << (uint8)(DeviceID());
	StoragePacket << (uint8)(m_nContain);
	MAP_VFOUP::iterator it;
	for(it = m_mapFoups.begin();it != m_mapFoups.end();it++)
	{
		StoragePacket << (uint8)(it->second.nID);
		StoragePacket << (uint16)(it->second.nBatchID);
		StoragePacket << (uint16)(it->second.nRoomID);
	}
	SendPacket(StoragePacket);
	return 0;
}

int VirtualStocker::ManualInputFoup(int nFoupID,int nBatchID)
{
	MAP_VFOUP::iterator it;
	it = m_mapFoups.find(nFoupID);
	if (it == m_mapFoups.end())
	{
		FoupIntoRoom(nFoupID,nBatchID);
		/*
		VirtualFoup foup;
		foup.nID = nFoupID;
		foup.nStatus = 0;
		m_mapFoups.insert(std::make_pair(nFoupID, foup));
		*/
		AMHSPacket Packet(STK_FOUP_EVENT, 8);
		Packet << uint8(DeviceID());
		Packet << uint8(0); // input
		Packet << uint8(GetRoomID(nFoupID)); // slot ID
		Packet << uint16(nBatchID); // lot ID
		Packet << uint16(nFoupID); // foup ID
		Packet << uint8(5); // manual input 1

		SendPacket(Packet);
	}
	return 0;
}

int VirtualStocker::AuthFoup(int nFoupID,int nMode)
{
	MAP_VFOUP::iterator it;
	it = m_mapFoups.find(nFoupID);
	if(it != m_mapFoups.end())
	{
			AMHSPacket Packet(STK_FOUP_EVENT,8);
	        Packet << uint8(DeviceID());
	        Packet << uint8(nMode); // input
	        Packet << uint8(GetRoomID(nFoupID)); // slot ID
	        Packet << uint16(it->second.nBatchID); // lot ID
	        Packet << uint16(nFoupID); // foup ID
	        Packet << uint8(5); // manual input 1
	        SendPacket(Packet);
	        return 0;
	}
	return 0;
}

int VirtualStocker::History()
{
	printf("Test big Packet \n");
	AMHSPacket Packet(STK_ACK_HISTORY, 2500);

	for (int i=0; i<2500; i++)
	{
		Packet << uint8(i);
	}

	SendPacket(Packet);
	return 0;
}

int VirtualStocker::ManualOutputFoup(int nFoupID)
{
	MAP_VFOUP::iterator it;
	it = m_mapFoups.find(nFoupID);
	VirtualFoup pFoup;
	if(it != m_mapFoups.end())
	{
		pFoup = it->second;
	}
	AMHSPacket Packet(STK_FOUP_EVENT, 8);
	Packet << uint8(DeviceID());
	Packet << uint8(1); // output
	Packet << uint8(GetRoomID(nFoupID));
	Packet << uint16(pFoup.nBatchID); // lot
	Packet << uint16(nFoupID); // foup
	Packet << uint8(5); // manual input 1

	SendPacket(Packet);
	FoupOutRoom(nFoupID);
	return 0;
}

void VirtualStocker::Handle_Auth(AMHSPacket& packet)
{
	uint8 nID = 0;
	uint8 nAuthRes = 0;
	uint64 uTime = 0;
	packet >> nID;
	packet >> nAuthRes;
	packet >> uTime;

	m_isOnline = nAuthRes > 0 ? true : false;
	__time64_t tTime;
	memcpy(&tTime, &uTime, 8);
	char timebuf[256] = "";
	_ctime64_s(timebuf, 256, &tTime);
	printf("Stocker %d Auth %d SysTime: %s\n", nID, nAuthRes, timebuf);
}

void VirtualStocker::Handle_FoupOperate(AMHSPacket& packet)
{
 	uint8 nID = 0;
	uint8 nMode = 0;
	uint8 nPick = 0;
	uint64 nFoupData1;
	uint16 nFoupData2;
	packet >> nID;
	packet >> nMode;
	packet >> nPick;
	packet >> nFoupData1;
	packet >> nFoupData2;
	if(nMode == 0)
	{
		MAP_VFOUP::iterator it;
		MAP_VROOM::iterator ite;
		if(nPick == 0)
		{
			ite = m_mapRooms.find(nFoupData1);
			if(ite->second.nStatus == 0)
			{
				m_nContain++;
				m_nFoupChange = 1;
			    VirtualFoup pFoup;
				int nID = 0;
				for(it = m_mapFoups.begin();it != m_mapFoups.end();it++)
				{
					if(nID <= it->first)
					{
						nID = it->first;
					}
				}
				CFoup.nBatchID = pFoup.nBatchID = 0;
			    CFoup.nID = pFoup.nID = nID + 1;
			    CFoup.nRoomID = pFoup.nRoomID = nFoupData1;
			    CFoup.nStatus = pFoup.nStatus = 0;
			    m_mapFoups.insert(std::make_pair(nID + 1,pFoup));
			    ItemRoom item;
			    item.nFoupID = nID + 1;
			    item.nStatus = 1;
			    ite->second = item;
			    AMHSPacket FoupPacket(STK_ACK_FOUP,2);
			    FoupPacket << (uint8)(DeviceID());
			    FoupPacket << (uint8)(0);
			    SendPacket(FoupPacket);
				AuthFoup(CFoup.nID,0);
				
			}
		    else
			{
				AMHSPacket FoupPacket(STK_ACK_FOUP,2);
				FoupPacket << (uint8)(DeviceID());
			    FoupPacket << (uint8)(5);
			    SendPacket(FoupPacket);
		    }
		}
		else if(nPick == 1)
		{
			AMHSPacket FoupPacket(STK_ACK_FOUP,2);
			FoupPacket << (uint8)(DeviceID());
			FoupPacket << (uint8)(0);
			SendPacket(FoupPacket);
		}
		else if(nPick == 2)
		{
			it = m_mapFoups.find(nFoupData1);
			if(it == m_mapFoups.end())
			{
				m_nFoupChange = 1;
				FoupIntoRoom(nFoupData1,0);
				it = m_mapFoups.find(nFoupData1);
				CFoup = it->second;
				AMHSPacket FoupPacket(STK_ACK_FOUP,2);
			    FoupPacket << (uint8)(DeviceID());
			    FoupPacket << (uint8)(0);
			    SendPacket(FoupPacket);
				AuthFoup(CFoup.nID,0);
			}
			else
			{
				AMHSPacket FoupPacket(STK_ACK_FOUP,2);
				FoupPacket << (uint8)(DeviceID());
			    FoupPacket << (uint8)(5);
			    SendPacket(FoupPacket);
			}
		}
		else if(nPick == 3)
		{
			AMHSPacket FoupPacket(STK_ACK_FOUP,2);
		    FoupPacket << (uint8)(DeviceID());
			FoupPacket << (uint8)(0);
			SendPacket(FoupPacket);
		}
	}
	else
	{
		MAP_VFOUP::iterator it;
		MAP_VROOM::iterator ite;
		if(nPick == 0)
		{
			ite = m_mapRooms.find(nFoupData1);
			if(ite != m_mapRooms.end())
			{
				m_nContain--;
		        m_nFoupChange = 2;
				int nID = ite->second.nFoupID;
				it = m_mapFoups.find(nID);
				CFoup = it->second;
				AuthFoup(CFoup.nID,1);
		        if(it != m_mapFoups.end())
		        {
					ite->second.nStatus = 0;
					ite->second.nFoupID = 0;
					m_mapFoups.erase(it);
			        AMHSPacket FoupPacket(STK_ACK_FOUP,2);
			        FoupPacket << (uint8)(DeviceID());
			        FoupPacket << (uint8)(0);
			        SendPacket(FoupPacket);
					
				    return ;
			    }
		     }
		}
		else if(nPick == 1)
		{
			 AMHSPacket FoupPacket(STK_ACK_FOUP,2);
			 FoupPacket << (uint8)(DeviceID());
			 FoupPacket << (uint8)(0);
			 SendPacket(FoupPacket);
	         return ;
		}
		else if(nPick == 2)
		{
			it = m_mapFoups.find(nFoupData1);
			if(it != m_mapFoups.end())
			{
				CFoup = it->second;
				AuthFoup(CFoup.nID,1);
				m_nFoupChange = 2;
				m_nContain--;
				int nRoomID = it->second.nRoomID;
				ite = m_mapRooms.find(nRoomID);
				ite->second.nFoupID = 0;
				ite->second.nStatus = 0;
				m_mapFoups.erase(it);
				AMHSPacket FoupPacket(STK_ACK_FOUP,2);
			    FoupPacket << (uint8)(DeviceID());
			    FoupPacket << (uint8)(0);
			    SendPacket(FoupPacket);
				
				return ;
			}
		}
		AMHSPacket FoupPacket(STK_ACK_FOUP,2);
	    FoupPacket << (uint8)(DeviceID());
		FoupPacket << (uint8)(5);
	    SendPacket(FoupPacket);
	}
}

void VirtualStocker::Handle_StatusQuery(AMHSPacket& packet)
{
	uint8 nID = 0;
	packet >> nID;
	UpdateStatus();
}

void VirtualStocker::Handle_RoomQuery(AMHSPacket& packet)
{
	uint8 nID = 0;
	packet >> nID;
	AMHSPacket RoomPacket(STK_ACK_ROOM,143);
	m_nWholeRoomStatus = (uint8)GetRoomStatus();
	RoomPacket << (uint8)(DeviceID());
	RoomPacket << (uint8)(m_nWholeRoomStatus);
	MAP_VROOM::iterator it;
	for(it = m_mapRooms.begin();it != m_mapRooms.end();it++)
	{
		RoomPacket << (uint8)(it->second.nStatus);
	}

	SendPacket(RoomPacket);
	return;
}

void VirtualStocker::Handle_StorageQuery(AMHSPacket& packet)
{
	uint8 nID = 0;
	packet >> nID;
	UpdateFoupInfo();
	return;
}

void VirtualStocker::Handle_InputStatus(AMHSPacket& packet)
{
	uint8 nID = 0;
	packet >> nID;
	AMHSPacket InputPacket(STK_ACK_INPUT_STATUS,3);
	InputPacket << (uint8)(DeviceID());
	InputPacket << (uint8)(0);
	InputPacket << (uint8)(0);

	SendPacket(InputPacket);
	return ;
}

void VirtualStocker::Handle_History(AMHSPacket& packet)
{
	uint8 nID = 0;
	uint64 nTime1 = 0;
	uint64 nTime2 = 0;
	packet >> nID;
	packet >> nTime1;
	packet >> nTime2;
	AMHSPacket HistoryPacket(STK_ACK_HISTORY,5*4);
	HistoryPacket << (uint8)(DeviceID());
	HistoryPacket << (uint32)(3);
	for(int i = 0;i < 3;i++)
	{
		HistoryPacket << (uint8)(i);
		HistoryPacket << (uint16)(0);
		HistoryPacket << (uint16)(i);
	}

	SendPacket(HistoryPacket);
	return;
}

void VirtualStocker::Handle_Alarms(AMHSPacket& packet)
{
	uint8 nID = 0;
	uint64 nTime1 = 0;
	uint64 nTime2 = 0;
	packet >> nID;
	packet >> nTime1;
	packet >> nTime2;
	AMHSPacket AlarmPacket(STK_ACK_ALARMS,23);
	AlarmPacket << (uint8)(DeviceID());
	AlarmPacket << (uint32)(2);
	for(int i = 0;i < 6;)
	{
		AlarmPacket << (uint16)(2012);
		AlarmPacket << (uint8)(12);
		AlarmPacket << (uint8)(18);
		AlarmPacket << (uint8)(10);
		AlarmPacket << (uint8)(40);
		AlarmPacket << (uint8)(i);
		AlarmPacket << (uint8)(i);
		i += 5;
	}

	SendPacket(AlarmPacket);
}

void VirtualStocker::Handle_StatusBackTime(AMHSPacket& packet)
{
	uint8 nID = 0;
	uint32 nStatusTime = 0;
	packet >> nID;
	packet >> nStatusTime;
	m_nMoveStatusTimeSet = nStatusTime;
	AMHSPacket StatusTimePacket(STK_ACK_STATUS_TIME,2);
	StatusTimePacket << (uint8)(DeviceID());
	StatusTimePacket << (uint8)(0);

	SendPacket(StatusTimePacket);
	return ;
}

void VirtualStocker::Handle_FoupBackTime(AMHSPacket& packet)
{
	uint8 nID = 0;
	uint32 nFoupTime = 0;
	packet >> nID;
	packet >> nFoupTime;
	m_nFoupInfoTimeSet = nFoupTime;
	AMHSPacket FoupTimePacket(STK_ACK_FOUP_TIME,2);
	FoupTimePacket << (uint8)(DeviceID());
	FoupTimePacket << (uint8)(0);

	SendPacket(FoupTimePacket);
	return;
}

void VirtualStocker::Handle_AuthBack(AMHSPacket& packet)
{
	uint8 nID = 0;
	uint8 nResult = 0;
	uint16 nYear = 0;
	uint8 nMonth = 0;
	uint8 nDay = 0;
	uint8 nHour = 0;
	uint8 nMinute = 0; 
	uint8 nSecond = 0;
	packet >> nID;
	packet >> nResult;
	packet >> nYear;
	packet >> nMonth;
	packet >> nDay;
	packet >> nHour;
	packet >> nMinute;
	packet >> nSecond;
	if(nResult == 0)
	{
		printf("STOCKER ID: %d authed successful at %d:%d:%d %d-%d-%d\n",nID,nHour,nMinute,nSecond,nDay,nMonth,nYear);
	}
	else
	{
		printf("STOCKER ID: %d authed failed at %d:%d:%d %d-%d-%d\n",nID,nHour,nMinute,nSecond,nDay,nMonth,nYear);
	}
}
void VirtualStocker::Handle_FoupEvent(AMHSPacket& packet)
{
	uint8 nID;
	uint8 nResult;
	packet >> nID;
	packet >> nResult;
	if(nResult == 0)
		printf("Foup authed successful!\n");
	else
		printf("Foup authed failed!\n");
}
void VirtualStocker::CreateTimer(void)
{
	timeBeginPeriod(1);
	m_nTimerID = timeSetEvent(
		1,
		1,
		&VirtualStocker::TimerHandler,
		(DWORD)this,
		TIME_PERIODIC);
	printf("ID: %u TimerID: %d\n", DeviceID(), m_nTimerID);
}

void VirtualStocker::DestoryTimer(void)
{
	if (m_nTimerID > 0)
	{
		timeKillEvent(m_nTimerID);
		timeEndPeriod(1);
		m_nTimerID = 0;
	}
}

void CALLBACK VirtualStocker::TimerHandler(UINT id, UINT msg, DWORD dwUser, DWORD dw1, DWORD dw2)
{
	VirtualStocker *pStocker = (VirtualStocker*)dwUser;
	pStocker->OnTimer();
}

void VirtualStocker::OnTimer()
{
	m_nTimeCounter++;
	if(m_nMoveStatusTimeSet > 0 && m_nTimeCounter % (m_nMoveStatusTimeSet * 1000) == 0)
		UpdateStatus();
	if(m_nFoupInfoTimeSet > 0 && m_nTimeCounter % (m_nFoupInfoTimeSet * 1000) == 0)
		UpdateFoupInfo();
}

int VirtualStocker::GetRoomStatus()
{
	int nStatus;
	if(m_nContain == 0)
		nStatus = 1;
	else if(m_nContain == 141)
		nStatus = 2;
	else if(0 < m_nContain && m_nContain < 141)
		nStatus = 0;
	return nStatus;
}

int VirtualStocker::GetRoomID(int nFoupID)
{
	MAP_VFOUP::iterator it;
	int nRoomID;
	it = m_mapFoups.find(nFoupID);
	if(it != m_mapFoups.end())
	{
		nRoomID = it->second.nRoomID;
		return nRoomID;
	}
	else 
		return -1;
}

int VirtualStocker::InitRoom(int nFoupID,int nBatchID,int nRoomID)
{
	MAP_VFOUP::iterator it;
	it = m_mapFoups.find(nFoupID);
	if(it == m_mapFoups.end())
	{
		m_nContain++;
		VirtualFoup pFoup;
		pFoup.nBatchID = nBatchID;
		pFoup.nID = nFoupID;
		pFoup.nRoomID = nRoomID;
		pFoup.nStatus = 0;
		m_mapFoups.insert(std::make_pair(nFoupID,pFoup));
		ItemRoom item;
		item.nFoupID = nFoupID;
		item.nStatus = 1;
		MAP_VROOM::iterator ite;
		ite = m_mapRooms.find(nRoomID);
		if(ite != m_mapRooms.end())
		{
			ite->second = item;
		}
	}
	return 0;
}

int VirtualStocker::FoupOutRoom(int nID)
{
	MAP_VFOUP::iterator it;
	int nRoomID;
	it = m_mapFoups.find(nID);
	if(it != m_mapFoups.end())
	{
		nRoomID = it->second.nRoomID;
		m_mapFoups.erase(it);
		ItemRoom item;
		item.nFoupID = 0;
		item.nStatus = 0;
		MAP_VROOM::iterator ite;
		ite = m_mapRooms.find(nRoomID);
		ite->second = item;
		m_nContain--;
	}
	return 0;
}

int VirtualStocker::FoupIntoRoom(int nID,int nBatchID)
{
	MAP_VFOUP::iterator it;
	it = m_mapFoups.find(nID);
	if(it != m_mapFoups.end())
		return 0;
	if(m_nContain == 141)
	{
		printf("%s","the Room in Stocker is full!");
		return 0;
	}
	if(m_nContain == 0)
	{
		VirtualFoup pFoup;
		pFoup.nID = nID;
		pFoup.nBatchID = nBatchID;
		pFoup.nRoomID = 0;
		m_mapFoups.insert(std::make_pair(nID,pFoup));
		ItemRoom item;
		item.nFoupID = nID;
		item.nStatus = 1;
		MAP_VROOM::iterator it;
		it = m_mapRooms.find(0);
		it->second = item;
		m_nContain++;
		return 1;
	}
	else
	{
		MAP_VROOM::iterator it;
		VirtualFoup pFoup;
		ItemRoom item;
		item.nFoupID = nID;
		item.nStatus = 1;
		for(it = m_mapRooms.begin(); 
		    it != m_mapRooms.end();it++)
		{
			if(it->second.nStatus == 0)
			{
				int nRoomID = it->first;
				it->second = item;
				pFoup.nRoomID = it->first;
				break;
			}
		}
		pFoup.nID = nID;
		pFoup.nBatchID = nBatchID;
		m_mapFoups.insert(std::make_pair(nID,pFoup));
		m_nContain++;
		return 1;
	}
}
