using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BaseRailElement
{
    public class RailEleUserDef:Mcs.RailSystem.Common.EleUserDef
    {
        public RailEleUserDef()
        {
            GraphType = 7;
        }

        public RailEleUserDef CreateEle(
            Mcs.RailSystem.Common.EleUserDef.UserDefType type, 
            Point pt, 
            Size size, 
            Int16 multiFactor,
            string text,Image image)
        {
            typeUserDef = type;
            railText = text;
            imageUserDef = image;
            DrawMultiFactor = multiFactor;
            rcUserDef.X = 0;
            rcUserDef.Y = 0;
            rcUserDef.Width = imageUserDef.Width < size.Width ? imageUserDef.Width : (size.Width-20);
            rcUserDef.Height = imageUserDef.Height < size.Height ? imageUserDef.Height : (size.Height-20);
            return this;
        }

        public override void Draw(Graphics canvas)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            canvas.DrawImage(imageUserDef, rcUserDef);
        }

        public override void DrawTracker(Graphics canvas)
        {
            if (canvas == null)
            {
                throw new Exception("Graphics对象Canvas不能为空");
            }
            Pen pen = new Pen(Color.Blue, 1);
            Point[] pts = new Point[4];
            pts[0] = rcUserDef.Location;
            pts[1] = new Point() { X = rcUserDef.X, Y = rcUserDef.Y + rcUserDef.Height };
            pts[2] = new Point() { X = rcUserDef.X + rcUserDef.Width, Y = rcUserDef.Y };
            pts[3] = new Point() { X = rcUserDef.X + rcUserDef.Width, Y = rcUserDef.Y + rcUserDef.Height };
            for (int i = 0; i < 4; i++)
            {
                Rectangle rc = new Rectangle(pts[i].X - 4, pts[i].Y - 4, 8, 8);
                canvas.DrawRectangle(pen, rc);
            }
            pen.Dispose();

        }

        public override int HitTest(Point point, bool isSelected)
        {
            if (isSelected)
            {
                int handleHit = HandleHitTest(point);
                if (handleHit > 0)
                    return handleHit;
            }
            Point[] wrapper = new Point[1];
            wrapper[0] = point;
            Rectangle rc = rcUserDef;
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(rc);
            Region region = new Region(path);
            if (region.IsVisible(wrapper[0]))
                return 0;
            else
                return -1;
        }

        private int HandleHitTest(Point point)
        {
            Point[] pts = new Point[4];
            pts[0] = rcUserDef.Location;
            pts[1] = new Point() { X = rcUserDef.X, Y = rcUserDef.Y + rcUserDef.Height };
            pts[2] = new Point() { X = rcUserDef.X + rcUserDef.Width, Y = rcUserDef.Y };
            pts[3] = new Point() { X = rcUserDef.X + rcUserDef.Width, Y = rcUserDef.Y + rcUserDef.Height };
            for (int i = 0; i < 4; i++)
            {
                Point pt = pts[i];
                Rectangle rc = new Rectangle(pt.X - 3, pt.Y - 3, 6, 6);
                if (rc.Contains(point))
                    return i + 1;
            }
            return -1;
        }

        public override void Move(Point start, Point end)
        {
            if (LocationLock)
                return;
            int x = (end.X - start.X) / DrawMultiFactor;
            int y = (end.Y - start.Y) / DrawMultiFactor;
            Translate(x, y);
        }

        protected void Translate(int offsetX, int offsetY)
        {
            Point pt = Point.Empty;
            pt = rcUserDef.Location;
            pt.Offset(offsetX, offsetY);
            rcUserDef.X = pt.X;
            rcUserDef.Y = pt.Y;
        }

        public override void MoveHandle(int handle, Point start, Point end)
        {
            if (sizeLock)
                return;
            int dx = (end.X - start.X) / DrawMultiFactor;
            int dy = (end.Y - start.Y) / DrawMultiFactor;
            Scale(handle, dx, dy);
        }

        private void Scale(int handle, int dx, int dy)
        {
            if (rcUserDef.Width > 0 && rcUserDef.Height > 0)
            {
                rcUserDef.Width += dx;
                rcUserDef.Height += dy;
            }
            else if (dx > 0 || dy > 0)
            {
                if (dx > 0 || dy > 0)
                {
                    rcUserDef.Width += dx;
                    rcUserDef.Height += dy;
                }
                else if (dx > 0)
                {
                    rcUserDef.Width += dx;
                }
                else if (dy > 0)
                {
                    rcUserDef.Height += dy;
                }
            }
        }

        public override void DrawEnlargeOrShrink(float draw_multi_factor)
        {
            DrawMultiFactor = Convert.ToInt16(draw_multi_factor);
            base.DrawEnlargeOrShrink(draw_multi_factor);
        }

        public override void ChangePropertyValue()
        {
            base.ChangePropertyValue();
        }

        public override bool ChosedInRegion(Rectangle rect)
        {
            Point pt = rcUserDef.Location;
            pt.X = pt.X * DrawMultiFactor;
            pt.Y = pt.Y * DrawMultiFactor;
            int widthTemp = rcUserDef.Width * DrawMultiFactor;
            int heightTemp = rcUserDef.Height * DrawMultiFactor;
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(rect);
            Region region = new Region(path);
            Rectangle rc = new Rectangle(pt.X, pt.Y, widthTemp, heightTemp);
            if (region.IsVisible(rc))
                return true;
            else
                return false;
        }

        public object Clone(string str)
        {
            RailEleUserDef cl = new RailEleUserDef();
            return cl;
        }


    }
}
