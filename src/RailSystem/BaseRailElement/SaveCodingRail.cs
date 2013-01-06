using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data;

namespace BaseRailElement
{
    public class SaveCodingRail
    {
        public List<BaseRailEle> railInfoEleList = new List<BaseRailEle>();
        public List<BaseRailEle> railCodingEleList = new List<BaseRailEle>();

        public DataSet ds = new DataSet();
        public DataTable dt = new DataTable("RailCodingEleTable");

        public void InitRailList(List<BaseRailEle> drawEleList)
        {
            //railInfoEleList.Clear();
            //railInfoEleList.AddRange(drawEleList);
            //List<BaseRailEle> tempList = GetStrEle(railInfoEleList);
            //railCodingEleList = ArrangeStrEle(tempList);
            //ComputerTagNumber();
            CreateCodingEleXml();
        }

        private void InitDataTable()
        {
            dt.Columns.Add("GraphType", typeof(int));
            dt.Columns.Add("CodingBegin", typeof(int));
            dt.Columns.Add("CodingEnd", typeof(int));
            dt.Columns.Add("CodingEndS", typeof(int));
            dt.Columns.Add("CodingNext", typeof(int));
            dt.Columns.Add("CodingNextS", typeof(int));
            dt.Columns.Add("CodingPrev", typeof(int));
        }

        private List<BaseRailEle> GetStrEle(List<BaseRailEle> paraList)
        {
            List<BaseRailEle> tempList = new List<BaseRailEle>();
            StraightRailEle strTemp = new StraightRailEle();
            CrossEle crossTemp = new CrossEle();
            Int16 num = Convert.ToInt16(paraList.Count);
            for (Int16 i = 0; i < num; i++)
            {
                if (paraList[i].GraphType == 1)
                {
                    strTemp = (StraightRailEle)paraList[i];
                    //if (strTemp.StartDot == "first dot")
                    //{
                    //    paraList[i].StartPoint = strTemp.PointList[0];
                    //    paraList[i].EndPoint = strTemp.PointList[1];
                    //}
                    //else if (strTemp.StartDot == "sec dot")
                    //{
                    //    paraList[i].StartPoint = strTemp.PointList[1];
                    //    paraList[i].EndPoint = strTemp.PointList[0];
                    //}
                    paraList[i].StartPoint = strTemp.PointList[0];
                    paraList[i].EndPoint = strTemp.PointList[1];
                    tempList.Add(paraList[i]);
                }
                else if (paraList[i].GraphType == 3)
                {
                    crossTemp = (CrossEle)paraList[i];
                    //if (crossTemp.StartDot == "first dot")
                    //{
                    //    paraList[i].StartPoint = crossTemp.PointList[0];
                    //    paraList[i].EndPoint = crossTemp.PointList[5];
                    //}
                    //else if (crossTemp.StartDot == "sec dot")
                    //{
                    //    paraList[i].StartPoint = crossTemp.PointList[5];
                    //    paraList[i].EndPoint = crossTemp.PointList[0];
                    //}
                    paraList[i].StartPoint = crossTemp.PointList[0];
                    paraList[i].EndPoint = crossTemp.PointList[5];
                }
            }
            return tempList;
        }

        private List<BaseRailEle> ArrangeStrEle(List<BaseRailEle> paraList)
        {
            List<BaseRailEle> tempList = new List<BaseRailEle>();
            //StraightRailEle strTemp = new StraightRailEle();
            //CrossEle crossTemp = new CrossEle();
            //Int16 num = Convert.ToInt16(paraList.Count);
            //for (Int16 i = 0; i < num; i++)
            //{
            //    for (Int16 j = 0; j < num; j++)
            //    {
            //        if (paraList[j].GraphType == 1)
            //        {
            //            strTemp = (StraightRailEle)paraList[j];
            //            if (strTemp.SegmentNumber == (i + 1))
            //            {
            //                tempList.Add(paraList[j]);
            //                break;
            //            }
            //        }
            //        else if (paraList[j].GraphType == 3)
            //        {
            //            crossTemp = (CrossEle)paraList[j];
            //            if (crossTemp.SegmentNumber == (i + 1))
            //            {
            //                tempList.Add(paraList[j]);
            //                break;
            //            }
            //        }
            //    }
            //}
            return tempList;
        }
        private void ComputerTagNumber()
        {
            //Int16 num =Convert.ToInt16(railCodingEleList.Count);
            //Int32 tempTagNum = -1;
            //for (Int16 i = 0; i < num; i++)
            //{
            //    if (railCodingEleList[i].GraphType == 1)
            //    {
            //        BaseRailElement.StraightRailEle str = (BaseRailElement.StraightRailEle)railCodingEleList[i];
            //        str.CodingBegin = tempTagNum + 1;
            //        tempTagNum = str.CodingBegin;
            //        str.CodingEnd = tempTagNum + railCodingEleList[i].TagNumber;
            //        tempTagNum = str.CodingEnd;
            //    }
            //}
        }

        private void CreateCodingEleXml()
        {
            //XmlDocument xmlDoc = new XmlDocument();
            //XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            //xmlDoc.AppendChild(xmlDecl);

            //XmlElement xmlEle;
            //xmlEle = xmlDoc.CreateElement("CodingEle");
            //xmlDoc.AppendChild(xmlEle);

            //Int16 num = Convert.ToInt16(railCodingEleList.Count());
            //for (Int16 i = 0; i < num; i++)
            //{
            //    XmlElement xmlName = xmlDoc.CreateElement("element");
            //    xmlEle.AppendChild(xmlName);
            //    if (railCodingEleList[i].GraphType == 1)
            //    {
            //        XmlElement xmlStrai = xmlDoc.CreateElement("类型标识符");
            //        XmlElement xmlSeg = xmlDoc.CreateElement("段号");
            //        XmlElement xmlStartCoding = xmlDoc.CreateElement("起点值");
            //        XmlElement xmlEndCoding = xmlDoc.CreateElement("终点值");
            //        XmlElement xmlNext = xmlDoc.CreateElement("next");
            //        XmlElement xmlprev = xmlDoc.CreateElement("prev");

            //        xmlStrai.InnerText = "0";
            //        xmlSeg.InnerText = railCodingEleList[i].SegmentNumber.ToString();
            //        xmlStartCoding.InnerText = railCodingEleList[i].StartCoding.ToString();
            //        xmlEndCoding.InnerText = railCodingEleList[i].EndCoding.ToString();
            //        if (i != (num - 1))
            //        {
            //            xmlNext.InnerText = railCodingEleList[i + 1].StartCoding.ToString();
            //        }
            //        if (i != 0)
            //        {
            //            xmlprev.InnerText = railCodingEleList[i - 1].EndCoding.ToString();
            //        }

            //        xmlName.AppendChild(xmlStrai);
            //        xmlName.AppendChild(xmlSeg);
            //        xmlName.AppendChild(xmlStartCoding);
            //        xmlName.AppendChild(xmlEndCoding);
            //        xmlName.AppendChild(xmlNext);
            //        xmlName.AppendChild(xmlprev);
            //    }
            //    else if (railCodingEleList[i].GraphType == 3)
            //    {
            //        XmlElement xmlStrai = xmlDoc.CreateElement("类型标识符");
            //        XmlElement xmlSeg = xmlDoc.CreateElement("段号");
            //        XmlElement xmlStartCoding = xmlDoc.CreateElement("起点值");
            //        XmlElement xmlEndCoding = xmlDoc.CreateElement("终点值");
            //        XmlElement xmlThirdCoding = xmlDoc.CreateElement("第三点");
            //        XmlElement xmlNext = xmlDoc.CreateElement("next");
            //        XmlElement xmlprev = xmlDoc.CreateElement("prev");

            //        xmlStrai.InnerText = "1";
            //        xmlSeg.InnerText = railCodingEleList[i].SegmentNumber.ToString();
            //        xmlStartCoding.InnerText = xmlStrai.InnerText + xmlSeg.InnerText + "1";
            //        xmlEndCoding.InnerText = xmlStrai.InnerText + xmlSeg.InnerText + "2";
            //        xmlThirdCoding.InnerText = xmlStrai.InnerText + xmlSeg.InnerText + "3";
            //        if (i != (num - 1))
            //        {
            //            xmlNext.InnerText = railCodingEleList[i + 1].StartCoding.ToString();
            //        }
            //        if (i != 0)
            //        {
            //            xmlprev.InnerText = railCodingEleList[i - 1].EndCoding.ToString();
            //        }

            //        xmlName.AppendChild(xmlStrai);
            //        xmlName.AppendChild(xmlSeg);
            //        xmlName.AppendChild(xmlStartCoding);
            //        xmlName.AppendChild(xmlEndCoding);
            //        xmlName.AppendChild(xmlThirdCoding);
            //        xmlName.AppendChild(xmlNext);
            //        xmlName.AppendChild(xmlprev);
            //    }
            //}

            //xmlDoc.Save(@"..\..\..\..\..\bin\config\rails_coding.xml");
            //xmlDoc = null;
        }
    }
}