// ���� ifdef ���Ǵ���ʹ�� DLL �������򵥵�
// ��ı�׼�������� DLL �е������ļ��������������϶���� VIRUALAMHS_EXPORTS
// ���ű���ġ���ʹ�ô� DLL ��
// �κ�������Ŀ�ϲ�Ӧ����˷��š�������Դ�ļ��а������ļ����κ�������Ŀ���Ὣ
// VIRUALAMHS_API ������Ϊ�Ǵ� DLL ����ģ����� DLL ���ô˺궨���
// ������Ϊ�Ǳ������ġ�
#ifdef VIRUALAMHS_EXPORTS
#define VIRUALAMHS_API __declspec(dllexport)
#else
#define VIRUALAMHS_API __declspec(dllimport)
#endif

#pragma once

class VirtualOHT;
class VirtualStocker;
#include <map>
#include <list>
typedef std::map<int, VirtualOHT*> MAP_VOHT;
typedef std::map<int, VirtualStocker*> MAP_VSTK;

typedef struct
{
	int nID;
	int nPosition;
	int nHandStatus;
	int nOnline;
	int nPosTime;
	int nStatusTime;
} ItemOHT;
typedef struct
{
	int nID;
	int nOnline;
	int nStatus;
	int nContain;
} ItemStocker;
typedef std::list<ItemOHT> LIST_OHT;
typedef std::list<ItemStocker> LIST_STOCKER;
typedef std::map<int,ItemStocker*> MAP_ItemStocker;
typedef std::map<int, ItemOHT*> MAP_ItemOHT;

typedef struct
{
	//TCHAR FoupID[256];
	int nStockerID;
	int nID;
	int nProcessStatus;
	int nRoomID;
	int nBatchID;
	int nDisabled;
} ItemFoup;
typedef std::list<ItemFoup> LIST_FOUP;
//zhang add the code in 2012.10.24
typedef std::map<int,ItemFoup*> MAP_ItemFoup;

class VIRUALAMHS_API CVirtualAMHS {
public:
	CVirtualAMHS(void);
	~CVirtualAMHS();

	// device auth
	int OHT_Auth(int nIndex,DWORD nPos = 0, int nHand = 0);
    int OHT_Init(int nIndex,int posTime,int statusTime);
	int Stocker_Auth(int nIndex, const char* sIP);
	int OHT_SetConstSpeed(int nSpeed,int nIdex = -1);

	int OHT_Offline(int nIndex);
	int Stocker_Offline(int nIndex);

	// for Stocker
	LIST_FOUP Stocker_GetFoupsStatus(int nStocker);
	int Stocker_ManualInputFoup(int nStocker,const TCHAR* sFoupID,int nBatchID);
	int Stocker_ManualOutputFoup(int nStocker,const TCHAR* nFoupID);
	int STK_FoupInitRoom(int nStockerID,ItemFoup *pFoup);
	int STK_History(int nStocker);
	int STK_AuthFoup(int nStocker);
	//int STK_SetFoupNum(int nIndex,int nContain);
	//int STK_GetFoup(int nSTK_ID,int nFoupID,int nBatchID);
	int STK_GetRoomID(int nSTK_ID,int nFoupID);
	int STK_FoupChangeType(int nStockerID); 
	ItemFoup STK_GetChangedFoup(int nStockerID);
	// for OHT
	LIST_OHT OHT_GetStatus();
	LIST_STOCKER Stocker_GetInfo();
	int SetTeachPosition(int nID, int nPos, int nType, int nSpeedRate);
	//LIST_OHTTime OHT_GetTimes();

private:
	MAP_VOHT* m_mapOHT;
	MAP_VSTK*	 m_mapSTK; 
	//LIST_OHTTime* m_listTime;
};
