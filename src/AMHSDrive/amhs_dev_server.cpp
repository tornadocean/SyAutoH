#include "StdAfx.h"
#include "amhs_dev_server.h"

amhs_dev_server::amhs_dev_server(boost::asio::io_service& io_service,
	const tcp::endpoint& endpoint)
	: io_service_(io_service),
	acceptor_(io_service, endpoint)
{
	acceptor_.set_option(boost::asio::ip::tcp::acceptor::reuse_address(true));
	start_accept();
}

void amhs_dev_server::start_accept()
{
	amhs_session_ptr new_session(new amhs_session(io_service_, room_));
	acceptor_.async_accept(new_session->socket(),
		boost::bind(&amhs_dev_server::handle_accept, this, new_session,
		boost::asio::placeholders::error));
}

void amhs_dev_server::handle_accept(amhs_session_ptr session,
	const boost::system::error_code& error)
{
	if (!error)
	{
		cout<< "amhs_dev_server::accept link" << endl;
		session->start();
	}

	start_accept();
}

int amhs_dev_server::GetConnectCount()
{
	return room_.GetCount();
}

amhs_oht_vec amhs_dev_server::OHT_GetDataSet()
{
	return room_.GetOhtDataSet();
}

amhs_stocker_vec amhs_dev_server::STK_GetDataSet()
{
	return room_.GetStkDataSet();
}

amhs_foup_vec amhs_dev_server::STK_GetFoupDataSet(int nID)
{
	return room_.GetStkFoupDataSet(nID);
}

amhs_foup_vec amhs_dev_server::STK_GetEraseFoupDataSet(int nID)
{
	return room_.GetStkEraseFoupDataSet(nID);
}

amhs_foup_vec amhs_dev_server::STK_GetLastOptFoup(int nID)
{
	return room_.GetStkLastOptFoup(nID);
}

amhs_foup_vec amhs_dev_server::STK_GetFoupInSys()
{
	return room_.GetStkFoupInSys();
}

void amhs_dev_server::OHT_Set_StatusBackTime(int nID, int ms)
{
	AMHSPacket packet(OHT_MCS_STATUS_BACK_TIME, 2);
	packet<< uint8(nID);
	packet<< uint8(ms);

	SendPacket_OHT(nID, packet);
}

void amhs_dev_server::OHT_Set_PosBackTime(int nID, int ms)
{
	AMHSPacket packet(OHT_MCS_POSITION_BACK_TIME, 2);
	packet << uint8(nID);
	packet << uint8(ms);

	SendPacket_OHT(nID, packet);
}

void amhs_dev_server::OHT_Move(int nID, int nControl)
{
	AMHSPacket packet(OHT_MCS_MOVE, 2);
	packet << uint8(nID);
	packet << uint8(nControl);

	SendPacket_OHT(nID, packet);
}

void amhs_dev_server::OHT_SetPath(int nID, int nType, int nStart, int nTarget, amhs_keypoint_vec& KeyPoints)
{
	Log.outBasic("OHT Setpath");
	size_t szKeyCount = KeyPoints.size();
	size_t szPacketLen = 0;
	szPacketLen = 11 + szKeyCount * 6;

	AMHSPacket packet(OHT_MCS_PATH, szPacketLen);
	packet << uint8(nID);
	packet << uint8(nType);
	packet << uint32(nStart);
	packet << uint32(nTarget);
	packet << uint8(szKeyCount);
	
	for (amhs_keypoint_vec::iterator it = KeyPoints.begin(); 
		it != KeyPoints.end(); ++it)
	{
		packet << uint32(it->nPos);
		packet << uint8(it->nType);
		packet << uint8(it->nSpeedRate);
	}

	SendPacket_OHT(nID, packet);
}

void amhs_dev_server::OHT_Foup(int nID, int nDevBuf, int nOperation)
{
	AMHSPacket packet(OHT_MCS_FOUPHANDING, 3);
	packet << uint8(nID);
	packet << uint8(nDevBuf);
	packet << uint8(nOperation);

	SendPacket_OHT(nID, packet);
}

//////////////////////////////////////////////////////////////////////////
//
void amhs_dev_server::STK_FOUP(int nID, int nMode, int nPick, int nFoupData)
{
	AMHSPacket packet(STK_MCS_FOUP, 13);
	packet << uint8(nID);
	packet << uint8(nMode);
	packet << uint8(nPick);
	packet << uint64(nFoupData);
	packet << uint16(0);

	SendPacket_STK(nID, packet);
}

void amhs_dev_server::STK_Status(int nID)
{
	AMHSPacket packet(STK_MCS_STATUS, 1);
	packet << uint8(nID);

	SendPacket_STK(nID, packet);
}

void amhs_dev_server::STK_Room(int nID)
{
	AMHSPacket packet(STK_MCS_ROOM, 1);
	packet << uint8(nID);

	SendPacket_STK(nID, packet);
}

void amhs_dev_server::STK_Storage(int nID)
{
	AMHSPacket packet(STK_MCS_STORAGE, 1);
	packet << uint8(nID);

	SendPacket_STK(nID, packet);
}

void amhs_dev_server::STK_InputStatus(int nID)
{
	AMHSPacket packet(STK_MCS_INPUT_STATUS, 1);
	packet << uint8(nID);

	SendPacket_STK(nID, packet);
}

void amhs_dev_server::STK_History(int nID, const SYSTEMTIME &timeStart, const SYSTEMTIME &timeEnd)
{
	AMHSPacket packet(STK_MCS_HISTORY, 17);
	packet << uint8(nID);
	packet << uint16(timeStart.wYear);
	packet << uint8(timeStart.wMonth);
	packet << uint8(timeStart.wDay);
	packet << uint8(timeStart.wHour);
	packet << uint8(timeStart.wMinute);
	packet << uint8(timeStart.wSecond);
	packet << uint8(0);
	packet << uint16(timeEnd.wYear);
	packet << uint8(timeEnd.wMonth);
	packet << uint8(timeEnd.wDay);
	packet << uint8(timeEnd.wHour);
	packet << uint8(timeEnd.wMinute);
	packet << uint8(timeEnd.wSecond);
	packet << uint8(0);

	SendPacket_STK(nID, packet);
}

void amhs_dev_server::STK_Alarms(int nID, const SYSTEMTIME &timeStart, const SYSTEMTIME &timeEnd)
{
	AMHSPacket packet(STK_MCS_ALARMS, 17);
	packet << uint8(nID);
	packet << uint16(timeStart.wYear);
	packet << uint8(timeStart.wMonth);
	packet << uint8(timeStart.wDay);
	packet << uint8(timeStart.wHour);
	packet << uint8(timeStart.wMinute);
	packet << uint8(timeStart.wSecond);
	packet << uint8(0);
	packet << uint16(timeEnd.wYear);
	packet << uint8(timeEnd.wMonth);
	packet << uint8(timeEnd.wDay);
	packet << uint8(timeEnd.wHour);
	packet << uint8(timeEnd.wMinute);
	packet << uint8(timeEnd.wSecond);
	packet << uint8(0);

	SendPacket_STK(nID, packet);
}

void amhs_dev_server::STK_Set_StatusBackTime(int nID, int nSecond)
{
	AMHSPacket packet(STK_MCS_STATUS_BACK_TIME, 5);
	packet << uint8(nID);
	packet << uint32(nSecond);

	SendPacket_STK(nID, packet);
}

void amhs_dev_server::STK_Set_FoupBackTime(int nID, int nSecond)
{
	AMHSPacket packet(STK_MCS_FOUP_BACK_TIME, 5);
	packet << uint8(nID);
	packet << uint32(nSecond);

	SendPacket_STK(nID, packet);
}