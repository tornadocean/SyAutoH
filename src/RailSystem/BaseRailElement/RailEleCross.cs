using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.ComponentModel;

namespace BaseRailElement
{
    public class RailEleCross : Mcs.RailSystem.Common.EleCross
    {
        public RailEleCross()
        {
            GraphType = 3;
            PenCross.Width = PenWidth;
            PenCross.Color = PenColor;
            PenCross.DashStyle = PenDashStyle;
        }

        public RailEleCross CreateEle(Point pt, Size size, Int16 multiFactor, string text)
        {
            Point[] pts = new Point[8];
            DrawMultiFactor = multiFactor;
            pt.Offset(pt.X / DrawMultiFactor - pt.X, pt.Y / DrawMultiFactor - pt.Y);
            pts[0] = pt;
            pts[1].X = pts[0].X + FirstPart;
            pts[1].Y = pts[0].Y;
            pts[2].X = pts[1].X;
            pts[2].Y = pts[0].Y + 5;
            pts[3].X = pts[0].X + FirstPart + SecPart;
            pts[3].Y = pts[2].Y;
            pts[4].X = pts[3].X;
            pts[4].Y = pts[0].Y;
            pts[5].X = pts[0].X + LenghtOfStrai;
            pts[5].Y = pts[0].Y;
            pts[6].X = pts[1].X;
            pts[6].Y = pts[0].Y - 5;
            pts[7].X = pts[3].X;
            pts[7].Y = pts[0].Y - 45;
            PointList.AddRange(pts);
            DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.first;
            this.railText = text;
            return this;
        }

        public override void Draw(Graphics canvas)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            if (PointList.Count < 2)
            {
                throw new Exception("对象不存在");
            }
            int n = PointList.Count;
            Point[] points = new Point[2];
            for (int i = 0; i < n; i++, i++)
            {
                points[0] = PointList[i];
                points[1] = PointList[i + 1];
                for (int j = 0; j < 2; j++)
                {
                    points[j].Offset(points[j].X * DrawMultiFactor - points[j].X, points[j].Y * DrawMultiFactor - points[j].Y);
                }
                canvas.DrawLines(PenCross, points);
            }
        }

        public override void DrawTracker(Graphics canvas)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            Pen pen = new Pen(Color.Blue, 1);
            Point[] pts = new Point[4];
            pts[0] = PointList[0];
            pts[1] = PointList[3];
            pts[2] = PointList[5];
            pts[3] = PointList[7];
            if (DrawMultiFactor != 1)
            {
                for (int i = 0; i < pts.Length; i++)
                {
                    pts[i].Offset(pts[i].X * DrawMultiFactor - pts[i].X, pts[i].Y * DrawMultiFactor - pts[i].Y);
                }
            }
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
                int handleHit = HandleHitTest(point, DirectionOfCross, Mirror);
                if (handleHit > 0)
                    return handleHit;
            }
            Point wrapper = new Point();
            wrapper = point;
            Rectangle rc = new Rectangle();
            Point[] pts = new Point[4];
            if (!Mirror)
            {
                pts[0] = PointList[0];
                pts[2] = PointList[5];
            }
            else if (Mirror)
            {
                pts[0] = PointList[5];
                pts[2] = PointList[0];
            }
            pts[1] = PointList[3];
            pts[3] = PointList[7];
            if (DrawMultiFactor != 1)
            {
                Point tempPt = Point.Empty;
                int n = pts.Length;
                for (int i = 0; i < n; i++)
                {
                    tempPt = pts[i];
                    tempPt.Offset(tempPt.X * DrawMultiFactor - tempPt.X, tempPt.Y * DrawMultiFactor - tempPt.Y);
                    pts[i] = tempPt;
                }
            }
            switch (DirectionOfCross)
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

        private int HandleHitTest(Point point, Mcs.RailSystem.Common.EleCross.DirectionCross direction, bool isMirror)
        {
            Point[] pts = new Point[4];
            pts[0] = PointList[0];
            pts[1] = PointList[3];
            pts[2] = PointList[5];
            pts[3] = PointList[7];
            Point tempPt = Point.Empty;
            for (int i = 0; i < 4; i++)
            {
                Point pt = pts[i];
                pt.Offset(pt.X * DrawMultiFactor - pt.X, pt.Y * DrawMultiFactor - pt.Y);
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
            Point[] pts = new Point[8];
            PointList.CopyTo(pts);
            for (int i = 0; i < 8; i++)
            {
                pts[i].Offset(offsetX, offsetY);
            }
            PointList.Clear();
            PointList.AddRange(pts);
        }

        public override void MoveHandle(int handle, Point start, Point end)
        {
            if (sizeLock)
                return;
            int dx = (end.X - start.X) / DrawMultiFactor;
            int dy = (end.Y - start.Y) / DrawMultiFactor;
            Scale(handle, dx, dy);
        }

        protected void Scale(int handle, int dx, int dy)
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
                        if (Math.Abs(ptsList[0].X - ptsList[1].X) > (1 + Math.Abs(dx)))
                        {
                            ptsList[0].Offset(dx, 0);
                            if (Math.Abs(ptsList[0].X - ptsList[1].X) > (1 + Math.Abs(dx)))
                            {
                                PointList[0] = ptsList[0];
                                if (ptsList[0].X < ptsList[1].X)
                                    FirstPart -= dx;
                                else
                                    FirstPart += dx;
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
                                PointList.Clear();
                                PointList.AddRange(ptsList);
                                if (ptsList[0].X < ptsList[1].X)
                                    SecPart += dx;
                                else
                                    SecPart -= dx;
                            }
                        }
                        break;
                    case 3:
                        if (Math.Abs(ptsList[4].X - ptsList[5].X) > (1 + Math.Abs(dx)))
                        {
                            ptsList[5].Offset(dx, 0);
                            if (Math.Abs(ptsList[4].X - ptsList[5].X) > (1 + Math.Abs(dx)))
                            {
                                PointList[5] = ptsList[5];
                                if (ptsList[0].X < ptsList[1].X)
                                    ThPart += dx;
                                else
                                    ThPart -= dx;
                            }
                        }
                        break;
                    case 4:
                        if (!Mirror && Math.Abs(ptsList[7].X - ptsList[6].X) > (1 + Math.Abs(dx)) && (dx * dy) < 0)
                        {
                            ptsList[7].Offset(dx, -dx);
                            if (Math.Abs(ptsList[7].X - ptsList[6].X) > (1 + Math.Abs(dx)))
                            {
                                PointList[7] = ptsList[7];
                                if (ptsList[7].X > ptsList[6].X)
                                    FourPart.Offset(dx, dx);
                                else
                                    FourPart.Offset(-dx, -dx);
                            }
                        }
                        else if (Mirror && Math.Abs(ptsList[7].X - ptsList[6].X) > (1 + Math.Abs(dx)) && (dx * dy) > 0)
                        {
                            ptsList[7].Offset(dx, dx);
                            if (Math.Abs(ptsList[7].X - ptsList[6].X) > (1 + Math.Abs(dx)))
                            {
                                PointList[7] = ptsList[7];
                                if (PointList[7].X > PointList[6].X)
                                    FourPart.Offset(dx, dx);
                                else
                                    FourPart.Offset(-dx, -dx);
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
                                PointList[0] = ptsList[0];
                                if (PointList[0].Y < PointList[1].Y)
                                    FirstPart -= dy;
                                else
                                    FirstPart += dy;
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
                                PointList.Clear();
                                PointList.AddRange(ptsList);
                                if (PointList[0].Y < PointList[1].Y)
                                    SecPart += dy;
                                else
                                    SecPart -= dy;
                            }
                        }
                        break;
                    case 3:
                        if (Math.Abs(ptsList[4].Y - ptsList[5].Y) > (1 + Math.Abs(dy)))
                        {
                            ptsList[5].Offset(0, dy);
                            if (Math.Abs(ptsList[4].Y - ptsList[5].Y) > (1 + Math.Abs(dy)))
                            {
                                PointList[5] = ptsList[5];
                                if (PointList[0].Y < PointList[1].Y)
                                    ThPart += dy;
                                else
                                    ThPart -= dy;
                            }
                        }
                        break;
                    case 4:
                        if (!Mirror && Math.Abs(ptsList[7].Y - ptsList[6].Y) > (1 + Math.Abs(dy)) && (dx * dy) > 0)
                        {
                            ptsList[7].Offset(dy, dy);
                            if (Math.Abs(ptsList[7].Y - ptsList[6].Y) > (1 + Math.Abs(dy)))
                            {
                                PointList[7] = ptsList[7];
                                if (PointList[7].Y > PointList[6].Y)
                                    FourPart.Offset(dy, dy);
                                else
                                    FourPart.Offset(-dy, -dy);
                            }
                        }
                        else if (Mirror && Math.Abs(ptsList[7].Y - ptsList[6].Y) > (1 + Math.Abs(dy)) && (dx * dy) < 0)
                        {
                            ptsList[7].Offset(-dy, dy);
                            if (Math.Abs(ptsList[7].Y - ptsList[6].Y) > (1 + Math.Abs(dy)))
                            {
                                PointList[7] = ptsList[7];
                                if (PointList[7].Y > PointList[6].Y)
                                    FourPart.Offset(dy, dy);
                                else
                                    FourPart.Offset(-dy, -dy);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return ;
        }

        public override void RotateCounterClw()
        {
            base.RotateCounterClw();
            RotateAngle = -90;
            Matrix matrix = new Matrix();
            PointF ptCenter = new PointF();
            PointF[] points = new PointF[8];
            PointF[] pts = new PointF[4];
            pts[0] = PointList[0];
            pts[1] = PointList[3];
            pts[2] = PointList[5];
            pts[3] = PointList[7];
            StartAngle = (StartAngle + 360) % 360;
            switch (StartAngle)
            {
                case 0:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.first;
                    ptCenter.X = ((float)(pts[0].X + pts[2].X)) / 2;
                    ptCenter.Y = ((float)(pts[1].Y + pts[3].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.four;
                    break;
                case 90:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.second;
                    ptCenter.X = ((float)(pts[1].X + pts[3].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[2].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.first;
                    break;
                case 180:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.third;
                    ptCenter.X = ((float)(pts[0].X + pts[2].X)) / 2;
                    ptCenter.Y = ((float)(pts[1].Y + pts[3].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.second;
                    break;
                case 270:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.four;
                    ptCenter.X = ((float)(pts[1].X + pts[3].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[2].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.third;
                    break;
                default:
                    break;
            }
            StartAngle += RotateAngle;
            matrix.RotateAt(RotateAngle, ptCenter);
            for (int i = 0; i < 8; i++)
            {
                points[i] = PointList[i];
            }
            matrix.TransformPoints(points);
            PointList.Clear();
            for (int i = 0; i < 8; i++)
            {
                PointList.Add(Point.Ceiling(points[i]));
            }
        }

        public override void RotateClw()
        {
            base.RotateCounterClw();
            RotateAngle = 90;
            Matrix matrix = new Matrix();
            PointF ptCenter = new PointF();
            PointF[] points = new PointF[8];
            PointF[] pts = new PointF[4];
            pts[0] = PointList[0];
            pts[1] = PointList[3];
            pts[2] = PointList[5];
            pts[3] = PointList[7];
            StartAngle = (StartAngle + 360) % 360;
            switch (StartAngle)
            {
                case 0:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.first;
                    ptCenter.X = ((float)(pts[0].X + pts[2].X)) / 2;
                    ptCenter.Y = ((float)(pts[1].Y + pts[3].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.second;
                    break;
                case 90:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.second;
                    ptCenter.X = ((float)(pts[1].X + pts[3].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[2].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.third;
                    break;
                case 180:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.third;
                    ptCenter.X = ((float)(pts[0].X + pts[2].X)) / 2;
                    ptCenter.Y = ((float)(pts[1].Y + pts[3].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.four;
                    break;
                case 270:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.four;
                    ptCenter.X = ((float)(pts[1].X + pts[3].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[2].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.first;
                    break;
                default:
                    break;
            }
            StartAngle += RotateAngle;
            matrix.RotateAt(RotateAngle, ptCenter);
            for (int i = 0; i < 8; i++)
            {
                points[i] = PointList[i];
            }
            matrix.TransformPoints(points);
            PointList.Clear();
            for (int i = 0; i < 8; i++)
            {
                PointList.Add(Point.Ceiling(points[i]));
            }
        }

        public override void DrawEnlargeOrShrink(float draw_multi_factor)
        {
            DrawMultiFactor = Convert.ToInt16(draw_multi_factor);
            base.DrawEnlargeOrShrink(draw_multi_factor);
        }

        public override void ObjectMirror()
        {
            PointF ptCenter = PointF.Empty;
            PointF[] pts = new PointF[8];
            for (int i = 0; i < 8; i++)
                pts[i] = PointList[i];
            if (PointList[0].Y == PointList[5].Y)
            {
                ptCenter = new PointF((float)(PointList[0].X + PointList[5].X) / 2, PointList[0].Y);
                for (int i = 0; i < 8; i++)
                {
                    if (pts[i].X < ptCenter.X)
                        pts[i].X += 2 * Math.Abs(pts[i].X - ptCenter.X);
                    else
                        pts[i].X -= 2 * Math.Abs(pts[i].X - ptCenter.X);
                }
            }
            else if (PointList[0].X == PointList[5].X)
            {
                ptCenter = new PointF(PointList[0].X, (float)(PointList[0].Y + PointList[5].Y) / 2);
                for (int i = 0; i < 8; i++)
                {
                    if (pts[i].Y < ptCenter.Y)
                        pts[i].Y += 2 * Math.Abs(pts[i].Y - ptCenter.Y);
                    else
                        pts[i].Y -= 2 * Math.Abs(pts[i].Y - ptCenter.Y);
                }
            }
            if (Mirror)
                Mirror = false;
            else if (!Mirror)
                Mirror = true;
            PointList.Clear();
            for (int i = 0; i < 8; i++)
                PointList.Add(Point.Ceiling(pts[i]));
        }

        public override void ChangePropertyValue()
        {
            Point ptTemp = new Point(FourPart.X * DrawMultiFactor, FourPart.Y * DrawMultiFactor);
            Point[] ptsList = new Point[8];
            PointList.CopyTo(ptsList);
            switch (DirectionOfCross)
            {
                case DirectionCross.first:
                    if (PointList[0].X < PointList[1].X)
                    {
                        ptsList[0].X = PointList[1].X - FirstPart;
                        ptsList[3].X = ptsList[1].X + SecPart;
                        ptsList[4].X = ptsList[3].X;
                        ptsList[5].X = ptsList[3].X + ThPart;
                        ptsList[7].X = ptsList[6].X + FourPart.X;
                        ptsList[7].Y = ptsList[6].Y - FourPart.Y;
                    }
                    else
                    {
                        ptsList[0].X = PointList[1].X + FirstPart;
                        ptsList[3].X = ptsList[1].X - SecPart;
                        ptsList[4].X = ptsList[3].X;
                        ptsList[5].X = ptsList[3].X - ThPart;
                        ptsList[7].X = ptsList[6].X - FourPart.X;
                        ptsList[7].Y = ptsList[6].Y - FourPart.Y;
                    }
                    PointList.Clear();
                    PointList.AddRange(ptsList);
                    break;
                case DirectionCross.second:
                    if (PointList[0].Y < PointList[1].Y)
                    {
                        ptsList[0].Y = ptsList[1].Y - FirstPart;
                        ptsList[3].Y = ptsList[1].Y + SecPart;
                        ptsList[4].Y = ptsList[3].Y;
                        ptsList[5].Y = ptsList[3].Y + ThPart;
                        ptsList[7].X = ptsList[6].X + FourPart.X;
                        ptsList[7].Y = ptsList[6].Y + FourPart.Y;
                    }
                    else
                    {
                        ptsList[0].Y = ptsList[1].Y + FirstPart;
                        ptsList[3].Y = ptsList[1].Y - SecPart;
                        ptsList[4].Y = ptsList[3].Y;
                        ptsList[5].Y = ptsList[3].Y - ThPart;
                        ptsList[7].X = ptsList[6].X + FourPart.X;
                        ptsList[7].Y = ptsList[6].Y - FourPart.Y;
                    }
                    PointList.Clear();
                    PointList.AddRange(ptsList);
                    break;
                case DirectionCross.third:
                    if (PointList[0].X < PointList[1].X)
                    {
                        ptsList[0].X = PointList[1].X - FirstPart;
                        ptsList[3].X = ptsList[1].X + SecPart;
                        ptsList[4].X = ptsList[3].X;
                        ptsList[5].X = ptsList[3].X + ThPart;
                        ptsList[7].X = ptsList[6].X + FourPart.X;
                        ptsList[7].Y = ptsList[6].Y + FourPart.Y;
                    }
                    else
                    {
                        ptsList[0].X = PointList[1].X + FirstPart;
                        ptsList[3].X = ptsList[1].X - SecPart;
                        ptsList[4].X = ptsList[3].X;
                        ptsList[5].X = ptsList[3].X - ThPart;
                        ptsList[7].X = ptsList[6].X - FourPart.X;
                        ptsList[7].Y = ptsList[6].Y + FourPart.Y;
                    }
                    PointList.Clear();
                    PointList.AddRange(ptsList);
                    break;
                case DirectionCross.four:
                    if (PointList[0].Y < PointList[1].Y)
                    {
                        ptsList[0].Y = ptsList[1].Y - FirstPart;
                        ptsList[3].Y = ptsList[1].Y + SecPart;
                        ptsList[4].Y = ptsList[3].Y;
                        ptsList[5].Y = ptsList[3].Y + ThPart;
                        ptsList[7].X = ptsList[6].X - FourPart.X;
                        ptsList[7].Y = ptsList[6].Y + FourPart.Y;
                    }
                    else
                    {
                        ptsList[0].Y = ptsList[1].Y + FirstPart;
                        ptsList[3].Y = ptsList[1].Y - SecPart;
                        ptsList[4].Y = ptsList[3].Y;
                        ptsList[5].Y = ptsList[3].Y - ThPart;
                        ptsList[7].X = ptsList[6].X - FourPart.X;
                        ptsList[7].Y = ptsList[6].Y - FourPart.Y;
                    }
                    PointList.Clear();
                    PointList.AddRange(ptsList);
                    break;
                case DirectionCross.NULL:
                    break;
            }
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

        public override DataRow SaveEleInfo(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["GraphType"] = GraphType;
            dr["LocationLock"] = locationLock;
            dr["SizeLock"] = sizeLock;
            dr["Selectable"] = Selectable;
            dr["Speed"] = Speed;
            dr["Mirror"] = Mirror;
            dr["FirstPart"] = FirstPart;
            dr["SecPart"] = SecPart;
            dr["ThPart"] = ThPart;
            dr["FourPart"] = FourPart.ToString(); ;
            dr["StartAngle"] = StartAngle;
            dr["RotateAngle"] = RotateAngle;
            dr["DirectionOfCross"] = DirectionOfCross;
            dr["PointListVol"] = PointList.Count;
            for (int i = 0; i < PointList.Count; i++)
            {
                dr["PointList" + i.ToString()] = PointList[i];
            }

            dr["drawMultiFactor"] = DrawMultiFactor;
            dr["startPoint"] = StartPoint.ToString();
            dr["endPoint"] = EndPoint.ToString();
            dr["CodingBegin"] = CodingBegin;
            dr["CodingEnd"] = CodingEnd;
            dr["CodingEndS"] = CodingEndS;
            dr["CodingPrev"] = CodingPrev;
            dr["CodingNext"] = CodingNext;
            dr["CodingNextS"] = CodingNextS;
            dr["railText"] = railText;
            dr["lenghtOfStrai"] = LenghtOfStrai;
            dr["Color"] = ColorTranslator.ToHtml(PenCross.Color);
            dr["DashStyle"] = PenCross.DashStyle;
            dr["PenWidth"] = PenCross.Width;

            dt.Rows.Add(dr);
            return dr;
        }

        public override DataRow SaveCodingInfo(DataTable dt)
        {
            DataRow dr = dt.NewRow();

            dr["GraphType"] = GraphType;
            dr["CodingBegin"] = CodingBegin;
            dr["CodingEnd"] = CodingEnd;
            dr["CodingEndS"] = CodingEndS;
            dr["CodingPrev"] = CodingPrev;
            dr["CodingNext"] = CodingNext;
            dr["CodingNextS"] = CodingNextS;

            dt.Rows.Add(dr);
            return dr;
        }

        public object Clone(string str)
        {
            RailEleCross cl = new RailEleCross();
            Point[] pts = new Point[8];
            PointList.CopyTo(pts);
            for (int i = 0; i < 8; i++)
            {
                pts[i].Offset(20, 20);
            }
            cl.PointList.AddRange(pts);
            cl.LenghtOfStrai = LenghtOfStrai;
            cl.StartAngle = StartAngle;
            cl.RotateAngle = RotateAngle;
            cl.DirectionOfCross = DirectionOfCross;
            cl.DrawMultiFactor = DrawMultiFactor;
            cl.FirstPart = FirstPart;
            cl.SecPart = SecPart;
            cl.ThPart = ThPart;
            cl.FourPart = FourPart;
            cl.Mirror = Mirror;
            cl.railText = str;
            cl.PenCross = PenCross;
            return cl;
        }

    }
}
