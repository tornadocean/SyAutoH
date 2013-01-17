#include "StdAfx.h"
#include "MaterialController.h"
#include "../shared/AMHSPacket.h"
#include "OptCodes.h"
#include "../shared/Log.h"
#include "../common/iConstDef.h"

#include <iostream>
#include <string>

using namespace std;



MaterialController::MaterialController(void)
{
	sLog.Init(3);
}

MaterialController::~MaterialController(void)
{
	m_MesReciver.unhookEvent(&m_MesSource);
}


int MaterialController::Init(void)
{
	CoInitializeEx(NULL, COINIT_MULTITHREADED);

	m_MesReciver.m_pFoupDB = &m_FoupDB;
	m_MesLink.Init(&m_MesSource);
	m_MesReciver.hookEvent(&m_MesSource);
	m_GuiHub.StartServer(&m_amhsDrive);
	m_amhsDrive.Init();
	m_core.Init();

	return 0;
}


void MaterialController::Check(void)
{
	return;
	////m_amhsDrive.Run();
	////m_amhsDrive.SetOHTBackMessage(24, 200);
	////m_amhsDrive.Check();

	//DR_OHT_LIST oht_list = m_amhsDrive.GetOhtList();
	//string strOhtList = "";
	//char buf[256] ="";
	//for (DR_OHT_LIST::iterator it = oht_list.begin(); 
	//	it != oht_list.end(); ++it)
	//{
	//	sprintf_s(buf, 256, "<%d,%d,%d>", it->nID, it->nPOS, it->nHand);
	//	strOhtList += buf;
	//}
	//m_GuiHub.SetData(MCS::GuiHub::upOhtPos, strOhtList.c_str());

	//strOhtList = "";
	//for (DR_OHT_LIST::iterator it = oht_list.begin(); 
	//	it != oht_list.end(); ++it)
	//{
	//	sprintf_s(buf, 256, "<%d,%s,%u>", it->nID, it->strIp.c_str(), it->uPort);
	//	strOhtList += buf;
	//}
	//m_GuiHub.SetData(MCS::GuiHub::upOhtInfo, strOhtList.c_str());
}

const char chLogo[] = 
"\r\n"
 " ______                       __\r\n"
"/\\__  _\\       __          __/\\ \\__\r\n"
"\\/_/\\ \\/ _ __ /\\_\\    ___ /\\_\\ \\, _\\  __  __\r\n"
"   \\ \\ \\/\\`'__\\/\\ \\ /' _ `\\/\\ \\ \\ \\/ /\\ \\/\\ \\\r\n"
"    \\ \\ \\ \\ \\/ \\ \\ \\/\\ \\/\\ \\ \\ \\ \\ \\_\\ \\ \\_\\ \\\r\n"
"     \\ \\_\\ \\_\\  \\ \\_\\ \\_\\ \\_\\ \\_\\ \\__\\\\/`____ \\\r\n"
"      \\/_/\\/_/   \\/_/\\/_/\\/_/\\/_/\\/__/ `/___/> \\\r\n"
"                                 C O R E  /\\___/\r\n"
"Material Control System V1.0.0.1 \r\n";
void MaterialController::PrintfInfo(void)
{
	string strLogo = 
		"\r\n"
		"___                                                               \r\n"
		"| |\\ \\                                                                \r\n"
		"| | \\ \\                                                                 \r\n"
		"| |  \\ \\                                                                \r\n"
		"|_|   \\_/\\                                                               \r\n"
		"Material Control System V1.0.0.1 \r\n";
	//Log.outBasic(chLogo);
	Log.outBasic(strLogo.c_str());
}


int MaterialController::Run(void)
{
	m_core.Run();
	return 0;
}


int MaterialController::Stop(void)
{
	m_core.Stop();

	CoUninitialize();

	return 0;
}
