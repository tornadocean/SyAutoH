using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GuiAccess;

namespace MCSControlLib.Common
{
    public delegate void DataChangeHander(object sender, object obData1, object obData2);
    public interface IMcsControlBase
    {
        event DataChangeHander DataChange;

        void ProcessGuiData(List<MCS.GuiDataItem> list);
        void PageInit();
        void PageExit();
        GuiAccess.DataHubCli DataHub
        {
            set;
            get;
        }
    }
}
