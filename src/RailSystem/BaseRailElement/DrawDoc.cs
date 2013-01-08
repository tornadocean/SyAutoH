using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;

namespace BaseRailElement
{
    //[XmlInclude(typeof(StraightRailEle))]
    //[XmlInclude(typeof(CurvedRailEle))]
    //[XmlInclude(typeof(CrossEle))]
    [XmlInclude(typeof(RailLabal))]

    public class DrawDoc : Mcs.RailSystem.Common.BaseRailEle
    {
        public DataSet dsEle = new DataSet();
        public DataTable dtEle = new DataTable("RailEleTable");

        public DataSet dsEleCoding = new DataSet();
        public DataTable dtEleCoding = new DataTable("RailEleCodingTable");

        private string _name = "";
        [Browsable(false)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [
        //XmlArrayItem(Type = typeof(StraightRailEle)),
        //XmlArrayItem(Type = typeof(CurvedRailEle)),
        //XmlArrayItem(Type = typeof(CrossEle)),
        XmlArrayItem(Type = typeof(RailLabal)),
        ]

        [XmlIgnore]
        public static DrawDoc EmptyDocument
        {
            get { return new DrawDoc(); }
        }

        List<Mcs.RailSystem.Common.BaseRailEle> drawObjectList = new List<Mcs.RailSystem.Common.BaseRailEle>();
        [Browsable(false)]
        public List<Mcs.RailSystem.Common.BaseRailEle> DrawObjectList
        {
            get { return drawObjectList; }
        }

        List<Mcs.RailSystem.Common.BaseRailEle> selectedDrawObjectList = new List<Mcs.RailSystem.Common.BaseRailEle>();
        [XmlIgnore]
        [Browsable(false)]
        public List<Mcs.RailSystem.Common.BaseRailEle> SelectedDrawObjectList
        {
            get { return selectedDrawObjectList; }
        }

        public List<Mcs.RailSystem.Common.BaseRailEle> CutAndCopyObjectList = new List<Mcs.RailSystem.Common.BaseRailEle>();

        private Mcs.RailSystem.Common.BaseRailEle lastHitedObject = null;

        private enum CutOrCopy
        {
            CutOp, CopyOp, NoneOp
        }
        private CutOrCopy _CutOrCopy = CutOrCopy.NoneOp;

        private bool chooseObject = false;
        private Point downPoint = Point.Empty;
        private Point lastPoint = Point.Empty;

        public DrawDoc()
        {
            InitDTEle();
            InitDTEleCoding();
        }

        private void InitDTEle()
        {
            dtEle.Columns.Add("GraphType", typeof(int));
            dtEle.Columns.Add("LocationLock", typeof(bool));
            dtEle.Columns.Add("SizeLock", typeof(bool));
            dtEle.Columns.Add("Selectable", typeof(bool));
            dtEle.Columns.Add("Speed", typeof(float));
            dtEle.Columns.Add("SegmentNumber", typeof(Int16));
            dtEle.Columns.Add("TagNumber", typeof(int));
            dtEle.Columns.Add("DrawMultiFactor", typeof(Int16));
            dtEle.Columns.Add("railText", typeof(string));
            dtEle.Columns.Add("CodingBegin", typeof(int));
            dtEle.Columns.Add("CodingEnd", typeof(int));
            dtEle.Columns.Add("CodingEndS", typeof(int));
            dtEle.Columns.Add("CodingNext", typeof(int));
            dtEle.Columns.Add("CodingNextS", typeof(int));
            dtEle.Columns.Add("CodingPrev", typeof(int));
            dtEle.Columns.Add("startPoint", typeof(string));
            dtEle.Columns.Add("endPoint", typeof(string));
            dtEle.Columns.Add("StartAngle", typeof(int));
            dtEle.Columns.Add("SweepAngle", typeof(int));
            dtEle.Columns.Add("rotateAngle", typeof(int));
            dtEle.Columns.Add("StartDot", typeof(string));

            dtEle.Columns.Add("Lenght", typeof(int));
            dtEle.Columns.Add("PointListVol", typeof(Int16));
            for (int i = 0; i < 8; i++)
            {
                dtEle.Columns.Add("PointList" + i.ToString(), typeof(string));
            }

            dtEle.Columns.Add("Radiu", typeof(int));
            dtEle.Columns.Add("Center", typeof(string));
            dtEle.Columns.Add("FirstDot", typeof(string));
            dtEle.Columns.Add("SecDot", typeof(string));
            dtEle.Columns.Add("oldRadiu", typeof(Int32));
            dtEle.Columns.Add("oldCenter", typeof(string));
            dtEle.Columns.Add("oldFirstDot", typeof(string));
            dtEle.Columns.Add("oldSecDot", typeof(string));
            dtEle.Columns.Add("DirectionCurvedAttribute", typeof(int));

            dtEle.Columns.Add("Mirror", typeof(bool));
            dtEle.Columns.Add("FirstPart", typeof(int));
            dtEle.Columns.Add("SecPart", typeof(int));
            dtEle.Columns.Add("ThPart", typeof(int));
            dtEle.Columns.Add("FourPart", typeof(string));
            dtEle.Columns.Add("lenghtOfStrai", typeof(Int32));
            dtEle.Columns.Add("DirectionOfCross", typeof(int));

            dtEle.Columns.Add("Color", typeof(string));
            dtEle.Columns.Add("DashStyle", typeof(DashStyle));
            dtEle.Columns.Add("PenWidth", typeof(float));
        }

        private void InitDTEleCoding()
        {
            dtEleCoding.Columns.Add("GraphType", typeof(int));
            dtEleCoding.Columns.Add("CodingBegin", typeof(int));
            dtEleCoding.Columns.Add("CodingEnd", typeof(int));
            dtEleCoding.Columns.Add("CodingEndS", typeof(int));
            dtEleCoding.Columns.Add("CodingNext", typeof(int));
            dtEleCoding.Columns.Add("CodingNextS", typeof(int));
            dtEleCoding.Columns.Add("CodingPrev", typeof(int));
        }

        public override void Draw(Graphics canvas)
        {
            // setting smoothing mode
            canvas.SmoothingMode = SmoothingMode.HighQuality;
            canvas.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;


            int n = drawObjectList.Count;
            for (int i = 0; i < n; i++)
            {
                if (this.DrawMultiFactor != -1)
                {
                    drawObjectList[i].DrawEnlargeOrShrink(DrawMultiFactor);
                }
                drawObjectList[i].Draw(canvas);
                if (selectedDrawObjectList.Contains(drawObjectList[i]))
                    drawObjectList[i].DrawTracker(canvas);
            }
            this.DrawMultiFactor = -1;
            if (chooseObject)
            {
                ChooseObject(canvas);
            }
        }

        public override int HitTest(Point point, bool isSelected)
        {
            int n = 0;
            int hit = -1;
            n = selectedDrawObjectList.Count;
            for (int i = 0; i < n; i++)
            {
                hit = selectedDrawObjectList[i].HitTest(point, true);
                if (hit >= 0)
                {
                    lastHitedObject = selectedDrawObjectList[i];
                    return hit;
                }
            }

            n = drawObjectList.Count;
            for (int i = n - 1; i >= 0; i--)
            {
                hit = drawObjectList[i].HitTest(point, false);
                if (hit >= 0)
                {
                    lastHitedObject = drawObjectList[i];

                    if (drawObjectList[i].Selectable)
                    {
                        SelectOne(drawObjectList[i]);
                        return hit;
                    }
                    break;
                }
            }
            if (hit == -1)
                lastHitedObject = null;
            selectedDrawObjectList.Clear();
            return -1;
        }

        public void SelectOne(Mcs.RailSystem.Common.BaseRailEle obj)
        {
            selectedDrawObjectList.Clear();
            if (obj != null)
                selectedDrawObjectList.Add(obj);
        }

        public void SelectMore(Mcs.RailSystem.Common.BaseRailEle obj)
        {
            if (obj != null)
                selectedDrawObjectList.Add(obj);
        }

        public void Cut()
        {
            CutAndCopyObjectList.Clear();
            if (selectedDrawObjectList.Count > 0)
            {
                foreach (Mcs.RailSystem.Common.BaseRailEle o in selectedDrawObjectList)
                {
                    CutAndCopyObjectList.Add(o);
                }
                _CutOrCopy = CutOrCopy.CutOp;
            }
        }

        public void Copy()
        {
            CutAndCopyObjectList.Clear();
            if (selectedDrawObjectList.Count > 0)
            {
                foreach (Mcs.RailSystem.Common.BaseRailEle o in selectedDrawObjectList)
                {
                    CutAndCopyObjectList.Add(o);
                }
                _CutOrCopy = CutOrCopy.CopyOp;
            }
        }

        public void Paste(string str)
        {
            if (CutAndCopyObjectList.Count > 0)
            {
                if (_CutOrCopy == CutOrCopy.CutOp)
                {
                    int n = selectedDrawObjectList.Count;
                    foreach (Mcs.RailSystem.Common.BaseRailEle obj in selectedDrawObjectList)
                    {
                        drawObjectList.Remove(obj);
                    }
                }
                else if (_CutOrCopy == CutOrCopy.CopyOp)
                {
                    Mcs.RailSystem.Common.BaseRailEle o = CutAndCopyObjectList[0];
                    if (1 == o.GraphType)
                    {
                        RailEleLine cl = (RailEleLine)o;
                        RailEleLine n = (RailEleLine)cl.Clone(str);
                        drawObjectList.Add(n);
                        SelectOne(n);
                    }
                    else if (2 == o.GraphType)
                    {
                        RailEleCurve cl = (RailEleCurve)o;
                        RailEleCurve n = (RailEleCurve)cl.Clone(str);
                        drawObjectList.Add(n);
                        SelectOne(n);
                    }
                    else if (3 == o.GraphType)
                    {
                        RailEleCross cl = (RailEleCross)o;
                        RailEleCross n = (RailEleCross)cl.Clone(str);
                        drawObjectList.Add(n);
                        SelectOne(n);
                    }
                    //else if (4 == o.GraphType)
                    //{
                    //    RailLabal cl = (RailLabal)o;
                    //    RailLabal n = (RailLabal)cl.Clone();
                    //    drawObjectList.Add(n);
                    //    SelectOne(n);
                    //}
                    CutAndCopyObjectList.RemoveAt(0);
                }
            }
        }

        public void Delete(Int16 index)
        {
            drawObjectList.RemoveAt(index);
            selectedDrawObjectList.RemoveAt(0);
        }

        public void ChooseObject(Graphics gp)
        {
            Rectangle rc = new Rectangle(downPoint.X, downPoint.Y, lastPoint.X - downPoint.X, lastPoint.Y - downPoint.Y);
            Pen pen = new Pen(Color.Black, 1);
            pen.DashStyle = DashStyle.Dot;
            gp.DrawRectangle(pen, rc);
            pen.Dispose();
        }

        public void ChangeChooseSign(bool isDown, Point pt)
        {
            if (isDown)
            {
                if (!chooseObject)
                {
                    downPoint = pt;
                    chooseObject = true;
                }
                else
                    lastPoint = pt;
            }
            else
            {
                Rectangle rc = new Rectangle(downPoint.X, downPoint.Y, lastPoint.X - downPoint.X, lastPoint.Y - downPoint.Y);
                int num = drawObjectList.Count;
                for (int i = 0; i < num; i++)
                {
                    if (drawObjectList[i].ChosedInRegion(rc))
                    {
                        SelectMore(drawObjectList[i]);
                    }
                }
                chooseObject = false;
                downPoint = Point.Empty;
                lastPoint = Point.Empty;
            }
        }

        public void DataXmlSave()
        {
            SaveElementInfo();
            SaveCodingInfo();
        }

        private void SaveElementInfo()
        {
            dtEle.Rows.Clear();
            dsEle.Tables.Clear();
            int num = drawObjectList.Count;
            for (int i = 0; i < num; i++)
            {
                drawObjectList[i].SaveEleInfo(dtEle);
            }
            dsEle.Tables.Add(dtEle);
        }

        private void SaveCodingInfo()
        {
            dtEleCoding.Rows.Clear();
            dsEleCoding.Tables.Clear();
            int num = drawObjectList.Count;
            for (int i = 0; i < num; i++)
            {
                drawObjectList[i].SaveCodingInfo(dtEleCoding);
            }
            dsEleCoding.Tables.Add(dtEleCoding);
        }
    }
}
