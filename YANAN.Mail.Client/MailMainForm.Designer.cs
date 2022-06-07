namespace YANAN.Mail.Client
{
    partial class MailMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailMainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageList_tabControl_left = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer_left = new CCWin.SkinControl.SkinSplitContainer();
            this.tabControl_mainleft = new CCWin.SkinControl.SkinTabControl();
            this.tabPage_mailBox = new CCWin.SkinControl.SkinTabPage();
            this.tree_mailbox = new CCWin.SkinControl.SkinTreeView();
            this.imageList_treeMailBox = new System.Windows.Forms.ImageList(this.components);
            this.tabPage_customer = new CCWin.SkinControl.SkinTabPage();
            this.tree_customer = new CCWin.SkinControl.SkinTreeView();
            this.tabPage_contacts = new CCWin.SkinControl.SkinTabPage();
            this.panel_contact_center = new System.Windows.Forms.Panel();
            this.dgrid_contact = new CCWin.SkinControl.SkinDataGridView();
            this.Linkman = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel_contact_top = new System.Windows.Forms.Panel();
            this.txt_contact_search = new CCWin.SkinControl.SkinTextBox();
            this.panel_top = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolBtnReceive = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnCompose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnReply = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnReplyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnForward = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnMove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripBtn_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBtnQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_center = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_left)).BeginInit();
            this.splitContainer_left.Panel1.SuspendLayout();
            this.splitContainer_left.SuspendLayout();
            this.tabControl_mainleft.SuspendLayout();
            this.tabPage_mailBox.SuspendLayout();
            this.tabPage_customer.SuspendLayout();
            this.tabPage_contacts.SuspendLayout();
            this.panel_contact_center.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrid_contact)).BeginInit();
            this.panel_contact_top.SuspendLayout();
            this.panel_top.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel_center.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList_tabControl_left
            // 
            this.imageList_tabControl_left.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_tabControl_left.ImageStream")));
            this.imageList_tabControl_left.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_tabControl_left.Images.SetKeyName(0, "mail_mail.png");
            this.imageList_tabControl_left.Images.SetKeyName(1, "mail_customer.png");
            this.imageList_tabControl_left.Images.SetKeyName(2, "mail_contact.png");
            // 
            // splitContainer_left
            // 
            this.splitContainer_left.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer_left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_left.LineBack = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.splitContainer_left.LineBack2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.splitContainer_left.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_left.Name = "splitContainer_left";
            // 
            // splitContainer_left.Panel1
            // 
            this.splitContainer_left.Panel1.Controls.Add(this.tabControl_mainleft);
            this.splitContainer_left.Panel1MinSize = 220;
            this.splitContainer_left.Size = new System.Drawing.Size(1366, 734);
            this.splitContainer_left.SplitterDistance = 220;
            this.splitContainer_left.TabIndex = 2;
            // 
            // tabControl_mainleft
            // 
            this.tabControl_mainleft.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl_mainleft.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.tabControl_mainleft.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.tabControl_mainleft.Controls.Add(this.tabPage_mailBox);
            this.tabControl_mainleft.Controls.Add(this.tabPage_customer);
            this.tabControl_mainleft.Controls.Add(this.tabPage_contacts);
            this.tabControl_mainleft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_mainleft.HeadBack = null;
            this.tabControl_mainleft.ImageList = this.imageList_tabControl_left;
            this.tabControl_mainleft.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.tabControl_mainleft.ItemSize = new System.Drawing.Size(70, 36);
            this.tabControl_mainleft.Location = new System.Drawing.Point(0, 0);
            this.tabControl_mainleft.Name = "tabControl_mainleft";
            this.tabControl_mainleft.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("tabControl_mainleft.PageArrowDown")));
            this.tabControl_mainleft.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("tabControl_mainleft.PageArrowHover")));
            this.tabControl_mainleft.PageBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tabControl_mainleft.PageBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.tabControl_mainleft.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("tabControl_mainleft.PageCloseHover")));
            this.tabControl_mainleft.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("tabControl_mainleft.PageCloseNormal")));
            this.tabControl_mainleft.PageDown = ((System.Drawing.Image)(resources.GetObject("tabControl_mainleft.PageDown")));
            this.tabControl_mainleft.PageHover = ((System.Drawing.Image)(resources.GetObject("tabControl_mainleft.PageHover")));
            this.tabControl_mainleft.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Top;
            this.tabControl_mainleft.PageNorml = null;
            this.tabControl_mainleft.SelectedIndex = 2;
            this.tabControl_mainleft.Size = new System.Drawing.Size(220, 734);
            this.tabControl_mainleft.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl_mainleft.TabIndex = 1;
            this.tabControl_mainleft.TabStop = false;
            // 
            // tabPage_mailBox
            // 
            this.tabPage_mailBox.BackColor = System.Drawing.Color.White;
            this.tabPage_mailBox.Controls.Add(this.tree_mailbox);
            this.tabPage_mailBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage_mailBox.ImageKey = "mail_mail.png";
            this.tabPage_mailBox.Location = new System.Drawing.Point(0, 0);
            this.tabPage_mailBox.Name = "tabPage_mailBox";
            this.tabPage_mailBox.Size = new System.Drawing.Size(220, 698);
            this.tabPage_mailBox.TabIndex = 0;
            this.tabPage_mailBox.TabItemImage = null;
            this.tabPage_mailBox.Text = "邮箱";
            // 
            // tree_mailbox
            // 
            this.tree_mailbox.BackColor = System.Drawing.Color.White;
            this.tree_mailbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tree_mailbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree_mailbox.HideSelection = false;
            this.tree_mailbox.ImageIndex = 0;
            this.tree_mailbox.ImageList = this.imageList_treeMailBox;
            this.tree_mailbox.ItemHeight = 28;
            this.tree_mailbox.Location = new System.Drawing.Point(0, 0);
            this.tree_mailbox.Name = "tree_mailbox";
            this.tree_mailbox.SelectedImageIndex = 0;
            this.tree_mailbox.ShowLines = false;
            this.tree_mailbox.ShowPlusMinus = false;
            this.tree_mailbox.ShowRootLines = false;
            this.tree_mailbox.Size = new System.Drawing.Size(220, 698);
            this.tree_mailbox.TabIndex = 0;
            this.tree_mailbox.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tree_mailbox_BeforeLabelEdit);
            this.tree_mailbox.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tree_mailbox_AfterCollapse);
            this.tree_mailbox.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tree_mailbox_AfterExpand);
            // 
            // imageList_treeMailBox
            // 
            this.imageList_treeMailBox.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_treeMailBox.ImageStream")));
            this.imageList_treeMailBox.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_treeMailBox.Images.SetKeyName(0, "collapse.png");
            this.imageList_treeMailBox.Images.SetKeyName(1, "expand.png");
            this.imageList_treeMailBox.Images.SetKeyName(2, "mail_deleted.png");
            this.imageList_treeMailBox.Images.SetKeyName(3, "mail_draft.png");
            this.imageList_treeMailBox.Images.SetKeyName(4, "mail_label.png");
            this.imageList_treeMailBox.Images.SetKeyName(5, "mail_receive_box.png");
            this.imageList_treeMailBox.Images.SetKeyName(6, "mail_rubbish.png");
            this.imageList_treeMailBox.Images.SetKeyName(7, "mail_send.png");
            this.imageList_treeMailBox.Images.SetKeyName(8, "mail_set_top.png");
            this.imageList_treeMailBox.Images.SetKeyName(9, "mail_unread.png");
            // 
            // tabPage_customer
            // 
            this.tabPage_customer.BackColor = System.Drawing.Color.White;
            this.tabPage_customer.Controls.Add(this.tree_customer);
            this.tabPage_customer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage_customer.ImageKey = "mail_customer.png";
            this.tabPage_customer.Location = new System.Drawing.Point(0, 0);
            this.tabPage_customer.Name = "tabPage_customer";
            this.tabPage_customer.Size = new System.Drawing.Size(220, 698);
            this.tabPage_customer.TabIndex = 1;
            this.tabPage_customer.TabItemImage = null;
            this.tabPage_customer.Text = "客户";
            // 
            // tree_customer
            // 
            this.tree_customer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tree_customer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree_customer.ItemHeight = 28;
            this.tree_customer.Location = new System.Drawing.Point(0, 0);
            this.tree_customer.Name = "tree_customer";
            this.tree_customer.ShowLines = false;
            this.tree_customer.ShowPlusMinus = false;
            this.tree_customer.ShowRootLines = false;
            this.tree_customer.Size = new System.Drawing.Size(220, 698);
            this.tree_customer.TabIndex = 0;
            // 
            // tabPage_contacts
            // 
            this.tabPage_contacts.BackColor = System.Drawing.Color.White;
            this.tabPage_contacts.Controls.Add(this.panel_contact_center);
            this.tabPage_contacts.Controls.Add(this.panel_contact_top);
            this.tabPage_contacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage_contacts.ImageKey = "mail_contact.png";
            this.tabPage_contacts.Location = new System.Drawing.Point(0, 0);
            this.tabPage_contacts.Name = "tabPage_contacts";
            this.tabPage_contacts.Size = new System.Drawing.Size(220, 698);
            this.tabPage_contacts.TabIndex = 2;
            this.tabPage_contacts.TabItemImage = null;
            this.tabPage_contacts.Text = "通讯录";
            // 
            // panel_contact_center
            // 
            this.panel_contact_center.Controls.Add(this.dgrid_contact);
            this.panel_contact_center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_contact_center.Location = new System.Drawing.Point(0, 30);
            this.panel_contact_center.Name = "panel_contact_center";
            this.panel_contact_center.Size = new System.Drawing.Size(220, 668);
            this.panel_contact_center.TabIndex = 1;
            // 
            // dgrid_contact
            // 
            this.dgrid_contact.AllowUserToAddRows = false;
            this.dgrid_contact.AllowUserToDeleteRows = false;
            this.dgrid_contact.AllowUserToResizeRows = false;
            this.dgrid_contact.AlternatingCellBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgrid_contact.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgrid_contact.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrid_contact.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgrid_contact.BackgroundColor = System.Drawing.Color.White;
            this.dgrid_contact.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgrid_contact.ColumnFont = new System.Drawing.Font("Arial", 9F);
            this.dgrid_contact.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrid_contact.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgrid_contact.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrid_contact.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Linkman});
            this.dgrid_contact.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrid_contact.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgrid_contact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrid_contact.EnableHeadersVisualStyles = false;
            this.dgrid_contact.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgrid_contact.HeadFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgrid_contact.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgrid_contact.LineNumber = false;
            this.dgrid_contact.Location = new System.Drawing.Point(0, 0);
            this.dgrid_contact.MultiSelect = false;
            this.dgrid_contact.Name = "dgrid_contact";
            this.dgrid_contact.ReadOnly = true;
            this.dgrid_contact.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgrid_contact.RowHeadersVisible = false;
            this.dgrid_contact.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrid_contact.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgrid_contact.RowTemplate.Height = 23;
            this.dgrid_contact.RowTemplate.ReadOnly = true;
            this.dgrid_contact.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrid_contact.Size = new System.Drawing.Size(220, 668);
            this.dgrid_contact.TabIndex = 2;
            this.dgrid_contact.TitleBack = null;
            this.dgrid_contact.TitleBackColorBegin = System.Drawing.Color.White;
            this.dgrid_contact.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            // 
            // Linkman
            // 
            this.Linkman.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Linkman.DataPropertyName = "Linkman";
            this.Linkman.FillWeight = 14.99F;
            this.Linkman.HeaderText = "联系人";
            this.Linkman.MinimumWidth = 100;
            this.Linkman.Name = "Linkman";
            this.Linkman.ReadOnly = true;
            // 
            // panel_contact_top
            // 
            this.panel_contact_top.Controls.Add(this.txt_contact_search);
            this.panel_contact_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_contact_top.Location = new System.Drawing.Point(0, 0);
            this.panel_contact_top.Name = "panel_contact_top";
            this.panel_contact_top.Size = new System.Drawing.Size(220, 30);
            this.panel_contact_top.TabIndex = 0;
            // 
            // txt_contact_search
            // 
            this.txt_contact_search.BackColor = System.Drawing.Color.White;
            this.txt_contact_search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_contact_search.DownBack = null;
            this.txt_contact_search.Icon = global::YANAN.Mail.Client.Properties.Resources.search;
            this.txt_contact_search.IconIsButton = true;
            this.txt_contact_search.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_contact_search.IsPasswordChat = '\0';
            this.txt_contact_search.IsSystemPasswordChar = false;
            this.txt_contact_search.Lines = new string[0];
            this.txt_contact_search.Location = new System.Drawing.Point(0, 0);
            this.txt_contact_search.Margin = new System.Windows.Forms.Padding(0);
            this.txt_contact_search.MaxLength = 32767;
            this.txt_contact_search.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_contact_search.MouseBack = null;
            this.txt_contact_search.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_contact_search.Multiline = true;
            this.txt_contact_search.Name = "txt_contact_search";
            this.txt_contact_search.NormlBack = null;
            this.txt_contact_search.Padding = new System.Windows.Forms.Padding(5, 5, 28, 5);
            this.txt_contact_search.ReadOnly = false;
            this.txt_contact_search.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_contact_search.Size = new System.Drawing.Size(220, 30);
            // 
            // 
            // 
            this.txt_contact_search.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_contact_search.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_contact_search.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_contact_search.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_contact_search.SkinTxt.Multiline = true;
            this.txt_contact_search.SkinTxt.Name = "BaseText";
            this.txt_contact_search.SkinTxt.Size = new System.Drawing.Size(187, 20);
            this.txt_contact_search.SkinTxt.TabIndex = 0;
            this.txt_contact_search.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_contact_search.SkinTxt.WaterText = "";
            this.txt_contact_search.SkinTxt.WordWrap = false;
            this.txt_contact_search.TabIndex = 0;
            this.txt_contact_search.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_contact_search.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_contact_search.WaterText = "";
            this.txt_contact_search.WordWrap = false;
            // 
            // panel_top
            // 
            this.panel_top.Controls.Add(this.menuStrip1);
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.Location = new System.Drawing.Point(0, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(1366, 34);
            this.panel_top.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackgroundImage = global::YANAN.Mail.Client.Properties.Resources.bg_white;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnReceive,
            this.toolBtnCompose,
            this.toolBtnReply,
            this.toolBtnReplyAll,
            this.toolBtnForward,
            this.toolBtnMove,
            this.toolStripBtn_delete,
            this.toolBtnArchive,
            this.toolBtnSetting,
            this.toolBtnRefresh,
            this.toolBtnQuit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1366, 34);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolBtnReceive
            // 
            this.toolBtnReceive.Image = global::YANAN.Mail.Client.Properties.Resources.mail_receive;
            this.toolBtnReceive.Name = "toolBtnReceive";
            this.toolBtnReceive.Size = new System.Drawing.Size(60, 30);
            this.toolBtnReceive.Text = "收取";
            // 
            // toolBtnCompose
            // 
            this.toolBtnCompose.Image = global::YANAN.Mail.Client.Properties.Resources.mail_write;
            this.toolBtnCompose.Name = "toolBtnCompose";
            this.toolBtnCompose.Size = new System.Drawing.Size(72, 30);
            this.toolBtnCompose.Text = "写邮件";
            // 
            // toolBtnReply
            // 
            this.toolBtnReply.Image = global::YANAN.Mail.Client.Properties.Resources.mail_reply;
            this.toolBtnReply.Name = "toolBtnReply";
            this.toolBtnReply.Size = new System.Drawing.Size(60, 30);
            this.toolBtnReply.Text = "回复";
            // 
            // toolBtnReplyAll
            // 
            this.toolBtnReplyAll.Image = global::YANAN.Mail.Client.Properties.Resources.mail_reply_all;
            this.toolBtnReplyAll.Name = "toolBtnReplyAll";
            this.toolBtnReplyAll.Size = new System.Drawing.Size(84, 30);
            this.toolBtnReplyAll.Text = "回复全部";
            // 
            // toolBtnForward
            // 
            this.toolBtnForward.Image = global::YANAN.Mail.Client.Properties.Resources.mail_forward;
            this.toolBtnForward.Name = "toolBtnForward";
            this.toolBtnForward.Size = new System.Drawing.Size(60, 30);
            this.toolBtnForward.Text = "转发";
            // 
            // toolBtnMove
            // 
            this.toolBtnMove.Image = global::YANAN.Mail.Client.Properties.Resources.mail_move;
            this.toolBtnMove.Name = "toolBtnMove";
            this.toolBtnMove.Size = new System.Drawing.Size(60, 30);
            this.toolBtnMove.Text = "移动";
            // 
            // toolStripBtn_delete
            // 
            this.toolStripBtn_delete.Image = global::YANAN.Mail.Client.Properties.Resources.mail_delete;
            this.toolStripBtn_delete.Name = "toolStripBtn_delete";
            this.toolStripBtn_delete.Size = new System.Drawing.Size(60, 30);
            this.toolStripBtn_delete.Text = "删除";
            // 
            // toolBtnArchive
            // 
            this.toolBtnArchive.Image = global::YANAN.Mail.Client.Properties.Resources.mail_move;
            this.toolBtnArchive.Name = "toolBtnArchive";
            this.toolBtnArchive.Size = new System.Drawing.Size(60, 30);
            this.toolBtnArchive.Text = "归档";
            // 
            // toolBtnSetting
            // 
            this.toolBtnSetting.Image = global::YANAN.Mail.Client.Properties.Resources.mail_more;
            this.toolBtnSetting.Name = "toolBtnSetting";
            this.toolBtnSetting.Size = new System.Drawing.Size(60, 30);
            this.toolBtnSetting.Text = "设置";
            // 
            // toolBtnRefresh
            // 
            this.toolBtnRefresh.Image = global::YANAN.Mail.Client.Properties.Resources.refresh;
            this.toolBtnRefresh.Name = "toolBtnRefresh";
            this.toolBtnRefresh.Size = new System.Drawing.Size(60, 30);
            this.toolBtnRefresh.Text = "刷新";
            // 
            // toolBtnQuit
            // 
            this.toolBtnQuit.Image = global::YANAN.Mail.Client.Properties.Resources.mail_quit;
            this.toolBtnQuit.Name = "toolBtnQuit";
            this.toolBtnQuit.Size = new System.Drawing.Size(60, 30);
            this.toolBtnQuit.Text = "退出";
            // 
            // panel_center
            // 
            this.panel_center.Controls.Add(this.splitContainer_left);
            this.panel_center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_center.Location = new System.Drawing.Point(0, 34);
            this.panel_center.Name = "panel_center";
            this.panel_center.Size = new System.Drawing.Size(1366, 734);
            this.panel_center.TabIndex = 4;
            // 
            // MailMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.panel_center);
            this.Controls.Add(this.panel_top);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MailMainForm";
            this.ShowInTaskbar = false;
            this.Text = "邮件主界面";
            this.splitContainer_left.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_left)).EndInit();
            this.splitContainer_left.ResumeLayout(false);
            this.tabControl_mainleft.ResumeLayout(false);
            this.tabPage_mailBox.ResumeLayout(false);
            this.tabPage_customer.ResumeLayout(false);
            this.tabPage_contacts.ResumeLayout(false);
            this.panel_contact_center.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrid_contact)).EndInit();
            this.panel_contact_top.ResumeLayout(false);
            this.panel_top.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel_center.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList_tabControl_left;
        private CCWin.SkinControl.SkinSplitContainer splitContainer_left;
        private CCWin.SkinControl.SkinTabControl tabControl_mainleft;
        private CCWin.SkinControl.SkinTabPage tabPage_mailBox;
        private CCWin.SkinControl.SkinTabPage tabPage_customer;
        private CCWin.SkinControl.SkinTabPage tabPage_contacts;
        private CCWin.SkinControl.SkinTreeView tree_mailbox;
        private CCWin.SkinControl.SkinTreeView tree_customer;
        private System.Windows.Forms.ImageList imageList_treeMailBox;
        private System.Windows.Forms.Panel panel_contact_center;
        private System.Windows.Forms.Panel panel_contact_top;
        private CCWin.SkinControl.SkinTextBox txt_contact_search;
        private CCWin.SkinControl.SkinDataGridView dgrid_contact;
        private System.Windows.Forms.DataGridViewTextBoxColumn Linkman;
        private System.Windows.Forms.Panel panel_top;
        private System.Windows.Forms.Panel panel_center;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolBtnReceive;
        private System.Windows.Forms.ToolStripMenuItem toolBtnCompose;
        private System.Windows.Forms.ToolStripMenuItem toolBtnReply;
        private System.Windows.Forms.ToolStripMenuItem toolBtnReplyAll;
        private System.Windows.Forms.ToolStripMenuItem toolBtnForward;
        private System.Windows.Forms.ToolStripMenuItem toolBtnMove;
        private System.Windows.Forms.ToolStripMenuItem toolStripBtn_delete;
        private System.Windows.Forms.ToolStripMenuItem toolBtnSetting;
        private System.Windows.Forms.ToolStripMenuItem toolBtnQuit;
        private System.Windows.Forms.ToolStripMenuItem toolBtnRefresh;
        private System.Windows.Forms.ToolStripMenuItem toolBtnArchive;
    }
}