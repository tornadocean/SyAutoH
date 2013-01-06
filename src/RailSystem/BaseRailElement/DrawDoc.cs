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
    [XmlInclude(typeof(StraightRailEle))]
    [XmlInclude(typeof(CurvedRailEle))]
    [XmlInclude(typeof(CrossEle))]
    [XmlInclude(typeof(RailLabal))]

    public class DrawDoc : BaseRailEle
    {
        public DataSet ds = new DataSet();
        public DataTable dt = new DataTable("RailEleTable");

        private string _name = "";
        [Browsable(false)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [
        XmlArrayItem(Type = typeof(StraightRailEle)),
        XmlArrayItem(Type = typeof(CurvedRailEle)),
        XmlArrayItem(Type = typeof(CrossEle)),
        XmlArrayItem(Type = typeof(RailLabal)),
        ]

        [XmlIgnore]
        public static DrawDoc EmptyDocument
        {
            get { return new DrawDoc(); }
        }

        List<BaseRailEle> drawObjectList = new List<BaseRailEle>();
        [Browsable(false)]
        public List<BaseRailEle> DrawObjectList
        {
            get { return drawObjectList; }
        }

        List<BaseRailEle> selectedDrawObjectList = new List<BaseRailEle>();
        [XmlIgnore]
        [Browsable(false)]
        public List<BaseRailEle> SelectedDrawObjectList
        {
            get { return selectedDrawObjectList; }
        }

        public List<BaseRailEle> CutAndCopyObjectList = new List<BaseRailEle>();

        private BaseRailEle lastHitedObject = null;

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
            dt.Columns.Add("GraphType", typeof(int));
            dt.Columns.Add("LocationLock", typeof(bool));
            dt.Columns.Add("SizeLock", typeof(bool));
            dt.Columns.Add("Selectable", typeof(bool));
            dt.Columns.Add("Speed", typeof(float));
            dt.Columns.Add("SegmentNumber", typeof(Int16));
            dt.Columns.Add("TagNumber", typeof(int));
            dt.Columns.Add("DrawMultiFactor", typeof(Int16));
            dt.Columns.Add("railText", typeof(string));
            dt.Columns.Add("CodingBegin", typeof(int));
            dt.Columns.Add("CodingEnd", typeof(int));
            dt.Columns.Add("startPoint", typeof(string));
            dt.Columns.Add("endPoint", typeof(string));
            dt.Columns.Add("StartAngle", typeof(int));
            dt.Columns.Add("SweepAngle", typeof(int));
            dt.Columns.Add("rotateAngle", typeof(int));
            dt.Columns.Add("StartDot", typeof(string));
            dt.Columns.Add("nextCoding", typeof(int));
            dt.Columns.Add("prevCoding", typeof(int));
            dt.Columns.Add("thirdDotCoding", typeof(Int32));

            dt.Columns.Add("Lenght", typeof(int));
            dt.Columns.Add("PointListVol", typeof(Int16));
            for (int i = 0; i < 8; i++)
            {
                dt.Columns.Add("PointList" + i.ToString(), typeof(string));
            }

            dt.Columns.Add("Radiu", typeof(int));
            dt.Columns.Add("Center", typeof(string));
            dt.Columns.Add("FirstDot", typeof(string));
            dt.Columns.Add("SecDot", typeof(string));
            dt.Columns.Add("oldRadiu", typeof(Int32));
            dt.Columns.Add("oldCenter", typeof(string));
            dt.Columns.Add("oldFirstDot", typeof(string));
            dt.Columns.Add("oldSecDot", typeof(string));
            dt.Columns.Add("DirectionCurvedAttribute", typeof(int));

            dt.Columns.Add("Mirror", typeof(bool));
            dt.Columns.Add("FirstPart", typeof(int));
            dt.Columns.Add("SecPart", typeof(int));
            dt.Columns.Add("ThPart", typeof(int));
            dt.Columns.Add("FourPart", typeof(string));
            dt.Columns.Add("lenghtOfStrai", typeof(Int32));
            dt.Columns.Add("DirectionOfCross", typeof(int));

            dt.Columns.Add("Color", typeof(string));
            dt.Columns.Add("DashStyle", typeof(DashStyle));
            dt.Columns.Add("PenWidth", typeof(float));
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

        public void SelectOne(BaseRailEle obj)
        {
            selectedDrawObjectList.Clear();
            if (obj != null)
                selectedDrawObjectList.Add(obj);
        }

        public void SelectMore(BaseRailEle obj)
        {
            if (obj != null)
                selectedDrawObjectList.Add(obj);
        }

        public void Cut()
        {
            CutAndCopyObjectList.Clear();
            if (selectedDrawObjectList.Count > 0)
            {
                foreach (BaseRailEle o in selectedDrawObjectList)
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
                foreach (BaseRailEle o in selectedDrawObjectList)
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
                    foreach (BaseRailEle obj in selectedDrawObjectList)
                    {
                        drawObjectList.Remove(obj);
                    }
                }
                else if (_CutOrCopy == CutOrCopy.CopyOp)
                {
                    BaseRailEle o = CutAndCopyObjectList[0];
                    if (1 == o.GraphType)
                    {
                        StraightRailEle cl = (StraightRailEle)o;
                        StraightRailEle n = (StraightRailEle)cl.Clone(str);
                        drawObjectList.Add(n);
                        SelectOne(n);
                    }
                    else if (2 == o.GraphType)
                    {
                        CurvedRailEle cl = (CurvedRailEle)o;
                        CurvedRailEle n = (CurvedRailEle)cl.Clone(str);
                        drawObjectList.Add(n);
                        SelectOne(n);
                    }
                    else if (3 == o.GraphType)
                    {
                        CrossEle cl = (CrossEle)o;
                        CrossEle n = (CrossEle)cl.Clone(str);
                        drawObjectList.Add(n);
                        SelectOne(n);
                    }
                    else if (4 == o.GraphType)
                    {
                        RailLabal cl = (RailLabal)o;
                        RailLabal n = (RailLabal)cl.Clone();
                        drawObjectList.Add(n);
                        SelectOne(n);
                    }
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
            dt.Rows.Clear();
            ds.Tables.Clear();
            int num = drawObjectList.Count;
            for (int i = 0; i < num; i++)
            {
                drawObjectList[i].DataSetXMLSave(dt);
            }
            ds.Tables.Add(dt);
        }
    }
}
