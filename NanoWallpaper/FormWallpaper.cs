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
using NanoWallpaper.D2dController;
using NanoWallpaper.Utility;
using unvell.D2DLib;
using unvell.D2DLib.WinForm;

namespace NanoWallpaper
{
    public partial class FormWallpaper : D2DForm
    {
        private int count = 0;

        private IntPtr workerw;

        private IKeyboardMouseEvents m_GlobalHook;

        private List<NanoD2d> MouseDownList = new List<NanoD2d>();

        private List<NanoD2d> coltroList = new List<NanoD2d>();

        public FormWallpaper(IntPtr workerw)
        {
            this.workerw = workerw;
            InitializeComponent();

            this.Location = new Point(0, 0);
            this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

            coltroList.Add(new NanoD2dPanel(this, new Point(100,400), new Size(200, 300)));

            ShowFPS = true;
            AnimationDraw = true;
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
            if (Monitor.GetTopWindowText() == "")
            {
                foreach (NanoD2d control in coltroList)
                {
                    if (control is IMouseDown clickableControl)
                    {
                        if (control.Location.X <= e.X && control.Location.Y <= e.Y && control.Location.X + control.controlSize.Width >= e.X && control.Location.Y + control.controlSize.Height >= e.Y)
                        {
                            clickableControl.OnMouseDown(sender, e);
                            MouseDownList.Add(control);
                        }
                    }
                }
            }
        }

        private void GlobalHookMouseUpExt(object sender, MouseEventExtArgs e)
        {
            if (Monitor.GetTopWindowText() == "")
            {
                foreach (NanoD2d control in MouseDownList)
                {
                    if (control is IMouseUp clickableControl)
                    {
                        clickableControl.OnMouseUp(sender, e);
                    }
                }
            }

            MouseDownList.Clear();
        }

        private void GlobalHookMouseClick(object sender, MouseEventArgs e)
        {
            label4.Text = $@"MouseClick:{e.Button}; Point: {e.Location}";

            if (Monitor.GetTopWindowText() == "")
            {
                foreach (NanoD2d control in coltroList)
                {
                    if (control is IMouseClick clickableControl)
                    {
                        if (control.Location.X <= e.X && control.Location.Y <= e.Y && control.Location.X + control.controlSize.Width >= e.X && control.Location.Y + control.controlSize.Height >= e.Y)
                        {
                            clickableControl.OnClick(sender, e);
                        }
                    }
                }
            }
        }

        private void GlobalHookMouseMoveExt(object sender, MouseEventExtArgs e)
        {
            label5.Text = $@"MouseMove:{e.Button}; Point: {e.Location}";

            if (Monitor.GetTopWindowText() == "")
            {
                foreach (var control in coltroList)
                {
                    if (control is IMouseMove clickableControl)
                    {
                        if (control.Location.X <= e.X && control.Location.Y <= e.Y && control.Location.X + control.controlSize.Width >= e.X && control.Location.Y + control.controlSize.Height >= e.Y)
                        {
                            clickableControl.OnMouseMove(sender, e);
                        }
                    }
                }
            }
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.MouseUpExt -= GlobalHookMouseUpExt;
            m_GlobalHook.MouseClick -= GlobalHookMouseClick;
            m_GlobalHook.MouseMoveExt -= GlobalHookMouseMoveExt;
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;

            m_GlobalHook.Dispose();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            new FormSetting(this).Show();
        }

        public void LoadSetting()
        {
            if (SettingData.BackgroundImagePos.Length > 0)
            {
                this.BackgroundImage = Device.LoadBitmap(SettingData.BackgroundImagePos);
            }
        }

        protected override void OnRender(D2DGraphics g)
        {
            base.OnRender(g);

            count++;
            label7.Text = count.ToString();

            coltroList.ForEach(s=>
            {
                if (s is ID2dBase d2dBase)
                {
                    d2dBase.OnRender(g);
                }
            });
        }

        private void FormWallpaper_FormClosed(object sender, FormClosedEventArgs e)
        {
            Unsubscribe();
        }
    }
}
