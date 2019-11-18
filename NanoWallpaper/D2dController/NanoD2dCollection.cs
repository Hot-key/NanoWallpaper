using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoWallpaper.ControllerInterface;

namespace NanoWallpaper.D2dController
{
    public class NanoD2dCollection : ID2dCollection<NanoD2d>
    {
        public IEnumerator<NanoD2d> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(NanoD2d item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(NanoD2d item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(NanoD2d[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(NanoD2d item)
        {
            throw new NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
    }
}
