﻿using System;
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
        public List<Mcs.RailSystem.Common.BaseRailEle> railInfoEleList = new List<Mcs.RailSystem.Common.BaseRailEle>();

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
            Mcs.RailSystem.Common.DrawDoc doc = new Mcs.RailSystem.Common.DrawDoc();
            doc.InitDataTable(dt);
            try 
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (Convert.ToInt16(dt.Rows[i][0]))
                    {
                        case 1:
                            StraightEle line = new StraightEle();
                            doc.ReadDataFromRow(i, line);
                            railInfoEleList.Add(line);
                            break;
                        case 2:
                            CurvedEle curve = new CurvedEle();
                            doc.ReadDataFromRow(i, curve);
                            railInfoEleList.Add(curve);
                            break;
                        case 3:
                            CrossEle cross = new CrossEle();
                            doc.ReadDataFromRow(i, cross);
                            railInfoEleList.Add(cross);
                            break;
                        case 5:
                            FoupDot foupDot = new FoupDot();
                            doc.ReadDataFromRow(i, foupDot);
                            railInfoEleList.Add(foupDot);
                            break;
                        case 6:
                            Device device = new Device();
                            doc.ReadDataFromRow(i, device);
                            railInfoEleList.Add(device);
                            break;
                    }
                }
            }
            catch 
            {
                MessageBox.Show("this is a error when open xml save file");
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
            foreach (Mcs.RailSystem.Common.BaseRailEle obj in railInfoEleList)
            {
                switch (obj.GraphType)
                {
                    case 1:
                        StraightEle strTemp = (StraightEle)obj;
                        pen.Width = strTemp.PenWidth;
                        canvas.DrawLine(pen, strTemp.PointList[0], strTemp.PointList[1]);
                        break;
                    case 2:
                        CurvedEle curTemp = (CurvedEle)obj;
                        Rectangle rc = new Rectangle();
                        rc.Location = new Point(curTemp.Center.X - curTemp.Radiu, curTemp.Center.Y - curTemp.Radiu);
                        rc.Size = new Size(curTemp.Radiu * 2, curTemp.Radiu * 2);
                        GraphicsPath gp = new GraphicsPath();
                        gp.AddArc(rc, curTemp.StartAngle, curTemp.SweepAngle);
                        pen.Width = curTemp.PenWidth;
                        canvas.DrawPath(pen, gp);
                        gp.Dispose();
                        break;
                    case 3:
                        CrossEle croTemp = (CrossEle)obj;
                        int n = croTemp.PointList.Count;
                        Point[] pts = new Point[n];
                        for (int i = 0; i < n; i++)
                        {
                            pts[i] = croTemp.PointList[i];
                        }
                        pen.Width = croTemp.PenWidth;
                        canvas.DrawLine(pen, pts[0], pts[2]);
                        pen.DashStyle = DashStyle.Dot;
                        canvas.DrawLine(pen, pts[0], pts[1]);
                        pen.DashStyle = DashStyle.Solid;
                        break;
                    case 5:
                        FoupDot dotTemp = (FoupDot)obj;
                        Rectangle rcDot = new Rectangle()
                        {
                            X = dotTemp.PtScratchDotIcon.X,
                            Y = dotTemp.PtScratchDotIcon.Y,
                            Width = dotTemp.IconWidth,
                            Height = dotTemp.IconHeight
                        };
                        canvas.DrawImage(dotTemp.ImageFoupWayIcon, rcDot);
                        rcDot.X = dotTemp.PtScratchDot.X;
                        rcDot.Y = dotTemp.PtScratchDot.Y;
                        rcDot.Width = dotTemp.RcFoupDot.Width;
                        rcDot.Height = dotTemp.RcFoupDot.Height;
                        pen.Width = dotTemp.PenWidth;
                        canvas.DrawRectangle(pen, rcDot);
                        break;
                    case 6:
                        Device deviceTemp = (Device)obj;
                        Rectangle rcDevice = new Rectangle()
                        {
                            X = deviceTemp.PtDevice.X,
                            Y = deviceTemp.PtDevice.Y,
                            Width = deviceTemp.WidthIcon,
                            Height = deviceTemp.HeightIcon
                        };
                        canvas.DrawImage(deviceTemp.ImageDevice, rcDevice);
                        if (deviceTemp.Stocker)
                        {
                            pen.Width = 1;
                            canvas.DrawRectangle(pen, deviceTemp.RcStockerRoom);
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
                    Point carrierCoor = ComputeCoordinates(railInfoEleList, Convert.ToUInt32(oht.PosCode));
                    oht.ShowInScreen(canvas, carrierCoor);
                    pen.Dispose();
                }
            }
        }

        private Point ComputeCoordinates(List<Mcs.RailSystem.Common.BaseRailEle> tempList, uint locationValue)
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
                switch (tempList[i].GraphType)
                {
                    case 1:
                        strTemp = (StraightEle)tempList[i];
                        if (locationValue > strTemp.CodingBegin && locationValue <= strTemp.CodingEnd)
                        {
                            offsetTemp = ((int)locationValue - strTemp.CodingBegin) * strTemp.Lenght / (strTemp.CodingEnd - strTemp.CodingBegin);
                            returnPt = strTemp.DotStart;
                            if (Math.Abs(strTemp.PointList[0].Y - strTemp.PointList[1].Y) < 3)
                            {
                                if (strTemp.DotStart.X < strTemp.DotEnd.X)
                                    returnPt.X = strTemp.DotStart.X + offsetTemp;
                                else if (strTemp.DotStart.X > strTemp.DotEnd.X)
                                    returnPt.X = strTemp.DotStart.X - offsetTemp;
                            }
                            else
                            {
                                if (strTemp.DotStart.Y < strTemp.DotEnd.Y)
                                    returnPt.Y = strTemp.DotStart.Y + offsetTemp;
                                else if (strTemp.DotStart.Y > strTemp.DotEnd.Y)
                                    returnPt.Y = strTemp.DotStart.Y - offsetTemp;
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

        private void SetEleStartDot(List<Mcs.RailSystem.Common.BaseRailEle> paraList)
        {
            List<Mcs.RailSystem.Common.BaseRailEle> tempList = new List<Mcs.RailSystem.Common.BaseRailEle>();
            StraightEle strTemp = new StraightEle();
            Int16 num = Convert.ToInt16(paraList.Count);
            for (Int16 i = 0; i < num; i++)
            {
                if (paraList[i].GraphType == 1)
                {
                    strTemp = (StraightEle)paraList[i];
                    paraList[i].DotStart = strTemp.PointList[0];
                    paraList[i].DotEnd = strTemp.PointList[1];
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
            switch (railInfoEleList[0].GraphType)
            {
                case 1:
                    StraightEle strEle = (StraightEle)railInfoEleList[0];
                    pt = strEle.PointList[0];
                    break;
                case 2:
                    CurvedEle curEle = (CurvedEle)railInfoEleList[0];
                    pt = curEle.FirstDot;
                    break;
                case 3:
                    CrossEle croEle = (CrossEle)railInfoEleList[0];
                    pt = croEle.PointList[0];
                    break;
            }

            Point ptMaxX = pt;
            Point ptMinX = pt;
            Point ptMaxY = pt;
            Point ptMinY = pt;
            foreach (Mcs.RailSystem.Common.BaseRailEle obj in railInfoEleList)
            {
                switch (obj.GraphType)
                {
                    case 1:
                        StraightEle strEle = (StraightEle)obj;
                        if (strEle.PointList[0].Y == strEle.PointList[1].Y)
                        {
                            if (strEle.PointList[0].X < strEle.PointList[1].X)
                            {
                                ptTempMinX = strEle.PointList[0];
                                ptTempMaxX = strEle.PointList[1];
                            }
                            else
                            {
                                ptTempMinX = strEle.PointList[1];
                                ptTempMaxX = strEle.PointList[0];
                            }
                        }
                        else if (strEle.PointList[0].X == strEle.PointList[1].X)
                        {
                            if (strEle.PointList[0].Y < strEle.PointList[1].Y)
                            {
                                ptTempMinY = strEle.PointList[0];
                                ptTempMaxY = strEle.PointList[1];
                            }
                            else
                            {
                                ptTempMinY = strEle.PointList[1];
                                ptTempMaxY = strEle.PointList[0];
                            }
                        }
                        break;
                    case 2:
                        CurvedEle curEle = (CurvedEle)obj;
                        if (curEle.FirstDot.X < curEle.SecDot.X)
                        {
                            ptTempMinX = curEle.FirstDot;
                            ptTempMaxX = curEle.SecDot;
                        }
                        else if (curEle.FirstDot.X > curEle.SecDot.X)
                        {
                            ptTempMinX = curEle.SecDot;
                            ptTempMaxX = curEle.FirstDot;
                        }
                        if (curEle.FirstDot.Y < curEle.SecDot.Y)
                        {
                            ptTempMinY = curEle.FirstDot;
                            ptTempMaxY = curEle.SecDot;
                        }
                        else if (curEle.FirstDot.Y > curEle.SecDot.Y)
                        {
                            ptTempMinY = curEle.SecDot;
                            ptTempMaxY = curEle.FirstDot;
                        }
                        break;
                    case 3:
                        CrossEle croEle = (CrossEle)obj;
                        if (croEle.PointList[0].Y == croEle.PointList[1].Y)
                        {
                            if (croEle.PointList[0].X < croEle.PointList[1].X)
                            {
                                ptTempMinX = croEle.PointList[0];
                                ptTempMaxX = croEle.PointList[1];
                            }
                            else if (croEle.PointList[0].X > croEle.PointList[1].X)
                            {
                                ptTempMinX = croEle.PointList[1];
                                ptTempMaxX = croEle.PointList[0];
                            }
                            if (croEle.PointList[0].Y < croEle.PointList[2].Y)
                            {
                                ptTempMinY = croEle.PointList[0];
                                ptTempMaxY = croEle.PointList[2];
                            }
                            else if (croEle.PointList[0].Y > croEle.PointList[2].Y)
                            {
                                ptTempMinY = croEle.PointList[2];
                                ptTempMaxY = croEle.PointList[0];
                            }
                        }
                        else if (croEle.PointList[0].X == croEle.PointList[1].X)
                        {
                            if (croEle.PointList[0].Y < croEle.PointList[1].Y)
                            {
                                ptTempMinY = croEle.PointList[0];
                                ptTempMaxY = croEle.PointList[1];
                            }
                            else if (croEle.PointList[0].Y > croEle.PointList[1].Y)
                            {
                                ptTempMinY = croEle.PointList[1];
                                ptTempMaxY = croEle.PointList[0];
                            }
                            if (croEle.PointList[0].X < croEle.PointList[2].X)
                            {
                                ptTempMinX = croEle.PointList[0];
                                ptTempMaxX = croEle.PointList[2];
                            }
                            else if (croEle.PointList[0].X > croEle.PointList[2].X)
                            {
                                ptTempMinX = croEle.PointList[2];
                                ptTempMaxX = croEle.PointList[0];
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
            xScale = Convert.ToSingle((showPicSz.Width - 40) * 1.0 / railWidth);
            yScale = Convert.ToSingle((showPicSz.Height - 40) * 1.0 / railHeight);
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


    public class StraightEle : Mcs.RailSystem.Common.EleLine
    {
        public StraightEle() { GraphType = 1; }
    }

    public class CurvedEle : Mcs.RailSystem.Common.EleCurve
    {
        public CurvedEle() { GraphType = 2; }
    }

    public class CrossEle : Mcs.RailSystem.Common.EleCross 
    {
        public CrossEle() { GraphType = 3; }
    }

    public class FoupDot : Mcs.RailSystem.Common.EleFoupDot
    {
        public FoupDot() { GraphType = 5; }
    }

    public class Device : Mcs.RailSystem.Common.EleDevice 
    {
        public Device() { GraphType = 6; }
    }
}
