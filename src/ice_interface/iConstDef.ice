// **********************************************************************
//
// Copyright (c) 2003-2009 ZeroC, Inc. All rights reserved.
//
// This copy of Ice is licensed to you under the terms described in the
// ICE_LICENSE file included in this distribution.
//
// **********************************************************************

#ifndef CONSTDEF_ICE
#define CONSTDEF_ICE


module MCS
{
	module dbcli
	{
		enum LoctionType
		{
			loctypeNone,
			loctypeStocker,
			loctypeOHT,
			loctypeBuff,
			loctypeProcessDev,
		};
	};
	module GuiHub
	{
			enum MesTransCtrl
			{
				transRun,
				transPause,
				transContinue,
				transStop,
			};
			
			enum GuiCommand
			{ 
				OhtPosTime, 
				OhtStatusTime, 
				OhtGetPosTable,
				OhtFoupHanding,
				OhtSetPath,
				OhtMove,
				
				OhtPathTest,
				OhtMoveTest,
				OhtFoupTest,
				
				StkHandFoup,
				StkInquiryStatus,
				StkInquiryRoom,
				StkInquiryStorage,
				StkInquiryInputStatus,
				StkFoupHistory,
				StkAlarmHistory,
				StkStatusTime, 
				StkSetFoupInfoBackTime,

				StkGetRoomStatus,

				StkGetFoupInSys,
				
				MesGetFoupTable,
				MesGetPosTable,
				MesFoupTransfer,
				MesTransControl,
				
			};
			
			
			enum PushData
			{
				upOhtInfo,
				upOhtPos,
				upOhtPosTable,
				upOhtStatus,
				
				upStkInfo,
				upStkFoupsInfo,
				upStkLastOptFoup,
				upStkStatus,
				upStkInputStatus,

				upStkFoupInSys,
				upStkRoomStatus,
				
				upMesFoupTable,
				upMesPosTable,
			};
			sequence<PushData> GuiPushDataList;
		
	};

};

#endif
