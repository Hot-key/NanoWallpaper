using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unvell.D2DLib.WinForm;

namespace NanoWallpaper.Utility.Plugin
{
    public static class Loader
    {
        public static List<string> LoadPlugin<T>(params string[] fileNames)
        {
            Type interfaceType = typeof(T);
            List<string> implementations = new List<string>();

            foreach (var file in fileNames)
            {
                try
                {
                    foreach (var type in System.Reflection.Assembly.LoadFile(file).GetTypes())
                    {
                        if (interfaceType.IsAssignableFrom(type) && interfaceType != type)
                        {
                            implementations.Add(type.Name);
                        }
                    }
                }
                catch { }
            }
            return implementations;
        }

        //public static T LoadItem<T>(string fileName, string name, D2DForm form, Point location, Size size)
        public static T LoadItem<T>(string fileName, string name, params object[] initData)
        {
            Type interfaceType = typeof(T);

            foreach (var type in System.Reflection.Assembly.LoadFile(fileName).GetTypes())
            {
                if (interfaceType.IsAssignableFrom(type) && interfaceType != type && type.Name == name)
                {
                    T instance = (T) Activator.CreateInstance(type, initData);

                    return instance;
                }
            }

            return default;
        }

        //public static T LoadItem<T>(string[] fileArray, string name, D2DForm form, Point location, Size size)
        public static T LoadItem<T>(string[] fileArray, string name, params object[] initData)
        {
            Type interfaceType = typeof(T);

            foreach (var fileName in fileArray)
            {
                foreach (var type in System.Reflection.Assembly.LoadFile(fileName).GetTypes())
                {
                    if (interfaceType.IsAssignableFrom(type) && interfaceType != type && type.FullName == name)
                    {
                        T instance = (T)Activator.CreateInstance(type, initData);

                        return instance;
                    }
                }
            }
            return default;
        }
    }
}
