#pragma once
#include <algorithm>
#include <cstdlib>
#include <deque>
#include <iostream>
#include <list>
#include <set>
#include <map>
#include <vector>
#include <boost/bind.hpp>
#include <boost/shared_ptr.hpp>
#include <boost/enable_shared_from_this.hpp>
#include <boost/asio.hpp>
#include "amhs_message.hpp"
#include "ThreadLock.h"

using boost::asio::ip::tcp;

//----------------------------------------------------------------------

typedef std::deque<amhs_message> amhs_message_queue;
enum AMHS_DEV_TYPE
{
	DEV_TYPE_UNKNOW = 0,
	DEV_TYPE_OHT = 1,
	DEV_TYPE_STOCKER = 2
};
//----------------------------------------------------------------------

class amhs_participant
{
public:
	virtual ~amhs_participant() {}
	virtual void deliver(const amhs_message& msg) = 0;
public:
	int nID_;
	int nDevType_;
	std::string sIP_;
	unsigned int uPort_;
};

typedef boost::shared_ptr<amhs_participant> amhs_participant_ptr;

typedef struct sData_OHT
{
	int nID;
	DWORD nPOS;
	int nHand;
	int nStatusTime;
	int nPosTime;
	int nPathResult;
	int nMoveStatus;
	int nMoveAlarm;
	int nFoupOpt;
	int nBackStatusMode;
	int nBackStatusMark;
	int nBackStausAlarm;
	bool  bNeedPath;
	amhs_participant_ptr p_participant;
} amhs_OHT;
typedef boost::shared_ptr<amhs_OHT> amhs_oht_ptr;
typedef std::map<int, amhs_oht_ptr> amhs_oht_map;
typedef std::set<amhs_oht_ptr> amhs_oht_set;
typedef std::vector<amhs_oht_ptr> amhs_oht_vec;

typedef struct sPath_KeyPoint
{
	uint32 nPos;
	uint8 nType;
	uint8 nSpeedRate;

} amhs_keyPoint;
typedef std::vector<amhs_keyPoint> amhs_keypoint_vec;

typedef struct sData_Foup
{
	uint8 nChaned;
	uint8 nfoupRoom;
	uint16 nBarCode;
	uint16 nLot;
	uint8 nInput;
} amhs_Foup;
typedef boost::shared_ptr<amhs_Foup> amhs_foup_ptr;
typedef std::map<uint16, amhs_foup_ptr> amhs_foup_map;
typedef std::vector<amhs_foup_ptr> amhs_foup_vec;

typedef struct sData_Stocker
{
	int nID;
	int nStatus;
	int nAuto;
	int nManu;
	uint8 room[141];
	amhs_foup_vec last_opt_foup_vec;
	amhs_foup_map foup_map;
	SYSTEMTIME last_opt_foup_time;
	amhs_participant_ptr p_participant;

	rwmutex rwLock_foup_map_;
	amhs_foup_vec GetFoups();
	void CleanFoups();
	void AddFoup(amhs_foup_ptr);
}amhs_Stocker;
typedef boost::shared_ptr<amhs_Stocker> amhs_stocker_ptr;
typedef std::map<int, amhs_stocker_ptr> amhs_stocker_map;
typedef std::set<amhs_stocker_ptr> amhs_stocker_set;
typedef std::vector<amhs_stocker_ptr> amhs_stocker_vec;

//----------------------------------------------------------------------

class amhs_room
{
public:
	amhs_room();
	void join(amhs_participant_ptr participant);
	void leave(amhs_participant_ptr participant);

	
	void SendPacket(amhs_participant_ptr participants, AMHSPacket &ack);
	void SendPacket(int nID, int nType, AMHSPacket& packet);
	int DecodePacket(amhs_participant_ptr participants, AMHSPacket& Packet);
	void deliver_all(const amhs_message& msg);
	int GetCount();

public:
	amhs_oht_vec OHT_GetOHTList();

	amhs_stocker_vec STK_GetStockerList();
	amhs_foup_vec STK_GetFoups(int nID);
	amhs_foup_vec STK_GetLastEventFoup(int nID);
	vector<int> GetStkRoom(int nID);

private:
	void Handle_OHT_AckStatusBackTime(amhs_participant_ptr, AMHSPacket&);
	void Handle_OHT_AckPosBackTime(amhs_participant_ptr, AMHSPacket&);
	void Handle_OHT_AckPath(amhs_participant_ptr, AMHSPacket&);
	void Handle_OHT_AckMove(amhs_participant_ptr, AMHSPacket&);
	void Handle_OHT_AckFoup(amhs_participant_ptr, AMHSPacket&);
	void Handle_OHT_Auth(amhs_participant_ptr, AMHSPacket&);
	void Handle_OHT_Pos(amhs_participant_ptr, AMHSPacket&);
	void Handle_OHT_Status(amhs_participant_ptr, AMHSPacket&);
	void Handle_OHT_NeedPath(amhs_participant_ptr, AMHSPacket&);
	void Handle_OHT_TeachPath(amhs_participant_ptr, AMHSPacket&);

	void Handle_STK_AckFoup(amhs_participant_ptr, AMHSPacket&);
	void Handle_STK_AckStatus(amhs_participant_ptr, AMHSPacket&);
	void Handle_STK_AckRoom(amhs_participant_ptr, AMHSPacket&);
	void Handle_STK_AckStorage(amhs_participant_ptr, AMHSPacket&);
	void Handle_STK_AckInputStatus(amhs_participant_ptr, AMHSPacket&);
	void Handle_STK_AckHistory(amhs_participant_ptr, AMHSPacket&);
	void Handle_STK_AckAlarms(amhs_participant_ptr, AMHSPacket&);
	void Handle_STK_Auth(amhs_participant_ptr, AMHSPacket&);
	void Handle_STK_FoupEvent(amhs_participant_ptr, AMHSPacket&);
	void Handle_STK_Ack_StatusTime(amhs_participant_ptr, AMHSPacket&);
	void Handle_STK_Ack_FoupTime(amhs_participant_ptr, AMHSPacket&);

private:
	std::set<amhs_participant_ptr> participants_;
	enum { max_recent_msgs = 100 };
	amhs_message_queue recent_msgs_;
	amhs_oht_map oht_map_;
	amhs_stocker_map stocker_map_;
	amhs_foup_map foup_map_;
	rwmutex rwLock_oht_map_;
	rwmutex rwLock_stocker_map_;

	typedef void (amhs_room::*HANDLE_OPT)(amhs_participant_ptr, AMHSPacket& packet);
	typedef std::map<int, HANDLE_OPT> OPT_MAP;
	OPT_MAP m_optHanders;
};

//----------------------------------------------------------------------
typedef std::map<int, AMHSPacket*> Packet_Map;
class amhs_session
	: public amhs_participant,
	public boost::enable_shared_from_this<amhs_session>
{
public:
	amhs_session(boost::asio::io_service& io_service, amhs_room& room);

	tcp::socket& socket();
	void PutSocketInfo(tcp::socket& socket_);
	void start();
	void deliver(const amhs_message& msg);
	void handle_read_header(const boost::system::error_code& error);
	void handle_read_body(const boost::system::error_code& error);
	void handle_write(const boost::system::error_code& error);

private:
	tcp::socket socket_;
	amhs_room& room_;
	amhs_message read_msg_;
	amhs_message_queue write_msgs_;
	Packet_Map read_packets_;
};

typedef boost::shared_ptr<amhs_session> amhs_session_ptr;

//----------------------------------------------------------------------

class amhs_dev_server
{
public:
	amhs_dev_server(boost::asio::io_service& io_service,
		const tcp::endpoint& endpoint);

public:
	int GetConnectCount();

	amhs_oht_vec OHT_GetDataSet();
	void OHT_Set_StatusBackTime(int nID, int ms);
	void OHT_Set_PosBackTime(int nID, int ms);
	void OHT_Move(int nID, int nControl);
	void OHT_Foup(int nID, int nDevBuf, int nOperation);
	void OHT_SetPath(int nID, int nType, int nStart, int nTarget, amhs_keypoint_vec& KeyPoints);

	amhs_stocker_vec STK_GetDataSet();
	amhs_foup_vec STK_GetFoupDataSet(int nID);
	amhs_foup_vec STK_GetLastOptFoup(int nID);
	vector<int> STK_GetRoom(int nID);

	void STK_FOUP(int nID, int nMode, int nPick, int nFoupData);
	void STK_Status(int nID);
	void STK_Room(int nID);
	void STK_Storage(int nID);
	void STK_InputStatus(int nID);
	void STK_History(int nID, const SYSTEMTIME &timeStart, const SYSTEMTIME &timeEnd);
	void STK_Alarms(int nID, const SYSTEMTIME &timeStart, const SYSTEMTIME &timeEnd);
	void STK_Set_StatusBackTime(int nID, int nSecond);
	void STK_Set_FoupBackTime(int nID, int nSecond);

private:
	void start_accept();
	void handle_accept(amhs_session_ptr session,
		const boost::system::error_code& error);
	inline void SendPacket_OHT(int nID, AMHSPacket& packet)
	{
		room_.SendPacket(nID, DEV_TYPE_OHT, packet);
	}

	inline void SendPacket_STK(int nID, AMHSPacket& packet)
	{
		room_.SendPacket(nID, DEV_TYPE_STOCKER, packet);
	}

private:
	boost::asio::io_service& io_service_;
	tcp::acceptor acceptor_;
	amhs_room room_;
};

typedef boost::shared_ptr<amhs_dev_server> amhs_server_ptr;
typedef std::list<amhs_server_ptr> amhs_server_list;