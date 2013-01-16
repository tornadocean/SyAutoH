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
        protected static DrawDocOp document = DrawDocOp.EmptyDocument;
        public static DrawDocOp Document
        {
            get { return document; }
            set { document = value; }
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

        public ObjectBaseEvents()
        {
        }

        public override void OnLButtonDown(Point point)
        {
            base.OnLButtonDown(point);
            int hit = document.HitTest(point, false);
            _hit = hit;
            if (hit > 0)
                selectObject = SelectObject.SelectHandle;
            else if (hit == 0)
                selectObject = SelectObject.SelectEle;
            else
                selectObject = SelectObject.SelectNone;
            document.ChangeChooseSign(true, point);
        }

        public override bool OnRButtonDown(Point point)
        {
            base.OnRButtonDown(point);
            int hit = document.HitTest(point, false);
            if (hit >= 0)
                return true;
            else
                return false;
        }

        public override void OnLButtonUp(Point point)
        {
            base.OnLButtonUp(point);
            document.ChangeChooseSign(false, point);
        }

        public override void OnMouseMoveLeft(Point point)
        {
            int dx = point.X - lastPoint.X;
            int dy = point.Y - lastPoint.Y;
            int n = document.SelectedDrawObjectList.Count;
            int tempDrawMultiFactor = 1;
            switch (selectObject)
            {
                case SelectObject.SelectHandle:
                    tempDrawMultiFactor = document.SelectedDrawObjectList[0].DrawMultiFactor;
                    if ((dx != 0 && dx / tempDrawMultiFactor != 0) || (dy != 0 && dy / tempDrawMultiFactor != 0))
                    {
                        if (document.SelectedDrawObjectList[0].GraphType == 1)
                        {
                            if (n == 1)
                            {
                                document.SelectedDrawObjectList[0].MoveHandle(_hit, lastPoint, point);
                            }
                        }
                        else if (document.SelectedDrawObjectList[0].GraphType == 2)
                        {
                            if (n == 1)
                            {
                                document.SelectedDrawObjectList[0].MoveHandle(_hit, lastPoint, point);
                            }
                        }
                        else if (document.SelectedDrawObjectList[0].GraphType == 3)
                        {
                            if (n == 1)
                            {
                                document.SelectedDrawObjectList[0].MoveHandle(_hit, lastPoint, point);
                            }
                        }
                        else if (document.SelectedDrawObjectList[0].GraphType == 4)
                        {
                            if (n == 1)
                            {
                                document.SelectedDrawObjectList[0].MoveHandle(_hit, lastPoint, point);
                            }
                        }
                        lastPoint.Offset(dx / tempDrawMultiFactor * tempDrawMultiFactor, dy / tempDrawMultiFactor * tempDrawMultiFactor);
                    }
                    break;
                case SelectObject.SelectEle:
                    tempDrawMultiFactor = document.SelectedDrawObjectList[0].DrawMultiFactor;
                    if ((dx != 0 && dx / tempDrawMultiFactor != 0) || (dy != 0 && dy / tempDrawMultiFactor != 0))
                    {
                        for (int i = 0; i < n; i++)
                        {
                            if (document.SelectedDrawObjectList[i].GraphType == 1)
                            {
                                RailEleLine de = (RailEleLine)document.SelectedDrawObjectList[i];
                                document.SelectedDrawObjectList[i].Move(lastPoint, point);
                            }
                            else if (document.SelectedDrawObjectList[i].GraphType == 2)
                            {
                                RailEleCurve de = (RailEleCurve)document.SelectedDrawObjectList[i];
                                document.SelectedDrawObjectList[i].Move(lastPoint, point);
                            }
                            else if (document.SelectedDrawObjectList[i].GraphType == 3)
                            {
                                RailEleCross de = (RailEleCross)document.SelectedDrawObjectList[i];
                                document.SelectedDrawObjectList[i].Move(lastPoint, point);
                            }
                            //else if (document.SelectedDrawObjectList[i].GraphType == 4)
                            //{
                            //    RailLabal de = (RailLabal)document.SelectedDrawObjectList[i];
                            //    document.SelectedDrawObjectList[i].Move(lastPoint, point);
                            //}
                        }
                        lastPoint.Offset(dx / tempDrawMultiFactor * tempDrawMultiFactor, dy / tempDrawMultiFactor * tempDrawMultiFactor);
                    }
                    break;
                case SelectObject.SelectNone:
                    document.ChangeChooseSign(true, point);
                    base.OnMouseMoveLeft(point);
                    break;
            }
        }

        public override Point DrapDrawRegion(Point point)
        {
            Point pt_offset = Point.Empty;
            pt_offset.X = point.X - lastPoint.X;
            pt_offset.Y = point.Y - lastPoint.Y;
            base.DrapDrawRegion(point);
            return pt_offset;
        }
        public override void ChangePropertyValue()
        {
            int n = document.SelectedDrawObjectList.Count;
            document.SelectedDrawObjectList[n - 1].ChangePropertyValue();
        }

        public void WorkRegionKeyDown(Direction direction)
        {
            Int16 offset = document.SelectedDrawObjectList[0].DrawMultiFactor;
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
    }
}
