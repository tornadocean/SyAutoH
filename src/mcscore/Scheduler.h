#pragma once
#include "../shared/Singleton.h"
#pragma comment(lib, "shared.lib")

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
	int GetMacroCommand(void);
};

#define sScheduler CScheduler::getSingleton()