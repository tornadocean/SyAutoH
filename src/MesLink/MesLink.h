// 下列 ifdef 块是创建使从 DLL 导出更简单的
// 宏的标准方法。此 DLL 中的所有文件都是用命令行上定义的 MESLINK_EXPORTS
// 符号编译的。在使用此 DLL 的
// 任何其他项目上不应定义此符号。这样，源文件中包含此文件的任何其他项目都会将
// MESLINK_API 函数视为是从 DLL 导入的，而此 DLL 则将用此宏定义的
// 符号视为是被导出的。
#ifdef MESLINK_EXPORTS
#define MESLINK_API __declspec(dllexport)
#else
#define MESLINK_API __declspec(dllimport)
#endif

#pragma  once

class MESLINK_API CMesData
{
public:
	int nType;
	int nFoupID;
	int nEquID;
};

[event_source(native)]
class MesMsgSource 
{
public:
	__event void MESPickFoup(int nBarCode, int nDevID, int nDevType);
	__event void MESPlaceFoup(int nBarCode, int nDevID, int nDevType);
};

class MesLinkServer;
// 此类是从 MesLink.dll 导出的
class MESLINK_API CMesLink {
public:
	CMesLink(void);
	~CMesLink();
private:
	MesLinkServer* m_pMesServer;

public:
	int Init(MesMsgSource* src);
	int GetMesData(CMesData& data);
};

