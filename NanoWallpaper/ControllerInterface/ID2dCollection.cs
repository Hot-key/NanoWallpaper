using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoWallpaper.ControllerInterface
{
    interface ID2dCollection<T> : ICollection<T> where T : ID2dBase
    {
        
    }
}
