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
    public class DrawDocOp : Mcs.RailSystem.Common.DrawDoc 
    {
        public DrawDocOp()
        {
            InitDTEle();
            InitDTEleCoding();
        }

        public static DrawDocOp EmptyDocument
        {
            get { return new DrawDocOp(); }
        }

        public override void Draw(Graphics canvas)
        {
            // setting smoothing mode
            canvas.SmoothingMode = SmoothingMode.HighQuality;
            canvas.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;


            int n = drawObjectList.Count;
            for (int i = 0; i < n; i++)
            {
                drawObjectList[i].Draw(canvas);
                if (selectedDrawObjectList.Contains(drawObjectList[i]))
                    drawObjectList[i].DrawTracker(canvas);
            }
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
                cutOrCopy = CutOrCopy.CutOp;
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
                cutOrCopy = CutOrCopy.CopyOp;
            }
        }

        public void Paste(string str)
        {
            if (CutAndCopyObjectList.Count > 0)
            {
                if (cutOrCopy == CutOrCopy.CutOp)
                {
                    int n = selectedDrawObjectList.Count;
                    foreach (Mcs.RailSystem.Common.BaseRailEle obj in selectedDrawObjectList)
                    {
                        drawObjectList.Remove(obj);
                    }
                }
                else if (cutOrCopy == CutOrCopy.CopyOp)
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
                    else if (5 == o.GraphType)
                    {
                        RailEleFoupDot cl = (RailEleFoupDot)o;
                        RailEleFoupDot n = (RailEleFoupDot)cl.Clone(str);
                        drawObjectList.Add(n);
                        SelectOne(n);
                    }
                    else if (6 == o.GraphType)
                    {
                        RailEleDevice cl = (RailEleDevice)o;
                        RailEleDevice n = (RailEleDevice)cl.Clone(str);
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
            listTreeNode.RemoveAt(index);
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
                SaveElementInfo(drawObjectList[i]);
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
                SaveCodingInfo(drawObjectList[i]);
            }
            dsEleCoding.Tables.Add(dtEleCoding);
        }
    }
}
