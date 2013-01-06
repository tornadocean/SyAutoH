using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Mcs.RailSystem.Common
{
    class PenStyle
    {
        private Color _color = Color.Black;
        [XmlIgnore]
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        [XmlElement("Color")]
        public string ColorXmlWrapper
        {
            get { return ColorTranslator.ToHtml(_color); }
            set { _color = ColorTranslator.FromHtml(value); }
        }

        private float _width = 1;
        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private DashStyle _dashStyle = DashStyle.Solid;
        public DashStyle DashStyle
        {
            get { return _dashStyle; }
            set { _dashStyle = value; }
        }

        private int _alpha = 255;
        public int Alpha
        {
            get { return _alpha; }
            set { _alpha = value; }
        }

        public static PenStyle Default
        {
            get
            {
                PenStyle ls = new PenStyle(Color.Black, 1, DashStyle.Solid);
                return ls;
            }
        }

        public PenStyle() { }

        public PenStyle(Color color, int width, DashStyle dashStyle)
        {
            _color = color;
            _width = width;
            _dashStyle = dashStyle;
        }

        public XmlElement WriteToXml(XmlDocument doc)
        {
            XmlElement xe = doc.CreateElement("LineStyle");
            xe.SetAttribute("Color", ColorConverter.SerializeColor(_color));
            xe.SetAttribute("Width", _width.ToString());
            xe.SetAttribute("DashStyle", _dashStyle.ToString());

            return xe;
        }

        public void ReadFromXml(XmlElement xmlElement)
        {
            string val;
            val = xmlElement.GetAttribute("Color");
            _color = ColorConverter.DeserializeColor(val);

            val = xmlElement.GetAttribute("Width");
            _width = Convert.ToInt32(val);

            val = xmlElement.GetAttribute("DashStyle");
            _dashStyle = (DashStyle)DashStyle.Parse(typeof(DashStyle), val);

        }
    }
}
