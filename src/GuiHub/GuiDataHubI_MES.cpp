#include "StdAfx.h"
#include "GuiDataHubI.h"
#include "../AMHSDrive/AMHSDrive.h"
#include "../shared/Util.h"
#include "../shared/Log.h"
#include "../SqlAceCli/SqlAceCli.h"
#include "IceUtil/Unicode.h"
#include "iConstDef.h"

void GuiDataHubI::MES_GetPositionTable(const std::string&, const ::Ice::Current& current)
{
	vector<int> nTypes;
	nTypes.push_back(0x40);
	nTypes.push_back(0x20);
	nTypes.push_back(0x80);

	string strVal = _GetKeyPointsTable(nTypes);
	UpdateDataOne(current.con, GuiHub::upMesPosTable, strVal);
}

void GuiDataHubI::MES_GetFoupTable(const std::string&, const ::Ice::Current& current)
{
	DBFoup db;
	VEC_FOUP foupList = db.GetFoupAllTable();
	string strVal = "";
	char buf[100] = "";
	for (auto it = foupList.cbegin();
		it != foupList.cend(); ++it)
	{
		strVal += "<";
		strVal += itoa(it->nBarCode, buf, 10);
		strVal += ",";
		strVal += itoa(it->nLot, buf, 10);
		strVal += ",";
		strVal += itoa(it->locFoup.nCarrier, buf, 10);
		strVal += ",";
		strVal += itoa(it->locFoup.nPort, buf, 10);
		strVal += ",";
		strVal += itoa(it->locFoup.nLocation, buf, 10);
		strVal += ",";
		strVal += itoa(it->locFoup.nLocType, buf, 10);
		strVal += ",";
		strVal += itoa(it->nStatus, buf, 10);
		strVal += ">";
	}
	UpdateDataOne(current.con, GuiHub::upMesFoupTable, strVal);
}

void GuiDataHubI::MES_TransControl(const std::string&, const ::Ice::Current&)
{
	LOG_ERROR("todo");
}
void GuiDataHubI::MES_FoupTransfer(const std::string&, const ::Ice::Current&)
{
	LOG_ERROR("todo");
}