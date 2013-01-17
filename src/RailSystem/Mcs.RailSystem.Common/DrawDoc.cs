using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Mcs.RailSystem.Common
{
    public class DrawDoc : BaseRailEle
    {
        protected DataSet dsEle = new DataSet();
        public DataSet DsEle
        {
            get { return dsEle; }
        }
        protected DataTable dtEle = new DataTable("RailEleTable");

        protected DataSet dsEleCoding = new DataSet();
        public DataSet DsEleCoding
        {
            get { return dsEleCoding; }
        }
        protected DataTable dtEleCoding = new DataTable("RailEleCodingTable");

        private string _name = "";
        [Browsable(false)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        protected List<BaseRailEle> drawObjectList = new List<BaseRailEle>();
        public List<BaseRailEle> DrawObjectList
        {
            get { return drawObjectList; }
        }
        protected List<BaseRailEle> selectedDrawObjectList = new List<BaseRailEle>();
        public List<BaseRailEle> SelectedDrawObjectList
        {
            get { return selectedDrawObjectList; }
        }

        protected List<BaseRailEle> cutAndCopyObjectList = new List<BaseRailEle>();
        public List<BaseRailEle> CutAndCopyObjectList
        {
            get { return cutAndCopyObjectList; }
        }
        protected BaseRailEle lastHitedObject = null;
        public BaseRailEle LastHitedObject
        {
            get { return lastHitedObject; }
            set { lastHitedObject = value; }
        }

        public enum CutOrCopy
        { 
            CutOp, CopyOp, NoneOp
        }
        protected CutOrCopy cutOrCopy = CutOrCopy.NoneOp;
        public CutOrCopy CutCopy
        {
            get { return cutOrCopy; }
            set { cutOrCopy = value; }
        }
        protected bool chooseObject = false;
        public bool IsChooseObject
        {
            get { return chooseObject; }
            set { chooseObject = value; }
        }
        protected Point downPoint = Point.Empty;
        public Point DownPoint
        {
            get { return downPoint; }
            set { downPoint = value; }
        }
        protected Point lastPoint = Point.Empty;
        public Point LastPoint
        {
            get { return lastPoint; }
            set { downPoint = value; }
        }

        public DrawDoc()
        {
        }

        protected void InitDTEle()
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
            dtEle.Columns.Add("CodingEndF", typeof(Int32));
            dtEle.Columns.Add("CodingNext", typeof(int));
            dtEle.Columns.Add("CodingNextF", typeof(Int32));
            dtEle.Columns.Add("CodingPrev", typeof(int));
            dtEle.Columns.Add("startPoint", typeof(string));
            dtEle.Columns.Add("endPoint", typeof(string));
            dtEle.Columns.Add("StartAngle", typeof(int));
            dtEle.Columns.Add("SweepAngle", typeof(int));
            dtEle.Columns.Add("rotateAngle", typeof(int));
            dtEle.Columns.Add("StartDot", typeof(string));

            dtEle.Columns.Add("Lenght", typeof(int));
            dtEle.Columns.Add("PointListVol", typeof(Int16));
            for (int i = 0; i < 3; i++)
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
            dtEle.Columns.Add("lenght", typeof(Int32));
            dtEle.Columns.Add("lenghtFork", typeof(Int32));
            dtEle.Columns.Add("DirectionOfCross", typeof(int));

            dtEle.Columns.Add("Color", typeof(string));
            dtEle.Columns.Add("DashStyle", typeof(DashStyle));
            dtEle.Columns.Add("PenWidth", typeof(float));
        }

        protected void InitDTEleCoding()
        {
            dtEleCoding.Columns.Add("GraphType", typeof(int));
            dtEleCoding.Columns.Add("CodingBegin", typeof(int));
            dtEleCoding.Columns.Add("CodingEnd", typeof(int));
            dtEleCoding.Columns.Add("CodingEndF", typeof(int));
            dtEleCoding.Columns.Add("CodingNext", typeof(int));
            dtEleCoding.Columns.Add("CodingNextF", typeof(int));
            dtEleCoding.Columns.Add("CodingPrev", typeof(int));
        }

        protected void SaveElementInfo(BaseRailEle obj)
        {
            DataRow dr=dtEle.NewRow();
            switch (obj.GraphType)
            {
                case 1:
                    EleLine line = (EleLine)obj;
                    dr["GraphType"] = line.GraphType;
                    dr["LocationLock"] = line.LocationLock;
                    dr["SizeLock"] = line.SizeLock;
                    dr["Selectable"] = line.Selectable;
                    dr["Speed"] = line.Speed;
                    dr["CodingBegin"] = line.CodingBegin;
                    dr["CodingEnd"] = line.CodingEnd;
                    dr["CodingNext"] = line.CodingNext;
                    dr["CodingPrev"] = line.CodingPrev;
                    dr["Lenght"] = line.Lenght;
                    dr["StartAngle"] = line.StartAngle;
                    dr["rotateAngle"] = line.RotateAngle;
                    dr["PointListVol"] = line.PointList.Count;
                    for (int i = 0; i < line.PointList.Count; i++)
                    {
                        dr["PointList" + i.ToString()] = line.PointList[i].ToString();
                    }

                    dr["DrawMultiFactor"] = line.DrawMultiFactor;
                    dr["startPoint"] = line.StartPoint.ToString();
                    dr["endPoint"] = line.EndPoint.ToString();
                    dr["railText"] = line.railText;

                    dr["Color"] = ColorTranslator.ToHtml(line.PenLine.Color);
                    dr["DashStyle"] = line.PenLine.DashStyle;
                    dr["PenWidth"] = line.PenLine.Width;

                    dtEle.Rows.Add(dr);
                    break;
                case 2:
                    EleCurve curve = (EleCurve)obj;
                    dr["GraphType"] = curve.GraphType;
                    dr["LocationLock"] = curve.LocationLock;
                    dr["SizeLock"] = curve.SizeLock;
                    dr["Selectable"] = curve.Selectable;
                    dr["Speed"] = curve.Speed;
                    dr["StartAngle"] = curve.StartAngle;
                    dr["SweepAngle"] = curve.SweepAngle;
                    dr["Radiu"] = curve.Radiu;
                    dr["Center"] = curve.Center.ToString();
                    dr["FirstDot"] = curve.FirstDot.ToString();
                    dr["SecDot"] = curve.SecDot.ToString();
                    dr["DirectionCurvedAttribute"] = curve.DirectionCurvedAttribute;

                    dr["drawMultiFactor"] = curve.DrawMultiFactor;
                    dr["startPoint"] = curve.StartPoint.ToString();
                    dr["endPoint"] = curve.EndPoint.ToString();
                    dr["CodingBegin"] = curve.CodingBegin;
                    dr["CodingEnd"] = curve.CodingEnd;
                    dr["CodingNext"] = curve.CodingNext;
                    dr["CodingPrev"] = curve.CodingPrev;
                    dr["railText"] = curve.railText;
                    dr["rotateAngle"] = curve.RotateAngle;
                    dr["oldRadiu"] = curve.oldRadiu;
                    dr["oldCenter"] = curve.oldCenter.ToString();
                    dr["oldFirstDot"] = curve.oldFirstDot.ToString();
                    dr["oldSecDot"] = curve.oldSecDot.ToString();

                    dr["Color"] = ColorTranslator.ToHtml(curve.PenCurve.Color);
                    dr["DashStyle"] = curve.PenCurve.DashStyle;
                    dr["PenWidth"] = curve.PenCurve.Width;

                    dtEle.Rows.Add(dr);
                    break;
                case 3:
                    EleCross cross = (EleCross)obj;
                    dr["GraphType"] = cross.GraphType;
                    dr["LocationLock"] = cross.LocationLock;
                    dr["SizeLock"] = cross.SizeLock;
                    dr["Selectable"] = cross.Selectable;
                    dr["Speed"] = cross.Speed;
                    dr["Mirror"] = cross.Mirror;
                    dr["StartAngle"] = cross.StartAngle;
                    dr["RotateAngle"] = cross.RotateAngle;
                    dr["DirectionOfCross"] = cross.DirectionOfCross;
                    dr["PointListVol"] = cross.PointList.Count;
                    for (int i = 0; i < cross.PointList.Count; i++)
                    {
                        dr["PointList" + i.ToString()] = cross.PointList[i];
                    }

                    dr["drawMultiFactor"] = cross.DrawMultiFactor;
                    dr["startPoint"] = cross.StartPoint.ToString();
                    dr["endPoint"] = cross.EndPoint.ToString();
                    dr["CodingBegin"] = cross.CodingBegin;
                    dr["CodingEnd"] = cross.CodingEnd;
                    dr["CodingEndF"] = cross.CodingEndFork;
                    dr["CodingPrev"] = cross.CodingPrev;
                    dr["CodingNext"] = cross.CodingNext;
                    dr["CodingNextF"] = cross.CodingNextFork;
                    dr["railText"] = cross.railText;
                    dr["lenght"] = cross.Lenght;
                    dr["lenghtFork"] = cross.LenghtFork;
                    dr["Color"] = ColorTranslator.ToHtml(cross.PenCross.Color);
                    dr["DashStyle"] = cross.PenCross.DashStyle;
                    dr["PenWidth"] = cross.PenCross.Width;

                    dtEle.Rows.Add(dr);
                    break;
            }
        }

        protected void SaveCodingInfo(BaseRailEle obj)
        {
            DataRow dr = dtEleCoding.NewRow();
            switch (obj.GraphType)
            {
                case 1:
                    EleLine line = (EleLine)obj;

                    dr["GraphType"] = line.GraphType;
                    dr["CodingBegin"] = line.CodingBegin;
                    dr["CodingEnd"] = line.CodingEnd;
                    dr["CodingNext"] = line.CodingNext;
                    dr["CodingPrev"] = line.CodingPrev;

                    dtEleCoding.Rows.Add(dr);
                    break;
                case 2:
                    EleCurve curve = (EleCurve)obj;

                    dr["GraphType"] = curve.GraphType;
                    dr["CodingBegin"] = curve.CodingBegin;
                    dr["CodingEnd"] = curve.CodingEnd;
                    dr["CodingNext"] = curve.CodingNext;
                    dr["CodingPrev"] = curve.CodingPrev;

                    dtEleCoding.Rows.Add(dr);
                    break;
                case 3:
                    EleCross cross = (EleCross)obj;

                    dr["GraphType"] = cross.GraphType;
                    dr["CodingBegin"] = cross.CodingBegin;
                    dr["CodingEnd"] = cross.CodingEnd;
                    dr["CodingEndF"] = cross.CodingEndFork;
                    dr["CodingPrev"] = cross.CodingPrev;
                    dr["CodingNext"] = cross.CodingNext;
                    dr["CodingNextF"] = cross.CodingNextFork;

                    dtEleCoding.Rows.Add(dr);
                    break;
            }
        }

        public void InitDataTable(DataTable dtSource)
        {
            dtEle = dtSource;
        }

        public void ReadDataFromRow(int row, BaseRailEle obj)
        {
            switch (obj.GraphType)
            {
                case 1:
                    EleLine line = (EleLine)obj;
                    string str = "";
                    string[] strPointArray = { };
                    Point ptTemp = Point.Empty;
                    Int16 pointListVolStr = 0;
                    for (int j = 0; j < dtEle.Columns.Count; j++)
                    {
                        switch (dtEle.Columns[j].ColumnName)
                        {
                            case "GraphType":
                                line.GraphType = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "Speed":
                                line.Speed = Convert.ToSingle(dtEle.Rows[row][j]);
                                break;
                            case "Lenght":
                                line.Lenght = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "StartAngle":
                                line.StartAngle = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "PointListVol":
                                pointListVolStr = Convert.ToInt16(dtEle.Rows[row][j]);
                                for (int k = 0; k < pointListVolStr; k++)
                                {
                                    str = dtEle.Rows[row][j + k + 1].ToString();
                                    str = str.Substring(1, str.Length - 2);
                                    strPointArray = str.Split(',');
                                    ptTemp = new Point() { X = int.Parse(strPointArray[0].Substring(2)), Y = int.Parse(strPointArray[1].Substring(2)) };
                                    line.PointList.Add(ptTemp);
                                }
                                break;
                            case "DrawMultiFactor":
                                line.DrawMultiFactor = Convert.ToInt16(dtEle.Rows[row][j]);
                                break;
                            case "startPoint":
                                str = dtEle.Rows[row][j].ToString();
                                str = str.Substring(1, str.Length - 2);
                                strPointArray = str.Split(',');
                                ptTemp = new Point() { X = int.Parse(strPointArray[0].Substring(2)), Y = int.Parse(strPointArray[1].Substring(2)) };
                                line.StartPoint = ptTemp;
                                break;
                            case "endPoint":
                                str = dtEle.Rows[row][j].ToString();
                                str = str.Substring(1, str.Length - 2);
                                strPointArray = str.Split(',');
                                ptTemp = new Point() { X = int.Parse(strPointArray[0].Substring(2)), Y = int.Parse(strPointArray[1].Substring(2)) };
                                line.EndPoint = ptTemp;
                                break;
                            case "railText":
                                line.railText = dtEle.Rows[row][j].ToString();
                                break;
                            case "rotateAngle":
                                line.RotateAngle = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "CodingBegin":
                                line.CodingBegin = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "CodingEnd":
                                line.CodingEnd = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "CodingNext":
                                line.CodingNext = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "prevCoding":
                                line.CodingPrev = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "Color":
                                line.PenColor = ColorTranslator.FromHtml(dtEle.Rows[row][j].ToString());
                                break;
                            case "DashStyle":
                                line.PenDashStyle = (System.Drawing.Drawing2D.DashStyle)(Convert.ToInt32(dtEle.Rows[row][j]));
                                break;
                            case "PenWidth":
                                line.PenWidth = Convert.ToSingle(dtEle.Rows[row][j]);
                                break;
                        }
                    }
                    break;
                case 2:
                    EleCurve curve = (EleCurve)obj;
                    string strcur = "";
                    string[] strPointArrayCur = { };
                    Point ptcur = Point.Empty;
                    for (int j = 0; j < dtEle.Columns.Count; j++)
                    {
                        switch (dtEle.Columns[j].ColumnName)
                        {
                            case "GraphType":
                                curve.GraphType = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "Speed":
                                curve.Speed = Convert.ToSingle(dtEle.Rows[row][j]);
                                break;
                            case "StartAngle":
                                curve.StartAngle = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "SweepAngle":
                                curve.SweepAngle = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "Radiu":
                                curve.Radiu = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "Center":
                                strcur = dtEle.Rows[row][j].ToString();
                                strcur = strcur.Substring(1, strcur.Length - 2);
                                strPointArrayCur = strcur.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.Center = ptcur;
                                break;
                            case "FirstDot":
                                strcur = dtEle.Rows[row][j].ToString();
                                strcur = strcur.Substring(1, strcur.Length - 2);
                                strPointArrayCur = strcur.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.FirstDot = ptcur;
                                break;
                            case "SecDot":
                                strcur = dtEle.Rows[row][j].ToString();
                                strcur = strcur.Substring(1, strcur.Length - 2);
                                strPointArrayCur = strcur.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.SecDot = ptcur;
                                break;
                            case "DirectionCurvedAttribute":
                                curve.DirectionCurvedAttribute = (Mcs.RailSystem.Common.EleCurve.DirectonCurved)Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "startPoint":
                                str = dtEle.Rows[row][j].ToString();
                                str = str.Substring(1, str.Length - 2);
                                strPointArrayCur = str.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.StartPoint = ptcur;
                                break;
                            case "endPoint":
                                str = dtEle.Rows[row][j].ToString();
                                str = str.Substring(1, str.Length - 2);
                                strPointArrayCur = str.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.EndPoint = ptcur;
                                break;
                            case "CodingBegin":
                                curve.CodingBegin = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "CodingEnd":
                                curve.CodingEnd = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "CodingNext":
                                curve.CodingNext = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "CodingPrev":
                                curve.CodingPrev = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "railText":
                                curve.railText = dtEle.Rows[row][j].ToString();
                                break;
                            case "rotateAngle":
                                curve.RotateAngle = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "oldRadiu":
                                curve.oldRadiu = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "oldCenter":
                                str = dtEle.Rows[row][j].ToString();
                                str = str.Substring(1, str.Length - 2);
                                strPointArrayCur = str.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.oldCenter = ptcur;
                                break;
                            case "oldFirstDot":
                                str = dtEle.Rows[row][j].ToString();
                                str = str.Substring(1, str.Length - 2);
                                strPointArrayCur = str.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.oldFirstDot = ptcur;
                                break;
                            case "oldSecDot":
                                str = dtEle.Rows[row][j].ToString();
                                str = str.Substring(1, str.Length - 2);
                                strPointArrayCur = str.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.oldSecDot = ptcur;
                                break;
                            case "Color":
                                curve.PenColor = ColorTranslator.FromHtml(dtEle.Rows[row][j].ToString());
                                break;
                            case "DashStyle":
                                curve.PenDashStyle = (System.Drawing.Drawing2D.DashStyle)(Convert.ToInt32(dtEle.Rows[row][j]));
                                break;
                            case "PenWidth":
                                curve.PenWidth = Convert.ToSingle(dtEle.Rows[row][j]);
                                break;
                        }
                    }
                    break;
                case 3:
                    EleCross cross = (EleCross)obj;
                    string strcro = "";
                    string[] strPointArrayCro = { };
                    Point ptcro = Point.Empty;
                    Int16 pointListVolCro = 0;
                    for (int j = 0; j < dtEle.Columns.Count; j++)
                    {
                        switch (dtEle.Columns[j].ColumnName)
                        {
                            case "GraphType":
                                cross.GraphType = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "Speed":
                                cross.Speed = Convert.ToSingle(dtEle.Rows[row][j]);
                                break;
                            case "Mirror":
                                cross.Mirror = Convert.ToBoolean(dtEle.Rows[row][j]);
                                break;
                            case "StartAngle":
                                cross.StartAngle = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "RotateAngle":
                                cross.RotateAngle = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "DirectionOfCross":
                                cross.DirectionOfCross = (Mcs.RailSystem.Common.EleCross.DirectionCross)Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "PointListVol":
                                pointListVolCro = Convert.ToInt16(dtEle.Rows[row][j]);
                                for (Int16 k = 0; k < pointListVolCro; k++)
                                {
                                    strcro = dtEle.Rows[row][j + k + 1].ToString();
                                    strcro = strcro.Substring(1, strcro.Length - 2);
                                    strPointArrayCro = strcro.Split(',');
                                    ptcro = new Point() { X = int.Parse(strPointArrayCro[0].Substring(2)), Y = int.Parse(strPointArrayCro[1].Substring(2)) };
                                    cross.PointList.Add(ptcro);
                                }
                                break;
                            case "drawMultiFactor":
                                cross.DrawMultiFactor = Convert.ToInt16(dtEle.Rows[row][j]);
                                break;
                            case "startPoint":
                                strcro = dtEle.Rows[row][j].ToString();
                                strcro = strcro.Substring(1, strcro.Length - 2);
                                strPointArrayCro = strcro.Split(',');
                                ptcro = new Point() { X = int.Parse(strPointArrayCro[0].Substring(2)), Y = int.Parse(strPointArrayCro[1].Substring(2)) };
                                cross.StartPoint = ptcro;
                                break;
                            case "endPoint":
                                strcro = dtEle.Rows[row][j].ToString();
                                strcro = strcro.Substring(1, strcro.Length - 2);
                                strPointArrayCro = strcro.Split(',');
                                ptcro = new Point() { X = int.Parse(strPointArrayCro[0].Substring(2)), Y = int.Parse(strPointArrayCro[1].Substring(2)) };
                                cross.EndPoint = ptcro;
                                break;
                            case "CodingBegin":
                                cross.CodingBegin = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "CodingEnd":
                                cross.CodingEnd = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "CodingEndF":
                                cross.CodingEndFork = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "CodingNext":
                                cross.CodingNext = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "CodingPrev":
                                cross.CodingPrev = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "CodingNextF":
                                cross.CodingNextFork = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "railText":
                                cross.railText = dtEle.Rows[row][j].ToString();
                                break;
                            case "lenght":
                                cross.Lenght = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "lenghtFork":
                                cross.LenghtFork = Convert.ToInt32(dtEle.Rows[row][j]);
                                break;
                            case "Color":
                                cross.PenColor = ColorTranslator.FromHtml(dtEle.Rows[row][j].ToString());
                                break;
                            case "DashStyle":
                                cross.PenDashStyle = (System.Drawing.Drawing2D.DashStyle)(Convert.ToInt32(dtEle.Rows[row][j]));
                                break;
                            case "PenWidth":
                                cross.PenWidth = Convert.ToSingle(dtEle.Rows[row][j]);
                                break;
                        }
                    }
                    break;
            }
        }


    }
}
