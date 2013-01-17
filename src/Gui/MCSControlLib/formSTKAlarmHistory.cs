using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCSControlLib.Common;

namespace MCSControlLib.Page.Stocker
{
    public partial class formSTKAlarmHistory : Form
    {
        public formSTKAlarmHistory()
        {
            InitializeComponent();
        }

        private void btnStkAlarmQuery_Click(object sender, EventArgs e)
        {
            if (dateTimePickerAlarmET.Value < dateTimePickerAlarmST.Value)
            {
                MessageBox.Show("please choose End Time again");
                return;
            }
            DateTime startTime = dateTimePickerAlarmST.Value;
            DateTime endTime = dateTimePickerAlarmET.Value;
            string strStart = TryConver.ToString(startTime);
            string strEnd = TryConver.ToString(endTime);
            string strVal = strStart + "," + strEnd;

            
        }
    }
}
