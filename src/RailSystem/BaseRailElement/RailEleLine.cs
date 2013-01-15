using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace BaseRailElement
{
    public class RailEleLine : Mcs.RailSystem.Common.EleLine
    {
        public RailEleLine()
        {
            GraphType = 1;
            PenLine.Width = PenWidth;
            PenLine.Color = PenColor;
            PenLine.DashStyle = PenDashStyle;
            PenLine.EndCap = LineCap.ArrowAnchor;
        }

        public RailEleLine CreateEle(Point pt, Size size, Int16 multiFactor, string text)
        {
            Point[] pts = new Point[2];
            DrawMultiFactor = multiFactor;
            pt.Offset(pt.X / DrawMultiFactor - pt.X, pt.Y / DrawMultiFactor - pt.Y);
            pts[0] = pt;
            if ((pt.X + Lenght) > size.Width)
            {
                pts[0] = new Point(pt.X - Lenght, pt.Y);
                pts[1] = new Point(pt.X, pt.Y);
            }
            else
            {
                pts[0] = new Point(pt.X, pt.Y);
                pts[1] = new Point(pt.X + Lenght, pt.Y);
            }
            PointList.AddRange(pts);
            this.railText = text;
            return this;
        }

        public override void Draw(Graphics canvas)
        {
            if (canvas == null)
            {
                throw new Exception("Graphics对象Canvas不能为空");
            }
            if (PointList.Count < 2)
            {
                throw new Exception("绘制线条的点至少需要2个");
            }
            int n = PointList.Count;
            Point[] pts = new Point[n];
            PointList.CopyTo(pts);
            if (pts[0].Y == pts[1].Y)
            {
                if (Lenght != Math.Abs(pts[0].X - pts[1].X))
                {
                    if (pts[0].X < pts[1].X)
                    {
                        pts[1].X = pts[0].X + Lenght;
                    }
                    else
                    {
                        pts[1].X = pts[0].X - Lenght;
                    }
                }
            }
            else if (pts[0].X == pts[1].X)
            {
                if (Lenght != Math.Abs(pts[0].Y - pts[1].Y))
                {
                    pts[1].Y = pts[0].Y + Lenght;
                }
            }
            for (int i = 0; i < n; i++)
            {
                pts[i].Offset(pts[i].X * (DrawMultiFactor - 1), pts[i].Y * (DrawMultiFactor - 1));
            }
            canvas.DrawLines(PenLine, pts);
        }

        public override void DrawTracker(Graphics canvas)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            int n = PointList.Count;
            Point[] pts = new Point[n];
            PointList.CopyTo(pts);
            Pen pen = new Pen(Color.Blue);
            pen.Width = 1;
            for (int i = 0; i < n; i++)
            {
                Point pt = pts[i];
                pt.Offset(pt.X * (DrawMultiFactor - 1), pt.Y * (DrawMultiFactor - 1));
                Rectangle rc = new Rectangle(pt.X - 4, pt.Y - 4, 8, 8);
                canvas.DrawRectangle(pen, rc);
            }
            pen.Dispose();
        }

        public override int HitTest(Point point, bool isSelected)
        {
            int n = PointList.Count;
            if (isSelected)
            {
                int hit = HandleHitTest(point);
                if (hit >= 0) return hit;
            }
            for (int i = 0; i < n - 1; i++)
            {
                Point pt1 = PointList[i];
                Point pt2 = PointList[i + 1];
                float angle = 0;
                int length = 0;
                if (pt1.X == pt2.X)
                {
                    angle = pt1.Y < pt2.Y ? 90 : -90;
                    length = Math.Abs(pt1.Y - pt2.Y);
                }
                else if (pt1.Y == pt2.Y)
                {
                    angle = pt1.X < pt2.X ? 0 : 180;
                    length = Math.Abs(pt1.X - pt2.X);
                }
                else
                {
                    float tan = (float)(pt2.Y - pt1.Y) / (pt2.X - pt1.X);
                    angle = (float)(Math.Atan(tan) * 180 / Math.PI);
                    int n1 = (pt2.Y - pt1.Y) * (pt2.Y - pt1.Y) + (pt2.X - pt1.X) * (pt2.X - pt1.X);
                    double d1 = Math.Sqrt(n1);
                    length = Convert.ToInt32(d1);
                }
                Rectangle rc = GetRedrawRc();
                Point[] wrapper = new Point[1];
                wrapper[0] = point;
                if (rc.Contains(wrapper[0]))
                    return 0;
            }
            return -1;
        }

        private int HandleHitTest(Point point)
        {
            int n = PointList.Count;
            Point[] pts = new Point[n];
            PointList.CopyTo(pts);
            for (int i = 0; i < n; i++)
            {
                pts[i].Offset(pts[i].X * (DrawMultiFactor - 1), pts[i].Y * (DrawMultiFactor - 1));
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

        private void Translate(int offsetX, int offsetY)
        {
            int n = PointList.Count;
            for (int i = 0; i < n; i++)
            {
                Point pt = PointList[i];
                pt.Offset(offsetX, offsetY);
                PointList[i] = pt;
            }
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
            Point pt1 = new Point(0);
            Point pt2 = new Point(0);
            int n = PointList.Count;
            for (int i = 0; i < n - 1; i++)
            {
                pt1 = PointList[i];
                pt2 = PointList[i + 1];
            }
            if (pt1.Y == pt2.Y)
            {
                Point pt = PointList[handle - 1];
                pt.Offset(dx, 0);
                PointList[handle - 1] = pt;
                Lenght = Math.Abs(PointList[1].X - PointList[0].X);
            }
            else if (pt1.X == pt2.X)
            {
                Point pt = PointList[handle - 1];
                pt.Offset(0, dy);
                PointList[handle - 1] = pt;
                Lenght = Math.Abs(PointList[1].Y - PointList[0].Y);
            }
        }

        public override void RotateCounterClw()
        {
            base.RotateCounterClw();
            Point pt = new Point();
            if (PointList[0].X == PointList[1].X)
            {
                pt.X = PointList[0].X;
                pt.Y = (PointList[0].Y + PointList[1].Y) / 2;
            }
            else if (PointList[0].Y == PointList[1].Y)
            {
                pt.X = (PointList[0].X + PointList[1].X) / 2;
                pt.Y = PointList[0].Y;
            }
            RotateAngle = -90;
            StartAngle -= RotateAngle;

            Rotate(pt, RotateAngle);
        }

        public override void RotateClw()
        {
            base.RotateClw();
            Point pt = new Point();
            if (PointList[0].X == PointList[1].X)
            {
                pt.X = PointList[0].X;
                pt.Y = (PointList[0].Y + PointList[1].Y) / 2;
            }
            else if (PointList[0].Y == PointList[1].Y)
            {
                pt.X = (PointList[0].X + PointList[1].X) / 2;
                pt.Y = PointList[0].Y;
            }
            RotateAngle = 90;
            StartAngle += RotateAngle;

            Rotate(pt, RotateAngle);
        }

        private void Rotate(Point pt, int angle)
        {
            Matrix matrix = new Matrix();
            matrix.RotateAt(angle, pt);

            int n = PointList.Count;
            Point[] points = new Point[n];
            PointList.CopyTo(points);
            matrix.TransformPoints(points);
            PointList.Clear();
            PointList.AddRange(points);
        }

        public override void DrawEnlargeOrShrink(float drawMultiFactor)
        {
            DrawMultiFactor = Convert.ToInt16(drawMultiFactor);
            base.DrawEnlargeOrShrink(drawMultiFactor);
        }

        public override void ChangePropertyValue()
        {
            Point[] pts = new Point[2];
            PointList.CopyTo(pts);
            if (PointList[0].X == PointList[1].X)
            {
                if (PointList[0].Y < PointList[1].Y)
                    pts[1].Y = pts[0].Y + Lenght;
                else
                    pts[0].Y = pts[1].Y + Lenght;
            }
            else if (PointList[0].Y == PointList[1].Y)
            {
                if (PointList[0].X < PointList[1].X)
                    pts[1].X = pts[0].X + Lenght;
                else
                    pts[0].X = pts[1].X + Lenght;
            }
            PointList.Clear();
            PointList.AddRange(pts);
            base.ChangePropertyValue();
        }

        public override bool ChosedInRegion(Rectangle rect)
        {
            int containedNum = 0;
            int n = PointList.Count;
            Point[] pts = new Point[n];
            for (int i = 0; i < n; i++)
            {
                pts[i] = PointList[i];
                pts[i].Offset(pts[i].X * DrawMultiFactor - pts[i].X, pts[i].Y * DrawMultiFactor - pts[i].Y);
            }
            for (int i = 0; i < n; i++)
            {
                if (rect.Contains(pts[i]))
                    containedNum++;
            }
            if (containedNum == n)
                return true;
            return false;
        }

        public object Clone(string str)
        {
            RailEleLine cl = new RailEleLine();
            cl.PenLine = PenLine;
            cl.PointList.AddRange(PointList);
            cl.Lenght = Lenght;
            cl.DrawMultiFactor = DrawMultiFactor;
            cl.railText = str;
            return cl;
        }

        private Rectangle GetRedrawRc()
        {
            int n = PointList.Count;
            int minX, minY, maxX, maxY;
            maxX = minX = PointList[0].X * DrawMultiFactor;
            maxY = minY = PointList[0].Y * DrawMultiFactor;
            for (int i = 1; i < n; i++)
            {
                if (PointList[i].X * DrawMultiFactor < minX)
                    minX = PointList[i].X * DrawMultiFactor;
                else if (PointList[i].X * DrawMultiFactor > maxX)
                    maxX = PointList[i].X * DrawMultiFactor;
                if (PointList[i].Y * DrawMultiFactor < minY)
                    minY = PointList[i].Y * DrawMultiFactor;
                else if (PointList[i].Y * DrawMultiFactor > maxY)
                    maxY = PointList[i].Y * DrawMultiFactor;
            }
            Rectangle rc = new Rectangle(minX, minY, maxX - minX, maxY - minY);
            rc.Inflate(5, 5);
            return rc;
        }

    }
}
