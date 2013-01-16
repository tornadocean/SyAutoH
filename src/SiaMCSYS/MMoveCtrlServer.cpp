#include "StdAfx.h"
#include "MMoveCtrlServer.h"
#include <signal.h>
#include "MaterialController.h"
#include "../shared/Threading/AtomicBoolean.h"
#include <iostream>

#include "../MesLink/MesLink.h"
#include "../SqlAceCli/SqlAceCli.h"
#include "../GuiHub/GuiHub.h"
#include "logclient.h"

using namespace std;

syamhs::Threading::AtomicBoolean g_Running(true);

/*** Signal Handler ***/
void _OnSignal(int s)
{
	switch(s)
	{
	case SIGINT:
	case SIGTERM:
	case SIGABRT:
#ifdef _WIN32
	case SIGBREAK:
#endif
		g_Running.SetVal(false);
		break;
	}

	signal(s, _OnSignal);
}

void MMoveCtrlServer::Stop()
{

}

void MMoveCtrlServer::Run(int argc, _TCHAR** argv)
{
	// init
	MaterialController MC;
	MC.PrintfInfo();
	if (0 != MC.Init())
	{
		cout<< "MCS Init failed." << endl;
		return;
	}
	LogS(10, LogType::Info, "MCS", "MCS Started.");
	
	MC.Run();
	{
		// hook signals
		signal(SIGINT, _OnSignal);
		signal(SIGTERM, _OnSignal);
		signal(SIGABRT, _OnSignal);
#ifdef _WIN32
		signal(SIGBREAK, _OnSignal);
#else
		signal(SIGHUP, _OnSignal);
#endif
		

		while(g_Running.GetVal())
		{
			//if(!(++loop_counter % 20))	 // 20 seconds
			//	CheckForDeadSockets();

			//if(!(loop_counter % 300))	// 5mins
			//	ThreadPool.IntegrityCheck();

			//if(!(loop_counter % 5))
			//{
			//	sInfoCore.TimeoutSockets();
			//	sSocketGarbageCollector.Update();
			//	CheckForDeadSockets();			  // Flood Protection
			//UNIXTIME = time(NULL);
			//g_localTime = *localtime(&UNIXTIME);
			//}

			//PatchMgr::getSingleton().UpdateJobs();

			//wprintf_s(L"MCS Running.\n");
			MC.Check();

			Sleep(200);
		}

		//sLog.outString("Shutting down...");
		wprintf_s(L"Shutting down....\n");
		signal(SIGINT, 0);
		signal(SIGTERM, 0);
		signal(SIGABRT, 0);
#ifdef _WIN32
		signal(SIGBREAK, 0);
#else
		signal(SIGHUP, 0);
#endif
	}

	MC.Stop();

}
