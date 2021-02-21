using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OBJMedium;

namespace PlaneBorder
{
    class Rect2d
    {
        public ObjVector2 Pos_TopLeft;
        public ObjVector2 Pos_BottomRight;
        public Rect2d()
        {
            Pos_TopLeft = new ObjVector2();
            Pos_BottomRight = new ObjVector2();
        }
        public Rect2d(ObjVector2 topleft, ObjVector2 bottomright)
        {
            Pos_TopLeft = topleft;
            Pos_BottomRight = bottomright;
        }
    }
}
