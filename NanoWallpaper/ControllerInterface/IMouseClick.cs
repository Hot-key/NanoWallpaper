using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NanoWallpaper.ControllerInterface
{
    public interface IMouseClick
    {
        void OnClick(object sender, MouseEventArgs e);
    }
}
