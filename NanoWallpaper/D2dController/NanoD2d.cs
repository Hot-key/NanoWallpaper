using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoWallpaper.ControllerInterface;
using unvell.D2DLib;

namespace NanoWallpaper.D2dController
{
    public class NanoD2d : ID2dBase
    {
        public Size Size { get; set; }

        public NanoD2d Parent { get; set; }

        public string Name { get; set; }

        public virtual Point Location { get; set; }

        public Point AbsolutePosition => Parent == null ? Location : new Point(Parent.AbsolutePosition.X + Location.X, Parent.AbsolutePosition.Y + Location.Y);

        public NanoD2dCollection Controls
        {
            get
            {
                if (this is NanoD2dCollection contralsNanoD2d)
                {
                    return contralsNanoD2d;
                }
                return new NanoD2dCollection(); // Empty NanoD2dCollection
            }
        }

        public virtual void OnRender(D2DGraphics g)
        {
        }
    }
}
