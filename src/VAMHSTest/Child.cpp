// Child.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "VAMHSTest.h"
#include "Child.h"
#include "afxdialogex.h"
#include <iostream>
using namespace std;

// Child �Ի���

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
	ON_BN_CLICKED(IDC_BUTTON3, &Child::OnBnClickedButton3)
	ON_BN_CLICKED(IDC_BUTTON2, &Child::OnBnClickedButton2)
END_MESSAGE_MAP()


// Child ��Ϣ��������
BOOL Child::OnInitDialog()
{
	CDialogEx::OnInitDialog();
	InitList();
	InitComBox();
	ReadXML();
	return true;

}
void Child::InitComBox()
{
	m_TeachPosType.AddString(L"0x01 ֱ��λ�õ�");
	m_TeachPosType.AddString(L"0x02 ���λ�õ�");
	m_TeachPosType.AddString(L"0x04 ����λ�õ�");
	m_TeachPosType.AddString(L"0x08 ���ٵ�");
	m_TeachPosType.AddString(L"0x10 ֹͣ��");
	m_TeachPosType.AddString(L"0x20 ȡ�ŵ�");
}
void Child::InitList()
{
	DWORD dwStyle;
	dwStyle = m_TeachPos_List.GetStyle();  //ȡ����ʽ
	dwStyle = LVS_EX_GRIDLINES | LVS_EX_FULLROWSELECT | LVS_EX_DOUBLEBUFFER;   //������ʽ
	m_TeachPos_List.SetExtendedStyle(dwStyle);     //��������

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
void Child::ReadXML()
{
	CMarkup XML;
	CString path = GetXMLPath();
	path += "../Config/TeachPOS.xml";
	if(!XML.Load(path))
	{
		XML.SetDoc(_T("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n"));
		XML.AddElem(_T("TeachPosList"));
	}
	XML.ResetMainPos();
	int i = 0;
	while(XML.FindChildElem(_T("TeachPOS")))
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
		switch(num)
		{
		case(1):
			Type = (_T("ֱ��λ�õ�"));
			break;
		case(2):
			Type = (_T("���λ�õ�"));
			break;
		case(4):
			Type = (_T("����λ�õ�"));
			break;
		case(8):
			Type = (_T("���ٵ�"));
			break;
		case(10):
			Type = (_T("ֹͣ��"));
			break;
		case(20):
			Type = (_T("��ŵ�"));
			break;
		}
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
}
void Child::DeleteXMLElem(CString ID,CString pos)
{
	CStringW path = GetXMLPath();
	path += "../Config/TeachPOS.xml";
	CMarkup XML;
	XML.Load(path);
	XML.ResetMainPos();
	while(XML.FindChildElem(_T("TeachPOS")))
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
}

void Child::OnBnClickedButton3()
{
	// TODO: �ڴ����ӿؼ�֪ͨ�����������
	CString ID;
	GetDlgItemText(IDC_ID, ID);
	MessageBox(ID);
}


void Child::OnBnClickedButton2()
{
	// TODO: �ڴ����ӿؼ�֪ͨ�����������
	CString str;
    int nId;
    POSITION pos = m_TeachPos_List.GetFirstSelectedItemPosition();
    if(pos==NULL)
    {
		MessageBox(_T("������ѡ��һ��"));
		return;
	}
	nId=(int)m_TeachPos_List.GetNextSelectedItem(pos);
	CString ID = m_TeachPos_List.GetItemText(nId,0);
	CString Pos = m_TeachPos_List.GetItemText(nId,1);
	DeleteXMLElem(ID,Pos);
	m_TeachPos_List.DeleteItem(nId);
}