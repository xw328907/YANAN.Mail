namespace YANAN.Mail.Client
{
    partial class MailListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgrid_maillist = new CCWin.SkinControl.SkinDataGridView();
            this.Viewed = new System.Windows.Forms.DataGridViewImageColumn();
            this.Importance = new System.Windows.Forms.DataGridViewImageColumn();
            this.AttachCount = new System.Windows.Forms.DataGridViewImageColumn();
            this.SendName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MailTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MailSizeString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsTop = new System.Windows.Forms.DataGridViewImageColumn();
            this.MailMainId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MailBoxId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrid_maillist)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgrid_maillist);
            this.splitContainer1.Size = new System.Drawing.Size(1140, 730);
            this.splitContainer1.SplitterDistance = 240;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgrid_maillist
            // 
            this.dgrid_maillist.AllowUserToAddRows = false;
            this.dgrid_maillist.AllowUserToDeleteRows = false;
            this.dgrid_maillist.AllowUserToResizeRows = false;
            this.dgrid_maillist.AlternatingCellBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgrid_maillist.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgrid_maillist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrid_maillist.BackgroundColor = System.Drawing.Color.White;
            this.dgrid_maillist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgrid_maillist.ColumnFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgrid_maillist.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrid_maillist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgrid_maillist.ColumnHeadersHeight = 30;
            this.dgrid_maillist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgrid_maillist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Viewed,
            this.Importance,
            this.AttachCount,
            this.SendName,
            this.Subject,
            this.MailTime,
            this.MailSizeString,
            this.IsTop,
            this.MailMainId,
            this.MailBoxId});
            this.dgrid_maillist.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgrid_maillist.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgrid_maillist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrid_maillist.EnableHeadersVisualStyles = false;
            this.dgrid_maillist.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgrid_maillist.HeadFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgrid_maillist.HeadForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.dgrid_maillist.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgrid_maillist.Location = new System.Drawing.Point(0, 0);
            this.dgrid_maillist.MultiSelect = false;
            this.dgrid_maillist.Name = "dgrid_maillist";
            this.dgrid_maillist.ReadOnly = true;
            this.dgrid_maillist.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgrid_maillist.RowHeadersVisible = false;
            this.dgrid_maillist.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgrid_maillist.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgrid_maillist.RowTemplate.Height = 23;
            this.dgrid_maillist.RowTemplate.ReadOnly = true;
            this.dgrid_maillist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrid_maillist.Size = new System.Drawing.Size(1140, 240);
            this.dgrid_maillist.TabIndex = 1;
            this.dgrid_maillist.TitleBack = null;
            this.dgrid_maillist.TitleBackColorBegin = System.Drawing.Color.White;
            this.dgrid_maillist.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            // 
            // Viewed
            // 
            this.Viewed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Viewed.DataPropertyName = "Viewed";
            this.Viewed.FillWeight = 0.14F;
            this.Viewed.HeaderText = "已读";
            this.Viewed.MinimumWidth = 20;
            this.Viewed.Name = "Viewed";
            this.Viewed.ReadOnly = true;
            this.Viewed.ToolTipText = "是否已读";
            this.Viewed.Width = 40;
            // 
            // Importance
            // 
            this.Importance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Importance.DataPropertyName = "Importance";
            this.Importance.FillWeight = 0.14F;
            this.Importance.HeaderText = "重要";
            this.Importance.Image = global::YANAN.Mail.Client.Properties.Resources.mail_importance;
            this.Importance.MinimumWidth = 20;
            this.Importance.Name = "Importance";
            this.Importance.ReadOnly = true;
            this.Importance.ToolTipText = "重要程度";
            this.Importance.Width = 40;
            // 
            // AttachCount
            // 
            this.AttachCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AttachCount.DataPropertyName = "AttachCount";
            this.AttachCount.FillWeight = 0.14F;
            this.AttachCount.HeaderText = "附件";
            this.AttachCount.Image = global::YANAN.Mail.Client.Properties.Resources.mail_attach;
            this.AttachCount.MinimumWidth = 20;
            this.AttachCount.Name = "AttachCount";
            this.AttachCount.ReadOnly = true;
            this.AttachCount.ToolTipText = "附件";
            this.AttachCount.Width = 40;
            // 
            // SendName
            // 
            this.SendName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SendName.DataPropertyName = "SendName";
            this.SendName.FillWeight = 14.99F;
            this.SendName.HeaderText = "发件人";
            this.SendName.MinimumWidth = 100;
            this.SendName.Name = "SendName";
            this.SendName.ReadOnly = true;
            this.SendName.Width = 220;
            // 
            // Subject
            // 
            this.Subject.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Subject.DataPropertyName = "Subject";
            this.Subject.FillWeight = 51.026F;
            this.Subject.HeaderText = "主题";
            this.Subject.MinimumWidth = 160;
            this.Subject.Name = "Subject";
            this.Subject.ReadOnly = true;
            // 
            // MailTime
            // 
            this.MailTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MailTime.DataPropertyName = "MailTime";
            this.MailTime.FillWeight = 14.82F;
            this.MailTime.HeaderText = "日期";
            this.MailTime.MinimumWidth = 80;
            this.MailTime.Name = "MailTime";
            this.MailTime.ReadOnly = true;
            // 
            // MailSizeString
            // 
            this.MailSizeString.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MailSizeString.DataPropertyName = "MailSizeString";
            this.MailSizeString.FillWeight = 12.65F;
            this.MailSizeString.HeaderText = "大小";
            this.MailSizeString.MinimumWidth = 50;
            this.MailSizeString.Name = "MailSizeString";
            this.MailSizeString.ReadOnly = true;
            this.MailSizeString.Width = 150;
            // 
            // IsTop
            // 
            this.IsTop.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IsTop.DataPropertyName = "IsTop";
            this.IsTop.FillWeight = 0.14F;
            this.IsTop.HeaderText = "置顶";
            this.IsTop.Image = global::YANAN.Mail.Client.Properties.Resources.mail_set_top;
            this.IsTop.MinimumWidth = 20;
            this.IsTop.Name = "IsTop";
            this.IsTop.ReadOnly = true;
            this.IsTop.ToolTipText = "置顶";
            this.IsTop.Width = 40;
            // 
            // MailMainId
            // 
            this.MailMainId.DataPropertyName = "MailMainId";
            this.MailMainId.HeaderText = "邮件ID";
            this.MailMainId.Name = "MailMainId";
            this.MailMainId.ReadOnly = true;
            this.MailMainId.Visible = false;
            // 
            // MailBoxId
            // 
            this.MailBoxId.HeaderText = "MailBoxId";
            this.MailBoxId.Name = "MailBoxId";
            this.MailBoxId.ReadOnly = true;
            this.MailBoxId.Visible = false;
            // 
            // MailListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 730);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MailListForm";
            this.Text = "MailListForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrid_maillist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private CCWin.SkinControl.SkinDataGridView dgrid_maillist;
        private System.Windows.Forms.DataGridViewImageColumn Viewed;
        private System.Windows.Forms.DataGridViewImageColumn Importance;
        private System.Windows.Forms.DataGridViewImageColumn AttachCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn SendName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subject;
        private System.Windows.Forms.DataGridViewTextBoxColumn MailTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn MailSizeString;
        private System.Windows.Forms.DataGridViewImageColumn IsTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn MailMainId;
        private System.Windows.Forms.DataGridViewTextBoxColumn MailBoxId;
    }
}