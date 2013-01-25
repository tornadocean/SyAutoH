#pragma once

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
private:
	int m_nID;
	int m_nFoupBarCode;
	int m_nTarget;
	std::vector<TranCommand> m_cmdList;
public:
	void Run(void);
};

