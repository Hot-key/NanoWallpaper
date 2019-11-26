using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using NanoWallpaper.ControllerInterface;
using NanoWallpaper.D2dController;
using unvell.D2DLib;
using unvell.D2DLib.WinForm;

namespace GithubHistory
{
    public class NanoGitLog : NanoD2d, IMouseClick, IMouseDown, IMouseUp, IMouseMove
    {
        private readonly D2DForm form;

        private bool isRender = false;

        private bool isFormMove = false;
        private Point basePoint;

        private System.Threading.Timer CommitTimer;

        HtmlWeb gitPageWeb = new HtmlWeb();

        public string GitName = "Hot-key";

        private List<List<Color>> gitColor = new List<List<Color>>();

        public NanoGitLog(D2DForm form, Point location, Size size)
        {
            this.form = form;
            this.Location = location;
            this.Size = size;
            this.Name = "NanoGitLog";

            CommitTimer = new System.Threading.Timer(GitParser, null, 0, 1000 * 60);
            // bg는 초기화 작업을 하므로 bg2 로 사용한다.
            bg = form.Device.CreateBitmapGraphics(new D2DSize(size.Width, size.Height));
        }

        ~NanoGitLog()
        {
            bg?.Dispose();
        }

        public override void OnRender(D2DGraphics g)
        {
            var rect = new D2DRect(AbsolutePosition.X, AbsolutePosition.Y, Size.Width, Size.Height);
            g.DrawBitmap(bg, rect);
        }

        public void OnClick(object sender, MouseEventArgs e)
        {
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                basePoint = new Point(e.X - this.Location.X, e.Y - this.Location.Y);
                isFormMove = true;
            }
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isFormMove = false;
            }

        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isFormMove)
            {
                this.Location = new Point(e.X - basePoint.X, e.Y - basePoint.Y);
            }
        }

        public override void OnPaint()
        {
            bg.BeginRender();
            bg.FillRectangle(0, 0, this.Size.Width, this.Size.Height, D2DColor.FromGDIColor(Color.FromArgb(230, 225, 225, 225)));
            for (var i = 0; i < gitColor.Count; i++)
            {
                for (var j = 0; j < gitColor[i].Count; j++)
                {
                    bg.FillRectangle(13 * i, 13 * j, 10, 10, D2DColor.FromGDIColor(gitColor[i][j]));
                }
            }
            isRender = false;
            bg.EndRender();
        }

        public override void OnPaint(Point offSet)
        {
            bg.BeginRender();
            bg.FillRectangle(offSet.X, offSet.Y, this.Size.Width, this.Size.Height, D2DColor.FromGDIColor(Color.FromArgb(230, 225, 225, 225)));
            for (var i = 0; i < gitColor.Count; i++)
            {
                for (var j = 0; j < gitColor[i].Count; j++)
                {
                    bg.FillRectangle(offSet.X + 13 * i, offSet.Y + 13 * j, 10, 10, D2DColor.FromGDIColor(gitColor[i][j]));
                }
            }
            isRender = false;
            bg.EndRender();
        }

        private void GitParser(object _)
        {
            if (GitName.Length > 1)
            {
                gitColor.Clear();

                var gitPageHtml = gitPageWeb.Load($"https://github.com/users/{GitName}/contributions");

                var farm = gitPageHtml.DocumentNode.SelectSingleNode("/div/div[1]/div/div[1]/svg/g");
                var yearFarm = farm.SelectNodes("g");

                for (int i = 0; i < yearFarm.Count; i++)
                {
                    gitColor.Add(new List<Color>());
                    var weekFarm = yearFarm[i].SelectNodes("rect");
                    foreach (var dayFarm in weekFarm)
                    {
                        gitColor[i].Add(ColorTranslator.FromHtml(dayFarm.Attributes["fill"].Value));
                    }
                }

                isRender = true;
            }
            else
            {
                MessageBox.Show("닉네임 입력이 필요합니다.", "오류!");
            }
        }
    }
}
