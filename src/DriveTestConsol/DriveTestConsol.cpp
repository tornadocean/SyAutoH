// DriveTestConsol.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"
#include "../AMHSDrive/AMHSDrive.h"

int _tmain(int argc, _TCHAR* argv[])
{
	CAMHSDrive amhsDev;
	amhsDev.Init();
	amhsDev.SetOHTLocation( 200);


	getchar();
	amhsDev.Clean();

	return 0;
}
