// ���� ifdef ���Ǵ���ʹ�� DLL �������򵥵�
// ��ı�׼�������� DLL �е������ļ��������������϶���� AMHSDRIVE_EXPORTS
// ���ű���ġ���ʹ�ô� DLL ��
// �κ�������Ŀ�ϲ�Ӧ����˷��š�������Դ�ļ��а������ļ����κ�������Ŀ���Ὣ
// AMHSDRIVE_API ������Ϊ�Ǵ� DLL ����ģ����� DLL ���ô˺궨���
// ������Ϊ�Ǳ������ġ�
#ifdef AMHSDRIVE_EXPORTS
#define AMHSDRIVE_API __declspec(dllexport)
#else
#define AMHSDRIVE_API __declspec(dllimport)
#pragma comment(lib, "amhsdrive.lib")
#endif

#pragma  once
// �����Ǵ� AMHSDrive.dll ������
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
