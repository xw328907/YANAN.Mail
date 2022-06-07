namespace YANAN.Mail.Client
{
    partial class MailFolderSelectForm
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
            this.toolSctrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.toolBtn_ok = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_cancel = new System.Windows.Forms.ToolStripButton();
            this.tree_mailfolder = new CCWin.SkinControl.SkinTreeView();
            this.toolSctrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolSctrip1
            // 
            this.toolSctrip1.Arrow = System.Drawing.Color.Black;
            this.toolSctrip1.AutoSize = false;
            this.toolSctrip1.Back = System.Drawing.Color.White;
            this.toolSctrip1.BackRadius = 4;
            this.toolSctrip1.BackRectangle = new System.Drawing.Rectangle(10, 10, 10, 10);
            this.toolSctrip1.Base = System.Drawing.Color.White;
            this.toolSctrip1.BaseFore = System.Drawing.Color.Black;
            this.toolSctrip1.BaseForeAnamorphosis = false;
            this.toolSctrip1.BaseForeAnamorphosisBorder = 4;
            this.toolSctrip1.BaseForeAnamorphosisColor = System.Drawing.Color.Black;
            this.toolSctrip1.BaseForeOffset = new System.Drawing.Point(0, 0);
            this.toolSctrip1.BaseHoverFore = System.Drawing.Color.Black;
            this.toolSctrip1.BaseItemAnamorphosis = true;
            this.toolSctrip1.BaseItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolSctrip1.BaseItemBorderShow = true;
            this.toolSctrip1.BaseItemDown = null;
            this.toolSctrip1.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolSctrip1.BaseItemMouse = null;
            this.toolSctrip1.BaseItemNorml = null;
            this.toolSctrip1.BaseItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolSctrip1.BaseItemRadius = 4;
            this.toolSctrip1.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.toolSctrip1.BaseItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolSctrip1.BindTabControl = null;
            this.toolSctrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.toolSctrip1.Fore = System.Drawing.Color.Black;
            this.toolSctrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.toolSctrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolSctrip1.HoverFore = System.Drawing.Color.Black;
            this.toolSctrip1.ItemAnamorphosis = true;
            this.toolSctrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolSctrip1.ItemBorderShow = true;
            this.toolSctrip1.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolSctrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolSctrip1.ItemRadius = 4;
            this.toolSctrip1.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.toolSctrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtn_ok,
            this.toolBtn_cancel});
            this.toolSctrip1.Location = new System.Drawing.Point(0, 0);
            this.toolSctrip1.Name = "toolSctrip1";
            this.toolSctrip1.Padding = new System.Windows.Forms.Padding(20, 0, 1, 0);
            this.toolSctrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.toolSctrip1.Size = new System.Drawing.Size(244, 34);
            this.toolSctrip1.SkinAllColor = true;
            this.toolSctrip1.TabIndex = 5;
            this.toolSctrip1.Text = "工具栏";
            this.toolSctrip1.TitleAnamorphosis = true;
            this.toolSctrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolSctrip1.TitleRadius = 4;
            this.toolSctrip1.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // toolBtn_ok
            // 
            this.toolBtn_ok.Image = global::YANAN.Mail.Client.Properties.Resources.save;
            this.toolBtn_ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_ok.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.toolBtn_ok.Name = "toolBtn_ok";
            this.toolBtn_ok.Size = new System.Drawing.Size(52, 31);
            this.toolBtn_ok.Text = "确定";
            // 
            // toolBtn_cancel
            // 
            this.toolBtn_cancel.Image = global::YANAN.Mail.Client.Properties.Resources.mail_quit;
            this.toolBtn_cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_cancel.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.toolBtn_cancel.Name = "toolBtn_cancel";
            this.toolBtn_cancel.Size = new System.Drawing.Size(52, 31);
            this.toolBtn_cancel.Text = "取消";
            // 
            // tree_mailfolder
            // 
            this.tree_mailfolder.BackColor = System.Drawing.Color.White;
            this.tree_mailfolder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tree_mailfolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree_mailfolder.ItemHeight = 28;
            this.tree_mailfolder.Location = new System.Drawing.Point(0, 34);
            this.tree_mailfolder.Name = "tree_mailfolder";
            this.tree_mailfolder.ShowLines = false;
            this.tree_mailfolder.ShowPlusMinus = false;
            this.tree_mailfolder.ShowRootLines = false;
            this.tree_mailfolder.Size = new System.Drawing.Size(244, 327);
            this.tree_mailfolder.TabIndex = 6;
            this.tree_mailfolder.TabStop = false;
            // 
            // MailFolderSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 361);
            this.Controls.Add(this.tree_mailfolder);
            this.Controls.Add(this.toolSctrip1);
            this.Name = "MailFolderSelectForm";
            this.Text = "选择文件夹";
            this.toolSctrip1.ResumeLayout(false);
            this.toolSctrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinToolStrip toolSctrip1;
        private System.Windows.Forms.ToolStripButton toolBtn_ok;
        private System.Windows.Forms.ToolStripButton toolBtn_cancel;
        private CCWin.SkinControl.SkinTreeView tree_mailfolder;
    }
}