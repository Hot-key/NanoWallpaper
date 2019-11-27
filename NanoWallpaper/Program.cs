using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using NanoWallpaper.Utility;

namespace NanoWallpaper
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Directory.Exists("./Plugin"))
            {
                Directory.CreateDirectory("./Plugin");
            }

            ScreenUtility.PrintVisibleWindowHandles(2);

            IntPtr progman = Win32Api.FindWindow("Progman", null);
            IntPtr result = IntPtr.Zero;

            Win32Api.SendMessageTimeout(progman,
                0x052C,
                new IntPtr(0),
                IntPtr.Zero,
                Win32Api.SendMessageTimeoutFlags.SMTO_NORMAL,
                1000,
                out result);

            ScreenUtility.PrintVisibleWindowHandles(2);

            IntPtr workerw = IntPtr.Zero;

            Win32Api.EnumWindows(new Win32Api.EnumWindowsProc((tophandle, topparamhandle) =>
            {
                IntPtr p = Win32Api.FindWindowEx(tophandle,
                    IntPtr.Zero,
                    "SHELLDLL_DefView",
                    IntPtr.Zero);

                if (p != IntPtr.Zero)
                {
                    // Gets the WorkerW Window after the current one.
                    workerw = Win32Api.FindWindowEx(IntPtr.Zero,
                        tophandle,
                        "WorkerW",
                        IntPtr.Zero);
                }

                return true;
            }), IntPtr.Zero);

            SettingData.LoadSetting();

            Application.Run(new FormWallpaper(workerw));

            RegistryKey theCurrentMachine = Registry.CurrentUser;
            RegistryKey theControlPanel = theCurrentMachine.OpenSubKey("Control Panel");
            RegistryKey theDesktop = theControlPanel.OpenSubKey("Desktop");
            Win32Api.SystemParametersInfo(Win32Api.SPI_SETDESKWALLPAPER, 0, Convert.ToString(theDesktop.GetValue("Wallpaper")), Win32Api.SPIF_UPDATEINIFILE);
        }
    }
}
