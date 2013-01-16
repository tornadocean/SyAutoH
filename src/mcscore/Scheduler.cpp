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
	GetMacroCommand();

	return 0;
}


int CScheduler::Stop(void)
{
	LOG_DEBUG("");
	return 0;
}


int CScheduler::GetMacroCommand(void)
{
	DBTransfer dbTransfer;
	m_listTrans = dbTransfer.GetTransferNoFinished();
	for (auto it = m_listTrans.cbegin();
		it != m_listTrans.cend(); ++it)
	{
		printf("Transfer: FoupID: %d, Target: %d\r\n", it->nFoupID, it->nTarget);
	}

	return 0;
}


void CScheduler::_taskCheckTrans(void)
{
	while (true)
	{
		GetMacroCommand();
		Sleep(1000);
	}
}
