#include "StdAfx.h"
#include "Scheduler.h"
#include "../SqlAceCli/SqlAceCli.h"

initialiseSingleton(CScheduler);
CScheduler::CScheduler(void)
{
}


CScheduler::~CScheduler(void)
{
}


int CScheduler::Init(void)
{
	LOG_DEBUG("");
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
	VEC_TRANS transList = dbTransfer.GetTransfer();
	for (auto it = transList.cbegin();
		it != transList.cend(); ++it)
	{
		printf("Transfer: FoupID: %d, Target: %d\r\n", it->nFoupID, it->nTarget);
	}

	return 0;
}
