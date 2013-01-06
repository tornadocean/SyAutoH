using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;

namespace BaseRailElement
{
    public class Device : BaseRailEle
    {
   //     private Bitmap image = new Bitmap(
        public Point deviceLocation = Point.Empty;
        private Int32 deviceCoding = -1;

        public Int32 DeviceCoding
        {
            get { return deviceCoding; }
            set { deviceCoding = value; }
        }

        public Device()
        {
            GraphType = 5;
        }

        public Device CreateEle(Point pt, Size size, Int16 multiFactor, string text)
        {
            deviceLocation.X = pt.X / multiFactor;
            deviceLocation.Y = pt.Y / multiFactor;
            DrawMultiFactor = multiFactor;
            this.railText = text;
            return this;
        }

        public override void Draw(Graphics canvas)
        {
        //    Image image;
            
        //    canvas.DrawImage(
        }

        public override void DrawTracker(Graphics canvas) 
        { }

        public override int HitTest(Point point, bool isSelected)
        {
            return 0;
        }

        protected override void Translate(int offsetX, int offsetY)
        { }

        protected override void Scale(int handle, int dx, int dy)
        { }

        public override void RotateCounterClw()
        { }

        public override void RotateClw()
        { }

        public override void DrawEnlargeOrShrink(float drawMultiFactor)
        { }

        public override void ChangePropertyValue()
        { }

        public override bool ChosedInRegion(Rectangle rect)
        {
            return false;
        }

        public override DataRow DataSetXMLSave(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            return dr;
        }
    }
}
