using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BaseRailElement
{
    public class ObjectCurvedOp
    {
        private int drawMultiFactor = 1;
        public int DrawMultiFactor
        {
            set { drawMultiFactor = value; }
        }

        public void DrawTracker(
            Graphics canvas, 
            Point centerDot,
            int radiuArc, 
            Mcs.RailSystem.Common.EleCurve.DirectonCurved direction)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            Point center = new Point(centerDot.X * drawMultiFactor, centerDot.Y * drawMultiFactor);
            int radiu = radiuArc * drawMultiFactor;
            Point[] points = new Point[4];
            Pen pen = new Pen(Color.Blue, 1);
            //SolidBrush bsh = new SolidBrush(Color.Black);
            switch (direction)
            {
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.first:
                    points[0] = center;
                    points[1] = new Point(center.X + radiu, center.Y);
                    points[2] = new Point(center.X + radiu, center.Y + radiu);
                    points[3] = new Point(center.X, center.Y + radiu);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.second:
                    points[0] = center;
                    points[1] = new Point(center.X, center.Y + radiu);
                    points[2] = new Point(center.X - radiu, center.Y + radiu);
                    points[3] = new Point(center.X - radiu, center.Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.third:
                    points[0] = center;
                    points[1] = new Point(center.X - radiu, center.Y);
                    points[2] = new Point(center.X - radiu, center.Y - radiu);
                    points[3] = new Point(center.X, center.Y - radiu);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.four:
                    points[0] = center;
                    points[1] = new Point(center.X, center.Y - radiu);
                    points[2] = new Point(center.X + radiu, center.Y - radiu);
                    points[3] = new Point(center.X + radiu, center.Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.NULL:
                    break;
            }
            for (int i = 0; i < 4; i++)
            {
                Rectangle rc = new Rectangle(points[i].X - 4, points[i].Y - 4, 8, 8);
                canvas.DrawRectangle(pen, rc);
                //canvas.FillRectangle(bsh, rc);
            }
            pen.Dispose();
            //bsh.Dispose();
        }

        public int HitTest(
            Point point,
            bool isSelected,
            Point centerArc,
            int radiuArc,
            Mcs.RailSystem.Common.EleCurve.DirectonCurved direction)
        {
            Point center = new Point(centerArc.X * drawMultiFactor, centerArc.Y * drawMultiFactor);
            int radiu = radiuArc * drawMultiFactor;
            if (isSelected)
            {
                int handleHit = HandleHitTest(point, center, radiu, direction);
                if (handleHit > 0)
                    return handleHit;
            }
            Point[] wrapper = new Point[1];
            wrapper[0] = point;
            Rectangle rc = new Rectangle();
            Point[] points = new Point[4];
            switch (direction)
            {
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.first:
                    points[0] = center;
                    points[2] = new Point(center.X + radiu, center.Y + radiu);
                    rc = new Rectangle(points[0].X, points[0].Y, points[2].X - points[0].X, points[2].Y - points[0].Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.second:
                    points[1] = new Point(center.X, center.Y + radiu);
                    points[3] = new Point(center.X - radiu, center.Y);
                    rc = new Rectangle(points[3].X, points[3].Y, points[1].X - points[3].X, points[1].Y - points[3].Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.third:
                    points[0] = center;
                    points[2] = new Point(center.X - radiu, center.Y - radiu);
                    rc = new Rectangle(points[2].X, points[2].Y, points[0].X - points[2].X, points[0].Y - points[2].Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.four:
                    points[1] = new Point(center.X, center.Y - radiu);
                    points[3] = new Point(center.X + radiu, center.Y);
                    rc = new Rectangle(points[1].X, points[1].Y, points[3].X - points[1].X, points[3].Y - points[1].Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.NULL:
                    break;
            }
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(rc);
            Region region = new Region(path);
            if (region.IsVisible(wrapper[0]))
                return 0;
            else
                return -1;
        }

        public int HandleHitTest(Point point,
            Point center,
            int radiu,
            Mcs.RailSystem.Common.EleCurve.DirectonCurved direction)
        {
            Point[] points = new Point[4];
            switch (direction)
            {
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.first:
                    points[0] = center;
                    points[1] = new Point(center.X + radiu, center.Y);
                    points[2] = new Point(center.X + radiu, center.Y + radiu);
                    points[3] = new Point(center.X, center.Y + radiu);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.second:
                    points[0] = center;
                    points[1] = new Point(center.X, center.Y + radiu);
                    points[2] = new Point(center.X - radiu, center.Y + radiu);
                    points[3] = new Point(center.X - radiu, center.Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.third:
                    points[0] = center;
                    points[1] = new Point(center.X - radiu, center.Y);
                    points[2] = new Point(center.X - radiu, center.Y - radiu);
                    points[3] = new Point(center.X, center.Y - radiu);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.four:
                    points[0] = center;
                    points[1] = new Point(center.X, center.Y - radiu);
                    points[2] = new Point(center.X + radiu, center.Y - radiu);
                    points[3] = new Point(center.X + radiu, center.Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.NULL:
                    break;
            }
            for (int i = 0; i < 4; i++)
            {
                Point pt = points[i];
                Rectangle rc = new Rectangle(pt.X - 3, pt.Y - 3, 6, 6);
                if (rc.Contains(point))
                    return i + 1;
            }
            return -1;
        }

        public Rectangle Scale(
            int handle,
            int dx,
            int dy,
            Point center,
            int radiu,
            Mcs.RailSystem.Common.EleCurve.DirectonCurved direction)
        {
            Point[] points = new Point[4];
            switch (direction)
            {
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.first:
                    points[0] = center;
                    points[1] = new Point(center.X + radiu, center.Y);
                    points[2] = new Point(center.X + radiu, center.Y + radiu);
                    points[3] = new Point(center.X, center.Y + radiu);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.second:
                    points[0] = center;
                    points[1] = new Point(center.X, center.Y + radiu);
                    points[2] = new Point(center.X - radiu, center.Y + radiu);
                    points[3] = new Point(center.X - radiu, center.Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.third:
                    points[0] = center;
                    points[1] = new Point(center.X - radiu, center.Y);
                    points[2] = new Point(center.X - radiu, center.Y - radiu);
                    points[3] = new Point(center.X, center.Y - radiu);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.four:
                    points[0] = center;
                    points[1] = new Point(center.X, center.Y - radiu);
                    points[2] = new Point(center.X + radiu, center.Y - radiu);
                    points[3] = new Point(center.X + radiu, center.Y);
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.NULL:
                    break;
            }
            Point pt = points[handle - 1];
            Point[] wrapper = new Point[] { pt };
            switch (direction)
            {
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.first:
                    if (1 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx < 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(pt.X, pt.Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (2 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dx > 0)
                            {
                                pt.Offset(dx, 0);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(dx, 0);
                    }
                    else if (3 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx > 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (4 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dy > 0)
                            {
                                pt.Offset(0, dy);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(0, dy);
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.second:
                    if (1 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx > 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(pt.X, pt.Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (2 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dy > 0)
                            {
                                pt.Offset(0, dy);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(0, dy);
                    }
                    else if (3 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx < 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (4 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dx < 0)
                            {
                                pt.Offset(dx, 0);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(dx, 0);
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.third:
                    if (1 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx > 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(pt.X, pt.Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (2 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dx < 0)
                            {
                                pt.Offset(dx, 0);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(dx, 0);
                    }
                    else if (3 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx < 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (4 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dy < 0)
                            {
                                pt.Offset(0, dy);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(0, dy);
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.four:
                    if (1 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx < 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(pt.X, pt.Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (2 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dy < 0)
                            {
                                pt.Offset(0, dy);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(0, dy);
                    }
                    else if (3 == handle)
                    {
                        int var = dx;
                        if (20 > radiu)
                        {
                            if (dx > 0)
                            {
                                pt.Offset(var, var);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(var, var);
                    }
                    else if (4 == handle)
                    {
                        if (20 > radiu)
                        {
                            if (dy > 0)
                            {
                                pt.Offset(0, dy);
                            }
                            else
                            {
                                return new Rectangle(points[0].X, points[0].Y, radiu, radiu);
                            }
                        }
                        pt.Offset(0, dy);
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.NULL:
                    break;
            }
            wrapper[0] = pt;

            int dw, dh;
            dw = wrapper[0].X - points[handle - 1].X;
            dh = wrapper[0].Y - points[handle - 1].Y;

            switch (direction)
            {
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.first:
                    switch (handle)
                    {
                        case 1:
                            radiu -= dw;
                            center = wrapper[0];
                            break;
                        case 2:
                            radiu += dw;
                            break;
                        case 3:
                            radiu += dw;
                            break;
                        case 4:
                            radiu += dh;
                            break;
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.second:
                    switch (handle)
                    {
                        case 1:
                            radiu += dw;
                            center = wrapper[0];
                            break;
                        case 2:
                            radiu += dh;
                            break;
                        case 3:
                            radiu -= dw;
                            break;
                        case 4:
                            radiu -= dw;
                            break;
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.third:
                    switch (handle)
                    {
                        case 1:
                            radiu += dw;
                            center = wrapper[0];
                            break;
                        case 2:
                            radiu -= dw;
                            break;
                        case 3:
                            radiu -= dw;
                            break;
                        case 4:
                            radiu -= dh;
                            break;
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.four:
                    switch (handle)
                    {
                        case 1:
                            radiu -= dw;
                            center = wrapper[0];
                            break;
                        case 2:
                            radiu -= dh;
                            break;
                        case 3:
                            radiu += dw;
                            break;
                        case 4:
                            radiu += dh;
                            break;
                    }
                    break;
                case Mcs.RailSystem.Common.EleCurve.DirectonCurved.NULL:
                    break;
            }
            return new Rectangle(center.X, center.Y, radiu, radiu);
        }
    }
}
