using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BaseRailElement
{
    public class RailAuxiliaryDraw : Mcs.RailSystem.Common.BaseRailEle
    {
        private Pen pen = new Pen(Color.Black, 1);
        public Pen PenDraw
        {
            get { return pen; }
            set { pen = value; }
        }

        public RailAuxiliaryDraw()
        {
            GraphType = 4;
        }

        public RailAuxiliaryDraw CreateEle(Int16 railType, Int16 multiFactor,Point ptStart)
        {
            RailAuxiliaryDrawType = railType;
            DrawMultiFactor = multiFactor;
            DotStart = ptStart;
            DotEnd = ptStart;
            DotEndFork = ptStart;
            return this;
        }

        public override void Draw(Graphics canvas)
        {
            if (canvas == null)
            {
                throw new Exception("Graphics对象Canvas不能为空");
            }
            switch (RailAuxiliaryDrawType)
            { 
                case 0:
                    int top = DotStart.X < DotEnd.X ? DotStart.X : DotEnd.X;
                    int left = DotStart.Y < dotEnd.Y ? DotStart.Y : dotEnd.Y;
                    int width = Math.Abs(DotStart.X - DotEnd.X);
                    int height = Math.Abs(DotStart.Y - DotEnd.Y);
                    canvas.DrawRectangle(pen, top, left, width, height);
                    break;
                case 1:
                    break;
                case 2:
                    break;

            }
        }

        public override void DrawTracker(Graphics canvas)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            switch (RailAuxiliaryDrawType)
            { 
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        public override int HitTest(Point point, bool isSelected)
        {
            return base.HitTest(point, isSelected);
        }

        private int HandleHitTest()
        {
            return -1;
        }

        public override void Move(Point start, Point end)
        { 
        }

        protected void Translate(int offsetX, int offsetY)
        {
            switch (RailAuxiliaryDrawType)
            { 
                case 0:
                    break;
            }
        }

        public override void MoveHandle(int handle, Point start, Point end)
        { }

        private void Scale(int handle, int dx, int dy)
        { }

        public override void DrawEnlargeOrShrink(float draw_multi_factor)
        {
            DrawMultiFactor = Convert.ToInt16(draw_multi_factor);
            base.DrawEnlargeOrShrink(draw_multi_factor);
        }

        public override bool ChosedInRegion(Rectangle rect)
        {
            return false;
        }

        public object Clone(string str)
        {
            RailAuxiliaryDraw cl = new RailAuxiliaryDraw();
            return cl;
        }

        private Rectangle ScaleOp()
        {
            Rectangle rc = new Rectangle();
            return rc;
        }



    }
}
