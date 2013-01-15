using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace BaseRailElement
{
    public class RailEleCurve : Mcs.RailSystem.Common.EleCurve
    {
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

        public override void Draw(Graphics canvas)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            if (Center.IsEmpty)
                throw new Exception("对象不存在");
            Rectangle rc = new Rectangle();
            rc.Location = new Point((Center.X - Radiu) * DrawMultiFactor, (Center.Y - Radiu) * DrawMultiFactor);
            rc.Width = Radiu * 2 * DrawMultiFactor;
            rc.Height = Radiu * 2 * DrawMultiFactor;
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(rc, StartAngle, SweepAngle);

            canvas.DrawPath(PenCurve, gp);
            gp.Dispose();
        }

        public override void DrawTracker(Graphics canvas)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            Point center = new Point(Center.X * DrawMultiFactor, Center.Y * DrawMultiFactor);
            int radiu = Radiu * DrawMultiFactor;
            Point[] points = new Point[4];
            Pen pen = new Pen(Color.Blue, 1);
            switch (DirectionCurvedAttribute)
            {
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.first:
                    points[0] = center;
                    points[1] = new Point(center.X + radiu, center.Y);
                    points[2] = new Point(center.X + radiu, center.Y + radiu);
                    points[3] = new Point(center.X, center.Y + radiu);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.second:
                    points[0] = center;
                    points[1] = new Point(center.X, center.Y + radiu);
                    points[2] = new Point(center.X - radiu, center.Y + radiu);
                    points[3] = new Point(center.X - radiu, center.Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.third:
                    points[0] = center;
                    points[1] = new Point(center.X - radiu, center.Y);
                    points[2] = new Point(center.X - radiu, center.Y - radiu);
                    points[3] = new Point(center.X, center.Y - radiu);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.four:
                    points[0] = center;
                    points[1] = new Point(center.X, center.Y - radiu);
                    points[2] = new Point(center.X + radiu, center.Y - radiu);
                    points[3] = new Point(center.X + radiu, center.Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.NULL:
                    break;
            }
            for (int i = 0; i < 4; i++)
            {
                Rectangle rc = new Rectangle(points[i].X - 4, points[i].Y - 4, 8, 8);
                canvas.DrawRectangle(pen, rc);
            }
            pen.Dispose();
        }

        public override int HitTest(Point point, bool isSelected)
        {
            Point center = new Point(Center.X * DrawMultiFactor, Center.Y * DrawMultiFactor);
            int radiu = Radiu * DrawMultiFactor;
            if (isSelected)
            {
                int handleHit = HandleHitTest(point, center, radiu, DirectionCurvedAttribute);
                if (handleHit > 0)
                    return handleHit;
            }
            Point[] wrapper = new Point[1];
            wrapper[0] = point;
            Rectangle rc = new Rectangle();
            Point[] points = new Point[4];
            switch (DirectionCurvedAttribute)
            {
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.first:
                    points[0] = center;
                    points[2] = new Point(center.X + radiu, center.Y + radiu);
                    rc = new Rectangle(points[0].X, points[0].Y, points[2].X - points[0].X, points[2].Y - points[0].Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.second:
                    points[1] = new Point(center.X, center.Y + radiu);
                    points[3] = new Point(center.X - radiu, center.Y);
                    rc = new Rectangle(points[3].X, points[3].Y, points[1].X - points[3].X, points[1].Y - points[3].Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.third:
                    points[0] = center;
                    points[2] = new Point(center.X - radiu, center.Y - radiu);
                    rc = new Rectangle(points[2].X, points[2].Y, points[0].X - points[2].X, points[0].Y - points[2].Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.four:
                    points[1] = new Point(center.X, center.Y - radiu);
                    points[3] = new Point(center.X + radiu, center.Y);
                    rc = new Rectangle(points[1].X, points[1].Y, points[3].X - points[1].X, points[3].Y - points[1].Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.NULL:
                    break;
            }
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(rc);
            Region region = new Region(path);
            if (region.IsVisible(wrapper[0]))
                return 0;
            else
                return -1;
        }

        private int HandleHitTest(Point point,
            Point center,
            int radiu,
            Mcs.RailSystem.Common.EleCurve.DirectonCurved direction)
        {
            Point[] points = new Point[4];
            switch (direction)
            {
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.first:
                    points[0] = center;
                    points[1] = new Point(center.X + radiu, center.Y);
                    points[2] = new Point(center.X + radiu, center.Y + radiu);
                    points[3] = new Point(center.X, center.Y + radiu);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.second:
                    points[0] = center;
                    points[1] = new Point(center.X, center.Y + radiu);
                    points[2] = new Point(center.X - radiu, center.Y + radiu);
                    points[3] = new Point(center.X - radiu, center.Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.third:
                    points[0] = center;
                    points[1] = new Point(center.X - radiu, center.Y);
                    points[2] = new Point(center.X - radiu, center.Y - radiu);
                    points[3] = new Point(center.X, center.Y - radiu);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.four:
                    points[0] = center;
                    points[1] = new Point(center.X, center.Y - radiu);
                    points[2] = new Point(center.X + radiu, center.Y - radiu);
                    points[3] = new Point(center.X + radiu, center.Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.NULL:
                    break;
            }
            for (int i = 0; i < 4; i++)
            {
                Point pt = points[i];
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

        private void Scale(int handle, int dx, int dy)
        {
            Point pt_first = FirstDot;
            Point pt_sec = SecDot;
            Rectangle rc = ScaleOp(handle, dx, dy, Center, Radiu, DirectionCurvedAttribute);
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
            DrawMultiFactor = Convert.ToInt16(draw_multi_factor);
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
            cl.railText = str;
            return cl;
        }

        private Rectangle ScaleOp(int handle,
            int dx,
            int dy,
            Point center,
            int radiu,
            Mcs.RailSystem.Common.EleCurve.DirectonCurved direction)
        {
            Point[] points = new Point[4];
            switch (direction)
            {
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.first:
                    points[0] = center;
                    points[1] = new Point(center.X + radiu, center.Y);
                    points[2] = new Point(center.X + radiu, center.Y + radiu);
                    points[3] = new Point(center.X, center.Y + radiu);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.second:
                    points[0] = center;
                    points[1] = new Point(center.X, center.Y + radiu);
                    points[2] = new Point(center.X - radiu, center.Y + radiu);
                    points[3] = new Point(center.X - radiu, center.Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.third:
                    points[0] = center;
                    points[1] = new Point(center.X - radiu, center.Y);
                    points[2] = new Point(center.X - radiu, center.Y - radiu);
                    points[3] = new Point(center.X, center.Y - radiu);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.four:
                    points[0] = center;
                    points[1] = new Point(center.X, center.Y - radiu);
                    points[2] = new Point(center.X + radiu, center.Y - radiu);
                    points[3] = new Point(center.X + radiu, center.Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.NULL:
                    break;
            }
            Point pt = points[handle - 1];
            Point[] wrapper = new Point[] { pt };
            switch (direction)
            {
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.first:
                    if (1 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx < 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(pt.X, pt.Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (2 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dx > 0)
                            {
                                pt.Offset(dx, 0);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(dx, 0);
                    }
                    else if (3 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx > 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (4 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dy > 0)
                            {
                                pt.Offset(0, dy);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(0, dy);
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.second:
                    if (1 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx > 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(pt.X, pt.Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (2 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dy > 0)
                            {
                                pt.Offset(0, dy);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(0, dy);
                    }
                    else if (3 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx < 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (4 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dx < 0)
                            {
                                pt.Offset(dx, 0);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(dx, 0);
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.third:
                    if (1 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx > 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(pt.X, pt.Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (2 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dx < 0)
                            {
                                pt.Offset(dx, 0);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(dx, 0);
                    }
                    else if (3 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx < 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (4 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dy < 0)
                            {
                                pt.Offset(0, dy);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(0, dy);
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.four:
                    if (1 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx < 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(pt.X, pt.Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (2 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dy < 0)
                            {
                                pt.Offset(0, dy);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(0, dy);
                    }
                    else if (3 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx > 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (4 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dy > 0)
                            {
                                pt.Offset(0, dy);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(0, dy);
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.NULL:
                    break;
            }
            wrapper[0] = pt;

            int dw, dh;
            dw = wrapper[0].X - points[handle - 1].X;
            dh = wrapper[0].Y - points[handle - 1].Y;

            switch (direction)
            {
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.first:
                    switch (handle)
                    {
                        case 1:
                            radiu -= dw;
                            center = wrapper[0];
                            break;
                        case 2:
                            radiu += dw;
                            break;
                        case 3:
                            radiu += dw;
                            break;
                        case 4:
                            radiu += dh;
                            break;
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.second:
                    switch (handle)
                    {
                        case 1:
                            radiu += dw;
                            center = wrapper[0];
                            break;
                        case 2:
                            radiu += dh;
                            break;
                        case 3:
                            radiu -= dw;
                            break;
                        case 4:
                            radiu -= dw;
                            break;
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.third:
                    switch (handle)
                    {
                        case 1:
                            radiu += dw;
                            center = wrapper[0];
                            break;
                        case 2:
                            radiu -= dw;
                            break;
                        case 3:
                            radiu -= dw;
                            break;
                        case 4:
                            radiu -= dh;
                            break;
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.four:
                    switch (handle)
                    {
                        case 1:
                            radiu -= dw;
                            center = wrapper[0];
                            break;
                        case 2:
                            radiu -= dh;
                            break;
                        case 3:
                            radiu += dw;
                            break;
                        case 4:
                            radiu += dh;
                            break;
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.NULL:
                    break;
            }
            return new Rectangle(center.X, center.Y, radiu, radiu);
        }

    }
}
