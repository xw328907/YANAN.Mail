namespace YANAN.Mail.Client
{
    partial class MailViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailViewForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.toolBtn_reply = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_replyAll = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_forward = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_move = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_delete = new System.Windows.Forms.ToolStripButton();
            this.panel_body = new System.Windows.Forms.Panel();
            this.toolBtn_quit = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.skinToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.skinToolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 36);
            this.panel1.TabIndex = 0;
            // 
            // skinToolStrip1
            // 
            this.skinToolStrip1.Arrow = System.Drawing.Color.Black;
            this.skinToolStrip1.AutoSize = false;
            this.skinToolStrip1.Back = System.Drawing.Color.White;
            this.skinToolStrip1.BackRadius = 4;
            this.skinToolStrip1.BackRectangle = new System.Drawing.Rectangle(10, 10, 10, 10);
            this.skinToolStrip1.Base = System.Drawing.Color.White;
            this.skinToolStrip1.BaseFore = System.Drawing.Color.Black;
            this.skinToolStrip1.BaseForeAnamorphosis = false;
            this.skinToolStrip1.BaseForeAnamorphosisBorder = 4;
            this.skinToolStrip1.BaseForeAnamorphosisColor = System.Drawing.Color.White;
            this.skinToolStrip1.BaseForeOffset = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.BaseHoverFore = System.Drawing.Color.White;
            this.skinToolStrip1.BaseItemAnamorphosis = true;
            this.skinToolStrip1.BaseItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.BaseItemBorderShow = true;
            this.skinToolStrip1.BaseItemDown = ((System.Drawing.Image)(resources.GetObject("skinToolStrip1.BaseItemDown")));
            this.skinToolStrip1.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.BaseItemMouse = ((System.Drawing.Image)(resources.GetObject("skinToolStrip1.BaseItemMouse")));
            this.skinToolStrip1.BaseItemNorml = null;
            this.skinToolStrip1.BaseItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.BaseItemRadius = 4;
            this.skinToolStrip1.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.BaseItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.BindTabControl = null;
            this.skinToolStrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.skinToolStrip1.Fore = System.Drawing.Color.Black;
            this.skinToolStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.skinToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.skinToolStrip1.HoverFore = System.Drawing.Color.White;
            this.skinToolStrip1.ItemAnamorphosis = true;
            this.skinToolStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.ItemBorderShow = true;
            this.skinToolStrip1.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.ItemRadius = 4;
            this.skinToolStrip1.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtn_reply,
            this.toolBtn_replyAll,
            this.toolBtn_forward,
            this.toolBtn_move,
            this.toolBtn_delete,
            this.toolBtn_quit});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(984, 36);
            this.skinToolStrip1.SkinAllColor = true;
            this.skinToolStrip1.TabIndex = 0;
            this.skinToolStrip1.Text = "工具栏";
            this.skinToolStrip1.TitleAnamorphosis = true;
            this.skinToolStrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.skinToolStrip1.TitleRadius = 4;
            this.skinToolStrip1.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // toolBtn_reply
            // 
            this.toolBtn_reply.Image = global::YANAN.Mail.Client.Properties.Resources.mail_reply;
            this.toolBtn_reply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_reply.Name = "toolBtn_reply";
            this.toolBtn_reply.Size = new System.Drawing.Size(52, 33);
            this.toolBtn_reply.Text = "回复";
            // 
            // toolBtn_replyAll
            // 
            this.toolBtn_replyAll.Image = global::YANAN.Mail.Client.Properties.Resources.mail_reply_all;
            this.toolBtn_replyAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_replyAll.Name = "toolBtn_replyAll";
            this.toolBtn_replyAll.Size = new System.Drawing.Size(76, 33);
            this.toolBtn_replyAll.Text = "回复全部";
            // 
            // toolBtn_forward
            // 
            this.toolBtn_forward.Image = global::YANAN.Mail.Client.Properties.Resources.mail_forward;
            this.toolBtn_forward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_forward.Name = "toolBtn_forward";
            this.toolBtn_forward.Size = new System.Drawing.Size(52, 33);
            this.toolBtn_forward.Text = "转发";
            this.toolBtn_forward.Click += new System.EventHandler(this.toolBtn_forward_Click);
            // 
            // toolBtn_move
            // 
            this.toolBtn_move.Image = global::YANAN.Mail.Client.Properties.Resources.mail_move;
            this.toolBtn_move.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_move.Name = "toolBtn_move";
            this.toolBtn_move.Size = new System.Drawing.Size(52, 33);
            this.toolBtn_move.Text = "移动";
            // 
            // toolBtn_delete
            // 
            this.toolBtn_delete.Image = global::YANAN.Mail.Client.Properties.Resources.mail_delete;
            this.toolBtn_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_delete.Name = "toolBtn_delete";
            this.toolBtn_delete.Size = new System.Drawing.Size(52, 33);
            this.toolBtn_delete.Text = "删除";
            // 
            // panel_body
            // 
            this.panel_body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_body.Location = new System.Drawing.Point(0, 36);
            this.panel_body.Name = "panel_body";
            this.panel_body.Size = new System.Drawing.Size(984, 625);
            this.panel_body.TabIndex = 1;
            // 
            // toolBtn_quit
            // 
            this.toolBtn_quit.Image = global::YANAN.Mail.Client.Properties.Resources.mail_quit;
            this.toolBtn_quit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_quit.Name = "toolBtn_quit";
            this.toolBtn_quit.Size = new System.Drawing.Size(52, 33);
            this.toolBtn_quit.Text = "退出";
            // 
            // MailViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.panel_body);
            this.Controls.Add(this.panel1);
            this.Name = "MailViewForm";
            this.Text = "查看邮件";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MailView_FormClosed);
            this.panel1.ResumeLayout(false);
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_body;
        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private System.Windows.Forms.ToolStripButton toolBtn_reply;
        private System.Windows.Forms.ToolStripButton toolBtn_replyAll;
        private System.Windows.Forms.ToolStripButton toolBtn_forward;
        private System.Windows.Forms.ToolStripButton toolBtn_move;
        private System.Windows.Forms.ToolStripButton toolBtn_delete;
        private System.Windows.Forms.ToolStripButton toolBtn_quit;
    }
}