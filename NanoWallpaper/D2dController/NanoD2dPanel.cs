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
    public class NanoD2dPanel : NanoD2dCollection, IMouseClick, IMouseDown, IMouseUp, IMouseMove
    {
        private readonly D2DForm form;

        private bool isFormMove = false;
        private Point basePoint;

        private Color backColor = Color.FromArgb(120, 66, 66, 66);
        private Color backColor2 = Color.FromArgb(120, 199, 172, 137);

        public NanoD2dPanel(D2DForm form, Point location, Size size)
        {
            this.form = form;
            this.Location = location;
            this.Size = size;
            this.Name = "NanoD2dPanel";
        }

        public override void OnRender(D2DGraphics g)
        {
            g.FillRectangle(AbsolutePosition.X, AbsolutePosition.Y, Size.Width, Size.Height, D2DColor.FromGDIColor(backColor));
            // 
            g.FillRectangle(AbsolutePosition.X + 5, AbsolutePosition.Y + 55, Size.Width - 10, Size.Height - 60, D2DColor.FromGDIColor(backColor2));

            g.DrawText(this.Name, D2DColor.FromGDIColor(Color.FromArgb(230, 247, 243, 243)), new Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), AbsolutePosition.X + 10, AbsolutePosition.Y + 10);
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
