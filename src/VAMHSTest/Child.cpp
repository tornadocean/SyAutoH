// Child.cpp : 实现文件
//

#include "stdafx.h"
#include "VAMHSTest.h"
#include "Child.h"
#include "afxdialogex.h"
#include <iostream>
using namespace std;

// Child 对话框

IMPLEMENT_DYNAMIC(Child, CDialogEx)

Child::Child(CWnd* pParent /*=NULL*/)
	: CDialogEx(Child::IDD, pParent)
{

}

Child::~Child()
{

}

void Child::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST1, m_TeachPos_List);
	DDX_Control(pDX, IDC_COMBO_OHT_TeachPOSType, m_TeachPosType);
	DDX_Control(pDX, IDC_ID, m_ID);
	DDX_Control(pDX, IDC_EDIT2, m_POS);
	DDX_Control(pDX, IDC_EDIT3, m_Speed);
}

BEGIN_MESSAGE_MAP(Child, CDialogEx)
	ON_BN_CLICKED(IDC_CREATEBUTTON2, &Child::OnBnClickedCreatebutton2)
	ON_BN_CLICKED(IDC_DELETEBUTTON, &Child::OnBnClickedDeletebutton)
END_MESSAGE_MAP()

// Child 消息处理程序
BOOL Child::OnInitDialog()
{
	CDialogEx::OnInitDialog();
	InitList();
	InitComBox();
	ReadOHTXML();
	return true;

}
void Child::InitComBox()
{
	m_TeachPosType.AddString(L"0x01 直道位置点");
	m_TeachPosType.AddString(L"0x02 弯道位置点");
	m_TeachPosType.AddString(L"0x04 道岔位置点");
	m_TeachPosType.AddString(L"0x08 减速点");
	m_TeachPosType.AddString(L"0x10 停止点");
	m_TeachPosType.AddString(L"0x20 取放点");
	m_TeachPosType.AddString(L"0x40 暂存柜取放点");
	m_TeachPosType.AddString(L"0x80 Stocker取放点");
}
void Child::InitList()
{
	DWORD dwStyle;
	dwStyle = m_TeachPos_List.GetStyle();  //取得样式
	dwStyle = LVS_EX_GRIDLINES | LVS_EX_FULLROWSELECT | LVS_EX_DOUBLEBUFFER;   //添加样式
	m_TeachPos_List.SetExtendedStyle(dwStyle);     //重新设置

	m_TeachPos_List.InsertColumn(0, _T("OHT ID"), LVCFMT_CENTER, 60);
	m_TeachPos_List.InsertColumn(1, _T("POS"), LVCFMT_CENTER, 100);
	m_TeachPos_List.InsertColumn(2, _T("Type"), LVCFMT_CENTER, 80);
	m_TeachPos_List.InsertColumn(3, _T("Speed"), LVCFMT_CENTER, 80);
}
CStringW Child::GetXMLPath()
{
	TCHAR path[200];
	GetModuleFileName(NULL,path,200);
	wstring ws = path;
	size_t nBar = ws.find_last_of('\\') + 1;
	ws = ws.substr(0, nBar);
	CStringW csw = ws.c_str();
	return csw;
}
CString Child::GetType(int num)
{
	CString Type;
	switch(num)
		{
		case(0x01):
			Type = (_T("直道位置点"));
			break;
		case(0x02):
			Type = (_T("弯道位置点"));
			break;
		case(0x04):
			Type = (_T("道岔位置点"));
			break;
		case(0x08):
			Type = (_T("减速点"));
			break;
		case(0x10):
			Type = (_T("停止点"));
			break;
		case(0x20):
			Type = (_T("取放点"));
			break;
		case(0x40):
			Type = (_T("暂存柜取放点"));
			break;
		case(0x80):
			Type = (_T("Stocker取放点"));
		}
	return Type;
}
void Child::ReadOHTXML()
{
	CMarkup XML;
	CString path = GetXMLPath();
	path += "../Config/VAMHSTest.xml";
	if(!XML.Load(path))
	{
		XML.SetDoc(_T("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n"));
		XML.AddElem(_T("OHTangTeachPosList"));
		XML.IntoElem();
		XML.AddElem(_T("OHTList"));
		XML.AddElem(_T("TeachPosList"));
	}
	XML.ResetMainPos();
	XML.FindElem();
	XML.FindChildElem(_T("TeachPosList"));
	XML.IntoElem();
	int i = 0;
	while(XML.FindChildElem(_T("TeachPos")))
	{
		XML.IntoElem();
		XML.FindChildElem(_T("DeviceID"));
		XML.IntoElem();
		CString CID = XML.GetData();
		XML.OutOfElem();
		XML.FindChildElem(_T("POS"));
		XML.IntoElem();
		CString CPOS = XML.GetData();
		XML.OutOfElem();
		XML.FindChildElem(_T("Type"));
		XML.IntoElem();
		CString CType = XML.GetData();
		int num = _ttoi(CType);
		CString Type;
		Type = GetType(num);
		XML.OutOfElem();
		XML.FindChildElem(_T("Speed"));
		XML.IntoElem();
		CString CSpeed = XML.GetData();
		XML.OutOfElem();
		XML.OutOfElem();
		CString str;
		m_TeachPos_List.InsertItem(i+1, str);
		m_TeachPos_List.SetItemText(i,0,CID);
		m_TeachPos_List.SetItemText(i,1,CPOS);
		m_TeachPos_List.SetItemText(i,2,Type);
		m_TeachPos_List.SetItemText(i,3,CSpeed);
		i++;
	}
	XML.OutOfElem();
}
void Child::DeleteXMLElem(CString ID,CString pos)
{
	CStringW path = GetXMLPath();
	path += "../Config/VAMHSTest.xml";
	CMarkup XML;
	XML.Load(path);
	XML.ResetMainPos();
	XML.FindElem();
	XML.FindChildElem(_T("TeachPosList"));
	XML.IntoElem();
	while(XML.FindChildElem(_T("TeachPos")))
	{
		XML.IntoElem();
		XML.FindChildElem(_T("DeviceID"));
		XML.IntoElem();
		CString xID = XML.GetData();
		XML.OutOfElem();
		XML.FindChildElem(_T("POS"));
		XML.IntoElem();
		CString xPos = XML.GetData();
		XML.OutOfElem();
		XML.OutOfElem();
		if((xID == ID) && (xPos == xPos))
			XML.RemoveChildElem();
	}
	XML.OutOfElem();
	XML.Save(path);
}

void Child::SaveOHTXML(CString nID,CString nPos,CString nType,CString nSpeed)
{
	CStringW path = GetXMLPath();
	path += "../Config/VAMHSTest.xml";
	CMarkup XML;
	XML.Load(path);
	XML.ResetMainPos();
	XML.FindElem();
	XML.FindChildElem(_T("TeachPosList"));
	XML.IntoElem();
	XML.AddChildElem(_T("TeachPos"));
	XML.IntoElem();
	XML.AddChildElem(_T("DeviceID"),nID);
	XML.AddChildElem(_T("POS"),nPos);
	XML.AddChildElem(_T("Type"),nType);
	XML.AddChildElem(_T("Speed"),nSpeed);
	XML.OutOfElem();
	XML.OutOfElem();
	XML.Save(path);
}

void Child::OnBnClickedCreatebutton2()
{
	// TODO: 在此添加控件通知处理程序代码
	CString nPos;
	CString nSpeed;
	CString nID;
	GetDlgItemText(IDC_ID,nID);
	GetDlgItemText(IDC_POS,nPos);
	GetDlgItemText(IDC_Speed,nSpeed);
	int nTypeString =m_TeachPosType.GetCurSel();
	int nType = 0;
	CString DType;
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
	case 6:
		nType = 0x40;
		break;
	case 7:
		nType = 0x80;
		break;
	default:
		nType = 0x01;
		break;
	}
	CString sType;
	sType.Format(_T("%d"),nType);
	DType = GetType(nType);
	SaveOHTXML(nID,nPos,sType,nSpeed);
	CString str;
	int ncount = m_TeachPos_List.GetItemCount();
	m_TeachPos_List.InsertItem(ncount,str);
	m_TeachPos_List.SetItemText(ncount,0,nID);
	m_TeachPos_List.SetItemText(ncount,1,nPos);
	m_TeachPos_List.SetItemText(ncount,2,DType);
	m_TeachPos_List.SetItemText(ncount,3,nSpeed);
}


void Child::OnBnClickedDeletebutton()
{
	// TODO: 在此添加控件通知处理程序代码
	CString str;
    int nId;
    POSITION pos = m_TeachPos_List.GetFirstSelectedItemPosition();
    if(pos==NULL)
    {
		MessageBox(_T("请选择一项"));
		return;
	}
	nId=(int)m_TeachPos_List.GetNextSelectedItem(pos);
	CString ID = m_TeachPos_List.GetItemText(nId,0);
	CString Pos = m_TeachPos_List.GetItemText(nId,1);
	DeleteXMLElem(ID,Pos);
	m_TeachPos_List.DeleteItem(nId);
}
