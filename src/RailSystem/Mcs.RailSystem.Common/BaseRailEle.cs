using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace Mcs.RailSystem.Common
{
    public abstract class BaseRailEle
    {
        private int graphType = 0;
        protected bool locationLock = false;
        protected bool sizeLock = false;
        private bool selectable = true;
        private float speed = 0;
        private Int16 drawMultiFactor = 1;
        protected Int32 codingBegin = -1;
        protected Int32 codingEnd = -1;
        protected Int32 codingEndFork = -1;
        protected Int32 codingPrev = -1;
        protected Int32 codingNext = -1;
        protected Int32 codingNextFork = -1;
        protected Point dotStart = Point.Empty;
        protected Point dotEnd = Point.Empty;
        protected Point dotEndFork = Point.Empty;
        public string railText = "";
        private Color trackerColor = Color.Blue;
        private Int16 railAuxiliaryDrawType = 4;

        [Browsable(false)]
        public int GraphType
        {
            get { return graphType; }
            set { graphType = value; }
        }
        [Description("位置锁定"), Category("锁定")]
        public bool LocationLock
        {
            get { return locationLock; }
            set { locationLock = value; }
        }
        [Description("尺寸锁定"), Category("锁定")]
        public bool SizeLock
        {
            get { return sizeLock; }
            set { sizeLock = value; }
        }
        [Browsable(false)]
        public bool Selectable
        {
            get { return selectable; }
            set { selectable = value; }
        }
        [Category("params")]
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        [Browsable(false)]
        public Int16 DrawMultiFactor
        {
            get { return drawMultiFactor; }
            set { drawMultiFactor = value; }
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
        public Int32 CodingEndFork
        {
            get { return codingEndFork; }
            set { codingEndFork = value; }
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
        public Int32 CodingNextFork
        {
            get { return codingNextFork; }
            set { codingNextFork = value; }
        }
        [Description("条形码终止"), Category("轨道段信息")]
        [ReadOnly(true)]
        public Point DotStart
        {
            get { return dotStart; }
            set { dotStart = value; }
        }
        [Description("条形码终止"), Category("轨道段信息")]
        [ReadOnly(true)]
        public Point DotEnd
        {
            get { return dotEnd; }
            set { dotEnd = value; }
        }
        [Description("条形码终止"), Category("轨道段信息")]
        [ReadOnly(true)]
        public Point DotEndFork
        {
            get { return dotEndFork; }
            set { dotEndFork = value; }
        }
        [Browsable(false)]
        public Color TrackerColor
        {
            get { return trackerColor; }
            set { trackerColor = value; }
        }
        [Browsable(false)]
        public Int16 RailAuxiliaryDrawType
        {
            get { return railAuxiliaryDrawType; }
            set { railAuxiliaryDrawType = value; }
        }

        public virtual void Draw(Graphics canvas) { }
        public virtual void DrawTracker(Graphics canvas) { }
        public virtual int HitTest(Point point, bool isSelected) { return -1; }
        public virtual void Move(Point start, Point end) { }
        public virtual void MoveHandle(int handle, Point start, Point end) { }
        public virtual Region GetRedrawRegion() { return null; }
        public virtual void ChangePropertyValue() { }
        public virtual void RotateCounterClw() { }
        public virtual void RotateClw() { }
        public virtual void DrawEnlargeOrShrink(float draw_multi_factor) { }
        public virtual void ObjectMirror() { }
        public virtual bool ChosedInRegion(Rectangle rect) { return false; }
    }
}
