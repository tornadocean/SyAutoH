#include "StdAfx.h"
#include "amhs_dev_server.h"
#include "../SqlAceCli/SqlAceCli.h"

amhs_room::amhs_room()
{
	Log.Init(3);

	m_optHanders.insert(std::make_pair(OHT_ACK_STATUS_BACK_TIME, 
		&amhs_room::Handle_OHT_AckStatusBackTime));
	m_optHanders.insert(std::make_pair(OHT_ACK_POSITION_BACK_TIME, 
		&amhs_room::Handle_OHT_AckPosBackTime));
	m_optHanders.insert(std::make_pair(OHT_ACK_PATH, 
		&amhs_room::Handle_OHT_AckPath));
	m_optHanders.insert(std::make_pair(OHT_ACK_MOVE, 
		&amhs_room::Handle_OHT_AckMove));
	m_optHanders.insert(std::make_pair(OHT_ACK_FOUP, 
		&amhs_room::Handle_OHT_AckFoup));
	m_optHanders.insert(std::make_pair(OHT_AUTH, 
		&amhs_room::Handle_OHT_Auth));
	m_optHanders.insert(std::make_pair(OHT_POSITION, 
		&amhs_room::Handle_OHT_Pos));
	m_optHanders.insert(std::make_pair(OHT_STATUS, 
		&amhs_room::Handle_OHT_Status));
	m_optHanders.insert(std::make_pair(OHT_TEACH_PATH,
		&amhs_room::Handle_OHT_TeachPath));

	//////////////////////////////////////////////////////////////////////////
	m_optHanders.insert(std::make_pair(STK_ACK_FOUP, 
		&amhs_room::Handle_STK_AckFoup));
	m_optHanders.insert(std::make_pair(STK_ACK_STATUS, 
		&amhs_room::Handle_STK_AckStatus));
	m_optHanders.insert(std::make_pair(STK_ACK_ROOM, 
		&amhs_room::Handle_STK_AckRoom));
	m_optHanders.insert(std::make_pair(STK_ACK_STORAGE, 
		&amhs_room::Handle_STK_AckStorage));
	m_optHanders.insert(std::make_pair(STK_ACK_INPUT_STATUS, 
		&amhs_room::Handle_STK_AckInputStatus));
	m_optHanders.insert(std::make_pair(STK_ACK_HISTORY, 
		&amhs_room::Handle_STK_AckHistory));
	m_optHanders.insert(std::make_pair(STK_ACK_ALARMS, 
		&amhs_room::Handle_STK_AckAlarms));
	m_optHanders.insert(std::make_pair(STK_AUTH, 
		&amhs_room::Handle_STK_Auth));
	m_optHanders.insert(std::make_pair(STK_FOUP_EVENT, 
		&amhs_room::Handle_STK_FoupEvent));
	m_optHanders.insert(std::make_pair(STK_ACK_STATUS_TIME, 
		&amhs_room::Handle_STK_Ack_StatusTime));
	m_optHanders.insert(std::make_pair(STK_ACK_FOUP_TIME, 
		&amhs_room::Handle_STK_Ack_FoupTime));
}

void amhs_room::join(amhs_participant_ptr participant)
{
	participants_.insert(participant);
}

void amhs_room::leave(amhs_participant_ptr participant)
{
	if (DEV_TYPE_OHT == participant->nDevType_)
	{
		WLock(rwLock_oht_map_)
		{
			amhs_oht_map::iterator it = oht_map_.find(participant->nID_);
			if (it != oht_map_.end())
			{
				oht_map_.erase(it);

			}
		}
	}
	else if (DEV_TYPE_STOCKER == participant->nDevType_)
	{
		WLock(rwLock_stocker_map_)
		{
			amhs_stocker_map::iterator it = stocker_map_.find(participant->nID_);
			if (it != stocker_map_.end())
			{
				stocker_map_.erase(it);
			}
		}
	}

	Log.Debug("amhs_room::leave", "client id: %d type: %d", participant->nID_, participant->nDevType_);
	participants_.erase(participant);
}

amhs_stocker_vec amhs_room::GetStkDataSet()
{
	amhs_stocker_vec stk_vec;
	RLock(rwLock_stocker_map_)
	{
		for (amhs_stocker_map::iterator it = stocker_map_.begin();
			it != stocker_map_.end(); ++it)
		{
			stk_vec.push_back(it->second);
		}
	}
	return stk_vec;
}

amhs_foup_vec amhs_room::GetStkFoupInSys()
{
	amhs_foup_vec foup_vec;
	RLock(rwLock_foup_map_)
	{
		for(amhs_foup_map::iterator it = foup_map_.begin();
			it != foup_map_.end(); ++it)
		{
			foup_vec.push_back(it->second);
		}
	}
	return foup_vec;
}

amhs_oht_vec amhs_room::GetOhtDataSet()
{
	amhs_oht_vec oht_vec;
	RLock(rwLock_oht_map_)
	{
		for (amhs_oht_map::iterator it = oht_map_.begin();
			it != oht_map_.end(); ++it)
		{
			oht_vec.push_back(it->second);
		}
	}
	return oht_vec;
}

amhs_foup_vec amhs_room::GetStkFoupDataSet(int nID)
{
	amhs_foup_vec foup_vec;
	RLock(rwLock_foup_map_)
	{
		amhs_stocker_map::iterator itStocker=stocker_map_.find(nID);
		for(amhs_foup_map::iterator itFoup = itStocker->second->foup_map.begin();
			itFoup != itStocker->second->foup_map.end(); ++itFoup)
		{
			foup_vec.push_back(itFoup->second);
		}
	}
	return foup_vec;
}

amhs_foup_vec amhs_room::GetStkEraseFoupDataSet(int nID)
{
	amhs_foup_vec foup_vec;
	RLock(rwLock_foup_map_)
	{
		amhs_stocker_map::iterator itStocker=stocker_map_.find(nID);
		for(amhs_foup_vec::iterator itFoup = itStocker->second->foup_erase_vec.begin();
			itFoup != itStocker->second->foup_erase_vec.end(); ++itFoup)
		{
			foup_vec.push_back(*itFoup);
		}
		itStocker->second->foup_erase_vec.clear();
	}
	return foup_vec;
}

amhs_foup_vec amhs_room::GetStkLastOptFoup(int nID)
{
	amhs_foup_vec foup_vec;
	amhs_stocker_map::iterator itStocker=stocker_map_.find(nID);
	for(amhs_foup_vec::iterator itFoup = itStocker->second->last_opt_foup_vec.begin();
		itFoup != itStocker->second->last_opt_foup_vec.end(); ++itFoup)
	{
		foup_vec.push_back(*itFoup);
	}
	return foup_vec;
}

void amhs_room::SendPacket(int nID, int nType, AMHSPacket& packet)
{
	amhs_participant_ptr pClient;
	if (DEV_TYPE_OHT == nType)
	{
		
		RLock(rwLock_oht_map_)
		{
			amhs_oht_map::iterator it = oht_map_.find(nID);
			if (it != oht_map_.end())
			{
				pClient = it->second->p_participant;
			}
		}
		if ((pClient != NULL)
			|| (254 == nID))
		{
			SendPacket(pClient, packet);
		}
	}
	else if (DEV_TYPE_STOCKER == nType)
	{
		RLock(rwLock_stocker_map_)
		{
			amhs_stocker_map::iterator it = stocker_map_.find(nID);
			if (it != stocker_map_.end())
			{
				pClient = it->second->p_participant;
			}
		}
		if ((pClient != NULL)
			|| (254 == nID))
		{
			SendPacket(pClient, packet);
		}
	}
}

void amhs_room::SendPacket(amhs_participant_ptr participants, AMHSPacket &packet)
{
	size_t szLen = packet.size();
	size_t szLimit = amhs_message::max_body_length;
	int nLoop = szLen / szLimit + 1;

	size_t szPacketLen = 0;
	for (int i=0; i<nLoop; i++)
	{
		amhs_message msg;
		msg.Index(i+1);
		if ((i+1) < nLoop)
		{
			msg.IsLast(0);
		}
		else
		{
			msg.IsLast(1);
		}
		if (szLen > szLimit)
		{
			szPacketLen = szLimit;
			szLen -= szLimit;
		}
		else
		{
			szPacketLen = szLen;
		}
		msg.command(packet.GetOpcode());
		msg.body_length(szPacketLen);
		msg.IsNeedRespond(true);
		msg.IsRespond(false);
		memcpy(msg.body(), packet.contents() + (i*szLimit), msg.body_length());
		msg.encode_header();
		//packet.hexlike();

		if (NULL == participants)
		{
			deliver_all(msg);
		}
		else
		{
			participants->deliver(msg);
		}
	}
}

void amhs_room::Handle_OHT_NeedPath(amhs_participant_ptr, AMHSPacket& Packet)
{
	uint8 ohtID = 0;
	Packet >> ohtID;

	WLock(rwLock_oht_map_)
	{
		amhs_oht_map::iterator it = oht_map_.find(ohtID);
		if (it != oht_map_.end())
		{
			it->second->bNeedPath = true;
		}
	}

}

void amhs_room::Handle_OHT_TeachPath(amhs_participant_ptr, AMHSPacket& Packet)
{
	uint8 nID = 0;
	uint32 nPos = 0;
	uint8 nType = 0;
	uint8 nSpeedRate = 0;

	Packet >> nID;
	Packet >> nPos;
	Packet >> nType;
	Packet >> nSpeedRate;

	DBKeyPoints dbKeyPts;
	dbKeyPts.SetKeyPointbyOHTTeach(nID, nPos, nType, nSpeedRate);

	//WLock(rwLock_oht_map_)
	//{
	//	amhs_oht_map::iterator it = oht_map_.find(nID);
	//	if (it != oht_map_.end())
	//	{
	//		//it->second->bNeedPath = true;
	//	}
	//}

	// TODO: write key points to DB


}

void amhs_room::Handle_STK_AckFoup(amhs_participant_ptr, AMHSPacket& Packet)
{
	uint8 nID = 0;
	uint8 nResult = 0;
	Packet >> nID; 
	Packet >> nResult;
	LOG_DEBUG("ID: %d, RESULT:%d", nID, nResult);
}

void amhs_room::Handle_STK_Ack_StatusTime(amhs_participant_ptr, AMHSPacket& Packet)
{
	uint8 nID = 0;
	uint8 nResult = 0;
	Packet >> nID; 
	Packet >> nResult;
	LOG_DEBUG("ID: %d, RESULT:%d", nID, nResult);
}

void amhs_room::Handle_STK_Ack_FoupTime(amhs_participant_ptr, AMHSPacket& Packet)
{
	uint8 nID = 0;
	uint8 nResult = 0;
	Packet >> nID; 
	Packet >> nResult;
	LOG_DEBUG("ID: %d, RESULT:%d", nID, nResult);
}

void amhs_room::Handle_STK_AckStatus(amhs_participant_ptr, AMHSPacket& Packet)
{
	uint8 nID=0;
	uint8 nStatus=0;
	Packet >> nID;
	Packet >> nStatus;

	WLock(rwLock_stocker_map_)
	{
		amhs_stocker_map::iterator itStocker=stocker_map_.find(nID);
		if(itStocker != stocker_map_.end())
		{
			itStocker->second->nStatus = nStatus;
		}
	}
	Log.Warning("amhs_room", "Packet handle not implemented\n");
}
void amhs_room::Handle_STK_AckRoom(amhs_participant_ptr, AMHSPacket& Packet)
{
	uint8 nID = 0;
	uint8 nStats = 0;
	uint8 item[141] = {0};

	size_t pkSize = Packet.size();
	LOG_DEBUG("Packet Size: %d", pkSize);
	Packet >> nID;
	Packet >> nStats;
	for (int i=0; i<140; i++)
	{
		uint8 uItem = 0;
		Packet >> uItem;
		item[i] = uItem;
	}

	LOG_DEBUG("ID:%d, STATUS: %d", nID, nStats);
	for (int i=0; i<141; i++)
	{
		printf("%d ", item[i]);
		if (i % 16 == 0)
		{
			printf("\r\n");
		}
	}
	LOG_DEBUG("Room end");
}
void amhs_room::Handle_STK_AckStorage(amhs_participant_ptr, AMHSPacket& Packet)
{
	Log.Warning("amhs_room", "Packet handle not implemented\n");
}
void amhs_room::Handle_STK_AckInputStatus(amhs_participant_ptr, AMHSPacket& Packet)
{
	uint8 nID = 0;
	uint8 nAuto = 0;
	uint8 nManu = 0;
	Packet >> nID;
	Packet >> nAuto;
	Packet >> nManu;

	WLock(rwLock_stocker_map_)
	{
		amhs_stocker_map::iterator itStocker=stocker_map_.find(nID);
		if(itStocker != stocker_map_.end())
		{
			itStocker->second->nAuto=nAuto;
			itStocker->second->nManu=nManu;
		}
	}
	Log.Warning("amhs_room", "Packet handle not implemented\n");
}
void amhs_room::Handle_STK_AckHistory(amhs_participant_ptr, AMHSPacket& Packet)
{
	size_t szLen = Packet.size();
	Log.Warning("amhs_room", "Packet handle not implemented.");
	Log.Debug("amhs_romm", "Stocker history packet len %d", szLen);
}
void amhs_room::Handle_STK_AckAlarms(amhs_participant_ptr, AMHSPacket& Packet)
{
	uint8 nID = 0;
	uint32 nCount = 0;
	Packet >> nID;
	Packet >> nCount;
	printf("ID: %u Alarms Count: %u \n", nID, nCount);
	for (uint32 i=0; i<nCount; i++)
	{
		uint16 year = 0;
		uint8   m = 0;
		uint8  day = 0;
		uint8 h = 0;
		uint8 nMin = 0;
		uint8 nSec = 0;
		uint8 nNull = 0;
		uint8 nAlarm = 0;

		Packet >> year;
		Packet >> m;
		Packet >> day;
		Packet >> h;
		Packet >> nMin;
		Packet >> nSec;
		Packet >> nNull;
		Packet >> nAlarm;

		printf("%u-%u-%u %u:%u:%u Alarm: %u \n", year, m, day,
			h, nMin, nSec, nAlarm);
	}
}

void amhs_room::Handle_STK_FoupEvent(amhs_participant_ptr participants, AMHSPacket& Packet)
{
	uint8 nID = 0;
	uint8 nChaned = 0;
	uint8 foupRoom = 0;
	uint16 foupLot = 0;
	uint16 foupBarCode = 0;
	uint8 nInput = 0;
	Packet >> nID;
	Packet >> nChaned;
	Packet >> foupRoom;
	Packet >> foupLot;
	Packet >> foupBarCode;
	Packet >> nInput;


	printf("Foup Event  ---> stockerID: %u, ChangeStatus: %u, foupRoom: %u\n", nID, nChaned, foupRoom);

	uint8 nAuthAck = 0;
	WLock(rwLock_foup_map_)
	{
		amhs_stocker_map::iterator itStocker=stocker_map_.find(nID);
		if (itStocker == stocker_map_.end())
		{
			nAuthAck = 0;
		}
		else
		{
			amhs_foup_map::iterator itFoup=itStocker->second->foup_map.find(foupBarCode);
			if(itFoup != itStocker->second->foup_map.end())
			{
				if(1 == nChaned)
				{
					itFoup->second->nChaned = nChaned;
					itFoup->second->nfoupRoom = foupRoom;
					itFoup->second->nInput = nInput;
					itFoup->second->nLot = foupLot;
					itStocker->second->last_opt_foup_vec.clear();
					itStocker->second->last_opt_foup_vec.push_back(itFoup->second);
					itStocker->second->foup_erase_vec.push_back(itFoup->second);
					itStocker->second->foup_map.erase(foupBarCode);
				}
				nAuthAck=0;
			}
			else
			{		
				amhs_foup_ptr pFoup = amhs_foup_ptr(new amhs_Foup());
				pFoup->nChaned=nChaned;
				pFoup->nfoupRoom=foupRoom;
				pFoup->nLot=foupLot;
				pFoup->nBarCode=foupBarCode;
				pFoup->nInput=nInput;
				pFoup->p_participant = participants;
				itStocker->second->foup_map.insert(std::make_pair(foupBarCode, pFoup));
				itStocker->second->last_opt_foup_vec.clear();
				itStocker->second->last_opt_foup_vec.push_back(pFoup);

				nAuthAck = 1;
			}
			amhs_foup_map::iterator itFoupTotal = foup_map_.find(foupBarCode);
			if(itFoupTotal != foup_map_.end())
			{
				if(1 == nChaned && (5 == nInput || 6 == nInput || 7 == nInput || 8 == nInput))
				{
					foup_map_.erase(itFoupTotal);
				}
			}
			else
			{
				if(0 == nChaned)
				{
				amhs_foup_ptr pFoup = amhs_foup_ptr(new amhs_Foup());
				pFoup->nChaned=nChaned;
				pFoup->nfoupRoom=foupRoom;
				pFoup->nLot=foupLot;
				pFoup->nBarCode=foupBarCode;
				pFoup->nInput=nInput;
				pFoup->p_participant = participants;
				foup_map_.insert(std::make_pair(foupBarCode, pFoup));
				}
			}
		}
	}


	AMHSPacket ack(STK_MCS_ACK_FOUP_EVENT, 2);
	ack << uint8(nID);
	ack << uint8(1);
	SendPacket(participants, ack);
}

void amhs_room::Handle_OHT_AckStatusBackTime(amhs_participant_ptr participants, AMHSPacket& Packet)
{
	uint8 nID = 0;
	uint8 nTime = 0;
	Packet >> nID;
	Packet >> nTime;

	WLock(rwLock_oht_map_)
	{
		amhs_oht_map::iterator it = oht_map_.find(nID);
		if (it != oht_map_.end())
		{
			it->second->nStatusTime = nTime;
		}
	}
}

void amhs_room::Handle_OHT_AckPosBackTime(amhs_participant_ptr participants, AMHSPacket& Packet)
{
	uint8 nID = 0;
	uint8 nTime = 0;
	Packet >> nID;
	Packet >> nTime;
	 
	WLock(rwLock_oht_map_)
	{
		amhs_oht_map::iterator it = oht_map_.find(nID);
		if (it != oht_map_.end())
		{
			it->second->nPosTime = nTime;
		}
	}
}

void amhs_room::Handle_OHT_AckPath(amhs_participant_ptr participants, AMHSPacket& Packet)
{
	uint8 nID = 0;
	uint8 nResult = 0;
	Packet >> nID;
	Packet >> nResult;

	WLock(rwLock_oht_map_)
	{
		amhs_oht_map::iterator it = oht_map_.find(nID);
		if (it != oht_map_.end())
		{
			it->second->nPathResult = nResult;
		}
	}
}

void amhs_room::Handle_OHT_AckMove(amhs_participant_ptr participants, AMHSPacket& Packet)
{
	uint8 nID = 0;
	uint8 nStatus = 0;
	uint8 nAlarm = 0;
	Packet >> nID;
	Packet >> nStatus;
	Packet >> nAlarm;

	WLock(rwLock_oht_map_)
	{
		amhs_oht_map::iterator it = oht_map_.find(nID);
		if (it != oht_map_.end())
		{
			it->second->nMoveStatus = nStatus;
			it->second->nMoveAlarm = nAlarm;
		}
	}
}

void amhs_room::Handle_OHT_AckFoup(amhs_participant_ptr participants, AMHSPacket& Packet)
{
	uint8 nID = 0;
	uint8 nResult = 0;
	Packet >> nID;
	Packet >> nResult;

	WLock(rwLock_oht_map_)
	{
		amhs_oht_map::iterator it = oht_map_.find(nID);
		if (it != oht_map_.end())
		{
			it->second->nFoupOpt = nResult;
		}
	}
}

void amhs_room::Handle_OHT_Status(amhs_participant_ptr participants, AMHSPacket& Packet)
{
	uint8 nID = 0;
	uint8 nMode = 0;
	uint8 nStatus = 0;
	uint8 nAlarm = 0;
	Packet >> nID;
	Packet >> nMode;
	Packet >> nStatus;
	Packet >> nAlarm;

	WLock(rwLock_oht_map_)
	{
		amhs_oht_map::iterator it = oht_map_.find(nID);
		if (it != oht_map_.end())
		{
			it->second->nBackStatusMode = nMode;
			it->second->nBackStatusMark = nStatus;
			it->second->nBackStausAlarm = nAlarm;
		}
	}
}

void amhs_room::Handle_OHT_Auth(amhs_participant_ptr participants, AMHSPacket& Packet)
{
	uint8		ohtID = 0;
	uint32		ohtPosition = 0;
	uint8		ohtHand = 0;
	Packet >> ohtID;
	Packet >> ohtPosition;
	Packet >> ohtHand;
	printf("OHT Auth  ---> id: %u, pos: %u, hand: %u\n", ohtID, ohtPosition, ohtHand);
	participants->nID_ = ohtID;
	participants->nDevType_ = DEV_TYPE_OHT;

	uint8 nAuthAck = 0;
	WLock(rwLock_oht_map_)
	{
		amhs_oht_map::iterator it = oht_map_.find(ohtID);
		if (it != oht_map_.end())
		{
			nAuthAck = 0;
		}
		else
		{
			amhs_oht_ptr pOht = amhs_oht_ptr(new amhs_OHT());
			pOht->nID = ohtID;
			pOht->nPOS = ohtPosition;
			pOht->nHand = ohtHand;
			pOht->p_participant = participants;
			oht_map_.insert(std::make_pair(ohtID, pOht));
			nAuthAck = 1;
		}
	}

	AMHSPacket ack(OHT_MCS_ACK_AUTH, 2);
	ack << uint8(ohtID);
	ack << nAuthAck;
	SendPacket(participants, ack);
}
void amhs_room::Handle_STK_Auth(amhs_participant_ptr participants, AMHSPacket& Packet)
{
	uint8 stockerID = 0;
	uint32 uIP = 0;
	uint8 room = 0;
	uint8 autoin = 0;
	uint8 manin = 0;
	Packet >> stockerID;
	Packet >> uIP;
	Packet >> room;
	Packet >> autoin;
	Packet >> manin;
	
	struct in_addr addr1;

	memcpy(&addr1, &uIP, 4);
	char* sIP = inet_ntoa(addr1);
	printf("STOCKER Auth ---> id: %d, ip: %s  room:%d, autoin: %d, manin:%d\n", stockerID, sIP,
		room, autoin, manin);
	participants->nDevType_ = DEV_TYPE_STOCKER;
	participants->nID_ = stockerID;

	uint8 nAuthAck = 0;
	WLock(rwLock_stocker_map_)
	{
		amhs_stocker_map::iterator it = stocker_map_.find(stockerID);
		if (it != stocker_map_.end())
		{
			nAuthAck = 1;
		}
		else
		{
			amhs_stocker_ptr pStocker = amhs_stocker_ptr(new amhs_Stocker());
			pStocker->nID = stockerID;
			pStocker->nStatus = 3;
			pStocker->nAuto=3;
			pStocker->nManu=3;
			pStocker->p_participant = participants;
			stocker_map_.insert(std::make_pair(stockerID, pStocker));
			nAuthAck = 0;
		}
	}

	AMHSPacket ack(STK_MCS_ACK_AUTH, 10);
	SYSTEMTIME st = {0};
	GetLocalTime(&st);
	ack << uint8(stockerID);
	ack << uint8(nAuthAck);
	//__time64_t ltime;
	//_time64( &ltime );
	//ack << uint64(ltime);
	ack << uint16(st.wYear);
	ack << uint8(st.wMonth);
	ack << uint8(st.wDay);
	ack << uint8(st.wHour);
	ack << uint8(st.wMinute);
	ack << uint8(st.wSecond);
	ack << uint8(0);

	SendPacket(participants, ack);
}

void amhs_room::Handle_OHT_Pos(amhs_participant_ptr participants, AMHSPacket& Packet)
{
	uint8		ohtID = 0;
	uint32		ohtPosition = 0;
	Packet >> ohtID;
	Packet >> ohtPosition;

	WLock(rwLock_oht_map_)
	{
		amhs_oht_map::iterator it = oht_map_.find(ohtID);
		if (it != oht_map_.end())
		{
			it->second->nPOS = ohtPosition;
		}
	}
}

int amhs_room::DecodePacket(amhs_participant_ptr participants, AMHSPacket& Packet)
{
	int mOpcode = Packet.GetOpcode();
	OPT_MAP::iterator it = m_optHanders.find(mOpcode);
	if (it != m_optHanders.end())
	{
		//Packet.hexlike();
		(this->*it->second)(participants, Packet);
		return 0;
	}
	else
	{
		return -1;
	}
}

void amhs_room::deliver_all(const amhs_message& msg)
{
	std::for_each(participants_.begin(), participants_.end(),
		boost::bind(&amhs_participant::deliver, _1, boost::ref(msg)));
}

int amhs_room::GetCount()
{
	return participants_.size();
}