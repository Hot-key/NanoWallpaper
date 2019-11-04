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
using NanoWallpaper.Utility;

namespace NanoWallpaper
{
    public partial class Form1 : Form
    {
        private IntPtr workerw;

        private IKeyboardMouseEvents m_GlobalHook;

        public Form1(IntPtr workerw)
        {
            this.workerw = workerw;
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = Monitor.GetTopWindowName();
            label2.Text = Monitor.GetTopWindowText();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Win32Api.SetParent(this.Handle, workerw);
            Subscribe();
        }

        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
            m_GlobalHook.MouseMoveExt += GlobalHookMouseMoveExt;
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            label3.Text = $@"KeyPress:{e.KeyChar}";
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            label4.Text = $@"MouseDown:{e.Button}; Point: {e.Location}";

            // MK_LBUTTON = 0x0001
            Win32Api.SendMessage(Win32Api.FindWindow(null, "제목 없음 - 그림판"), WindowsMessages.WM_LBUTTONDOWN, 1, MakeLParam(e.Location.X, e.Location.Y));
            button1.Text = "asdf1";
            // MK_LBUTTON = 0x0001
            Win32Api.SendMessage(Win32Api.FindWindow(null, "제목 없음 - 그림판"), WindowsMessages.WM_LBUTTONUP, 0, MakeLParam(e.Location.X, e.Location.Y));
            button1.Text = "asdf2";
        }

        public int MakeLParam(int LoWord, int HiWord)
        {
            return (int)((HiWord << 16) | (LoWord & 0xFFFF));
        }


        private void GlobalHookMouseMoveExt(object sender, MouseEventExtArgs e)
        {
            label5.Text = $@"MouseMove:{e.Button}; Point: {e.Location}";
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;

            //It is recommened to dispose it
            m_GlobalHook.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("asdf1234");
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            button1.Text = "asdf";
        }
    }
}
