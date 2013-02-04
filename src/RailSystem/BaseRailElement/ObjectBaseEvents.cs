using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Diagnostics;

namespace BaseRailElement
{
    public class ObjectBaseEvents : BaseEvents
    {
        protected static DrawDocOp documentOp = DrawDocOp.EmptyDocument;
        public static DrawDocOp DocumentOp
        {
            get { return documentOp; }
            set { documentOp = value; }
        }

        public enum SelectObject
        {
            SelectEle, SelectHandle, SelectNone
        }
        private SelectObject selectObject = SelectObject.SelectNone;

        private int _hit = -1;

        public enum Direction
        {
            up, down, left, right, Null
        }
        public Direction direction = Direction.Null;

        private bool mouseLDown = false;
        public bool MouseLDown
        {
            get { return mouseLDown; }
            set { mouseLDown = value; }
        }
        private bool mouseRDown = false;
        public bool MouseRDown
        {
            get { return mouseRDown; }
            set { mouseRDown = value; }
        }
        private bool mouseLMove = false;
        public bool MouseLMove
        {
            get { return mouseLMove; }
            set { mouseLMove = value; }
        }
        private bool mouseRMove = false;
        public bool MouseRMove
        {
            get { return mouseRMove; }
            set { mouseRMove = value; }
        }

        public ObjectBaseEvents()
        {
        }

        public override void OnLButtonDown(Point point)
        {
            base.OnLButtonDown(point);
            if (drawToolType == 4)
            {
                int hit = documentOp.HitTest(point, false);
                _hit = hit;
                if (hit > 0)
                    selectObject = SelectObject.SelectHandle;
                else if (hit == 0)
                    selectObject = SelectObject.SelectEle;
                else
                    selectObject = SelectObject.SelectNone;
                documentOp.ChangeChooseSign(true, point);
            }
            else if (4 != drawToolType)
            {
                BaseRailElement.RailAuxiliaryDraw auxDraw = new RailAuxiliaryDraw();
                auxDraw.CreateEle(drawToolType, documentOp.DrawMultiFactor, point);
                documentOp.ListAuxiliaryDraw.Add(auxDraw);
                documentOp.LastHitedObject = auxDraw;
            }
        }

        public override bool OnRButtonDown(Point point)
        {
            base.OnRButtonDown(point);
            if (4 == drawToolType)
            {
            int hit = documentOp.HitTest(point, false);
            if (hit >= 0)
                return true;
            else
                return false;
            }
            else if (3 == drawToolType &&bMouseLDown && downPoint == lastPoint)
            {
                BaseRailElement.RailAuxiliaryDraw obj = (BaseRailElement.RailAuxiliaryDraw)documentOp.LastHitedObject;
                if (obj.PtsCurve[0] != lastPoint && obj.PtsCurve[0] == obj.PtsCurve[1])
                {
                    obj.PtsCurve[1] = point;
                }
                else if (obj.PtsCurve[0] != lastPoint && obj.PtsCurve[0] == obj.PtsCurve[2])
                {
                    obj.PtsCurve[2] = point;
                }
            }
            return false;
        }

        public override void OnLButtonUp(Point point)
        {
            base.OnLButtonUp(point);
            if (4 == drawToolType)
            {
                if (selectObject == SelectObject.SelectEle && hasContact)
                {
                    hasContact = false;
                    documentOp.SelectedDrawObjectList[0].Move(contactStartDot, contactEndDot);
                    documentOp.SelectedDrawObjectList[0].TrackerColor = Color.Blue;
                }
                documentOp.ChangeChooseSign(false, point);
            }
            else if ((0 == drawToolType ||1 == drawToolType)
                && !((BaseRailElement.RailAuxiliaryDraw)documentOp.LastHitedObject).BFinish)
            {
                documentOp.LastHitedObject.DotEnd = point;
                ((BaseRailElement.RailAuxiliaryDraw)documentOp.LastHitedObject).BFinish = true;
                documentOp.SelectedDrawObjectList.Clear();
                documentOp.SelectedDrawObjectList.Add((BaseRailElement.RailAuxiliaryDraw)documentOp.LastHitedObject);
            }
            else if (2 == drawToolType && !((BaseRailElement.RailAuxiliaryDraw)documentOp.LastHitedObject).BFinish)
            {
                documentOp.LastHitedObject.DotEnd = point;
                ((BaseRailElement.RailAuxiliaryDraw)documentOp.LastHitedObject).BFinish = true;
                documentOp.SelectedDrawObjectList.Clear();
                documentOp.SelectedDrawObjectList.Add((BaseRailElement.RailAuxiliaryDraw)documentOp.LastHitedObject);
            }
            else if (3 == drawToolType && !((BaseRailElement.RailAuxiliaryDraw)documentOp.LastHitedObject).BFinish)
            {
                ((BaseRailElement.RailAuxiliaryDraw)documentOp.LastHitedObject).BFinish = true;
                documentOp.SelectedDrawObjectList.Clear();
                documentOp.SelectedDrawObjectList.Add((BaseRailElement.RailAuxiliaryDraw)documentOp.LastHitedObject);
            }


        }

        public override void OnMouseMoveLeft(Point point)
        {
            if (4 == drawToolType)
            {
                int dx = point.X - lastPoint.X;
                int dy = point.Y - lastPoint.Y;
                int n = documentOp.SelectedDrawObjectList.Count;
                int tempDrawMultiFactor = 1;
                switch (selectObject)
                {
                    case SelectObject.SelectHandle:
                        tempDrawMultiFactor = documentOp.SelectedDrawObjectList[0].DrawMultiFactor;
                        if ((dx != 0 && dx / tempDrawMultiFactor != 0) || (dy != 0 && dy / tempDrawMultiFactor != 0))
                        {
                            if (1 == documentOp.SelectedDrawObjectList[0].GraphType
                                || 2 == documentOp.SelectedDrawObjectList[0].GraphType
                                || 3 == documentOp.SelectedDrawObjectList[0].GraphType
                                || 4 == documentOp.SelectedDrawObjectList[0].GraphType
                                || 5 == documentOp.SelectedDrawObjectList[0].GraphType
                                || 6 == documentOp.SelectedDrawObjectList[0].GraphType
                                || 7 == documentOp.SelectedDrawObjectList[0].GraphType)
                            {
                                if (n == 1)
                                {
                                    documentOp.SelectedDrawObjectList[0].MoveHandle(_hit, lastPoint, point);
                                }
                            }
                            else if (documentOp.SelectedDrawObjectList[0].GraphType == 4)
                            {
                                if (n == 1)
                                {
                                    documentOp.SelectedDrawObjectList[0].MoveHandle(_hit, lastPoint, point);
                                }
                            }
                            lastPoint.Offset(dx / tempDrawMultiFactor * tempDrawMultiFactor, dy / tempDrawMultiFactor * tempDrawMultiFactor);
                        }
                        break;
                    case SelectObject.SelectEle:
                        tempDrawMultiFactor = documentOp.SelectedDrawObjectList[0].DrawMultiFactor;
                        if ((dx != 0 && dx / tempDrawMultiFactor != 0) || (dy != 0 && dy / tempDrawMultiFactor != 0))
                        {
                            for (int i = 0; i < n; i++)
                            {
                                if (1 == documentOp.SelectedDrawObjectList[i].GraphType
                                    || 2 == documentOp.SelectedDrawObjectList[i].GraphType
                                    || 3 == documentOp.SelectedDrawObjectList[i].GraphType
                                    || 4 == documentOp.SelectedDrawObjectList[0].GraphType
                                    || 5 == documentOp.SelectedDrawObjectList[i].GraphType
                                    || 6 == documentOp.SelectedDrawObjectList[i].GraphType
                                    || 7 == documentOp.SelectedDrawObjectList[0].GraphType)
                                {
                                    documentOp.SelectedDrawObjectList[i].Move(lastPoint, point);
                                }
                            }
                            lastPoint.Offset(dx / tempDrawMultiFactor * tempDrawMultiFactor, dy / tempDrawMultiFactor * tempDrawMultiFactor);
                        }
                        if (1 == documentOp.SelectedDrawObjectList.Count)
                        {
                            CheckAutoContact();
                        }
                        break;
                    case SelectObject.SelectNone:
                        documentOp.ChangeChooseSign(true, point);
                        base.OnMouseMoveLeft(point);
                        break;
                }
            }
            else if (4 != drawToolType)
            {
                documentOp.LastHitedObject.DotEnd = point;
                base.OnMouseMoveLeft(point);
            }
        }

        public override void ChangePropertyValue()
        {
            int n = documentOp.SelectedDrawObjectList.Count;
            if (5 == documentOp.SelectedDrawObjectList[n - 1].GraphType)
            {
                RailEleFoupDot dot = (RailEleFoupDot)documentOp.SelectedDrawObjectList[n - 1];
                if ( dot.CodingScratchDot!=dot.CodingScratchDotOri)
                {
                    if (!ComputeFoupDotPoint(dot))
                    {
                        MessageBox.Show("please set line coding firstly");
                    }
                }
            }
            documentOp.SelectedDrawObjectList[n - 1].ChangePropertyValue();
        }

        public void WorkRegionKeyDown(Direction direction)
        {
            Int16 offset = documentOp.SelectedDrawObjectList[0].DrawMultiFactor;
            selectObject = SelectObject.SelectEle;
            switch (direction)
            {
                case Direction.up:
                    OnMouseMoveLeft(new Point(lastPoint.X, lastPoint.Y - offset));
                    break;
                case Direction.down:
                    OnMouseMoveLeft(new Point(lastPoint.X, lastPoint.Y + offset));
                    break;
                case Direction.left:
                    OnMouseMoveLeft(new Point(lastPoint.X - offset, lastPoint.Y));
                    break;
                case Direction.right:
                    OnMouseMoveLeft(new Point(lastPoint.X + offset, lastPoint.Y));
                    break;
            }
        }

        private void CheckAutoContact()
        {
            Point[] pts=new Point[3];
            pts[0] = documentOp.SelectedDrawObjectList[0].DotStart;
            pts[1] = documentOp.SelectedDrawObjectList[0].DotEnd;
            pts[2] = documentOp.SelectedDrawObjectList[0].DotEndFork;
            Rectangle rc;
            int selectedObjListDotCount = 0;
            switch (documentOp.SelectedDrawObjectList[0].GraphType)
            {
                case 1:
                case 2:
                    selectedObjListDotCount = 2;
                    break;
                case 3:
                    selectedObjListDotCount = 3;
                    break;
            }

            for (int i = 0; i < selectedObjListDotCount; i++)
            {
                rc = new Rectangle(pts[i].X - 4, pts[i].Y - 4, 8, 8);
                int drawObjListCount = documentOp.DrawObjectList.Count;
                for (int j = 0; j < drawObjListCount; j++)
                {
                    Mcs.RailSystem.Common.BaseRailEle obj = documentOp.DrawObjectList[j];
                    if (obj != documentOp.SelectedDrawObjectList[0])
                    {
                        if (rc.Contains(obj.DotStart))
                        {
                            hasContact = true;
                            contactStartDot = pts[i];
                            contactEndDot = obj.DotStart;
                            documentOp.SelectedDrawObjectList[0].TrackerColor = Color.Red;
                            j = drawObjListCount;
                            i = selectedObjListDotCount;
                        }
                        else if(rc.Contains(obj.DotEnd))
                        {
                            hasContact = true;
                            contactStartDot = pts[i];
                            contactEndDot = obj.DotEnd;
                            documentOp.SelectedDrawObjectList[0].TrackerColor = Color.Red;
                            j = drawObjListCount;
                            i = selectedObjListDotCount;
                        }
                        else if (rc.Contains(obj.DotEndFork))
                        {
                            hasContact = true;
                            contactStartDot = pts[i];
                            contactEndDot = obj.DotEndFork;
                            documentOp.SelectedDrawObjectList[0].TrackerColor = Color.Red;
                            j = drawObjListCount;
                            i = selectedObjListDotCount;
                        }
                        else if (hasContact)
                        {
                            hasContact = false;
                            documentOp.SelectedDrawObjectList[0].TrackerColor = Color.Blue;
                            contactStartDot = Point.Empty;
                            contactEndDot = Point.Empty;
                        }
                    }
                }
            }
        }

        public void ImportKeyDotFromDB()
        {
            int dotnum = documentOp.DrawObjectList.Count;
            for (int i = 0; i < dotnum; i++)
            {
                if (6 == documentOp.DrawObjectList[i].GraphType)
                {
                    Mcs.RailSystem.Common.EleDevice device = (Mcs.RailSystem.Common.EleDevice)documentOp.DrawObjectList[i];
                    device.FoupDotFirst = null;
                    device.ListFoupDot.Clear();
                }
            }
            for (int i = dotnum - 1; i > -1; i--)
            {
                if (5 == documentOp.DrawObjectList[i].GraphType)
                {
                    documentOp.DrawObjectList.RemoveAt(i);
                }
            }

            //添加读取
            /***************************************************
             * ***********************************************
             * *********************************************/
            Mcs.RailSystem.Common.EleFoupDot dot = new Mcs.RailSystem.Common.EleFoupDot();
        //    dot.CodingScratchDot
            if (!ComputeFoupDotPoint(dot))
            {
                MessageBox.Show("import data false,please set line coding firstly");
            }

        }

        private bool ComputeFoupDotPoint(Mcs.RailSystem.Common.EleFoupDot dot)
        {
            int num = documentOp.DrawObjectList.Count;
            int i;
            for (i = 0; i < num; i++)
            {
                if (1 == documentOp.DrawObjectList[i].GraphType
                    && ((RailEleLine)(documentOp.DrawObjectList[i])).CodingBegin != ((RailEleLine)(documentOp.DrawObjectList[i])).CodingEnd
                    && dot.CodingScratchDot >= ((RailEleLine)(documentOp.DrawObjectList[i])).CodingBegin
                    && dot.CodingScratchDot <= ((RailEleLine)(documentOp.DrawObjectList[i])).CodingEnd)
                {
                    RailEleLine line = (RailEleLine)documentOp.DrawObjectList[i];
                    if (line.PointList[0].Y == line.PointList[1].Y)
                    {
                        Point pt = Point.Empty;
                        if (line.PointList[0].X < line.PointList[1].X)
                            pt.X = line.PointList[0].X + (dot.CodingScratchDot - line.CodingBegin) * line.Lenght / (line.CodingEnd - line.CodingBegin);
                        else if (line.PointList[0].X > line.PointList[1].X)
                            pt.X = line.PointList[0].X - (dot.CodingScratchDot - line.CodingBegin) * line.Lenght / (line.CodingEnd - line.CodingBegin);
                        pt.Y = line.PointList[0].Y;
                        dot.PtScratchDot = pt;
                        dot.CodingScratchDotOri = dot.CodingScratchDot;
                        pt.Offset(dot.PtOffset.X, dot.PtOffset.Y);
                        dot.PtScratchDotIcon = pt;
                    }
                    else
                    {
                        Point pt = Point.Empty;
                        if (line.PointList[0].Y < line.PointList[1].Y)
                            pt.Y = line.PointList[0].Y + (dot.CodingScratchDot - line.CodingBegin) * line.Lenght / (line.CodingEnd - line.CodingBegin);
                        else if (line.PointList[0].Y > line.PointList[1].Y)
                            pt.Y = line.PointList[0].Y - (dot.CodingScratchDot - line.CodingBegin) * line.Lenght / (line.CodingEnd - line.CodingBegin);
                        pt.X = line.PointList[0].X;
                        dot.PtScratchDot = pt;
                        dot.CodingScratchDotOri = dot.CodingScratchDot;
                        pt.Offset(dot.PtOffset.X, dot.PtOffset.Y);
                        dot.PtScratchDotIcon = pt;
                    }
                    break;
                }
            }
            if (i == num)
            {
                dot.CodingScratchDot = dot.CodingScratchDotOri;
                return false;
            }
            return true;
        }

        public void ProRegionAddNode(string str)
        {
            TreeNode tempTN;
            tempTN = new TreeNode(str);
            tempTN.Name = str;
            documentOp.ListTreeNode.Add(tempTN);
            documentOp.NodeSelected = tempTN;
        }

        



    }
}
