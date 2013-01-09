using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WinFormElement
{
    public struct OhtPos
    {
         public byte nID;
         public uint nPos;
         public byte nHand;
         public uint nError;
    }
    
    public class Vehicle
    {
        private byte nID;
        private byte nHandStatus;
        private uint nPosCode;
        private bool bState;
        private bool bAlarm;
        private Point ptOld = Point.Empty;
        private Point ptTemp = Point.Empty;
        private int dtLast;
        
        public int UpdateTime
        {
            get { return dtLast; }
            set { dtLast = value; }
        }
        public byte Hand
        {
            get { return nHandStatus; }
            set { nHandStatus = value;  }
        }

        public byte ID
        {
            get { return nID; }
        }
        public uint PosCode
        {
            get { return nPosCode; }
            set { nPosCode = value; }
        }
        public bool IsState
        {
            get { return bState; }
            set { bState = value; }
        }
        public bool IsAlarm
        {
            get { return bAlarm; }
            set { bAlarm = value; }
        }

        public Vehicle(byte ID)
        {
            nID = ID;
        }

        public bool ShowInScreen(Graphics canvas, Point location)
        {
            Color vehicleStatus = Color.Blue;
            switch (Hand)
            {
                case 0:
                    vehicleStatus = Color.Red;
                    break;
                case 1:
                    vehicleStatus = Color.Blue;
                    break;
                case 2:
                    vehicleStatus = Color.Green;
                    break;
                case 3:
                    vehicleStatus = Color.Yellow;
                    break;
                case 4:
                    vehicleStatus = Color.GreenYellow;
                    break;
                case 5:
                    vehicleStatus = Color.Linen;
                    break;
            }
            Pen pen = new Pen(vehicleStatus);
            SolidBrush bsh = new SolidBrush(vehicleStatus);
            ChangeVehiclePoint(location);
            Point[] tranglePts = new Point[3];
            tranglePts[0] = location;
            ComputeVehicleShape(location, tranglePts);
            GraphicsPath path = new GraphicsPath();
            path.AddLines(tranglePts);
            canvas.DrawPath(pen, path);
            canvas.FillPath(bsh, path);
            pen.Dispose();
            bsh.Dispose();
            //            Debug.WriteLine(string.Format("trangelePts {0},{1},{2}", tranglePts[0], tranglePts[1], tranglePts[2]));
            return false;
        }

        private void ChangeVehiclePoint(Point pt)
        {
            if (pt != ptTemp)
            {
                ptOld = ptTemp;
                ptTemp = pt;
            }
        }

        private void ComputeVehicleShape(Point pt, Point[] pts)
        {
            Point[] tempPts = new Point[3];
            int dx = pt.X - ptOld.X;
            int dy = pt.Y - ptOld.Y;
            Int16 dxSign = 0;
            Int16 dysign = 0;
            if (dx != 0)
            {
                dxSign = Convert.ToInt16(dx / Math.Abs(dx));
            }
            if (dy != 0)
            {
                dysign = Convert.ToInt16(dy / Math.Abs(dy));
            }
            tempPts[0].Offset(pts[0].X + dxSign * 3, pts[0].Y + dysign * 3);
            tempPts[1].Offset(pts[0].X - dxSign * 3, pts[0].Y - dysign * 3);
            tempPts[2].Offset(pts[0].X - dxSign * 3, pts[0].Y - dysign * 3);
            if (tempPts[0].X != tempPts[1].X)
            {
                double angle = Math.Atan((tempPts[0].Y - tempPts[1].Y) * 1.0 / (tempPts[0].X - tempPts[1].X));
                tempPts[1].Offset(-Convert.ToInt32(3 * Math.Sin(angle)), Convert.ToInt32(3 * Math.Cos(angle)));
                tempPts[2].Offset(Convert.ToInt32(3 * Math.Sin(angle)), -Convert.ToInt32(3 * Math.Cos(angle)));
            }
            else
            {
                tempPts[1].Offset(3, 0);
                tempPts[2].Offset(-3, 0);
            }
            pts[0].Offset(tempPts[0].X - pts[0].X, tempPts[0].Y - pts[0].Y);
            pts[1].Offset(tempPts[1].X - pts[1].X, tempPts[1].Y - pts[1].Y);
            pts[2].Offset(tempPts[2].X - pts[2].X, tempPts[2].Y - pts[2].Y);
        }
    }
}
