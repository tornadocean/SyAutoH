using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Mcs.RailSystem.Common
{
    public class EleCurve : BaseRailEle
    {
        private int startAngle = 0;
        private int sweepAngle = 90;
        private int rotateAngle = 90;
        public int oldRadiu = 50;
        private int radiu = 50;
        public Point oldCenter = Point.Empty;
        private Point center = Point.Empty;
        public Point oldFirstDot = Point.Empty;
        private Point firstDot = Point.Empty;
        public Point oldSecDot = Point.Empty;
        private Point secDot = Point.Empty;
        private DirectonCurved directionCurved = DirectonCurved.NULL;
        private PenStyle curvePen = new PenStyle();
        private Pen pen = new Pen(Color.Black, 1);

        public enum DirectonCurved
        {
            first, second, third, four, NULL
        }

        [Browsable(false)]
        public int StartAngle
        {
            get { return startAngle; }
            set { startAngle = value; }
        }
        [Browsable(false)]
        public int SweepAngle
        {
            get { return sweepAngle; }
            set { sweepAngle = value; }
        }
        [Browsable(false)]
        public int RotateAngle
        {
            get { return rotateAngle; }
            set { rotateAngle = value; }
        }
        [Category("轨道坐标")]
        public int Radiu
        {
            get { return radiu; }
            set { oldRadiu = radiu; radiu = value; }
        }
        [Category("轨道坐标")]
        public Point Center
        {
            get { return center; }
            set { oldCenter = center; center = value; }
        }
        [Browsable(false)]
        public Point FirstDot
        {
            get { return firstDot; }
            set { oldFirstDot = firstDot; firstDot = value; }
        }
        [Browsable(false)]
        public Point SecDot
        {
            get { return secDot; }
            set { oldSecDot = secDot; secDot = value; }
        }
        [Browsable(false)]
        public new Int32 CodingEndFork
        {
            get;
            set;
        }
        [Browsable(false)]
        public new Int32 CodingNextFork
        {
            get;
            set;
        }
        [Browsable(false)]
        public new Point DotEndFork
        {
            get;
            set;
        }
        [Browsable(false)]
        public DirectonCurved DirectionCurvedAttribute
        {
            get { return directionCurved; }
            set { directionCurved = value; }
        }
        [Category("线属性")]
        public Color PenColor
        {
            get { return curvePen.Color; }
            set { curvePen.Color = value; pen.Color = value; }
        }
        [Category("线属性")]
        public float PenWidth
        {
            get { return curvePen.Width; }
            set { curvePen.Width = value; pen.Width = value; }
        }
        [Category("线属性")]
        public DashStyle PenDashStyle
        {
            get { return curvePen.DashStyle; }
            set { curvePen.DashStyle = value; pen.DashStyle = value; }
        }
        [Browsable(false)]
        public Pen PenCurve
        {
            get { return pen; }
            set { pen = value; }
        }
    }
}
