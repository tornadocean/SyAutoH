// ���� ifdef ���Ǵ���ʹ�� DLL �������򵥵�
// ��ı�׼�������� DLL �е������ļ��������������϶���� MCSCORE_EXPORTS
// ���ű���ġ���ʹ�ô� DLL ��
// �κ�������Ŀ�ϲ�Ӧ����˷��š�������Դ�ļ��а������ļ����κ�������Ŀ���Ὣ
// MCSCORE_API ������Ϊ�Ǵ� DLL ����ģ����� DLL ���ô˺궨���
// ������Ϊ�Ǳ������ġ�
#ifdef MCSCORE_EXPORTS
#define MCSCORE_API __declspec(dllexport)
#else
#define MCSCORE_API __declspec(dllimport)
#pragma comment(lib, "mcscore.lib")
#endif

#pragma once
#include <vector>
typedef std::vector<int> INT_LIST;

// �����Ǵ� mcscore.dll ������
class CAMHSDrive;
class MCSCORE_API CMCSCore {
public:
	CMCSCore(void);
	~CMCSCore();
	// TODO: �ڴ�������ķ�����

public:
	int Init(CAMHSDrive* pDrive);
	INT_LIST GetPath(int nFrom, int nTo);
	int Run(void);
	int Stop(void);
};
