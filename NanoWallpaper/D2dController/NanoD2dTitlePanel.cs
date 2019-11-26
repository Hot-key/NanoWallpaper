using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NanoWallpaper.ControllerInterface;
using unvell.D2DLib;
using unvell.D2DLib.WinForm;

namespace NanoWallpaper.D2dController
{
    public sealed class NanoD2dTitlePanel : NanoD2dCollection, IMouseClick, IMouseDown, IMouseUp, IMouseMove
    {
        private readonly D2DForm form;

        private bool isFormMove = false;
        private Point basePoint;

        public Color BackColor { get; set; }
        private Color BackColor2 { get; set; }

        public NanoD2dTitlePanel(D2DForm form, Point location, Size size)
        {
            BackColor = Color.FromArgb(120, 66, 66, 66);
            BackColor2 = Color.FromArgb(120, 199, 172, 137);

            this.form = form;
            this.Location = location;
            this.Size = size;
            this.Name = "NanoD2dPanel";

            bg = form.Device.CreateBitmapGraphics(new D2DSize(size.Width, size.Height));

            //this.OnPaint(bg, new Point(0, 0));
        }

        ~NanoD2dTitlePanel()
        {
            bg?.Dispose();
        }

        public override void OnRender(D2DGraphics g)
        {
            var rect = new D2DRect(AbsolutePosition.X, AbsolutePosition.Y, Size.Width, Size.Height);
            g.DrawBitmap(bg, rect);
            //g.FillRectangle(AbsolutePosition.X, AbsolutePosition.Y, Size.Width, Size.Height, D2DColor.FromGDIColor(backColor));

            //g.FillRectangle(AbsolutePosition.X + 5, AbsolutePosition.Y + 55, Size.Width - 10, Size.Height - 60, D2DColor.FromGDIColor(backColor2));

            //g.DrawText(this.Name, D2DColor.FromGDIColor(Color.FromArgb(230, 247, 243, 243)), new Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point), AbsolutePosition.X + 10, AbsolutePosition.Y + 10);
        }

        public void OnClick(object sender, MouseEventArgs e)
        {
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                BackColor = Color.FromArgb(66, 66, 66);
                basePoint = new Point(e.X - this.Location.X, e.Y - this.Location.Y);
                isFormMove = true;
            }
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                BackColor = Color.FromArgb(180, 66, 66, 66);
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


        public override void OnPaint()
        {
            bg.BeginRender();
            bg.FillRectangle(Location.X, Location.Y, Size.Width, Size.Height, D2DColor.FromGDIColor(BackColor));
            bg.FillRectangle(Location.X + 5, Location.Y + 55, Size.Width - 10, Size.Height - 60, D2DColor.FromGDIColor(BackColor2));
            bg.DrawText(this.Name, D2DColor.FromGDIColor(Color.FromArgb(230, 247, 243, 243)), new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point), Location.X + 10, Location.Y + 10);
            bg.EndRender();
        }

        public override void OnPaint(Point offSet)
        {
            bg.BeginRender();
            bg.FillRectangle(offSet.X, offSet.Y, Size.Width, Size.Height, D2DColor.FromGDIColor(BackColor));
            bg.FillRectangle(offSet.X + 5, offSet.Y + 55, Size.Width - 10, Size.Height - 60, D2DColor.FromGDIColor(BackColor2));
            bg.DrawText(this.Name, D2DColor.FromGDIColor(Color.FromArgb(230, 247, 243, 243)), new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point), offSet.X + 10, offSet.Y + 10);
            bg.EndRender();
        }
    }
}
