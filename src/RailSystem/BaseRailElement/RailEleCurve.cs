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
    public class RailEleCurve : Mcs.RailSystem.Common.EleCurve
    {
        private ObjectCurvedOp objCurveOp = new ObjectCurvedOp();

        public RailEleCurve()
        {
            GraphType = 2;
            PenCurve.Width = PenWidth;
            PenCurve.Color = PenColor;
            PenCurve.DashStyle = PenDashStyle;
            PenCurve.EndCap = LineCap.ArrowAnchor;
        }

        public RailEleCurve CreateEle(Point centerDot, Size size, Int16 multiFactor, string text)
        {
            DrawMultiFactor = multiFactor;
            objCurveOp.DrawMultiFactor = multiFactor;
            Point pt = centerDot;
            pt.Offset(centerDot.X / DrawMultiFactor - centerDot.X, centerDot.Y / DrawMultiFactor - centerDot.Y);
            Center = pt;
            oldCenter = pt;
            DirectionCurvedAttribute = DirectonCurved.first;
            Point pt_first = new Point(Center.X + Radiu, Center.Y);
            Point pt_sec = new Point(Center.X, Center.Y + Radiu);
            FirstDot = pt_first;
            oldFirstDot = pt_first;
            SecDot = pt_sec;
            oldSecDot = pt_sec;
            this.railText = text;
            return this;
        }

        public override void Draw(Graphics _canvas)
        {
            if (_canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            if (Center.IsEmpty)
                throw new Exception("对象不存在");
            Rectangle rc = new Rectangle();
            rc.Location = new Point((Center.X - Radiu) * DrawMultiFactor, (Center.Y - Radiu) * DrawMultiFactor);
            rc.Width = Radiu * 2 * DrawMultiFactor;
            rc.Height = Radiu * 2 * DrawMultiFactor;
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(rc, StartAngle, SweepAngle);

            _canvas.DrawPath(PenCurve, gp);
            gp.Dispose();
        }

        public override void DrawTracker(Graphics canvas)
        {
            objCurveOp.DrawTracker(canvas, Center, Radiu, DirectionCurvedAttribute);
        }

        public override int HitTest(Point point, bool isSelected)
        {
            return objCurveOp.HitTest(point, isSelected, Center, Radiu, DirectionCurvedAttribute);
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
            Point pt = Center;
            pt.Offset(offsetX, offsetY);
            Center = pt;
            oldCenter = pt;
            pt = FirstDot;
            pt.Offset(offsetX, offsetY);
            FirstDot = pt;
            oldFirstDot = pt;
            pt = SecDot;
            pt.Offset(offsetX, offsetY);
            SecDot = pt;
            oldSecDot = pt;
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
            Point pt_first = FirstDot;
            Point pt_sec = SecDot;
            Rectangle rc = objCurveOp.Scale(handle, dx, dy, Center, Radiu, DirectionCurvedAttribute);
            Center = rc.Location;
            oldCenter = Center;
            Radiu = rc.Width;
            oldRadiu = Radiu;
            switch (DirectionCurvedAttribute)
            {
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.first:
                    pt_first.X = Center.X + Radiu;
                    pt_first.Y = Center.Y;
                    pt_sec.X = Center.X;
                    pt_sec.Y = Center.Y + Radiu;
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.second:
                    pt_first.X = Center.X;
                    pt_first.Y = Center.Y + Radiu;
                    pt_sec.X = Center.X - Radiu;
                    pt_sec.Y = Center.Y;
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.third:
                    pt_first.X = Center.X - Radiu;
                    pt_first.Y = Center.Y;
                    pt_sec.X = Center.X;
                    pt_sec.Y = Center.Y - Radiu;
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.four:
                    pt_first.X = Center.X;
                    pt_first.Y = Center.Y - Radiu;
                    pt_sec.X = Center.X + Radiu;
                    pt_sec.Y = Center.Y;
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.NULL:
                    break;
            }
            FirstDot = pt_first;
            oldFirstDot = FirstDot;
            SecDot = pt_sec;
            oldSecDot = SecDot;
        }

        public override void RotateCounterClw()
        {
            base.RotateCounterClw();
            RotateAngle = -90;
            Matrix matrix = new Matrix();
            PointF pt_center = new PointF();
            Point[] pts = new Point[4];
            pts[0] = Center;
            pts[1] = FirstDot;
            pts[2] = SecDot;
            StartAngle = (StartAngle + 360) % 360;
            switch (StartAngle)
            {
                case 0:
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.first;
                    pt_center.X = (float)(Center.X + FirstDot.X) / 2;
                    pt_center.Y = (float)(Center.Y + SecDot.Y) / 2;
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.four;
                    break;
                case 90:
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.second;
                    pt_center.X = (float)(Center.X + SecDot.X) / 2;
                    pt_center.Y = (float)(Center.Y + FirstDot.Y) / 2;
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.first;
                    break;
                case 180:
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.third;
                    pt_center.X = (float)(Center.X + FirstDot.X) / 2;
                    pt_center.Y = (float)(Center.Y + SecDot.Y) / 2;
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.second;
                    break;
                case 270:
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.four;
                    pt_center.X = (float)(Center.X + SecDot.X) / 2;
                    pt_center.Y = (float)(Center.Y + FirstDot.Y) / 2;
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.third;
                    break;
            }
            StartAngle += RotateAngle;
            matrix.RotateAt(RotateAngle, pt_center);
            matrix.TransformPoints(pts);
            Center = pts[0];
            oldCenter = pts[0];
            FirstDot = pts[1];
            oldFirstDot = pts[1];
            SecDot = pts[2];
            oldSecDot = pts[2];
        }

        public override void RotateClw()
        {
            base.RotateClw();
            RotateAngle = 90;
            Matrix matrix = new Matrix();
            PointF pt_center = PointF.Empty;
            Point[] pts = new Point[4];
            pts[0] = Center;
            pts[1] = FirstDot;
            pts[2] = SecDot;
            StartAngle = (StartAngle + 360) % 360;
            switch (StartAngle)
            {
                case 0:
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.first;
                    pt_center.X = (float)(Center.X + FirstDot.X) / 2;
                    pt_center.Y = (float)(Center.Y + SecDot.Y) / 2;
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.second;
                    break;
                case 90:
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.second;
                    pt_center.X = (float)(Center.X + SecDot.X) / 2;
                    pt_center.Y = (float)(Center.Y + FirstDot.Y) / 2;
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.third;
                    break;
                case 180:
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.third;
                    pt_center.X = (float)(Center.X + FirstDot.X) / 2;
                    pt_center.Y = (float)(Center.Y + SecDot.Y) / 2;
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.four;
                    break;
                case 270:
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.four;
                    pt_center.X = (float)(Center.X + SecDot.X) / 2;
                    pt_center.Y = (float)(Center.Y + FirstDot.Y) / 2;
                    DirectionCurvedAttribute = Mcs.RailSystem.Common.EleCurve.DirectonCurved.first;
                    break;
            }
            StartAngle += RotateAngle;
            matrix.RotateAt(RotateAngle, pt_center);
            matrix.TransformPoints(pts);
            Center = pts[0];
            oldCenter = pts[0];
            FirstDot = pts[1];
            oldFirstDot = pts[1];
            SecDot = pts[2];
            oldSecDot = pts[2];
        }

        public override void DrawEnlargeOrShrink(float draw_multi_factor)
        {
            objCurveOp.DrawMultiFactor = Convert.ToInt16(draw_multi_factor);
            base.DrawEnlargeOrShrink(draw_multi_factor);
        }

        public override void ChangePropertyValue()
        {
            Point[] pts = new Point[3];
            int dx = 0, dy = 0;
            if (oldRadiu != Radiu)
            {
                switch (DirectionCurvedAttribute)
                {
                    case Mcs.RailSystem.Common.EleCurve.DirectonCurved.first:
                        pts[0].X = Center.X + Radiu;
                        pts[0].Y = Center.Y;
                        pts[1].X = Center.X;
                        pts[1].Y = Center.Y + Radiu;
                        break;
                    case Mcs.RailSystem.Common.EleCurve.DirectonCurved.second:
                        pts[0].X = Center.X;
                        pts[0].Y = Center.Y + Radiu;
                        pts[1].X = Center.X - Radiu;
                        pts[1].Y = Center.Y;
                        break;
                    case Mcs.RailSystem.Common.EleCurve.DirectonCurved.third:
                        pts[0].X = Center.X - Radiu;
                        pts[0].Y = Center.Y;
                        pts[1].X = Center.X;
                        pts[1].Y = Center.Y - Radiu;
                        break;
                    case Mcs.RailSystem.Common.EleCurve.DirectonCurved.four:
                        pts[0].X = Center.X;
                        pts[0].Y = Center.Y - Radiu;
                        pts[1].X = Center.X + Radiu;
                        pts[1].Y = Center.Y;
                        break;
                    default:
                        break;
                }
                FirstDot = pts[0];
                oldFirstDot = pts[0];
                SecDot = pts[1];
                oldSecDot = pts[1];
                oldRadiu = Radiu;
            }
            else if (oldCenter.X != Center.X
                || oldCenter.Y != Center.Y)
            {
                dx = Center.X - oldCenter.X;
                dy = Center.Y - oldCenter.Y;
                FirstDot.Offset(dx, dy);
                oldFirstDot = FirstDot;
                SecDot.Offset(dx, dy);
                oldSecDot = SecDot;
                oldCenter = Center;
            }
            else if (oldFirstDot.X != FirstDot.X
                || oldFirstDot.Y != FirstDot.Y)
            {
                dx = FirstDot.X - oldFirstDot.X;
                dy = FirstDot.Y - oldFirstDot.Y;
                Center.Offset(dx, dy);
                oldCenter = Center;
                SecDot.Offset(dx, dy);
                oldSecDot = SecDot;
                oldFirstDot = FirstDot;
            }
            else if (oldSecDot.X != SecDot.X
                || oldSecDot.Y != SecDot.Y)
            {
                dx = SecDot.X - oldSecDot.X;
                dy = SecDot.Y - oldSecDot.Y;
                Center.Offset(dx, dy);
                oldCenter = Center;
                FirstDot.Offset(dx, dy);
                oldFirstDot = FirstDot;
                oldSecDot = SecDot;
            }
            base.ChangePropertyValue();
        }

        public override bool ChosedInRegion(Rectangle rect)
        {
            Point[] pts = new Point[3];
            pts[0].X = Center.X * DrawMultiFactor;
            pts[0].Y = Center.Y * DrawMultiFactor;
            pts[1].X = FirstDot.X * DrawMultiFactor;
            pts[1].Y = FirstDot.Y * DrawMultiFactor;
            pts[2].X = SecDot.X * DrawMultiFactor;
            pts[2].Y = SecDot.Y * DrawMultiFactor;
            if (rect.Contains(pts[0]) && rect.Contains(pts[1]) && rect.Contains(pts[2]))
                return true;
            else
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
            dr["StartAngle"] = StartAngle;
            dr["SweepAngle"] = SweepAngle;
            dr["Radiu"] = Radiu;
            dr["Center"] = Center.ToString();
            dr["FirstDot"] = FirstDot.ToString();
            dr["SecDot"] = SecDot.ToString();
            dr["DirectionCurvedAttribute"] = DirectionCurvedAttribute;

            dr["drawMultiFactor"] = DrawMultiFactor;
            dr["startPoint"] = StartPoint.ToString();
            dr["endPoint"] = EndPoint.ToString();
            dr["CodingBegin"] = CodingBegin;
            dr["CodingEnd"] = CodingEnd;
            dr["CodingNext"] = CodingNext;
            dr["CodingPrev"] = CodingPrev;
            dr["railText"] = railText;
            dr["rotateAngle"] = RotateAngle;
            dr["oldRadiu"] = oldRadiu;
            dr["oldCenter"] = oldCenter.ToString();
            dr["oldFirstDot"] = oldFirstDot.ToString();
            dr["oldSecDot"] = oldSecDot.ToString();

            dr["Color"] = ColorTranslator.ToHtml(PenCurve.Color);
            dr["DashStyle"] = PenCurve.DashStyle;
            dr["PenWidth"] = PenCurve.Width;

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
            RailEleCurve cl = new RailEleCurve();
            Point pt = new Point();
            cl.PenCurve = PenCurve;
            pt = Center;
            pt.Offset(20, 20);
            cl.Center = pt;
            cl.oldCenter = pt;
            pt = FirstDot;
            pt.Offset(20, 20);
            cl.FirstDot = pt;
            cl.oldFirstDot = pt;
            pt = SecDot;
            pt.Offset(20, 20);
            cl.SecDot = pt;
            cl.oldSecDot = pt;
            cl.Radiu = Radiu;
            cl.oldRadiu = Radiu;
            cl.StartAngle = StartAngle;
            cl.SweepAngle = SweepAngle;
            cl.DrawMultiFactor = DrawMultiFactor;
            cl.DirectionCurvedAttribute = DirectionCurvedAttribute;
            cl.objCurveOp.DrawMultiFactor = DrawMultiFactor;
            cl.railText = str;
            return cl;
        }

    }
}
