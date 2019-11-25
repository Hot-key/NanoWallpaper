namespace NanoWallpaper
{
    partial class FormSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.BasicSetting = new System.Windows.Forms.TabPage();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.TextFieldImageLocation = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.WallPaperSetting = new System.Windows.Forms.TabPage();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialRaisedButton2 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialTabControl1.SuspendLayout();
            this.BasicSetting.SuspendLayout();
            this.WallPaperSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTabControl1.Controls.Add(this.BasicSetting);
            this.materialTabControl1.Controls.Add(this.WallPaperSetting);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(0, 109);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(554, 405);
            this.materialTabControl1.TabIndex = 0;
            // 
            // BasicSetting
            // 
            this.BasicSetting.BackColor = System.Drawing.Color.White;
            this.BasicSetting.Controls.Add(this.materialRaisedButton1);
            this.BasicSetting.Controls.Add(this.TextFieldImageLocation);
            this.BasicSetting.Location = new System.Drawing.Point(4, 22);
            this.BasicSetting.Name = "BasicSetting";
            this.BasicSetting.Padding = new System.Windows.Forms.Padding(3);
            this.BasicSetting.Size = new System.Drawing.Size(546, 379);
            this.BasicSetting.TabIndex = 1;
            this.BasicSetting.Text = "Basic Setting";
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.materialRaisedButton1.AutoSize = true;
            this.materialRaisedButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Icon = null;
            this.materialRaisedButton1.Location = new System.Drawing.Point(426, 337);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(114, 36);
            this.materialRaisedButton1.TabIndex = 2;
            this.materialRaisedButton1.Text = "Save Setting";
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.materialRaisedButton1_Click);
            // 
            // TextFieldImageLocation
            // 
            this.TextFieldImageLocation.BackColor = System.Drawing.Color.White;
            this.TextFieldImageLocation.Depth = 0;
            this.TextFieldImageLocation.Hint = "Set Image Location";
            this.TextFieldImageLocation.Location = new System.Drawing.Point(8, 6);
            this.TextFieldImageLocation.MaxLength = 32767;
            this.TextFieldImageLocation.MouseState = MaterialSkin.MouseState.HOVER;
            this.TextFieldImageLocation.Name = "TextFieldImageLocation";
            this.TextFieldImageLocation.PasswordChar = '\0';
            this.TextFieldImageLocation.SelectedText = "";
            this.TextFieldImageLocation.SelectionLength = 0;
            this.TextFieldImageLocation.SelectionStart = 0;
            this.TextFieldImageLocation.Size = new System.Drawing.Size(679, 23);
            this.TextFieldImageLocation.TabIndex = 0;
            this.TextFieldImageLocation.TabStop = false;
            this.TextFieldImageLocation.UseSystemPasswordChar = false;
            this.TextFieldImageLocation.Click += new System.EventHandler(this.TextFieldImageLocation_Click);
            // 
            // WallPaperSetting
            // 
            this.WallPaperSetting.Controls.Add(this.materialRaisedButton2);
            this.WallPaperSetting.Location = new System.Drawing.Point(4, 22);
            this.WallPaperSetting.Name = "WallPaperSetting";
            this.WallPaperSetting.Padding = new System.Windows.Forms.Padding(3);
            this.WallPaperSetting.Size = new System.Drawing.Size(546, 379);
            this.WallPaperSetting.TabIndex = 2;
            this.WallPaperSetting.Text = "WallPaperSetting";
            this.WallPaperSetting.UseVisualStyleBackColor = true;
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Location = new System.Drawing.Point(0, 59);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(566, 44);
            this.materialTabSelector1.TabIndex = 1;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // materialRaisedButton2
            // 
            this.materialRaisedButton2.AutoSize = true;
            this.materialRaisedButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialRaisedButton2.Depth = 0;
            this.materialRaisedButton2.Icon = null;
            this.materialRaisedButton2.Location = new System.Drawing.Point(402, 337);
            this.materialRaisedButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton2.Name = "materialRaisedButton2";
            this.materialRaisedButton2.Primary = true;
            this.materialRaisedButton2.Size = new System.Drawing.Size(138, 36);
            this.materialRaisedButton2.TabIndex = 0;
            this.materialRaisedButton2.Text = "Save WallPaper";
            this.materialRaisedButton2.UseVisualStyleBackColor = true;
            this.materialRaisedButton2.Click += new System.EventHandler(this.materialRaisedButton2_Click);
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 526);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.materialTabControl1);
            this.Name = "FormSetting";
            this.Text = "FormSetting";
            this.materialTabControl1.ResumeLayout(false);
            this.BasicSetting.ResumeLayout(false);
            this.BasicSetting.PerformLayout();
            this.WallPaperSetting.ResumeLayout(false);
            this.WallPaperSetting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage BasicSetting;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialSingleLineTextField TextFieldImageLocation;
        private System.Windows.Forms.TabPage WallPaperSetting;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton2;
    }
}