using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoWallpaper.D2dController;
using unvell.D2DLib;

namespace NanoWallpaper.ControllerInterface
{
    interface ID2dBase
    {
        NanoD2dCollection Controls { get; }
        void OnRender(D2DGraphics g);
    }
}
