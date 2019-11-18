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
        public Size controlSize;

        public Point Location { get; set; }

        public virtual void OnRender(D2DGraphics g)
        {
        }
    }
}
