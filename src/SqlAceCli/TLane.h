// TLane.h : CTableLane 的声明

#pragma once

// 代码生成在 2012年12月18日, 16:44

class CTableLaneAccessor
{
public:
	LONG m_id;
	LONG m_Start;
	LONG m_Finish;
	LONG m_Prev;
	LONG m_Next;
	LONG m_Next_Frok;
	LONG m_Length;
	LONG m_MapID;
	BYTE m_Type;
	VARIANT_BOOL m_Enable;

	// 以下向导生成的数据成员包含
	//列映射中相应字段的状态值。
	// 可以使用这些值保存数据库返回的 NULL 值或在编译器返回
	// 错误时保存错误信息。有关如何使用
	//这些字段的详细信息，
	// 请参见 Visual C++ 文档中的
	//“向导生成的访问器中的字段状态数据成员”。
	// 注意: 在设置/插入数据前必须初始化这些字段!

	DBSTATUS m_dwidStatus;
	DBSTATUS m_dwStartStatus;
	DBSTATUS m_dwFinishStatus;
	DBSTATUS m_dwPrevStatus;
	DBSTATUS m_dwNextStatus;
	DBSTATUS m_dwNext_FrokStatus;
	DBSTATUS m_dwLengthStatus;
	DBSTATUS m_dwMapIDStatus;
	DBSTATUS m_dwTypeStatus;
	DBSTATUS m_dwEnableStatus;

	// 以下向导生成的数据成员包含
	//列映射中相应字段的长度值。
	// 注意: 对变长列，在设置/插入
	//       数据前必须初始化这些字段!

	DBLENGTH m_dwidLength;
	DBLENGTH m_dwStartLength;
	DBLENGTH m_dwFinishLength;
	DBLENGTH m_dwPrevLength;
	DBLENGTH m_dwNextLength;
	DBLENGTH m_dwNext_FrokLength;
	DBLENGTH m_dwLengthLength;
	DBLENGTH m_dwMapIDLength;
	DBLENGTH m_dwTypeLength;
	DBLENGTH m_dwEnableLength;


	void GetRowsetProperties(CDBPropSet* pPropSet)
	{
		pPropSet->AddProperty(DBPROP_CANFETCHBACKWARDS, true, DBPROPOPTIONS_OPTIONAL);
		pPropSet->AddProperty(DBPROP_CANSCROLLBACKWARDS, true, DBPROPOPTIONS_OPTIONAL);
		pPropSet->AddProperty(DBPROP_IRowsetChange, true, DBPROPOPTIONS_OPTIONAL);
		pPropSet->AddProperty(DBPROP_UPDATABILITY, DBPROPVAL_UP_CHANGE | DBPROPVAL_UP_INSERT | DBPROPVAL_UP_DELETE);
	}

	HRESULT OpenDataSource()
	{
		CDataSource _db;
		HRESULT hr;
//#error 安全问题：连接字符串可能包含密码。
// 此连接字符串中可能包含明文密码和/或其他重要
// 信息。请在查看完此连接字符串并找到所有与安全
// 有关的问题后移除 #error。可能需要将此密码存
// 储为其他格式或使用其他的用户身份验证。
		hr = _db.OpenFromInitializationString(DbConnectString);
		if (FAILED(hr))
		{
#ifdef _DEBUG
			AtlTraceErrorRecords(hr);
#endif
			return hr;
		}
		return m_session.Open(_db);
	}

	void CloseDataSource()
	{
		m_session.Close();
	}

	operator const CSession&()
	{
		return m_session;
	}

	CSession m_session;

	DEFINE_COMMAND_EX(CTableLaneAccessor, L" \
	SELECT \
		id, \
		Start, \
		Finish, \
		Prev, \
		Next, \
		Next_Frok, \
		Length, \
		MapID, \
		Type, \
		Enable \
		FROM dbo.Lane")


	// 为解决某些提供程序的若干问题，以下代码可能以
	// 不同于提供程序所报告的顺序来绑定列

	BEGIN_COLUMN_MAP(CTableLaneAccessor)
		COLUMN_ENTRY_LENGTH_STATUS(1, m_id, m_dwidLength, m_dwidStatus)
		COLUMN_ENTRY_LENGTH_STATUS(2, m_Start, m_dwStartLength, m_dwStartStatus)
		COLUMN_ENTRY_LENGTH_STATUS(3, m_Finish, m_dwFinishLength, m_dwFinishStatus)
		COLUMN_ENTRY_LENGTH_STATUS(4, m_Prev, m_dwPrevLength, m_dwPrevStatus)
		COLUMN_ENTRY_LENGTH_STATUS(5, m_Next, m_dwNextLength, m_dwNextStatus)
		COLUMN_ENTRY_LENGTH_STATUS(6, m_Next_Frok, m_dwNext_FrokLength, m_dwNext_FrokStatus)
		COLUMN_ENTRY_LENGTH_STATUS(7, m_Length, m_dwLengthLength, m_dwLengthStatus)
		COLUMN_ENTRY_LENGTH_STATUS(8, m_MapID, m_dwMapIDLength, m_dwMapIDStatus)
		COLUMN_ENTRY_LENGTH_STATUS(9, m_Type, m_dwTypeLength, m_dwTypeStatus)
		_COLUMN_ENTRY_CODE(10, DBTYPE_BOOL, _SIZE_TYPE(m_Enable), 0, 0, offsetbuf(m_Enable), offsetbuf(m_dwEnableLength), offsetbuf(m_dwEnableStatus))
	END_COLUMN_MAP()
};

class CTableLane : public CCommand<CAccessor<CTableLaneAccessor> >
{
public:
	HRESULT OpenAll()
	{
		HRESULT hr;
		hr = OpenDataSource();
		if (FAILED(hr))
			return hr;
		__if_exists(GetRowsetProperties)
		{
			CDBPropSet propset(DBPROPSET_ROWSET);
			__if_exists(HasBookmark)
			{
				if( HasBookmark() )
					propset.AddProperty(DBPROP_IRowsetLocate, true);
			}
			GetRowsetProperties(&propset);
			return OpenRowset(&propset);
		}
		__if_not_exists(GetRowsetProperties)
		{
			__if_exists(HasBookmark)
			{
				if( HasBookmark() )
				{
					CDBPropSet propset(DBPROPSET_ROWSET);
					propset.AddProperty(DBPROP_IRowsetLocate, true);
					return OpenRowset(&propset);
				}
			}
		}
		return OpenRowset();
	}

	HRESULT OpenRowset(DBPROPSET *pPropSet = NULL)
	{
		HRESULT hr = Open(m_session, NULL, pPropSet);
#ifdef _DEBUG
		if(FAILED(hr))
			AtlTraceErrorRecords(hr);
#endif
		return hr;
	}

	void CloseAll()
	{
		Close();
		ReleaseCommand();
		CloseDataSource();
	}
};


