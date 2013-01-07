using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Gemma;

namespace LogWinClientCS
{
    public class LogWinClient
    {
        private Ice.Communicator communicator = null;
        private LogPrx remote = null;

        public LogWinClient()
        {

        }

        private string GetProcessPath()
        {
            string strPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            FileInfo fi = new FileInfo(strPath);
            strPath = fi.DirectoryName;
            return strPath;
        }

        public void ConnectServer()
        {
            try
            {
                //Disconnect();
                //communicator = Ice.Util.initialize();
                //remote =
                 //   LogPrxHelper.checkedCast(communicator.stringToProxy("ICLog:tcp -p 15356"));

                //Init();
                Ice.InitializationData initData = new Ice.InitializationData();
                initData.properties = Ice.Util.createProperties();
                initData.properties.setProperty("ICLog.Proxy", "ICLog:tcp -p 15366");

                string strConfigFile = GetProcessPath() + @"\..\Config\IceClientConfig.txt";
                try
                {
                    initData.properties.load(strConfigFile);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                communicator = Ice.Util.initialize(initData);
                string strProxy = initData.properties.getProperty("ICLog.Proxy");
                remote =
                    LogPrxHelper.uncheckedCast(communicator.stringToProxy(strProxy));
                remote.ice_timeout(1000);
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.Message);
            }
        }

        public void Disconnect()
        {
            if (null != communicator)
            {
                communicator.shutdown();
                communicator.destroy();
            }
        }

        public LogMsg[] GetLog(int nID, int nCount, int[] types, int[] ids, string sKey)
        {
            try
            {
                LogMsg[] list =  remote.GetLog(nID, nCount, types, ids, sKey);
                return list;
            }
            catch (System.Exception ex)
            {
                //ConnectServer();
                string str = ex.Message;
                return null;
            }
        }

        public LogMsg[] GetOfflineLog(int nID, int nCount, long ltimeEnd, long ltimeBegin,
            int[] types, int[] ids, string sKey)
        {
            try
            {
                LogMsg[] list = remote.GetLogOffline(nID, nCount, ltimeEnd, ltimeBegin, types, ids, sKey);
                return list;
            }
            catch (System.Exception ex)
            {
                //ConnectServer();
                string str = ex.Message;
                return null;
            }
        }

        public int GetOfflineCount(long lTimeEnd, long lTimeBegin, int[] types, int[] ids, string sKey)
        {
            try
            {
                return remote.GetCountOffline(lTimeEnd, lTimeBegin, types, ids, sKey);
            }
            catch (System.Exception ex)
            {
                //ConnectServer();
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public int GetNowID()
        {
            try
            { 
               int n = remote.GetLastID();
                if (n<0)
                {
                   // ConnectServer();
                }
                return n;
            }
            catch (Ice.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
           
        }

        public void Log(int nID, byte type, string strUnit, string strMsg)
        {
            System.DateTime dt = System.DateTime.Now;
            string strDt = string.Format("{0}{1:0#}{2:0#}{3:0#}{4:0#}{5:0#}{6:00#}", dt.Year, dt.Month, dt.Day,
                dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
            Int64 lTime = 0;
            lTime = System.Convert.ToInt64(strDt);

            List<LogMsg> msgList = new List<LogMsg>();
            LogMsg msg = new LogMsg();
            msg.nEventID = nID;
            msg.lTime = lTime;
            msg.nType = (short)type;
            msg.strUnit = strUnit;
            msg.strMsg = strMsg;
            msgList.Add(msg);
            try
            {
                remote.sendLog(msgList.ToArray());
            }
            catch (Ice.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        public int GetAlarmCount()
        {
            try
            {
                int n = remote.GetAlarmCount();
                if (n < 0)
                {
                    // ConnectServer();
                }
                return n;
            }
            catch (System.Exception ex)
            {
                //ConnectServer();
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public void ClearAlarm(int[] idList)
        {
            try
            {
                remote.ClearAlarms(idList);
            }
            catch (System.Exception ex)
            {
                string str = ex.Message;
            }
        }

        public AlarmMsg[] GetAlarm(int nStart, int nCount)
        {
            try
            {
                AlarmMsg[] list = remote.GetAlarms(nStart, nCount);
                return list;
            }
            catch (System.Exception ex)
            {
                //ConnectServer();
                string str = ex.Message;
                return null;
            }
        }
    }
}
