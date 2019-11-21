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

        private NanoD2dCollection controls = new NanoD2dCollection();

        public FormWallpaper(IntPtr workerw)
        {
            this.workerw = workerw;
            InitializeComponent();

            this.Location = new Point(0, 0);
            this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

            NanoD2dPanel panel = new NanoD2dPanel(this, new Point(100, 400), new Size(800, 700));
            controls.Add(panel);

            NanoD2dPanel panel2 = new NanoD2dPanel(this, new Point(65, 15), new Size(250, 340));
            panel.Add(panel2);

            NanoD2dPanel panel3 = new NanoD2dPanel(this, new Point(165, 115), new Size(430, 240));
            panel.Add(panel3);

            NanoD2dPanel panel4 = new NanoD2dPanel(this, new Point(65, 15), new Size(200, 200));
            panel2.Add(panel4);

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
                foreach (var (control, index) in GetAllTextBoxControls(controls).OrderByDescending(s => s.Item2))
                {
                    if (control is IMouseDown clickableControl)
                    {
                        if (control.AbsolutePosition.X <= e.X && control.AbsolutePosition.Y <= e.Y && control.AbsolutePosition.X + control.Size.Width >= e.X && control.AbsolutePosition.Y + control.Size.Height >= e.Y)
                        {
                            clickableControl.OnMouseDown(sender, e);
                            MouseDownList.Add(control);
                            return;
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
                foreach (var (control, index) in GetAllTextBoxControls(controls).OrderByDescending(s => s.Item2))
                {
                    if (control is IMouseClick clickableControl)
                    {
                        if (control.AbsolutePosition.X <= e.X && control.AbsolutePosition.Y <= e.Y && control.AbsolutePosition.X + control.Size.Width >= e.X && control.AbsolutePosition.Y + control.Size.Height >= e.Y)
                        {
                            clickableControl.OnClick(sender, e);
                            return;
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
                foreach (var (control, index) in GetAllTextBoxControls(controls).OrderByDescending(s => s.Item2))
                {
                    if (control is IMouseMove clickableControl)
                    {
                        if (control.AbsolutePosition.X <= e.X && control.AbsolutePosition.Y <= e.Y && control.AbsolutePosition.X + control.Size.Width >= e.X && control.AbsolutePosition.Y + control.Size.Height >= e.Y)
                        {
                            clickableControl.OnMouseMove(sender, e);
                            return;
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

            foreach (var (nanoD2d, index) in GetAllTextBoxControls(controls).OrderBy(s => s.Item2))
            {
                if (nanoD2d is ID2dBase d2dBase)
                {
                    d2dBase.OnRender(g);
                }
            }
        }

        private void FormWallpaper_FormClosed(object sender, FormClosedEventArgs e)
        {
            Unsubscribe();
        }

        private List<Tuple<NanoD2d,int>> GetAllTextBoxControls(NanoD2d container, int index = 0)
        {
            List<Tuple<NanoD2d,int>> controlList = new List<Tuple<NanoD2d, int>>();
            foreach (NanoD2d c in container.Controls)
            {
                controlList.AddRange(GetAllTextBoxControls(c, index + 1));
                if (c != null)
                    controlList.Add(Tuple.Create(c, index));
            }
            return controlList;
        }
    }
}
