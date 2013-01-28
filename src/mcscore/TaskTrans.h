#pragma once
#include "../AMHSDrive/AMHSDrive.h"
#include "../SqlAceCli/SqlAceCli.h"

struct TranCommand
{
	std::string m_strCommand;
	int nDevID;
	int nDevType;
	int nFrom;
	int nTo;
	int nPort;
	int nPosition;

	TranCommand()
		: m_strCommand("")
		, nDevID(0)
		, nDevType(0)
		, nFrom(0)
		, nTo(0)
		, nPort(0)
		, nPosition(0)
	{
	}
};

class CTaskTrans
{
public:
	CTaskTrans(int nID, int nFoupBarCode, int nTarget);
	~CTaskTrans(void);

public:
	void SetDrive(CAMHSDrive * pDrive)
	{ 
		m_amhsDrive = pDrive;
	}
private:
	CAMHSDrive* m_amhsDrive;
	int m_nID;
	int m_nFoupBarCode;
	int m_nFoupKeyPoint;
	int m_nTarget;
	std::vector<TranCommand> m_cmdList;
	FoupLocation m_locFoupSource;

public:
	void Run(void);
	void _findFoupSource(void);
	void _prepareFoup(void);
	void _findFoupPos(void);
	void _ohtMoveToFoupSource(void);
	void _ohtPickFoup(void);
	void _ohtMoveToTarget(void);
	void _ohtPlaceFoup(void);
	void _targetOperate(void);
};

