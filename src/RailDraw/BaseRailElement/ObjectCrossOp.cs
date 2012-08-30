﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BaseRailElement
{
    public class ObjectCrossOp
    {
        private List<Point> pointList = new List<Point>();
        public List<Point> PointList
        {
            get { return pointList; }
            set { pointList = value; }
        }

        private List<Point> saveList = new List<Point>();
        public List<Point> SaveList
        {
            get { return saveList; }
            set { saveList = value; }
        }

        public void DrawTracker(Graphics canvas, CrossEle.DirectionCross direction)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            Pen pen = new Pen(Color.White, 2);
            SolidBrush bsh = new SolidBrush(Color.Black);
            Point[] pts = new Point[4];
            pts[0] = PointList[0];
            pts[1] = PointList[3];
            pts[2] = PointList[5];
            pts[3] = PointList[7];           
            for (int i = 0; i < 4; i++)
            {
                Rectangle rc = new Rectangle(pts[i].X - 2, pts[i].Y - 2, 4, 4);
                canvas.DrawRectangle(pen, rc);
                canvas.FillRectangle(bsh, rc);
            }
            pen.Dispose();
            bsh.Dispose();
        }
        public int HitTest(
            Point point,
            bool isSelected,
            CrossEle.DirectionCross direction,
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
                pts[0] = PointList[0];
                pts[2] = PointList[5];
            }
            else if (isMirror)
            {
                pts[0] = PointList[5];
                pts[2] = PointList[0];
            }
            pts[1] = PointList[3];           
            pts[3] = PointList[7];
            switch (direction)
            {
                case CrossEle.DirectionCross.first:
                    rc = new Rectangle(pts[0].X, pts[3].Y, pts[2].X - pts[0].X, pts[1].Y - pts[3].Y);
                    break;
                case CrossEle.DirectionCross.second:
                    rc = new Rectangle(pts[1].X, pts[0].Y, pts[3].X - pts[1].X, pts[2].Y - pts[0].Y);
                    break;
                case CrossEle.DirectionCross.third:
                    rc = new Rectangle(pts[2].X, pts[1].Y, pts[0].X - pts[2].X, pts[3].Y - pts[1].Y);
                    break;
                case CrossEle.DirectionCross.four:
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

        public int HandleHitTest(Point point, CrossEle.DirectionCross direction, bool isMirror)
        {
            Point[] pts = new Point[4];
            pts[0] = PointList[0];
            pts[1] = PointList[3];
            pts[2] = PointList[5];
            pts[3] = PointList[7];
            for (int i = 0; i < 4; i++)
            {
                Point pt = pts[i];
                Rectangle rc = new Rectangle(pt.X - 3, pt.Y - 3, 6, 6);
                if (rc.Contains(point))
                    return i + 1;
            }
            return -1;
        }

        public int scale(int handle, int dx, int dy, bool isMirror)
        {
            Point[] ptsList = new Point[8];
            Point[] ptsHandle = new Point[4];
            PointList.CopyTo(ptsList);
            ptsHandle[0] = PointList[0];
            ptsHandle[1] = PointList[3];
            ptsHandle[2] = PointList[5];
            ptsHandle[3] = PointList[7];
            if (ptsHandle[0].Y == ptsHandle[2].Y)
            {
                switch (handle)
                {
                    case 1:
                        if (Math.Abs(ptsList[0].X - ptsList[1].X) > (1+Math.Abs(dx)))
                        {
                            ptsList[0].Offset(dx, 0);
                            if (Math.Abs(ptsList[0].X - ptsList[1].X) > (1 + Math.Abs(dx)))
                                PointList[0] = ptsList[0];
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
                                PointList.Clear();
                                PointList.AddRange(ptsList);
                            }
                        }
                        break;
                    case 3:
                        if (Math.Abs(ptsList[4].X - ptsList[5].X) > (1 + Math.Abs(dx)))
                        {
                            ptsList[5].Offset(dx, 0);
                            if (Math.Abs(ptsList[4].X - ptsList[5].X) > (1 + Math.Abs(dx)))
                                PointList[5] = ptsList[5];
                        }
                        break;
                    case 4:
                        if (!isMirror && Math.Abs(ptsList[7].X - ptsList[6].X) > (1 + Math.Abs(dx)) && (dx * dy) < 0)
                        {
                            ptsList[7].Offset(dx, -dx);
                            if (Math.Abs(ptsList[7].X - ptsList[6].X) > (1 + Math.Abs(dx)))
                                PointList[7] = ptsList[7];
                        }
                        else if (isMirror && Math.Abs(ptsList[7].X - ptsList[6].X) > (1 + Math.Abs(dx)) && (dx * dy) > 0)
                        {
                            ptsList[7].Offset(dx, dx);
                            if (Math.Abs(ptsList[7].X - ptsList[6].X) > (1 + Math.Abs(dx)))
                                PointList[7] = ptsList[7];
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
                                PointList[0] = ptsList[0];
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
                                PointList.Clear();
                                PointList.AddRange(ptsList);
                            }
                        }
                        break;
                    case 3:
                        if (Math.Abs(ptsList[4].Y - ptsList[5].Y) > (1 + Math.Abs(dy)))
                        {
                            ptsList[5].Offset(0, dy);
                            if (Math.Abs(ptsList[4].Y - ptsList[5].Y) > (1 + Math.Abs(dy)))
                                PointList[5] = ptsList[5];
                        }
                        break;
                    case 4:
                        if (!isMirror && Math.Abs(ptsList[7].Y - ptsList[6].Y) > (1 + Math.Abs(dy)) && (dx * dy) > 0)
                        {
                            ptsList[7].Offset(dy, dy);
                            if (Math.Abs(ptsList[7].Y - ptsList[6].Y) > (1 + Math.Abs(dy)))
                                PointList[7] = ptsList[7];
                        }
                        else if (isMirror && Math.Abs(ptsList[7].Y - ptsList[6].Y) > (1 + Math.Abs(dy)) && (dx * dy) < 0)
                        {
                            ptsList[7].Offset(-dy, dy);
                            if (Math.Abs(ptsList[7].Y - ptsList[6].Y) > (1 + Math.Abs(dy)))
                                PointList[7] = ptsList[7];
                        }
                        break;
                    default:
                        break;
                }
            }
            return 0;
        }
    }
}
