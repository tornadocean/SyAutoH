// 下列 ifdef 块是创建使从 DLL 导出更简单的
// 宏的标准方法。此 DLL 中的所有文件都是用命令行上定义的 AMHSDRIVE_EXPORTS
// 符号编译的。在使用此 DLL 的
// 任何其他项目上不应定义此符号。这样，源文件中包含此文件的任何其他项目都会将
// AMHSDRIVE_API 函数视为是从 DLL 导入的，而此 DLL 则将用此宏定义的
// 符号视为是被导出的。
#ifdef AMHSDRIVE_EXPORTS
#define AMHSDRIVE_API __declspec(dllexport)
#else
#define AMHSDRIVE_API __declspec(dllimport)
#pragma comment(lib, "amhsdrive.lib")
#endif

#pragma  once
// 此类是从 AMHSDrive.dll 导出的
#include <vector>
#include <map>

typedef struct  sVec_OHT
{
	int nID;
	int nPOS;
	int nHand;
	int nStatusTime;
	int nPosTime;
	int nPathResult;
	int nMoveStatus;
	int nMoveAlarm;
	int nFoupOpt;
	int nBackStatusMode;
	int nBackStatusMark;
	int nBackStausAlarm;
	bool  bNeedPath;
	string strIp;
	unsigned int uPort;
} driveOHT;

typedef std::vector<driveOHT> DR_OHT_LIST;

typedef struct sVec_Foup
{
	int nChaned;
	int nfoupRoom;
	int nBarCode;
	int nLot;
	int nInput;
} driveFOUP;

typedef std::vector<driveFOUP> DR_FOUP_LIST;

typedef struct sVec_STK
{
	int nID;
	int nStatus;
	int nAuto;
	int nManu;
	SYSTEMTIME last_opt_foup_time;
} driveSTK;

typedef std::vector<driveSTK> DR_STK_LIST;

typedef struct sPathKeyPoint
{
	int nPos;
	int nType;
	int nSpeedRate;
} keyPoint;
typedef std::vector<keyPoint> PATH_POINT_LIST;

class AMHSPacket;
class AMHSDRIVE_API CAMHSDrive {
public:
	CAMHSDrive(void);
	~CAMHSDrive();

	int Init();
	int Check();
	int Clean();

	DR_OHT_LIST GetOhtList();
	void OHTStatusBackTime(int nID, int ms);
	void OHTPosBackTime(int nID, int ms);
	void OHTMove(int nID, int nControl);
	void OHTFoup(int nID, int nDevBuf, int nOperation);
	void OHTSetPath(int nID, int nType, int nStart, int nTarget, PATH_POINT_LIST& KeyPoints);

	DR_STK_LIST GetStkList();
	DR_FOUP_LIST GetStkFoupList(int nID);
	DR_FOUP_LIST GetStkLastOptFoup(int nID);
	void STKCleanLastEvent(int nID);
	//void GetStkRoom(int nID, int room[141]);
	vector<int> GetStkRoom(int nID);
	void STKFoupHand(int nID, int nOpt, int nMode, int nData);
	void STKStockerStatus(int nID);
	void STKStockerRoom(int nID);
	void STKFoupStorage(int nID);
	void STKInputStatus(int nID);
	void STKHistory(int nID, const SYSTEMTIME &timeStart, const SYSTEMTIME &timeEnd);
	void STKAlarms(int nID, const SYSTEMTIME &timeStart, const SYSTEMTIME &timeEnd);
	void STKStatusBackTime(int nID, int ms);
	void STKFoupInfoBackTime(int nID,int ms);

	int SetOHTLocation(int nPoint);
};
