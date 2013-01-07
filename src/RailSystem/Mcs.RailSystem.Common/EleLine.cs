using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Mcs.RailSystem.Common
{
    public class EleLine : BaseRailEle
    {
        private int lenght = 100;
        private int startAngle = 0;
        private int rotateAngle = 90;
        private Int32 codingBegin = -1;
        private Int32 codingEnd = -1;
        private Int32 codingNext = -1;
        private Int32 codingPrev = -1;
        private List<Point> pointList = new List<Point>();
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
        [Description("条形码起始"), Category("轨道段信息")]
        public Int32 CodingBegin
        {
            get { return codingBegin; }
            set { codingBegin = value; }
        }
        [Description("条形码终止"), Category("轨道段信息")]
        public Int32 CodingEnd
        {
            get { return codingEnd; }
            set { codingEnd = value; }
        }
        [Description("条形码起始"), Category("轨道段信息")]
        public Int32 CodingNext
        {
            get { return codingNext; }
            set { codingNext = value; }
        }
        [Description("条形码起始"), Category("轨道段信息")]
        public Int32 CodingPrev
        {
            get { return codingPrev; }
            set { codingPrev = value; }
        }
        public List<Point> PointList
        {
            get { return pointList; }
            set { pointList = value; }
        }
        [Category("线属性")]
        public Color PenColor
        {
            get { return linePen.Color; }
            set { linePen.Color = value; pen.Color = value; }
        }
        [Category("线属性")]
        public float PenWidth
        {
            get { return linePen.Width; }
            set { linePen.Width = value; pen.Width = value; }
        }
        [Category("线属性")]
        public DashStyle PenDashStyle
        {
            get { return linePen.DashStyle; }
            set { linePen.DashStyle = value; pen.DashStyle = value; }
        }
        public Pen PenLine
        {
            get { return pen; }
            set { pen = value; }
        }
    }
}
