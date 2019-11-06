using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoWallpaper
{
    public static class SettingData
    {
        public static void LoadSetting()
        {
            if (File.Exists(SettingData.SettingFilePos))
            {
                var SettingDataString = File.ReadAllLines(SettingData.SettingFilePos);

                BackgroundImagePos = SettingDataString[0];
            }
        }
        public static void SaveSetting()
        {
            StringBuilder settingBuilder = new StringBuilder();

            settingBuilder.AppendLine(BackgroundImagePos); // line 1

            File.WriteAllText(SettingData.SettingFilePos, settingBuilder.ToString());
        }

        public static string BackgroundImagePos = ""; // line 1

        public static string SettingFilePos = "./setting.dat";
    }
}
