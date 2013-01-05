using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;

namespace WinFormElement
{
    public class FormShowRegion
    {
        public List<RailEle> railInfoEleList = new List<RailEle>();
        public List<RailEle> railCodingEleList = new List<RailEle>();

        public float xScale = 1;
        public float yScale = 1;
        public Point ptTranslate = Point.Empty;
        public Int16 canvasMoveX = 0;
        public Int16 canvasMoveY = 0;
        public Point ptCanStrOffset = Point.Empty;
        public Rectangle rcRailEle = Rectangle.Empty;   //using for storing adjust rail rectangle
        private Size szShowPic = Size.Empty;

        public bool ReadRailSaveFile()
        {
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml("..//config//rails.xml");
            }
            catch(Exception exp)
            {
                Debug.WriteLine(string.Format("read xml error is {0}", exp));
            }
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                DataColumn dc = dt.Columns[0];
                if (dc.ColumnName == "GraphType")
                {
                    switch (dt.Rows[i][0].ToString())
                    {
                        case "1":
                            StraightEle strTemp = new StraightEle();
                            Int16 pointListVolStr = 0;
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                switch (dt.Columns[j].ColumnName)
                                {
                                    case "GraphType":
                                        strTemp.graphType = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "Speed":
                                        strTemp.speed = Convert.ToSingle(dt.Rows[i][j]);
                                        break;
                                    case "SegmentNumber":
                                        strTemp.segmentNumber = Convert.ToInt16(dt.Rows[i][j]);
                                        break;
                                    case "TagNumber":
                                        strTemp.tagNumber = Convert.ToInt16(dt.Rows[i][j]);
                                        break;
                                    case "Lenght":
                                        strTemp.lenght = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "StartAngle":
                                        strTemp.startAngle = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "StartDot":
                                        strTemp.startDot = Convert.ToString(dt.Rows[i][j]);
                                        break;
                                    case "PointListVol":
                                        pointListVolStr = Convert.ToInt16(dt.Rows[i][j]);
                                        for (int k = 0; k < pointListVolStr; k++)
                                        {
                                            string str = dt.Rows[i][j + k + 1].ToString();
                                            str = str.Substring(1, str.Length - 2);
                                            string[] strPointArray = str.Split(',');
                                            Point ptTemp = new Point() { X = int.Parse(strPointArray[0].Substring(2)), Y = int.Parse(strPointArray[1].Substring(2)) };
                                            strTemp.pointList.Add(ptTemp);
                                        }
                                        break;
                                    case "CodingBegin":
                                        strTemp.codingBegin = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "CodingEnd":
                                        strTemp.codingEnd = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                }
                            }
                            railInfoEleList.Add(strTemp);
                            break;
                        case "2":
                            CurvedEle curTemp = new CurvedEle();
                            string strcur = "";
                            string[] strPointArrayCur = { };
                            Point ptcur = Point.Empty;
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                switch (dt.Columns[j].ColumnName)
                                {
                                    case "GraphType":
                                        curTemp.graphType = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "Speed":
                                        curTemp.speed = Convert.ToSingle(dt.Rows[i][j]);
                                        break;
                                    case "SegmentNumber":
                                        curTemp.segmentNumber = Convert.ToInt16(dt.Rows[i][j]);
                                        break;
                                    case "TagNumber":
                                        curTemp.tagNumber = Convert.ToInt16(dt.Rows[i][j]);
                                        break;
                                    case "StartAngle":
                                        curTemp.startAngle = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "SweepAngle":
                                        curTemp.sweepAngle = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "Radiu":
                                        curTemp.radiu = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "Center":
                                        strcur = dt.Rows[i][j].ToString();
                                        strcur = strcur.Substring(1, strcur.Length - 2);
                                        strPointArrayCur = strcur.Split(',');
                                        ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                        curTemp.center = ptcur;
                                        break;
                                    case "FirstDot":
                                        strcur = dt.Rows[i][j].ToString();
                                        strcur = strcur.Substring(1, strcur.Length - 2);
                                        strPointArrayCur = strcur.Split(',');
                                        ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                        curTemp.firstDot = ptcur;
                                        break;
                                    case "SecDot":
                                        strcur = dt.Rows[i][j].ToString();
                                        strcur = strcur.Substring(1, strcur.Length - 2);
                                        strPointArrayCur = strcur.Split(',');
                                        ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                        curTemp.secDot = ptcur;
                                        break;
                                    case "CodingBegin":
                                        curTemp.codingBegin = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "CodingEnd":
                                        curTemp.codingEnd = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                }
                            }
                            railInfoEleList.Add(curTemp);
                            break;
                        case "3":
                            CrossEle croTemp = new CrossEle();
                            string strcro = "";
                            string[] strPointArrayCro = { };
                            Point ptcro = Point.Empty;
                            Int16 pointListVolCro = 0;
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                switch (dt.Columns[j].ColumnName)
                                {
                                    case "GraphType":
                                        croTemp.graphType = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "Speed":
                                        croTemp.speed = Convert.ToSingle(dt.Rows[i][j]);
                                        break;
                                    case "SegmentNumber":
                                        croTemp.segmentNumber = Convert.ToInt16(dt.Rows[i][j]);
                                        break;
                                    case "TagNumber":
                                        croTemp.tagNumber = Convert.ToInt16(dt.Rows[i][j]);
                                        break;
                                    case "FirstPart":
                                        croTemp.firstPart = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "SecPart":
                                        croTemp.secPart = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "ThPart":
                                        croTemp.thPart = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "FourPart":
                                        strcro = dt.Rows[i][j].ToString();
                                        strcro = strcro.Substring(1, strcro.Length - 2);
                                        strPointArrayCro = strcro.Split(',');
                                        ptcro = new Point() { X = int.Parse(strPointArrayCro[0].Substring(2)), Y = int.Parse(strPointArrayCro[1].Substring(2)) };
                                        croTemp.fourPart = ptcro;
                                        break;
                                    case "StartAngle":
                                        croTemp.startAngle = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "RotateAngle":
                                        croTemp.rotateAngle = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "PointListVol":
                                        pointListVolCro = Convert.ToInt16(dt.Rows[i][j]);
                                        for (Int16 k = 0; k < pointListVolCro; k++)
                                        {
                                            strcro = dt.Rows[i][j + k + 1].ToString();
                                            strcro = strcro.Substring(1, strcro.Length - 2);
                                            strPointArrayCro = strcro.Split(',');
                                            ptcro = new Point() { X = int.Parse(strPointArrayCro[0].Substring(2)), Y = int.Parse(strPointArrayCro[1].Substring(2)) };
                                            croTemp.pointList.Add(ptcro);
                                        }
                                        break;
                                    case "CodingBegin":
                                        croTemp.codingBegin = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                    case "CodingEnd":
                                        croTemp.codingEnd = Convert.ToInt32(dt.Rows[i][j]);
                                        break;
                                }
                            }
                            railInfoEleList.Add(croTemp);
                            break;
                    }
                }
            }
            return true;
        }

        public void InitRailList()
        {
            SetEleStartDot(railInfoEleList);
        }

        public void DrawRailInfo(Graphics canvas)
        {
            Pen pen = new Pen(Color.Black, 1);
            foreach (RailEle obj in railInfoEleList)
            {
                switch (obj.graphType)
                {
                    case 1:
                        StraightEle strTemp = (StraightEle)obj;
                        canvas.DrawLine(pen, strTemp.pointList[0], strTemp.pointList[1]);
                        break;
                    case 2:
                        CurvedEle curTemp = (CurvedEle)obj;
                        Rectangle rc = new Rectangle();
                        rc.Location = new Point(curTemp.center.X - curTemp.radiu, curTemp.center.Y - curTemp.radiu);
                        rc.Size = new Size(curTemp.radiu * 2, curTemp.radiu * 2);
                        GraphicsPath gp = new GraphicsPath();
                        gp.AddArc(rc, curTemp.startAngle, curTemp.sweepAngle);
                        canvas.DrawPath(pen, gp);
                        gp.Dispose();
                        break;
                    case 3:
                        CrossEle croTemp = (CrossEle)obj;
                        int n = croTemp.pointList.Count;
                        Point[] pts = new Point[2];
                        for (int i = 0; i < n; i++, i++)
                        {
                            pts[0] = croTemp.pointList[i];
                            pts[1] = croTemp.pointList[i + 1];
                            canvas.DrawLines(pen, pts);
                        }
                        break;
                    default:
                        break;
                }
            }
            pen.Dispose();
        }

        public void DrawVehicleInfo(Graphics canvas, Dictionary<uint, Vehicle> vList)
        {
            foreach (KeyValuePair<uint, Vehicle> item in vList)
            {
                Vehicle oht = item.Value;
                //obj.VehiclePosCoding = obj.tempTest.offsetOfText;
                //obj.PosCode += 50;

                if (oht.PosCode > 6100)
                {
                    //obj.VehiclePosCoding = 0;
                }

               // if (obj.VehiclePosCoding != -1)
                {
                    Pen pen = new Pen(Color.Red, 1);
                    Point carrierCoor = ComputeCoordinates(railCodingEleList, Convert.ToUInt32(oht.PosCode));
                    oht.ShowInScreen(canvas, carrierCoor);
                    pen.Dispose();
                }
            }
        }

        private Point ComputeCoordinates(List<RailEle> tempList, uint locationValue)
        {
            Point returnPt = Point.Empty;
            if (null == tempList)
            {
                return Point.Empty;
            }
            Int32 offsetTemp = 0;
            StraightEle strTemp = new StraightEle();
            CurvedEle curTemp = new CurvedEle();
            CrossEle croTemp = new CrossEle();
            Int32 listCount = tempList.Count;
            for (int i = 0; i < listCount; i++)
            {
                switch (tempList[i].graphType)
                {
                    case 1:
                        strTemp = (StraightEle)tempList[i];
                        if (locationValue > strTemp.codingBegin && locationValue <= strTemp.codingEnd)
                        {
                            offsetTemp = ((int)locationValue - strTemp.codingBegin) * strTemp.lenght / (strTemp.codingEnd - strTemp.codingBegin);
                            returnPt = strTemp.startPoint;
                            if (Math.Abs(strTemp.pointList[0].Y - strTemp.pointList[1].Y) < 3)
                            {
                                if (strTemp.startPoint.X < strTemp.endPoint.X)
                                    returnPt.X = strTemp.startPoint.X + offsetTemp;
                                else if (strTemp.startPoint.X > strTemp.endPoint.X)
                                    returnPt.X = strTemp.startPoint.X - offsetTemp;
                            }
                            else
                            {
                                if (strTemp.startPoint.Y < strTemp.endPoint.Y)
                                    returnPt.Y = strTemp.startPoint.Y + offsetTemp;
                                else if (strTemp.startPoint.Y > strTemp.endPoint.Y)
                                    returnPt.Y = strTemp.startPoint.Y - offsetTemp;
                            }
                            i = listCount;
                        }
                        break;
                    case 2:
                        curTemp = (CurvedEle)tempList[i];
                        break;
                    case 3:
                        croTemp = (CrossEle)tempList[i];
                        break;
                }
            }

            return returnPt;  
        }

        private void SetEleStartDot(List<RailEle> paraList)
        {
            List<RailEle> tempList = new List<RailEle>();
            StraightEle strTemp = new StraightEle();
            Int16 num = Convert.ToInt16(paraList.Count);
            for (Int16 i = 0; i < num; i++)
            {
                if (paraList[i].graphType == 1)
                {
                    strTemp = (StraightEle)paraList[i];
                    if (strTemp.startDot == "first dot")
                    {
                        paraList[i].startPoint = strTemp.pointList[0];
                        paraList[i].endPoint = strTemp.pointList[1];
                    }
                    else if (strTemp.startDot == "sec dot")
                    {
                        paraList[i].startPoint = strTemp.pointList[1];
                        paraList[i].endPoint = strTemp.pointList[0];
                    }
                    tempList.Add(paraList[i]);
                }
            }
        }

        public void AdjustRailSize(Size showPicSz)
        {
            Point ptTempMaxX = Point.Empty;
            Point ptTempMinX = Point.Empty;
            Point ptTempMaxY = Point.Empty;
            Point ptTempMinY = Point.Empty;
            Point pt = Point.Empty;
            szShowPic = showPicSz;
            switch (railInfoEleList[0].graphType)
            {
                case 1:
                    StraightEle strEle = (StraightEle)railInfoEleList[0];
                    pt = strEle.pointList[0];
                    break;
                case 2:
                    CurvedEle curEle = (CurvedEle)railInfoEleList[0];
                    pt = curEle.firstDot;
                    break;
                case 3:
                    CrossEle croEle = (CrossEle)railInfoEleList[0];
                    pt = croEle.pointList[0];
                    break;
            }

            Point ptMaxX = pt;
            Point ptMinX = pt;
            Point ptMaxY = pt;
            Point ptMinY = pt;
            foreach (RailEle obj in railInfoEleList)
            {
                switch (obj.graphType)
                {
                    case 1:
                        StraightEle strEle = (StraightEle)obj;
                        if (strEle.pointList[0].Y == strEle.pointList[1].Y)
                        {
                            if (strEle.pointList[0].X < strEle.pointList[1].X)
                            {
                                ptTempMinX = strEle.pointList[0];
                                ptTempMaxX = strEle.pointList[1];
                            }
                            else
                            {
                                ptTempMinX = strEle.pointList[1];
                                ptTempMaxX = strEle.pointList[0];
                            }
                        }
                        else if (strEle.pointList[0].X == strEle.pointList[1].X)
                        {
                            if (strEle.pointList[0].Y < strEle.pointList[1].Y)
                            {
                                ptTempMinY = strEle.pointList[0];
                                ptTempMaxY = strEle.pointList[1];
                            }
                            else
                            {
                                ptTempMinY = strEle.pointList[1];
                                ptTempMaxY = strEle.pointList[0];
                            }
                        }
                        break;
                    case 2:
                        CurvedEle curEle = (CurvedEle)obj;
                        if (curEle.firstDot.X < curEle.secDot.X)
                        {
                            ptTempMinX = curEle.firstDot;
                            ptTempMaxX = curEle.secDot;
                        }
                        else if (curEle.firstDot.X > curEle.secDot.X)
                        {
                            ptTempMinX = curEle.secDot;
                            ptTempMaxX = curEle.firstDot;
                        }
                        if(curEle.firstDot.Y<curEle.secDot.Y)
                        {
                            ptTempMinY = curEle.firstDot;
                            ptTempMaxY = curEle.secDot;
                        }
                        else if (curEle.firstDot.Y > curEle.secDot.Y)
                        {
                            ptTempMinY = curEle.secDot;
                            ptTempMaxY = curEle.firstDot;
                        }
                        break;
                    case 3:
                        CrossEle croEle = (CrossEle)obj;
                        if (croEle.pointList[0].Y == croEle.pointList[5].Y)
                        {
                            if (croEle.pointList[0].X < croEle.pointList[5].X)
                            {
                                ptTempMinX = croEle.pointList[0];
                                ptTempMaxX = croEle.pointList[5];
                            }
                            else if (croEle.pointList[0].X > croEle.pointList[5].X)
                            {
                                ptTempMinX = croEle.pointList[5];
                                ptTempMaxX = croEle.pointList[0];
                            }
                            if (croEle.pointList[3].Y < croEle.pointList[7].Y)
                            {
                                ptTempMinY = croEle.pointList[3];
                                ptTempMaxY = croEle.pointList[7];
                            }
                            else if (croEle.pointList[3].Y > croEle.pointList[7].Y)
                            {
                                ptTempMinY = croEle.pointList[7];
                                ptTempMaxY = croEle.pointList[3];
                            }
                        }
                        else if (croEle.pointList[0].X == croEle.pointList[5].X)
                        {
                            if (croEle.pointList[0].Y < croEle.pointList[5].Y)
                            {
                                ptTempMinY = croEle.pointList[0];
                                ptTempMaxY = croEle.pointList[5];
                            }
                            else if (croEle.pointList[0].Y > croEle.pointList[5].Y)
                            {
                                ptTempMinY = croEle.pointList[5];
                                ptTempMaxY = croEle.pointList[0];
                            }
                            if (croEle.pointList[3].X < croEle.pointList[7].X)
                            {
                                ptTempMinX = croEle.pointList[3];
                                ptTempMaxX = croEle.pointList[7];
                            }
                            else if (croEle.pointList[3].X > croEle.pointList[7].X)
                            {
                                ptTempMinX = croEle.pointList[7];
                                ptTempMaxX = croEle.pointList[3];
                            }
                        }
                        break;
                }
                if (ptMaxX.X < ptTempMaxX.X)
                {
                    ptMaxX = ptTempMaxX;
                }
                if (ptMinX.X > ptTempMinX.X)
                {
                    ptMinX = ptTempMinX;
                }
                if (ptMaxY.Y < ptTempMaxY.Y)
                {
                    ptMaxY = ptTempMaxY;
                }
                if (ptMinX.Y > ptTempMinY.Y)
                {
                    ptMinY = ptTempMinY;
                }
            }
            int railWidth = ptMaxX.X - ptMinX.X;
            int railHeight = ptMaxY.Y - ptMinY.Y;
            xScale = Convert.ToSingle((showPicSz.Width - 20) * 1.0 / railWidth);
            yScale = Convert.ToSingle((showPicSz.Height - 20) * 1.0 / railHeight);
            if (xScale < yScale)
            {
                yScale = xScale;
            }
            else
            {
                xScale = yScale;
            }
            ptTranslate = Point.Empty;
            ptTranslate.Offset(Convert.ToInt32(showPicSz.Width / 2 - (ptMinX.X + railWidth / 2) * xScale), Convert.ToInt32(showPicSz.Height / 2 - (ptMinY.Y + railHeight / 2) * xScale));
            rcRailEle.X = Convert.ToInt32(ptMinX.X * xScale + ptTranslate.X);//this is a error
            rcRailEle.Y = Convert.ToInt32(ptMinY.Y * yScale + ptTranslate.Y);//this is a error
            rcRailEle.Width = Convert.ToInt32(railWidth * xScale);
            rcRailEle.Height = Convert.ToInt32(railHeight * yScale);
        }

        public void CanvasTranslate(string direction,Int16 canvasOffset)
        {
            switch (direction)
            {
                case "up":
                    canvasMoveY -= canvasOffset;
                    break;
                case "down":
                    canvasMoveY += canvasOffset;
                    break;
                case "left":
                    canvasMoveX -= canvasOffset;
                    break;
                case "right":
                    canvasMoveX += canvasOffset;
                    break;
                case "center":
                    canvasMoveX = 0;
                    canvasMoveY = 0;
                    break;
            }
        }

        public void CanvasStretchOffset(float ratio)
        {
            Rectangle rc = new Rectangle();
            Point rcCenter = Point.Empty;
            ptCanStrOffset = Point.Empty;
            rc.X = Convert.ToInt32(rcRailEle.X * ratio);
            rc.Y = Convert.ToInt32(rcRailEle.Y * ratio);
            rc.Width = Convert.ToInt32(rcRailEle.Width * ratio);
            rc.Height = Convert.ToInt32(rcRailEle.Height * ratio);
            rcCenter.X = rc.X + rc.Width / 2;
            rcCenter.Y = rc.Y + rc.Height / 2;
            ptCanStrOffset.Offset(szShowPic.Width / 2 - rcCenter.X, szShowPic.Height / 2 - rcCenter.Y);
        }
    }


    public abstract class RailEle
    {
        public int graphType = 0;
        public float speed = 0;
        public Int16 segmentNumber = 0;
        public Int16 tagNumber = 0;
        public Point startPoint = Point.Empty;
        public Point endPoint = Point.Empty;
    }

    public class StraightEle : RailEle
    {
        public int lenght = 0;
        public int startAngle = 0;
        public string startDot = "";
        public List<Point> pointList = new List<Point>();
        public Int32 codingBegin = -1;
        public Int32 codingEnd = -1;
    }

    public class CurvedEle : RailEle
    {
        public int startAngle = 0;
        public int sweepAngle = 0;
        public int radiu = 0;
        public Point center = Point.Empty;
        public Point firstDot = Point.Empty;
        public Point secDot = Point.Empty;
        public Int32 codingBegin = -1;
        public Int32 codingEnd = -1;
    }

    public class CrossEle : RailEle
    {
        public int firstPart = 0;
        public int secPart = 0;
        public int thPart = 0;
        public Point fourPart = Point.Empty;
        public int startAngle = 0;
        public int rotateAngle = 0;
        public List<Point> pointList = new List<Point>();
        public Int32 codingBegin = -1;
        public Int32 codingEnd = -1;
    }
}
