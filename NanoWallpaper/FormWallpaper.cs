﻿using System;
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
using Newtonsoft.Json;
using Polenter.Serialization;
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

        public NanoD2dCollection controls = new NanoD2dCollection();

        //private FormSetting settingForm;

        public FormWallpaper(IntPtr workerw)
        {
            this.workerw = workerw;
            InitializeComponent();

            this.Location = new Point(0, 0);
            this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

            //settingForm = new FormSetting(this);

            NanoD2dTitlePanel nanoPanel1 = new NanoD2dTitlePanel(this, new Point(700, 400), new Size(800, 700));
            NanoD2dTitlePanel nanoPanel1_1 = new NanoD2dTitlePanel(this, new Point(65, 15), new Size(250, 340));
            NanoD2dTitlePanel nanoPanel1_1_1 = new NanoD2dTitlePanel(this, new Point(65, 15), new Size(200, 200));

            controls.Add(nanoPanel1);
            nanoPanel1.Add(nanoPanel1_1);
            nanoPanel1_1.Add(nanoPanel1_1_1);

            NanoD2dTitlePanel nanoPanel2 = new NanoD2dTitlePanel(this, new Point(100, 400), new Size(800, 700));
            NanoD2dTitlePanel nanoPanel2_1 = new NanoD2dTitlePanel(this, new Point(165, 115), new Size(430, 240));

            controls.Add(nanoPanel2);
            nanoPanel2.Add(nanoPanel2_1);

            NanoD2dTitlePanel nanoPanel3 = new NanoD2dTitlePanel(this, new Point(100, 400), new Size(800, 700));
            NanoD2dPanel nanoPanel3_1 = new NanoD2dPanel(this, new Point(165, 115), new Size(430, 240));

            controls.Add(nanoPanel3);
            nanoPanel3.Add(nanoPanel3_1);

            //NanoGitLog gitLog = new NanoGitLog(this, new Point(1100,300), new Size(722, 108));

            //controls.Add(gitLog);

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
            //settingForm.Show();
            new FormSetting(this).Show();
        }

        public void LoadSetting()
        {
            if (SettingData.BackgroundImagePos.Length > 0)
            {
                this.BackgroundImage = Device.LoadBitmap(SettingData.BackgroundImagePos);
            }
            if (SettingData.WallPaperFormJosn.Length > 0)
            {
                var serializer = new SharpSerializer();

                //this.controls = ;
            }
        }

        protected override void OnRender(D2DGraphics g)
        {
            base.OnRender(g);

            foreach (var (nanoD2d, index) in GetAllTextBoxControls(controls).OrderBy(s => s.Item2))
            {
                nanoD2d.bg?.BeginRender();
                nanoD2d.bg?.Clear(D2DColor.Transparent);
                nanoD2d.bg?.EndRender();
            }

            count++;
            label7.Text = count.ToString();

            foreach (var (nanoD2d, index) in GetAllTextBoxControls(controls).OrderBy(s => s.Item2))
            {
                if (index != 0)
                {
                    nanoD2d.OnPaint(new Point(0, 0));
                }
                else
                {
                    nanoD2d.OnPaint(new Point(0, 0));
                }
            }

            foreach (var (nanoD2d, index) in GetAllTextBoxControls(controls).OrderByDescending(s => s.Item2))
            {
                if (index != 0)
                {
                    var rect = new D2DRect(nanoD2d.Location.X, nanoD2d.Location.Y, nanoD2d.Size.Width, nanoD2d.Size.Height);

                    nanoD2d.Parent?.bg?.BeginRender();
                    nanoD2d.Parent?.bg?.DrawBitmap(nanoD2d.bg, rect);
                    nanoD2d.Parent?.bg?.EndRender();
                }
            }

            foreach (var nanoD2d in controls)
            {
                nanoD2d.OnRender(g);
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

        public void SaveWallpaper()
        {

        }
    }
}
