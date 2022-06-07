namespace YANAN.Mail.Client
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.tabControl = new CCWin.SkinControl.SkinTabControl();
            this.tabPageMailBox = new CCWin.SkinControl.SkinTabPage();
            this.tabPageSignature = new CCWin.SkinControl.SkinTabPage();
            this.tabPageTemplate = new CCWin.SkinControl.SkinTabPage();
            this.tabPageFilter = new CCWin.SkinControl.SkinTabPage();
            this.tabPageLabel = new CCWin.SkinControl.SkinTabPage();
            this.tabPageBlacklist = new CCWin.SkinControl.SkinTabPage();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.tabControl.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.tabControl.Controls.Add(this.tabPageMailBox);
            this.tabControl.Controls.Add(this.tabPageSignature);
            this.tabControl.Controls.Add(this.tabPageTemplate);
            this.tabControl.Controls.Add(this.tabPageFilter);
            this.tabControl.Controls.Add(this.tabPageLabel);
            this.tabControl.Controls.Add(this.tabPageBlacklist);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.HeadBack = null;
            this.tabControl.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.tabControl.ItemSize = new System.Drawing.Size(70, 36);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("tabControl.PageArrowDown")));
            this.tabControl.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("tabControl.PageArrowHover")));
            this.tabControl.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("tabControl.PageCloseHover")));
            this.tabControl.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("tabControl.PageCloseNormal")));
            this.tabControl.PageDown = ((System.Drawing.Image)(resources.GetObject("tabControl.PageDown")));
            this.tabControl.PageDownTxtColor = System.Drawing.Color.Red;
            this.tabControl.PageHover = ((System.Drawing.Image)(resources.GetObject("tabControl.PageHover")));
            this.tabControl.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.tabControl.PageNorml = null;
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(704, 641);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 1;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // tabPageMailBox
            // 
            this.tabPageMailBox.BackColor = System.Drawing.Color.White;
            this.tabPageMailBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageMailBox.Location = new System.Drawing.Point(0, 36);
            this.tabPageMailBox.Name = "tabPageMailBox";
            this.tabPageMailBox.Size = new System.Drawing.Size(704, 605);
            this.tabPageMailBox.TabIndex = 0;
            this.tabPageMailBox.TabItemImage = null;
            this.tabPageMailBox.Text = "邮箱设置";
            // 
            // tabPageSignature
            // 
            this.tabPageSignature.BackColor = System.Drawing.Color.White;
            this.tabPageSignature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageSignature.Location = new System.Drawing.Point(0, 36);
            this.tabPageSignature.Name = "tabPageSignature";
            this.tabPageSignature.Size = new System.Drawing.Size(704, 645);
            this.tabPageSignature.TabIndex = 1;
            this.tabPageSignature.TabItemImage = null;
            this.tabPageSignature.Text = "签名";
            // 
            // tabPageTemplate
            // 
            this.tabPageTemplate.BackColor = System.Drawing.Color.White;
            this.tabPageTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageTemplate.Location = new System.Drawing.Point(0, 36);
            this.tabPageTemplate.Name = "tabPageTemplate";
            this.tabPageTemplate.Size = new System.Drawing.Size(704, 645);
            this.tabPageTemplate.TabIndex = 2;
            this.tabPageTemplate.TabItemImage = null;
            this.tabPageTemplate.Text = "模板";
            // 
            // tabPageFilter
            // 
            this.tabPageFilter.BackColor = System.Drawing.Color.White;
            this.tabPageFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageFilter.Location = new System.Drawing.Point(0, 36);
            this.tabPageFilter.Name = "tabPageFilter";
            this.tabPageFilter.Size = new System.Drawing.Size(704, 645);
            this.tabPageFilter.TabIndex = 3;
            this.tabPageFilter.TabItemImage = null;
            this.tabPageFilter.Text = "过滤器";
            // 
            // tabPageLabel
            // 
            this.tabPageLabel.BackColor = System.Drawing.Color.White;
            this.tabPageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageLabel.Location = new System.Drawing.Point(0, 36);
            this.tabPageLabel.Name = "tabPageLabel";
            this.tabPageLabel.Size = new System.Drawing.Size(704, 645);
            this.tabPageLabel.TabIndex = 4;
            this.tabPageLabel.TabItemImage = null;
            this.tabPageLabel.Text = "标签";
            // 
            // tabPageBlacklist
            // 
            this.tabPageBlacklist.BackColor = System.Drawing.Color.White;
            this.tabPageBlacklist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageBlacklist.Location = new System.Drawing.Point(0, 36);
            this.tabPageBlacklist.Name = "tabPageBlacklist";
            this.tabPageBlacklist.Size = new System.Drawing.Size(704, 645);
            this.tabPageBlacklist.TabIndex = 5;
            this.tabPageBlacklist.TabItemImage = null;
            this.tabPageBlacklist.Text = "黑名单";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(704, 641);
            this.Controls.Add(this.tabControl);
            this.Name = "SettingForm";
            this.Text = "邮箱设置";
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private CCWin.SkinControl.SkinTabControl tabControl;
        private CCWin.SkinControl.SkinTabPage tabPageMailBox;
        private CCWin.SkinControl.SkinTabPage tabPageSignature;
        private CCWin.SkinControl.SkinTabPage tabPageTemplate;
        private CCWin.SkinControl.SkinTabPage tabPageFilter;
        private CCWin.SkinControl.SkinTabPage tabPageLabel;
        private CCWin.SkinControl.SkinTabPage tabPageBlacklist;
    }
}