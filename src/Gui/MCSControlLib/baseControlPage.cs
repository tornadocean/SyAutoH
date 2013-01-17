using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCS.GuiHub;

namespace MCSControlLib.Common
{
    public partial class baseControlPage : UserControl
    {
        public event DataChangeHander DataChange;
        protected GuiAccess.DataHubCli m_dataHub = null;
        protected delegate void ProcessHandler(ArrayList item);
        protected Dictionary<PushData, ProcessHandler> m_dictProcess = new Dictionary<PushData, ProcessHandler>();

        public baseControlPage()
        {
            InitProcessDictionary(); 
        }

        protected void NotifyDataChange(object obData1, object obData2)
        {
            if (null != this.DataChange)
            {
                this.DataChange(this, obData1, obData2);
            }
        }

        protected virtual void InitProcessDictionary()
        {

        }

        public GuiAccess.DataHubCli DataHub
        {
            set
            {
                m_dataHub = value;
            }
            get
            {
                return m_dataHub;
            }
        }

        public virtual void PageInit()
        {

        }

        public virtual void PageExit()
        {
            PushData[] cmds = new PushData[] { };
            m_dataHub.Async_SetPushCmdList(cmds);
        }

        public void ProcessGuiData(List<MCS.GuiDataItem> list)
        {
            foreach (MCS.GuiDataItem item in list)
            {
                ProcessGuiDataItem(item);
            }
        }

        private void ProcessGuiDataItem(MCS.GuiDataItem guiData)
        {
            ProcessHandler handler = null;
            bool bGet = m_dictProcess.TryGetValue(guiData.enumTag, out handler);
            if (true == bGet)
            {
                ArrayList alDatas = GuiAccess.DataHubCli.ConvertToArrayList(guiData.sVal);
                foreach (ArrayList item in alDatas)
                {
                    handler(item);
                }
            }
        }
    }
}
