using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Data;

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
        private Point startPoint = Point.Empty;
        private Point endPoint = Point.Empty;
        public string railText = "";

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
        [Browsable(false)]
        public Point StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }
        [Browsable(false)]
        public Point EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
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
        public virtual DataRow SaveEleInfo(DataTable dt) { return null; }
        public virtual DataRow SaveCodingInfo(DataTable dt) { return null; }
    }
}
