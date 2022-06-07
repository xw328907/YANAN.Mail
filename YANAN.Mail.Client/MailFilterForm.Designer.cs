namespace YANAN.Mail.Client
{
    partial class MailFilterForm
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
            this.panel_main = new System.Windows.Forms.Panel();
            this.panel_right = new System.Windows.Forms.Panel();
            this.btn_save = new CCWin.SkinControl.SkinButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.combo_mail_label = new CCWin.SkinControl.SkinComboBox();
            this.chk_stop_other = new System.Windows.Forms.CheckBox();
            this.chk_set_mail_read = new System.Windows.Forms.CheckBox();
            this.chk_set_label = new System.Windows.Forms.CheckBox();
            this.combo_move_folder = new CCWin.SkinControl.SkinComboBox();
            this.chk_move_folder = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radio_event_delete = new System.Windows.Forms.RadioButton();
            this.radio_event_normal = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.combo_attach = new CCWin.SkinControl.SkinComboBox();
            this.chk_attach = new System.Windows.Forms.CheckBox();
            this.txt_mailsize_max = new System.Windows.Forms.NumericUpDown();
            this.txt_mailsize_min = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chk_mailsize = new System.Windows.Forms.CheckBox();
            this.chk_subject_UpperLower = new System.Windows.Forms.CheckBox();
            this.txt_subject_value = new CCWin.SkinControl.SkinTextBox();
            this.combo_subject_operate = new CCWin.SkinControl.SkinComboBox();
            this.chk_subject = new System.Windows.Forms.CheckBox();
            this.chk_send_UpperLower = new System.Windows.Forms.CheckBox();
            this.txt_send_value = new CCWin.SkinControl.SkinTextBox();
            this.combo_send_operate = new CCWin.SkinControl.SkinComboBox();
            this.chk_send = new System.Windows.Forms.CheckBox();
            this.chk_receive_UpperLower = new System.Windows.Forms.CheckBox();
            this.txt_receive_value = new CCWin.SkinControl.SkinTextBox();
            this.combo_receive_operate = new CCWin.SkinControl.SkinComboBox();
            this.chk_receive = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radio_condition_any = new System.Windows.Forms.RadioButton();
            this.radio_condtion_all = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.chk_FilterDoTime_send = new System.Windows.Forms.CheckBox();
            this.chk_FilterDoTime_receive = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chk_IsFilter = new System.Windows.Forms.CheckBox();
            this.txt_FilterName = new CCWin.SkinControl.SkinTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_left = new System.Windows.Forms.Panel();
            this.dgrid_filter = new CCWin.SkinControl.SkinDataGridView();
            this.FilterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsFilter = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FilterConditionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.toolBtn_add = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_delete = new System.Windows.Forms.ToolStripButton();
            this.panel_left_top = new System.Windows.Forms.Panel();
            this.combo_mailbox = new CCWin.SkinControl.SkinComboBox();
            this.panel_main.SuspendLayout();
            this.panel_right.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_mailsize_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_mailsize_min)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel_left.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrid_filter)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel_left_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.panel_right);
            this.panel_main.Controls.Add(this.panel_left);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(0, 0);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(680, 600);
            this.panel_main.TabIndex = 1;
            // 
            // panel_right
            // 
            this.panel_right.Controls.Add(this.btn_save);
            this.panel_right.Controls.Add(this.groupBox2);
            this.panel_right.Controls.Add(this.groupBox1);
            this.panel_right.Controls.Add(this.panel1);
            this.panel_right.Controls.Add(this.label3);
            this.panel_right.Controls.Add(this.chk_FilterDoTime_send);
            this.panel_right.Controls.Add(this.chk_FilterDoTime_receive);
            this.panel_right.Controls.Add(this.label2);
            this.panel_right.Controls.Add(this.chk_IsFilter);
            this.panel_right.Controls.Add(this.txt_FilterName);
            this.panel_right.Controls.Add(this.label1);
            this.panel_right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_right.Location = new System.Drawing.Point(200, 0);
            this.panel_right.Name = "panel_right";
            this.panel_right.Size = new System.Drawing.Size(480, 600);
            this.panel_right.TabIndex = 2;
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.Red;
            this.btn_save.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btn_save.DownBack = null;
            this.btn_save.DownBaseColor = System.Drawing.Color.Transparent;
            this.btn_save.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btn_save.ForeColor = System.Drawing.Color.White;
            this.btn_save.Location = new System.Drawing.Point(163, 543);
            this.btn_save.MouseBack = null;
            this.btn_save.Name = "btn_save";
            this.btn_save.NormlBack = null;
            this.btn_save.Size = new System.Drawing.Size(90, 35);
            this.btn_save.TabIndex = 38;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.combo_mail_label);
            this.groupBox2.Controls.Add(this.chk_stop_other);
            this.groupBox2.Controls.Add(this.chk_set_mail_read);
            this.groupBox2.Controls.Add(this.chk_set_label);
            this.groupBox2.Controls.Add(this.combo_move_folder);
            this.groupBox2.Controls.Add(this.chk_move_folder);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Location = new System.Drawing.Point(20, 323);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(431, 204);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "过滤执行操作 ";
            // 
            // combo_mail_label
            // 
            this.combo_mail_label.BackColor = System.Drawing.Color.White;
            this.combo_mail_label.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_mail_label.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_mail_label.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combo_mail_label.FormattingEnabled = true;
            this.combo_mail_label.ItemHeight = 20;
            this.combo_mail_label.Location = new System.Drawing.Point(101, 95);
            this.combo_mail_label.Name = "combo_mail_label";
            this.combo_mail_label.Size = new System.Drawing.Size(200, 26);
            this.combo_mail_label.TabIndex = 17;
            this.combo_mail_label.WaterText = "";
            // 
            // chk_stop_other
            // 
            this.chk_stop_other.Location = new System.Drawing.Point(10, 161);
            this.chk_stop_other.Name = "chk_stop_other";
            this.chk_stop_other.Size = new System.Drawing.Size(156, 26);
            this.chk_stop_other.TabIndex = 15;
            this.chk_stop_other.Text = "停止处理其他规则";
            this.chk_stop_other.UseVisualStyleBackColor = true;
            // 
            // chk_set_mail_read
            // 
            this.chk_set_mail_read.Location = new System.Drawing.Point(10, 129);
            this.chk_set_mail_read.Name = "chk_set_mail_read";
            this.chk_set_mail_read.Size = new System.Drawing.Size(156, 26);
            this.chk_set_mail_read.TabIndex = 14;
            this.chk_set_mail_read.Tag = "1200";
            this.chk_set_mail_read.Text = "将邮件标记为已读";
            this.chk_set_mail_read.UseVisualStyleBackColor = true;
            // 
            // chk_set_label
            // 
            this.chk_set_label.Location = new System.Drawing.Point(10, 95);
            this.chk_set_label.Name = "chk_set_label";
            this.chk_set_label.Size = new System.Drawing.Size(84, 26);
            this.chk_set_label.TabIndex = 12;
            this.chk_set_label.Tag = "1100";
            this.chk_set_label.Text = "设置标签";
            this.chk_set_label.UseVisualStyleBackColor = true;
            // 
            // combo_move_folder
            // 
            this.combo_move_folder.BackColor = System.Drawing.Color.White;
            this.combo_move_folder.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_move_folder.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_move_folder.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combo_move_folder.FormattingEnabled = true;
            this.combo_move_folder.ItemHeight = 20;
            this.combo_move_folder.Location = new System.Drawing.Point(101, 57);
            this.combo_move_folder.Name = "combo_move_folder";
            this.combo_move_folder.Size = new System.Drawing.Size(200, 26);
            this.combo_move_folder.TabIndex = 11;
            this.combo_move_folder.WaterText = "";
            // 
            // chk_move_folder
            // 
            this.chk_move_folder.Location = new System.Drawing.Point(10, 57);
            this.chk_move_folder.Name = "chk_move_folder";
            this.chk_move_folder.Size = new System.Drawing.Size(84, 26);
            this.chk_move_folder.TabIndex = 10;
            this.chk_move_folder.Tag = "1000";
            this.chk_move_folder.Text = "移动至";
            this.chk_move_folder.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radio_event_delete);
            this.panel2.Controls.Add(this.radio_event_normal);
            this.panel2.Location = new System.Drawing.Point(6, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(376, 23);
            this.panel2.TabIndex = 9;
            // 
            // radio_event_delete
            // 
            this.radio_event_delete.AutoSize = true;
            this.radio_event_delete.Location = new System.Drawing.Point(171, 3);
            this.radio_event_delete.Name = "radio_event_delete";
            this.radio_event_delete.Size = new System.Drawing.Size(71, 16);
            this.radio_event_delete.TabIndex = 8;
            this.radio_event_delete.Tag = "1300";
            this.radio_event_delete.Text = "彻底删除";
            this.radio_event_delete.UseVisualStyleBackColor = true;
            // 
            // radio_event_normal
            // 
            this.radio_event_normal.AutoSize = true;
            this.radio_event_normal.Checked = true;
            this.radio_event_normal.Location = new System.Drawing.Point(4, 3);
            this.radio_event_normal.Name = "radio_event_normal";
            this.radio_event_normal.Size = new System.Drawing.Size(71, 16);
            this.radio_event_normal.TabIndex = 7;
            this.radio_event_normal.TabStop = true;
            this.radio_event_normal.Text = "普通规则";
            this.radio_event_normal.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.combo_attach);
            this.groupBox1.Controls.Add(this.chk_attach);
            this.groupBox1.Controls.Add(this.txt_mailsize_max);
            this.groupBox1.Controls.Add(this.txt_mailsize_min);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chk_mailsize);
            this.groupBox1.Controls.Add(this.chk_subject_UpperLower);
            this.groupBox1.Controls.Add(this.txt_subject_value);
            this.groupBox1.Controls.Add(this.combo_subject_operate);
            this.groupBox1.Controls.Add(this.chk_subject);
            this.groupBox1.Controls.Add(this.chk_send_UpperLower);
            this.groupBox1.Controls.Add(this.txt_send_value);
            this.groupBox1.Controls.Add(this.combo_send_operate);
            this.groupBox1.Controls.Add(this.chk_send);
            this.groupBox1.Controls.Add(this.chk_receive_UpperLower);
            this.groupBox1.Controls.Add(this.txt_receive_value);
            this.groupBox1.Controls.Add(this.combo_receive_operate);
            this.groupBox1.Controls.Add(this.chk_receive);
            this.groupBox1.Location = new System.Drawing.Point(19, 113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 196);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "过滤条件";
            // 
            // combo_attach
            // 
            this.combo_attach.BackColor = System.Drawing.Color.White;
            this.combo_attach.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_attach.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_attach.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combo_attach.FormattingEnabled = true;
            this.combo_attach.ItemHeight = 20;
            this.combo_attach.Location = new System.Drawing.Point(97, 157);
            this.combo_attach.Name = "combo_attach";
            this.combo_attach.Size = new System.Drawing.Size(70, 26);
            this.combo_attach.TabIndex = 21;
            this.combo_attach.WaterText = "";
            // 
            // chk_attach
            // 
            this.chk_attach.AutoSize = true;
            this.chk_attach.Location = new System.Drawing.Point(6, 162);
            this.chk_attach.Name = "chk_attach";
            this.chk_attach.Size = new System.Drawing.Size(60, 16);
            this.chk_attach.TabIndex = 20;
            this.chk_attach.Tag = "1202";
            this.chk_attach.Text = "带附件";
            this.chk_attach.UseVisualStyleBackColor = true;
            // 
            // txt_mailsize_max
            // 
            this.txt_mailsize_max.BackColor = System.Drawing.Color.White;
            this.txt_mailsize_max.Location = new System.Drawing.Point(233, 125);
            this.txt_mailsize_max.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.txt_mailsize_max.Name = "txt_mailsize_max";
            this.txt_mailsize_max.Size = new System.Drawing.Size(80, 21);
            this.txt_mailsize_max.TabIndex = 19;
            // 
            // txt_mailsize_min
            // 
            this.txt_mailsize_min.BackColor = System.Drawing.Color.White;
            this.txt_mailsize_min.Location = new System.Drawing.Point(97, 125);
            this.txt_mailsize_min.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.txt_mailsize_min.Name = "txt_mailsize_min";
            this.txt_mailsize_min.Size = new System.Drawing.Size(80, 21);
            this.txt_mailsize_min.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(321, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 22);
            this.label5.TabIndex = 17;
            this.label5.Text = "KB";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(184, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 22);
            this.label4.TabIndex = 16;
            this.label4.Text = "KB   至";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chk_mailsize
            // 
            this.chk_mailsize.AutoSize = true;
            this.chk_mailsize.Location = new System.Drawing.Point(6, 129);
            this.chk_mailsize.Name = "chk_mailsize";
            this.chk_mailsize.Size = new System.Drawing.Size(72, 16);
            this.chk_mailsize.TabIndex = 12;
            this.chk_mailsize.Tag = "1300";
            this.chk_mailsize.Text = "邮件大小";
            this.chk_mailsize.UseVisualStyleBackColor = true;
            // 
            // chk_subject_UpperLower
            // 
            this.chk_subject_UpperLower.AutoSize = true;
            this.chk_subject_UpperLower.Location = new System.Drawing.Point(342, 94);
            this.chk_subject_UpperLower.Name = "chk_subject_UpperLower";
            this.chk_subject_UpperLower.Size = new System.Drawing.Size(84, 16);
            this.chk_subject_UpperLower.TabIndex = 11;
            this.chk_subject_UpperLower.Text = "区分大小写";
            this.chk_subject_UpperLower.UseVisualStyleBackColor = true;
            // 
            // txt_subject_value
            // 
            this.txt_subject_value.BackColor = System.Drawing.Color.White;
            this.txt_subject_value.DownBack = null;
            this.txt_subject_value.Icon = null;
            this.txt_subject_value.IconIsButton = false;
            this.txt_subject_value.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_subject_value.IsPasswordChat = '\0';
            this.txt_subject_value.IsSystemPasswordChar = false;
            this.txt_subject_value.Lines = new string[0];
            this.txt_subject_value.Location = new System.Drawing.Point(175, 88);
            this.txt_subject_value.Margin = new System.Windows.Forms.Padding(0);
            this.txt_subject_value.MaxLength = 32767;
            this.txt_subject_value.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_subject_value.MouseBack = null;
            this.txt_subject_value.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_subject_value.Multiline = false;
            this.txt_subject_value.Name = "txt_subject_value";
            this.txt_subject_value.NormlBack = null;
            this.txt_subject_value.Padding = new System.Windows.Forms.Padding(5);
            this.txt_subject_value.ReadOnly = false;
            this.txt_subject_value.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_subject_value.Size = new System.Drawing.Size(160, 28);
            // 
            // 
            // 
            this.txt_subject_value.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_subject_value.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_subject_value.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_subject_value.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_subject_value.SkinTxt.Name = "BaseText";
            this.txt_subject_value.SkinTxt.Size = new System.Drawing.Size(150, 14);
            this.txt_subject_value.SkinTxt.TabIndex = 0;
            this.txt_subject_value.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_subject_value.SkinTxt.WaterText = "";
            this.txt_subject_value.TabIndex = 10;
            this.txt_subject_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_subject_value.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_subject_value.WaterText = "";
            this.txt_subject_value.WordWrap = true;
            // 
            // combo_subject_operate
            // 
            this.combo_subject_operate.BackColor = System.Drawing.Color.White;
            this.combo_subject_operate.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_subject_operate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_subject_operate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combo_subject_operate.FormattingEnabled = true;
            this.combo_subject_operate.ItemHeight = 20;
            this.combo_subject_operate.Location = new System.Drawing.Point(97, 89);
            this.combo_subject_operate.Name = "combo_subject_operate";
            this.combo_subject_operate.Size = new System.Drawing.Size(70, 26);
            this.combo_subject_operate.TabIndex = 9;
            this.combo_subject_operate.WaterText = "";
            // 
            // chk_subject
            // 
            this.chk_subject.AutoSize = true;
            this.chk_subject.Location = new System.Drawing.Point(6, 94);
            this.chk_subject.Name = "chk_subject";
            this.chk_subject.Size = new System.Drawing.Size(48, 16);
            this.chk_subject.TabIndex = 8;
            this.chk_subject.Tag = "1102";
            this.chk_subject.Text = "主题";
            this.chk_subject.UseVisualStyleBackColor = true;
            // 
            // chk_send_UpperLower
            // 
            this.chk_send_UpperLower.AutoSize = true;
            this.chk_send_UpperLower.Location = new System.Drawing.Point(342, 58);
            this.chk_send_UpperLower.Name = "chk_send_UpperLower";
            this.chk_send_UpperLower.Size = new System.Drawing.Size(84, 16);
            this.chk_send_UpperLower.TabIndex = 7;
            this.chk_send_UpperLower.Text = "区分大小写";
            this.chk_send_UpperLower.UseVisualStyleBackColor = true;
            // 
            // txt_send_value
            // 
            this.txt_send_value.BackColor = System.Drawing.Color.White;
            this.txt_send_value.DownBack = null;
            this.txt_send_value.Icon = null;
            this.txt_send_value.IconIsButton = false;
            this.txt_send_value.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_send_value.IsPasswordChat = '\0';
            this.txt_send_value.IsSystemPasswordChar = false;
            this.txt_send_value.Lines = new string[0];
            this.txt_send_value.Location = new System.Drawing.Point(175, 52);
            this.txt_send_value.Margin = new System.Windows.Forms.Padding(0);
            this.txt_send_value.MaxLength = 32767;
            this.txt_send_value.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_send_value.MouseBack = null;
            this.txt_send_value.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_send_value.Multiline = false;
            this.txt_send_value.Name = "txt_send_value";
            this.txt_send_value.NormlBack = null;
            this.txt_send_value.Padding = new System.Windows.Forms.Padding(5);
            this.txt_send_value.ReadOnly = false;
            this.txt_send_value.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_send_value.Size = new System.Drawing.Size(160, 28);
            // 
            // 
            // 
            this.txt_send_value.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_send_value.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_send_value.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_send_value.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_send_value.SkinTxt.Name = "BaseText";
            this.txt_send_value.SkinTxt.Size = new System.Drawing.Size(150, 14);
            this.txt_send_value.SkinTxt.TabIndex = 0;
            this.txt_send_value.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_send_value.SkinTxt.WaterText = "";
            this.txt_send_value.TabIndex = 6;
            this.txt_send_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_send_value.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_send_value.WaterText = "";
            this.txt_send_value.WordWrap = true;
            // 
            // combo_send_operate
            // 
            this.combo_send_operate.BackColor = System.Drawing.Color.White;
            this.combo_send_operate.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_send_operate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_send_operate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combo_send_operate.FormattingEnabled = true;
            this.combo_send_operate.ItemHeight = 20;
            this.combo_send_operate.Location = new System.Drawing.Point(97, 53);
            this.combo_send_operate.Name = "combo_send_operate";
            this.combo_send_operate.Size = new System.Drawing.Size(70, 26);
            this.combo_send_operate.TabIndex = 5;
            this.combo_send_operate.WaterText = "";
            // 
            // chk_send
            // 
            this.chk_send.AutoSize = true;
            this.chk_send.Location = new System.Drawing.Point(6, 58);
            this.chk_send.Name = "chk_send";
            this.chk_send.Size = new System.Drawing.Size(84, 16);
            this.chk_send.TabIndex = 4;
            this.chk_send.Tag = "1100";
            this.chk_send.Text = "发件人地址";
            this.chk_send.UseVisualStyleBackColor = true;
            // 
            // chk_receive_UpperLower
            // 
            this.chk_receive_UpperLower.AutoSize = true;
            this.chk_receive_UpperLower.Location = new System.Drawing.Point(342, 24);
            this.chk_receive_UpperLower.Name = "chk_receive_UpperLower";
            this.chk_receive_UpperLower.Size = new System.Drawing.Size(84, 16);
            this.chk_receive_UpperLower.TabIndex = 3;
            this.chk_receive_UpperLower.Text = "区分大小写";
            this.chk_receive_UpperLower.UseVisualStyleBackColor = true;
            // 
            // txt_receive_value
            // 
            this.txt_receive_value.BackColor = System.Drawing.Color.White;
            this.txt_receive_value.DownBack = null;
            this.txt_receive_value.Icon = null;
            this.txt_receive_value.IconIsButton = false;
            this.txt_receive_value.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_receive_value.IsPasswordChat = '\0';
            this.txt_receive_value.IsSystemPasswordChar = false;
            this.txt_receive_value.Lines = new string[0];
            this.txt_receive_value.Location = new System.Drawing.Point(175, 18);
            this.txt_receive_value.Margin = new System.Windows.Forms.Padding(0);
            this.txt_receive_value.MaxLength = 32767;
            this.txt_receive_value.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_receive_value.MouseBack = null;
            this.txt_receive_value.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_receive_value.Multiline = false;
            this.txt_receive_value.Name = "txt_receive_value";
            this.txt_receive_value.NormlBack = null;
            this.txt_receive_value.Padding = new System.Windows.Forms.Padding(5);
            this.txt_receive_value.ReadOnly = false;
            this.txt_receive_value.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_receive_value.Size = new System.Drawing.Size(160, 28);
            // 
            // 
            // 
            this.txt_receive_value.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_receive_value.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_receive_value.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_receive_value.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_receive_value.SkinTxt.Name = "BaseText";
            this.txt_receive_value.SkinTxt.Size = new System.Drawing.Size(150, 14);
            this.txt_receive_value.SkinTxt.TabIndex = 0;
            this.txt_receive_value.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_receive_value.SkinTxt.WaterText = "";
            this.txt_receive_value.TabIndex = 2;
            this.txt_receive_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_receive_value.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_receive_value.WaterText = "";
            this.txt_receive_value.WordWrap = true;
            // 
            // combo_receive_operate
            // 
            this.combo_receive_operate.BackColor = System.Drawing.Color.White;
            this.combo_receive_operate.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_receive_operate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_receive_operate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combo_receive_operate.FormattingEnabled = true;
            this.combo_receive_operate.ItemHeight = 20;
            this.combo_receive_operate.Location = new System.Drawing.Point(97, 19);
            this.combo_receive_operate.Name = "combo_receive_operate";
            this.combo_receive_operate.Size = new System.Drawing.Size(70, 26);
            this.combo_receive_operate.TabIndex = 1;
            this.combo_receive_operate.WaterText = "";
            // 
            // chk_receive
            // 
            this.chk_receive.AutoSize = true;
            this.chk_receive.Location = new System.Drawing.Point(6, 24);
            this.chk_receive.Name = "chk_receive";
            this.chk_receive.Size = new System.Drawing.Size(84, 16);
            this.chk_receive.TabIndex = 0;
            this.chk_receive.Tag = "1101";
            this.chk_receive.Text = "收件人地址";
            this.chk_receive.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radio_condition_any);
            this.panel1.Controls.Add(this.radio_condtion_all);
            this.panel1.Location = new System.Drawing.Point(80, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(376, 23);
            this.panel1.TabIndex = 8;
            // 
            // radio_condition_any
            // 
            this.radio_condition_any.AutoSize = true;
            this.radio_condition_any.Location = new System.Drawing.Point(171, 2);
            this.radio_condition_any.Name = "radio_condition_any";
            this.radio_condition_any.Size = new System.Drawing.Size(119, 16);
            this.radio_condition_any.TabIndex = 8;
            this.radio_condition_any.Text = "满足以下任一条件";
            this.radio_condition_any.UseVisualStyleBackColor = true;
            // 
            // radio_condtion_all
            // 
            this.radio_condtion_all.AutoSize = true;
            this.radio_condtion_all.Checked = true;
            this.radio_condtion_all.Location = new System.Drawing.Point(4, 3);
            this.radio_condtion_all.Name = "radio_condtion_all";
            this.radio_condtion_all.Size = new System.Drawing.Size(119, 16);
            this.radio_condtion_all.TabIndex = 7;
            this.radio_condtion_all.TabStop = true;
            this.radio_condtion_all.Text = "满足以下所有条件";
            this.radio_condtion_all.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "执行条件";
            // 
            // chk_FilterDoTime_send
            // 
            this.chk_FilterDoTime_send.AutoSize = true;
            this.chk_FilterDoTime_send.Checked = true;
            this.chk_FilterDoTime_send.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_FilterDoTime_send.Location = new System.Drawing.Point(251, 52);
            this.chk_FilterDoTime_send.Name = "chk_FilterDoTime_send";
            this.chk_FilterDoTime_send.Size = new System.Drawing.Size(84, 16);
            this.chk_FilterDoTime_send.TabIndex = 5;
            this.chk_FilterDoTime_send.Text = "发送邮件时";
            this.chk_FilterDoTime_send.UseVisualStyleBackColor = true;
            // 
            // chk_FilterDoTime_receive
            // 
            this.chk_FilterDoTime_receive.AutoSize = true;
            this.chk_FilterDoTime_receive.Checked = true;
            this.chk_FilterDoTime_receive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_FilterDoTime_receive.Location = new System.Drawing.Point(84, 53);
            this.chk_FilterDoTime_receive.Name = "chk_FilterDoTime_receive";
            this.chk_FilterDoTime_receive.Size = new System.Drawing.Size(84, 16);
            this.chk_FilterDoTime_receive.TabIndex = 4;
            this.chk_FilterDoTime_receive.Text = "收取邮件时";
            this.chk_FilterDoTime_receive.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "执行时机";
            // 
            // chk_IsFilter
            // 
            this.chk_IsFilter.Location = new System.Drawing.Point(393, 15);
            this.chk_IsFilter.Name = "chk_IsFilter";
            this.chk_IsFilter.Size = new System.Drawing.Size(57, 24);
            this.chk_IsFilter.TabIndex = 2;
            this.chk_IsFilter.Text = "启用";
            this.chk_IsFilter.UseVisualStyleBackColor = true;
            // 
            // txt_FilterName
            // 
            this.txt_FilterName.BackColor = System.Drawing.Color.White;
            this.txt_FilterName.DownBack = null;
            this.txt_FilterName.Icon = null;
            this.txt_FilterName.IconIsButton = false;
            this.txt_FilterName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_FilterName.IsPasswordChat = '\0';
            this.txt_FilterName.IsSystemPasswordChar = false;
            this.txt_FilterName.Lines = new string[0];
            this.txt_FilterName.Location = new System.Drawing.Point(83, 14);
            this.txt_FilterName.Margin = new System.Windows.Forms.Padding(0);
            this.txt_FilterName.MaxLength = 32767;
            this.txt_FilterName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txt_FilterName.MouseBack = null;
            this.txt_FilterName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txt_FilterName.Multiline = false;
            this.txt_FilterName.Name = "txt_FilterName";
            this.txt_FilterName.NormlBack = null;
            this.txt_FilterName.Padding = new System.Windows.Forms.Padding(5);
            this.txt_FilterName.ReadOnly = false;
            this.txt_FilterName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_FilterName.Size = new System.Drawing.Size(276, 28);
            // 
            // 
            // 
            this.txt_FilterName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_FilterName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_FilterName.SkinTxt.Font = new System.Drawing.Font("Arial", 9F);
            this.txt_FilterName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txt_FilterName.SkinTxt.Name = "BaseText";
            this.txt_FilterName.SkinTxt.Size = new System.Drawing.Size(266, 14);
            this.txt_FilterName.SkinTxt.TabIndex = 0;
            this.txt_FilterName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_FilterName.SkinTxt.WaterText = "";
            this.txt_FilterName.TabIndex = 1;
            this.txt_FilterName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_FilterName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txt_FilterName.WaterText = "";
            this.txt_FilterName.WordWrap = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "规则名称";
            // 
            // panel_left
            // 
            this.panel_left.Controls.Add(this.dgrid_filter);
            this.panel_left.Controls.Add(this.toolStrip1);
            this.panel_left.Controls.Add(this.panel_left_top);
            this.panel_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_left.Location = new System.Drawing.Point(0, 0);
            this.panel_left.Name = "panel_left";
            this.panel_left.Size = new System.Drawing.Size(200, 600);
            this.panel_left.TabIndex = 1;
            // 
            // dgrid_filter
            // 
            this.dgrid_filter.AllowUserToAddRows = false;
            this.dgrid_filter.AllowUserToDeleteRows = false;
            this.dgrid_filter.AllowUserToResizeRows = false;
            this.dgrid_filter.AlternatingCellBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            this.dgrid_filter.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgrid_filter.BackgroundColor = System.Drawing.Color.White;
            this.dgrid_filter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgrid_filter.ColumnFont = null;
            this.dgrid_filter.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrid_filter.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgrid_filter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrid_filter.ColumnHeadersVisible = false;
            this.dgrid_filter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FilterName,
            this.IsFilter,
            this.FilterConditionId});
            this.dgrid_filter.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgrid_filter.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgrid_filter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrid_filter.EnableHeadersVisualStyles = false;
            this.dgrid_filter.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgrid_filter.HeadFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgrid_filter.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgrid_filter.Location = new System.Drawing.Point(0, 35);
            this.dgrid_filter.MultiSelect = false;
            this.dgrid_filter.Name = "dgrid_filter";
            this.dgrid_filter.ReadOnly = true;
            this.dgrid_filter.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgrid_filter.RowHeadersVisible = false;
            this.dgrid_filter.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgrid_filter.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgrid_filter.RowTemplate.Height = 28;
            this.dgrid_filter.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgrid_filter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrid_filter.Size = new System.Drawing.Size(200, 531);
            this.dgrid_filter.TabIndex = 6;
            this.dgrid_filter.TabStop = false;
            this.dgrid_filter.TitleBack = null;
            this.dgrid_filter.TitleBackColorBegin = System.Drawing.Color.White;
            this.dgrid_filter.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // FilterName
            // 
            this.FilterName.DataPropertyName = "FilterName";
            this.FilterName.HeaderText = "标签名称";
            this.FilterName.Name = "FilterName";
            this.FilterName.ReadOnly = true;
            this.FilterName.Width = 160;
            // 
            // IsFilter
            // 
            this.IsFilter.DataPropertyName = "IsFilter";
            this.IsFilter.HeaderText = "启用";
            this.IsFilter.Name = "IsFilter";
            this.IsFilter.ReadOnly = true;
            this.IsFilter.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsFilter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsFilter.Width = 40;
            // 
            // FilterConditionId
            // 
            this.FilterConditionId.DataPropertyName = "FilterConditionId";
            this.FilterConditionId.HeaderText = "标签主键ID";
            this.FilterConditionId.Name = "FilterConditionId";
            this.FilterConditionId.ReadOnly = true;
            this.FilterConditionId.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Arrow = System.Drawing.Color.Black;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Back = System.Drawing.Color.White;
            this.toolStrip1.BackRadius = 4;
            this.toolStrip1.BackRectangle = new System.Drawing.Rectangle(10, 10, 10, 10);
            this.toolStrip1.Base = System.Drawing.Color.White;
            this.toolStrip1.BaseFore = System.Drawing.Color.Black;
            this.toolStrip1.BaseForeAnamorphosis = false;
            this.toolStrip1.BaseForeAnamorphosisBorder = 4;
            this.toolStrip1.BaseForeAnamorphosisColor = System.Drawing.Color.Black;
            this.toolStrip1.BaseForeOffset = new System.Drawing.Point(0, 0);
            this.toolStrip1.BaseHoverFore = System.Drawing.Color.Black;
            this.toolStrip1.BaseItemAnamorphosis = false;
            this.toolStrip1.BaseItemBorder = System.Drawing.Color.Black;
            this.toolStrip1.BaseItemBorderShow = true;
            this.toolStrip1.BaseItemDown = null;
            this.toolStrip1.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolStrip1.BaseItemMouse = null;
            this.toolStrip1.BaseItemNorml = null;
            this.toolStrip1.BaseItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolStrip1.BaseItemRadius = 4;
            this.toolStrip1.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.None;
            this.toolStrip1.BaseItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolStrip1.BindTabControl = null;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.toolStrip1.Font = new System.Drawing.Font("Arial", 9F);
            this.toolStrip1.Fore = System.Drawing.Color.Black;
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.HoverFore = System.Drawing.Color.Black;
            this.toolStrip1.ItemAnamorphosis = false;
            this.toolStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolStrip1.ItemBorderShow = true;
            this.toolStrip1.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolStrip1.ItemRadius = 4;
            this.toolStrip1.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtn_add,
            this.toolBtn_delete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 566);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.toolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.None;
            this.toolStrip1.Size = new System.Drawing.Size(200, 34);
            this.toolStrip1.SkinAllColor = true;
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "工具栏";
            this.toolStrip1.TitleAnamorphosis = true;
            this.toolStrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(189)))), ((int)(((byte)(210)))));
            this.toolStrip1.TitleRadius = 4;
            this.toolStrip1.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.None;
            // 
            // toolBtn_add
            // 
            this.toolBtn_add.Image = global::YANAN.Mail.Client.Properties.Resources.mail_add;
            this.toolBtn_add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_add.Name = "toolBtn_add";
            this.toolBtn_add.Size = new System.Drawing.Size(51, 31);
            this.toolBtn_add.Text = "新增";
            // 
            // toolBtn_delete
            // 
            this.toolBtn_delete.Image = global::YANAN.Mail.Client.Properties.Resources.mail_delete;
            this.toolBtn_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_delete.Name = "toolBtn_delete";
            this.toolBtn_delete.Size = new System.Drawing.Size(51, 31);
            this.toolBtn_delete.Text = "删除";
            // 
            // panel_left_top
            // 
            this.panel_left_top.Controls.Add(this.combo_mailbox);
            this.panel_left_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_left_top.Location = new System.Drawing.Point(0, 0);
            this.panel_left_top.Name = "panel_left_top";
            this.panel_left_top.Size = new System.Drawing.Size(200, 35);
            this.panel_left_top.TabIndex = 0;
            // 
            // combo_mailbox
            // 
            this.combo_mailbox.BackColor = System.Drawing.Color.White;
            this.combo_mailbox.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_mailbox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.combo_mailbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combo_mailbox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combo_mailbox.DropDownHeight = 120;
            this.combo_mailbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_mailbox.FormattingEnabled = true;
            this.combo_mailbox.IntegralHeight = false;
            this.combo_mailbox.ItemHeight = 28;
            this.combo_mailbox.Location = new System.Drawing.Point(0, 0);
            this.combo_mailbox.Margin = new System.Windows.Forms.Padding(2);
            this.combo_mailbox.Name = "combo_mailbox";
            this.combo_mailbox.Size = new System.Drawing.Size(200, 34);
            this.combo_mailbox.TabIndex = 0;
            this.combo_mailbox.WaterText = "";
            // 
            // MailFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 600);
            this.Controls.Add(this.panel_main);
            this.Name = "MailFilterForm";
            this.Text = "过滤器";
            this.panel_main.ResumeLayout(false);
            this.panel_right.ResumeLayout(false);
            this.panel_right.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_mailsize_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_mailsize_min)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_left.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrid_filter)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel_left_top.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_main;
        private CCWin.SkinControl.SkinToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolBtn_delete;
        private System.Windows.Forms.Panel panel_left;
        private System.Windows.Forms.Panel panel_left_top;
        private CCWin.SkinControl.SkinComboBox combo_mailbox;
        private System.Windows.Forms.Panel panel_right;
        private CCWin.SkinControl.SkinTextBox txt_FilterName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_IsFilter;
        private System.Windows.Forms.CheckBox chk_FilterDoTime_send;
        private System.Windows.Forms.CheckBox chk_FilterDoTime_receive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radio_condition_any;
        private System.Windows.Forms.RadioButton radio_condtion_all;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chk_receive;
        private CCWin.SkinControl.SkinComboBox combo_receive_operate;
        private CCWin.SkinControl.SkinTextBox txt_receive_value;
        private System.Windows.Forms.CheckBox chk_receive_UpperLower;
        private System.Windows.Forms.CheckBox chk_send_UpperLower;
        private CCWin.SkinControl.SkinTextBox txt_send_value;
        private CCWin.SkinControl.SkinComboBox combo_send_operate;
        private System.Windows.Forms.CheckBox chk_send;
        private System.Windows.Forms.CheckBox chk_subject_UpperLower;
        private CCWin.SkinControl.SkinTextBox txt_subject_value;
        private CCWin.SkinControl.SkinComboBox combo_subject_operate;
        private System.Windows.Forms.CheckBox chk_subject;
        private System.Windows.Forms.CheckBox chk_mailsize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txt_mailsize_min;
        private System.Windows.Forms.NumericUpDown txt_mailsize_max;
        private CCWin.SkinControl.SkinComboBox combo_attach;
        private System.Windows.Forms.CheckBox chk_attach;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radio_event_delete;
        private System.Windows.Forms.RadioButton radio_event_normal;
        private CCWin.SkinControl.SkinComboBox combo_move_folder;
        private System.Windows.Forms.CheckBox chk_move_folder;
        private System.Windows.Forms.CheckBox chk_set_label;
        private System.Windows.Forms.CheckBox chk_stop_other;
        private System.Windows.Forms.CheckBox chk_set_mail_read;
        private CCWin.SkinControl.SkinButton btn_save;
        private System.Windows.Forms.ToolStripButton toolBtn_add;
        private CCWin.SkinControl.SkinComboBox combo_mail_label;
        private CCWin.SkinControl.SkinDataGridView dgrid_filter;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilterName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilterConditionId;
    }
}