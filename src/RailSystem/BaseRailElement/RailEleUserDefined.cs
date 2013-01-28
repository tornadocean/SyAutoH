using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BaseRailElement
{
    public class RailEleUserDef:Mcs.RailSystem.Common.EleUserDef
    {
        public RailEleUserDef()
        {
            GraphType = 7;
        }

        public RailEleUserDef CreateEle(Point pt, Size size, Int16 multiFactor, string text,Image image)
        {
            imageUserDef = image;
            rcUserDef.X = 0;
            rcUserDef.Y = 0;
            rcUserDef.Width = imageUserDef.Width < size.Width ? imageUserDef.Width : size.Width;
            rcUserDef.Height = imageUserDef.Height < size.Height ? imageUserDef.Height : size.Height;
            railText = text;
            return this;
        }

        public override void Draw(Graphics canvas)
        {
            if (canvas == null)
                throw new Exception("Graphics对象Canvas不能为空");
            canvas.DrawImage(imageUserDef, rcUserDef);
        }


    }
}
