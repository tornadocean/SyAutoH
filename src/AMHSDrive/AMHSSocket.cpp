#include "StdAfx.h"
#include "AMHSPacket.h"
#include "AMHSSocket.h"
#include "OptCodes.h"
#include <time.h>

//#pragma pack(push, 1)
//struct AMHSPktHeader
//{
//	uint8 comm;
//	uint32 cmd;
//	uint16 size;
//	uint16 index;
//	uint8  bLast;
//	uint8 reversed[4];
//};
//#pragma pack(pop)

AMHSSocket::AMHSSocket(SOCKET fd)
	:
Socket(fd, AMHSSOCKET_SENDBUF_SIZE, AMHSSOCKET_RECVBUF_SIZE),
	//Authed(false),
	mOpcode(0),
	mRemaining(0),
	mSize(0),
	//mSeed(RandomUInt()),
	mRequestID(0)
	//mSession(NULL),
	//pAuthenticationPacket(NULL),
	//_latency(0),
	//mQueued(false),
	//m_nagleEanbled(false),
	//m_fullAccountName(NULL)
{
}


AMHSSocket::~AMHSSocket(void)
{
}

void AMHSSocket::OnConnect()
{
	//sWorld.mAcceptedConnections++;
	//_latency = getMSTime();

	/*AMHSPacket wp(0x0801, 2);

	wp << uint8(254);
	wp << uint8(0);

	wp.hexlike();
	SendPacket(&wp);*/

	string sIP = this->GetRemoteIP();
	uint32 uPort = this->GetRemotePort();
	printf("OnConnect ---> IP: %s Port: %d\n", sIP.c_str(), uPort);
}

void AMHSSocket::OnDisconnect()
{
	//if(mSession)
	//{
	//	mSession->SetSocket(0);
	//	mSession = NULL;
	//}

	//if(mRequestID != 0)
	//{
	//	sLogonCommHandler.UnauthedSocketClose(mRequestID);
	//	mRequestID = 0;
	//}

	//if(mQueued)
	//{
	//	sWorld.RemoveQueuedSocket(this);	// Remove from queued sockets.
	//	mQueued = false;
	//}
	printf("Socket Disconnect\n");
}

void AMHSSocket::OnRead()
{
	for(;;)
	{
		// Check for the header if we don't have any bytes to wait for.
		if(mRemaining == 0)
		{
			int len = readBuffer.GetSize();
			if(readBuffer.GetSize() < 14)
			{
				// No header in the packet, let's wait.
				return;
			}

			// Copy from packet buffer into header local var
			AMHSPktHeader Header;
			readBuffer.Read((uint8*)&Header, sizeof(AMHSPktHeader));

			//mRemaining = mSize = ntohs(Header.size) - 4;
			mRemaining = mSize = Header.size;
			mOpcode = Header.cmd;
		}

		/*if (mSize <= 0)
		{
		return;
		}*/

		AMHSPacket* Packet;

		if(mRemaining > 0)
		{
			if(readBuffer.GetSize() < mRemaining)
			{
				// We have a fragmented packet. Wait for the complete one before proceeding.
				return;
			}
		}

		Packet = new AMHSPacket(static_cast<uint16>(mOpcode), mSize);
		Packet->resize(mSize);

		if(mRemaining > 0)
		{
			// Copy from packet buffer into our actual buffer.
			///Read(mRemaining, (uint8*)Packet->contents());
			readBuffer.Read((uint8*)Packet->contents(), mRemaining);
		}

		//sWorldLog.LogPacket(mSize, static_cast<uint16>(mOpcode), mSize ? Packet->contents() : NULL, 0, (mSession ? mSession->GetAccountId() : 0));
		mRemaining = mSize = mOpcode = 0;

		if (Packet->size() > 0)
		{
			
			string sIP = this->GetRemoteIP();
			uint32 uPort = this->GetRemotePort();
			printf("OnRead ---> IP: %s Port: %d\n", sIP.c_str(), uPort);
			printf("Command: 0x%0000X ", Packet->GetOpcode());
			Packet->hexlike();
		}


		// Check for packets that we handle
		switch(Packet->GetOpcode())
		{
		case OHT_AUTH:
			{
				uint8		ohtID = 0;
				uint16		ohtPosition = 0;
				uint8		ohtHand = 0;
				*Packet >> ohtID;
				*Packet >> ohtPosition;
				*Packet >> ohtHand;
				printf("OHT Auth  ---> id: %d, pos: %d, hand: %d\n", ohtID, ohtPosition, ohtHand);
				delete Packet;

				AMHSPacket ack(OHT_MCS_ACK_AUTH, 2);
				ack << uint8(ohtID);
				ack << uint8(1); // success
				SendPacket(&ack);

				__time64_t ltime;
				_time64( &ltime );
				char bufTime[256] = "";
				_ctime64_s(bufTime, &ltime);
				printf( "The time is %s\n", bufTime ); // C4996

			}
			break;
		case STK_AUTH:
			{
				uint8 stockerID = 0;
				uint32 uIP = 0;
				*Packet >> stockerID;
				*Packet >> uIP;
				struct in_addr addr1;
				
				memcpy(&addr1, &uIP, 4);
				char* sIP = inet_ntoa(addr1);
				printf("STOCKER Auth ---> id: %d, ip: %s\n", stockerID, sIP);
				delete Packet;

				AMHSPacket ack(STK_MCS_ACK_AUTH, 10);
				ack << uint8(stockerID);
				ack << uint8(1);
				__time64_t ltime;
				_time64( &ltime );
				ack << uint64(ltime);

				SendPacket(&ack);
			}
			break;
		default:
			{
				//if(mSession) mSession->QueuePacket(Packet);
				//else delete Packet;
				delete Packet;
			}
			break;
		}
	}
}


void AMHSSocket::OutPacket(uint16 opcode, size_t len, const void* data)
{
	OUTPACKET_RESULT res;
	/*if((len + 10) > WORLDSOCKET_SENDBUF_SIZE)
	{
	LOG_ERROR("WARNING: Tried to send a packet of %u bytes (which is too large) to a socket. Opcode was: %u (0x%03X)", (unsigned int)len, (unsigned int)opcode, (unsigned int)opcode);
	return;
	}*/

	res = _OutPacket(opcode, len, data);
	if(res == OUTPACKET_RESULT_SUCCESS)
		return;

	if(res == OUTPACKET_RESULT_NO_ROOM_IN_BUFFER)
	{
		/* queue the packet */
		queueLock.Acquire();
		AMHSPacket* pck = new AMHSPacket(opcode, len);
		if(len) pck->append((const uint8*)data, len);
		_queue.Push(pck);
		queueLock.Release();
	}
}

OUTPACKET_RESULT AMHSSocket::_OutPacket(uint16 opcode, size_t len, const void* data)
{
	bool rv;
	if(!IsConnected())
		return OUTPACKET_RESULT_NOT_CONNECTED;

	BurstBegin();
	//if((m_writeByteCount + len + 4) >= m_writeBufferSize)
	if(writeBuffer.GetSpace() < (len + 4))
	{
		BurstEnd();
		return OUTPACKET_RESULT_NO_ROOM_IN_BUFFER;
	}

	// Packet logger :)
	//sWorldLog.LogPacket((uint32)len, opcode, (const uint8*)data, 1, (mSession ? mSession->GetAccountId() : 0));

	// First, create the header.
	AMHSPktHeader Header;
	int n = sizeof(AMHSPktHeader);
	memset(&Header, 0, sizeof(AMHSPktHeader));
	

	Header.cmd = opcode;
	//Header.size = ntohs((uint16)len + 2);
	Header.size = len;

	// Pass the header to our send buffer
	rv = BurstSend((const uint8*)&Header, sizeof(AMHSPktHeader));

	// Pass the rest of the packet to our send buffer (if there is any)
	if(len > 0 && rv)
	{
		rv = BurstSend((const uint8*)data, (uint32)len);
	}

	if(rv) BurstPush();
	BurstEnd();
	return rv ? OUTPACKET_RESULT_SUCCESS : OUTPACKET_RESULT_SOCKET_ERROR;
}