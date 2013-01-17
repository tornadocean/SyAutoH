using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCSControlLib.Common
{
    public partial class TryConver
    {
        static public byte ToByte(string value)
        {
            byte byteValue = 0;
            try
            {
                byteValue = System.Convert.ToByte(value);
            }
            catch (System.Exception /*ex*/)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                byteValue = 0;
            }
            return byteValue;
        }

        static public uint ToUInt32(string value)
        {
            uint intValue = 0;
            try
            {
                intValue = System.Convert.ToUInt32(value);
            }
            catch (System.Exception /*ex*/)
            {
                intValue = 0;
            }
            return intValue;
        }

        static public int ToInt32(string value)
        {
            int intValue = 0;
            if (value.Length <= 0)
            {
                return int.MinValue;
            }

            try
            {
                intValue = System.Convert.ToInt32(value);
            }
            catch (System.Exception /*ex*/)
            {
                intValue = 0;
            }
            return intValue;
        }

        static public string ToString(System.DateTime datatime)
        {
            string strTime = null;
            int nYear = datatime.Year;
            int nMonth = datatime.Month;
            int nDay = datatime.Day;
            int nHour = datatime.Hour;
            int nMin = datatime.Minute;
            int nSec = datatime.Second;

            strTime = nYear.ToString() + "-"
                + nMonth.ToString() + "-"
                + nDay.ToString() + " "
                + nHour.ToString() + ":"
                + nMin.ToString() + ":"
                + nSec.ToString();
            return strTime;
        }
    }
}
