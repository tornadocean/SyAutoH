using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;
using System.Diagnostics;

namespace BaseRailElement
{
    public class RailLabal : Mcs.RailSystem.Common.BaseRailEle
    {
        private ObjectLabelOp objectLabelOp = new ObjectLabelOp();
        private Font font = null;
        [XmlIgnore]
        [Description("文本字体"), Category("文本")]
        public Font Font
        {
            get { return font; }
            set { font = value; }
        }

        private string text = "Label";
        [Description("文本内容"), Category("文本")]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private Rectangle rect = new Rectangle();

        private Point rectOrigionLoca = new Point();
        public Point RectOrigionLoca
        {
            get { return rectOrigionLoca; }
            set { rectOrigionLoca = value; }
        }


        public RailLabal() { GraphType = 4; }

        public RailLabal CreatEle(Int16 multiFactor, string text)
        {
            DrawMultiFactor = multiFactor;
            Point rectLocation = new Point(10, 10);
            Size rectSize = new Size(50, 15);
            rect.Location = rectLocation;
            rect.Size = rectSize;
            rectOrigionLoca = rectLocation;
            objectLabelOp.DrawMultiFactor = multiFactor;
            this.railText = text;
            return this;
        }

        public override void Draw(Graphics canvas)
        {
            if (font == null)
            {
                font = new Font("新宋体", 12, FontStyle.Regular);
            }
            Rectangle rc = rect;
            Point pt = rc.Location;
            pt.Offset(pt.X * DrawMultiFactor - pt.X, pt.Y * DrawMultiFactor - pt.Y);
            rc.Location = pt;
            SolidBrush bsh = new SolidBrush(Color.Black);
            canvas.DrawString(text, font, bsh, rc);
        }

        public override void DrawTracker(Graphics canvas)
        {
            Rectangle rc = rect;
            Point pt = rect.Location;
            pt.Offset(pt.X * DrawMultiFactor - pt.X, pt.Y * DrawMultiFactor - pt.Y);
            rc.Location = pt;
            objectLabelOp.DrawTracker(canvas, rc);
        }

        public override int HitTest(Point point, bool isSelected)
        {
            Rectangle rc = rect;
            Point pt = rect.Location;
            pt.Offset(pt.X * DrawMultiFactor - pt.X, pt.Y * DrawMultiFactor - pt.Y);
            rc.Location = pt;
            return objectLabelOp.HitTest(point, isSelected, rc);
        }

        protected void Translate(int offsetX, int offsetY)
        {
            Point pt = rect.Location;
            pt.Offset(offsetX, offsetY);
            rect.Location = pt;
            rectOrigionLoca = pt;
            Debug.WriteLine(string.Format("label pt is {0}", pt));
        }

        protected void Scale(int handle, int dx, int dy)
        {
            rect = objectLabelOp.Scale(handle, rect, dx, dy);
            rectOrigionLoca.X = rect.X / DrawMultiFactor;
            rectOrigionLoca.Y = rect.Y / DrawMultiFactor;
        }

        public object Clone()
        {
            RailLabal cl = new RailLabal();
            Point pt = rect.Location;
            pt.Offset(10, 10);
            cl.rect = rect;
            cl.rect.Location = pt;
            cl.DrawMultiFactor = DrawMultiFactor;
            cl.Font = font;
            cl.GraphType = GraphType;
            cl.LocationLock = locationLock;
            cl.RectOrigionLoca = RectOrigionLoca;
            cl.SizeLock = SizeLock;
            cl.Text = text;
            cl.objectLabelOp.DrawMultiFactor = DrawMultiFactor;
            this.railText = railText;
            return cl;
        }

        public override void DrawEnlargeOrShrink(float multiFactor)
        {
            objectLabelOp.DrawMultiFactor = Convert.ToInt16(multiFactor);
        }

        public override bool ChosedInRegion(Rectangle rc)
        {
            if (rc.Contains(rect))
                return true;
            return false;
        }
    }
}
