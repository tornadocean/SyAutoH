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
using System.Diagnostics;
using System.Data;

namespace BaseRailElement
{
    public class CrossEle : BaseRailEle
    {
        ObjectCrossOp objectCrossOp = new ObjectCrossOp();
        
        private bool mirror = false;
        private int lenghtOfStrai = 100;
        private int firstPart = 30;
        private int secPart = 40;
        private int thPart = 30;
        private Point fourPart = new Point(40, 40);
        private int startAngle = 0;
        private int rotateAngle = 90;
        private Int32 codingBegin = -1;
        private Int32 codingEnd = -1;
        private Int32 codingEndS = -1;
        private Int32 codingPrev = -1;
        private Int32 codingNext = -1;
        private Int32 codingNextS = -1;
        //private string startDot = "first dot";
        private DirectionCross directionOfCross = DirectionCross.NULL;
        public DataTable dt = new DataTable();
        private PenStyle crossPen = new PenStyle();
        private Pen pen = new Pen(Color.Black, 1);

        public enum DirectionCross
        {
            first, second, third, four, NULL
        }

        [Browsable(false)]
        public bool Mirror
        {
            get { return mirror; }
            set { mirror = value; }
        }
        [Browsable(false)]
        public int LenghtOfStrai
        {
            get { return lenghtOfStrai; }
            set { lenghtOfStrai = value; }
        }
        [Category("轨道长度")]
        public int FirstPart
        {
            get { return objectCrossOp.FirstPart; }
            set { firstPart = value; objectCrossOp.FirstPart = value; }
        }
        [Category("轨道长度")]
        public int SecPart
        {
            get { return objectCrossOp.SecPart; }
            set { secPart = value; objectCrossOp.SecPart = value; }
        }
        [Category("轨道长度")]
        public int ThPart
        {
            get { return objectCrossOp.ThPart; }
            set { thPart = value; objectCrossOp.ThPart = value; }
        }
        [Category("轨道长度")]
        public Point FourPart
        {
            get { return objectCrossOp.FourPart; }
            set { fourPart = value; objectCrossOp.FourPart = value; }
        }
        [Browsable(false)]
        public List<Point> PointList
        {
            get { return objectCrossOp.PointList; }
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
        [Description("条形码终止"), Category("轨道段信息")]
        public Int32 CodingEndS
        {
            get { return codingEndS; }
            set { codingEndS = value; }
        }
        [Description("条形码终止"), Category("轨道段信息")]
        public Int32 CodingPrev
        {
            get { return codingPrev; }
            set { codingPrev = value; }
        }
        [Description("条形码终止"), Category("轨道段信息")]
        public Int32 CodingNext
        {
            get { return codingNext; }
            set { codingNext = value; }
        }
        [Description("条形码终止"), Category("轨道段信息")]
        public Int32 CodingNextS
        {
            get { return codingNextS; }
            set { codingNextS = value; }
        }
        [Browsable(false)]
        public int StartAngle
        {
            get { return startAngle; }
            set { startAngle = value; }
        }
        [Browsable(false)]
        public int RotateAngle
        {
            get { return rotateAngle; }
            set { rotateAngle = value; }
        }
        //public class StartDotConverter : TypeConverter
        //{
        //    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        //    {
        //        return true;
        //    }

        //    public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        //    {
        //        return new StandardValuesCollection(new string[] { "first dot", "sec dot" });
        //    }
        //}
        //[XmlIgnore]
        //[TypeConverter(typeof(StartDotConverter)), Category("轨道段信息"), Description("二维码起始端")]
        //public string StartDot
        //{
        //    get { return startDot; }
        //    set { startDot = value; }
        //}
        [XmlIgnore]
        [ReadOnly(true), Category("端点坐标")]
        public Point FirstDot
        {
            get { return PointList[0]; }
        }
        [XmlIgnore]
        [ReadOnly(true), Category("端点坐标")]
        public Point SecDot
        {
            get { return PointList[5]; }
        }
        [XmlIgnore]
        [ReadOnly(true), Category("端点坐标")]
        public Point ThirdDot
        {
            get { return PointList[7]; }
        }
        [Browsable(false)]
        public DirectionCross DirectionOfCross
        {
            get { return directionOfCross; }
            set { directionOfCross = value; }
        }
        [Category("线属性")]
        public Color PenColor
        {
            get { return crossPen.Color; }
            set { crossPen.Color = value; pen.Color = value; }
        }
        [Category("线属性")]
        public float PenWidth
        {
            get { return crossPen.Width; }
            set { crossPen.Width = value; pen.Width = value; }
        }
        [Category("线属性")]
        public DashStyle PenDashStyle
        {
            get { return crossPen.DashStyle; }
            set { crossPen.DashStyle = value; pen.DashStyle = value; }
        }

        public CrossEle()
        {
            GraphType = 3;
            crossPen.Width = 3.0f;
            pen.Color = crossPen.Color;
            pen.Width = crossPen.Width;
            pen.DashStyle = crossPen.DashStyle;
        }

        public CrossEle CreateEle(Point pt, Size size, Int16 multiFactor, string text)
        {
            Point[] pts = new Point[8];
            DrawMultiFactor = multiFactor;
            objectCrossOp.DrawMultiFactor = DrawMultiFactor;
            pt.Offset(pt.X / DrawMultiFactor - pt.X, pt.Y / DrawMultiFactor - pt.Y);
            pts[0] = pt;
            pts[1].X = pts[0].X + firstPart;
            pts[1].Y = pts[0].Y;
            pts[2].X = pts[1].X;
            pts[2].Y = pts[0].Y + 5;
            pts[3].X = pts[0].X + firstPart + secPart;
            pts[3].Y = pts[2].Y;
            pts[4].X = pts[3].X;
            pts[4].Y = pts[0].Y;
            pts[5].X = pts[0].X + lenghtOfStrai;
            pts[5].Y = pts[0].Y;
            pts[6].X = pts[1].X;
            pts[6].Y = pts[0].Y - 5;
            pts[7].X = pts[3].X;
            pts[7].Y = pts[0].Y - 45;
            PointList.AddRange(pts);
            directionOfCross = DirectionCross.first;
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
                canvas.DrawLines(pen, points);
            }
        }

        public override void DrawTracker(Graphics canvas)
        {
            objectCrossOp.DrawTracker(canvas, directionOfCross);
        }

        public override int HitTest(Point point, bool isSelected)
        {
            return objectCrossOp.HitTest(point, isSelected, directionOfCross, Mirror);
        }

        protected override void Translate(int offsetX, int offsetY)
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

        protected override void Scale(int handle, int dx, int dy)
        {
            objectCrossOp.scale(handle, dx, dy, Mirror);
            firstPart = FirstPart;
            secPart = SecPart;
            thPart = ThPart;
            fourPart.X = FourPart.X;
            fourPart.Y = FourPart.Y;
            Debug.WriteLine(string.Format("first is {0},sec is {1},th is {2}", firstPart, secPart, thPart));
        }

        public override void RotateCounterClw()
        {
            base.RotateCounterClw();
            rotateAngle = -90;
            Matrix matrix = new Matrix();
            PointF ptCenter = new PointF();
            PointF[] points = new PointF[8];
            PointF[] pts = new PointF[4];
            pts[0] = PointList[0];
            pts[1] = PointList[3];
            pts[2] = PointList[5];
            pts[3] = PointList[7];
            startAngle = (startAngle + 360) % 360;
            switch (startAngle)
            {
                case 0:
                    directionOfCross = DirectionCross.first;
                    ptCenter.X = ((float)(pts[0].X + pts[2].X)) / 2;
                    ptCenter.Y = ((float)(pts[1].Y + pts[3].Y)) / 2;
                    directionOfCross = DirectionCross.four;
                    break;
                case 90:
                    directionOfCross = DirectionCross.second;
                    ptCenter.X = ((float)(pts[1].X + pts[3].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[2].Y)) / 2;
                    directionOfCross = DirectionCross.first;
                    break;
                case 180:
                    directionOfCross = DirectionCross.third;
                    ptCenter.X = ((float)(pts[0].X + pts[2].X)) / 2;
                    ptCenter.Y = ((float)(pts[1].Y + pts[3].Y)) / 2;
                    directionOfCross = DirectionCross.second;
                    break;
                case 270:
                    directionOfCross = DirectionCross.four;
                    ptCenter.X = ((float)(pts[1].X + pts[3].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[2].Y)) / 2;
                    directionOfCross = DirectionCross.third;
                    break;
                default:
                    break;
            }
            startAngle += rotateAngle;
            matrix.RotateAt(rotateAngle, ptCenter);
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
            rotateAngle = 90;
            Matrix matrix = new Matrix();
            PointF ptCenter = new PointF();
            PointF[] points = new PointF[8];
            PointF[] pts = new PointF[4];
            pts[0] = PointList[0];
            pts[1] = PointList[3];
            pts[2] = PointList[5];
            pts[3] = PointList[7];
            startAngle = (startAngle + 360) % 360;
            switch (startAngle)
            {
                case 0:
                    directionOfCross = DirectionCross.first;
                    ptCenter.X = ((float)(pts[0].X + pts[2].X)) / 2;
                    ptCenter.Y = ((float)(pts[1].Y + pts[3].Y)) / 2;
                    directionOfCross = DirectionCross.second;
                    break;
                case 90:
                    directionOfCross = DirectionCross.second;
                    ptCenter.X = ((float)(pts[1].X + pts[3].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[2].Y)) / 2;
                    directionOfCross = DirectionCross.third;
                    break;
                case 180:
                    directionOfCross = DirectionCross.third;
                    ptCenter.X = ((float)(pts[0].X + pts[2].X)) / 2;
                    ptCenter.Y = ((float)(pts[1].Y + pts[3].Y)) / 2;
                    directionOfCross = DirectionCross.four;
                    break;
                case 270:
                    directionOfCross = DirectionCross.four;
                    ptCenter.X = ((float)(pts[1].X + pts[3].X)) / 2;
                    ptCenter.Y = ((float)(pts[0].Y + pts[2].Y)) / 2;
                    directionOfCross = DirectionCross.first;
                    break;
                default:
                    break;
            }
            startAngle += rotateAngle;
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

        public object Clone(string str)
        {
            CrossEle cl = new CrossEle();
            Point[] pts = new Point[8];
            PointList.CopyTo(pts);
            for (int i = 0; i < 8; i++)
            {
                pts[i].Offset(20, 20);
            }
            cl.PointList.AddRange(pts);
            cl.lenghtOfStrai = lenghtOfStrai;
            cl.startAngle = startAngle;
            cl.rotateAngle = rotateAngle;
            cl.directionOfCross = directionOfCross;
            cl.DrawMultiFactor = DrawMultiFactor;
            cl.objectCrossOp.DrawMultiFactor = DrawMultiFactor;
            cl.FirstPart = firstPart;
            cl.SecPart = secPart;
            cl.ThPart = thPart;
            cl.FourPart = fourPart;
            cl.mirror = mirror;
            cl.railText = str;
            cl.pen = pen;
            return cl;
        }

        public override void DrawEnlargeOrShrink(float multiFactor)
        {
            objectCrossOp.DrawMultiFactor = Convert.ToInt16(multiFactor);
            base.DrawEnlargeOrShrink(DrawMultiFactor);
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
            if (mirror)
                mirror = false;
            else if (!mirror)
                mirror = true;
            PointList.Clear();
            for (int i = 0; i < 8; i++)
                PointList.Add(Point.Ceiling(pts[i]));
        }

        public override void ChangePropertyValue()
        {
            Point ptTemp = new Point(fourPart.X * DrawMultiFactor, fourPart.Y * DrawMultiFactor);
            Point[] ptsList = new Point[8];
            PointList.CopyTo(ptsList);
            switch (directionOfCross)
            {
                case DirectionCross.first:
                    if (PointList[0].X < PointList[1].X)
                    {
                        ptsList[0].X = PointList[1].X - firstPart;
                        ptsList[3].X = ptsList[1].X + secPart;
                        ptsList[4].X = ptsList[3].X;
                        ptsList[5].X = ptsList[3].X + thPart;
                        ptsList[7].X = ptsList[6].X + fourPart.X;
                        ptsList[7].Y = ptsList[6].Y - fourPart.Y;
                    }
                    else
                    {
                        ptsList[0].X = PointList[1].X + firstPart;
                        ptsList[3].X = ptsList[1].X - secPart;
                        ptsList[4].X = ptsList[3].X;
                        ptsList[5].X = ptsList[3].X - thPart;
                        ptsList[7].X = ptsList[6].X - fourPart.X;
                        ptsList[7].Y = ptsList[6].Y - fourPart.Y;
                    }
                    PointList.Clear();
                    PointList.AddRange(ptsList);
                    break;
                case DirectionCross.second:
                    if (PointList[0].Y < PointList[1].Y)
                    {
                        ptsList[0].Y = ptsList[1].Y - firstPart;
                        ptsList[3].Y = ptsList[1].Y + secPart;
                        ptsList[4].Y = ptsList[3].Y;
                        ptsList[5].Y = ptsList[3].Y + thPart;
                        ptsList[7].X = ptsList[6].X + fourPart.X;
                        ptsList[7].Y = ptsList[6].Y + fourPart.Y;
                    }
                    else
                    {
                        ptsList[0].Y = ptsList[1].Y + firstPart;
                        ptsList[3].Y = ptsList[1].Y - secPart;
                        ptsList[4].Y = ptsList[3].Y;
                        ptsList[5].Y = ptsList[3].Y - thPart;
                        ptsList[7].X = ptsList[6].X + fourPart.X;
                        ptsList[7].Y = ptsList[6].Y - fourPart.Y;
                    }
                    PointList.Clear();
                    PointList.AddRange(ptsList);
                    break;
                case DirectionCross.third:
                    if (PointList[0].X < PointList[1].X)
                    {
                        ptsList[0].X = PointList[1].X - firstPart;
                        ptsList[3].X = ptsList[1].X + secPart;
                        ptsList[4].X = ptsList[3].X;
                        ptsList[5].X = ptsList[3].X + thPart;
                        ptsList[7].X = ptsList[6].X + fourPart.X;
                        ptsList[7].Y = ptsList[6].Y + fourPart.Y;
                    }
                    else
                    {
                        ptsList[0].X = PointList[1].X + firstPart;
                        ptsList[3].X = ptsList[1].X - secPart;
                        ptsList[4].X = ptsList[3].X;
                        ptsList[5].X = ptsList[3].X - thPart;
                        ptsList[7].X = ptsList[6].X - fourPart.X;
                        ptsList[7].Y = ptsList[6].Y + fourPart.Y;
                    }
                    PointList.Clear();
                    PointList.AddRange(ptsList);
                    break;
                case DirectionCross.four:
                    if (PointList[0].Y < PointList[1].Y)
                    {
                        ptsList[0].Y = ptsList[1].Y - firstPart;
                        ptsList[3].Y = ptsList[1].Y + secPart;
                        ptsList[4].Y = ptsList[3].Y;
                        ptsList[5].Y = ptsList[3].Y + thPart;
                        ptsList[7].X = ptsList[6].X - fourPart.X;
                        ptsList[7].Y = ptsList[6].Y + fourPart.Y;
                    }
                    else
                    {
                        ptsList[0].Y = ptsList[1].Y + firstPart;
                        ptsList[3].Y = ptsList[1].Y - secPart;
                        ptsList[4].Y = ptsList[3].Y;
                        ptsList[5].Y = ptsList[3].Y - thPart;
                        ptsList[7].X = ptsList[6].X - fourPart.X;
                        ptsList[7].Y = ptsList[6].Y - fourPart.Y;
                    }
                    PointList.Clear();
                    PointList.AddRange(ptsList);
                    break;
                case DirectionCross.NULL:
                    break;
            }
            Debug.WriteLine(string.Format("first is {0},sec is {1},th is {2}", firstPart, secPart, thPart));
        }

        public override bool ChosedInRegion(Rectangle rect)
        {
            return objectCrossOp.ChosedInRegion(rect);
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
            dr["Mirror"] = mirror;
            dr["FirstPart"] = firstPart;
            dr["SecPart"] = secPart;
            dr["ThPart"] = thPart;
            dr["FourPart"] = fourPart.ToString(); ;
            dr["StartAngle"] = startAngle;
            dr["RotateAngle"] = rotateAngle;
            dr["DirectionOfCross"] = directionOfCross;
            dr["PointListVol"] = PointList.Count;
            for (int i = 0; i < PointList.Count; i++)
            {
                dr["PointList" + i.ToString()] = PointList[i];
            }

            dr["drawMultiFactor"] = DrawMultiFactor;
            dr["startPoint"] = StartPoint.ToString();
            dr["endPoint"] = EndPoint.ToString();
            dr["CodingBegin"] = codingBegin;
            dr["CodingEnd"] = codingEnd;
            dr["CodingEndS"] = codingEndS;
            dr["CodingPrev"] = codingPrev;
            dr["CodingNext"] = codingNext;
            dr["CodingNextS"] = codingNextS;
            dr["railText"] = railText;
            dr["lenghtOfStrai"] = lenghtOfStrai;
            
            //dr["startDot"] = startDot;

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
            dr["CodingBegin"] = codingBegin;
            dr["CodingEnd"] = codingEnd;
            dr["CodingEndS"] = codingEndS;
            dr["CodingPrev"] = codingPrev;
            dr["CodingNext"] = codingNext;
            dr["CodingNextS"] = codingNextS;

            dt.Rows.Add(dr);
            return dr;
        }
    }
}
