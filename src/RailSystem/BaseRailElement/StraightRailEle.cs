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
using System.Windows.Forms;
using System.Data;

namespace BaseRailElement
{
    public class StraightRailEle : BaseRailEle
    {
        private ObjectStraightOp objectStaightOp = new ObjectStraightOp();
        private int lenght = 100;
        private int startAngle = 0;
        private int rotateAngle = 90;
        private Int32 nextCoding = -1;
        private Int32 prevCoding = -1;
        private string startDot = "first dot";
        private Int32 codingBegin = -1;
        private Int32 codingEnd = -1;
        public DataTable dt = new DataTable();
        private PenStyle linePen = new PenStyle();
        private Pen pen = new Pen(Color.Black, 3);

        [Category("其他")]
        public int Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }
        [Browsable(false)]
        public int StartAngle
        {
            get { return startAngle; }
            set { startAngle = value; }
        }
        [Browsable(false)]
        public List<Point> PointList
        {
            get { return objectStaightOp.PointList; }
        }
        [ReadOnly(true), Category("端点坐标")]
        public Point FirstDot
        {
            get { return PointList[0]; }
        }
        [ReadOnly(true), Category("端点坐标")]
        public Point SecDot
        {
            get { return PointList[1]; }
        }
        public class StartDotConverter : TypeConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(new string[] { "first dot", "sec dot" });
            }
        }
        [TypeConverter(typeof(StartDotConverter)), Category("轨道段信息"), Description("二维码起始端")]
        public string StartDot
        {
            get { return startDot; }
            set { startDot = value; }
        }
        [Browsable(false)]
        public Int32 RotateAngle
        {
            get { return rotateAngle; }
            set { rotateAngle = value; }
        }
        [XmlIgnore]
        [Browsable(false)]
        public Int32 NextCoding
        {
            get { return nextCoding; }
            set { nextCoding = value; }
        }
        [XmlIgnore]
        [Browsable(false)]
        public Int32 PrevCoding
        {
            get { return prevCoding; }
            set { prevCoding = value; }
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
        [Category("线属性")]
        public Color PenColor
        {
            get { return linePen.Color; }
            set { linePen.Color = value; pen.Color = value; }
        }
        [Category("线属性")]
        public float PenWidth
        {
            get { return linePen.Width; }
            set { linePen.Width = value; pen.Width = value; }
        }
        [Category("线属性")]
        public DashStyle PenDashStyle
        {
            get { return linePen.DashStyle; }
            set { linePen.DashStyle = value; pen.DashStyle = value; }
        }

        public StraightRailEle() 
        { 
            GraphType = 1;
            linePen.Width = 3.0f;
            pen.Color = linePen.Color;
            pen.Width = linePen.Width;
            pen.DashStyle = linePen.DashStyle;
            pen.EndCap = LineCap.ArrowAnchor;
        }

        public StraightRailEle CreateEle(Point pt, Size size, Int16 multiFactor, string text)
        {
            Point[] pts = new Point[2];
            DrawMultiFactor = multiFactor;
            objectStaightOp.DrawMultiFactor = multiFactor;
            pt.Offset(pt.X / DrawMultiFactor - pt.X, pt.Y / DrawMultiFactor - pt.Y);
            pts[0] = pt;
            if ((pt.X + Lenght) > size.Width)
            {
                pts[0] = new Point(pt.X - lenght, pt.Y);
                pts[1] = new Point(pt.X, pt.Y);
            }
            else
            {
                pts[0] = new Point(pt.X, pt.Y);
                pts[1] = new Point(pt.X + lenght, pt.Y);
            }
            PointList.AddRange(pts);
            this.railText = text;
            return this;
        }

        public override void Draw(Graphics canvas)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            if (PointList.Count < 2)
            {
                throw new Exception("绘制线条的点至少需要2个");
            }
            int n = PointList.Count;
            Point[] pts = new Point[n];
            PointList.CopyTo(pts);
            if (pts[0].Y == pts[1].Y)
            {
                if (lenght != Math.Abs(pts[0].X - pts[1].X))
                {
                    if (pts[0].X < pts[1].X)
                    {
                        pts[1].X = pts[0].X + lenght;
                    }
                    else
                    {
                        pts[1].X = pts[0].X - lenght;
                    }
                }
            }
            else if (pts[0].X == pts[1].X)
            {
                if (lenght != Math.Abs(pts[0].Y - pts[1].Y))
                {
                    pts[1].Y = pts[0].Y + lenght;
                }
            }
            for (int i = 0; i < n; i++)
                pts[i].Offset(pts[i].X * (DrawMultiFactor - 1), pts[i].Y * (DrawMultiFactor - 1));
            
            canvas.DrawLines(pen, pts);
        }

        public override void DrawTracker(Graphics canvas)
        {
            objectStaightOp.DrawTracker(canvas);
        }

        public override int HitTest(Point point, bool isSelected)
        {
            return objectStaightOp.HitTest(point, isSelected);
        }

        protected override void Translate(int offsetX, int offsetY)
        {
            objectStaightOp.Translate(offsetX, offsetY);
        }

        protected override void Scale(int handle, int dx, int dy)
        {
            lenght = objectStaightOp.Scale(handle, dx, dy, lenght);
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
            rotateAngle = -90;
            startAngle -= rotateAngle;
            objectStaightOp.Rotate(pt, rotateAngle);
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
            rotateAngle = 90;
            startAngle += rotateAngle;
            objectStaightOp.Rotate(pt, rotateAngle);
        }

        public override void DrawEnlargeOrShrink(float drawMultiFactor)
        {
            objectStaightOp.DrawMultiFactor = Convert.ToInt16(drawMultiFactor);
            base.DrawEnlargeOrShrink(drawMultiFactor);
        }

        public override void ChangePropertyValue()
        {
            Point[] pts = new Point[2];
            PointList.CopyTo(pts);
            if (PointList[0].X == PointList[1].X)
            {
                if (PointList[0].Y < PointList[1].Y)
                    pts[1].Y = pts[0].Y + lenght;
                else
                    pts[0].Y = pts[1].Y + lenght;
            }
            else if (PointList[0].Y == PointList[1].Y)
            {
                if (PointList[0].X < PointList[1].X)
                    pts[1].X = pts[0].X + lenght;
                else
                    pts[0].X = pts[1].X + lenght;
            }
            PointList.Clear();
            PointList.AddRange(pts);
            base.ChangePropertyValue();
        }

        public object Clone(string str)
        {
            StraightRailEle cl = new StraightRailEle();
            cl.pen = pen;
            cl.PointList.AddRange(PointList);
            cl.lenght = lenght;
            cl.DrawMultiFactor = DrawMultiFactor;
            cl.objectStaightOp.DrawMultiFactor = DrawMultiFactor;
            cl.railText = str;
            return cl;
        }

        public override bool ChosedInRegion(Rectangle rect)
        {
            return objectStaightOp.ChosedInRegion(rect);
        }

        public override DataRow DataSetXMLSave(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["GraphType"] = GraphType;
            dr["LocationLock"] = locationLock;
            dr["SizeLock"] = sizeLock;
            dr["Selectable"] = Selectable;
            dr["Speed"] = Speed;
            dr["SegmentNumber"] = SegmentNumber;
            dr["TagNumber"] = TagNumber;
            dr["CodingBegin"] = CodingBegin;
            dr["CodingEnd"] = CodingEnd;
            dr["Lenght"] = lenght;
            dr["StartAngle"] = startAngle;
            dr["StartDot"] = startDot;
            dr["PointListVol"] = PointList.Count;
            for (int i = 0; i < PointList.Count; i++)
            {
                dr["PointList" + i.ToString()] = PointList[i].ToString();
            }

            dr["DrawMultiFactor"] = DrawMultiFactor;
            dr["startPoint"] = StartPoint.ToString();
            dr["endPoint"] = EndPoint.ToString(); 
            dr["railText"] = railText;
            dr["rotateAngle"] = rotateAngle;
            dr["nextCoding"] = nextCoding;
            dr["prevCoding"] = prevCoding;

            dr["Color"] = ColorTranslator.ToHtml(pen.Color);
            dr["DashStyle"] = pen.DashStyle;
            dr["PenWidth"] = pen.Width;

            dt.Rows.Add(dr);
            return dr;
        }
    }
}
