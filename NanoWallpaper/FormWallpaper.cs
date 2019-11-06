using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using NanoWallpaper.ControllerInterface;
using NanoWallpaper.Utility;

namespace NanoWallpaper
{
    public partial class FormWallpaper : Form
    {
        private IntPtr workerw;

        private IKeyboardMouseEvents m_GlobalHook;

        private List<Control> MouseDownList = new List<Control>();

        public FormWallpaper(IntPtr workerw)
        {
            this.workerw = workerw;
            InitializeComponent();

            this.Location = new Point(0, 0);
            this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = Monitor.GetTopWindowName();
            label2.Text = Monitor.GetTopWindowText();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label6.Text = this.Handle.ToString();
            Win32Api.SetParent(this.Handle, workerw);
            label7.Text = this.Handle.ToString();
            Subscribe();

            LoadSetting();
        }

        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.MouseUpExt += GlobalHookMouseUpExt;
            m_GlobalHook.MouseClick += GlobalHookMouseClick;
            m_GlobalHook.MouseMoveExt += GlobalHookMouseMoveExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            label3.Text = $@"KeyPress:{e.KeyChar}";
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            foreach (Control control in GetAllControls(this))
            {
                if (control is IMouseDown clickableControl)
                {
                    var absolutePoint = control.PointToScreen(Point.Empty);

                    if (absolutePoint.X <= e.X && absolutePoint.Y <= e.Y && absolutePoint.X + control.Width >= e.X && absolutePoint.Y + control.Height >= e.Y)
                    {
                        clickableControl.OnMouseDown(sender, e);
                        MouseDownList.Add(control);
                    }
                }
            }
        }

        private void GlobalHookMouseUpExt(object sender, MouseEventExtArgs e)
        {
            foreach (Control control in MouseDownList)
            {
                if (control is IMouseUp clickableControl)
                {
                    clickableControl.OnMouseUp(sender, e);
                }
            }

            MouseDownList.Clear();
        }

        private void GlobalHookMouseClick(object sender, MouseEventArgs e)
        {
            label4.Text = $@"MouseClick:{e.Button}; Point: {e.Location}";

            foreach (Control control in GetAllControls(this))
            {
                if (control is IMouseClick clickableControl)
                {
                    var absolutePoint = control.PointToScreen(Point.Empty);

                    if (absolutePoint.X <= e.X && absolutePoint.Y <= e.Y && absolutePoint.X + control.Width >= e.X && absolutePoint.Y + control.Height >= e.Y)
                    {
                        clickableControl.OnClick(sender, e);
                    }
                }
            }
        }

        public int MakeLParam(int LoWord, int HiWord)
        {
            return (int)((HiWord << 16) | (LoWord & 0xFFFF));
        }


        private void GlobalHookMouseMoveExt(object sender, MouseEventExtArgs e)
        {
            label5.Text = $@"MouseMove:{e.Button}; Point: {e.Location}";

            foreach (Control control in GetAllControls(this))
            {
                if (control is IMouseMove clickableControl)
                {
                    var absolutePoint = control.PointToScreen(Point.Empty);
                    if (absolutePoint.X <= e.X && absolutePoint.Y <= e.Y && absolutePoint.X + control.Width >= e.X && absolutePoint.Y + control.Height >= e.Y)
                    {
                        clickableControl.OnMouseMove(sender, e);
                    }
                }
            }
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;

            //It is recommened to dispose it
            m_GlobalHook.Dispose();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            label7.Text = "asdf";
        }

        private List<Control> GetAllControls(Control container, List<Control> list)
        {
            foreach (Control c in container.Controls)
            {
                list.Add(c);
                if (c.Controls.Count > 0)
                {
                    list = GetAllControls(c, list);
                }
            }

            return list;
        }

        private List<Control> GetAllControls(Control container)
        {
            return GetAllControls(container, new List<Control>());
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            new FormSetting(this).Show();
        }

        public void LoadSetting()
        {
            if (SettingData.BackgroundImagePos.Length > 0)
            {
                this.BackgroundImage = Image.FromFile(SettingData.BackgroundImagePos);
            }
        }
    }
}
