using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BaseRailElement
{
    public class RailEleFoupDot : Mcs.RailSystem.Common.EleFoupDot
    {
        public RailEleFoupDot()
        {
            GraphType = 5;
            PenFoupDot.Width = PenWidth;
            PenFoupDot.Color = PenColor;
            PenFoupDot.DashStyle = PenDashStyle;
        }

        public RailEleFoupDot CreateEle(Point pt, Size size, Int16 multiFactor, string text)
        {
            DrawMultiFactor = multiFactor;
            pt.Offset(pt.X / DrawMultiFactor - pt.X, pt.Y / DrawMultiFactor - pt.Y);
            ptScratchDot = pt;
            pt.Offset(-3, 5);
            ptScratchDotIcon = pt;
            ptOffset.X = ptScratchDotIcon.X - ptScratchDot.X;
            ptOffset.Y = ptScratchDotIcon.Y - ptScratchDot.Y;
            string strPath = Application.StartupPath;
            strPath = strPath.Substring(0, strPath.IndexOf("bin\\")) + @"src\RailSystem\Mcs.RailSystem.Common\Resources\foupWay.bmp";
            imageFoupWayIcon = Image.FromFile(strPath);
            rcFoupDot = new Rectangle(ptScratchDot.X, ptScratchDot.Y, 2, 2);
            iconWidth = 10;
            iconHeight = 10;
            this.railText = text;
            return this;
        }

        public override void Draw(System.Drawing.Graphics canvas)
        {
            if (canvas == null)
            {
                throw new Exception("Graphics对象Canvas不能为空");
            }
            if (ptScratchDot.IsEmpty)
            {
                throw new Exception("there is no element");
            }
            rcFoupDot.X = ptScratchDot.X;
            rcFoupDot.Y = ptScratchDot.Y;
            canvas.DrawImage(imageFoupWayIcon, ptScratchDotIcon.X, ptScratchDotIcon.Y, iconWidth, iconHeight);
            canvas.DrawRectangle(pen, rcFoupDot);
        }

        public override void DrawTracker(System.Drawing.Graphics canvas)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            if (ptScratchDot.IsEmpty)
                throw new Exception("the ptScratchDot is empty");
            Pen pen = new Pen(Color.Blue);
            pen.Width = 1;
            Point pt = new Point(ptScratchDotIcon.X + iconWidth, ptScratchDotIcon.Y + iconHeight);
            pt.Offset(pt.X * (DrawMultiFactor - 1), pt.Y * (DrawMultiFactor - 1));
            Rectangle rc = new Rectangle(pt.X - 4, pt.Y - 4, 8, 8);
            canvas.DrawRectangle(pen, rc);
            pen.Dispose();
        }

        public override int HitTest(System.Drawing.Point point, bool isSelected)
        {
            if (isSelected)
            {
                int handleHit = HandleHitTest(point);
                if (handleHit > 0)
                    return handleHit;
            }
            Rectangle rc = new Rectangle(ptScratchDotIcon.X, ptScratchDotIcon.Y, iconWidth, iconHeight);
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
            Point pt = ptScratchDotIcon;
            pt.Offset(iconWidth, iconHeight);
            Rectangle rc = new Rectangle(pt.X - 4, pt.Y - 4, 8, 8);
            if (rc.Contains(point))
                return 1;
            return -1;
        }

        public override void Move(System.Drawing.Point start, System.Drawing.Point end)
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
            if (lockDotIcon)
            {
                
                pt = ptScratchDot;
                pt.Offset(offsetX, offsetY);
                ptScratchDot = pt;
                codingScratchDot = -2;
                codingScratchDotOri = -2;
            }
            pt = ptScratchDotIcon;
            pt.Offset(offsetX, offsetY);
            PtScratchDotIcon = pt;
            ptOffset.X = ptScratchDotIcon.X - ptScratchDot.X;
            ptOffset.Y = ptScratchDotIcon.Y - ptScratchDot.Y;
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
            if (iconWidth > 0 && iconHeight > 0)
            {
                iconWidth += dx;
                iconHeight += dy;
            }
            else if (dx > 0 || dy > 0)
            {
                if (dx > 0 && dy > 0)
                {
                    iconWidth += dx;
                    iconHeight += dy;
                }
                else if (dx > 0)
                {
                    iconWidth += dx;
                }
                else if (dy > 0)
                {
                    iconHeight += dy;
                }
            }
        }

        public override void DrawEnlargeOrShrink(float draw_multi_factor)
        {
            DrawMultiFactor = Convert.ToInt16(draw_multi_factor);
            base.DrawEnlargeOrShrink(draw_multi_factor);
        }

        public override void ChangePropertyValue()
        {            
            base.ChangePropertyValue();
        }

        public override bool ChosedInRegion(Rectangle rect)
        {
            Point pt = ptScratchDotIcon;
            pt.X = pt.X * DrawMultiFactor;
            pt.Y = pt.Y * DrawMultiFactor;
            int widthTemp = iconWidth * DrawMultiFactor;
            int heightTemp = iconHeight * DrawMultiFactor;
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(rect);
            Region region = new Region(path);
            Rectangle rc = new Rectangle(pt.X, pt.Y, widthTemp, heightTemp);
            if(region.IsVisible(rc))
                return true;
            else 
                return false;
        }


    }
}
