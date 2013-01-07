using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;

namespace BaseRailElement
{
    public class CurvedRailEle : BaseRailEle
    {
        private ObjectCurvedOp objectCurvedOp = new ObjectCurvedOp();
        
        private int startAngle = 0;
        private int sweepAngle = 90;
        private int rotateAngle = 90;
        public int oldRadiu = 50;
        private int radiu = 50;
        public Point oldCenter = new Point();
        private Point center = new Point();
        public Point oldFirstDot = Point.Empty;
        private Point firstDot = Point.Empty;
        public Point oldSecDot = Point.Empty;
        private Point secDot = Point.Empty;
        private Int32 codingBegin = -1;
        private Int32 codingEnd = -1;
        private Int32 codingNext = -1;
        private Int32 codingPrev = -1;
        private DirectonCurved directionCurved = DirectonCurved.NULL;
        public DataTable dt = new DataTable();
        private PenStyle curvePen = new PenStyle();
        private Pen pen = new Pen(Color.Black, 1);

        public enum DirectonCurved
        {
            first, second, third, four, NULL
        }

        [Browsable(false)]
        public int StartAngle
        {
            get { return startAngle; }
            set { startAngle = value; }
        }
        [Browsable(false)]
        public int SweepAngle
        {
            get { return sweepAngle; }
            set { sweepAngle = value; }
        }
        [Browsable(false)]
        public int RotateAngle
        {
            get { return rotateAngle; }
            set { rotateAngle = value; }
        }
        [Category("轨道坐标")]
        public int Radiu
        {
            get { return radiu; }
            set { oldRadiu = radiu; radiu = value; }
        }
        [Category("轨道坐标")]
        public Point Center
        {
            get { return center; }
            set { oldCenter = center; center = value; }
        }
        [Category("轨道坐标")]
        public Point FirstDot
        {
            get { return firstDot; }
            set { oldFirstDot = firstDot; firstDot = value; }
        }
        [Category("轨道坐标")]
        public Point SecDot
        {
            get { return secDot; }
            set { oldSecDot = secDot; secDot = value; }
        }
        [Description("条形码起始"), Category("轨道段信息")]
        public Int32 CodingBegin
        {
            get { return codingBegin; }
            set { codingBegin = value; }
        }
        [Description("条形码终止"), Category("轨道段信息")]
        public Int32 CodingEnd
        {
            get { return codingEnd; }
            set { codingEnd = value; }
        }
        [Description("条形码起始"), Category("轨道段信息")]
        public Int32 CodingNext
        {
            get { return codingNext; }
            set { codingNext = value; }
        }
        [Description("条形码起始"), Category("轨道段信息")]
        public Int32 CodingPrev
        {
            get { return codingPrev; }
            set { codingPrev = value; }
        }
        [Browsable(false)]
        public DirectonCurved DirectionCurvedAttribute
        {
            get { return directionCurved; }
            set { directionCurved = value; }
        }
        [Category("线属性")]
        public Color PenColor
        {
            get { return curvePen.Color; }
            set { curvePen.Color = value; pen.Color = value; }
        }
        [Category("线属性")]
        public float PenWidth
        {
            get { return curvePen.Width; }
            set { curvePen.Width = value; pen.Width = value; }
        }
        [Category("线属性")]
        public DashStyle PenDashStyle
        {
            get { return curvePen.DashStyle; }
            set { curvePen.DashStyle = value; pen.DashStyle = value; }
        }

        public CurvedRailEle() 
        {
            GraphType = 2;
            curvePen.Width = 3.0f;
            pen.Color = curvePen.Color;
            pen.Width = curvePen.Width;
            pen.DashStyle = curvePen.DashStyle;
            pen.EndCap = LineCap.ArrowAnchor;
        }

        public CurvedRailEle CreateEle(Point centerDot, Size size, Int16 multiFactor, string text)
        {
            DrawMultiFactor = multiFactor;
            objectCurvedOp.DrawMultiFactor = DrawMultiFactor;
            Point pt = centerDot;
            pt.Offset(centerDot.X / DrawMultiFactor - centerDot.X, centerDot.Y / DrawMultiFactor - centerDot.Y);
            center = pt;
            oldCenter = pt;
            directionCurved = DirectonCurved.first;
            Point pt_first = new Point(center.X + radiu, center.Y);
            Point pt_sec = new Point(center.X, center.Y + radiu);
            firstDot = pt_first;
            oldFirstDot = pt_first;
            secDot = pt_sec;
            oldSecDot = pt_sec;
            this.railText = text;
            return this;
        }

        public override void Draw(Graphics canvas)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            if (center.IsEmpty)
            {
                throw new Exception("对象不存在");
            }
            Rectangle rc = new Rectangle();
            rc.Location = new Point((center.X - radiu) * DrawMultiFactor, (center.Y - radiu) * DrawMultiFactor);
            rc.Width = radiu * 2 * DrawMultiFactor;
            rc.Height = radiu * 2 * DrawMultiFactor;
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(rc, startAngle, sweepAngle);
            
            canvas.DrawPath(pen, gp);
            gp.Dispose();
        }

        public override void DrawTracker(Graphics canvas)
        {
        //    objectCurvedOp.DrawTracker(canvas, center, radiu, directionCurved);
        }

        public override int HitTest(Point point, bool isSelected)
        {
        //    return objectCurvedOp.HitTest(point, isSelected, center, radiu, directionCurved);
            return 1;
        }

        protected override void Translate(int offsetX, int offsetY)
        {
            Point pt = center;
            pt.Offset(offsetX, offsetY);
            center = pt;
            oldCenter = pt;
            pt = firstDot;
            pt.Offset(offsetX, offsetY);
            firstDot = pt;
            oldFirstDot = pt;
            pt = secDot;
            pt.Offset(offsetX, offsetY);
            SecDot = pt;
            oldSecDot = pt;
        }

        protected override void Scale(int handle, int dx, int dy)
        {
            //Point pt_first = firstDot;
            //Point pt_sec = secDot;
            //Rectangle rc = objectCurvedOp.Scale(handle, dx, dy, center, radiu, directionCurved);
            //center = rc.Location;
            //oldCenter = center;
            //radiu = rc.Width;
            //oldRadiu = radiu;
            //switch (directionCurved)
            //{
            //    case DirectonCurved.first:
            //        pt_first.X = center.X + radiu;
            //        pt_first.Y = center.Y;
            //        pt_sec.X = center.X;
            //        pt_sec.Y = center.Y + radiu;
            //        break;
            //    case DirectonCurved.second:
            //        pt_first.X = center.X;
            //        pt_first.Y = center.Y + radiu;
            //        pt_sec.X = center.X - radiu;
            //        pt_sec.Y = center.Y;
            //        break;
            //    case DirectonCurved.third:
            //        pt_first.X = center.X - radiu;
            //        pt_first.Y = center.Y;
            //        pt_sec.X = center.X;
            //        pt_sec.Y = center.Y - radiu;
            //        break;
            //    case DirectonCurved.four:
            //        pt_first.X = center.X;
            //        pt_first.Y = center.Y - radiu;
            //        pt_sec.X = center.X + radiu;
            //        pt_sec.Y = center.Y;
            //        break;
            //    case DirectonCurved.NULL:
            //        break;
            //}
            //firstDot = pt_first;
            //oldFirstDot = firstDot;
            //secDot = pt_sec;
            //oldSecDot = secDot;
        }

        public override void RotateCounterClw()
        {
            base.RotateCounterClw();
            RotateAngle = -90;
            Matrix matrix = new Matrix();
            PointF pt_center = new PointF();
            Point[] pts = new Point[4];
            pts[0] = center;
            pts[1] = firstDot;
            pts[2] = secDot;
            startAngle = (startAngle + 360) % 360;
            switch (startAngle)
            {
                case 0:
                    directionCurved = DirectonCurved.first;
                    pt_center.X = (float)(center.X + firstDot.X) / 2;
                    pt_center.Y = (float)(center.Y + secDot.Y) / 2;
                    directionCurved = DirectonCurved.four;
                    break;
                case 90:
                    directionCurved = DirectonCurved.second;
                    pt_center.X = (float)(center.X + secDot.X) / 2;
                    pt_center.Y = (float)(center.Y + firstDot.Y) / 2;
                    directionCurved = DirectonCurved.first;
                    break;
                case 180:
                    directionCurved = DirectonCurved.third;
                    pt_center.X = (float)(center.X + firstDot.X) / 2;
                    pt_center.Y = (float)(center.Y + secDot.Y) / 2;
                    directionCurved = DirectonCurved.second;
                    break;
                case 270:
                    directionCurved = DirectonCurved.four;
                    pt_center.X = (float)(center.X + secDot.X) / 2;
                    pt_center.Y = (float)(center.Y + firstDot.Y) / 2;
                    directionCurved = DirectonCurved.third;
                    break;
            }
            startAngle += rotateAngle;
            matrix.RotateAt(rotateAngle, pt_center);
            matrix.TransformPoints(pts);
            center = pts[0];
            oldCenter = pts[0];
            firstDot = pts[1];
            oldFirstDot = pts[1];
            secDot = pts[2];
            oldSecDot = pts[2];
        }

        public override void RotateClw()
        {
            base.RotateClw();
            RotateAngle = 90;
            Matrix matrix = new Matrix();
            PointF pt_center = PointF.Empty;
            Point[] pts = new Point[4];
            pts[0] = center;
            pts[1] = firstDot;
            pts[2] = secDot;
            startAngle = (startAngle + 360) % 360;
            switch (startAngle)
            {
                case 0:
                    directionCurved = DirectonCurved.first;
                    pt_center.X = (float)(center.X + firstDot.X) / 2;
                    pt_center.Y = (float)(center.Y + secDot.Y) / 2;
                    directionCurved = DirectonCurved.second;
                    break;
                case 90:
                    directionCurved = DirectonCurved.second;
                    pt_center.X = (float)(center.X + secDot.X) / 2;
                    pt_center.Y = (float)(center.Y + firstDot.Y) / 2;
                    directionCurved = DirectonCurved.third;
                    break;
                case 180:
                    directionCurved = DirectonCurved.third;
                    pt_center.X = (float)(center.X + firstDot.X) / 2;
                    pt_center.Y = (float)(center.Y + secDot.Y) / 2;
                    directionCurved = DirectonCurved.four;
                    break;
                case 270:
                    directionCurved = DirectonCurved.four;
                    pt_center.X = (float)(center.X + secDot.X) / 2;
                    pt_center.Y = (float)(center.Y + firstDot.Y) / 2;
                    directionCurved = DirectonCurved.first;
                    break;
            }
            startAngle += rotateAngle;
            matrix.RotateAt(rotateAngle, pt_center);
            matrix.TransformPoints(pts);
            center = pts[0];
            oldCenter = pts[0];
            firstDot = pts[1];
            oldFirstDot = pts[1];
            secDot = pts[2];
            oldSecDot = pts[2];
        }

        public object Clone(string str)
        {
            CurvedRailEle cl = new CurvedRailEle();
            Point pt = new Point();
            cl.pen = pen;
            pt = center;
            pt.Offset(20, 20);
            cl.center = pt;
            cl.oldCenter = pt;
            pt = firstDot;
            pt.Offset(20, 20);
            cl.firstDot = pt;
            cl.oldFirstDot = pt;
            pt = secDot;
            pt.Offset(20, 20);
            cl.secDot = pt;
            cl.oldSecDot = pt;
            cl.radiu = radiu;
            cl.oldRadiu = radiu;
            cl.startAngle = startAngle;
            cl.sweepAngle = sweepAngle;
            cl.DrawMultiFactor = DrawMultiFactor;
            cl.directionCurved = directionCurved;
            cl.objectCurvedOp.DrawMultiFactor = DrawMultiFactor;
            cl.railText = str;
            return cl;
        }

        public override void DrawEnlargeOrShrink(float multiFactor)
        {
            objectCurvedOp.DrawMultiFactor = Convert.ToInt16(multiFactor);
            base.DrawEnlargeOrShrink(DrawMultiFactor);
        }

        public override void ChangePropertyValue()
        {
            Point[] pts = new Point[3];
            int dx = 0, dy = 0;
            if (oldRadiu != radiu)
            {
                switch (directionCurved)
                {
                    case DirectonCurved.first:
                        pts[0].X = center.X + radiu;
                        pts[0].Y = center.Y;
                        pts[1].X = center.X;
                        pts[1].Y = center.Y + radiu;
                        break;
                    case DirectonCurved.second:
                        pts[0].X = center.X;
                        pts[0].Y = center.Y + radiu;
                        pts[1].X = center.X - radiu;
                        pts[1].Y = center.Y;
                        break;
                    case DirectonCurved.third:
                        pts[0].X = center.X - radiu;
                        pts[0].Y = center.Y;
                        pts[1].X = center.X;
                        pts[1].Y = center.Y - radiu;
                        break;
                    case DirectonCurved.four:
                        pts[0].X = center.X;
                        pts[0].Y = center.Y - radiu;
                        pts[1].X = center.X + radiu;
                        pts[1].Y = center.Y;
                        break;
                    case DirectonCurved.NULL:
                        break;
                }
                firstDot = pts[0];
                oldFirstDot = pts[0];
                secDot = pts[1];
                oldSecDot = pts[1];
                oldRadiu = radiu;
            }
            else if (oldCenter.X != center.X
                || oldCenter.Y != center.Y)
            {
                dx = center.X - oldCenter.X;
                dy = center.Y - oldCenter.Y;
                firstDot.Offset(dx, dy);
                oldFirstDot = firstDot;
                secDot.Offset(dx, dy);
                oldSecDot = secDot;
                oldCenter = center;
            }
            else if (oldFirstDot.X != firstDot.X
                || oldFirstDot.Y != firstDot.Y)
            {
                dx = firstDot.X - oldFirstDot.X;
                dy = firstDot.Y - oldFirstDot.Y;
                center.Offset(dx, dy);
                oldCenter = center;
                secDot.Offset(dx, dy);
                oldSecDot = secDot;
                oldFirstDot = firstDot;
            }
            else if (oldSecDot.X != secDot.X
                || oldSecDot.Y != secDot.Y)
            {
                dx = secDot.X - oldSecDot.X;
                dy = secDot.Y - oldSecDot.Y;
                center.Offset(dx, dy);
                oldCenter = center;
                firstDot.Offset(dx, dy);
                oldFirstDot = firstDot;
                oldSecDot = secDot;
            }
            base.ChangePropertyValue();
        }

        public override bool ChosedInRegion(Rectangle rect)
        {
            Point[] pts = new Point[3];
            pts[0].X = center.X * DrawMultiFactor;
            pts[0].Y = center.Y * DrawMultiFactor;
            pts[1].X = firstDot.X * DrawMultiFactor;
            pts[1].Y = firstDot.Y * DrawMultiFactor;
            pts[2].X = secDot.X * DrawMultiFactor;
            pts[2].Y = secDot.Y * DrawMultiFactor;
            if (rect.Contains(pts[0]) && rect.Contains(pts[1]) && rect.Contains(pts[2]))
                return true;
            else
                return false;
        }

        public override DataRow DataSetXMLSave(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["GraphType"] = GraphType;
            dr["LocationLock"] = locationLock;
            dr["SizeLock"] = sizeLock;
            dr["Selectable"] = Selectable;
            dr["Speed"] = Speed;
            //dr["SegmentNumber"] = SegmentNumber;
            //dr["TagNumber"] = TagNumber;
            dr["StartAngle"] = startAngle;
            dr["SweepAngle"] = sweepAngle;
            dr["Radiu"] = radiu;
            dr["Center"] = center.ToString();
            dr["FirstDot"] = firstDot.ToString();
            dr["SecDot"] = secDot.ToString();
            dr["DirectionCurvedAttribute"] = directionCurved;

            dr["drawMultiFactor"] = DrawMultiFactor;
            dr["startPoint"] = StartPoint.ToString(); 
            dr["endPoint"] = EndPoint.ToString();
            dr["CodingBegin"] = CodingBegin;
            dr["CodingEnd"] = CodingEnd;
            dr["CodingNext"] = codingNext;
            dr["CodingPrev"] = codingPrev;
            dr["railText"] = railText;
            dr["rotateAngle"] = rotateAngle;
            dr["oldRadiu"] = oldRadiu;
            dr["oldCenter"] = oldCenter.ToString();
            dr["oldFirstDot"] = oldFirstDot.ToString();
            dr["oldSecDot"] = oldSecDot.ToString();

            dr["Color"] = ColorTranslator.ToHtml(pen.Color);
            dr["DashStyle"] = pen.DashStyle;
            dr["PenWidth"] = pen.Width;

            dt.Rows.Add(dr);
            return dr;
        }

        public DataRow SaveCodingDataTable(DataTable dt)
        {
            DataRow dr = dt.NewRow();

            dr["GraphType"] = GraphType;
            dr["CodingBegin"] = CodingBegin;
            dr["CodingEnd"] = CodingEnd;
            dr["CodingNext"] = codingNext;
            dr["CodingPrev"] = codingPrev;

            dt.Rows.Add(dr);
            return dr;
        }
    }
}
