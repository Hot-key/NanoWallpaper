using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unvell.D2DLib;

namespace NanoWallpaper.ControllerInterface
{
    interface ID2dBase
    {
        void OnRender(D2DGraphics g);
    }
}
