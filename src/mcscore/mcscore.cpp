// mcscore.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "mcscore.h"
#include "PathProductor.h"
#include "Scheduler.h"

// 这是已导出类的构造函数。
// 有关类定义的信息，请参阅 mcscore.h
CMCSCore::CMCSCore()
{
	return;
}

CMCSCore::~CMCSCore()
{
	delete sPathProductor.getSingletonPtr();
	delete sScheduler.getSingletonPtr();
}

int CMCSCore::Init()
{
	new CPathProductor;
	new CScheduler;

	sScheduler.Init();

	sPathProductor.GetLaneData();
	{
		int nFrom = 50;
		int nTo = 1050;
		auto path = GetPath(nFrom, nTo);
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
	}

	{
		int nFrom = 1050;
		int nTo = 50;
		auto path = GetPath(nFrom, nTo);
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
	}

	return 0;
}

INT_LIST CMCSCore::GetPath(int nFrom, int nTo)
{
	INT_LIST list;
	list = sPathProductor.ProductPath(nFrom, nTo);
	return list;
}


int CMCSCore::Run(void)
{
	sScheduler.Run();

	return 0;
}


int CMCSCore::Stop(void)
{
	sScheduler.Stop();

	return 0;
}
