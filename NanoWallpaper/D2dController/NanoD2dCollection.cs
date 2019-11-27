using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoWallpaper.ControllerInterface;
using Newtonsoft.Json.Linq;

namespace NanoWallpaper.D2dController
{
    public class NanoD2dCollection : NanoD2d, ID2dCollection<NanoD2d>
    {
        public List<NanoD2d> innerCol;
        public override Point Location { get; set; }
        public int Count => innerCol.Count;
        public bool IsReadOnly => false;

        public NanoD2dCollection()
        {
            innerCol = new List<NanoD2d>();
        }

        public NanoD2dCollection(params NanoD2d[] initData)
        {
            innerCol = new List<NanoD2d>(initData);
        }

        public NanoD2d this[int index]
        {
            get => innerCol[index];
            set => innerCol[index] = value;
        }

        public IEnumerator<NanoD2d> GetEnumerator()
        {
            return new NanoD2dEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new NanoD2dEnumerator(this);
        }

        public void Add(NanoD2d item)
        {
            item.Parent = this;
            innerCol.Add(item);
        }

        public void AddRange(params NanoD2d[] items)
        {
            foreach (var item in items)
            {
                item.Parent = this;
                innerCol.Add(item);
            }
        }

        public void Clear()
        {
            innerCol.Clear();
        }

        public bool Contains(NanoD2d item)
        {
            bool found = false;

            foreach (NanoD2d bx in innerCol)
            {
                // Equality defined by the Box
                // class's implmentation of IEquatable<T>.
                if (bx.Equals(item))
                {
                    found = true;
                }
            }

            return found;
        }

        public bool Contains(NanoD2d item, EqualityComparer<NanoD2d> comp)
        {
            bool found = false;

            foreach (NanoD2d bx in innerCol)
            {
                if (comp.Equals(bx, item))
                {
                    found = true;
                }
            }

            return found;
        }

        public void CopyTo(NanoD2d[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("The array cannot be null.");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
            if (Count > array.Length - arrayIndex + 1)
                throw new ArgumentException("The destination array has fewer elements than the collection.");

            for (int i = 0; i < innerCol.Count; i++)
            {
                array[i + arrayIndex] = innerCol[i];
            }
        }

        public bool Remove(NanoD2d item)
        {
            bool result = false;

            // Iterate the inner collection to 
            // find the box to be removed.
            for (int i = 0; i < innerCol.Count; i++)
            {
                NanoD2d curBox = innerCol[i];

                if (Equals(curBox, item))
                {
                    innerCol.RemoveAt(i);
                    result = true;
                    break;
                }
            }
            return result;
        }

        public override JObject OnSave()
        {
            JObject tmpJObject = new JObject();
            JObject sizeJObject = new JObject();
            JObject locationJObject = new JObject();
            JArray subDataJArray = new JArray();

            sizeJObject.Add("Height", this.Size.Height);
            sizeJObject.Add("Width", this.Size.Width);

            locationJObject.Add("X", this.Location.X);
            locationJObject.Add("Y", this.Location.Y);

            tmpJObject.Add("Size", sizeJObject);
            tmpJObject.Add("Location", locationJObject);
            tmpJObject.Add("Name", Name);
            tmpJObject.Add("Type", this.GetType().FullName);

            foreach (var nanoD2d in innerCol)
            {
                subDataJArray.Add(nanoD2d.OnSave());
            }

            tmpJObject.Add("SubData", subDataJArray);

            return tmpJObject;

        }
    }
}
