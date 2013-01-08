#include "StdAfx.h"
#include "VirtualOHT.h"
#include <MMSystem.h>

#pragma comment(lib, "winmm.lib")

VirtualOHT::VirtualOHT(void)
	: m_nPos(0),
	m_nHand(0),
	m_nPosUpdateTimeSet(0),
	m_nStatusUpdateTimeSet(0),
	m_nSpeed(100),
	m_nTimerID(0),
	m_nTimeCounter(0),
	isSetPath(false),
	isMove(false),
	isStop(false),
	m_nHandChange(-1)
{
	m_optHanders.insert(std::make_pair(OHT_MCS_ACK_AUTH, 
		&VirtualOHT::Handle_Auth));
	m_optHanders.insert(std::make_pair(OHT_MCS_POSITION_BACK_TIME, 
		&VirtualOHT::Handle_SetPosTime));
	m_optHanders.insert(std::make_pair(OHT_MCS_STATUS_BACK_TIME, 
		&VirtualOHT::Handle_SetStatusTime));
	m_optHanders.insert(std::make_pair(OHT_MCS_FOUPHANDING, 
		&VirtualOHT::Handle_FoupHanding));
	m_optHanders.insert(std::make_pair(OHT_MCS_PATH,
		&VirtualOHT::Handle_SetPath));
	m_optHanders.insert(std::make_pair(OHT_MCS_MOVE,
		&VirtualOHT::Handle_Move));
	CreateTimer();
}

VirtualOHT::~VirtualOHT(void)
{
	DestoryTimer();
}

int VirtualOHT::SetTeachPosition(uint32 nPos, uint8 nType, uint8 nSpeedRate)
{
	AMHSPacket teachPos(OHT_TEACH_PATH, 7);
	teachPos << uint8(DeviceID());
	teachPos << uint32(nPos);
	teachPos << uint8(nType);
	teachPos << uint8(nSpeedRate);

	SendPacket(teachPos);
	return 0;
}
int VirtualOHT::AskPath()
{
	AMHSPacket askForPath(OHT_NEED_PATH,1);
	askForPath << uint8(DeviceID());
	SendPacket(askForPath);
	m_nStatus = 1;
	return 0;
}

int VirtualOHT::Auth( uint32 nPos, int nHand)
{
	AMHSPacket authPacket(OHT_AUTH, 4);
	authPacket<< uint8(DeviceID());		// device id
	authPacket<< uint32(nPos);		// oht location
	authPacket<< uint8(nHand);				// oht hand status;
	m_nStatus = 1;

	SendPacket(authPacket);

	return 0;
}

int VirtualOHT::UpdateStatus()
{
	AMHSPacket authPacket(OHT_STATUS, 4);
	authPacket<< uint8(DeviceID());		// device id
	authPacket<< uint8(1);
	authPacket<< uint8(m_nStatus);		
	authPacket<< uint8(m_nHand);		
	SendPacket(authPacket);
	return 0;
}

int VirtualOHT::UpdatePos()
{
	AMHSPacket authPacket(OHT_POSITION, 3);
	authPacket<< uint8(DeviceID());		// device id
	authPacket<< uint32(m_nPos);		// oht location
	SendPacket(authPacket);

	return 0;
}

void VirtualOHT::HandleCommand(AMHSPacket& packet)
{
	OPT_MAP::iterator it = m_optHanders.find(packet.GetOpcode());
	if (it != m_optHanders.end())
	{
		(this->*it->second)(packet);
	}
}

void VirtualOHT::Handle_SetStatusTime(AMHSPacket& packet)
{
	uint8 nID = 0;
	uint8 nTime = 0;
	packet >> nID;
	packet >> nTime;
	m_nStatusUpdateTimeSet = nTime * 16;

	AMHSPacket authPacket(OHT_ACK_STATUS_BACK_TIME, 2);
	authPacket<< uint8(DeviceID());		// device id
	authPacket<< uint8(nTime);	
	SendPacket(authPacket);
}

void VirtualOHT::Handle_FoupHanding(AMHSPacket& packet)
{
	uint8 nID = 0;
	uint8 nBuff = 0;
	uint8 nOpt = 0;
	packet >> nID;
	packet >> nBuff;
	packet >> nOpt;
	if (1 == nOpt)
	{
		m_nHand = 0;
		m_nHandChange = 1;
		m_nOHT.nOHT_ID = (int)(DeviceID());
		m_nOHT.nPosition = m_nPos;
	}
	else
	{
		m_nHand = 1;
		m_nHandChange = 0;
		m_nOHT.nOHT_ID = (int)(DeviceID());
		m_nOHT.nPosition = m_nPos;
	}

	m_nStatus = 5;
	Sleep(5000); // hand operation time

	AMHSPacket authPacket(OHT_ACK_FOUP, 2);
	authPacket<< uint8(DeviceID());		// device id
	authPacket<< uint8(0);
	SendPacket(authPacket);
	m_nStatus = 6;
}

void VirtualOHT::Handle_SetPosTime(AMHSPacket&  packet)
{
	uint8 nID = 0;
	uint8 nPosTime = 0;
	packet >> nID;
	packet >> nPosTime;
	m_nPosUpdateTimeSet = nPosTime * 16;

	AMHSPacket authPacket(OHT_ACK_POSITION_BACK_TIME, 2);
	authPacket<< uint8(DeviceID());		// device id
	authPacket<< uint8(nPosTime);	
	SendPacket(authPacket);
}
void VirtualOHT::Handle_SetPath(AMHSPacket& packet)
{
	uint8 nID = 0;
	uint8 nEType = 0;
	uint32 nStartPos = 0;
	uint32 nEndPos = 0;
	uint8 nKeyPosNum = 0;
	uint32 nKeyPos = 0;
	uint8 nKeyPosType = 0;
	uint8 nKeyPosSpeed = 0;
	packet >> nID;
	packet >> nEType;
	packet >> nStartPos;
	packet >> nEndPos;
	packet >> nKeyPosNum;
	for(int i = 1;i <= nKeyPosNum;i++)
	{
		PathInfo Item;
		packet >> nKeyPos;
		packet >> nKeyPosType;
		packet >> nKeyPosSpeed;
		Item.nposition = nKeyPos;
		Item.nType = nKeyPosType;
		Item.nSpeed = nKeyPosSpeed;
		m_listPath.push_back(Item);
	}
	/*
	if(nEType == 0)
		isMove = true;
	*/
	AMHSPacket authPacket(OHT_ACK_PATH, 2);
	if(!m_listPath.empty())
	{
		isSetPath = true;
		m_nPos = nStartPos;
		authPacket << uint8(DeviceID());
		authPacket << uint8(0);
		isStop = false;
		m_nStatus = 2;
	}
	else
	{
		authPacket << uint8(DeviceID());
		authPacket << uint8(1);
	}
	SendPacket(authPacket);
}
void VirtualOHT::Handle_Auth(AMHSPacket& packet)
{
	uint8 nID = 0;
	uint8 nAuthRes = 0;
	packet >> nID;
	packet >> nAuthRes;
	m_isOnline = nAuthRes > 0 ? true : false;
	printf("OHT %d Auth %d\n", nID, nAuthRes);
}
void VirtualOHT::Handle_Move(AMHSPacket& packet)
{
	uint8 nID = 0;
	uint8 nMoveID = 0;
	AMHSPacket authPacket(OHT_ACK_MOVE,3);
	packet >> nID;
	packet >> nMoveID;
	switch(nMoveID)
	{
	case(0):
		if(m_listPath.empty())
		{
			authPacket << uint8(DeviceID());
			authPacket << uint8(3);
			authPacket << uint8(2);
			SendPacket(authPacket);
			AskPath();
			return;
		}
		if(isStop == true)
		{
			authPacket << uint8(DeviceID());
			authPacket << uint8(3);
			authPacket << uint8(2);
			SendPacket(authPacket);
		}
		else
		{
			isMove = true;
			authPacket << uint8(DeviceID());
			authPacket << uint8(0);
			authPacket << uint8(0);
			SendPacket(authPacket);
		}
		break;
	case(1):
		isMove = false;
		authPacket << uint8(DeviceID());
		authPacket << uint8(1);
		authPacket << uint8(0);
		SendPacket(authPacket);
		break;
	case(2):
		isStop = true;
		authPacket << uint8(DeviceID());
		authPacket << uint8(2);
		authPacket << uint8(0);
		SendPacket(authPacket);
		break;
	}
}

void VirtualOHT::CreateTimer(void)
{
	// Create a periodic timer
	timeBeginPeriod(1);
	m_nTimerID = timeSetEvent(
		1,
		1,
		&VirtualOHT::TimerHandler,
		(DWORD)this,
		TIME_PERIODIC);
	printf("ID: %u TimerID: %d\n", DeviceID(), m_nTimerID);
}

void VirtualOHT::DestoryTimer(void)
{
	if (m_nTimerID > 0)
	{
		timeKillEvent(m_nTimerID);
		timeEndPeriod(1);
		m_nTimerID = 0;
	}
}

void CALLBACK VirtualOHT::TimerHandler(UINT id, UINT msg, DWORD dwUser, DWORD dw1, DWORD dw2)
{
	VirtualOHT *pOht = (VirtualOHT*)dwUser;
	pOht->OnTimer();
}

void VirtualOHT::OnTimer(void)
{
	m_nTimeCounter++;
	if(isStop)
	{
		m_listPath.clear();
		return;
	}
	if(isMove)
	{	
		DWORD nEndPos;
		int nSpeed;
		PATH_SET_LIST::iterator it;
		it = m_listPath.end();
		it--;
		nEndPos = it->nposition;
		if(m_nPos < nEndPos)
		{
			m_nStatus = 3;
			for(it = m_listPath.begin();it != m_listPath.end();)
			{
				if((it->nposition) > ((++it)->nposition))
				{
					if(m_nTimeCounter % 2000 == 0)
						m_nPos = it->nposition;
					break;
				}
				it--;
				if((m_nPos >= (DWORD)it->nposition) && (m_nPos < (DWORD)(++it)->nposition))
				{		
					it--;		
					nSpeed = it->nSpeed;
					if(nSpeed == 0)
						nSpeed = (++it)->nSpeed;
					int speed = (nSpeed * m_nSpeed) / 100;
					if(speed == 0)
						speed = 1;
					if(m_nTimeCounter % (1000 / speed) == 0)
						m_nPos++;		
					break;	
				}
			}	
		}	
		else
		{	
			m_nPos = nEndPos;
			isMove = false;	
			m_nStatus = 4;
		}
	}

	/*
		else
		{
			m_nPos += 1;
			if (m_nPos > 12400)
			{
				m_nPos = 0;	
			}	
		}
	*/
		if ((m_nPosUpdateTimeSet > 0) 
			&& (m_nTimeCounter % m_nPosUpdateTimeSet == 0))
		{
			UpdatePos();
		}
		if ((m_nStatusUpdateTimeSet > 0)
			&& (m_nTimeCounter % m_nStatusUpdateTimeSet == 0))
		{
			UpdateStatus();
		}
}
