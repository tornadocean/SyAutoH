using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BaseRailElement
{
    public class RailAuxiliaryDraw : Mcs.RailSystem.Common.BaseRailEle
    {
        private Pen pen = new Pen(Color.Black, 1);
        private bool bFinish = false;
        private Point[] ptsCurve;
        private Rectangle rc = new Rectangle();

        public Pen PenDraw
        {
            get { return pen; }
            set { pen = value; }
        }
        public bool BFinish
        {
            get { return bFinish; }
            set { bFinish = value; }
        }
        public Point[] PtsCurve
        {
            get { return ptsCurve; }
            set { ptsCurve = value; }
        }
        public Rectangle Rc
        {
            get { return rc; }
            set { rc = value; }
        }

        public RailAuxiliaryDraw()
        {
            GraphType = 4;
        }

        public RailAuxiliaryDraw CreateEle(Int16 railType, Int16 multiFactor,Point ptStart)
        {
            RailAuxiliaryDrawType = railType;
            DrawMultiFactor = multiFactor;
            DotStart = ptStart;
            DotEnd = ptStart;
            DotEndFork = ptStart;

            if (3 == railType)
            {
                ptsCurve = new Point[3];
                ptsCurve[0] = ptStart;
                ptsCurve[1] = ptStart;
                ptsCurve[2] = ptStart;
            }
            return this;
        }

        public override void Draw(Graphics canvas)
        {
            if (canvas == null)
            {
                throw new Exception("Graphics对象Canvas不能为空");
            }
            switch (RailAuxiliaryDrawType)
            { 
                case 0:
                    int topRc = DotStart.X < DotEnd.X ? DotStart.X : DotEnd.X;
                    int leftRc = DotStart.Y < dotEnd.Y ? DotStart.Y : dotEnd.Y;
                    int widthRc = Math.Abs(DotStart.X - DotEnd.X);
                    int heightRc = Math.Abs(DotStart.Y - DotEnd.Y);
                    rc.X = topRc;
                    rc.Y = leftRc;
                    rc.Width = widthRc;
                    rc.Height = heightRc;
                    canvas.DrawRectangle(pen, rc);
                    break;
                case 1:
                    int topEl = DotStart.X < DotEnd.X ? DotStart.X : DotEnd.X;
                    int leftEl = DotStart.Y < dotEnd.Y ? DotStart.Y : dotEnd.Y;
                    int widthEl = Math.Abs(DotStart.X - DotEnd.X);
                    int heightEl = Math.Abs(DotStart.Y - DotEnd.Y);
                    rc.X = topEl;
                    rc.Y = leftEl;
                    rc.Width = widthEl;
                    rc.Height = heightEl;
                    canvas.DrawEllipse(pen,rc);
                    break;
                case 2:
                    canvas.DrawLine(pen, DotStart, DotEnd);
                    break;
                case 3:
                    canvas.DrawCurve(pen, ptsCurve);
                    break;

            }
        }

        public override void DrawTracker(Graphics canvas)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            Point[] pts = new Point[4];
            Pen pen = new Pen(Color.Blue, 1);
            switch (RailAuxiliaryDrawType)
            { 
                case 0:
                case 1:
                    pts[0] = rc.Location;
                    pts[1].X = pts[0].X;
                    pts[1].Y = pts[0].Y + rc.Height;
                    pts[2].X = pts[0].X + rc.Width;
                    pts[2].Y = pts[0].Y;
                    pts[3].X = pts[0].X + rc.Width;
                    pts[3].Y = pts[0].Y + rc.Height;
                    for (int i = 0; i < 4; i++)
                    {
                        Rectangle rctemp = new Rectangle(pts[i].X - 4, pts[i].Y - 4, 8, 8);
                        canvas.DrawRectangle(pen, rctemp);
                    }
                    break;
                case 2:
                    pts[0] = dotStart;
                    pts[1] = dotEnd;
                    for (int i = 0; i < 2;i++ )
                    {
                        Rectangle rctemp = new Rectangle(pts[i].X - 4, pts[i].Y - 4, 8, 8);
                        canvas.DrawRectangle(pen, rctemp);
                    }
                    break;
                case 3:
                    for (int i = 0; i < 3;i++ )
                    {
                        Rectangle rctemp = new Rectangle(ptsCurve[i].X - 4, ptsCurve[i].Y - 4, 8, 8);
                        canvas.DrawRectangle(pen, rctemp);
                    }
                    break;
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
            if (0 == RailAuxiliaryDrawType || 1 == RailAuxiliaryDrawType)
            {
                GraphicsPath path = new GraphicsPath();
                path.AddRectangle(rc);
                Region region = new Region(path);
                if (region.IsVisible(point))
                    return 0;
            }
            else if (2 == RailAuxiliaryDrawType)
            {
                Point pt1 = dotStart;
                Point pt2 = dotEnd;
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
                Rectangle rc = new Rectangle(pt1.X - 5, pt1.Y - 5, length + 10, 10);
                Point[] wrapper = new Point[1];
                wrapper[0] = point;
                if (angle != 0)
                {
                    Matrix matrix = new Matrix();
                    matrix.RotateAt(-angle, pt1);
                    matrix.TransformPoints(wrapper);
                }
                if (rc.Contains(wrapper[0]))
                    return 0;
            }
            else if (3 == RailAuxiliaryDrawType)
            {
                Point wrapper = new Point();
                wrapper = point;
                Point[] pts = new Point[3];
                for (int i = 0; i < 3; i++)
                {
                    pts[i] = ptsCurve[i];
                    pts[i].Offset(pts[i].X * DrawMultiFactor - pts[i].X, pts[i].Y * DrawMultiFactor - pts[i].Y);
                }
                GraphicsPath path = new GraphicsPath();
                path.AddPolygon(pts);
                Region region = new Region(path);
                if (region.IsVisible(wrapper))
                    return 0;
            }
            return -1;
        }

        private int HandleHitTest(Point point)
        {
            if (0 == RailAuxiliaryDrawType || 1 == RailAuxiliaryDrawType)
            {
                Point[] pts=new Point[4];
                pts[0] = new Point() { X = rc.X, Y = rc.Y };
                pts[1] = new Point() { X = rc.X, Y = rc.Y + rc.Height };
                pts[2] = new Point() { X = rc.X + rc.Width, Y = rc.Y };
                pts[3] = new Point() { X = rc.X + rc.Width, Y = rc.Y + rc.Height };
                for (int i = 0; i < 4; i++)
                {
                    Point pt = pts[i];
                    Rectangle rctemp = new Rectangle(pt.X - 3, pt.Y - 3, 6, 6);
                    if (rctemp.Contains(point))
                        return i + 1;
                }
            }
            else if (2 == RailAuxiliaryDrawType)
            {
                Point[] pts = new Point[2];
                pts[0] = dotStart;
                pts[1] = dotEnd;
                for (int i = 0; i < 2; i++)
                {
                    pts[i].Offset(pts[i].X * (DrawMultiFactor - 1), pts[i].Y * (DrawMultiFactor - 1));
                    Point pt = pts[i];
                    Rectangle rc = new Rectangle(pt.X - 3, pt.Y - 3, 6, 6);
                    if (rc.Contains(point))
                        return i + 1;
                }
            }
            else if (3 == RailAuxiliaryDrawType)
            {
                Point[] pts = new Point[3];
                pts[0] = ptsCurve[0];
                pts[1] = ptsCurve[1];
                pts[2] = ptsCurve[2];
                for (int i = 0; i < 3; i++)
                {
                    Point pt = pts[i];
                    pt.Offset(pt.X * DrawMultiFactor - pt.X, pt.Y * DrawMultiFactor - pt.Y);
                    Rectangle rc = new Rectangle(pt.X - 3, pt.Y - 3, 6, 6);
                    if (rc.Contains(point))
                        return i + 1;
                }
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
            Point pt = new Point();
            switch (RailAuxiliaryDrawType)
            { 
                case 0:
                case 1:
                case 2:
                    pt = dotStart;
                    pt.Offset(offsetX, offsetY);
                    dotStart = pt;
                    pt = dotEnd;
                    pt.Offset(offsetX, offsetY);
                    dotEnd = pt;
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        pt = ptsCurve[i];
                        pt.Offset(offsetX, offsetY);
                        ptsCurve[i] = pt;
                    }
                    break;
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
            switch (RailAuxiliaryDrawType)
            { 
                case 0:
                case 1:
                    if (rc.Width > 0 && rc.Height > 0)
                    {
                        rc.Width += dx;
                        rc.Height += dy;
                    }
                    else if (dx > 0 || dy > 0)
                    {
                        if (dx > 0 && dy > 0)
                        {
                            rc.Width += dx;
                            rc.Height += dy;
                        }
                        else if (dx > 0) { rc.Width += dx; }
                        else if (dy > 0) { rc.Height += dy; }
                    }
                    dotStart = rc.Location;
                    dotEnd.X = rc.X + rc.Width;
                    dotEnd.Y = rc.Y + rc.Height;
                    break;
                case 2:
                    Point[] pts = new Point[2];
                    pts[0] = dotStart;
                    pts[1] = dotEnd;
                    Point pt = pts[handle - 1];
                    pt.Offset(dx, dy);
                    pts[handle - 1] = pt;
                    dotStart = pts[0];
                    dotEnd = pts[1];
                    break;
                case 3:
                    Point[] ptsTemp = new Point[3];
                    for (int i = 0; i < 3;i++ )
                    {
                        ptsTemp[i] = ptsCurve[i];
                    }
                    ptsTemp[handle - 1].Offset(dx, dy);
                    for (int i = 0; i < 3; i++)
                    {
                        ptsCurve[i] = ptsTemp[i];
                    }
                    break;
            }
        }

        public override void DrawEnlargeOrShrink(float draw_multi_factor)
        {
            DrawMultiFactor = Convert.ToInt16(draw_multi_factor);
            base.DrawEnlargeOrShrink(draw_multi_factor);
        }

        public override bool ChosedInRegion(Rectangle rect)
        {
            GraphicsPath pathRc = new GraphicsPath();
            pathRc.AddRectangle(rect);
            Region regionRc = new Region(pathRc);
            switch (RailAuxiliaryDrawType)
            { 
                case 0:
                case 1:
                case 2:
                    if (regionRc.IsVisible(dotStart)
                        && regionRc.IsVisible(dotEnd))
                        return true;
                    break;
                case 3:
                    if (regionRc.IsVisible(ptsCurve[0])
                        && regionRc.IsVisible(ptsCurve[1])
                        && regionRc.IsVisible(ptsCurve[2]))
                        return true;
                    break;
            }
            return false;
        }



    }
}
