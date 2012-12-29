// VAMHSTestDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "VAMHSTest.h"
#include "VAMHSTestDlg.h"
#include "afxdialogex.h"
#include "../VirtualAMHS/VirtualAMHS.h"
#include "CMarkup.h"
#include <iostream>
#include "DlgAddOht.h"
using namespace std;
#ifdef _DEBUG
#define new DEBUG_NEW
#endif

CVirtualAMHS* g_pVDev = NULL;
MAP_ItemOHT g_mapOHTs;
MAP_ItemStocker g_mapStockers;
MAP_ItemFoup g_mapFoups;
const int STOCKER_ID = 24;

// 用于应用程序“关于”菜单项的 CAboutDlg 对话框

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// 对话框数据
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

// 实现
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// CVAMHSTestDlg 对话框



CVAMHSTestDlg::CVAMHSTestDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CVAMHSTestDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

CVAMHSTestDlg::~CVAMHSTestDlg()
{
	MAP_ItemOHT::iterator it;
	for(it = g_mapOHTs.begin();it != g_mapOHTs.end();it++)
	{
		it->second->nOnline = 0;
	}
	SaveOHTXML();
	MAP_ItemStocker::iterator ite;
	
	for(ite = g_mapStockers.begin();ite != g_mapStockers.end();ite++)
	{
		ite->second->nOnline = 0;
	}
	SaveSTKXML();
	
	it = g_mapOHTs.begin();
	while(it != g_mapOHTs.end())
	{
		delete it->second;
		++it;
	}
	g_mapOHTs.clear();
	ite = g_mapStockers.begin();
	while(ite != g_mapStockers.end())
	{
		delete ite->second;
		++ite;
	}
	g_mapStockers.clear();
}

void CVAMHSTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST_OHT, m_listCtrlOHT);
	DDX_Control(pDX, IDC_LIST_FOUP, m_listCtrlFOUP);
	DDX_Control(pDX, IDC_COMBO_OHT_TeachPOSType, m_cbOhtTeachType);
	DDX_Control(pDX, IDC_EDIT1, m_ConstSpeed);
	DDX_Control(pDX, IDC_LIST_FOUP2, m_listCtrlSTOCKER);
	DDX_Control(pDX, IDC_STOCKER_ID_EDIT, m_stockerIDEdit);
}

BEGIN_MESSAGE_MAP(CVAMHSTestDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BN_AddOHT, &CVAMHSTestDlg::OnBnClickedBnOHTonline)
	ON_WM_DESTROY()
	ON_BN_CLICKED(IDC_BN_AddSTK, &CVAMHSTestDlg::OnBnClickedBnAddstk)
	ON_BN_CLICKED(IDC_BN_STK_IN, &CVAMHSTestDlg::OnBnClickedBnStkIn)
	ON_BN_CLICKED(IDC_BN_STK_OUT, &CVAMHSTestDlg::OnBnClickedBnStkOut)
	ON_BN_CLICKED(IDC_BN_OHT_Add, &CVAMHSTestDlg::OnBnClickedBnOhtAdd)
	ON_BN_CLICKED(IDC_BN_SetHand, &CVAMHSTestDlg::OnBnClickedBnSethand)
	ON_BN_CLICKED(IDC_BN_SetPos, &CVAMHSTestDlg::OnBnClickedBnTeachPos)
	ON_WM_TIMER()
	ON_BN_CLICKED(IDC_BN_STK_HISTORY, &CVAMHSTestDlg::OnBnClickedBnStkHistory)
	ON_BN_CLICKED(IDC_BN_OHT_OFF, &CVAMHSTestDlg::OnBnClickedBnOhtOff)
	ON_BN_CLICKED(IDC_TeachPOS_Edit, &CVAMHSTestDlg::OnBnClickedTeachposEdit)
	ON_BN_CLICKED(IDC_OHT_DEL, &CVAMHSTestDlg::OnBnClickedOhtDel)
	ON_BN_CLICKED(IDC_SENDALL_BUTTON, &CVAMHSTestDlg::OnBnClickedSendallButton)
	ON_BN_CLICKED(IDC_SPEED_SET_BUTTON, &CVAMHSTestDlg::OnBnClickedSpeedSetButton)
	ON_BN_CLICKED(IDC_BN_ALLOHTOnLine, &CVAMHSTestDlg::OnBnClickedBnAllohtonline)
	ON_BN_CLICKED(IDC_ADD_STOCKER_BUTTON, &CVAMHSTestDlg::OnBnClickedAddStockerButton)
	ON_NOTIFY(NM_CLICK, IDC_LIST_FOUP2, &CVAMHSTestDlg::OnNMClickListFoup2)
	ON_BN_CLICKED(IDC_BN_AddSTK2, &CVAMHSTestDlg::OnBnClickedBnAddstk2)
	ON_BN_CLICKED(IDC_STK_ALL_ONLINE_BUTTON, &CVAMHSTestDlg::OnBnClickedStkAllOnlineButton)
	ON_BN_CLICKED(IDC_DELETE_STK_BUTTON, &CVAMHSTestDlg::OnBnClickedDeleteStkButton)
END_MESSAGE_MAP()

// CVAMHSTestDlg 消息处理程序

BOOL CVAMHSTestDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// 将“关于...”菜单项添加到系统菜单中。

	// IDM_ABOUTBOX 必须在系统命令范围内。
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO: 在此添加额外的初始化代码
	InitListCtrlOHT();
	InitListCtrlFOUP();
	InitListCtrlSTOCKER();
	AllocConsole();                     // 打开控制台资源
	FILE* file;
	freopen_s( &file, "CONOUT$", "w+t", stdout);// 申请写
	g_pVDev = new CVirtualAMHS();
	SetTimer(100, 500, NULL);

	m_cbOhtTeachType.AddString(L"0x01 直道位置点");
	m_cbOhtTeachType.AddString(L"0x02 弯道位置点");
	m_cbOhtTeachType.AddString(L"0x04 道岔位置点");
	m_cbOhtTeachType.AddString(L"0x08 减速点");
	m_cbOhtTeachType.AddString(L"0x10 停止点");
	m_cbOhtTeachType.AddString(L"0x20 取放点");
	DisplaySpeed();

	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

void CVAMHSTestDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// 如果向对话框添加最小化按钮，则需要下面的代码
//  来绘制该图标。对于使用文档/视图模型的 MFC 应用程序，
//  这将由框架自动完成。

void CVAMHSTestDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 用于绘制的设备上下文

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// 使图标在工作区矩形中居中
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// 绘制图标
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

//当用户拖动最小化窗口时系统调用此函数取得光标
//显示。
HCURSOR CVAMHSTestDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void CVAMHSTestDlg::OnBnClickedBnOHTonline()
{
	int nSpeed = GetSpeed();
	int nOHT_ID = GetSelectOhtID();
	int nPosTime;
	int nPosition;
	int nStatusTime;
	int nHand;
	CMarkup XML;
	CString path;
	path = GetPath();
	path += "../Config/VAMHSTest.xml";
	XML.Load(path);
	XML.FindElem();
	XML.FindChildElem(_T("OHTList"));
	XML.IntoElem();
	while(XML.FindChildElem(_T("OHT")))
	{
		XML.IntoElem();
		int nID = GetElemData(XML,_T("ID"));
		if(nID == nOHT_ID)
		{
			nPosition = GetElemData(XML,_T("POS"));
			nHand = GetElemData(XML,_T("HAND"));
			nPosTime = GetElemData(XML,_T("PosTime"));
			nStatusTime = GetElemData(XML,_T("StatusTime"));
		}
		XML.OutOfElem();
	}
	XML.OutOfElem();
	if (nOHT_ID >= 0)
	{
		int nAdd = g_pVDev->OHT_Auth(nOHT_ID,nPosition,nHand);
		int nInit = g_pVDev->OHT_Init(nOHT_ID,nPosTime,nStatusTime);
	    int speedSet = g_pVDev->OHT_SetConstSpeed(nSpeed,nOHT_ID);
	}
}

void CVAMHSTestDlg::AddFoupXMLElem(int STK_ID,int Foup_ID,ItemFoup* pFoup)
{
	CString path = GetPath();
	path += "../Config/VAMHSTest.xml";
	CMarkup xml;
	xml.Load(path);
	xml.FindElem();
	xml.FindChildElem(_T("StockerList"));
	xml.IntoElem();
	while(xml.FindChildElem(_T("Stocker")))
	{
		xml.IntoElem();
	    xml.FindChildElem(_T("StockerInfo"));
	    xml.IntoElem();
	    xml.FindChildElem(_T("ID"));
		xml.IntoElem();
		CString xSTK_ID = xml.GetData();
		int nSTK_ID = _ttoi(xSTK_ID);
		xml.OutOfElem();
		xml.OutOfElem();
		if(nSTK_ID != STK_ID)
			xml.OutOfElem();
		else
		{
			xml.AddChildElem(_T("Foup"));
	        xml.IntoElem();
	        xml.AddChildElem(_T("ID"),pFoup->nID);
			xml.AddChildElem(_T("Location"),pFoup->nRoomID);
	        xml.AddChildElem(_T("Status"),pFoup->nProcessStatus);
			xml.AddChildElem(_T("BatchID"),pFoup->nBatchID);
			xml.Save(path);
			return;
		}
	}
}

void CVAMHSTestDlg::OnDestroy()
{
	CDialogEx::OnDestroy();

	// TODO: 在此处添加消息处理程序代码
	//SaveXML();

	if (g_pVDev != NULL)
	{
		delete g_pVDev;
		g_pVDev = NULL;
	}
	FreeConsole();// 释放控制台资源
}

void CVAMHSTestDlg::OnBnClickedBnAddstk()
{
	int nSTK_ID = GetSelectStockerID();
	MAP_ItemStocker::iterator it;
	if(nSTK_ID >= 0)
	{
		it = g_mapStockers.find(nSTK_ID);
		g_pVDev->Stocker_Auth(nSTK_ID,"192.168.55.10");
		m_listCtrlFOUP.DeleteAllItems();
		ReadFOUPXML(nSTK_ID);
		//g_pVDev->STK_AuthFoup(nSTK_ID);
		//g_pVDev->STK_SetFoupNum(nSTK_ID,it->second->nContain);
	}
		return;
}

void CVAMHSTestDlg::OnBnClickedBnStkIn()
{
	CString selectStocker;
	GetDlgItemText(IDC_SELECT_STK_EDIT,selectStocker);
	if(selectStocker == "")
	{
		MessageBox(_T("Please select the Stocker First!"));
		return;
	}
	CString strFoup;
	GetDlgItemText(IDC_EDIT_STK_FOUP, strFoup);
	int Foup_ID = _ttoi(strFoup);
	
	MAP_ItemFoup::iterator it;
	it = g_mapFoups.find(Foup_ID);
	if(it != g_mapFoups.end())
	{
		if(it->second->nDisabled == 1)
			MessageBox(_T("This Foup is disabled!"));
		else
			MessageBox(_T("Foup 已存在！"));
		return ;
	}
	else 
	{
		MAP_ItemStocker::iterator it;
		it = g_mapStockers.find(selectSTK);
		if(it->second->nOnline == 1)
		{
			//foupNum++;
			g_pVDev->Stocker_ManualInputFoup(selectSTK, strFoup,0);
			//g_pVDev->STK_SetFoupNum(selectSTK,foupNum);
			int nRoomID = g_pVDev->STK_GetRoomID(selectSTK,Foup_ID);
			ItemFoup* item = new ItemFoup;
            item->nID = Foup_ID;
		    item->nRoomID = nRoomID;
		    item->nProcessStatus = 0;
			item->nBatchID = 0;
			item->nStockerID = selectSTK;
		    g_mapFoups.insert(std::make_pair(Foup_ID,item));
		    CString str;
		    m_listCtrlFOUP.InsertItem(0,str);
		    SetFOUPListItemData(item,0);
		    AddFoupXMLElem(selectSTK,Foup_ID,item);
		}
		else 
			MessageBox(_T("Stocker is not online!"));
	}
}

void CVAMHSTestDlg::OnBnClickedBnStkOut()
{
	CString str;
	GetDlgItemText(IDC_SELECT_STK_EDIT,str);
	if(str == "")
	{
		MessageBox(_T("Please Select the Stocker first!"));
		return ;
	}
	int nFoup_ID = GetSelectFoupID();
	CString strFoup;
	strFoup.Format(_T("%d"),nFoup_ID);
	POSITION pos = m_listCtrlFOUP.GetFirstSelectedItemPosition();
    if(pos==NULL)
    {
		MessageBox(_T("请至少选择一项"));
		return;
	}
	//foupNum--;
	int nId=(int)m_listCtrlFOUP.GetNextSelectedItem(pos);

	MAP_ItemStocker::iterator it;
	it = g_mapStockers.find(selectSTK);
	if(it->second->nOnline == 1)
	{
		m_listCtrlFOUP.DeleteItem(nId);
	    MAP_ItemFoup::iterator it;
	    it = g_mapFoups.find(nFoup_ID);
	    g_mapFoups.erase(it);
	    g_pVDev->Stocker_ManualOutputFoup(selectSTK, strFoup);
	    //g_pVDev->STK_SetFoupNum(selectSTK,foupNum);
	    DeleteFoupXML(selectSTK,nFoup_ID);
	}
	else
	{
		MessageBox(_T("Stocker selected is not online!"));
	} 
}

int CVAMHSTestDlg::GetElemData(CMarkup xml,CString tag)
{
	xml.FindChildElem(tag);
	xml.IntoElem();
	CString data = xml.GetData();
	int value = _ttoi(data);
	xml.OutOfElem();
	return value;
}

void CVAMHSTestDlg::SaveOHTXML()
{
	CStringW filePath = GetPath();
	filePath += "../Config/VAMHSTest.xml";
	CMarkup XML;
	XML.Load(filePath);
	MAP_ItemOHT::iterator it;
	for(it = g_mapOHTs.begin();it != g_mapOHTs.end();it++)
	{
		int nPosTime;
		int nStatusTime;
		int nID = it->second->nID;
		bool contain = false;
		XML.ResetMainPos();
	    XML.FindElem();
	    XML.FindChildElem(_T("OHTList"));
	    XML.IntoElem();
		while(XML.FindChildElem(_T("OHT")))
		{
			XML.FindChildElem(_T("ID"));
			XML.IntoElem();
			int xID = GetElemData(XML,_T("ID"));
			XML.OutOfElem();
			if(xID == nID)
			{
				contain = true;
				XML.FindChildElem(_T("PosTime"));
				XML.IntoElem();
				CString CPosTime = XML.GetData();
				nPosTime = _ttoi(CPosTime);
				XML.OutOfElem();
				XML.FindChildElem(_T("StatusTime"));
				XML.IntoElem();
				CString CStatusTime;
				CStatusTime = XML.GetData();
				nStatusTime = _ttoi(CStatusTime);
				XML.OutOfElem();
				XML.RemoveChildElem();
				XML.AddChildElem(_T("OHT"));
				XML.IntoElem();
				XML.AddChildElem(_T("ID"),xID);
				XML.AddChildElem(_T("POS"),it->second->nPosition);
				XML.AddChildElem(_T("HAND"),it->second->nHandStatus);
				XML.AddChildElem(_T("Online"),it->second->nOnline);
		        if(it->second->nPosTime > 0)
			    {
					XML.AddChildElem(_T("PosTime"),it->second->nPosTime);
			    }
				else if(nPosTime <= 0)
					XML.AddChildElem(_T("PosTime"),nPosTime);
			    if(it->second->nStatusTime > 0)
			    {
					XML.AddChildElem(_T("StatusTime"),it->second->nStatusTime);
			    }
				else if(nStatusTime <= 0)
				{
					XML.AddChildElem(_T("StatusTime"),nStatusTime);
				}
				XML.OutOfElem();
				XML.OutOfElem();
			}
		}
		if(!contain)
		{
			XML.AddChildElem(_T("OHT"));
			XML.IntoElem();
			XML.AddChildElem(_T("ID"),it->second->nID);
			XML.AddChildElem(_T("POS"),it->second->nPosition);
			XML.AddChildElem(_T("HAND"),it->second->nHandStatus);
			XML.AddChildElem(_T("Online"),it->second->nOnline);
			if(it->second->nPosTime > 0)
				XML.AddChildElem(_T("PosTime"),it->second->nPosTime);
			else
				XML.AddChildElem(_T("PosTime"),0);
			if(it->second->nStatusTime > 0)
				XML.AddChildElem(_T("StatusTime"),it->second->nStatusTime);
			else
				XML.AddChildElem(_T("StatusTime"),0);
			XML.OutOfElem();
			XML.OutOfElem();
		}
	}
	XML.OutOfElem();
	XML.Save(filePath);
}

void CVAMHSTestDlg::ReadOHTXML()
{
	CStringW filePath = GetPath();
	filePath += "../Config/VAMHSTest.xml";
	CMarkup XML;
	if(!XML.Load(filePath))
	{
		XML.SetDoc(_T("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n")); 
		XML.AddElem(_T("VAMHSTestList"));
		XML.IntoElem();
		XML.AddElem(_T("OHTList"));
		XML.AddElem(_T("TeachPosList"));
		XML.AddElem(_T("StockerList"));
		XML.AddElem(_T("ConstSpeed"));
		XML.AddChildElem(_T("Speed"),100);
		XML.Save(filePath);
		return ;
	}
	XML.ResetMainPos();
	XML.FindElem();
	XML.FindChildElem(_T("OHTList"));
	XML.IntoElem();
	while(XML.FindChildElem(_T("OHT")))
	{
		XML.IntoElem();  //into OHT
		ItemOHT* pOht = new ItemOHT;
		int ID = GetElemData(XML,_T("ID"));
		int POS = GetElemData(XML,_T("POS"));
		int HAND = GetElemData(XML,_T("HAND"));
		int Online = 0;//GetElemData(XML,_T("Online"));
		int posTime = GetElemData(XML,_T("PosTime"));
		int statusTime = GetElemData(XML,_T("StatusTime"));
		pOht->nHandStatus = HAND;
		pOht->nID = ID;
		pOht->nOnline = Online;
		pOht->nPosition = POS;
		pOht->nPosTime = posTime;
		pOht->nStatusTime = statusTime;
		g_mapOHTs.insert(std::make_pair(ID, pOht));
		CString str;
		m_listCtrlOHT.InsertItem(0, str);
		SetOHTListItemData(pOht, 0);
		XML.OutOfElem();
	}
	XML.OutOfElem();
}

void CVAMHSTestDlg::ReadSTKXML()
{
	CMarkup xml;
	CString path = GetPath();
	path += "../Config/VAMHSTest.xml";
	xml.Load(path);
	xml.FindElem();
	xml.FindChildElem(_T("StockerList"));
	xml.IntoElem();
	while(xml.FindChildElem(_T("Stocker")))
	{
		xml.IntoElem();
		xml.FindChildElem(_T("StockerInfo"));
		xml.IntoElem();
		ItemStocker* item = new ItemStocker;
		int nID = GetElemData(xml,_T("ID"));
		int nStatus = GetElemData(xml,_T("Status"));
		int nContain = GetElemData(xml,_T("nContain"));
		int nOnline = 0;//GetElemData(xml,_T("Online"));
		item->nID = nID;
		item->nStatus = nStatus;
		item->nContain = nContain;
		item->nOnline = nOnline;
		g_mapStockers.insert(std::make_pair(nID,item));
		CString str;
		m_listCtrlSTOCKER.InsertItem(0, str);
		SetStockerListItemData(item, 0);
		xml.OutOfElem();
		xml.OutOfElem();
	}
	xml.OutOfElem();
}

void CVAMHSTestDlg::SaveSTKXML()
{
	CMarkup xml;
	CString path = GetPath();
	path += "../Config/VAMHSTest.xml";
	xml.Load(path);
	MAP_ItemStocker::iterator it;
	for(it = g_mapStockers.begin();it != g_mapStockers.end();it++)
	{
		int nID = it->second->nID;
		bool contain = false;
	    xml.ResetMainPos();
	    xml.FindElem();
	    xml.FindChildElem(_T("StockerList"));
		xml.IntoElem();
		while(xml.FindChildElem(_T("Stocker")))
		{
			xml.IntoElem();
			xml.FindChildElem(_T("StockerInfo"));
			xml.IntoElem();
			xml.FindChildElem(_T("ID"));
			xml.IntoElem();
			CString CID = xml.GetData();
			int xID = _ttoi(CID);
			xml.OutOfElem();
			xml.OutOfElem();
			if(xID != nID)
			{
				xml.OutOfElem();
			}
			else
			{
				contain = true;
				xml.RemoveChildElem();
				xml.InsertChildElem(_T("StockerInfo"));
				xml.IntoElem();
				xml.AddChildElem(_T("ID"),xID);
				xml.AddChildElem(_T("Status"),it->second->nStatus);
				xml.AddChildElem(_T("nContain"),it->second->nContain);
				xml.AddChildElem(_T("Online"),it->second->nOnline);
				xml.OutOfElem();
				xml.OutOfElem();
				xml.OutOfElem();
			}
		}
		if(!contain)
		{
			xml.AddChildElem(_T("Stocker"));
			xml.IntoElem();
			xml.AddChildElem(_T("StockerInfo"));
			xml.IntoElem();
			xml.AddChildElem(_T("ID"),it->second->nID);
			xml.AddChildElem(_T("Status"),it->second->nStatus);
			xml.AddChildElem(_T("Contain"),it->second->nContain);
			xml.AddChildElem(_T("Online"),it->second->nOnline);
			xml.OutOfElem();
			xml.OutOfElem();
		}
	}
	xml.OutOfElem();
	xml.Save(path);
}

void CVAMHSTestDlg::ReadFOUPXML(int STK_ID)
{
	//foupNum = 0;
	CMarkup xml;
	CString path = GetPath();
	path += "../Config/VAMHSTest.xml";
	xml.Load(path);
	xml.FindElem();
	xml.FindChildElem(_T("StockerList"));
	xml.IntoElem();
	while(xml.FindChildElem(_T("Stocker")))
	{
		xml.IntoElem();
	    xml.FindChildElem(_T("StockerInfo"));
	    xml.IntoElem();
	    xml.FindChildElem(_T("ID"));
	    xml.IntoElem();
	    CString CID = xml.GetData();
	    int nID = _ttoi(CID);
	    xml.OutOfElem();
	    xml.OutOfElem();
		if(nID != STK_ID)
			xml.OutOfElem();
		else
	    {
			while(xml.FindChildElem(_T("Foup")))
		    {
				//foupNum++;
			    xml.IntoElem();
			    ItemFoup* item = new ItemFoup;
		        int nID = GetElemData(xml,_T("ID"));
		        int nRoomID = GetElemData(xml,_T("Location"));
		        int nStatus = GetElemData(xml,_T("Status"));
				int nBatchID = GetElemData(xml,_T("BatchID"));
			    item->nID = nID;
			    item->nRoomID = nRoomID;
			    item->nProcessStatus = nStatus;
				item->nBatchID = nBatchID;
				item->nStockerID = STK_ID;
			    g_mapFoups.insert(std::make_pair(nID,item));
				g_pVDev->STK_FoupInitRoom(STK_ID,item);
			    CString str;
			    m_listCtrlFOUP.InsertItem(0,str);
			    SetFOUPListItemData(item,0);
		        xml.OutOfElem();
		       //xml.OutOfElem();
			}
		}
	}
	xml.OutOfElem();
}

void CVAMHSTestDlg::DeleteSTKXML(int STK_ID)
{
	CString path = GetPath();
	path += "../Config/VAMHSTest.xml";
	CMarkup xml;
	xml.Load(path);
	xml.FindElem();
	xml.FindChildElem(_T("StockerList"));
	xml.IntoElem();
	while(xml.FindChildElem(_T("Stocker")))
	{
		xml.IntoElem();
		xml.FindChildElem(_T("StockerInfo"));
		xml.IntoElem();
		xml.FindChildElem(_T("ID"));
		xml.IntoElem();
		CString CID = xml.GetData();
		int nID = _ttoi(CID);
		xml.OutOfElem();
		xml.OutOfElem();
		xml.OutOfElem();
		if(nID == STK_ID)
		{
			xml.RemoveChildElem();
			xml.Save(path);
			return ; 
		}
	}
}

void CVAMHSTestDlg::DeleteFoupXML(int STK_ID,int Foup_ID)
{
	CString path = GetPath();
	path += "../Config/VAMHSTest.xml";
	CMarkup xml;
	xml.Load(path);
	xml.FindElem();
	xml.FindChildElem(_T("StockerList"));
	xml.IntoElem();
	while(xml.FindChildElem(_T("Stocker")))
	{
		xml.IntoElem();
		xml.FindChildElem(_T("StockerInfo"));
		xml.IntoElem();
		xml.FindChildElem(_T("ID"));
		xml.IntoElem();
		CString CSTK_ID = xml.GetData();
	    int nSTK_ID = _ttoi(CSTK_ID);
		xml.OutOfElem();
		xml.OutOfElem();
		if(nSTK_ID != STK_ID)
			xml.OutOfElem();
		else
		{
			while(xml.FindChildElem(_T("Foup")))
			{
				xml.IntoElem();
			    xml.FindChildElem(_T("ID"));
				xml.IntoElem();
			    CString CFoup_ID = xml.GetData();
			    int nFoup_ID = _ttoi(CFoup_ID);
			    xml.OutOfElem();
				xml.OutOfElem();
			    if(nFoup_ID == Foup_ID)
				{
					xml.RemoveChildElem();
					xml.Save(path);
					return;
				}
			}
		}
	}
}

void CVAMHSTestDlg::DeleteElem(int nID)
{
	CMarkup XML;
	CString path;
	path = GetPath();
	path += "../Config/VAMHSTest.xml";
	XML.Load(path);
	XML.FindElem();
	XML.FindChildElem(_T("OHTList"));
	XML.IntoElem();
	while(XML.FindChildElem(_T("OHT")))
	{
		XML.IntoElem();
		int xID = GetElemData(XML,_T("ID"));
		if(xID == nID)
		{
			XML.OutOfElem();
			XML.RemoveChildElem();
		}
		XML.OutOfElem();
	}
	XML.Save(path);
}

CStringW CVAMHSTestDlg::GetPath()
{
	TCHAR path[200];
	GetModuleFileName(NULL,path,200);
	wstring ws = path;
	size_t nBar = ws.find_last_of('\\') + 1;
	ws = ws.substr(0, nBar);
	CStringW csw = ws.c_str();
	return csw;
}

int CVAMHSTestDlg::GetSpeed()
{
	CMarkup xml;
	CString path = GetPath();
	path += "../Config/VAMHSTest.xml";
	xml.Load(path);
	xml.FindElem();
	if(xml.FindChildElem(_T("ConstSpeed")))
	{
		xml.IntoElem();
		xml.FindChildElem(_T("Speed"));
		xml.IntoElem();
		CString cSpeed = xml.GetData();
		int nSpeed = _ttoi(cSpeed);
		return nSpeed;
	}
	else 
		return 0;
}

void CVAMHSTestDlg::DisplaySpeed()
{
	int speed = GetSpeed();
	CString CSpeed;
	CSpeed.Format(_T("%d"),speed);
    SetDlgItemText(IDC_SPEED_SET_EDIT,CSpeed);
}

void CVAMHSTestDlg::OnBnClickedBnOhtAdd()
{
	CDlgAddOht dlgAddOht;
	if(dlgAddOht.DoModal() == IDOK)
	{
		int nOHT_ID = dlgAddOht.OhtID();
		MAP_ItemOHT::iterator it = g_mapOHTs.find(nOHT_ID);
		MAP_ItemOHT::iterator itEnd = g_mapOHTs.end();
		if (it != g_mapOHTs.end())
		{
			MessageBox(_T("OHT己存在!"));
		}
		else
		{
			// add
			ItemOHT* pOht = new ItemOHT;
			g_mapOHTs.insert(std::make_pair(nOHT_ID, pOht));
			pOht->nID = nOHT_ID;
			pOht->nPosition = 0;
			pOht->nHandStatus = 0;
			pOht->nOnline = 0;

			CString str;
			m_listCtrlOHT.InsertItem(0, str);
			SetOHTListItemData(pOht, 0);
		}
	}
}

void CVAMHSTestDlg::OnBnClickedBnSethand()
{
	// TODO: 在此添加控件通知处理程序代码
	int hand_Set = GetDlgItemInt(IDC_EDIT_OHT_HAND);
	if((hand_Set != 0) && (hand_Set != 1))
		MessageBox(_T("Set the wrong hand,it should be 0 or 1 !"));
	int nOHT_ID = GetSelectOhtID();
	MAP_ItemOHT::iterator it = g_mapOHTs.find(nOHT_ID);
	it->second->nHandStatus = hand_Set;
	SaveOHTXML();
}

void CVAMHSTestDlg::OnBnClickedBnTeachPos()
{
	int nPos = 0;
	int nSpeed = 0;

	nPos = GetDlgItemInt(IDC_EDIT_OHT_POS);
	nSpeed  = GetDlgItemInt(IDC_EDIT_OHT_SPEED);
	int nTypeString = m_cbOhtTeachType.GetCurSel();
	int nType = 0;
	switch(nTypeString)
	{
	case 0:
		nType = 0x01;
		break;
	case 1:
		nType = 0x02;
		break;
	case 2:
		nType = 0x04;
		break;
	case 3:
		nType = 0x08;
		break;
	case 4:
		nType = 0x10;
		break;
	case 5:
		nType = 0x20;
		break;
	default:
		nType = 0x01;
		break;
	}

	int nOhtID = 0;
	nOhtID = GetSelectOhtID();
	CStringW filePath = GetPath();
	filePath += "../Config/VAMHSTest.xml";
	CMarkup xml;
	xml.Load(filePath);
	xml.ResetMainPos();
	xml.FindElem();
	//xml.IntoElem();
	xml.FindChildElem(_T("TeachPosList"));
	xml.IntoElem();
    xml.AddChildElem(_T("TeachPos"));//add the elem failed 

	xml.IntoElem();
	xml.AddChildElem(_T("DeviceID"),nOhtID);
	xml.AddChildElem(_T("POS"),nPos);
	CString Type;
	Type.Format(_T("%d"),nType);
	xml.AddChildElem(_T("Type"),Type);
	xml.AddChildElem(_T("Speed"),nSpeed);
	xml.OutOfElem();
	xml.OutOfElem();
	xml.Save(filePath);
	g_pVDev->SetTeachPosition(nOhtID, nPos, nType, nSpeed);
	//m_Dialog.DoModal();
}

void CVAMHSTestDlg::InitListCtrlOHT(void)
{
	DWORD dwStyle;
	dwStyle = m_listCtrlOHT.GetStyle();  //取得样式
	dwStyle =    LVS_EX_GRIDLINES | LVS_EX_FULLROWSELECT
		| LVS_EX_DOUBLEBUFFER;   //添加样式
	m_listCtrlOHT.SetExtendedStyle(dwStyle);     //重新设置

	m_listCtrlOHT.InsertColumn(0, _T("ID"), LVCFMT_CENTER, 30);
	m_listCtrlOHT.InsertColumn(1, _T("POS"), LVCFMT_CENTER, 80);
	m_listCtrlOHT.InsertColumn(2, _T("HAND"), LVCFMT_CENTER, 50);
	m_listCtrlOHT.InsertColumn(3, _T("Status"), LVCFMT_CENTER, 50);
	m_listCtrlOHT.InsertColumn(4, _T("Online"), LVCFMT_CENTER, 50);
	ReadOHTXML();
}

void CVAMHSTestDlg::InitListCtrlFOUP(void)
{
	DWORD dwStyle;
	dwStyle = m_listCtrlFOUP.GetStyle();  //取得样式
	dwStyle = LVS_EX_GRIDLINES | LVS_EX_FULLROWSELECT ;   //添加样式
	m_listCtrlFOUP.SetExtendedStyle(dwStyle);     //重新设置
	m_listCtrlFOUP.InsertColumn(0, _T("ID"), LVCFMT_CENTER, 55);
	m_listCtrlFOUP.InsertColumn(1, _T("RoomID"), LVCFMT_CENTER, 65);
	m_listCtrlFOUP.InsertColumn(2, _T("Status"), LVCFMT_CENTER, 65);
	m_listCtrlFOUP.InsertColumn(3,_T("Lot"),LVCFMT_CENTER,65);
}

void CVAMHSTestDlg::InitListCtrlSTOCKER(void)
{
	DWORD dwStyle;
	dwStyle = m_listCtrlSTOCKER.GetStyle();
	dwStyle = LVS_EX_GRIDLINES | LVS_EX_FULLROWSELECT
		| LVS_EX_DOUBLEBUFFER;   //添加样式
	m_listCtrlSTOCKER.SetExtendedStyle(dwStyle);
	m_listCtrlSTOCKER.InsertColumn(0,_T("ID"),LVCFMT_CENTER,30);
	m_listCtrlSTOCKER.InsertColumn(1,_T("Status"),LVCFMT_CENTER,50);
	m_listCtrlSTOCKER.InsertColumn(2,_T("Contain"),LVCFMT_CENTER,60);
	m_listCtrlSTOCKER.InsertColumn(3,_T("Online"),LVCFMT_CENTER,50);
	ReadSTKXML();
}

void CVAMHSTestDlg::OnTimer(UINT_PTR nIDEvent)
{
	LIST_OHT ohts = g_pVDev->OHT_GetStatus();
	LIST_STOCKER stockers = g_pVDev->Stocker_GetInfo();
	LIST_FOUP foups = g_pVDev->Stocker_GetFoupsStatus(STOCKER_ID);

	LIST_OHT::iterator itOht = ohts.begin();
	while(itOht != ohts.end())
	{
		MAP_ItemOHT::iterator itMap = g_mapOHTs.find(itOht->nID);
		if (itMap != g_mapOHTs.end())
		{
			itMap->second->nHandStatus = itOht->nHandStatus;
			itMap->second->nPosition = itOht->nPosition;
			itMap->second->nOnline = itOht->nOnline;
			itMap->second->nPosTime = itOht->nPosTime;
			itMap->second->nStatusTime = itOht->nStatusTime;
		}
		++itOht;
	}

	int nCount = m_listCtrlOHT.GetItemCount();
	for (int i=0; i<nCount; i++)
	{
		int nOht = m_listCtrlOHT.GetItemData(i);
		MAP_ItemOHT::iterator it = g_mapOHTs.find(nOht);
		if (it != g_mapOHTs.end())
		{
			ItemOHT* pOht = it->second;
			SetOHTListItemData(pOht, i);
		}
	}
	
	int nChangeType = g_pVDev->STK_FoupChangeType(selectSTK);
	if(nChangeType != 0)
	{
		ItemFoup FoupItem;
		if(nChangeType == 1)
		{
			FoupItem = g_pVDev->STK_GetChangedFoup(selectSTK);
			//ItemFoup* FoupItem1 = &FoupItem;
			g_mapFoups.insert(std::make_pair(FoupItem.nID,&FoupItem));
			CString str;
			m_listCtrlFOUP.InsertItem(0,str);
			SetFOUPListItemData(&FoupItem,0);
			AddFoupXMLElem(selectSTK,FoupItem.nID,&FoupItem);
		}
		if(nChangeType == 2)
		{
			FoupItem = g_pVDev->STK_GetChangedFoup(selectSTK);
			MAP_ItemFoup::iterator it;
			it = g_mapFoups.find(FoupItem.nID);
			if(it != g_mapFoups.end())
			{
				int item;
				for(item = 0;FoupItem.nID != m_listCtrlFOUP.GetItemData(item);item++);
				m_listCtrlFOUP.DeleteItem(item);
				DeleteFoupXML(selectSTK,FoupItem.nID);
				g_mapFoups.erase(it);
			}
		}
	}

	LIST_STOCKER::iterator itStocker = stockers.begin();
	while(itStocker != stockers.end())
	{
		MAP_ItemStocker::iterator itMap = g_mapStockers.find(itStocker->nID);
		if(itMap != g_mapStockers.end())
		{
			itMap->second->nContain = itStocker->nContain;
			itMap->second->nOnline = itStocker->nOnline;
		}
		++itStocker;
	}
	int nSTKCount = m_listCtrlSTOCKER.GetItemCount();
	for (int i=0; i<nSTKCount; i++)
	{
		int nStocker = m_listCtrlSTOCKER.GetItemData(i);
		MAP_ItemStocker::iterator it = g_mapStockers.find(nStocker);
		if (it != g_mapStockers.end())
		{
			ItemStocker* pStocker = it->second;
			SetStockerListItemData(pStocker, i);
		}
	}

	SaveOHTXML();
	SaveSTKXML();
	CDialogEx::OnTimer(nIDEvent);
}

void CVAMHSTestDlg::SetOHTListItemData(ItemOHT* pOHT, int nListIndex)
{
	CString str;
	str.Format(_T("%d"), pOHT->nID);
	m_listCtrlOHT.SetItemText(nListIndex, 0, str);
	str.Format(_T("%d"), pOHT->nPosition);
	m_listCtrlOHT.SetItemText(nListIndex, 1, str);
	str.Format(_T("%d"), pOHT->nHandStatus);
	m_listCtrlOHT.SetItemText(nListIndex, 2, str);
	m_listCtrlOHT.SetItemText(nListIndex, 3, _T("Idle"));
	if (pOHT->nOnline > 0)
	{
		str = _T("On");
	}
	else
	{
		str = _T("Off");
	}
	m_listCtrlOHT.SetItemText(nListIndex, 4, str);
	m_listCtrlOHT.SetItemData(nListIndex, pOHT->nID);
}
void CVAMHSTestDlg::SetStockerListItemData(ItemStocker* pStocker,int nListIndex)
{ 
	CString str;
	str.Format(_T("%d"),pStocker->nID);
	m_listCtrlSTOCKER.SetItemText(nListIndex,0,str);
	m_listCtrlSTOCKER.SetItemText(nListIndex,1,_T("Idle"));
	str.Format(_T("%d"),pStocker->nContain);
	str += "/141";
	m_listCtrlSTOCKER.SetItemText(nListIndex,2,str);
	if (pStocker->nOnline > 0)
	{
		str = _T("On");
	}
	else
	{
		str = _T("Off");
	}
	m_listCtrlSTOCKER.SetItemText(nListIndex,3,str);
	m_listCtrlSTOCKER.SetItemData(nListIndex,pStocker->nID);
}

void CVAMHSTestDlg::SetFOUPListItemData(ItemFoup* pFoup,int nListIndex)
{
	CString str;
	str.Format(_T("%d"),pFoup->nID);
	m_listCtrlFOUP.SetItemText(nListIndex,0,str);
	str.Format(_T("%d"),pFoup->nRoomID);
	m_listCtrlFOUP.SetItemText(nListIndex,1,str);
	m_listCtrlFOUP.SetItemText(nListIndex,2,_T("working"));
	str.Format(_T("%d"),pFoup->nBatchID);
	m_listCtrlFOUP.SetItemText(nListIndex,3,str);
	m_listCtrlFOUP.SetItemData(nListIndex,pFoup->nID);
}
void CVAMHSTestDlg::OnBnClickedBnStkHistory()
{
	g_pVDev->STK_History(STOCKER_ID);
}


int CVAMHSTestDlg::GetSelectOhtID(void)
{
	int nRet = -1;
	int nItem = m_listCtrlOHT.GetNextItem(-1, LVNI_ALL | LVNI_SELECTED);
	if (nItem >= 0)
	{
		nRet = m_listCtrlOHT.GetItemData(nItem);
	}
	else
	{
		nRet = -1;
	}
	return nRet;
}
int CVAMHSTestDlg::GetSelectStockerID(void)
{
	int nRet = -1;
	int nItem = m_listCtrlSTOCKER.GetNextItem(-1, LVNI_ALL | LVNI_SELECTED);
	if (nItem >= 0)
	{
		nRet = m_listCtrlSTOCKER.GetItemData(nItem);
	}
	else
	{
		nRet = -1;
	}
	return nRet;
}
int CVAMHSTestDlg::GetSelectFoupID(void)
{
	int nRet = -1;
	int nItem = m_listCtrlFOUP.GetNextItem(-1, LVNI_ALL | LVNI_SELECTED);
	if (nItem >= 0)
	{
		nRet = m_listCtrlFOUP.GetItemData(nItem);
	}
	else
	{
		nRet = -1;
	}
	return nRet;
}

void CVAMHSTestDlg::OnBnClickedBnOhtOff()
{
	int nOHT_ID = GetSelectOhtID();
	if (nOHT_ID >= 0)
	{
		int nAdd = g_pVDev->OHT_Offline(nOHT_ID);
	}
}
void CVAMHSTestDlg::OnBnClickedTeachposEdit()
{
	// TODO: 在此添加控件通知处理程序代码
	m_Dialog.DoModal();
}


void CVAMHSTestDlg::OnBnClickedOhtDel()
{
	// TODO: 在此添加控件通知处理程序代码
	int OHT_ID = GetSelectOhtID();
    int nId;
    POSITION pos = m_listCtrlOHT.GetFirstSelectedItemPosition();
    if(pos==NULL)
    {
		MessageBox(_T("请至少选择一项"));
		return;
	}
	nId=(int)m_listCtrlOHT.GetNextSelectedItem(pos);
	m_listCtrlOHT.DeleteItem(nId);
	int nOFF = g_pVDev->OHT_Offline(OHT_ID);
	MAP_ItemOHT::iterator it = g_mapOHTs.find(OHT_ID);
	g_mapOHTs.erase(it);
	DeleteElem(OHT_ID);
}


void CVAMHSTestDlg::OnBnClickedSendallButton()
{
	// TODO: 在此添加控件通知处理程序代码
	bool isFind = false;
	int OHT_ID = GetSelectOhtID();
	if(OHT_ID < 0)
	{
		MessageBox(_T("Please select the OHT!"));
		return ; 
	}
	CString path = GetPath();
	path += "../Config/VAMHSTest.xml";
	CMarkup xml;
	xml.Load(path);
	xml.FindElem();
	xml.FindChildElem(_T("TeachPosList"));
	xml.IntoElem();
	while(xml.FindChildElem(_T("TeachPos")))
	{
		xml.IntoElem();
		int ID = GetElemData(xml,_T("DeviceID"));
		if(ID == OHT_ID)
		{
			isFind = true;
			int nPos = GetElemData(xml,_T("POS"));
			int nType = GetElemData(xml,_T("Type"));
			int nSpeed = GetElemData(xml,_T("Speed"));
			g_pVDev->SetTeachPosition(ID, nPos, nType, nSpeed);
		}
		xml.OutOfElem();
	}
	xml.OutOfElem();
	if(!isFind)
		MessageBox(_T("The OHT selected has no TeachPos,please send them after edited!"));
}

void CVAMHSTestDlg::OnBnClickedSpeedSetButton()
{
	// TODO: 在此添加控件通知处理程序代码
	int nSpeed = GetDlgItemInt(IDC_SPEED_SET_EDIT);
	int speedSet = g_pVDev->OHT_SetConstSpeed(nSpeed);
	CMarkup xml;
	CString path = GetPath();
	path += "../Config/VAMHSTest.xml";
	xml.Load(path);
	xml.FindElem();
	while(xml.FindChildElem(_T("ConstSpeed")))
	{
		xml.RemoveChildElem();
	}
	xml.AddChildElem(_T("ConstSpeed"));
	xml.IntoElem();
	xml.AddChildElem(_T("Speed"),nSpeed);
	xml.OutOfElem();
	xml.Save(path);
    DisplaySpeed();
}

void CVAMHSTestDlg::OnBnClickedBnAllohtonline()
{
	// TODO: 在此添加控件通知处理程序代码
    int nSpeed = GetSpeed();
	MAP_ItemOHT::iterator it;
	for(it = g_mapOHTs.begin();it != g_mapOHTs.end();it++)
	{
		if(it->second->nOnline != 1)
		{
			int nOHT_ID = it->second->nID;
			int nPosTime;
	        int nPosition;
	        int nStatusTime;
	        int nHand;
	        CMarkup XML;
	        CString path;
	        path = GetPath();
	        path += "../Config/VAMHSTest.xml";
	        XML.Load(path);
	        XML.FindElem();
	        XML.FindChildElem(_T("OHTList"));
	        XML.IntoElem();
	        while(XML.FindChildElem(_T("OHT")))
			{
				XML.IntoElem();
				int nID = GetElemData(XML,_T("ID"));
		        if(nID == nOHT_ID)
				{
					nPosition = GetElemData(XML,_T("POS"));
			        nHand = GetElemData(XML,_T("HAND"));
			        nPosTime = GetElemData(XML,_T("PosTime"));
			        nStatusTime = GetElemData(XML,_T("StatusTime"));
		        }
	        	XML.OutOfElem();
	        }
	        XML.OutOfElem();
	        if (nOHT_ID >= 0)
	        {
			    int nAdd = g_pVDev->OHT_Auth(nOHT_ID,nPosition,nHand);
		        int nInit = g_pVDev->OHT_Init(nOHT_ID,nPosTime,nStatusTime);
	            int speedSet = g_pVDev->OHT_SetConstSpeed(nSpeed,nOHT_ID);
	        }
		} 
	}
}


void CVAMHSTestDlg::OnBnClickedAddStockerButton()
{
	// TODO: 在此添加控件通知处理程序代码
	int nStocker_ID = GetDlgItemInt(IDC_STOCKER_ID_EDIT);
	if(nStocker_ID > 253 || nStocker_ID < 0)
	{
		MessageBox(_T("Stocker ID 应在0~253之间!"));
		return ;
	}
	MAP_ItemStocker::iterator it = g_mapStockers.find(nStocker_ID);
	MAP_ItemStocker::iterator itEnd = g_mapStockers.end();
	if (it != g_mapStockers.end())
	{
		MessageBox(_T("Stocker己存在!"));
	}
	else
	{
		// add
		selectSTK = nStocker_ID;
		CString stocker_select;
		stocker_select.Format(_T("%d"),nStocker_ID);
		SetDlgItemText(IDC_SELECT_STK_EDIT,stocker_select);
		m_listCtrlFOUP.DeleteAllItems();
		ItemStocker* pStocker = new ItemStocker;
		g_mapStockers.insert(std::make_pair(nStocker_ID, pStocker));
		pStocker->nID = nStocker_ID;
		pStocker->nStatus = 0;
		pStocker->nContain = 0;
		pStocker->nOnline = 0;
		SaveSTKXML();
		MAP_ItemFoup::iterator ite;
		int nFoupCount = 0;
		for(ite = g_mapFoups.begin();ite != g_mapFoups.end();ite++)
		{
			if(ite->second->nStockerID == nStocker_ID)
			{
				nFoupCount++;
				ite->second->nDisabled = 0;
				ItemFoup* item;
				item = ite->second;
				CString str;
		        m_listCtrlFOUP.InsertItem(0,str);
		        SetFOUPListItemData(item,0);
				AddFoupXMLElem(nStocker_ID,item->nID,item);
			}
		}

		pStocker->nContain = nFoupCount;
		pStocker->nID = nStocker_ID;
		pStocker->nStatus = 0;
		//pStocker->nContain = 0;
		pStocker->nOnline = 0;

		CString str;
		m_listCtrlSTOCKER.InsertItem(0, str);
		SetStockerListItemData(pStocker, 0);

	}
}

void CVAMHSTestDlg::OnNMClickListFoup2(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMITEMACTIVATE pNMItemActivate = reinterpret_cast<LPNMITEMACTIVATE>(pNMHDR);
	// TODO: 在此添加控件通知处理程序代码
	int nListIndex = pNMItemActivate->iItem;
	if(nListIndex >= 0)
	{
		int nStockerID = m_listCtrlSTOCKER.GetItemData(nListIndex);	
	    if(nStockerID != selectSTK)	
	    {	
			selectSTK = nStockerID;
		    m_listCtrlFOUP.DeleteAllItems();
		    ReadFOUPXML(selectSTK);	
		    CString CID;	
		    CID.Format(_T("%d"),nStockerID);	
		    SetDlgItemText(IDC_SELECT_STK_EDIT,CID);
	    }
	   // g_pVDev->STK_SetFoupNum(nStockerID,foupNum);
	}
	*pResult = 0;
}

void CVAMHSTestDlg::OnBnClickedBnAddstk2()
{
	// TODO: 在此添加控件通知处理程序代码
	int nSTK_ID = GetSelectStockerID();
	if(nSTK_ID >= 0)
	{
		int nOff = g_pVDev->Stocker_Offline(nSTK_ID);
	}
}


void CVAMHSTestDlg::OnBnClickedStkAllOnlineButton()
{
	// TODO: 在此添加控件通知处理程序代码
	MAP_ItemStocker::iterator it;
	for(it = g_mapStockers.begin();it != g_mapStockers.end();it++)
	{	
		int nSTOCKER_ID = it->second->nID;
		if(nSTOCKER_ID > 0)
		{
			g_pVDev->Stocker_Auth(nSTOCKER_ID, "192.168.55.10");
			m_listCtrlFOUP.DeleteAllItems();
			ReadFOUPXML(nSTOCKER_ID);
//			g_pVDev->STK_SetFoupNum(nSTOCKER_ID,it->second->nContain);
		}
	}
	return ;
}


void CVAMHSTestDlg::OnBnClickedDeleteStkButton()
{
	// TODO: 在此添加控件通知处理程序代码
	int nSTK_ID = GetSelectStockerID();
	int nId;
    POSITION pos = m_listCtrlSTOCKER.GetFirstSelectedItemPosition();
    if(pos == NULL)
    {
		MessageBox(_T("请至少选择一项"));
		return;
	}
	nId=(int)m_listCtrlSTOCKER.GetNextSelectedItem(pos);
	m_listCtrlSTOCKER.DeleteItem(nId);
	MAP_ItemStocker::iterator it;
	it = g_mapStockers.find(nSTK_ID);
	int nOff = g_pVDev->Stocker_Offline(nSTK_ID);
	g_mapStockers.erase(it);
	DeleteSTKXML(nSTK_ID);
	m_listCtrlFOUP.DeleteAllItems();
	MAP_ItemFoup::iterator ite;
	for(ite = g_mapFoups.begin();ite != g_mapFoups.end();ite++)
	{
		if(ite->second->nStockerID == nSTK_ID)
		{
			ite->second->nDisabled = 1;
		}
	}
}
