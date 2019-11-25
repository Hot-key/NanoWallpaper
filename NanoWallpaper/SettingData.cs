using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoWallpaper.D2dController;
using Newtonsoft.Json;
using Polenter.Serialization;

namespace NanoWallpaper
{
    public static class SettingData
    {
        public static void LoadSetting()
        {
            BackgroundImagePos = "";
            WallPaperFormJosn = "";

            if (File.Exists(SettingData.SettingFilePos))
            {
                var SettingDataString = File.ReadAllLines(SettingData.SettingFilePos);

                BackgroundImagePos = SettingDataString[0];
            }

            if (File.Exists(SettingData.FormFilePos))
            {
                var SettingDataString = File.ReadAllText(SettingData.FormFilePos);

                WallPaperFormJosn = SettingDataString;
            }
        }

        public static void SaveBasicSetting(string data)
        {
            BackgroundImagePos = data;

            StringBuilder settingBuilder = new StringBuilder();

            settingBuilder.AppendLine(BackgroundImagePos); // line 1

            File.WriteAllText(SettingData.SettingFilePos, settingBuilder.ToString());
        }

        public static void SaveWallPaperSetting(NanoD2dCollection mainForm)
        {
            var serializer = new SharpSerializer();

            serializer.Serialize(mainForm, SettingData.FormFilePos);
            //File.WriteAllText(SettingData.FormFilePos, JsonConvert.SerializeObject(mainForm));
        }


        public static string BackgroundImagePos { get; private set; }

        public static string WallPaperFormJosn { get; private set; }

        public static string SettingFilePos = "./setting.dat";
        public static string FormFilePos = "./FormSetting.json";
    }
}
