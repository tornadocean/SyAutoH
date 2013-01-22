// TKeyPoints.h : CTableKeyPoints 的声明

#pragma once

// 代码生成在 2013年1月22日, 11:18

class CTableKeyPointsAccessor
{
public:
	LONG m_Id;
	LONG m_Position;
	LONG m_Type;
	BYTE m_SpeedRate;
	BYTE m_TeachMode;
	BYTE m_OHT_ID;
	LONG m_Lane_ID;
	LONG m_Prev;
	LONG m_Next;
	TCHAR m_Name[51];
	double m_refX;
	double m_refY;

	// 以下向导生成的数据成员包含
	//列映射中相应字段的状态值。
	// 可以使用这些值保存数据库返回的 NULL 值或在编译器返回
	// 错误时保存错误信息。有关如何使用
	//这些字段的详细信息，
	// 请参见 Visual C++ 文档中的
	//“向导生成的访问器中的字段状态数据成员”。
	// 注意: 在设置/插入数据前必须初始化这些字段!

	DBSTATUS m_dwIdStatus;
	DBSTATUS m_dwPositionStatus;
	DBSTATUS m_dwTypeStatus;
	DBSTATUS m_dwSpeedRateStatus;
	DBSTATUS m_dwTeachModeStatus;
	DBSTATUS m_dwOHT_IDStatus;
	DBSTATUS m_dwLane_IDStatus;
	DBSTATUS m_dwPrevStatus;
	DBSTATUS m_dwNextStatus;
	DBSTATUS m_dwNameStatus;
	DBSTATUS m_dwrefXStatus;
	DBSTATUS m_dwrefYStatus;

	// 以下向导生成的数据成员包含
	//列映射中相应字段的长度值。
	// 注意: 对变长列，在设置/插入
	//       数据前必须初始化这些字段!

	DBLENGTH m_dwIdLength;
	DBLENGTH m_dwPositionLength;
	DBLENGTH m_dwTypeLength;
	DBLENGTH m_dwSpeedRateLength;
	DBLENGTH m_dwTeachModeLength;
	DBLENGTH m_dwOHT_IDLength;
	DBLENGTH m_dwLane_IDLength;
	DBLENGTH m_dwPrevLength;
	DBLENGTH m_dwNextLength;
	DBLENGTH m_dwNameLength;
	DBLENGTH m_dwrefXLength;
	DBLENGTH m_dwrefYLength;


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

	DEFINE_COMMAND_EX(CTableKeyPointsAccessor, L" \
	SELECT \
		Id, \
		Position, \
		Type, \
		SpeedRate, \
		TeachMode, \
		OHT_ID, \
		Lane_ID, \
		Prev, \
		Next, \
		Name, \
		refX, \
		refY \
		FROM dbo.KeyPoints")


	// 为解决某些提供程序的若干问题，以下代码可能以
	// 不同于提供程序所报告的顺序来绑定列

	BEGIN_COLUMN_MAP(CTableKeyPointsAccessor)
		COLUMN_ENTRY_LENGTH_STATUS(1, m_Id, m_dwIdLength, m_dwIdStatus)
		COLUMN_ENTRY_LENGTH_STATUS(2, m_Position, m_dwPositionLength, m_dwPositionStatus)
		COLUMN_ENTRY_LENGTH_STATUS(3, m_Type, m_dwTypeLength, m_dwTypeStatus)
		COLUMN_ENTRY_LENGTH_STATUS(4, m_SpeedRate, m_dwSpeedRateLength, m_dwSpeedRateStatus)
		COLUMN_ENTRY_LENGTH_STATUS(5, m_TeachMode, m_dwTeachModeLength, m_dwTeachModeStatus)
		COLUMN_ENTRY_LENGTH_STATUS(6, m_OHT_ID, m_dwOHT_IDLength, m_dwOHT_IDStatus)
		COLUMN_ENTRY_LENGTH_STATUS(7, m_Lane_ID, m_dwLane_IDLength, m_dwLane_IDStatus)
		COLUMN_ENTRY_LENGTH_STATUS(8, m_Prev, m_dwPrevLength, m_dwPrevStatus)
		COLUMN_ENTRY_LENGTH_STATUS(9, m_Next, m_dwNextLength, m_dwNextStatus)
		COLUMN_ENTRY_LENGTH_STATUS(10, m_Name, m_dwNameLength, m_dwNameStatus)
		COLUMN_ENTRY_LENGTH_STATUS(11, m_refX, m_dwrefXLength, m_dwrefXStatus)
		COLUMN_ENTRY_LENGTH_STATUS(12, m_refY, m_dwrefYLength, m_dwrefYStatus)
	END_COLUMN_MAP()
};

class CTableKeyPoints : public CCommand<CAccessor<CTableKeyPointsAccessor> >
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


