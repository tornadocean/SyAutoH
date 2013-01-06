using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RailDraw
{
    public class CommonFunction
    {
        static Cursor newCursor = System.Windows.Forms.Cursors.Default;
        static public Cursor CreatCursor(string str)
        {
            switch (str)
            {
                case "drap":
                    newCursor = new Cursor(@"..\\..\\src\RailSystem\RailDraw\resources\drap.cur");
                    break;
                case "draw":
                    newCursor = new Cursor(@"..\\..\\src\RailSystem\RailDraw\resources\draw.cur");
                    break;
            }
            return newCursor;
        }
    }
}
