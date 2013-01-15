#pragma once

#include "../MesLink/MesLink.h"
#include "../SqlAceCli/SqlAceCli.h"
#include "../GuiHub/GuiHub.h"
#include "../AMHSDrive/AMHSDrive.h"
#include "../mcscore/mcscore.h"

[event_receiver(native)]
class MesMsgReceiver 
{
public:
	DBFoup* m_pFoupDB;

	void MESHanderPick(int nBarCode, int nLoc, int nLocType)
	{
		wprintf_s(L"FoupPick was called with value %d %d %d.\n", nBarCode, nLoc, nLocType);
		int nOHT = 0;
		int nStocker = 0;
		int nFoup = m_pFoupDB->FindFoup(nBarCode);

		if (nFoup > 0)
		{
			int nLocal = 0;
			int nType = 0;
			//m_pFoupDB->GetFoupLocation(nFoup, nLocal, nType);
			//wprintf_s(L"Find Foup: %d at Location: %d Type: %d.\n", 
			//	nBarCode, nLocal, nType);

			//m_pFoupDB->SetFoupLocation(nFoup, nLocal, nType);
			//wprintf_s(L"Find Foup: %d at Location: %d Type: %d.\n", 
			//	nBarCode, nLocal, nType);
		}
		else
		{
			wprintf_s(L"Can not Fine Foup %d.\n", nBarCode);
		}
	}

	void MESHanderPlace(int nBarCode, int nLoc, int nLocType)
	{
		wprintf_s(L"FoupPlace was called with value %s %d %d.\n", nBarCode, nLoc, nLocType);
		int nOHT = 0;
		int nStocker = 0;
		int nFoup = m_pFoupDB->FindFoup(nBarCode);

		if (nFoup > 0)
		{
			int nLocal = 0;
			int nType = 0;
			//m_pFoupDB->GetFoupLocation(nFoup, nLocal, nType);
			//wprintf_s(L"Find Foup: %d at Location: %d Type: %d.\n", 
			//	nBarCode, nLocal, nType);
		}
		else
		{
			wprintf_s(L"Can not Fine Foup %d.\n", nBarCode);
		}
	}

	void hookEvent(MesMsgSource* pSource) 
	{
		__hook(&MesMsgSource::MESPickFoup, pSource, &MesMsgReceiver::MESHanderPick);
		__hook(&MesMsgSource::MESPlaceFoup, pSource, &MesMsgReceiver::MESHanderPlace);
	}

	void unhookEvent(MesMsgSource* pSource) 
	{
		__unhook(&MesMsgSource::MESPickFoup, pSource, &MesMsgReceiver::MESHanderPick);
		__unhook(&MesMsgSource::MESPlaceFoup, pSource, &MesMsgReceiver::MESHanderPlace);
	}
};

class MaterialController 
{
public:
	MaterialController(void);
	~MaterialController(void);

public:
	int Init(void);
	void Check(void);

private:
	CMesLink m_MesLink;
	DBFoup m_FoupDB;
	MesMsgReceiver m_MesReciver;
	CGuiHub m_GuiHub;
	MesMsgSource m_MesSource;
	CAMHSDrive m_amhsDrive;
	CMCSCore m_core;
public:
	void PrintfInfo(void);
};

