using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace BaseRailElement
{
    public class ObjectCrossOp
    {
        private int drawMultiFactor = 1;
        public int DrawMultiFactor
        {
            set { drawMultiFactor = value; }
        }

        private List<Point> pointList = new List<Point>();
        public List<Point> PointList
        {
            get { return pointList; }
            set { pointList = value; }
        }

        private int firstPart = 30;
        public int FirstPart
        {
            get { return firstPart; }
            set { firstPart = value; }
        }
        private int secPart = 40;
        public int SecPart
        {
            get { return secPart; }
            set { secPart = value; }
        }
        private int thPart = 30;
        public int ThPart
        {
            get { return thPart; }
            set { thPart = value; }
        }

        private Point fourPart = new Point(40, 40);
        public Point FourPart
        {
            get { return fourPart; }
            set { fourPart = value; }
        }

        public void DrawTracker(Graphics canvas, Mcs.RailSystem.Common.EleCross.DirectionCross direction)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            Pen pen = new Pen(Color.Blue, 1);
            //SolidBrush bsh = new SolidBrush(Color.Blue);
            Point[] pts = new Point[4];
            pts[0] = pointList[0];
            pts[1] = pointList[3];
            pts[2] = pointList[5];
            pts[3] = pointList[7];
            if (drawMultiFactor != 1)
            {
                for (int i = 0; i < pts.Length; i++)
                {
                    pts[i].Offset(pts[i].X * drawMultiFactor - pts[i].X, pts[i].Y * drawMultiFactor - pts[i].Y);
                }
            }
            for (int i = 0; i < 4; i++)
            {
                Rectangle rc = new Rectangle(pts[i].X - 4, pts[i].Y - 4, 8, 8);
                canvas.DrawRectangle(pen, rc);
                //canvas.FillRectangle(bsh, rc);
            }
            pen.Dispose();
            //bsh.Dispose();
        }

        public int HitTest(
            Point point,
            bool isSelected,
            Mcs.RailSystem.Common.EleCross.DirectionCross direction,
            bool isMirror)
        {
            if (isSelected)
            {
                int handleHit = HandleHitTest(point, direction, isMirror);
                if (handleHit > 0)
                    return handleHit;
            }
            Point wrapper = new Point();
            wrapper = point;
            Rectangle rc = new Rectangle();
            Point[] pts = new Point[4];
            if (!isMirror)
            {
                pts[0] = pointList[0];
                pts[2] = pointList[5];
            }
            else if (isMirror)
            {
                pts[0] = pointList[5];
                pts[2] = pointList[0];
            }
            pts[1] = pointList[3];
            pts[3] = pointList[7];
            if (drawMultiFactor != 1)
            {
                Point tempPt = Point.Empty;
                int n = pts.Length;
                for (int i = 0; i < n; i++)
                {
                    tempPt = pts[i];
                    tempPt.Offset(tempPt.X * drawMultiFactor - tempPt.X, tempPt.Y * drawMultiFactor - tempPt.Y);
                    pts[i] = tempPt;
                }
            }
            switch (direction)
            {
                case Mcs.RailSystem.Common.EleCross.DirectionCross.first:
                    rc = new Rectangle(pts[0].X, pts[3].Y, pts[2].X - pts[0].X, pts[1].Y - pts[3].Y);
                    break;
                case Mcs.RailSystem.Common.EleCross.DirectionCross.second:
                    rc = new Rectangle(pts[1].X, pts[0].Y, pts[3].X - pts[1].X, pts[2].Y - pts[0].Y);
                    break;
                case Mcs.RailSystem.Common.EleCross.DirectionCross.third:
                    rc = new Rectangle(pts[2].X, pts[1].Y, pts[0].X - pts[2].X, pts[3].Y - pts[1].Y);
                    break;
                case Mcs.RailSystem.Common.EleCross.DirectionCross.four:
                    rc = new Rectangle(pts[3].X, pts[2].Y, pts[1].X - pts[3].X, pts[0].Y - pts[2].Y);
                    break;
                default:
                    break;
            }
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(rc);
            Region region = new Region(path);
            if (region.IsVisible(wrapper))
                return 0;
            else
                return -1;
        }

        public int HandleHitTest(Point point, Mcs.RailSystem.Common.EleCross.DirectionCross direction, bool isMirror)
        {
            Point[] pts = new Point[4];
            pts[0] = pointList[0];
            pts[1] = pointList[3];
            pts[2] = pointList[5];
            pts[3] = pointList[7];
            Point tempPt = Point.Empty;
            for (int i = 0; i < 4; i++)
            {
                Point pt = pts[i];
                pt.Offset(pt.X * drawMultiFactor - pt.X, pt.Y * drawMultiFactor - pt.Y);
                Rectangle rc = new Rectangle(pt.X - 3, pt.Y - 3, 6, 6);
                if (rc.Contains(point))
                    return i + 1;
            }
            return -1;
        }

        public int scale(int handle, int dx, int dy, bool isMirror)
        {
            Debug.WriteLine(string.Format("object orign first is {0},sec is {1},th is {2}", firstPart, secPart, thPart));
            Point[] ptsList = new Point[8];
            Point[] ptsHandle = new Point[4];
            pointList.CopyTo(ptsList);
            ptsHandle[0] = pointList[0];
            ptsHandle[1] = pointList[3];
            ptsHandle[2] = pointList[5];
            ptsHandle[3] = pointList[7];
            if (ptsHandle[0].Y == ptsHandle[2].Y)
            {
                switch (handle)
                {
                    case 1:
                        if (Math.Abs(ptsList[0].X - ptsList[1].X) > (1 + Math.Abs(dx)))
                        {
                            ptsList[0].Offset(dx, 0);
                            if (Math.Abs(ptsList[0].X - ptsList[1].X) > (1 + Math.Abs(dx)))
                            {
                                pointList[0] = ptsList[0];
                                if (ptsList[0].X < ptsList[1].X)
                                    firstPart -= dx;
                                else
                                    firstPart += dx;
                            }
                        }
                        break;
                    case 2:
                        if (Math.Abs(ptsList[2].X - ptsList[3].X) > (1 + Math.Abs(dx)))
                        {
                            ptsList[3].Offset(dx, 0);
                            ptsList[4].X += dx;
                            ptsList[5].X += dx;
                            if (Math.Abs(ptsList[2].X - ptsList[3].X) > (1 + Math.Abs(dx)))
                            {
                                pointList.Clear();
                                pointList.AddRange(ptsList);
                                if (ptsList[0].X < ptsList[1].X)
                                    secPart += dx;
                                else
                                    secPart -= dx;
                            }
                        }
                        break;
                    case 3:
                        if (Math.Abs(ptsList[4].X - ptsList[5].X) > (1 + Math.Abs(dx)))
                        {
                            ptsList[5].Offset(dx, 0);
                            if (Math.Abs(ptsList[4].X - ptsList[5].X) > (1 + Math.Abs(dx)))
                            {
                                pointList[5] = ptsList[5];
                                if (ptsList[0].X < ptsList[1].X)
                                    thPart += dx;
                                else
                                    thPart -= dx;
                            }
                        }
                        break;
                    case 4:
                        if (!isMirror && Math.Abs(ptsList[7].X - ptsList[6].X) > (1 + Math.Abs(dx)) && (dx * dy) < 0)
                        {
                            ptsList[7].Offset(dx, -dx);
                            if (Math.Abs(ptsList[7].X - ptsList[6].X) > (1 + Math.Abs(dx)))
                            {
                                pointList[7] = ptsList[7];
                                if (ptsList[7].X > ptsList[6].X)
                                    fourPart.Offset(dx, dx);
                                else
                                    fourPart.Offset(-dx, -dx);
                            }
                        }
                        else if (isMirror && Math.Abs(ptsList[7].X - ptsList[6].X) > (1 + Math.Abs(dx)) && (dx * dy) > 0)
                        {
                            ptsList[7].Offset(dx, dx);
                            if (Math.Abs(ptsList[7].X - ptsList[6].X) > (1 + Math.Abs(dx)))
                            {
                                pointList[7] = ptsList[7];
                                if (pointList[7].X > pointList[6].X)
                                    fourPart.Offset(dx, dx);
                                else
                                    fourPart.Offset(-dx, -dx);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (ptsHandle[0].X == ptsHandle[2].X)
            {
                switch (handle)
                {
                    case 1:
                        if (Math.Abs(ptsList[0].Y - ptsList[1].Y) > (1 + Math.Abs(dy)))
                        {
                            ptsList[0].Offset(0, dy);
                            if (Math.Abs(ptsList[0].Y - ptsList[1].Y) > (1 + Math.Abs(dy)))
                            {
                                pointList[0] = ptsList[0];
                                if (pointList[0].Y < pointList[1].Y)
                                    firstPart -= dy;
                                else
                                    firstPart += dy;
                            }
                        }
                        break;
                    case 2:
                        if (Math.Abs(ptsList[2].Y - ptsList[3].Y) > (1 + Math.Abs(dy)))
                        {
                            ptsList[3].Offset(0, dy);
                            ptsList[4].Y += dy;
                            ptsList[5].Y += dy;
                            if (Math.Abs(ptsList[2].Y - ptsList[3].Y) > (1 + Math.Abs(dy)))
                            {
                                pointList.Clear();
                                pointList.AddRange(ptsList);
                                if (pointList[0].Y < pointList[1].Y)
                                    secPart += dy;
                                else
                                    secPart -= dy;
                            }
                        }
                        break;
                    case 3:
                        if (Math.Abs(ptsList[4].Y - ptsList[5].Y) > (1 + Math.Abs(dy)))
                        {
                            ptsList[5].Offset(0, dy);
                            if (Math.Abs(ptsList[4].Y - ptsList[5].Y) > (1 + Math.Abs(dy)))
                            {
                                pointList[5] = ptsList[5];
                                if (pointList[0].Y < pointList[1].Y)
                                    thPart += dy;
                                else
                                    thPart -= dy;
                            }
                        }
                        break;
                    case 4:
                        if (!isMirror && Math.Abs(ptsList[7].Y - ptsList[6].Y) > (1 + Math.Abs(dy)) && (dx * dy) > 0)
                        {
                            ptsList[7].Offset(dy, dy);
                            if (Math.Abs(ptsList[7].Y - ptsList[6].Y) > (1 + Math.Abs(dy)))
                            {
                                pointList[7] = ptsList[7];
                                if (pointList[7].Y > pointList[6].Y)
                                    fourPart.Offset(dy, dy);
                                else
                                    fourPart.Offset(-dy, -dy);
                            }
                        }
                        else if (isMirror && Math.Abs(ptsList[7].Y - ptsList[6].Y) > (1 + Math.Abs(dy)) && (dx * dy) < 0)
                        {
                            ptsList[7].Offset(-dy, dy);
                            if (Math.Abs(ptsList[7].Y - ptsList[6].Y) > (1 + Math.Abs(dy)))
                            {
                                pointList[7] = ptsList[7];
                                if (pointList[7].Y > pointList[6].Y)
                                    fourPart.Offset(dy, dy);
                                else
                                    fourPart.Offset(-dy, -dy);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            Debug.WriteLine(string.Format("object first is {0},sec is {1},th is {2}", firstPart, secPart, thPart));
            return 0;
        }

        public bool ChosedInRegion(Rectangle rect)
        {
            int containedNum = 0;
            int n = pointList.Count;
            Point[] pts = new Point[n];
            for (int i = 0; i < n; i++)
            {
                pts[i] = pointList[i];
                pts[i].Offset(pts[i].X * drawMultiFactor - pts[i].X, pts[i].Y * drawMultiFactor - pts[i].Y);
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
    }
}
