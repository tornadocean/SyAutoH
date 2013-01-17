#pragma once
#include "../shared/Singleton.h"
#pragma comment(lib, "shared.lib")
#include "../SqlAceCli/SqlAceCli.h"
#include "boost/threadpool.hpp"
#include "../shared/ThreadLock.h"

const int Max_Run_Trans = 1;
class CScheduler;
class CScheduler : public Singleton<CScheduler>
{
public:
	CScheduler(void);
	~CScheduler(void);
	int Init(void);
	int Run(void);
	int Stop(void);

private:
	VEC_TRANS m_listTrans;
	rwmutex m_rwmxMapTrans;
	MAP_TRANS m_mapTrans;
	boost::threadpool::pool m_tpTask;
	int m_nTransRunCount;
	DBTransfer m_dbTransfer;

private:
	int GetNewTrans(void);
	void _taskCheckTrans(void);
	void _taskRunTrans(int nID, int nBarCode, int nTarget);

};

#define sScheduler CScheduler::getSingleton()