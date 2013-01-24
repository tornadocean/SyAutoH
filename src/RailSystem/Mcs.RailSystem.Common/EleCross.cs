using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Mcs.RailSystem.Common
{
    public class EleCross : BaseRailEle
    {
        private int startAngle = 0;
        private int rotateAngle = 90;
        private int lenght = 30;
        private int lenghtFork = 30;
        private bool mirror = false;
        private List<Point> pointList = new List<Point>();
        private DirectionCross directionOfCross = DirectionCross.NULL;
        private PenStyle crossPen = new PenStyle();
        private Pen pen = new Pen(Color.Black, 3);

        public enum DirectionCross
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
        public int RotateAngle
        {
            get { return rotateAngle; }
            set { rotateAngle = value; }
        }
        [Category("轨道长度")]
        public int Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }
        [Category("轨道长度")]
        public int LenghtFork
        {
            get { return lenghtFork; }
            set { lenghtFork = value; }
        }
        [Browsable(false)]
        public bool Mirror
        {
            get { return mirror; }
            set { mirror = value; }
        }
        [Browsable(false)]
        public List<Point> PointList
        {
            get { return pointList; }
        }
        [Browsable(false)]
        public DirectionCross DirectionOfCross
        {
            get { return directionOfCross; }
            set { directionOfCross = value; }
        }
        [Category("线属性")]
        public Color PenColor
        {
            get { return crossPen.Color; }
            set { crossPen.Color = value; pen.Color = value; }
        }
        [Category("线属性")]
        public float PenWidth
        {
            get { return crossPen.Width; }
            set { crossPen.Width = value; pen.Width = value; }
        }
        [Category("线属性")]
        public DashStyle PenDashStyle
        {
            get { return crossPen.DashStyle; }
            set { crossPen.DashStyle = value; pen.DashStyle = value; }
        }
        [Browsable(false)]
        public Pen PenCross
        {
            get { return pen; }
            set { pen = value; }
        }
    }
}
