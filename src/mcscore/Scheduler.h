#pragma once
#include "../shared/Singleton.h"
#pragma comment(lib, "shared.lib")
#include "../SqlAceCli/SqlAceCli.h"
#include "boost/threadpool.hpp"


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
	MAP_TRANS m_mapTrans;
	boost::threadpool::pool m_tpTask;

private:
	int GetMacroCommand(void);
	void _taskCheckTrans(void);
};

#define sScheduler CScheduler::getSingleton()