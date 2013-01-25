using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GuiAccess;
using System.Data;

namespace McsRemote.Control
{
    public interface IPageGuiAccess
    {
        GuiAccess.DataHubCli DataHub
        {
            set;
            get;
        }

        void ProcessGuiData();
    }
}
