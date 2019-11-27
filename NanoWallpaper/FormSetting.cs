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

        private TreeNode sourceNode;

        public FormSetting(FormWallpaper wallpaper)
        {
            InitializeComponent();

            this.wallpaper = wallpaper;

            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900,
                Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            TextFieldImageLocation.Text = SettingData.BackgroundImagePos;

            listBox1.Items.AddRange(SettingData.PluginDataList.ToArray());

            foreach (var pluginDataInfo in SettingData.PluginDataString)
            {
                var tmpStrings = pluginDataInfo.Value.Split('\\');
                comboBox1.Items.Add($"{pluginDataInfo.Key} - {tmpStrings.Skip(tmpStrings.Length - 1).First()}");
            }

            SetTreeViewData(null, wallpaper.controls);
            treeView1.ExpandAll();


            listBox2.Items.Clear();

            foreach (TreeNode node in treeView1.GetAllNodes())
            {
                if (node.Tag is NanoD2dCollection collectionNode)
                {
                    listBox2.Items.Add(node.Text);
                }
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
            wallpaper.showControl(null);
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            if (FormWallpaper.controlNames.Any(s=>s == materialSingleLineTextField6.Text))
            {
                MessageBox.Show("동일한 이름의 컨트롤러가 이미 있습니다!");
                return;
            }

            var name = comboBox1.SelectedItem.ToString().Split('-')[0].Trim();
            var controlD2d = Loader.LoadItem<NanoD2d>(SettingData.PluginDataString[name], name, wallpaper,
                new Point(Convert.ToInt32(materialSingleLineTextField3.Text),
                    Convert.ToInt32(materialSingleLineTextField4.Text)),
                new Size(Convert.ToInt32(materialSingleLineTextField2.Text),
                    Convert.ToInt32(materialSingleLineTextField5.Text)));
            controlD2d.Name = materialSingleLineTextField6.Text;

            wallpaper.controls.Add(controlD2d);

            SetTreeViewData(null, wallpaper.controls);
            treeView1.ExpandAll();

            listBox2.Items.Clear();

            foreach (TreeNode node in treeView1.GetAllNodes())
            {
                if (node.Tag is NanoD2dCollection collectionNode)
                {
                    listBox2.Items.Add(node.Text);
                }
            }
        }

        private void SetTreeViewData(TreeNode startNode, NanoD2dCollection controls)
        {
            foreach (var control in controls)
            {
                TreeNode tmpNode = new TreeNode(control.Name)
                {
                    Tag = control
                };

                if (startNode == null)
                {
                    treeView1.Nodes.Add(tmpNode);
                }
                else
                {
                    startNode.Nodes.Add(tmpNode);
                }

                if (control is NanoD2dCollection subControls)
                {
                    SetTreeViewData(tmpNode, subControls);
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selectedNode = treeView1.SelectedNode.Tag as NanoD2d;
            wallpaper.showControl(selectedNode);
        }

        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            wallpaper.Close();
        }

        private void materialRaisedButton6_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Nodes.Count > 0)
            {
                MessageBox.Show("자식노드가 있으면 지울 수 없습니다.");
            }
            else
            {
                var selectNode = treeView1.SelectedNode;

                treeView1.Nodes.Remove(selectNode);

                if (selectNode.Parent == null)
                {
                    wallpaper.controls.Remove(selectNode.Tag as NanoD2d);
                }
                else
                {
                    (selectNode?.Parent.Tag as NanoD2dCollection)?.Remove(selectNode.Tag as NanoD2d);
                }
            }
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            sourceNode = (TreeNode) e.Item;
            DoDragDrop(e.Item.ToString(), DragDropEffects.Move | DragDropEffects.Copy);
        }
        
        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;

        }
        
        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            Point pos = treeView1.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = treeView1.GetNodeAt(pos);

            if (targetNode != null)
            {
                var nodeCopy = new TreeNode(sourceNode.Text, sourceNode.ImageIndex, sourceNode.SelectedImageIndex)
                {
                    Tag = sourceNode.Tag
                };

                if (sourceNode.Index > targetNode.Index)
                {
                    if (targetNode.Parent == null)
                    {
                        treeView1.Nodes.Insert(targetNode.Index, nodeCopy);
                    }
                    else
                    {
                        targetNode.Parent.Nodes.Insert(targetNode.Index, nodeCopy);
                    }
                }
                else
                {
                    if (targetNode.Parent == null)
                    {
                        treeView1.Nodes.Insert(targetNode.Index + 1, nodeCopy);
                    }
                    else
                    {
                        targetNode.Parent.Nodes.Insert(targetNode.Index + 1, nodeCopy);
                    }
                }

                if (sourceNode.Level != targetNode.Level)
                {
                    if (targetNode.Parent != null)
                    {
                        var controls = targetNode.Parent.Tag as NanoD2dCollection;
                        var selectControl = sourceNode.Tag as NanoD2d;

                        (selectControl?.Parent as NanoD2dCollection)?.Remove(selectControl);
                        controls?.Add(selectControl);
                    }
                    else
                    {
                        var selectControl = sourceNode.Tag as NanoD2d;

                        (selectControl?.Parent as NanoD2dCollection)?.Remove(selectControl);
                        wallpaper.controls.Add(selectControl);
                    }
                }

                sourceNode.Remove();
                treeView1.Invalidate();
            }
        }

        private void materialRaisedButton8_Click(object sender, EventArgs e) // parent
        {
            var selectNode = treeView1.SelectedNode;
            var nodeName = selectNode.Text;

            List<TreeNode> nodes = treeView1.GetAllNodes();
            var node = nodes.Find(s=> s.Text == listBox2.SelectedItem.ToString());

            var nodeCopy = new TreeNode(selectNode.Text, selectNode.ImageIndex, selectNode.SelectedImageIndex)
            {
                Tag = selectNode.Tag
            };

            node.Nodes.Add(nodeCopy);

            selectNode.Remove();
            treeView1.Invalidate();

            var controls = node.Tag as NanoD2dCollection;
            var selectControl = selectNode.Tag as NanoD2d;

            (selectControl?.Parent as NanoD2dCollection)?.Remove(selectControl);
            controls?.Add(selectControl);
            
        }
    }

    public static class TreeNodeEx
    {
        public static List<TreeNode> GetAllNodes(this TreeView _self)
        {
            List<TreeNode> result = new List<TreeNode>();
            foreach (TreeNode child in _self.Nodes)
            {
                result.AddRange(child.GetAllNodes());
            }
            return result;
        }
        public static List<TreeNode> GetAllNodes(this TreeNode _self)
        {
            List<TreeNode> result = new List<TreeNode>();
            result.Add(_self);
            foreach (TreeNode child in _self.Nodes)
            {
                result.AddRange(child.GetAllNodes());
            }
            return result;
        }
    }
}
