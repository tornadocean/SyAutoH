#pragma once
class CTaskTrans
{
public:
	CTaskTrans(int nID, int nFoupBarCode, int nTarget);
	~CTaskTrans(void);
private:
	int m_nID;
	int m_nFoupBarCode;
	int m_nTarget;
public:
	void Run(void);
};

