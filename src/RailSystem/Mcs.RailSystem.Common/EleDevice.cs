using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace Mcs.RailSystem.Common
{
    public class EleDevice : BaseRailEle
    {
        protected Int16 deviceID = 0;
        protected Point ptDevice = Point.Empty;
        protected List<EleFoupDot> listFoupDot=new List<EleFoupDot>();
        protected Point ptFirstDot = Point.Empty;
        protected Point ptOffset = Point.Empty;
        protected EleFoupDot foupDotFirst;
        protected bool isStocker = false;
        protected Int16 room = 0;
        protected Rectangle rcStockerRoom = new Rectangle();
        protected Image imageDevice;
        protected Int32 widthIcon = 16;
        protected Int32 heightIcon = 16;

        [Browsable(false)]
        public Int16 DeviecID
        {
            get { return deviceID; }
            set { deviceID = value; }
        }
        [Browsable(false)]
        public Point PtDevice
        {
            get { return ptDevice; }
            set { ptDevice = value; }
        }
        [Browsable(false)]
        public List<EleFoupDot> ListFoupDot
        {
            get { return listFoupDot; }
            set { listFoupDot = value; }
        }
        [Browsable(false)]
        public Point PtFirstDot
        {
            get { return ptFirstDot; }
            set { ptFirstDot = value; }
        }
        [Browsable(false)]
        public Point PtOffset
        {
            get { return ptOffset; }
            set { ptOffset = value; }
        }
        [Browsable(false)]
        public EleFoupDot FoupDotFirst
        {
            get { return foupDotFirst; }
            set { foupDotFirst = value; }
        }
        public bool Stocker
        {
            get { return isStocker; }
            set { isStocker = value; }
        }
        [Browsable(false)]
        public Int16 Room
        {
            get { return room; }
            set { room = value; }
        }
        [Browsable(false)]
        public Rectangle RcStockerRoom
        {
            get { return rcStockerRoom; }
            set { rcStockerRoom = value; }
        }
        [Browsable(false)]
        public Image ImageDevice
        {
            get { return imageDevice; }
            set { imageDevice = value; }
        }
        public Int32 WidthIcon
        {
            get { return widthIcon; }
            set { widthIcon = value; }
        }
        public Int32 HeightIcon
        {
            get { return heightIcon; }
            set { heightIcon = value; }
        }

        [Browsable(false)]
        public new float Speed
        {
            get;
            set;
        }
        [Browsable(false)]
        public new Int32 CodingBegin
        {
            get;
            set;
        }
        [Browsable(false)]
        public new Int32 CodingEnd
        {
            get;
            set;
        }
        [Browsable(false)]
        public new Int32 CodingEndFork
        {
            get;
            set;
        }
        [Browsable(false)]
        public new Int32 CodingPrev
        {
            get;
            set;
        }
        [Browsable(false)]
        public new Int32 CodingNext
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
        public new Point DotStart
        {
            get;
            set;
        }
        [Browsable(false)]
        public new Point DotEnd
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
        public new bool SizeLock
        {
            get;
            set;
        }
    }
}
