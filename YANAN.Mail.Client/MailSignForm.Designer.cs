namespace YANAN.Mail.Client
{
    partial class MailSignForm
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
            this.components = new System.ComponentModel.Container();
            this.tablelayout_right = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radio_default_no = new System.Windows.Forms.RadioButton();
            this.radio_default_yes = new System.Windows.Forms.RadioButton();
            this.txt_SignatureName = new CCWin.SkinControl.SkinTextBox();
            this.combo_MailBoxId = new CCWin.SkinControl.SkinComboBox();
            this.txt_signContent = new WinHtmlEditor.HtmlEditor();
            this.btn_save = new CCWin.SkinControl.SkinButton();
            this.tree_sign = new CCWin.SkinControl.SkinTreeView();
            this.panel_left = new System.Windows.Forms.Panel();
            this.toolSctrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.toolBtn_add = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_delete = new System.Windows.Forms.ToolStripButton();
            this.tablelayout_right.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_left.SuspendLayout();
            this.toolSctrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablelayout_right
            // 
            this.tablelayout_right.ColumnCount = 4;
            this.tablelayout_right.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tablelayout_right.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablelayout_right.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tablelayout_right.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablelayout_right.Controls.Add(this.label2, 0, 1);
            this.tablelayout_right.Controls.Add(this.label1, 0, 0);
            this.tablelayout_right.Controls.Add(this.label3, 2, 1);
            this.tablelayout_right.Controls.Add(this.panel1, 3, 1);
            this.tablelayout_right.Controls.Add(this.txt_SignatureName, 1, 1);
            this.tablelayout_right.Controls.Add(this.combo_MailBoxId, 1, 0);
            this.tablelayout_right.Controls.Add(this.txt_signContent, 0, 2);
            this.tablelayout_right.Controls.Add(this.btn_save, 1, 3);
            this.tablelayout_right.Location = new System.Drawing.Point(180, 0);
            this.tablelayout_right.Name = "tablelayout_right";
            this.tablelayout_right.RowCount = 4;
            this.tablelayout_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tablelayout_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tablelayout_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablelayout_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tablelayout_right.Size = new System.Drawing.Size(504, 505);
            this.tablelayout_right.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "签名标题";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "邮箱";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(255, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "默认签名";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radio_default_no);
            this.panel1.Controls.Add(this.radio_default_yes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(315, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(186, 24);
            this.panel1.TabIndex = 44;
            // 
            // radio_default_no
            // 
            this.radio_default_no.AutoSize = true;
            this.radio_default_no.Checked = true;
            this.radio_default_no.Location = new System.Drawing.Point(73, 3);
            this.radio_default_no.Name = "radio_default_no";
            this.radio_default_no.Size = new System.Drawing.Size(35, 16);
            this.radio_default_no.TabIndex = 1;
            this.radio_default_no.TabStop = true;
            this.radio_default_no.Text = "否";
            this.radio_default_no.UseVisualStyleBackColor = true;
            // 
            // radio_default_yes
            // 
            this.radio_default_yes.AutoSize = true;
            this.radio_default_yes.Location = new System.Drawing.Point(10, 5);
            this.radio_default_yes.Name = "radio_default_yes";
            this.radio_default_yes.Size = new System.Drawing.Size(35, 16);
            this.radio_default_yes.TabIndex = 0;
            this.radio_default_yes.Text = "是";
            this.radio_default_yes.UseVisualStyleBackColor = true;
            // 
            // txt_SignatureName
            // 
            this.txt_SignatureName.BackColor = System.Drawing.Color.White;
            this.txt_SignatureName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_SignatureName.DownBack = null;
            this.txt_SignatureName.Icon = null;
            this.txt_SignatureName.IconIsButton = false;
            this.txt_SignatureName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_SignatureName.IsPasswordChat = '\0';
            this.txt_SignatureName.IsSystemPasswordChar = false;
            this.txt_SignatureName.Lines = new string[0];
            this.txt_SignatureName.Location = new System.Drawing.Point(60, 30);
            this.txt_SignatureName.Margin = new System.Windows.Forms.Padding(0);
            this.txt_SignatureName.MaxLength = 32767;
            this.txt_SignatureName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_SignatureName.MouseBack = null;
            this.txt_SignatureName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_SignatureName.Multiline = true;
            this.txt_SignatureName.Name = "txt_SignatureName";
            this.txt_SignatureName.NormlBack = null;
            this.txt_SignatureName.Padding = new System.Windows.Forms.Padding(5);
            this.txt_SignatureName.ReadOnly = false;
            this.txt_SignatureName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_SignatureName.Size = new System.Drawing.Size(192, 30);
            // 
            // 
            // 
            this.txt_SignatureName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_SignatureName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_SignatureName.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_SignatureName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_SignatureName.SkinTxt.Multiline = true;
            this.txt_SignatureName.SkinTxt.Name = "BaseText";
            this.txt_SignatureName.SkinTxt.Size = new System.Drawing.Size(182, 20);
            this.txt_SignatureName.SkinTxt.TabIndex = 0;
            this.txt_SignatureName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_SignatureName.SkinTxt.WaterText = "";
            this.txt_SignatureName.TabIndex = 45;
            this.txt_SignatureName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_SignatureName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_SignatureName.WaterText = "";
            this.txt_SignatureName.WordWrap = true;
            // 
            // combo_MailBoxId
            // 
            this.combo_MailBoxId.BackColor = System.Drawing.Color.White;
            this.combo_MailBoxId.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_MailBoxId.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_MailBoxId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combo_MailBoxId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combo_MailBoxId.FormattingEnabled = true;
            this.combo_MailBoxId.Location = new System.Drawing.Point(63, 3);
            this.combo_MailBoxId.Name = "combo_MailBoxId";
            this.combo_MailBoxId.Size = new System.Drawing.Size(186, 22);
            this.combo_MailBoxId.TabIndex = 46;
            this.combo_MailBoxId.WaterText = "";
            // 
            // txt_signContent
            // 
            this.txt_signContent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txt_signContent.BodyFont = new WinHtmlEditor.HtmlFontProperty("Arial", WinHtmlEditor.HtmlFontSize.Small, false, false, false, false, false, false);
            this.txt_signContent.BodyInnerHTML = null;
            this.txt_signContent.BodyInnerText = null;
            this.tablelayout_right.SetColumnSpan(this.txt_signContent, 4);
            this.txt_signContent.EnterToBR = false;
            this.txt_signContent.FontSize = WinHtmlEditor.FontSize.One;
            this.txt_signContent.Location = new System.Drawing.Point(3, 63);
            this.txt_signContent.Name = "txt_signContent";
            this.txt_signContent.ShowStatusBar = false;
            this.txt_signContent.ShowToolBar = true;
            this.txt_signContent.ShowWb = true;
            this.txt_signContent.Size = new System.Drawing.Size(498, 399);
            this.txt_signContent.TabIndex = 47;
            this.txt_signContent.WebBrowserShortcutsEnabled = true;
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.Red;
            this.btn_save.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btn_save.DownBack = null;
            this.btn_save.DownBaseColor = System.Drawing.Color.Transparent;
            this.btn_save.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btn_save.ForeColor = System.Drawing.Color.White;
            this.btn_save.Location = new System.Drawing.Point(63, 468);
            this.btn_save.MouseBack = null;
            this.btn_save.Name = "btn_save";
            this.btn_save.NormlBack = null;
            this.btn_save.Size = new System.Drawing.Size(90, 34);
            this.btn_save.TabIndex = 48;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = false;
            // 
            // tree_sign
            // 
            this.tree_sign.BackColor = System.Drawing.Color.White;
            this.tree_sign.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tree_sign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree_sign.ItemHeight = 28;
            this.tree_sign.Location = new System.Drawing.Point(0, 0);
            this.tree_sign.Name = "tree_sign";
            this.tree_sign.ShowLines = false;
            this.tree_sign.ShowPlusMinus = false;
            this.tree_sign.ShowRootLines = false;
            this.tree_sign.Size = new System.Drawing.Size(185, 505);
            this.tree_sign.TabIndex = 0;
            // 
            // panel_left
            // 
            this.panel_left.Controls.Add(this.toolSctrip1);
            this.panel_left.Controls.Add(this.tree_sign);
            this.panel_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_left.Location = new System.Drawing.Point(0, 0);
            this.panel_left.Name = "panel_left";
            this.panel_left.Size = new System.Drawing.Size(185, 505);
            this.panel_left.TabIndex = 2;
            // 
            // toolSctrip1
            // 
            this.toolSctrip1.Arrow = System.Drawing.Color.Black;
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
            this.toolSctrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
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
            this.toolBtn_add,
            this.toolBtn_delete});
            this.toolSctrip1.Location = new System.Drawing.Point(0, 480);
            this.toolSctrip1.Name = "toolSctrip1";
            this.toolSctrip1.Padding = new System.Windows.Forms.Padding(20, 0, 1, 0);
            this.toolSctrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.toolSctrip1.Size = new System.Drawing.Size(185, 25);
            this.toolSctrip1.SkinAllColor = true;
            this.toolSctrip1.TabIndex = 4;
            this.toolSctrip1.Text = "工具栏";
            this.toolSctrip1.TitleAnamorphosis = true;
            this.toolSctrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolSctrip1.TitleRadius = 4;
            this.toolSctrip1.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // toolBtn_add
            // 
            this.toolBtn_add.Image = global::YANAN.Mail.Client.Properties.Resources.mail_add;
            this.toolBtn_add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_add.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.toolBtn_add.Name = "toolBtn_add";
            this.toolBtn_add.Size = new System.Drawing.Size(52, 22);
            this.toolBtn_add.Text = "新增";
            // 
            // toolBtn_delete
            // 
            this.toolBtn_delete.Image = global::YANAN.Mail.Client.Properties.Resources.mail_delete;
            this.toolBtn_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_delete.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.toolBtn_delete.Name = "toolBtn_delete";
            this.toolBtn_delete.Size = new System.Drawing.Size(52, 22);
            this.toolBtn_delete.Text = "删除";
            // 
            // MailSignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 505);
            this.Controls.Add(this.panel_left);
            this.Controls.Add(this.tablelayout_right);
            this.Name = "MailSignForm";
            this.Text = "邮箱签名";
            this.tablelayout_right.ResumeLayout(false);
            this.tablelayout_right.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_left.ResumeLayout(false);
            this.panel_left.PerformLayout();
            this.toolSctrip1.ResumeLayout(false);
            this.toolSctrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tablelayout_right;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radio_default_no;
        private System.Windows.Forms.RadioButton radio_default_yes;
        private CCWin.SkinControl.SkinTextBox txt_SignatureName;
        private CCWin.SkinControl.SkinComboBox combo_MailBoxId;
        private WinHtmlEditor.HtmlEditor txt_signContent;
        private CCWin.SkinControl.SkinButton btn_save;
        private CCWin.SkinControl.SkinTreeView tree_sign;
        private System.Windows.Forms.Panel panel_left;
        private CCWin.SkinControl.SkinToolStrip toolSctrip1;
        private System.Windows.Forms.ToolStripButton toolBtn_add;
        private System.Windows.Forms.ToolStripButton toolBtn_delete;
    }
}