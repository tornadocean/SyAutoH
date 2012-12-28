#pragma once
#include "iGuiHub.h"
#include "../shared/ThreadLock.h"
using namespace MCS;

class ClientInfo
{
private:
	
	
public: 
	MCS::GuiDataUpdaterPrx client;
	int nTryCount;
	MCS::GuiHub::GuiPushDataList m_listPushData;
	Ice::ConnectionPtr m_ptrCon;
	
	ClientInfo()
	{
		nTryCount = 0;
	}
	~ClientInfo()
	{

	}

	
};

//typedef set<MCS::GuiDataUpdaterPrx> SET_UPDATER;
typedef map<MCS::GuiDataUpdaterPrx, ClientInfo*> MAP_UPDATER;

class CAMHSDrive;
class GuiDataHubI : public GuiDataHub
{
public:
	GuiDataHubI(void);
	virtual ~GuiDataHubI(void);

public:
	virtual Ice::Int SetPushCmd(const ::MCS::GuiDataUpdaterPrx&, const ::MCS::GuiHub::GuiPushDataList&, ::Ice::Int, const ::Ice::Current& /* = ::Ice::Current */);
	virtual Ice::Int SetPushTimer(::Ice::Int, ::Ice::Int, const ::Ice::Current& /* = ::Ice::Current */);
	virtual std::string MCS::GuiDataHub::ReadData(MCS::GuiHub::GuiCommand, Ice::Int, const Ice::Current &);
	virtual Ice::Int MCS::GuiDataHub::WriteData(MCS::GuiHub::GuiCommand, const std::string &, Ice::Int, const Ice::Current &);
	virtual void SetDataUpdater(const ::MCS::GuiDataUpdaterPrx&, const ::Ice::Current& /* = ::Ice::Current */);
	virtual void EraseDataUpdater(const ::MCS::GuiDataUpdaterPrx&, const ::Ice::Current& /* = ::Ice::Current */);

public:
	void UpdateDataAll(MCS::GuiHub::PushData, const std::string &);
	void removeUpdater(const ::MCS::GuiDataUpdaterPrx& updater);

	CAMHSDrive* AMHSDrive() const { return m_pAMHSDrive; }
	void AMHSDrive(CAMHSDrive* val) { m_pAMHSDrive = val; }

private:
	MAP_UPDATER m_mapUpdater;
	rwmutex m_rwUpdaterSet;
	rwmutex m_rwTimerSet;
	CAMHSDrive* m_pAMHSDrive;

	typedef void (GuiDataHubI::*WriteHander)(const std::string&, const ::Ice::Current&);
	typedef std::map<int, WriteHander> HANDLE_MAP;
	HANDLE_MAP m_mapHandles;
	typedef MCS::GuiDataItem (GuiDataHubI::*MakePushData)(void);
	typedef std::map<MCS::GuiHub::PushData, MakePushData> MAP_MAKEPUSH;
	MAP_MAKEPUSH m_mapMakePush;
	void UpdateDataOne(const Ice::ConnectionPtr, MCS::GuiHub::PushData, const std::string &);

	int m_nTimerID;
	int m_nTimerPeriord;

private:
	void SetTimer();
	void StopTimer();
	static void CALLBACK TimerHandler(UINT id, UINT msg, DWORD dwUser, DWORD dw1, DWORD dw2);
	void OnTimer(void);
	void PushDatatoCli(const ::MCS::GuiDataUpdaterPrx&, MCS::GuiHub::PushData, const std::string &);

private:
	void OHT_SetPositionBackTime(const std::string&, const ::Ice::Current&);
	void OHT_SetStatusBackTime(const std::string&, const ::Ice::Current&);
	void OHT_GetPositionTable(const std::string&, const ::Ice::Current&);
	void OHT_FoupHanding(const std::string&, const ::Ice::Current&);
	void OHT_SetPath(const std::string&, const ::Ice::Current&);
	void OHT_Move(const std::string&, const ::Ice::Current&);
	
	void OHT_PathTest(const std::string&, const ::Ice::Current&);
	void OHT_FoupTest(const std::string&, const ::Ice::Current&);
	void OHT_MoveTest(const std::string&, const ::Ice::Current&);

	void STK_SetStatusBackTime(const std::string&, const ::Ice::Current&);
	void STK_SetFoupInfoBackTime(const std::string&, const ::Ice::Current&);
	void STK_FoupHanding(const std::string&, const ::Ice::Current&);
	void STK_StockerStatus(const std::string&, const ::Ice::Current&);
	void STK_StockerRoom(const std::string&, const ::Ice::Current&);
	void STK_StockerFoupStorage(const std::string&, const ::Ice::Current&);
	void STK_InputStatus(const std::string&, const ::Ice::Current&);
	void STK_History(const std::string&, const ::Ice::Current&);
	void STK_Alarms(const std::string&, const ::Ice::Current&);
	void STK_GetFoupInSys(const std::string&, const ::Ice::Current&);

private:
	GuiDataItem Push_OHT_DevInfo();
	GuiDataItem Push_OHT_Position();

	GuiDataItem Push_STK_DevInfo();
	GuiDataItem Push_STK_FoupInfo();
	GuiDataItem Push_STK_LastOptFoup();
	GuiDataItem Push_STK_Status();
	GuiDataItem Push_STK_InputStatus();

private:
	SYSTEMTIME ToTime(std::string&);
};

class UpdateCallback : public IceUtil::Shared
{
public:
	GuiDataUpdaterPrx client;
	GuiDataHubI*   m_view;

	UpdateCallback()
	{
		client = NULL;
		m_view = NULL;
	}

	void response()
	{
		//m_view->PopCallPtr();
	}

	void exception(const Ice::Exception& ex)
	{
		//cerr << "call failed:\n" << ex << endl;
		//int i=0;
		//i = client->ice_getHash();
		//cout << "call failed:\n" << ex.ice_name().c_str() << endl;

		if (NULL != m_view)
		{
			m_view->removeUpdater(client);
		}
	}
};
typedef IceUtil::Handle<UpdateCallback> UpdateCallbackPtr;