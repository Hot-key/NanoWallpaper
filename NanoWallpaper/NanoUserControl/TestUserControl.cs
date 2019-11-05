using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NanoWallpaper.ControllerInterface;

namespace NanoWallpaper.NanoUserControl
{
    public partial class TestUserControl : UserControl , IClickable
    {
        public TestUserControl()
        {
            InitializeComponent();
        }

        private void TestUserControl_Load(object sender, EventArgs e)
        {

        }

        public void OnClick(object sender, System.EventArgs e)
        {
            this.BackColor = Color.Aqua;
        }
    }
}
