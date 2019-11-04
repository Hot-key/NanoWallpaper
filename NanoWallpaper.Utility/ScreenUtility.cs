using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NanoWallpaper.Utility
{
    public static class ScreenUtility
    {
        public static void PrintVisibleWindowHandles(IntPtr hwnd, int maxLevel = -1, int level = 0)
        {
            bool isVisible = Win32Api.IsWindowVisible(hwnd);

            if (isVisible && (maxLevel == -1 || level <= maxLevel))
            {
                StringBuilder className = new StringBuilder(256);
                Win32Api.GetClassName(hwnd, className, className.Capacity);

                StringBuilder windowTitle = new StringBuilder(256);
                Win32Api.GetWindowText(hwnd, windowTitle, className.Capacity);

                Console.WriteLine("".PadLeft(level * 2) + "0x{0:X8} \"{1}\" {2}", hwnd.ToInt64(), windowTitle, className);

                level++;

                // Enumerates all child windows of the current window
                Win32Api.EnumChildWindows(hwnd, new Win32Api.EnumWindowsProc((childhandle, childparamhandle) =>
                {
                    PrintVisibleWindowHandles(childhandle, maxLevel, level);
                    return true;
                }), IntPtr.Zero);
            }
        }
        public static void PrintVisibleWindowHandles(int maxLevel = -1)
        {
            // Enumerates all existing top window handles. This includes open and visible windows, as well as invisible windows.
            Win32Api.EnumWindows(new Win32Api.EnumWindowsProc((tophandle, topparamhandle) =>
            {
                PrintVisibleWindowHandles(tophandle, maxLevel);
                return true;
            }), IntPtr.Zero);
        }
    }
}
