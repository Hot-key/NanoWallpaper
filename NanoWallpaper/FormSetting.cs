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
    }
}
