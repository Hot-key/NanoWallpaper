using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using NanoWallpaper.D2dController;
using NanoWallpaper.Utility.Plugin;

namespace NanoWallpaper
{
    public partial class FormSetting : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        private FormWallpaper wallpaper;

        public FormSetting(FormWallpaper wallpaper)
        {
            InitializeComponent();

            this.wallpaper = wallpaper;

            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            TextFieldImageLocation.Text = SettingData.BackgroundImagePos;

            listBox1.Items.AddRange(SettingData.PluginDataList.ToArray());

            foreach (var pluginDataInfo in SettingData.PluginDataString)
            {
                var tmpStrings = pluginDataInfo.Value.Split('\\');
                comboBox1.Items.Add($"{pluginDataInfo.Key} - {tmpStrings.Skip(tmpStrings.Length - 1).First()}");
            }
        }

        private void TextFieldImageLocation_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();

            fd.ShowDialog();

            if (fd.FileName.Length > 1)
            {
                TextFieldImageLocation.Text = fd.FileName;
            }
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            SettingData.SaveBasicSetting(TextFieldImageLocation.Text);
            wallpaper.LoadSetting();
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            SettingData.SaveWallPaperSetting(wallpaper.controls);
            wallpaper.LoadSetting();
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            SettingData.SavePluginSetting(listBox1.Items.Cast<String>().ToArray());
        }

        private void materialSingleLineTextField1_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();

            fd.ShowDialog();

            if (fd.FileName.Length > 1)
            {
                TextFieldImageLocation.Text = fd.FileName;
                listBox1.Items.Add(fd.FileName);
            }
        }

        private void FormSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            var name = comboBox1.SelectedItem.ToString().Split('-')[0].Trim();
            var controlD2d = Loader.LoadItem<NanoD2d>(SettingData.PluginDataString[name], name, wallpaper, new Point(Convert.ToInt32(materialSingleLineTextField3.Text), Convert.ToInt32(materialSingleLineTextField4.Text)), new Size(Convert.ToInt32(materialSingleLineTextField2.Text), Convert.ToInt32(materialSingleLineTextField5.Text)));

            wallpaper.controls.Add(controlD2d);
        }
    }
}
