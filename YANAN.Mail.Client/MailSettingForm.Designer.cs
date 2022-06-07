namespace YANAN.Mail.Client
{
    partial class MailSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailSettingForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.toolStripBtn_add = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtn_delete = new System.Windows.Forms.ToolStripButton();
            this.tree_mailbox = new CCWin.SkinControl.SkinTreeView();
            this.imageList_treeMailBox = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txt_MailAddress = new CCWin.SkinControl.SkinTextBox();
            this.txt_MailPassword = new CCWin.SkinControl.SkinTextBox();
            this.cbox_ProtocolTypeId = new CCWin.SkinControl.SkinComboBox();
            this.txt_PopServer = new CCWin.SkinControl.SkinTextBox();
            this.txt_PopPort = new CCWin.SkinControl.SkinTextBox();
            this.txt_SmtpServer = new CCWin.SkinControl.SkinTextBox();
            this.txt_SmtpPort = new CCWin.SkinControl.SkinTextBox();
            this.cbox_KeepDays = new CCWin.SkinControl.SkinComboBox();
            this.txt_ShowName = new CCWin.SkinControl.SkinTextBox();
            this.txt_NickName = new CCWin.SkinControl.SkinTextBox();
            this.txt_cc = new CCWin.SkinControl.SkinTextBox();
            this.txt_bcc = new CCWin.SkinControl.SkinTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btn_save = new CCWin.SkinControl.SkinButton();
            this.txt_ReceiveTimer = new CCWin.SkinControl.SkinTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chk_ReceiveTimer = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radio_default_no = new System.Windows.Forms.RadioButton();
            this.radio_default_yes = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.dataPicker_ReceiveBeginTime = new System.Windows.Forms.DateTimePicker();
            this.combo_receiveTime = new CCWin.SkinControl.SkinComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.skinToolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.skinToolStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.tree_mailbox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(684, 505);
            this.splitContainer1.SplitterDistance = 188;
            this.splitContainer1.TabIndex = 0;
            // 
            // skinToolStrip1
            // 
            this.skinToolStrip1.Arrow = System.Drawing.Color.Black;
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
            this.skinToolStrip1.BaseItemAnamorphosis = true;
            this.skinToolStrip1.BaseItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.skinToolStrip1.BaseItemBorderShow = true;
            this.skinToolStrip1.BaseItemDown = null;
            this.skinToolStrip1.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.skinToolStrip1.BaseItemMouse = null;
            this.skinToolStrip1.BaseItemNorml = null;
            this.skinToolStrip1.BaseItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.skinToolStrip1.BaseItemRadius = 4;
            this.skinToolStrip1.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.BaseItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.skinToolStrip1.BindTabControl = null;
            this.skinToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.skinToolStrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.skinToolStrip1.Fore = System.Drawing.Color.Black;
            this.skinToolStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.skinToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.skinToolStrip1.HoverFore = System.Drawing.Color.White;
            this.skinToolStrip1.ItemAnamorphosis = true;
            this.skinToolStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.skinToolStrip1.ItemBorderShow = true;
            this.skinToolStrip1.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.skinToolStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.skinToolStrip1.ItemRadius = 4;
            this.skinToolStrip1.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtn_add,
            this.toolStripBtn_delete});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 480);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.Padding = new System.Windows.Forms.Padding(20, 0, 1, 0);
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(188, 25);
            this.skinToolStrip1.SkinAllColor = true;
            this.skinToolStrip1.TabIndex = 3;
            this.skinToolStrip1.Text = "skinToolStrip1";
            this.skinToolStrip1.TitleAnamorphosis = true;
            this.skinToolStrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.skinToolStrip1.TitleRadius = 4;
            this.skinToolStrip1.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // toolStripBtn_add
            // 
            this.toolStripBtn_add.Image = global::YANAN.Mail.Client.Properties.Resources.mail_add;
            this.toolStripBtn_add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtn_add.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.toolStripBtn_add.Name = "toolStripBtn_add";
            this.toolStripBtn_add.Size = new System.Drawing.Size(52, 22);
            this.toolStripBtn_add.Text = "新增";
            this.toolStripBtn_add.Click += new System.EventHandler(this.toolStripBtn_add_Click);
            // 
            // toolStripBtn_delete
            // 
            this.toolStripBtn_delete.Image = global::YANAN.Mail.Client.Properties.Resources.mail_delete;
            this.toolStripBtn_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtn_delete.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.toolStripBtn_delete.Name = "toolStripBtn_delete";
            this.toolStripBtn_delete.Size = new System.Drawing.Size(52, 22);
            this.toolStripBtn_delete.Text = "删除";
            this.toolStripBtn_delete.Click += new System.EventHandler(this.toolStripBtn_delete_Click);
            // 
            // tree_mailbox
            // 
            this.tree_mailbox.BackColor = System.Drawing.Color.White;
            this.tree_mailbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tree_mailbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree_mailbox.ImageIndex = 0;
            this.tree_mailbox.ImageList = this.imageList_treeMailBox;
            this.tree_mailbox.ItemHeight = 28;
            this.tree_mailbox.Location = new System.Drawing.Point(0, 0);
            this.tree_mailbox.Name = "tree_mailbox";
            this.tree_mailbox.SelectedImageIndex = 0;
            this.tree_mailbox.ShowLines = false;
            this.tree_mailbox.ShowPlusMinus = false;
            this.tree_mailbox.ShowRootLines = false;
            this.tree_mailbox.Size = new System.Drawing.Size(188, 505);
            this.tree_mailbox.TabIndex = 2;
            this.tree_mailbox.TabStop = false;
            this.tree_mailbox.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tree_mailbox_NodeMouseClick);
            // 
            // imageList_treeMailBox
            // 
            this.imageList_treeMailBox.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_treeMailBox.ImageStream")));
            this.imageList_treeMailBox.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_treeMailBox.Images.SetKeyName(0, "mail_receive_box.png");
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.13837F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.7478F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.7478F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.36604F));
            this.tableLayoutPanel1.Controls.Add(this.txt_MailAddress, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_MailPassword, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbox_ProtocolTypeId, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txt_PopServer, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txt_PopPort, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.txt_SmtpServer, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txt_SmtpPort, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.cbox_KeepDays, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txt_ShowName, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.txt_NickName, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.txt_cc, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.txt_bcc, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.btn_save, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.txt_ReceiveTimer, 3, 12);
            this.tableLayoutPanel1.Controls.Add(this.label13, 4, 12);
            this.tableLayoutPanel1.Controls.Add(this.chk_ReceiveTimer, 2, 12);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.dataPicker_ReceiveBeginTime, 2, 11);
            this.tableLayoutPanel1.Controls.Add(this.combo_receiveTime, 1, 11);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tableLayoutPanel1.RowCount = 15;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(492, 505);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // txt_MailAddress
            // 
            this.txt_MailAddress.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.txt_MailAddress, 4);
            this.txt_MailAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_MailAddress.DownBack = null;
            this.txt_MailAddress.Icon = null;
            this.txt_MailAddress.IconIsButton = false;
            this.txt_MailAddress.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_MailAddress.IsPasswordChat = '\0';
            this.txt_MailAddress.IsSystemPasswordChar = false;
            this.txt_MailAddress.Lines = new string[0];
            this.txt_MailAddress.Location = new System.Drawing.Point(93, 10);
            this.txt_MailAddress.Margin = new System.Windows.Forms.Padding(0);
            this.txt_MailAddress.MaxLength = 120;
            this.txt_MailAddress.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_MailAddress.MouseBack = null;
            this.txt_MailAddress.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_MailAddress.Multiline = true;
            this.txt_MailAddress.Name = "txt_MailAddress";
            this.txt_MailAddress.NormlBack = null;
            this.txt_MailAddress.Padding = new System.Windows.Forms.Padding(5);
            this.txt_MailAddress.ReadOnly = false;
            this.txt_MailAddress.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_MailAddress.Size = new System.Drawing.Size(399, 30);
            // 
            // 
            // 
            this.txt_MailAddress.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_MailAddress.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_MailAddress.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_MailAddress.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_MailAddress.SkinTxt.MaxLength = 120;
            this.txt_MailAddress.SkinTxt.Multiline = true;
            this.txt_MailAddress.SkinTxt.Name = "BaseText";
            this.txt_MailAddress.SkinTxt.Size = new System.Drawing.Size(389, 20);
            this.txt_MailAddress.SkinTxt.TabIndex = 0;
            this.txt_MailAddress.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_MailAddress.SkinTxt.WaterText = "";
            this.txt_MailAddress.SkinTxt.WordWrap = false;
            this.txt_MailAddress.TabIndex = 1;
            this.txt_MailAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_MailAddress.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_MailAddress.WaterText = "";
            this.txt_MailAddress.WordWrap = false;
            // 
            // txt_MailPassword
            // 
            this.txt_MailPassword.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.txt_MailPassword, 4);
            this.txt_MailPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_MailPassword.DownBack = null;
            this.txt_MailPassword.Icon = null;
            this.txt_MailPassword.IconIsButton = false;
            this.txt_MailPassword.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_MailPassword.IsPasswordChat = '*';
            this.txt_MailPassword.IsSystemPasswordChar = false;
            this.txt_MailPassword.Lines = new string[0];
            this.txt_MailPassword.Location = new System.Drawing.Point(93, 40);
            this.txt_MailPassword.Margin = new System.Windows.Forms.Padding(0);
            this.txt_MailPassword.MaxLength = 50;
            this.txt_MailPassword.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_MailPassword.MouseBack = null;
            this.txt_MailPassword.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_MailPassword.Multiline = true;
            this.txt_MailPassword.Name = "txt_MailPassword";
            this.txt_MailPassword.NormlBack = null;
            this.txt_MailPassword.Padding = new System.Windows.Forms.Padding(5);
            this.txt_MailPassword.ReadOnly = false;
            this.txt_MailPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_MailPassword.Size = new System.Drawing.Size(399, 30);
            // 
            // 
            // 
            this.txt_MailPassword.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_MailPassword.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_MailPassword.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_MailPassword.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_MailPassword.SkinTxt.MaxLength = 50;
            this.txt_MailPassword.SkinTxt.Multiline = true;
            this.txt_MailPassword.SkinTxt.Name = "BaseText";
            this.txt_MailPassword.SkinTxt.PasswordChar = '*';
            this.txt_MailPassword.SkinTxt.Size = new System.Drawing.Size(389, 20);
            this.txt_MailPassword.SkinTxt.TabIndex = 0;
            this.txt_MailPassword.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_MailPassword.SkinTxt.WaterText = "";
            this.txt_MailPassword.SkinTxt.WordWrap = false;
            this.txt_MailPassword.TabIndex = 13;
            this.txt_MailPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_MailPassword.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_MailPassword.WaterText = "";
            this.txt_MailPassword.WordWrap = false;
            // 
            // cbox_ProtocolTypeId
            // 
            this.cbox_ProtocolTypeId.BackColor = System.Drawing.Color.White;
            this.cbox_ProtocolTypeId.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbox_ProtocolTypeId.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tableLayoutPanel1.SetColumnSpan(this.cbox_ProtocolTypeId, 2);
            this.cbox_ProtocolTypeId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbox_ProtocolTypeId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbox_ProtocolTypeId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_ProtocolTypeId.DropDownWidth = 120;
            this.cbox_ProtocolTypeId.FormattingEnabled = true;
            this.cbox_ProtocolTypeId.Location = new System.Drawing.Point(96, 73);
            this.cbox_ProtocolTypeId.Name = "cbox_ProtocolTypeId";
            this.cbox_ProtocolTypeId.Size = new System.Drawing.Size(230, 22);
            this.cbox_ProtocolTypeId.TabIndex = 14;
            this.cbox_ProtocolTypeId.WaterText = "";
            // 
            // txt_PopServer
            // 
            this.txt_PopServer.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.txt_PopServer, 2);
            this.txt_PopServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_PopServer.DownBack = null;
            this.txt_PopServer.Icon = null;
            this.txt_PopServer.IconIsButton = false;
            this.txt_PopServer.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_PopServer.IsPasswordChat = '\0';
            this.txt_PopServer.IsSystemPasswordChar = false;
            this.txt_PopServer.Lines = new string[0];
            this.txt_PopServer.Location = new System.Drawing.Point(93, 100);
            this.txt_PopServer.Margin = new System.Windows.Forms.Padding(0);
            this.txt_PopServer.MaxLength = 32767;
            this.txt_PopServer.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_PopServer.MouseBack = null;
            this.txt_PopServer.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_PopServer.Multiline = true;
            this.txt_PopServer.Name = "txt_PopServer";
            this.txt_PopServer.NormlBack = null;
            this.txt_PopServer.Padding = new System.Windows.Forms.Padding(5);
            this.txt_PopServer.ReadOnly = false;
            this.txt_PopServer.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_PopServer.Size = new System.Drawing.Size(236, 30);
            // 
            // 
            // 
            this.txt_PopServer.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_PopServer.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_PopServer.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_PopServer.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_PopServer.SkinTxt.Multiline = true;
            this.txt_PopServer.SkinTxt.Name = "BaseText";
            this.txt_PopServer.SkinTxt.Size = new System.Drawing.Size(226, 20);
            this.txt_PopServer.SkinTxt.TabIndex = 0;
            this.txt_PopServer.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_PopServer.SkinTxt.WaterText = "";
            this.txt_PopServer.TabIndex = 15;
            this.txt_PopServer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_PopServer.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_PopServer.WaterText = "";
            this.txt_PopServer.WordWrap = true;
            // 
            // txt_PopPort
            // 
            this.txt_PopPort.BackColor = System.Drawing.Color.White;
            this.txt_PopPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_PopPort.DownBack = null;
            this.txt_PopPort.Icon = null;
            this.txt_PopPort.IconIsButton = false;
            this.txt_PopPort.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_PopPort.IsPasswordChat = '\0';
            this.txt_PopPort.IsSystemPasswordChar = false;
            this.txt_PopPort.Lines = new string[0];
            this.txt_PopPort.Location = new System.Drawing.Point(379, 100);
            this.txt_PopPort.Margin = new System.Windows.Forms.Padding(0);
            this.txt_PopPort.MaxLength = 32767;
            this.txt_PopPort.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_PopPort.MouseBack = null;
            this.txt_PopPort.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_PopPort.Multiline = true;
            this.txt_PopPort.Name = "txt_PopPort";
            this.txt_PopPort.NormlBack = null;
            this.txt_PopPort.Padding = new System.Windows.Forms.Padding(5);
            this.txt_PopPort.ReadOnly = false;
            this.txt_PopPort.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_PopPort.Size = new System.Drawing.Size(113, 30);
            // 
            // 
            // 
            this.txt_PopPort.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_PopPort.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_PopPort.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_PopPort.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_PopPort.SkinTxt.Multiline = true;
            this.txt_PopPort.SkinTxt.Name = "BaseText";
            this.txt_PopPort.SkinTxt.Size = new System.Drawing.Size(103, 20);
            this.txt_PopPort.SkinTxt.TabIndex = 0;
            this.txt_PopPort.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_PopPort.SkinTxt.WaterText = "";
            this.txt_PopPort.TabIndex = 16;
            this.txt_PopPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_PopPort.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_PopPort.WaterText = "";
            this.txt_PopPort.WordWrap = true;
            // 
            // txt_SmtpServer
            // 
            this.txt_SmtpServer.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.txt_SmtpServer, 2);
            this.txt_SmtpServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_SmtpServer.DownBack = null;
            this.txt_SmtpServer.Icon = null;
            this.txt_SmtpServer.IconIsButton = false;
            this.txt_SmtpServer.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_SmtpServer.IsPasswordChat = '\0';
            this.txt_SmtpServer.IsSystemPasswordChar = false;
            this.txt_SmtpServer.Lines = new string[0];
            this.txt_SmtpServer.Location = new System.Drawing.Point(93, 130);
            this.txt_SmtpServer.Margin = new System.Windows.Forms.Padding(0);
            this.txt_SmtpServer.MaxLength = 32767;
            this.txt_SmtpServer.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_SmtpServer.MouseBack = null;
            this.txt_SmtpServer.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_SmtpServer.Multiline = true;
            this.txt_SmtpServer.Name = "txt_SmtpServer";
            this.txt_SmtpServer.NormlBack = null;
            this.txt_SmtpServer.Padding = new System.Windows.Forms.Padding(5);
            this.txt_SmtpServer.ReadOnly = false;
            this.txt_SmtpServer.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_SmtpServer.Size = new System.Drawing.Size(236, 30);
            // 
            // 
            // 
            this.txt_SmtpServer.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_SmtpServer.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_SmtpServer.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_SmtpServer.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_SmtpServer.SkinTxt.Multiline = true;
            this.txt_SmtpServer.SkinTxt.Name = "BaseText";
            this.txt_SmtpServer.SkinTxt.Size = new System.Drawing.Size(226, 20);
            this.txt_SmtpServer.SkinTxt.TabIndex = 0;
            this.txt_SmtpServer.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_SmtpServer.SkinTxt.WaterText = "";
            this.txt_SmtpServer.TabIndex = 17;
            this.txt_SmtpServer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_SmtpServer.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_SmtpServer.WaterText = "";
            this.txt_SmtpServer.WordWrap = true;
            // 
            // txt_SmtpPort
            // 
            this.txt_SmtpPort.BackColor = System.Drawing.Color.White;
            this.txt_SmtpPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_SmtpPort.DownBack = null;
            this.txt_SmtpPort.Icon = null;
            this.txt_SmtpPort.IconIsButton = false;
            this.txt_SmtpPort.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_SmtpPort.IsPasswordChat = '\0';
            this.txt_SmtpPort.IsSystemPasswordChar = false;
            this.txt_SmtpPort.Lines = new string[0];
            this.txt_SmtpPort.Location = new System.Drawing.Point(379, 130);
            this.txt_SmtpPort.Margin = new System.Windows.Forms.Padding(0);
            this.txt_SmtpPort.MaxLength = 32767;
            this.txt_SmtpPort.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_SmtpPort.MouseBack = null;
            this.txt_SmtpPort.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_SmtpPort.Multiline = true;
            this.txt_SmtpPort.Name = "txt_SmtpPort";
            this.txt_SmtpPort.NormlBack = null;
            this.txt_SmtpPort.Padding = new System.Windows.Forms.Padding(5);
            this.txt_SmtpPort.ReadOnly = false;
            this.txt_SmtpPort.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_SmtpPort.Size = new System.Drawing.Size(113, 30);
            // 
            // 
            // 
            this.txt_SmtpPort.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_SmtpPort.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_SmtpPort.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_SmtpPort.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_SmtpPort.SkinTxt.Multiline = true;
            this.txt_SmtpPort.SkinTxt.Name = "BaseText";
            this.txt_SmtpPort.SkinTxt.Size = new System.Drawing.Size(103, 20);
            this.txt_SmtpPort.SkinTxt.TabIndex = 0;
            this.txt_SmtpPort.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_SmtpPort.SkinTxt.WaterText = "";
            this.txt_SmtpPort.TabIndex = 18;
            this.txt_SmtpPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_SmtpPort.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_SmtpPort.WaterText = "";
            this.txt_SmtpPort.WordWrap = true;
            // 
            // cbox_KeepDays
            // 
            this.cbox_KeepDays.BackColor = System.Drawing.Color.White;
            this.cbox_KeepDays.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbox_KeepDays.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tableLayoutPanel1.SetColumnSpan(this.cbox_KeepDays, 2);
            this.cbox_KeepDays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbox_KeepDays.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbox_KeepDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_KeepDays.FormattingEnabled = true;
            this.cbox_KeepDays.Location = new System.Drawing.Point(96, 163);
            this.cbox_KeepDays.Name = "cbox_KeepDays";
            this.cbox_KeepDays.Size = new System.Drawing.Size(230, 22);
            this.cbox_KeepDays.TabIndex = 19;
            this.cbox_KeepDays.WaterText = "";
            // 
            // txt_ShowName
            // 
            this.txt_ShowName.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.txt_ShowName, 2);
            this.txt_ShowName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_ShowName.DownBack = null;
            this.txt_ShowName.Icon = null;
            this.txt_ShowName.IconIsButton = false;
            this.txt_ShowName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_ShowName.IsPasswordChat = '\0';
            this.txt_ShowName.IsSystemPasswordChar = false;
            this.txt_ShowName.Lines = new string[0];
            this.txt_ShowName.Location = new System.Drawing.Point(93, 190);
            this.txt_ShowName.Margin = new System.Windows.Forms.Padding(0);
            this.txt_ShowName.MaxLength = 32767;
            this.txt_ShowName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_ShowName.MouseBack = null;
            this.txt_ShowName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_ShowName.Multiline = true;
            this.txt_ShowName.Name = "txt_ShowName";
            this.txt_ShowName.NormlBack = null;
            this.txt_ShowName.Padding = new System.Windows.Forms.Padding(5);
            this.txt_ShowName.ReadOnly = false;
            this.txt_ShowName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_ShowName.Size = new System.Drawing.Size(236, 30);
            // 
            // 
            // 
            this.txt_ShowName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_ShowName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_ShowName.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_ShowName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_ShowName.SkinTxt.Multiline = true;
            this.txt_ShowName.SkinTxt.Name = "BaseText";
            this.txt_ShowName.SkinTxt.Size = new System.Drawing.Size(226, 20);
            this.txt_ShowName.SkinTxt.TabIndex = 0;
            this.txt_ShowName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_ShowName.SkinTxt.WaterText = "";
            this.txt_ShowName.TabIndex = 20;
            this.txt_ShowName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_ShowName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_ShowName.WaterText = "";
            this.txt_ShowName.WordWrap = true;
            // 
            // txt_NickName
            // 
            this.txt_NickName.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.txt_NickName, 2);
            this.txt_NickName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_NickName.DownBack = null;
            this.txt_NickName.Icon = null;
            this.txt_NickName.IconIsButton = false;
            this.txt_NickName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_NickName.IsPasswordChat = '\0';
            this.txt_NickName.IsSystemPasswordChar = false;
            this.txt_NickName.Lines = new string[0];
            this.txt_NickName.Location = new System.Drawing.Point(93, 220);
            this.txt_NickName.Margin = new System.Windows.Forms.Padding(0);
            this.txt_NickName.MaxLength = 32767;
            this.txt_NickName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_NickName.MouseBack = null;
            this.txt_NickName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_NickName.Multiline = true;
            this.txt_NickName.Name = "txt_NickName";
            this.txt_NickName.NormlBack = null;
            this.txt_NickName.Padding = new System.Windows.Forms.Padding(5);
            this.txt_NickName.ReadOnly = false;
            this.txt_NickName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_NickName.Size = new System.Drawing.Size(236, 30);
            // 
            // 
            // 
            this.txt_NickName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_NickName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_NickName.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_NickName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_NickName.SkinTxt.Multiline = true;
            this.txt_NickName.SkinTxt.Name = "BaseText";
            this.txt_NickName.SkinTxt.Size = new System.Drawing.Size(226, 20);
            this.txt_NickName.SkinTxt.TabIndex = 0;
            this.txt_NickName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_NickName.SkinTxt.WaterText = "";
            this.txt_NickName.TabIndex = 21;
            this.txt_NickName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_NickName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_NickName.WaterText = "";
            this.txt_NickName.WordWrap = true;
            // 
            // txt_cc
            // 
            this.txt_cc.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.txt_cc, 2);
            this.txt_cc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_cc.DownBack = null;
            this.txt_cc.Icon = null;
            this.txt_cc.IconIsButton = false;
            this.txt_cc.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_cc.IsPasswordChat = '\0';
            this.txt_cc.IsSystemPasswordChar = false;
            this.txt_cc.Lines = new string[0];
            this.txt_cc.Location = new System.Drawing.Point(93, 250);
            this.txt_cc.Margin = new System.Windows.Forms.Padding(0);
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
            this.txt_cc.Size = new System.Drawing.Size(236, 30);
            // 
            // 
            // 
            this.txt_cc.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_cc.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_cc.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_cc.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_cc.SkinTxt.Multiline = true;
            this.txt_cc.SkinTxt.Name = "BaseText";
            this.txt_cc.SkinTxt.Size = new System.Drawing.Size(226, 20);
            this.txt_cc.SkinTxt.TabIndex = 0;
            this.txt_cc.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_cc.SkinTxt.WaterText = "";
            this.txt_cc.TabIndex = 22;
            this.txt_cc.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_cc.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_cc.WaterText = "";
            this.txt_cc.WordWrap = true;
            // 
            // txt_bcc
            // 
            this.txt_bcc.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.txt_bcc, 2);
            this.txt_bcc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_bcc.DownBack = null;
            this.txt_bcc.Icon = null;
            this.txt_bcc.IconIsButton = false;
            this.txt_bcc.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_bcc.IsPasswordChat = '\0';
            this.txt_bcc.IsSystemPasswordChar = false;
            this.txt_bcc.Lines = new string[0];
            this.txt_bcc.Location = new System.Drawing.Point(93, 280);
            this.txt_bcc.Margin = new System.Windows.Forms.Padding(0);
            this.txt_bcc.MaxLength = 32767;
            this.txt_bcc.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_bcc.MouseBack = null;
            this.txt_bcc.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_bcc.Multiline = true;
            this.txt_bcc.Name = "txt_bcc";
            this.txt_bcc.NormlBack = null;
            this.txt_bcc.Padding = new System.Windows.Forms.Padding(5);
            this.txt_bcc.ReadOnly = false;
            this.txt_bcc.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_bcc.Size = new System.Drawing.Size(236, 30);
            // 
            // 
            // 
            this.txt_bcc.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_bcc.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_bcc.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_bcc.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_bcc.SkinTxt.Multiline = true;
            this.txt_bcc.SkinTxt.Name = "BaseText";
            this.txt_bcc.SkinTxt.Size = new System.Drawing.Size(226, 20);
            this.txt_bcc.SkinTxt.TabIndex = 0;
            this.txt_bcc.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_bcc.SkinTxt.WaterText = "";
            this.txt_bcc.TabIndex = 23;
            this.txt_bcc.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_bcc.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_bcc.WaterText = "";
            this.txt_bcc.WordWrap = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 30);
            this.label1.TabIndex = 25;
            this.label1.Text = "邮箱地址";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 30);
            this.label2.TabIndex = 26;
            this.label2.Text = "邮箱密码";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 30);
            this.label3.TabIndex = 27;
            this.label3.Text = "协议类型";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 30);
            this.label4.TabIndex = 28;
            this.label4.Text = "收件服务器";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(332, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 30);
            this.label5.TabIndex = 29;
            this.label5.Text = "端口";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(332, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 30);
            this.label6.TabIndex = 30;
            this.label6.Text = "端口";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 30);
            this.label7.TabIndex = 31;
            this.label7.Text = "发件服务器";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 30);
            this.label8.TabIndex = 32;
            this.label8.Text = "邮局备份";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 190);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 30);
            this.label9.TabIndex = 33;
            this.label9.Text = "显示名称";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 220);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 30);
            this.label10.TabIndex = 34;
            this.label10.Text = "发件名称";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(3, 250);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 30);
            this.label11.TabIndex = 35;
            this.label11.Text = "默认抄送";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(3, 280);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 30);
            this.label12.TabIndex = 36;
            this.label12.Text = "默认密送";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.Red;
            this.btn_save.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btn_save.DownBack = null;
            this.btn_save.DownBaseColor = System.Drawing.Color.Transparent;
            this.btn_save.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btn_save.ForeColor = System.Drawing.Color.White;
            this.btn_save.Location = new System.Drawing.Point(96, 423);
            this.btn_save.MouseBack = null;
            this.btn_save.Name = "btn_save";
            this.btn_save.NormlBack = null;
            this.btn_save.Size = new System.Drawing.Size(90, 35);
            this.btn_save.TabIndex = 37;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_ReceiveTimer
            // 
            this.txt_ReceiveTimer.BackColor = System.Drawing.Color.White;
            this.txt_ReceiveTimer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_ReceiveTimer.DownBack = null;
            this.txt_ReceiveTimer.Icon = null;
            this.txt_ReceiveTimer.IconIsButton = false;
            this.txt_ReceiveTimer.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_ReceiveTimer.IsPasswordChat = '\0';
            this.txt_ReceiveTimer.IsSystemPasswordChar = false;
            this.txt_ReceiveTimer.Lines = new string[] {
        "15"};
            this.txt_ReceiveTimer.Location = new System.Drawing.Point(329, 370);
            this.txt_ReceiveTimer.Margin = new System.Windows.Forms.Padding(0);
            this.txt_ReceiveTimer.MaxLength = 32767;
            this.txt_ReceiveTimer.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_ReceiveTimer.MouseBack = null;
            this.txt_ReceiveTimer.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_ReceiveTimer.Multiline = true;
            this.txt_ReceiveTimer.Name = "txt_ReceiveTimer";
            this.txt_ReceiveTimer.NormlBack = null;
            this.txt_ReceiveTimer.Padding = new System.Windows.Forms.Padding(5);
            this.txt_ReceiveTimer.ReadOnly = false;
            this.txt_ReceiveTimer.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_ReceiveTimer.Size = new System.Drawing.Size(50, 30);
            // 
            // 
            // 
            this.txt_ReceiveTimer.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_ReceiveTimer.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_ReceiveTimer.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_ReceiveTimer.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_ReceiveTimer.SkinTxt.Multiline = true;
            this.txt_ReceiveTimer.SkinTxt.Name = "BaseText";
            this.txt_ReceiveTimer.SkinTxt.Size = new System.Drawing.Size(40, 20);
            this.txt_ReceiveTimer.SkinTxt.TabIndex = 0;
            this.txt_ReceiveTimer.SkinTxt.Text = "15";
            this.txt_ReceiveTimer.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_ReceiveTimer.SkinTxt.WaterText = "";
            this.txt_ReceiveTimer.TabIndex = 39;
            this.txt_ReceiveTimer.Text = "15";
            this.txt_ReceiveTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_ReceiveTimer.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_ReceiveTimer.WaterText = "";
            this.txt_ReceiveTimer.WordWrap = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(382, 370);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 30);
            this.label13.TabIndex = 40;
            this.label13.Text = "分钟";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chk_ReceiveTimer
            // 
            this.chk_ReceiveTimer.AutoSize = true;
            this.chk_ReceiveTimer.Checked = true;
            this.chk_ReceiveTimer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ReceiveTimer.Dock = System.Windows.Forms.DockStyle.Right;
            this.chk_ReceiveTimer.Location = new System.Drawing.Point(214, 373);
            this.chk_ReceiveTimer.Name = "chk_ReceiveTimer";
            this.chk_ReceiveTimer.Size = new System.Drawing.Size(112, 24);
            this.chk_ReceiveTimer.TabIndex = 41;
            this.chk_ReceiveTimer.Text = "定时收取邮件 每隔";
            this.chk_ReceiveTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chk_ReceiveTimer.UseVisualStyleBackColor = true;
            this.chk_ReceiveTimer.CheckedChanged += new System.EventHandler(this.chk_ReceiveTimer_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(3, 310);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 30);
            this.label14.TabIndex = 42;
            this.label14.Text = "默认邮箱";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.radio_default_no);
            this.panel1.Controls.Add(this.radio_default_yes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(96, 313);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(230, 24);
            this.panel1.TabIndex = 43;
            // 
            // radio_default_no
            // 
            this.radio_default_no.AutoSize = true;
            this.radio_default_no.Checked = true;
            this.radio_default_no.Location = new System.Drawing.Point(55, 4);
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
            this.radio_default_yes.Location = new System.Drawing.Point(3, 5);
            this.radio_default_yes.Name = "radio_default_yes";
            this.radio_default_yes.Size = new System.Drawing.Size(35, 16);
            this.radio_default_yes.TabIndex = 0;
            this.radio_default_yes.Text = "是";
            this.radio_default_yes.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(3, 340);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(87, 30);
            this.label15.TabIndex = 44;
            this.label15.Text = "初始收取日期";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataPicker_ReceiveBeginTime
            // 
            this.dataPicker_ReceiveBeginTime.CustomFormat = "yyyy-MM-dd";
            this.dataPicker_ReceiveBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dataPicker_ReceiveBeginTime.Location = new System.Drawing.Point(214, 343);
            this.dataPicker_ReceiveBeginTime.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dataPicker_ReceiveBeginTime.Name = "dataPicker_ReceiveBeginTime";
            this.dataPicker_ReceiveBeginTime.Size = new System.Drawing.Size(112, 21);
            this.dataPicker_ReceiveBeginTime.TabIndex = 45;
            this.dataPicker_ReceiveBeginTime.Visible = false;
            // 
            // combo_receiveTime
            // 
            this.combo_receiveTime.BackColor = System.Drawing.Color.White;
            this.combo_receiveTime.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_receiveTime.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_receiveTime.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combo_receiveTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_receiveTime.FormattingEnabled = true;
            this.combo_receiveTime.Location = new System.Drawing.Point(96, 343);
            this.combo_receiveTime.Name = "combo_receiveTime";
            this.combo_receiveTime.Size = new System.Drawing.Size(112, 22);
            this.combo_receiveTime.TabIndex = 46;
            this.combo_receiveTime.WaterText = "";
            // 
            // MailSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 505);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MailSettingForm";
            this.Text = "MailSetting";
            this.Load += new System.EventHandler(this.MailSetting_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripBtn_add;
        private System.Windows.Forms.ToolStripButton toolStripBtn_delete;
        private CCWin.SkinControl.SkinTreeView tree_mailbox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CCWin.SkinControl.SkinTextBox txt_MailAddress;
        private CCWin.SkinControl.SkinTextBox txt_MailPassword;
        private CCWin.SkinControl.SkinComboBox cbox_ProtocolTypeId;
        private CCWin.SkinControl.SkinTextBox txt_PopServer;
        private CCWin.SkinControl.SkinTextBox txt_PopPort;
        private CCWin.SkinControl.SkinTextBox txt_SmtpServer;
        private CCWin.SkinControl.SkinTextBox txt_SmtpPort;
        private CCWin.SkinControl.SkinComboBox cbox_KeepDays;
        private CCWin.SkinControl.SkinTextBox txt_ShowName;
        private CCWin.SkinControl.SkinTextBox txt_NickName;
        private CCWin.SkinControl.SkinTextBox txt_cc;
        private CCWin.SkinControl.SkinTextBox txt_bcc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private CCWin.SkinControl.SkinButton btn_save;
        private CCWin.SkinControl.SkinTextBox txt_ReceiveTimer;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chk_ReceiveTimer;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radio_default_no;
        private System.Windows.Forms.RadioButton radio_default_yes;
        private System.Windows.Forms.ImageList imageList_treeMailBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker dataPicker_ReceiveBeginTime;
        private CCWin.SkinControl.SkinComboBox combo_receiveTime;
    }
}