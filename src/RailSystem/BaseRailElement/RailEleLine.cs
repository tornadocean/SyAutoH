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
    public class RailEleLine:Mcs.RailSystem.Common.EleLine
    {
        private ObjectStraightOp objLineOp = new ObjectStraightOp();

        [Browsable(false)]
        public List<Point> PointList
        {
            get { return objLineOp.PointList; }
        }

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
            objLineOp.DrawMultiFactor = multiFactor;
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
            objLineOp.DrawTracker(canvas);
        }

        public override int HitTest(Point point, bool isSelected)
        {
            return objLineOp.HitTest(point, isSelected);
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
            objLineOp.Translate(offsetX, offsetY);
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
            Lenght = objLineOp.Scale(handle, dx, dy, Lenght);
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
            objLineOp.Rotate(pt, RotateAngle);
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
            objLineOp.Rotate(pt, RotateAngle);
        }

        public override void DrawEnlargeOrShrink(float drawMultiFactor)
        {
            objLineOp.DrawMultiFactor = Convert.ToInt16(drawMultiFactor);
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
            return objLineOp.ChosedInRegion(rect);
        }

        public override DataRow SaveEleInfo(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["GraphType"] = GraphType;
            dr["LocationLock"] = locationLock;
            dr["SizeLock"] = sizeLock;
            dr["Selectable"] = Selectable;
            dr["Speed"] = Speed;
            dr["CodingBegin"] = CodingBegin;
            dr["CodingEnd"] = CodingEnd;
            dr["CodingNext"] = CodingNext;
            dr["CodingPrev"] = CodingPrev;
            dr["Lenght"] = Lenght;
            dr["StartAngle"] = StartAngle;
            dr["rotateAngle"] = RotateAngle;
            dr["PointListVol"] = PointList.Count;
            for (int i = 0; i < PointList.Count; i++)
            {
                dr["PointList" + i.ToString()] = PointList[i].ToString();
            }

            dr["DrawMultiFactor"] = DrawMultiFactor;
            dr["startPoint"] = StartPoint.ToString();
            dr["endPoint"] = EndPoint.ToString();
            dr["railText"] = railText;
            
            dr["Color"] = ColorTranslator.ToHtml(PenLine.Color);
            dr["DashStyle"] = PenLine.DashStyle;
            dr["PenWidth"] = PenLine.Width;

            dt.Rows.Add(dr);
            return dr;
        }

        public override DataRow SaveCodingInfo(DataTable dt)
        {
            DataRow dr = dt.NewRow();

            dr["GraphType"] = GraphType;
            dr["CodingBegin"] = CodingBegin;
            dr["CodingEnd"] = CodingEnd;
            dr["CodingNext"] = CodingNext;
            dr["CodingPrev"] = CodingPrev;

            dt.Rows.Add(dr);
            return dr;
        }


        public object Clone(string str)
        {
            RailEleLine cl = new RailEleLine();
            cl.PenLine = PenLine;
            cl.PointList.AddRange(PointList);
            cl.Lenght = Lenght;
            cl.DrawMultiFactor = DrawMultiFactor;
            cl.objLineOp.DrawMultiFactor = DrawMultiFactor;
            cl.railText = str;
            return cl;
        }

    }
}
