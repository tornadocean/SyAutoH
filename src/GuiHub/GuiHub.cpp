// GuiHub.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "GuiHub.h"
#include "UserManagementServer.h"
#include "GuiDataHubServer.h"

// 这是已导出类的构造函数。
// 有关类定义的信息，请参阅 GuiHub.h
CGuiHub::CGuiHub()
	: 
m_pUserMgt(NULL),
m_pGuiDataHub(NULL)
{
	return;
}
CGuiHub::~CGuiHub()
{
	if (NULL != m_pUserMgt)
	{
		delete m_pUserMgt;
		m_pUserMgt = NULL;
	}

	if (NULL != m_pGuiDataHub)
	{
		delete m_pGuiDataHub;
		m_pGuiDataHub = NULL;
	}
}

int CGuiHub::StartServer(CAMHSDrive* pDrive)
{
	if (NULL == m_pUserMgt)
	{
		m_pUserMgt = new UserManagementServer();
		m_pUserMgt->InitIce();
	}

	if (NULL == m_pGuiDataHub)
	{
		m_pGuiDataHub = new GuiDataHubServer();
		m_pGuiDataHub->InitIce();
		m_pGuiDataHub->SetDrive(pDrive);
	}

	return 0;
}

void CGuiHub::SetData(MCS::GuiHub::PushData nTag, const char* chVal)
{
	if (NULL != m_pGuiDataHub)
	{
		m_pGuiDataHub->UpdateData(nTag, chVal);
	}
}