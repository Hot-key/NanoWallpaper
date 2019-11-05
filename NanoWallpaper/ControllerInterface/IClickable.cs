using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoWallpaper.ControllerInterface
{
    interface IClickable
    {
        void OnClick(object sender, System.EventArgs e);
    }
}
