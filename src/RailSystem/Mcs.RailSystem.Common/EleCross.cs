using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Mcs.RailSystem.Common
{
    public class EleCross : BaseRailEle
    {
        private int startAngle = 0;
        private int rotateAngle = 90;
        private int lenghtOfStrai = 100;
        private int firstPart = 30;
        private int secPart = 40;
        private int thPart = 30;
        private Point fourPart = new Point(40, 40);
        private bool mirror = false;
        private Int32 codingBegin = -1;
        private Int32 codingEnd = -1;
        private Int32 codingEndS = -1;
        private Int32 codingPrev = -1;
        private Int32 codingNext = -1;
        private Int32 codingNextS = -1;
        private List<Point> pointList = new List<Point>();
        private DirectionCross directionOfCross = DirectionCross.NULL;
        private PenStyle crossPen = new PenStyle();
        private Pen pen = new Pen(Color.Black, 3);

        public enum DirectionCross
        {
            first, second, third, four, NULL
        }

        [Browsable(false)]
        public int StartAngle
        {
            get { return startAngle; }
            set { startAngle = value; }
        }
        [Browsable(false)]
        public int RotateAngle
        {
            get { return rotateAngle; }
            set { rotateAngle = value; }
        }
        [Browsable(false)]
        public int LenghtOfStrai
        {
            get { return lenghtOfStrai; }
            set { lenghtOfStrai = value; }
        }
        [Category("轨道长度")]
        public int FirstPart
        {
            get { return firstPart; }
            set { firstPart = value; }
        }
        [Category("轨道长度")]
        public int SecPart
        {
            get { return secPart; }
            set { secPart = value; }
        }
        [Category("轨道长度")]
        public int ThPart
        {
            get { return thPart; }
            set { thPart = value; }
        }
        [Category("轨道长度")]
        public Point FourPart
        {
            get { return fourPart; }
            set { fourPart = value; }
        }
        [Browsable(false)]
        public bool Mirror
        {
            get { return mirror; }
            set { mirror = value; }
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
        [Description("条形码终止"), Category("轨道段信息")]
        public Int32 CodingEndS
        {
            get { return codingEndS; }
            set { codingEndS = value; }
        }
        [Description("条形码终止"), Category("轨道段信息")]
        public Int32 CodingPrev
        {
            get { return codingPrev; }
            set { codingPrev = value; }
        }
        [Description("条形码终止"), Category("轨道段信息")]
        public Int32 CodingNext
        {
            get { return codingNext; }
            set { codingNext = value; }
        }
        [Description("条形码终止"), Category("轨道段信息")]
        public Int32 CodingNextS
        {
            get { return codingNextS; }
            set { codingNextS = value; }
        }
        [Browsable(false)]
        public List<Point> PointList
        {
            get { return pointList; }
        }
        public DirectionCross DirectionOfCross
        {
            get { return directionOfCross; }
            set { directionOfCross = value; }
        }
        [Category("线属性")]
        public Color PenColor
        {
            get { return crossPen.Color; }
            set { crossPen.Color = value; pen.Color = value; }
        }
        [Category("线属性")]
        public float PenWidth
        {
            get { return crossPen.Width; }
            set { crossPen.Width = value; pen.Width = value; }
        }
        [Category("线属性")]
        public DashStyle PenDashStyle
        {
            get { return crossPen.DashStyle; }
            set { crossPen.DashStyle = value; pen.DashStyle = value; }
        }
        [Browsable(false)]
        public Pen PenCross
        {
            get { return pen; }
            set { pen = value; }
        }
    }
}
