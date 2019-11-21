using System;
using System.Collections;
using System.Collections.Generic;

namespace NanoWallpaper.D2dController
{
    public class NanoD2dEnumerator : IEnumerator<NanoD2d>
    {
        private NanoD2dCollection collection;
        private int curIndex;
        private NanoD2d curNanoD2d;


        public NanoD2dEnumerator(NanoD2dCollection collection)
        {
            this.collection = collection;
            curIndex = -1;
            curNanoD2d = default(NanoD2d);
        }

        void IDisposable.Dispose() { }

        public bool MoveNext()
        {
            if (++curIndex >= collection.Count)
            {
                return false;
            }
            else
            {
                curNanoD2d = collection[curIndex];
            }
            return true;
        }

        public void Reset()
        {
            curIndex = -1;
        }

        public NanoD2d Current => curNanoD2d;

        object IEnumerator.Current => Current;
    }
}