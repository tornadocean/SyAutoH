#pragma once
#include "VirtualAMHSDevice.h"

class VirtualFoup
{
public:
	int nID;
	int nStatus;
	int nBatchID;
	int nRoomID;

	VirtualFoup()
		: nStatus(0),
		nID(0),
		nBatchID(0),
		nRoomID(0)
	{

	}
};
typedef struct
{
	int nStatus;
	int nFoupID;
}ItemRoom;

typedef std::map<int, VirtualFoup> MAP_VFOUP;
typedef std::map<int,ItemRoom> MAP_VROOM;

class VirtualStocker : public VirtualAMHSDevice
{
public:
	VirtualStocker(void);
	virtual ~VirtualStocker(void);

public:
	virtual void HandleCommand(AMHSPacket& packet);

public:
	int Auth( const char* sIP);
	int AuthFoup(int nFoupID,int nMode);
	int ManualInputFoup(int nFoupID,int nBatchID);
	int ManualOutputFoup(int nFoupID);
	int FoupIntoRoom(int nID,int nBatchID);
	int FoupOutRoom(int nID);
	int InitRoom(int nFoupID,int nBatchID,int nRoomID);
	int GetRoomID(int nFoupID);
	int UpdateStatus();
	int UpdateFoupInfo();
	void CreateTimer(void);
	void DestoryTimer(void);
	static void CALLBACK TimerHandler(UINT id, UINT msg, DWORD dwUser, DWORD dw1, DWORD dw2);
	void OnTimer(void);
	int History();
	int m_nContain;
	int m_nWholeRoomStatus;
	int m_nTimerID;
	int m_nTimeCounter;
	int m_nMoveStatus;
	int m_nFoupChange;
	uint64 m_nMoveStatusTimeSet;
	uint64 m_nFoupInfoTimeSet;
	VirtualFoup CFoup;
	void Handle_FoupOperate(AMHSPacket& packet);
private:
	void Handle_Auth(AMHSPacket& packet);
	
	void Handle_StatusQuery(AMHSPacket& packet);
	void Handle_RoomQuery(AMHSPacket& packet);
	void Handle_StorageQuery(AMHSPacket& packet);
	void Handle_InputStatus(AMHSPacket& packet);
	void Handle_History(AMHSPacket& packet);
	void Handle_Alarms(AMHSPacket& packet);
	void Handle_StatusBackTime(AMHSPacket& packet);
	void Handle_FoupBackTime(AMHSPacket& packet);
	void Handle_AuthBack(AMHSPacket& packet);
	void Handle_FoupEvent(AMHSPacket& packet);

private:
	typedef void (VirtualStocker::*CommandHander)(AMHSPacket& packet);
	typedef std::map<int, CommandHander> OPT_MAP;
	int GetRoomStatus();
	
	OPT_MAP m_optHanders;
	MAP_VFOUP m_mapFoups;
	MAP_VROOM m_mapRooms;

};

