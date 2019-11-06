using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NanoWallpaper.ControllerInterface;

namespace NanoWallpaper.NanoUserControl
{
    public partial class TestUserControl : UserControl , IMouseClick, IMouseUp, IMouseDown, IMouseMove
    {
        private bool isFormMove = false;
        private Point basePoint;

        public TestUserControl()
        {
            InitializeComponent();
        }

        private void TestUserControl_Load(object sender, EventArgs e)
        {

        }

        public void OnClick(object sender, System.EventArgs e)
        {
            this.BackColor = Color.Aqua;
        }
        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isFormMove = false;
            }
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            label2.Text = e.Location.ToString();
            if (e.Button == MouseButtons.Left)
            {
                basePoint = new Point(e.X - this.Location.X, e.Y - this.Location.Y); ;
                isFormMove = true;
            }
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            label5.Text = e.Location.ToString();
            label6.Text = basePoint.ToString();
            if (isFormMove)
            {
                var absolutePoint = this.PointToScreen(Point.Empty);
                this.Location = new Point(e.X - basePoint.X, e.Y- basePoint.Y);
            }
        }

        private void TestUserControl_MouseUp(object sender, MouseEventArgs e)
        {
            label3.Text = "OnMouseUp";
            OnMouseUp(sender, e);
        }

        private void TestUserControl_MouseDown(object sender, MouseEventArgs e)
        {
            label3.Text = "OnMouseDown";
            OnMouseDown(sender, e);
        }

        private void TestUserControl_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Text = "OnMouseMove";
            OnMouseMove(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = this.Location.ToString();
            label4.Text = isFormMove.ToString();
        }
    }
}
