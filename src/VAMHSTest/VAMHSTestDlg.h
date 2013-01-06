
// VAMHSTestDlg.h : 头文件
//

#pragma once
#include "afxcmn.h"
#include "../VirtualAMHS/VirtualAMHS.h"
#include "../shared/Log.h"
#include "afxwin.h"
#include "Child.h"

// CVAMHSTestDlg 对话框
class CVAMHSTestDlg : public CDialogEx
{
// 构造
public:
	CVAMHSTestDlg(CWnd* pParent = NULL);	// 标准构造函数
	~CVAMHSTestDlg();

// 对话框数据
	enum { IDD = IDD_VAMHSTEST_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持


// 实现
protected:
	HICON m_hIcon;

	// 生成的消息映射函数
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	Child m_Dialog;
	afx_msg void OnBnClickedBnOHTonline();
	afx_msg void OnDestroy();
	afx_msg void OnBnClickedBnAddstk();
	afx_msg void OnBnClickedBnStkIn();
	afx_msg void OnBnClickedBnStkOut();
	afx_msg void OnBnClickedBnOhtAdd();
	afx_msg void OnBnClickedBnSethand();
	afx_msg void OnBnClickedBnTeachPos();
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	afx_msg void OnBnClickedBnStkHistory();
	afx_msg void OnBnClickedBnOhtOff();
	afx_msg void OnBnClickedTeachposEdit();
	afx_msg void OnBnClickedOhtDel();
	afx_msg void OnBnClickedSendallButton();
	afx_msg void OnBnClickedSpeedSetButton();
	afx_msg void OnBnClickedBnAllohtonline();
	afx_msg void OnBnClickedAddStockerButton();
	CListCtrl m_listCtrlOHT;
	CListCtrl m_listCtrlFOUP;
	CComboBox m_cbOhtTeachType;
	CEdit m_ConstSpeed;
	CListCtrl m_listCtrlSTOCKER;
	CEdit m_stockerIDEdit;
	void DisplaySpeed();
	void InitListCtrlOHT(void);
	void InitListCtrlFOUP(void);
	void InitListCtrlSTOCKER(void);
	void SetOHTListItemData(ItemOHT* pOHT, int nListIndex);
	void SetStockerListItemData(ItemStocker* pStocker,int nListIndex);
	void SetFOUPListItemData(ItemFoup* pFoup,int nListIndex);
	void SetStockerInputStatus(int nHandType,int nPosition);
	void DeleteElem(int nID);
	int GetElemData(CMarkup xml,CString tag);
	int GetSelectOhtID(void);
	int GetSelectFoupID(void);
	int GetSelectStockerID(void);
	int GetSpeed();
	void SaveOHTXML();
	void ReadOHTXML();
	void SaveSTKXML();
	void ReadSTKXML();
	void ReadFOUPXML(int STK_ID);
	void DeleteFoupXML(int STK_ID,int Foup_ID);
	void DeleteSTKXML(int STK_ID);
	void AddFoupXMLElem(int STK_ID,int Foup_ID,ItemFoup* pFoup);
	CStringW GetPath();
	int selectSTK;
	//int foupNum;
	afx_msg void OnNMClickListFoup2(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnBnClickedBnAddstk2();
	afx_msg void OnBnClickedStkAllOnlineButton();
	afx_msg void OnBnClickedDeleteStkButton();
	afx_msg void OnBnClickedClearFoupsButton();
	afx_msg void OnBnClickedSetStkposButton();
};
