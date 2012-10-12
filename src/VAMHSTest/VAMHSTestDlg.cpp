
// VAMHSTestDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "VAMHSTest.h"
#include "VAMHSTestDlg.h"
#include "afxdialogex.h"
#include "../VirtualAMHS/VirtualAMHS.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

CVirtualAMHS* pVirualAMHSDevice = NULL;
const int StockerID = 24;

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

void CVAMHSTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CVAMHSTestDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BN_AddOHT, &CVAMHSTestDlg::OnBnClickedBnAddoht)
	ON_WM_DESTROY()
	ON_BN_CLICKED(IDC_BN_AddSTK, &CVAMHSTestDlg::OnBnClickedBnAddstk)
	ON_BN_CLICKED(IDC_BN_STK_IN, &CVAMHSTestDlg::OnBnClickedBnStkIn)
	ON_BN_CLICKED(IDC_BN_STK_OUT, &CVAMHSTestDlg::OnBnClickedBnStkOut)
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
	AllocConsole();                     // 打开控制台资源
	FILE* file;
	freopen_s( &file, "CONOUT$", "w+t", stdout);// 申请写

	pVirualAMHSDevice = new CVirtualAMHS();

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



void CVAMHSTestDlg::OnBnClickedBnAddoht()
{
	int nOHT_ID = GetDlgItemInt(IDC_EDIT_OHTID);
	if (nOHT_ID > 0 && nOHT_ID < 254)
	{
		int nAdd = pVirualAMHSDevice->AddOHT(nOHT_ID);
		if (0 == nAdd)
		{
			CString str;
			str.Format(_T("成功添加OHT %d"), nOHT_ID);
			//MessageBox(str);
			wprintf(str);
		}
	}
	else
	{
		MessageBox(_T("不合理的OHT ID值, 应在1至253之间."));
	}
}


void CVAMHSTestDlg::OnDestroy()
{
	CDialogEx::OnDestroy();

	// TODO: 在此处添加消息处理程序代码
	if (pVirualAMHSDevice != NULL)
	{
		delete pVirualAMHSDevice;
		pVirualAMHSDevice = NULL;
	}
	FreeConsole();                      // 释放控制台资源
}


void CVAMHSTestDlg::OnBnClickedBnAddstk()
{
	pVirualAMHSDevice->AddStocker(StockerID, "192.168.55.10");
}


void CVAMHSTestDlg::OnBnClickedBnStkIn()
{
	CString strFoup;
	GetDlgItemText(IDC_EDIT_STK_FOUP, strFoup);
	pVirualAMHSDevice->ManualInputFoup(StockerID, strFoup);
}


void CVAMHSTestDlg::OnBnClickedBnStkOut()
{
	CString strFoup;
	GetDlgItemText(IDC_EDIT_STK_FOUP, strFoup);
	pVirualAMHSDevice->ManualOutputFoup(StockerID, strFoup);

}
