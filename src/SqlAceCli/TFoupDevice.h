// TFoupDevice.h : CTableFoupDevice ������

#pragma once

// ���������� 2013��1��25��, 8:57

class CTableFoupDeviceAccessor
{
public:
	LONG m_Id;
	LONG m_DevID;
	LONG m_DevType;
	LONG m_DevPortID;
	LONG m_DevPortKP;

	// ���������ɵ����ݳ�Ա����
	//��ӳ������Ӧ�ֶε�״ֵ̬��
	// ����ʹ����Щֵ�������ݿⷵ�ص� NULL ֵ���ڱ���������
	// ����ʱ���������Ϣ���й����ʹ��
	//��Щ�ֶε���ϸ��Ϣ��
	// ��μ� Visual C++ �ĵ��е�
	//�������ɵķ������е��ֶ�״̬���ݳ�Ա����
	// ע��: ������/��������ǰ�����ʼ����Щ�ֶ�!

	DBSTATUS m_dwIdStatus;
	DBSTATUS m_dwDevIDStatus;
	DBSTATUS m_dwDevTypeStatus;
	DBSTATUS m_dwDevPortIDStatus;
	DBSTATUS m_dwDevPortKPStatus;

	// ���������ɵ����ݳ�Ա����
	//��ӳ������Ӧ�ֶεĳ���ֵ��
	// ע��: �Ա䳤�У�������/����
	//       ����ǰ�����ʼ����Щ�ֶ�!

	DBLENGTH m_dwIdLength;
	DBLENGTH m_dwDevIDLength;
	DBLENGTH m_dwDevTypeLength;
	DBLENGTH m_dwDevPortIDLength;
	DBLENGTH m_dwDevPortKPLength;


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
//#error ��ȫ���⣺�����ַ������ܰ������롣
// �������ַ����п��ܰ������������/��������Ҫ
// ��Ϣ�����ڲ鿴��������ַ������ҵ������밲ȫ
// �йص�������Ƴ� #error��������Ҫ���������
// ��Ϊ������ʽ��ʹ���������û������֤��
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

	DEFINE_COMMAND_EX(CTableFoupDeviceAccessor, L" \
	SELECT \
		Id, \
		DevID, \
		DevType, \
		DevPortID, \
		DevPortKP \
		FROM dbo.FoupDevice")


	// Ϊ���ĳЩ�ṩ������������⣬���´��������
	// ��ͬ���ṩ�����������˳��������

	BEGIN_COLUMN_MAP(CTableFoupDeviceAccessor)
		COLUMN_ENTRY_LENGTH_STATUS(1, m_Id, m_dwIdLength, m_dwIdStatus)
		COLUMN_ENTRY_LENGTH_STATUS(2, m_DevID, m_dwDevIDLength, m_dwDevIDStatus)
		COLUMN_ENTRY_LENGTH_STATUS(3, m_DevType, m_dwDevTypeLength, m_dwDevTypeStatus)
		COLUMN_ENTRY_LENGTH_STATUS(4, m_DevPortID, m_dwDevPortIDLength, m_dwDevPortIDStatus)
		COLUMN_ENTRY_LENGTH_STATUS(5, m_DevPortKP, m_dwDevPortKPLength, m_dwDevPortKPStatus)
	END_COLUMN_MAP()
};

class CTableFoupDevice : public CCommand<CAccessor<CTableFoupDeviceAccessor> >
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


