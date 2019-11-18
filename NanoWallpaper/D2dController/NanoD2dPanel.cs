using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NanoWallpaper.ControllerInterface;
using unvell.D2DLib;
using unvell.D2DLib.WinForm;

namespace NanoWallpaper.D2dController
{
    public class NanoD2dPanel : NanoD2d, IMouseClick, IMouseDown, IMouseUp, IMouseMove
    {
        private readonly D2DForm form;

        private bool isFormMove = false;
        private Point basePoint;

        private Color backColor = Color.FromArgb(180, 66, 66, 66);

        public NanoD2dPanel(D2DForm form, Point location, Size controlSize)
        {
            this.form = form;
            this.Location = location;
            this.controlSize = controlSize;
        }

        public override void OnRender(D2DGraphics g)
        {
            g.FillRectangle(Location.X, Location.Y, controlSize.Width, controlSize.Height, D2DColor.FromGDIColor(backColor));
        }

        public void OnClick(object sender, MouseEventArgs e)
        {
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                backColor = Color.FromArgb(66, 66, 66);
                basePoint = new Point(e.X - this.Location.X, e.Y - this.Location.Y);
                isFormMove = true;
            }
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                backColor = Color.FromArgb(180, 66, 66, 66);
                isFormMove = false;
            }

        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isFormMove)
            {
                this.Location = new Point(e.X - basePoint.X, e.Y - basePoint.Y);
            }
        }
    }
}
