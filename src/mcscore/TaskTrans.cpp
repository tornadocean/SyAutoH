#include "StdAfx.h"
#include "TaskTrans.h"
#include "../SqlAceCli/SqlAceCli.h"
#include "PathProductor.h"


CTaskTrans::CTaskTrans(int nID, int nFoupBarCode, int nTarget)
	: m_nID(nID)
	, m_nFoupBarCode(nFoupBarCode)
	, m_nTarget(nTarget)
{
}


CTaskTrans::~CTaskTrans(void)
{
}


void CTaskTrans::Run(void)
{
	LOG_DEBUG("ID:%d, FOUP:%d, TARGET:%d", 
		m_nID, m_nFoupBarCode, m_nTarget);

	//DBTransfer db;
	//db.SetTransferStatus(m_nID, "Prepare");
	//Sleep(2000);
	//db.SetTransferStatus(m_nID, "Finished");
	
	// make path
	sLog.outDebug("Make Path.");
	int nFrom = 50;
	int nTo = 1050;

	auto path = sPathProductor.ProductPath(nFrom, nTo);
	cout<< "Start: " << nFrom << " To: " << nTo << endl;
	cout << "Path: " << endl;
	auto it = path.cbegin();
	if (it != path.cend())
	{
		cout << *it;
		for (++it;
			it != path.cend(); ++it)
		{
			cout<< "->" << *it;
		}
		cout<< endl;
	}
	//db.SetTransferStatus(m_nID, "Add");
	// make commands
	sLog.outDebug("Make Commands.");
	Sleep(500);
	// run commands
	sLog.outDebug("Run Commands.");
	Sleep(2000);
}
