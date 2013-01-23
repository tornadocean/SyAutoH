using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Mcs.RailSystem.Common
{
    public class EleFoupDot : BaseRailEle
    {
        protected Point ptScratchDotIcon = Point.Empty;
        protected Point ptScratchDot = Point.Empty;
        protected Point ptOffset = Point.Empty;
        protected Int32 iconWidth = 10;
        protected Int32 iconHeight = 10;
        protected Int32 codingScratchDot = -2;
        protected Int32 codingScratchDotOri = -2;
        protected Int16 deviceNum = -1;
        protected Image imageFoupWayIcon;
        protected Rectangle rcFoupDot;
        protected bool lockDotIcon = true;
        private PenStyle foupDotPen = new PenStyle();
        protected Pen pen = new Pen(Color.Black, 3);

        [Browsable(false)]
        public Point PtScratchDotIcon
        {
            get { return ptScratchDotIcon; }
            set { ptScratchDotIcon = value; }
        }
        [Browsable(false)]
        public Point PtScratchDot
        {
            get { return ptScratchDot; }
            set { ptScratchDot = value; }
        }
        [Browsable(false)]
        public Point PtOffset
        {
            get { return ptOffset; }
            set { ptOffset = value; }
        }
        [Description("Foup口条形"), Category("位置信息")]
        public Int32 CodingScratchDot
        {
            get { return codingScratchDot; }
            set
            {
                codingScratchDotOri = codingScratchDot;
                codingScratchDot = value; }
        }
        [Browsable(false)]
        public Int32 CodingScratchDotOri
        {
            get { return codingScratchDotOri; }
            set { codingScratchDotOri = value; }
        }
        public Int16 DeviceNum
        {
            get { return deviceNum; }
            set { deviceNum = value; }
        }
        [ Category("外观")]
        public Int32 IconWidth
        {
            get { return iconWidth; }
            set { iconWidth = value; }
        }
        [Category("外观")]
        public Int32 IconHeight
        {
            get { return iconHeight; }
            set { iconHeight = value; }
        }
        [Description("相对位置锁定"), Category("锁定")]
        public bool LockDotIcon
        {
            get { return lockDotIcon; }
            set { lockDotIcon = value; }
        }

        [Browsable(false)]
        public new Int32 CodingBegin
        {
            get;
            set;
        }
        [Browsable(false)]
        public new Int32 CodingEnd
        {
            get;
            set;
        }
        [Browsable(false)]
        public new Int32 CodingEndFork
        {
            get;
            set;
        }
        [Browsable(false)]
        public new Int32 CodingPrev
        {
            get;
            set;
        }
        [Browsable(false)]
        public new Int32 CodingNext
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
        public new Point DotStart
        {
            get;
            set;
        }
        [Browsable(false)]
        public new Point DotEnd
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
        [Category("线属性")]
        public Color PenColor
        {
            get { return foupDotPen.Color; }
            set { foupDotPen.Color = value; pen.Color = value; }
        }
        [Category("线属性")]
        public float PenWidth
        {
            get { return foupDotPen.Width; }
            set { foupDotPen.Width = value; pen.Width = value; }
        }
        [Category("线属性")]
        public DashStyle PenDashStyle
        {
            get { return foupDotPen.DashStyle; }
            set { foupDotPen.DashStyle = value; pen.DashStyle = value; }
        }
        [Browsable(false)]
        public Pen PenFoupDot
        {
            get { return pen; }
            set { pen = value; }
        }
    }
}
