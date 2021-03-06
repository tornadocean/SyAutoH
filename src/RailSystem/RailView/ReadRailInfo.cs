﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Drawing;

namespace RailView
{
    public class ReadRailInfo
    {
        public List<RailEle> railInfoEleList = new List<RailEle>();
        public List<RailEle> OpenFile()
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load("..//config//rails.xml");
            }
            catch 
            {
                MessageBox.Show("文件不存在，请打开组态软件进行配置");
                return railInfoEleList;
            }
            XmlNode root = xmlDoc.SelectSingleNode("DrawDoc");
            XmlNodeList rootNodeList = xmlDoc.SelectSingleNode("DrawDoc").ChildNodes;
            foreach (XmlNode rxn in rootNodeList)
            {
                XmlElement rxe = (XmlElement)rxn;
                if (rxe.Name == "DrawObjectList")
                {
                    XmlNodeList childNodeList = rxe.ChildNodes;
                    foreach (XmlNode cxn in childNodeList)
                    {
                        XmlElement cxe = (XmlElement)cxn;
                        XmlNodeList cTwoNodeList=null;
                        XmlNodeList cThNodeList = null;
                        XmlNodeList cFNodeList = null;
                        Point pt = Point.Empty;
                        switch (cxe.GetAttribute("xsi:type"))
                        {
                            case "StraightRailEle":
                                StraightEle strTemp = new StraightEle();
                                cTwoNodeList = cxe.ChildNodes;                               
                                foreach (XmlNode ctwoxn in cTwoNodeList)
                                {
                                    XmlElement ctwoxe=(XmlElement)ctwoxn;
                                    if (ctwoxe.Name == "GraphType")
                                        strTemp.graphType = int.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "Speed")
                                        strTemp.speed = float.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "SegmentNumber")
                                        strTemp.segmentNumber = Int16.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "TagNumber")
                                        strTemp.tagNumber = Convert.ToInt16(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "Lenght")
                                        strTemp.lenght = int.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "StartAngle")
                                        strTemp.startAngle = int.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "PointList")
                                    {
                                        cThNodeList = ctwoxe.ChildNodes;
                                        foreach (XmlNode cthxn in cThNodeList)
                                        {
                                            XmlElement cthxe = (XmlElement)cthxn;
                                            if (cthxe.Name == "Point")
                                            {
                                                cFNodeList = cthxe.ChildNodes;
                                                foreach (XmlNode cfxn in cFNodeList)
                                                {
                                                    XmlElement cfxe = (XmlElement)cfxn;
                                                    if (cfxe.Name == "X")
                                                        pt.X = int.Parse(cfxe.InnerText);
                                                    else if (cfxe.Name == "Y")
                                                        pt.Y = int.Parse(cfxe.InnerText);
                                                }
                                                strTemp.pointList.Add(pt);
                                            }
                                        }
                                    }
                                }
                                railInfoEleList.Add(strTemp);
                                break;
                            case "CurvedRailEle":
                                CurvedEle curTemp = new CurvedEle();
                                cTwoNodeList = cxn.ChildNodes;                                
                                foreach(XmlNode ctwoxn in cTwoNodeList)
                                {
                                    XmlElement ctwoxe=(XmlElement)ctwoxn;
                                    if (ctwoxe.Name == "GraphType")
                                        curTemp.graphType = int.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "speed")
                                        curTemp.speed = float.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "SegmentNumber")
                                        curTemp.segmentNumber = Convert.ToInt16(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "TagNumber")
                                        curTemp.tagNumber = Int16.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "StartAngle")
                                        curTemp.startAngle = int.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "SweepAngle")
                                        curTemp.sweepAngle = int.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "Radiu")
                                        curTemp.radiu = int.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "Center")
                                    {
                                        cThNodeList = ctwoxe.ChildNodes;
                                        foreach (XmlNode cthxn in cThNodeList)
                                        {
                                            XmlElement cthxe = (XmlElement)cthxn;
                                            if (cthxe.Name == "X")
                                                curTemp.center.X = int.Parse(cthxe.InnerText);
                                            else if (cthxe.Name == "Y")
                                                curTemp.center.Y = int.Parse(cthxe.InnerText);
                                        }
                                    }
                                    else if (ctwoxn.Name == "FirstDot")
                                    {
                                        cThNodeList = ctwoxn.ChildNodes;
                                        foreach (XmlNode cthxn in cThNodeList)
                                        {
                                            XmlElement cthxe = (XmlElement)cthxn;
                                            if (cthxe.Name == "X")
                                                curTemp.firstDot.X = int.Parse(cthxe.InnerText);
                                            else if (cthxe.Name == "Y")
                                                curTemp.firstDot.Y = int.Parse(cthxe.InnerText);
                                        }
                                    }
                                    else if (ctwoxn.Name == "SecDot")
                                    {
                                        cThNodeList = ctwoxn.ChildNodes;
                                        foreach (XmlNode cthxn in cThNodeList)
                                        {
                                            XmlElement cthxe = (XmlElement)cthxn;
                                            if (cthxe.Name == "X")
                                                curTemp.secDot.X = int.Parse(cthxe.InnerText);
                                            else if (cthxe.Name == "Y")
                                                curTemp.secDot.Y = int.Parse(cthxe.InnerText);
                                        }
                                    }
                                }
                                railInfoEleList.Add(curTemp);
                                break;
                            case "CrossEle":
                                CrossEle croTemp = new CrossEle();
                                cTwoNodeList = cxn.ChildNodes;
                                foreach (XmlNode ctwoxn in cTwoNodeList)
                                {
                                    XmlElement ctwoxe = (XmlElement)ctwoxn;
                                    if (ctwoxe.Name == "GraphType")
                                        croTemp.graphType = int.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "Speed")
                                        croTemp.speed = float.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "SegmentNumber")
                                        croTemp.segmentNumber = Convert.ToInt16(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "TagNumber")
                                        croTemp.tagNumber = Int16.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "FirstPart")
                                        croTemp.firstPart = int.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "SecPart")
                                        croTemp.secPart = int.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "ThPart")
                                        croTemp.thPart = int.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "StartAngle")
                                        croTemp.startAngle = int.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "RotateAngle")
                                        croTemp.rotateAngle = int.Parse(ctwoxe.InnerText);
                                    else if (ctwoxe.Name == "FourPart")
                                    {
                                        cThNodeList = ctwoxe.ChildNodes;
                                        foreach (XmlNode cthxn in cThNodeList)
                                        {
                                            XmlElement cthxe = (XmlElement)cthxn;
                                            if (cthxe.Name == "X")
                                                croTemp.fourPart.X = int.Parse(cthxe.InnerText);
                                            else if (cthxe.Name == "Y")
                                                croTemp.fourPart.Y = int.Parse(cthxe.InnerText);
                                        }
                                    }
                                    else if (ctwoxe.Name == "PointList")
                                    {
                                        cThNodeList = ctwoxe.ChildNodes;
                                        foreach (XmlNode cthxn in cThNodeList)
                                        {
                                            XmlElement cthxe = (XmlElement)cthxn;
                                            if (cthxe.Name == "Point")
                                            {
                                                cFNodeList = cthxe.ChildNodes;
                                                foreach (XmlNode cfxn in cFNodeList)
                                                {
                                                    XmlElement cfxe = (XmlElement)cfxn;
                                                    if (cfxe.Name == "X")
                                                        pt.X = int.Parse(cfxe.InnerText);
                                                    else if (cfxe.Name == "Y")
                                                        pt.Y = int.Parse(cfxe.InnerText);
                                                }
                                                croTemp.pointList.Add(pt);
                                            }
                                        }
                                    }
                                }
                                railInfoEleList.Add(croTemp);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return railInfoEleList;
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
        public List<Point> pointList = new List<Point>();
    }

    public class CurvedEle : RailEle
    {
        public int startAngle = 0;
        public int sweepAngle = 0;
        public int radiu = 0;
        public Point center = Point.Empty;
        public Point firstDot = Point.Empty;
        public Point secDot = Point.Empty;
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
    }
}
