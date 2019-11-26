using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NanoWallpaper.D2dController;
using NanoWallpaper.Utility.Plugin;
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
            PluginDataList = new List<string>();
            PluginDataString = new Dictionary<string, string>();

            if (File.Exists(SettingData.SettingFilePos))
            {
                var settingDataString = File.ReadAllLines(SettingData.SettingFilePos);

                BackgroundImagePos = settingDataString[0];
            }

            if (File.Exists(SettingData.FormFilePos))
            {
                var settingDataString = File.ReadAllText(SettingData.FormFilePos);

                WallPaperFormJosn = settingDataString;
            }

            if (File.Exists(SettingData.PluginFilePos))
            {
                var settingDataString = File.ReadAllLines(SettingData.PluginFilePos);
                PluginDataList = settingDataString.ToList();

                foreach (var dataLine in settingDataString)
                {
                    var tmpName = Loader.LoadPlugin<NanoD2d>(dataLine);

                    tmpName.ForEach(s =>
                    {
                        PluginDataString.Add(s, dataLine);
                    });
                }

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

        public static void SavePluginSetting(params string[] pluginList)
        {
            foreach (var pluginPos in pluginList)
            {
                PluginDataList.Add(pluginPos);
            }

            StringBuilder settingBuilder = new StringBuilder();

            foreach (var pluginPos in pluginList)
            {
                settingBuilder.AppendLine(pluginPos);
            }

            File.WriteAllText(SettingData.PluginFilePos, settingBuilder.ToString());
        }

        public static string BackgroundImagePos { get; private set; }
        public static string WallPaperFormJosn { get; private set; }
        public static List<string> PluginDataList { get; private set; }
        public static Dictionary<string, string> PluginDataString { get; private set; }

        public static string SettingFilePos = "./setting.dat";
        public static string FormFilePos = "./FormSetting.json";
        public static string PluginFilePos = "./Plugin/data.dat";
    }
}
