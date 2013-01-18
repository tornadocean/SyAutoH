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
        [Browsable(false)]
        public new Int32 CodingEndFork
        {
            get;
            set;
        }
        [Browsable(false)]
        public new Int32 CodingNextFork
        {
            get;
            set;
        }
        [Browsable(false)]
        public new Point DotEndFork
        {
            get;
            set;
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
        [Browsable(false)]
        public Pen PenLine
        {
            get { return pen; }
            set { pen = value; }
        }
        
    }
}
