#include "StdAfx.h"
#include "Scheduler.h"

initialiseSingleton(CScheduler);
CScheduler::CScheduler(void)
	: m_tpTask(10)
{
}


CScheduler::~CScheduler(void)
{
}


int CScheduler::Init(void)
{
	LOG_DEBUG("");
	m_tpTask.schedule(boost::bind(&CScheduler::_taskCheckTrans, this));
	return 0;
}


int CScheduler::Run(void)
{
	LOG_DEBUG("");
	//test

	return 0;
}


int CScheduler::Stop(void)
{
	LOG_DEBUG("");
	return 0;
}


int CScheduler::GetMacroCommand(void)
{
	bool bChanged = false;
	DBTransfer dbTransfer;
	VEC_TRANS listTrans = dbTransfer.GetTransferNoFinished();
	if (listTrans.size() != m_mapTrans.size())
	{
		m_mapTrans.clear();
	}
	
	for (auto it = listTrans.cbegin();
		it != listTrans.cend(); ++it)
	{
		int nID = it->nID;
		auto itFind = m_mapTrans.find(nID);
		if (itFind == m_mapTrans.cend())
		{
			bChanged = true;
			m_mapTrans.insert(std::make_pair(nID, *it));
		}
	}

	if (true == bChanged)
	{
		return listTrans.size();
	}
	else
	{
		return 0;
	}
	
}


void CScheduler::_taskCheckTrans(void)
{
	while (true)
	{
		if (GetMacroCommand() > 0)
		{
			for (auto it = m_mapTrans.cbegin();
				it != m_mapTrans.cend(); ++it)
			{
				auto pTrans = &(it->second);
				printf("Transfer: ID: %d, FoupID: %d, Target: %d\r\n", 
					pTrans->nID, pTrans->nFoupID,pTrans->nTarget);
			}
		}
		
		Sleep(1000);
	}
}
