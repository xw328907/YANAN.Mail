namespace YANAN.Mail.Client
{
    partial class MailLabelForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tablelayout_right = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_save = new CCWin.SkinControl.SkinButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Memo = new CCWin.SkinControl.SkinTextBox();
            this.txt_MailLabelName = new CCWin.SkinControl.SkinTextBox();
            this.btn_colorSelect = new System.Windows.Forms.Button();
            this.lbl_Color = new System.Windows.Forms.Label();
            this.panel_left = new System.Windows.Forms.Panel();
            this.dg_label = new CCWin.SkinControl.SkinDataGridView();
            this.ColorDisplay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MailLabelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MailLabelId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Memo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolSctrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.toolBtn_add = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_delete = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tablelayout_right.SuspendLayout();
            this.panel_left.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_label)).BeginInit();
            this.toolSctrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablelayout_right
            // 
            this.tablelayout_right.ColumnCount = 3;
            this.tablelayout_right.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tablelayout_right.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tablelayout_right.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 164F));
            this.tablelayout_right.Controls.Add(this.label2, 0, 1);
            this.tablelayout_right.Controls.Add(this.label1, 0, 0);
            this.tablelayout_right.Controls.Add(this.btn_save, 1, 3);
            this.tablelayout_right.Controls.Add(this.label3, 0, 2);
            this.tablelayout_right.Controls.Add(this.txt_Memo, 1, 2);
            this.tablelayout_right.Controls.Add(this.txt_MailLabelName, 1, 0);
            this.tablelayout_right.Controls.Add(this.btn_colorSelect, 2, 1);
            this.tablelayout_right.Controls.Add(this.lbl_Color, 1, 1);
            this.tablelayout_right.Location = new System.Drawing.Point(15, 12);
            this.tablelayout_right.Margin = new System.Windows.Forms.Padding(8);
            this.tablelayout_right.Name = "tablelayout_right";
            this.tablelayout_right.RowCount = 4;
            this.tablelayout_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tablelayout_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tablelayout_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tablelayout_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tablelayout_right.Size = new System.Drawing.Size(384, 362);
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
            this.label2.Text = "标签颜色";
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
            this.label1.Text = "标签名称";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.Red;
            this.btn_save.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btn_save.DownBack = null;
            this.btn_save.DownBaseColor = System.Drawing.Color.Transparent;
            this.btn_save.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btn_save.ForeColor = System.Drawing.Color.White;
            this.btn_save.Location = new System.Drawing.Point(70, 135);
            this.btn_save.Margin = new System.Windows.Forms.Padding(10, 15, 3, 3);
            this.btn_save.MouseBack = null;
            this.btn_save.Name = "btn_save";
            this.btn_save.NormlBack = null;
            this.btn_save.Size = new System.Drawing.Size(90, 34);
            this.btn_save.TabIndex = 48;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 60);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label3.Size = new System.Drawing.Size(54, 60);
            this.label3.TabIndex = 49;
            this.label3.Text = "标签备注";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txt_Memo
            // 
            this.txt_Memo.BackColor = System.Drawing.Color.White;
            this.tablelayout_right.SetColumnSpan(this.txt_Memo, 2);
            this.txt_Memo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Memo.DownBack = null;
            this.txt_Memo.Icon = null;
            this.txt_Memo.IconIsButton = false;
            this.txt_Memo.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_Memo.IsPasswordChat = '\0';
            this.txt_Memo.IsSystemPasswordChar = false;
            this.txt_Memo.Lines = new string[0];
            this.txt_Memo.Location = new System.Drawing.Point(60, 60);
            this.txt_Memo.Margin = new System.Windows.Forms.Padding(0);
            this.txt_Memo.MaxLength = 32767;
            this.txt_Memo.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_Memo.MouseBack = null;
            this.txt_Memo.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_Memo.Multiline = true;
            this.txt_Memo.Name = "txt_Memo";
            this.txt_Memo.NormlBack = null;
            this.txt_Memo.Padding = new System.Windows.Forms.Padding(5);
            this.txt_Memo.ReadOnly = false;
            this.txt_Memo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_Memo.Size = new System.Drawing.Size(324, 60);
            // 
            // 
            // 
            this.txt_Memo.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Memo.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Memo.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_Memo.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_Memo.SkinTxt.Multiline = true;
            this.txt_Memo.SkinTxt.Name = "BaseText";
            this.txt_Memo.SkinTxt.Size = new System.Drawing.Size(314, 50);
            this.txt_Memo.SkinTxt.TabIndex = 0;
            this.txt_Memo.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_Memo.SkinTxt.WaterText = "";
            this.txt_Memo.TabIndex = 50;
            this.txt_Memo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_Memo.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_Memo.WaterText = "";
            this.txt_Memo.WordWrap = true;
            // 
            // txt_MailLabelName
            // 
            this.txt_MailLabelName.BackColor = System.Drawing.Color.White;
            this.tablelayout_right.SetColumnSpan(this.txt_MailLabelName, 2);
            this.txt_MailLabelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_MailLabelName.DownBack = null;
            this.txt_MailLabelName.Icon = null;
            this.txt_MailLabelName.IconIsButton = false;
            this.txt_MailLabelName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_MailLabelName.IsPasswordChat = '\0';
            this.txt_MailLabelName.IsSystemPasswordChar = false;
            this.txt_MailLabelName.Lines = new string[0];
            this.txt_MailLabelName.Location = new System.Drawing.Point(60, 0);
            this.txt_MailLabelName.Margin = new System.Windows.Forms.Padding(0);
            this.txt_MailLabelName.MaxLength = 32767;
            this.txt_MailLabelName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_MailLabelName.MouseBack = null;
            this.txt_MailLabelName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_MailLabelName.Multiline = true;
            this.txt_MailLabelName.Name = "txt_MailLabelName";
            this.txt_MailLabelName.NormlBack = null;
            this.txt_MailLabelName.Padding = new System.Windows.Forms.Padding(5);
            this.txt_MailLabelName.ReadOnly = false;
            this.txt_MailLabelName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_MailLabelName.Size = new System.Drawing.Size(324, 30);
            // 
            // 
            // 
            this.txt_MailLabelName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_MailLabelName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_MailLabelName.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_MailLabelName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_MailLabelName.SkinTxt.Multiline = true;
            this.txt_MailLabelName.SkinTxt.Name = "BaseText";
            this.txt_MailLabelName.SkinTxt.Size = new System.Drawing.Size(314, 20);
            this.txt_MailLabelName.SkinTxt.TabIndex = 0;
            this.txt_MailLabelName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_MailLabelName.SkinTxt.WaterText = "";
            this.txt_MailLabelName.TabIndex = 45;
            this.txt_MailLabelName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_MailLabelName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_MailLabelName.WaterText = "";
            this.txt_MailLabelName.WordWrap = true;
            // 
            // btn_colorSelect
            // 
            this.btn_colorSelect.Location = new System.Drawing.Point(223, 33);
            this.btn_colorSelect.Name = "btn_colorSelect";
            this.btn_colorSelect.Size = new System.Drawing.Size(32, 24);
            this.btn_colorSelect.TabIndex = 1;
            this.btn_colorSelect.Text = "...";
            this.btn_colorSelect.UseVisualStyleBackColor = true;
            // 
            // lbl_Color
            // 
            this.lbl_Color.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Color.Location = new System.Drawing.Point(63, 30);
            this.lbl_Color.Name = "lbl_Color";
            this.lbl_Color.Size = new System.Drawing.Size(154, 30);
            this.lbl_Color.TabIndex = 51;
            this.lbl_Color.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_left
            // 
            this.panel_left.Controls.Add(this.dg_label);
            this.panel_left.Controls.Add(this.toolSctrip1);
            this.panel_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_left.Location = new System.Drawing.Point(0, 0);
            this.panel_left.Name = "panel_left";
            this.panel_left.Size = new System.Drawing.Size(185, 505);
            this.panel_left.TabIndex = 2;
            // 
            // dg_label
            // 
            this.dg_label.AllowUserToAddRows = false;
            this.dg_label.AllowUserToDeleteRows = false;
            this.dg_label.AllowUserToResizeRows = false;
            this.dg_label.AlternatingCellBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            this.dg_label.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dg_label.BackgroundColor = System.Drawing.Color.White;
            this.dg_label.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dg_label.ColumnFont = null;
            this.dg_label.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_label.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dg_label.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_label.ColumnHeadersVisible = false;
            this.dg_label.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColorDisplay,
            this.MailLabelName,
            this.Color,
            this.MailLabelId,
            this.Memo});
            this.dg_label.ColumnSelectBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(235)))), ((int)(((byte)(252)))));
            this.dg_label.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_label.DefaultCellStyle = dataGridViewCellStyle3;
            this.dg_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_label.EnableHeadersVisualStyles = false;
            this.dg_label.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dg_label.HeadFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dg_label.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.dg_label.Location = new System.Drawing.Point(0, 0);
            this.dg_label.MultiSelect = false;
            this.dg_label.Name = "dg_label";
            this.dg_label.ReadOnly = true;
            this.dg_label.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dg_label.RowHeadersVisible = false;
            this.dg_label.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(235)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dg_label.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dg_label.RowTemplate.Height = 28;
            this.dg_label.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dg_label.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_label.Size = new System.Drawing.Size(185, 480);
            this.dg_label.TabIndex = 5;
            this.dg_label.TabStop = false;
            this.dg_label.TitleBack = null;
            this.dg_label.TitleBackColorBegin = System.Drawing.Color.White;
            this.dg_label.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // ColorDisplay
            // 
            this.ColorDisplay.DataPropertyName = "ColorDisplay";
            this.ColorDisplay.HeaderText = "标签颜色";
            this.ColorDisplay.Name = "ColorDisplay";
            this.ColorDisplay.ReadOnly = true;
            this.ColorDisplay.Width = 28;
            // 
            // MailLabelName
            // 
            this.MailLabelName.DataPropertyName = "MailLabelName";
            this.MailLabelName.HeaderText = "标签名称";
            this.MailLabelName.Name = "MailLabelName";
            this.MailLabelName.ReadOnly = true;
            this.MailLabelName.Width = 152;
            // 
            // Color
            // 
            this.Color.DataPropertyName = "Color";
            this.Color.FillWeight = 1F;
            this.Color.HeaderText = "标签颜色值";
            this.Color.Name = "Color";
            this.Color.ReadOnly = true;
            this.Color.Visible = false;
            this.Color.Width = 20;
            // 
            // MailLabelId
            // 
            this.MailLabelId.DataPropertyName = "MailLabelId";
            this.MailLabelId.HeaderText = "标签主键ID";
            this.MailLabelId.Name = "MailLabelId";
            this.MailLabelId.ReadOnly = true;
            this.MailLabelId.Visible = false;
            // 
            // Memo
            // 
            this.Memo.DataPropertyName = "Memo";
            this.Memo.HeaderText = "标签备注";
            this.Memo.Name = "Memo";
            this.Memo.ReadOnly = true;
            this.Memo.Visible = false;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.tablelayout_right);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(185, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(499, 505);
            this.panel1.TabIndex = 3;
            // 
            // MailLabelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 505);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_left);
            this.Name = "MailLabelForm";
            this.Text = "邮箱模板";
            this.tablelayout_right.ResumeLayout(false);
            this.tablelayout_right.PerformLayout();
            this.panel_left.ResumeLayout(false);
            this.panel_left.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_label)).EndInit();
            this.toolSctrip1.ResumeLayout(false);
            this.toolSctrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tablelayout_right;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private CCWin.SkinControl.SkinTextBox txt_MailLabelName;
        private CCWin.SkinControl.SkinButton btn_save;
        private System.Windows.Forms.Panel panel_left;
        private CCWin.SkinControl.SkinToolStrip toolSctrip1;
        private System.Windows.Forms.ToolStripButton toolBtn_add;
        private System.Windows.Forms.ToolStripButton toolBtn_delete;
        private System.Windows.Forms.Label label3;
        private CCWin.SkinControl.SkinDataGridView dg_label;
        private CCWin.SkinControl.SkinTextBox txt_Memo;
        private System.Windows.Forms.Button btn_colorSelect;
        private System.Windows.Forms.Label lbl_Color;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColorDisplay;
        private System.Windows.Forms.DataGridViewTextBoxColumn MailLabelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Color;
        private System.Windows.Forms.DataGridViewTextBoxColumn MailLabelId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Memo;
    }
}