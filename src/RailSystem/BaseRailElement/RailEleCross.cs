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
        private ObjectCrossOp objCrossOp = new ObjectCrossOp();

        [Browsable(false)]
        public List<Point> PointList
        {
            get { return objCrossOp.PointList; }
        }

        public RailEleCross()
        {
            GraphType = 3;
            PenCross.Width = PenWidth;
            PenCross.Color = PenColor;
            PenCross.DashStyle = PenDashStyle;
            PenCross.EndCap = LineCap.ArrowAnchor;
        }

        public RailEleCross CreateEle(Point pt, Size size, Int16 multiFactor, string text)
        {
            Point[] pts = new Point[8];
            DrawMultiFactor = multiFactor;
            objCrossOp.DrawMultiFactor = DrawMultiFactor;
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
            objCrossOp.DrawTracker(canvas, DirectionOfCross);
        }

        public override int HitTest(Point point, bool isSelected)
        {
            return objCrossOp.HitTest(point, isSelected, DirectionOfCross, Mirror);
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
            objCrossOp.scale(handle, dx, dy, Mirror);
            FirstPart = objCrossOp.FirstPart;
            SecPart =objCrossOp.SecPart;
            ThPart =objCrossOp.ThPart;
            FourPart =objCrossOp.FourPart;
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
            objCrossOp.DrawMultiFactor = Convert.ToInt16(draw_multi_factor);
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
            return objCrossOp.ChosedInRegion(rect);
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
            cl.objCrossOp.DrawMultiFactor = DrawMultiFactor;
            cl.objCrossOp.FirstPart = FirstPart;
            cl.objCrossOp.SecPart = SecPart;
            cl.objCrossOp.ThPart = ThPart;
            cl.objCrossOp.FourPart = FourPart;
            return cl;
        }

    }
}
