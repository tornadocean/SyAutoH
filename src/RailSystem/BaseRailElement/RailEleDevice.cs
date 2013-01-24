using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace BaseRailElement
{
    public class RailEleDevice : Mcs.RailSystem.Common.EleDevice
    {
        public RailEleDevice()
        {
            GraphType = 6;
        }

        public RailEleDevice CreateEle(Point pt, Size size, Int16 multiFactor, string text)
        {
            DrawMultiFactor = multiFactor;
            pt.Offset(pt.X / DrawMultiFactor - pt.X, pt.Y / DrawMultiFactor - pt.Y);
            ptDevice = pt;
            string strPath = Application.StartupPath;
            strPath = strPath.Substring(0, strPath.IndexOf("bin\\")) + @"src\RailSystem\Mcs.RailSystem.Common\Resources\devicesmall.bmp";
            imageDevice = Image.FromFile(strPath);
            widthIcon = imageDevice.Width;
            heightIcon = imageDevice.Height;
            rcStockerRoom.X = ptDevice.X;
            rcStockerRoom.Y = ptDevice.Y + heightIcon + 5;
            rcStockerRoom.Width = widthIcon;
            rcStockerRoom.Height = 5;
            this.railText = text;
            string strID = text.Substring(text.IndexOf('_') + 1);
            deviceID = Convert.ToInt16(strID);
            return this;
        }

        public override void Draw(Graphics canvas)
        {
            if (canvas == null)
            {
                throw new Exception("Graphics对象Canvas不能为空");
            }
            if (ptDevice.IsEmpty)
            {
                throw new Exception("there is no device");
            }
            if(locationLock)
            {
                ptDevice.X = foupDotFirst.PtScratchDot.X + ptOffset.X;
                ptDevice.Y = foupDotFirst.PtScratchDot.Y + ptOffset.Y;
            }
            Rectangle rc = new Rectangle();
            rc.X = ptDevice.X * DrawMultiFactor;
            rc.Y = ptDevice.Y * DrawMultiFactor;
            rc.Width = widthIcon * DrawMultiFactor;
            rc.Height = heightIcon * DrawMultiFactor;
            canvas.DrawImage(imageDevice, rc);
            if (Stocker)
            {
                Pen pen = new Pen(Color.Black, 1);
                rcStockerRoom.X = rc.X;
                rcStockerRoom.Y = rc.Y + rc.Width + 5;
                rcStockerRoom.Width = rc.Width;
                canvas.DrawRectangle(pen, rcStockerRoom);
            }
        }

        public override void DrawTracker(Graphics canvas) 
        {
            if (canvas == null)
            {
                throw new Exception("Graphics对象Canvas不能为空");
            }
            if (ptDevice.IsEmpty)
            {
                throw new Exception("there is no device");
            }
            Pen pen = new Pen(Color.Blue, 1);
            Point pt = new Point(ptDevice.X + widthIcon, ptDevice.Y + heightIcon);
            pt.Offset(pt.X * (DrawMultiFactor - 1), pt.Y * (DrawMultiFactor - 1));
            Rectangle rc = new Rectangle(pt.X - 4, pt.Y - 4, 8, 8);
            canvas.DrawRectangle(pen, rc);
            pen.Dispose();
        }

        public override int HitTest(Point point, bool isSelected)
        {
            if (isSelected)
            {
                int handleHit = HandleHitTest(point);
                if (handleHit > 0)
                    return handleHit;
            }
            Rectangle rc =
                new Rectangle(ptDevice.X * DrawMultiFactor, ptDevice.Y * DrawMultiFactor, widthIcon * DrawMultiFactor, heightIcon * DrawMultiFactor);
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(rc);
            Region region = new Region(path);
            if (region.IsVisible(point))
                return 0;
            else
                return -1;
        }

        private int HandleHitTest(Point point)
        {
            Point pt = ptDevice;
            pt.X *= DrawMultiFactor;
            pt.Y *= DrawMultiFactor;
            pt.Offset(widthIcon * DrawMultiFactor, heightIcon * DrawMultiFactor);
            Rectangle rc = new Rectangle(pt.X - 4, pt.Y - 4, 8, 8);
            if (rc.Contains(point))
                return 1;
            return -1;
        }

        public override void Move(Point start, Point end)
        {
            if (locationLock)
            {
                return;
            }
            int x = (end.X - start.X) / DrawMultiFactor;
            int y = (end.Y - start.Y) / DrawMultiFactor;
            Translate(x, y);
        }

        private void Translate(int offsetX, int offsetY)
        {
            Point pt = Point.Empty;
            pt = ptDevice;
            pt.Offset(offsetX, offsetY);
            ptDevice = pt;
        }

        public override void MoveHandle(int handle, Point start, Point end)
        {
            if (sizeLock)
                return;
            int dx = (end.X - start.X) / DrawMultiFactor;
            int dy = (end.Y - start.Y) / DrawMultiFactor;
            Scale(handle, dx, dy);
        }

        private void Scale(int handle, int dx, int dy)
        {
            if (widthIcon > 0 && heightIcon > 0)
            {
                widthIcon += dx;
                heightIcon += dy;
            }
            else if (dx > 0 || dy > 0)
            {
                if (dx > 0 && dy > 0)
                {
                    widthIcon += dx;
                    heightIcon += dy;
                }
                else if (dx > 0) { widthIcon += dx; }
                else if (dy > 0) { heightIcon += dy; }
            }
        }

        public override void DrawEnlargeOrShrink(float draw_multi_factor)
        {
            DrawMultiFactor = Convert.ToInt16(draw_multi_factor);
            base.DrawEnlargeOrShrink(draw_multi_factor);
        }

        public override void ChangePropertyValue()
        {
            if (locationLock)
            {
                ptOffset.X = ptDevice.X - foupDotFirst.PtScratchDot.X;
                ptOffset.Y = ptDevice.Y - foupDotFirst.PtScratchDot.Y;
            }
            base.ChangePropertyValue();
        }

        public override bool ChosedInRegion(Rectangle rect)
        {
            Point pt = ptDevice;
            pt.X = pt.X * DrawMultiFactor;
            pt.Y = pt.Y * DrawMultiFactor;
            int widthTemp = widthIcon * DrawMultiFactor;
            int heightTemp = heightIcon * DrawMultiFactor;
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(rect);
            Region region = new Region(path);
            Rectangle rc = new Rectangle(pt.X, pt.Y, widthTemp, heightTemp);
            if (region.IsVisible(rc))
                return true;
            else
                return false;
        }
    }
}
