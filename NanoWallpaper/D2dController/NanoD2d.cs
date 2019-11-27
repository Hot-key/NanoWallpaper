using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoWallpaper.ControllerInterface;
using Newtonsoft.Json.Linq;
using unvell.D2DLib;

namespace NanoWallpaper.D2dController
{
    [Serializable()]
    public class NanoD2d : ID2dBase
    {
        public Size Size { get; set; }

        public NanoD2d Parent { get; set; }

        public string Name { get; set; }

        public virtual Point Location { get; set; }

        public D2DBitmapGraphics bg { get; protected set; }

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

        public virtual void OnPaint()
        {
        }

        public virtual void OnPaint(Point point)
        {
        }

        public virtual JObject OnSave()
        {
            JObject tmpJObject = new JObject();
            JObject sizeJObject = new JObject();
            JObject locationJObject = new JObject();

            sizeJObject.Add("Height", this.Size.Height);
            sizeJObject.Add("Width", this.Size.Width);

            locationJObject.Add("X", this.Location.X);
            locationJObject.Add("Y", this.Location.Y);

            tmpJObject.Add("Size", sizeJObject);
            tmpJObject.Add("Location", locationJObject);
            tmpJObject.Add("Name", Name);
            tmpJObject.Add("Type", this.GetType().FullName);

            if (this is NanoD2dCollection collection)
            {
                JArray subDataJArray = new JArray();

                foreach (var nanoD2d in collection.innerCol)
                {
                    subDataJArray.Add(nanoD2d.OnSave());
                }

                tmpJObject.Add("SubData", subDataJArray);
            }


            return tmpJObject;
        }
    }
}
