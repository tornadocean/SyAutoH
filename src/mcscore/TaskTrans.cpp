#include "StdAfx.h"
#include "TaskTrans.h"
#include "../SqlAceCli/SqlAceCli.h"


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

	DBTransfer db;
	db.SetTransferStatus(m_nID, "Prepare");
	Sleep(2000);
	//db.SetTransferStatus(m_nID, "Finished");
	db.SetTransferStatus(m_nID, "Add");
}
