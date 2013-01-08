#pragma once
#include "VirtualAMHSDevice.h"
typedef struct 
{
	int nposition;
	int nType;
	int nSpeed;
} PathInfo;
typedef std::list<PathInfo> PATH_SET_LIST;
typedef struct
{
	int nOHT_ID;
	int nPosition;
}ChangeOHT;
class VirtualOHT : public VirtualAMHSDevice
{
public:
	VirtualOHT(void);
	virtual ~VirtualOHT(void);
public:
	int Auth( uint32 nPos, int nHand);
	int UpdatePos();
	int UpdateStatus();
	int SetTeachPosition(uint32 nPos, uint8 nType, uint8 nSpeedRate);
	int AskPath();

	virtual void HandleCommand(AMHSPacket& packet);

private:
	void Handle_Auth(AMHSPacket& );
	void Handle_SetPosTime(AMHSPacket& );
	void Handle_SetStatusTime(AMHSPacket& );
	void Handle_FoupHanding(AMHSPacket& );
	void Handle_SetPath(AMHSPacket& );
	void Handle_Move(AMHSPacket& );
	
private:
	typedef void (VirtualOHT::*CommandHander)(AMHSPacket& packet);
	typedef std::map<int, CommandHander> OPT_MAP;
	OPT_MAP m_optHanders;
	PATH_SET_LIST m_listPath;

public:
	bool isStop;
	bool isMove;
	bool isSetPath;
	int m_nHandChange;
	int m_nHand;
	int m_nStatus;
	int m_nSpeed;
	DWORD m_nPos;
	int m_nPosUpdateTimeSet;
	int m_nStatusUpdateTimeSet;
	int m_nTimerID;
	int m_nTimeCounter;
	ChangeOHT m_nOHT;
	void CreateTimer(void);
	void DestoryTimer(void);
	static void CALLBACK TimerHandler(UINT id, UINT msg, DWORD dwUser, DWORD dw1, DWORD dw2);
	void OnTimer(void);
};

