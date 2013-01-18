using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            DrawMultiFactor = multiFactor;
            Point[] pts = new Point[3];
            pt.Offset(pt.X / DrawMultiFactor - pt.X, pt.Y / DrawMultiFactor - pt.Y);
            pts[0] = pt;
            pts[1].X = pts[0].X + Lenght;
            pts[1].Y = pts[0].Y;
            pts[2] = pts[1];
            int rotateAngle = -45;
            Matrix matrix = new Matrix();
            Point[] points = new Point[1];
            points[0] = pts[2];
            matrix.RotateAt(rotateAngle, pts[0]);
            matrix.TransformPoints(points);
            pts[2] = points[0];
            PointList.AddRange(pts);
            DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.first;
            dotStart = PointList[0];
            dotEnd = PointList[1];
            dotEndFork = PointList[2];
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
            Point[] points = new Point[n];
            for (int i = 0; i < n; i++)
            {
                points[i] = PointList[i];
                points[i].Offset(points[i].X * DrawMultiFactor - points[i].X, points[i].Y * DrawMultiFactor - points[i].Y);
            }
            canvas.DrawLine(PenCross, points[0], points[2]);
            PenCross.DashStyle = DashStyle.Dot;
            canvas.DrawLine(PenCross, points[0], points[1]);
            PenCross.DashStyle = DashStyle.Solid;
        }

        public override void DrawTracker(Graphics canvas)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            Pen pen = new Pen(Color.Blue, 1);
            pen.Color = TrackerColor;
            int num = PointList.Count;
            Point[] pts = new Point[num];
            for (int i = 0; i < num;i++ )
            { 
                pts[i] = PointList[i]; 
            }
            if (DrawMultiFactor != 1)
            {
                for (int i = 0; i < pts.Length; i++)
                {
                    pts[i].Offset(pts[i].X * DrawMultiFactor - pts[i].X, pts[i].Y * DrawMultiFactor - pts[i].Y);
                }
            }
            for (int i = 0; i < num; i++)
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
            Point[] pts = new Point[3];
            for (int i = 0; i < PointList.Count;i++ )
            {
                pts[i] = PointList[i];
                pts[i].Offset(pts[i].X * DrawMultiFactor - pts[i].X, pts[i].Y * DrawMultiFactor - pts[i].Y);
            }
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(pts);
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
            pts[1] = PointList[1];
            pts[2] = PointList[2];
            for (int i = 0; i < 3;i++ )
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
            Point[] pts = new Point[3];
            PointList.CopyTo(pts);
            for (int i = 0; i < 3; i++)
            {
                pts[i].Offset(offsetX, offsetY);
            }
            PointList.Clear();
            PointList.AddRange(pts);
            dotStart = PointList[0];
            dotEnd = PointList[1];
            dotEndFork = PointList[2];
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
            Point[] ptsList = new Point[3];
            PointList.CopyTo(ptsList);
            if (ptsList[0].Y == ptsList[1].Y)
            {
                switch (handle)
                {
                    case 1:
                        if (Math.Abs(ptsList[0].X - ptsList[1].X) > (1 + Math.Abs(dx)))
                        {
                            ptsList[0].Offset(dx, 0);
                            if (Math.Abs(ptsList[0].X - ptsList[1].X) > (1 + Math.Abs(dx)))
                            {
                                ptsList[2].Offset(dx, 0);
                                PointList[0] = ptsList[0];
                                if (ptsList[0].X < ptsList[1].X)
                                    Lenght -= dx;
                                else
                                    Lenght += dx;
                            }
                            PointList[2] = ptsList[2];
                        }
                        break;
                    case 2:
                        if (Math.Abs(ptsList[0].X - ptsList[1].X) > (1 + Math.Abs(dx)))
                        {
                            ptsList[1].Offset(dx, 0);
                            if (Math.Abs(ptsList[0].X - ptsList[1].X) > (1 + Math.Abs(dx)))
                            {
                                PointList[1] = ptsList[1];
                                if (ptsList[0].X < ptsList[1].X)
                                    Lenght += dx;
                                else
                                    Lenght -= dx;
                            }
                        }
                        break;
                    case 3:
                        if (!Mirror && Math.Abs(ptsList[2].X - ptsList[0].X) > (1 + Math.Abs(dx)) && (dx * dy) < 0)
                        {
                            ptsList[2].Offset(dx, -dx);
                            if (Math.Abs(ptsList[2].X - ptsList[0].X) > (1 + Math.Abs(dx)))
                            {
                                PointList[2] = ptsList[2];
                                LenghtFork = Convert.ToInt32(Math.Sqrt(
                                    (ptsList[2].X - ptsList[0].X) * (ptsList[2].X - ptsList[0].X) * 2));
                            }
                        }
                        else if (Mirror && Math.Abs(ptsList[2].X - ptsList[0].X) > (1 + Math.Abs(dx)) && (dx * dy) > 0)
                        {
                            ptsList[2].Offset(dx, dx);
                            if (Math.Abs(ptsList[2].X - ptsList[0].X) > (1 + Math.Abs(dx)))
                            {
                                PointList[2] = ptsList[2];
                                LenghtFork = Convert.ToInt32(Math.Sqrt(
                                    (ptsList[2].X - ptsList[0].X) * (ptsList[2].X - ptsList[0].X) * 2));
                            }
                        }
                        break;
                }
            }
            else if (ptsList[0].X == ptsList[1].X)
            {
                switch (handle)
                {
                    case 1:
                        if (Math.Abs(ptsList[0].Y - ptsList[1].Y) > (1 + Math.Abs(dy)))
                        {
                            ptsList[0].Offset(0, dy);
                            if (Math.Abs(ptsList[0].Y - ptsList[1].Y) > (1 + Math.Abs(dy)))
                            {
                                ptsList[2].Offset(0, dy);
                                PointList[0] = ptsList[0];
                                if (ptsList[0].Y < ptsList[1].Y)
                                    Lenght -= dy;
                                else
                                    Lenght += dy;
                            }
                            PointList[2] = ptsList[2];
                        }
                        break;
                    case 2:
                        if (Math.Abs(ptsList[0].Y - ptsList[1].Y) > (1 + Math.Abs(dy)))
                        {
                            ptsList[1].Offset(0, dy);
                            if (Math.Abs(ptsList[0].Y - ptsList[1].Y) > (1 + Math.Abs(dy)))
                            {
                                PointList[1] = ptsList[1];
                                if (ptsList[0].Y < ptsList[1].Y)
                                    Lenght += dy;
                                else
                                    Lenght -= dy;
                            }
                        }
                        break;
                    case 3:
                        if (!Mirror && Math.Abs(ptsList[2].Y - ptsList[0].Y) > (1 + Math.Abs(dy)) && (dx * dy) > 0)
                        {
                            ptsList[2].Offset(dy, dy);
                            if (Math.Abs(ptsList[2].Y - ptsList[0].Y) > (1 + Math.Abs(dy)))
                            {
                                PointList[2] = ptsList[2];
                                LenghtFork = Convert.ToInt32(Math.Sqrt(
                                    (ptsList[2].X - ptsList[0].X) * (ptsList[2].X - ptsList[0].X) * 2));
                            }
                        }
                        else if (Mirror && Math.Abs(ptsList[2].Y - ptsList[0].Y) > (1 + Math.Abs(dy)) && (dx * dy) < 0)
                        {
                            ptsList[2].Offset(-dy, dy);
                            if (Math.Abs(ptsList[2].Y - ptsList[0].Y) > (1 + Math.Abs(dx)))
                            {
                                PointList[2] = ptsList[2];
                                LenghtFork = Convert.ToInt32(Math.Sqrt(
                                    (ptsList[2].X - ptsList[0].X) * (ptsList[2].X - ptsList[0].X) * 2));
                            }
                        }
                        break;
                }
            }
            dotStart = PointList[0];
            dotEnd = PointList[1];
            dotEndFork = PointList[2];
            return ;
        }

        public override void RotateCounterClw()
        {
            base.RotateCounterClw();
            RotateAngle = -90;
            Matrix matrix = new Matrix();
            PointF ptCenter = new PointF();
            int num = PointList.Count;
            PointF[] pts = new PointF[num];
            for (int i = 0; i < num; i++)
            {
                pts[i] = PointList[i];
            }
            StartAngle = (StartAngle + 360) % 360;
            switch (StartAngle)
            {
                case 0:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.first;
                    ptCenter.X = ((float)(pts[0].X + pts[1].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[2].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.four;
                    break;
                case 90:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.second;
                    ptCenter.X = ((float)(pts[0].X + pts[2].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[1].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.first;
                    break;
                case 180:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.third;
                    ptCenter.X = ((float)(pts[0].X + pts[1].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[2].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.second;
                    break;
                case 270:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.four;
                    ptCenter.X = ((float)(pts[0].X + pts[2].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[1].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.third;
                    break;
                default:
                    break;
            }
            StartAngle += RotateAngle;
            matrix.RotateAt(RotateAngle, ptCenter);
            matrix.TransformPoints(pts);
            PointList.Clear();
            for (int i = 0; i < num; i++)
            {
                PointList.Add(Point.Ceiling(pts[i]));
            }
            dotStart = PointList[0];
            dotEnd = PointList[1];
            dotEndFork = PointList[2];
        }

        public override void RotateClw()
        {
            base.RotateCounterClw();
            RotateAngle = 90;
            Matrix matrix = new Matrix();
            PointF ptCenter = new PointF();
            int num = PointList.Count;
            PointF[] pts = new PointF[num];
            for (int i = 0; i < num; i++)
            {
                pts[i] = PointList[i];
            }
            StartAngle = (StartAngle + 360) % 360;
            switch (StartAngle)
            {
                case 0:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.first;
                    ptCenter.X = ((float)(pts[0].X + pts[1].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[2].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.second;
                    break;
                case 90:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.second;
                    ptCenter.X = ((float)(pts[0].X + pts[2].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[1].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.third;
                    break;
                case 180:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.third;
                     ptCenter.X = ((float)(pts[0].X + pts[1].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[2].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.four;
                    break;
                case 270:
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.four;
                    ptCenter.X = ((float)(pts[0].X + pts[2].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[1].Y)) / 2;
                    DirectionOfCross = Mcs.RailSystem.Common.EleCross.DirectionCross.first;
                    break;
                default:
                    break;
            }
            StartAngle += RotateAngle;
            matrix.RotateAt(RotateAngle, ptCenter);
            matrix.TransformPoints(pts);
            PointList.Clear();
            for (int i = 0; i < num; i++)
            {
                PointList.Add(Point.Ceiling(pts[i]));
            }
            dotStart = PointList[0];
            dotEnd = PointList[1];
            dotEndFork = PointList[2];
        }

        public override void DrawEnlargeOrShrink(float draw_multi_factor)
        {
            DrawMultiFactor = Convert.ToInt16(draw_multi_factor);
            base.DrawEnlargeOrShrink(draw_multi_factor);
        }

        public override void ObjectMirror()
        {
            PointF ptCenter = PointF.Empty;
            int num = PointList.Count;
            PointF[] pts = new PointF[num];
            for (int i = 0; i < num; i++)
                pts[i] = PointList[i];
            if (PointList[0].Y == PointList[1].Y)
            {
                ptCenter = new PointF((float)(PointList[0].X + PointList[1].X) / 2, PointList[0].Y);
                for (int i = 0; i < num; i++)
                {
                    if (pts[i].X < ptCenter.X)
                        pts[i].X += 2 * Math.Abs(pts[i].X - ptCenter.X);
                    else
                        pts[i].X -= 2 * Math.Abs(pts[i].X - ptCenter.X);
                }
            }
            else if (PointList[0].X == PointList[1].X)
            {
                ptCenter = new PointF((float)(PointList[0].X + PointList[2].X) / 2, (float)(PointList[0].Y + PointList[1].Y) / 2);
                for (int i = 0; i < num;i++ )
                {
                    if (pts[i].X < ptCenter.X)
                        pts[i].X += 2 * Math.Abs(pts[i].X - ptCenter.X);
                    else
                        pts[i].X -= 2 * Math.Abs(pts[i].X - ptCenter.X);
                }
            }
            if (Mirror)
                Mirror = false;
            else if (!Mirror)
                Mirror = true;
            PointList.Clear();
            for (int i = 0; i < num; i++)
                PointList.Add(Point.Ceiling(pts[i]));
            dotStart = PointList[0];
            dotEnd = PointList[1];
            dotEndFork = PointList[2];
        }

        public override void ChangePropertyValue()
        {
            int num = PointList.Count;
            Point[] ptsList = new Point[num];
            PointList.CopyTo(ptsList);
            Point ptBase = new Point();
            ptBase.X = ptsList[0].X < ptsList[1].X ? ptsList[0].X : ptsList[1].X;
            ptBase.X = ptBase.X < ptsList[2].X ? ptBase.X : ptsList[2].X;
            ptBase.Y = ptsList[0].Y > ptsList[1].Y ? ptsList[0].Y : ptsList[1].Y;
            ptBase.Y = ptBase.Y > ptsList[2].Y ? ptBase.Y : ptsList[2].Y;
            ptsList[0] = ptBase;
            ptsList[1].X = ptBase.X + Lenght;
            ptsList[1].Y = ptBase.Y;
            ptsList[2].X = ptBase.X + LenghtFork;
            ptsList[2].Y = ptBase.Y;
            Matrix matrix = new Matrix();
            Point[] points = new Point[1];
            points[0] = ptsList[2];
            int rotateAngle = -45;
            matrix.RotateAt(rotateAngle, ptsList[0]);
            matrix.TransformPoints(points);
            matrix.Reset();
            ptsList[2] = points[0];
            PointF ptCenter = new PointF();
            int temp = (Lenght > LenghtFork ? Lenght : LenghtFork) / 2;
            ptCenter.X = ptsList[0].X + temp;
            ptCenter.Y = (ptsList[0].Y + ptsList[2].Y) / 2;
            matrix.RotateAt(StartAngle, ptCenter);
            matrix.TransformPoints(ptsList);
            PointList.Clear();
            for (int i = 0; i < num; i++)
            {
                PointList.Add(Point.Ceiling(ptsList[i]));
            }
            if (Mirror)
            {
                ObjectMirror();
                Mirror = true;
            }
            dotStart = PointList[0];
            dotEnd = PointList[1];
            dotEndFork = PointList[2];
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
            RailEleCross cl = new RailEleCross();
            int num = PointList.Count;
            Point[] pts = new Point[num];
            PointList.CopyTo(pts);
            for (int i = 0; i < num; i++)
            {
                pts[i].Offset(20, 20);
            }
            cl.PointList.AddRange(pts);
            cl.Lenght = Lenght;
            cl.LenghtFork = LenghtFork;
            cl.StartAngle = StartAngle;
            cl.RotateAngle = RotateAngle;
            cl.DirectionOfCross = DirectionOfCross;
            cl.DrawMultiFactor = DrawMultiFactor;
            cl.Mirror = Mirror;
            cl.railText = str;
            cl.PenCross = PenCross;
            cl.dotStart = dotStart;
            cl.dotEnd = dotEnd;
            cl.dotEndFork = dotEndFork;
            return cl;
        }

    }
}
