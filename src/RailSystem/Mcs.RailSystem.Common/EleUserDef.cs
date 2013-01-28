using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace Mcs.RailSystem.Common
{
    public class EleUserDef:BaseRailEle
    {
        protected string strSavePath = "";
        protected Rectangle rcUserDef = new Rectangle();
        protected Image imageUserDef;

        [Browsable(false)]
        public string StrSavePath
        {
            get { return strSavePath; }
            set { strSavePath = value; }
        }
        [Browsable(false)]
        public Rectangle RcUserDef
        {
            get { return rcUserDef; }
            set { rcUserDef = value; }
        }
        [Browsable(false)]
        public Image ImageUserDef
        {
            get { return imageUserDef; }
            set { imageUserDef = value; }
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


    }
}
