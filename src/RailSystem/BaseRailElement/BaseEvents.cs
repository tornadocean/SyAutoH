using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BaseRailElement
{
    public abstract class BaseEvents
    {
        protected static Point downPoint = Point.Empty;
        public static Point DownPoint
        {
            get { return downPoint; }
            set { downPoint = value; }
        }

        protected static Point lastPoint = Point.Empty;
        public static Point LastPoint
        {
            get { return lastPoint; }
            set { lastPoint = value; }
        }
        protected bool hasContact = false;
        protected Point contactStartDot = Point.Empty;
        protected Point contactEndDot = Point.Empty;
        protected Int16 drawToolType = 4;
        public Int16 DrawToolType
        {
            get { return drawToolType; }
            set { drawToolType = value; }
        }

        public virtual void OnLButtonDown(Point point)
        {
            downPoint = point;
            lastPoint = point;
        }

        public virtual bool OnRButtonDown(Point point)
        {
            downPoint = point;
            lastPoint = point;
            return false;
        }

        public virtual void OnMouseMoveLeft(Point point)
        {
            lastPoint = point;
        }

        public virtual void OnLButtonUp(Point point)
        {
            lastPoint = point;
        }

        public virtual void ChangePropertyValue()
        {
        }
    }
}
