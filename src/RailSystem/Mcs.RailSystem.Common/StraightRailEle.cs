using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Data;

namespace Mcs.RailSystem.Common
{
    public class StraightRailEle
    {
        private BaseRailElement.ObjectStraightOp objectStaightOp = new BaseRailElement.ObjectStraightOp();
        private int lenght = 100;
        private int startAngle = 0;
        private int rotateAngle = 90;
        private Int32 codingBegin = -1;
        private Int32 codingEnd = -1;
        private Int32 codingNext = -1;
        private Int32 codingPrev = -1;
        public DataTable dt = new DataTable();
        private PenStyle linePen = new PenStyle();
        private Pen pen = new Pen(Color.Black, 3);

        [Category("其他")]
        public int Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }
        [Browsable(false)]
        public int StartAngle
        {
            get { return startAngle; }
            set { startAngle = value; }
        }
        [Browsable(false)]
        public Int32 RotateAngle
        {
            get { return rotateAngle; }
            set { rotateAngle = value; }
        }
        [Browsable(false)]
        public List<Point> PointList
        {
            get { return objectStaightOp.PointList; }
        }

    }
}
