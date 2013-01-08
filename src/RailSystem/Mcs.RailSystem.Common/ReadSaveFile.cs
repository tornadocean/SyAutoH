using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;

namespace Mcs.RailSystem.Common
{
    public class ReadSaveFile : BaseRailEle
    {
        DataTable dt = new DataTable();
        public void InitDataTable(DataTable dtSource)
        {
            dt = dtSource;
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
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        switch (dt.Columns[j].ColumnName)
                        {
                            case "GraphType":
                                line.GraphType = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "Speed":
                                line.Speed = Convert.ToSingle(dt.Rows[row][j]);
                                break;
                            case "Lenght":
                                line.Lenght = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "StartAngle":
                                line.StartAngle = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "PointListVol":
                                pointListVolStr = Convert.ToInt16(dt.Rows[row][j]);
                                for (int k = 0; k < pointListVolStr; k++)
                                {
                                    str = dt.Rows[row][j + k + 1].ToString();
                                    str = str.Substring(1, str.Length - 2);
                                    strPointArray = str.Split(',');
                                    ptTemp = new Point() { X = int.Parse(strPointArray[0].Substring(2)), Y = int.Parse(strPointArray[1].Substring(2)) };
                                    line.PointList.Add(ptTemp);
                                }
                                break;
                            case "DrawMultiFactor":
                                line.DrawMultiFactor = Convert.ToInt16(dt.Rows[row][j]);
                                break;
                            case "startPoint":
                                str = dt.Rows[row][j].ToString();
                                str = str.Substring(1, str.Length - 2);
                                strPointArray = str.Split(',');
                                ptTemp = new Point() { X = int.Parse(strPointArray[0].Substring(2)), Y = int.Parse(strPointArray[1].Substring(2)) };
                                line.StartPoint = ptTemp;
                                break;
                            case "endPoint":
                                str = dt.Rows[row][j].ToString();
                                str = str.Substring(1, str.Length - 2);
                                strPointArray = str.Split(',');
                                ptTemp = new Point() { X = int.Parse(strPointArray[0].Substring(2)), Y = int.Parse(strPointArray[1].Substring(2)) };
                                line.EndPoint = ptTemp;
                                break;
                            case "railText":
                                line.railText = dt.Rows[row][j].ToString();
                                break;
                            case "rotateAngle":
                                line.RotateAngle = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "CodingBegin":
                                line.CodingBegin = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "CodingEnd":
                                line.CodingEnd = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "CodingNext":
                                line.CodingNext = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "prevCoding":
                                line.CodingPrev = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "Color":
                                line.PenColor = ColorTranslator.FromHtml(dt.Rows[row][j].ToString());
                                break;
                            case "DashStyle":
                                line.PenDashStyle = (System.Drawing.Drawing2D.DashStyle)(Convert.ToInt32(dt.Rows[row][j]));
                                break;
                            case "PenWidth":
                                line.PenWidth = Convert.ToSingle(dt.Rows[row][j]);
                                break;
                        }
                    }
                    break;
                case 2:
                    EleCurve curve = (EleCurve)obj;
                    string strcur = "";
                    string[] strPointArrayCur = { };
                    Point ptcur = Point.Empty;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        switch (dt.Columns[j].ColumnName)
                        {
                            case "GraphType":
                                curve.GraphType = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "Speed":
                                curve.Speed = Convert.ToSingle(dt.Rows[row][j]);
                                break;
                            case "StartAngle":
                                curve.StartAngle = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "SweepAngle":
                                curve.SweepAngle = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "Radiu":
                                curve.Radiu = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "Center":
                                strcur = dt.Rows[row][j].ToString();
                                strcur = strcur.Substring(1, strcur.Length - 2);
                                strPointArrayCur = strcur.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.Center = ptcur;
                                break;
                            case "FirstDot":
                                strcur = dt.Rows[row][j].ToString();
                                strcur = strcur.Substring(1, strcur.Length - 2);
                                strPointArrayCur = strcur.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.FirstDot = ptcur;
                                break;
                            case "SecDot":
                                strcur = dt.Rows[row][j].ToString();
                                strcur = strcur.Substring(1, strcur.Length - 2);
                                strPointArrayCur = strcur.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.SecDot = ptcur;
                                break;
                            case "DirectionCurvedAttribute":
                                curve.DirectionCurvedAttribute = (Mcs.RailSystem.Common.EleCurve.DirectonCurved)Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "startPoint":
                                str = dt.Rows[row][j].ToString();
                                str = str.Substring(1, str.Length - 2);
                                strPointArrayCur = str.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.StartPoint = ptcur;
                                break;
                            case "endPoint":
                                str = dt.Rows[row][j].ToString();
                                str = str.Substring(1, str.Length - 2);
                                strPointArrayCur = str.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.EndPoint = ptcur;
                                break;
                            case "CodingBegin":
                                curve.CodingBegin = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "CodingEnd":
                                curve.CodingEnd = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "CodingNext":
                                curve.CodingNext = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "CodingPrev":
                                curve.CodingPrev = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "railText":
                                curve.railText = dt.Rows[row][j].ToString();
                                break;
                            case "rotateAngle":
                                curve.RotateAngle = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "oldRadiu":
                                curve.oldRadiu = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "oldCenter":
                                str = dt.Rows[row][j].ToString();
                                str = str.Substring(1, str.Length - 2);
                                strPointArrayCur = str.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.oldCenter = ptcur;
                                break;
                            case "oldFirstDot":
                                str = dt.Rows[row][j].ToString();
                                str = str.Substring(1, str.Length - 2);
                                strPointArrayCur = str.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.oldFirstDot = ptcur;
                                break;
                            case "oldSecDot":
                                str = dt.Rows[row][j].ToString();
                                str = str.Substring(1, str.Length - 2);
                                strPointArrayCur = str.Split(',');
                                ptcur = new Point() { X = int.Parse(strPointArrayCur[0].Substring(2)), Y = int.Parse(strPointArrayCur[1].Substring(2)) };
                                curve.oldSecDot = ptcur;
                                break;
                            case "Color":
                                curve.PenColor = ColorTranslator.FromHtml(dt.Rows[row][j].ToString());
                                break;
                            case "DashStyle":
                                curve.PenDashStyle = (System.Drawing.Drawing2D.DashStyle)(Convert.ToInt32(dt.Rows[row][j]));
                                break;
                            case "PenWidth":
                                curve.PenWidth = Convert.ToSingle(dt.Rows[row][j]);
                                break;
                        }
                    }
                    break;
                case 3:
                    EleCross cross =  (EleCross)obj;
                    string strcro = "";
                    string[] strPointArrayCro = { };
                    Point ptcro = Point.Empty;
                    Int16 pointListVolCro = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        switch (dt.Columns[j].ColumnName)
                        {
                            case "GraphType":
                                cross.GraphType = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "Speed":
                                cross.Speed = Convert.ToSingle(dt.Rows[row][j]);
                                break;
                            case "Mirror":
                                cross.Mirror = Convert.ToBoolean(dt.Rows[row][j]);
                                break;
                            case "FirstPart":
                                cross.FirstPart = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "SecPart":
                                cross.SecPart = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "ThPart":
                                cross.ThPart = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "FourPart":
                                strcro = dt.Rows[row][j].ToString();
                                strcro = strcro.Substring(1, strcro.Length - 2);
                                strPointArrayCro = strcro.Split(',');
                                ptcro = new Point() { X = int.Parse(strPointArrayCro[0].Substring(2)), Y = int.Parse(strPointArrayCro[1].Substring(2)) };
                                cross.FourPart = ptcro;
                                break;
                            case "StartAngle":
                                cross.StartAngle = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "RotateAngle":
                                cross.RotateAngle = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "DirectionOfCross":
                                cross.DirectionOfCross = (Mcs.RailSystem.Common.EleCross.DirectionCross)Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "PointListVol":
                                pointListVolCro = Convert.ToInt16(dt.Rows[row][j]);
                                for (Int16 k = 0; k < pointListVolCro; k++)
                                {
                                    strcro = dt.Rows[row][j + k + 1].ToString();
                                    strcro = strcro.Substring(1, strcro.Length - 2);
                                    strPointArrayCro = strcro.Split(',');
                                    ptcro = new Point() { X = int.Parse(strPointArrayCro[0].Substring(2)), Y = int.Parse(strPointArrayCro[1].Substring(2)) };
                                    cross.PointList.Add(ptcro);
                                }
                                break;
                            case "drawMultiFactor":
                                cross.DrawMultiFactor = Convert.ToInt16(dt.Rows[row][j]);
                                break;
                            case "startPoint":
                                strcro = dt.Rows[row][j].ToString();
                                strcro = strcro.Substring(1, strcro.Length - 2);
                                strPointArrayCro = strcro.Split(',');
                                ptcro = new Point() { X = int.Parse(strPointArrayCro[0].Substring(2)), Y = int.Parse(strPointArrayCro[1].Substring(2)) };
                                cross.StartPoint = ptcro;
                                break;
                            case "endPoint":
                                strcro = dt.Rows[row][j].ToString();
                                strcro = strcro.Substring(1, strcro.Length - 2);
                                strPointArrayCro = strcro.Split(',');
                                ptcro = new Point() { X = int.Parse(strPointArrayCro[0].Substring(2)), Y = int.Parse(strPointArrayCro[1].Substring(2)) };
                                cross.EndPoint = ptcro;
                                break;
                            case "CodingBegin":
                                cross.CodingBegin = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "CodingEnd":
                                cross.CodingEnd = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "CodingEndS":
                                cross.CodingEndS = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "CodingNext":
                                cross.CodingNext = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "CodingPrev":
                                cross.CodingPrev = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "CodingNextS":
                                cross.CodingNextS = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "railText":
                                cross.railText = dt.Rows[row][j].ToString();
                                break;
                            case "lenghtOfStrai":
                                cross.LenghtOfStrai = Convert.ToInt32(dt.Rows[row][j]);
                                break;
                            case "Color":
                                cross.PenColor = ColorTranslator.FromHtml(dt.Rows[row][j].ToString());
                                break;
                            case "DashStyle":
                                cross.PenDashStyle = (System.Drawing.Drawing2D.DashStyle)(Convert.ToInt32(dt.Rows[row][j]));
                                break;
                            case "PenWidth":
                                cross.PenWidth = Convert.ToSingle(dt.Rows[row][j]);
                                break;
                        }
                    }
                    break;
            }
        }

        
    }
}
