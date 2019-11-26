using System.Drawing;
using System.Windows.Forms;
using NanoWallpaper.ControllerInterface;
using unvell.D2DLib;
using unvell.D2DLib.WinForm;

namespace NanoWallpaper.D2dController
{
    public class NanoD2dPanel : NanoD2dCollection, IMouseClick, IMouseDown, IMouseUp, IMouseMove
    {
        public bool isFormMoveAble = true;

        private bool isFormMove = false;
        private Point basePoint;

        public Color BackColor { get; set; }

        public NanoD2dPanel(D2DForm form, Point location, Size size)
        {
            BackColor = Color.FromArgb(120, 66, 66, 66);
            this.Location = location;
            this.Size = size;
            this.Name = "NanoD2dPanel";

            bg = form.Device.CreateBitmapGraphics(new D2DSize(size.Width, size.Height));
        }

        ~NanoD2dPanel()
        {
            bg?.Dispose();
        }

        public override void OnRender(D2DGraphics g)
        {
            var rect = new D2DRect(AbsolutePosition.X, AbsolutePosition.Y, Size.Width, Size.Height);
            g.DrawBitmap(bg, rect);
        }

        public void OnClick(object sender, MouseEventArgs e)
        {
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (isFormMoveAble)
            {
                if (e.Button == MouseButtons.Left)
                {
                    basePoint = new Point(e.X - this.Location.X, e.Y - this.Location.Y);
                    isFormMove = true;
                }
            }
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (isFormMoveAble)
            {
                if (e.Button == MouseButtons.Left)
                {
                    isFormMove = false;
                }
            }

        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isFormMoveAble)
            {
                if (isFormMove)
                {
                    this.Location = new Point(e.X - basePoint.X, e.Y - basePoint.Y);
                }
            }
        }


        public override void OnPaint()
        {
            bg.BeginRender();
            bg.FillRectangle(Location.X, Location.Y, Size.Width, Size.Height, D2DColor.FromGDIColor(BackColor));
            bg.EndRender();
        }

        public override void OnPaint(Point offSet)
        {
            bg.BeginRender();
            bg.FillRectangle(offSet.X, offSet.Y, Size.Width, Size.Height, D2DColor.FromGDIColor(BackColor));
            bg.EndRender();
        }
    }
}