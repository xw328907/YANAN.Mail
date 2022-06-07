namespace YANAN.Mail.Client
{
    partial class MailComposeForm
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
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.toolBtn_send = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_save = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_cc = new System.Windows.Forms.ToolStripButton();
            this.tooBtn_bcc = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_attach = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_product = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_quit = new System.Windows.Forms.ToolStripButton();
            this.tablePanel_receive = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_subject = new CCWin.SkinControl.SkinTextBox();
            this.txt_cc = new CCWin.SkinControl.SkinTextBox();
            this.txt_bcc = new CCWin.SkinControl.SkinTextBox();
            this.lblReceiver = new System.Windows.Forms.LinkLabel();
            this.lblCc = new System.Windows.Forms.LinkLabel();
            this.lblBcc = new System.Windows.Forms.LinkLabel();
            this.txt_receiver = new CCWin.SkinControl.SkinTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtContent = new WinHtmlEditor.HtmlEditor();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chk_singleSend = new System.Windows.Forms.CheckBox();
            this.datePicker_timer = new System.Windows.Forms.DateTimePicker();
            this.chk_timer = new System.Windows.Forms.CheckBox();
            this.chk_read = new System.Windows.Forms.CheckBox();
            this.chk_important = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.combox_sign = new CCWin.SkinControl.SkinComboBox();
            this.combox_sender = new CCWin.SkinControl.SkinComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer_autosave = new System.Windows.Forms.Timer(this.components);
            this.skinToolStrip1.SuspendLayout();
            this.tablePanel_receive.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            this.skinToolStrip1.BaseForeAnamorphosisColor = System.Drawing.Color.Black;
            this.skinToolStrip1.BaseForeOffset = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.BaseHoverFore = System.Drawing.Color.Black;
            this.skinToolStrip1.BaseItemAnamorphosis = false;
            this.skinToolStrip1.BaseItemBorder = System.Drawing.Color.Black;
            this.skinToolStrip1.BaseItemBorderShow = true;
            this.skinToolStrip1.BaseItemDown = null;
            this.skinToolStrip1.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.skinToolStrip1.BaseItemMouse = null;
            this.skinToolStrip1.BaseItemNorml = null;
            this.skinToolStrip1.BaseItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.skinToolStrip1.BaseItemRadius = 4;
            this.skinToolStrip1.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.None;
            this.skinToolStrip1.BaseItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.skinToolStrip1.BindTabControl = null;
            this.skinToolStrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.skinToolStrip1.Font = new System.Drawing.Font("Arial", 9F);
            this.skinToolStrip1.Fore = System.Drawing.Color.Black;
            this.skinToolStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.skinToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.skinToolStrip1.HoverFore = System.Drawing.Color.Black;
            this.skinToolStrip1.ItemAnamorphosis = false;
            this.skinToolStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.skinToolStrip1.ItemBorderShow = true;
            this.skinToolStrip1.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.skinToolStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.skinToolStrip1.ItemRadius = 4;
            this.skinToolStrip1.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.None;
            this.skinToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtn_send,
            this.toolBtn_save,
            this.toolBtn_cc,
            this.tooBtn_bcc,
            this.toolBtn_attach,
            this.toolBtn_product,
            this.toolBtn_quit});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.None;
            this.skinToolStrip1.Size = new System.Drawing.Size(1184, 34);
            this.skinToolStrip1.SkinAllColor = true;
            this.skinToolStrip1.TabIndex = 1;
            this.skinToolStrip1.Text = "工具栏";
            this.skinToolStrip1.TitleAnamorphosis = true;
            this.skinToolStrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.skinToolStrip1.TitleRadius = 4;
            this.skinToolStrip1.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.None;
            // 
            // toolBtn_send
            // 
            this.toolBtn_send.Image = global::YANAN.Mail.Client.Properties.Resources.mail_sent;
            this.toolBtn_send.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_send.Name = "toolBtn_send";
            this.toolBtn_send.Size = new System.Drawing.Size(51, 31);
            this.toolBtn_send.Text = "发送";
            // 
            // toolBtn_save
            // 
            this.toolBtn_save.Image = global::YANAN.Mail.Client.Properties.Resources.mail_draft2;
            this.toolBtn_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_save.Name = "toolBtn_save";
            this.toolBtn_save.Size = new System.Drawing.Size(63, 31);
            this.toolBtn_save.Text = "存草稿";
            // 
            // toolBtn_cc
            // 
            this.toolBtn_cc.BackColor = System.Drawing.Color.White;
            this.toolBtn_cc.Image = global::YANAN.Mail.Client.Properties.Resources.mail_cc;
            this.toolBtn_cc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_cc.Name = "toolBtn_cc";
            this.toolBtn_cc.Size = new System.Drawing.Size(51, 31);
            this.toolBtn_cc.Text = "抄送";
            // 
            // tooBtn_bcc
            // 
            this.tooBtn_bcc.Image = global::YANAN.Mail.Client.Properties.Resources.mail_secret_send;
            this.tooBtn_bcc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tooBtn_bcc.Name = "tooBtn_bcc";
            this.tooBtn_bcc.Size = new System.Drawing.Size(51, 31);
            this.tooBtn_bcc.Text = "密送";
            // 
            // toolBtn_attach
            // 
            this.toolBtn_attach.Image = global::YANAN.Mail.Client.Properties.Resources.mail_attach;
            this.toolBtn_attach.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_attach.Name = "toolBtn_attach";
            this.toolBtn_attach.Size = new System.Drawing.Size(51, 31);
            this.toolBtn_attach.Text = "附件";
            // 
            // toolBtn_product
            // 
            this.toolBtn_product.Image = global::YANAN.Mail.Client.Properties.Resources.mail_product;
            this.toolBtn_product.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_product.Name = "toolBtn_product";
            this.toolBtn_product.Size = new System.Drawing.Size(51, 31);
            this.toolBtn_product.Text = "产品";
            this.toolBtn_product.Visible = false;
            // 
            // toolBtn_quit
            // 
            this.toolBtn_quit.Image = global::YANAN.Mail.Client.Properties.Resources.mail_quit;
            this.toolBtn_quit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_quit.Name = "toolBtn_quit";
            this.toolBtn_quit.Size = new System.Drawing.Size(51, 31);
            this.toolBtn_quit.Text = "退出";
            // 
            // tablePanel_receive
            // 
            this.tablePanel_receive.ColumnCount = 2;
            this.tablePanel_receive.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tablePanel_receive.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanel_receive.Controls.Add(this.label8, 0, 3);
            this.tablePanel_receive.Controls.Add(this.txt_subject, 1, 3);
            this.tablePanel_receive.Controls.Add(this.txt_cc, 1, 1);
            this.tablePanel_receive.Controls.Add(this.txt_bcc, 1, 2);
            this.tablePanel_receive.Controls.Add(this.lblReceiver, 0, 0);
            this.tablePanel_receive.Controls.Add(this.lblCc, 0, 1);
            this.tablePanel_receive.Controls.Add(this.lblBcc, 0, 2);
            this.tablePanel_receive.Controls.Add(this.txt_receiver, 1, 0);
            this.tablePanel_receive.Dock = System.Windows.Forms.DockStyle.Top;
            this.tablePanel_receive.Location = new System.Drawing.Point(0, 34);
            this.tablePanel_receive.Name = "tablePanel_receive";
            this.tablePanel_receive.Padding = new System.Windows.Forms.Padding(3);
            this.tablePanel_receive.RowCount = 4;
            this.tablePanel_receive.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tablePanel_receive.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tablePanel_receive.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tablePanel_receive.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tablePanel_receive.Size = new System.Drawing.Size(1184, 118);
            this.tablePanel_receive.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Arial", 9F);
            this.label8.Location = new System.Drawing.Point(5, 89);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 24);
            this.label8.TabIndex = 4;
            this.label8.Text = "主题";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_subject
            // 
            this.txt_subject.BackColor = System.Drawing.Color.Transparent;
            this.txt_subject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_subject.DownBack = null;
            this.txt_subject.Icon = null;
            this.txt_subject.IconIsButton = false;
            this.txt_subject.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_subject.IsPasswordChat = '\0';
            this.txt_subject.IsSystemPasswordChar = false;
            this.txt_subject.Lines = new string[0];
            this.txt_subject.Location = new System.Drawing.Point(65, 89);
            this.txt_subject.Margin = new System.Windows.Forms.Padding(2);
            this.txt_subject.MaxLength = 32767;
            this.txt_subject.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_subject.MouseBack = null;
            this.txt_subject.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_subject.Multiline = false;
            this.txt_subject.Name = "txt_subject";
            this.txt_subject.NormlBack = null;
            this.txt_subject.Padding = new System.Windows.Forms.Padding(5);
            this.txt_subject.ReadOnly = false;
            this.txt_subject.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_subject.Size = new System.Drawing.Size(1114, 28);
            // 
            // 
            // 
            this.txt_subject.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_subject.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_subject.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_subject.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_subject.SkinTxt.Name = "BaseText";
            this.txt_subject.SkinTxt.Size = new System.Drawing.Size(1104, 14);
            this.txt_subject.SkinTxt.TabIndex = 0;
            this.txt_subject.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_subject.SkinTxt.WaterText = "";
            this.txt_subject.TabIndex = 8;
            this.txt_subject.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_subject.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_subject.WaterText = "";
            this.txt_subject.WordWrap = true;
            // 
            // txt_cc
            // 
            this.txt_cc.BackColor = System.Drawing.Color.Transparent;
            this.txt_cc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_cc.DownBack = null;
            this.txt_cc.Icon = null;
            this.txt_cc.IconIsButton = false;
            this.txt_cc.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_cc.IsPasswordChat = '\0';
            this.txt_cc.IsSystemPasswordChar = false;
            this.txt_cc.Lines = new string[0];
            this.txt_cc.Location = new System.Drawing.Point(65, 33);
            this.txt_cc.Margin = new System.Windows.Forms.Padding(2);
            this.txt_cc.MaxLength = 32767;
            this.txt_cc.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_cc.MouseBack = null;
            this.txt_cc.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_cc.Multiline = true;
            this.txt_cc.Name = "txt_cc";
            this.txt_cc.NormlBack = null;
            this.txt_cc.Padding = new System.Windows.Forms.Padding(5);
            this.txt_cc.ReadOnly = false;
            this.txt_cc.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_cc.Size = new System.Drawing.Size(1114, 28);
            // 
            // 
            // 
            this.txt_cc.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_cc.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_cc.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_cc.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_cc.SkinTxt.Multiline = true;
            this.txt_cc.SkinTxt.Name = "BaseText";
            this.txt_cc.SkinTxt.Size = new System.Drawing.Size(1104, 18);
            this.txt_cc.SkinTxt.TabIndex = 0;
            this.txt_cc.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_cc.SkinTxt.WaterText = "";
            this.txt_cc.SkinTxt.WordWrap = false;
            this.txt_cc.TabIndex = 3;
            this.txt_cc.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_cc.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_cc.WaterText = "";
            this.txt_cc.WordWrap = false;
            // 
            // txt_bcc
            // 
            this.txt_bcc.BackColor = System.Drawing.Color.Transparent;
            this.txt_bcc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_bcc.DownBack = null;
            this.txt_bcc.Icon = null;
            this.txt_bcc.IconIsButton = false;
            this.txt_bcc.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_bcc.IsPasswordChat = '\0';
            this.txt_bcc.IsSystemPasswordChar = false;
            this.txt_bcc.Lines = new string[0];
            this.txt_bcc.Location = new System.Drawing.Point(65, 61);
            this.txt_bcc.Margin = new System.Windows.Forms.Padding(2);
            this.txt_bcc.MaxLength = 32767;
            this.txt_bcc.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_bcc.MouseBack = null;
            this.txt_bcc.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_bcc.Multiline = false;
            this.txt_bcc.Name = "txt_bcc";
            this.txt_bcc.NormlBack = null;
            this.txt_bcc.Padding = new System.Windows.Forms.Padding(5);
            this.txt_bcc.ReadOnly = false;
            this.txt_bcc.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_bcc.Size = new System.Drawing.Size(1114, 28);
            // 
            // 
            // 
            this.txt_bcc.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_bcc.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_bcc.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_bcc.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_bcc.SkinTxt.Name = "BaseText";
            this.txt_bcc.SkinTxt.Size = new System.Drawing.Size(1104, 14);
            this.txt_bcc.SkinTxt.TabIndex = 0;
            this.txt_bcc.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_bcc.SkinTxt.WaterText = "";
            this.txt_bcc.SkinTxt.WordWrap = false;
            this.txt_bcc.TabIndex = 5;
            this.txt_bcc.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_bcc.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_bcc.WaterText = "";
            this.txt_bcc.WordWrap = false;
            // 
            // lblReceiver
            // 
            this.lblReceiver.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblReceiver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReceiver.Font = new System.Drawing.Font("Arial", 9F);
            this.lblReceiver.Location = new System.Drawing.Point(6, 3);
            this.lblReceiver.Name = "lblReceiver";
            this.lblReceiver.Size = new System.Drawing.Size(54, 28);
            this.lblReceiver.TabIndex = 10;
            this.lblReceiver.TabStop = true;
            this.lblReceiver.Text = "收件人";
            this.lblReceiver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblReceiver.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // lblCc
            // 
            this.lblCc.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblCc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCc.Font = new System.Drawing.Font("Arial", 9F);
            this.lblCc.Location = new System.Drawing.Point(6, 31);
            this.lblCc.Name = "lblCc";
            this.lblCc.Size = new System.Drawing.Size(54, 28);
            this.lblCc.TabIndex = 11;
            this.lblCc.TabStop = true;
            this.lblCc.Text = "抄送";
            this.lblCc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCc.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // lblBcc
            // 
            this.lblBcc.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblBcc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBcc.Font = new System.Drawing.Font("Arial", 9F);
            this.lblBcc.Location = new System.Drawing.Point(6, 59);
            this.lblBcc.Name = "lblBcc";
            this.lblBcc.Size = new System.Drawing.Size(54, 28);
            this.lblBcc.TabIndex = 12;
            this.lblBcc.TabStop = true;
            this.lblBcc.Text = "密送";
            this.lblBcc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBcc.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // txt_receiver
            // 
            this.txt_receiver.BackColor = System.Drawing.Color.Transparent;
            this.txt_receiver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_receiver.DownBack = null;
            this.txt_receiver.Icon = null;
            this.txt_receiver.IconIsButton = false;
            this.txt_receiver.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_receiver.IsPasswordChat = '\0';
            this.txt_receiver.IsSystemPasswordChar = false;
            this.txt_receiver.Lines = new string[0];
            this.txt_receiver.Location = new System.Drawing.Point(63, 3);
            this.txt_receiver.Margin = new System.Windows.Forms.Padding(0);
            this.txt_receiver.MaxLength = 32767;
            this.txt_receiver.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_receiver.MouseBack = null;
            this.txt_receiver.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_receiver.Multiline = false;
            this.txt_receiver.Name = "txt_receiver";
            this.txt_receiver.NormlBack = null;
            this.txt_receiver.Padding = new System.Windows.Forms.Padding(5);
            this.txt_receiver.ReadOnly = false;
            this.txt_receiver.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_receiver.Size = new System.Drawing.Size(1118, 28);
            // 
            // 
            // 
            this.txt_receiver.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_receiver.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_receiver.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txt_receiver.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_receiver.SkinTxt.Name = "BaseText";
            this.txt_receiver.SkinTxt.Size = new System.Drawing.Size(1108, 18);
            this.txt_receiver.SkinTxt.TabIndex = 0;
            this.txt_receiver.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_receiver.SkinTxt.WaterText = "";
            this.txt_receiver.SkinTxt.WordWrap = false;
            this.txt_receiver.TabIndex = 13;
            this.txt_receiver.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_receiver.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_receiver.WaterText = "";
            this.txt_receiver.WordWrap = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtContent);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 152);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 529);
            this.panel1.TabIndex = 2;
            // 
            // txtContent
            // 
            this.txtContent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtContent.BodyInnerHTML = null;
            this.txtContent.BodyInnerText = null;
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.EnterToBR = false;
            this.txtContent.FontSize = WinHtmlEditor.FontSize.Three;
            this.txtContent.Location = new System.Drawing.Point(0, 0);
            this.txtContent.Name = "txtContent";
            this.txtContent.ShowStatusBar = false;
            this.txtContent.ShowToolBar = true;
            this.txtContent.ShowWb = true;
            this.txtContent.Size = new System.Drawing.Size(1184, 501);
            this.txtContent.TabIndex = 11;
            this.txtContent.WebBrowserShortcutsEnabled = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chk_singleSend);
            this.panel2.Controls.Add(this.datePicker_timer);
            this.panel2.Controls.Add(this.chk_timer);
            this.panel2.Controls.Add(this.chk_read);
            this.panel2.Controls.Add(this.chk_important);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.combox_sign);
            this.panel2.Controls.Add(this.combox_sender);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 501);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1184, 28);
            this.panel2.TabIndex = 0;
            // 
            // chk_singleSend
            // 
            this.chk_singleSend.Location = new System.Drawing.Point(599, 2);
            this.chk_singleSend.Name = "chk_singleSend";
            this.chk_singleSend.Size = new System.Drawing.Size(72, 24);
            this.chk_singleSend.TabIndex = 25;
            this.chk_singleSend.Text = "分别发送";
            this.chk_singleSend.UseVisualStyleBackColor = true;
            // 
            // datePicker_timer
            // 
            this.datePicker_timer.CustomFormat = "yyyy-MM-dd HH:mm";
            this.datePicker_timer.Enabled = false;
            this.datePicker_timer.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePicker_timer.Location = new System.Drawing.Point(752, 4);
            this.datePicker_timer.Name = "datePicker_timer";
            this.datePicker_timer.Size = new System.Drawing.Size(160, 21);
            this.datePicker_timer.TabIndex = 31;
            this.datePicker_timer.Visible = false;
            // 
            // chk_timer
            // 
            this.chk_timer.Location = new System.Drawing.Point(680, 3);
            this.chk_timer.Name = "chk_timer";
            this.chk_timer.Size = new System.Drawing.Size(74, 24);
            this.chk_timer.TabIndex = 28;
            this.chk_timer.Text = "定时发送";
            this.chk_timer.UseVisualStyleBackColor = true;
            // 
            // chk_read
            // 
            this.chk_read.Location = new System.Drawing.Point(521, 2);
            this.chk_read.Name = "chk_read";
            this.chk_read.Size = new System.Drawing.Size(72, 24);
            this.chk_read.TabIndex = 21;
            this.chk_read.Text = "已读回执";
            this.chk_read.UseVisualStyleBackColor = true;
            // 
            // chk_important
            // 
            this.chk_important.Location = new System.Drawing.Point(445, 3);
            this.chk_important.Name = "chk_important";
            this.chk_important.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.chk_important.Size = new System.Drawing.Size(68, 24);
            this.chk_important.TabIndex = 18;
            this.chk_important.Text = "紧急";
            this.chk_important.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(243, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 24);
            this.label2.TabIndex = 8;
            this.label2.Text = "签名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // combox_sign
            // 
            this.combox_sign.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combox_sign.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combox_sign.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combox_sign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combox_sign.FormattingEnabled = true;
            this.combox_sign.Location = new System.Drawing.Point(299, 3);
            this.combox_sign.Name = "combox_sign";
            this.combox_sign.Size = new System.Drawing.Size(140, 22);
            this.combox_sign.TabIndex = 15;
            this.combox_sign.WaterText = "";
            // 
            // combox_sender
            // 
            this.combox_sender.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combox_sender.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combox_sender.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combox_sender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combox_sender.FormattingEnabled = true;
            this.combox_sender.Location = new System.Drawing.Point(57, 3);
            this.combox_sender.Name = "combox_sender";
            this.combox_sender.Size = new System.Drawing.Size(180, 22);
            this.combox_sender.TabIndex = 13;
            this.combox_sender.WaterText = "";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "发件人";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer_autosave
            // 
            this.timer_autosave.Interval = 60000;
            // 
            // MailComposeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 681);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tablePanel_receive);
            this.Controls.Add(this.skinToolStrip1);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MailComposeForm";
            this.Text = "未命名 - 写邮件";
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            this.tablePanel_receive.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private System.Windows.Forms.TableLayoutPanel tablePanel_receive;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton toolBtn_send;
        private System.Windows.Forms.ToolStripButton toolBtn_save;
        private System.Windows.Forms.ToolStripButton toolBtn_cc;
        private System.Windows.Forms.ToolStripButton tooBtn_bcc;
        private System.Windows.Forms.ToolStripButton toolBtn_attach;
        private System.Windows.Forms.ToolStripButton toolBtn_product;
        private System.Windows.Forms.ToolStripButton toolBtn_quit;
        private CCWin.SkinControl.SkinComboBox combox_sender;
        private CCWin.SkinControl.SkinComboBox combox_sign;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chk_important;
        private System.Windows.Forms.CheckBox chk_read;
        private System.Windows.Forms.CheckBox chk_timer;
        private System.Windows.Forms.DateTimePicker datePicker_timer;
        private WinHtmlEditor.HtmlEditor txtContent;
        private System.Windows.Forms.Label label8;
        private CCWin.SkinControl.SkinTextBox txt_subject;
        private CCWin.SkinControl.SkinTextBox txt_cc;
        private System.Windows.Forms.Timer timer_autosave;
        private System.Windows.Forms.CheckBox chk_singleSend;
        private CCWin.SkinControl.SkinTextBox txt_bcc;
        private System.Windows.Forms.LinkLabel lblReceiver;
        private System.Windows.Forms.LinkLabel lblCc;
        private System.Windows.Forms.LinkLabel lblBcc;
        private CCWin.SkinControl.SkinTextBox txt_receiver;
    }
}