namespace YANAN.Mail.Client
{
    partial class MailViewDetailForm
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
            this.panel_top = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picBox_more = new System.Windows.Forms.PictureBox();
            this.lbl_subject = new System.Windows.Forms.Label();
            this.panel_center = new System.Windows.Forms.Panel();
            this.panel_body = new System.Windows.Forms.Panel();
            this.html_mailbody = new WinHtmlEditor.HtmlEditor();
            this.panel_head = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_sender = new System.Windows.Forms.Label();
            this.lbl_receiver = new System.Windows.Forms.Label();
            this.lbl_cc = new System.Windows.Forms.Label();
            this.lbl_bcc = new System.Windows.Forms.Label();
            this.lbl_mailtime = new System.Windows.Forms.Label();
            this.panel_top.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_more)).BeginInit();
            this.panel_center.SuspendLayout();
            this.panel_body.SuspendLayout();
            this.panel_head.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_top
            // 
            this.panel_top.Controls.Add(this.panel1);
            this.panel_top.Controls.Add(this.lbl_subject);
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.Location = new System.Drawing.Point(0, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(800, 38);
            this.panel_top.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.picBox_more);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(752, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(48, 38);
            this.panel1.TabIndex = 1;
            // 
            // picBox_more
            // 
            this.picBox_more.Image = global::YANAN.Mail.Client.Properties.Resources.mail_more;
            this.picBox_more.Location = new System.Drawing.Point(28, 12);
            this.picBox_more.Name = "picBox_more";
            this.picBox_more.Size = new System.Drawing.Size(16, 16);
            this.picBox_more.TabIndex = 0;
            this.picBox_more.TabStop = false;
            // 
            // lbl_subject
            // 
            this.lbl_subject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_subject.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_subject.Location = new System.Drawing.Point(0, 0);
            this.lbl_subject.Name = "lbl_subject";
            this.lbl_subject.Size = new System.Drawing.Size(800, 38);
            this.lbl_subject.TabIndex = 0;
            this.lbl_subject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_center
            // 
            this.panel_center.Controls.Add(this.panel_body);
            this.panel_center.Controls.Add(this.panel_head);
            this.panel_center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_center.Location = new System.Drawing.Point(0, 38);
            this.panel_center.Name = "panel_center";
            this.panel_center.Size = new System.Drawing.Size(800, 562);
            this.panel_center.TabIndex = 1;
            // 
            // panel_body
            // 
            this.panel_body.Controls.Add(this.html_mailbody);
            this.panel_body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_body.Location = new System.Drawing.Point(0, 140);
            this.panel_body.Name = "panel_body";
            this.panel_body.Size = new System.Drawing.Size(800, 422);
            this.panel_body.TabIndex = 1;
            // 
            // html_mailbody
            // 
            this.html_mailbody.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.html_mailbody.BodyFont = new WinHtmlEditor.HtmlFontProperty("Arial", WinHtmlEditor.HtmlFontSize.Small, false, false, false, false, false, false);
            this.html_mailbody.BodyInnerHTML = null;
            this.html_mailbody.BodyInnerText = null;
            this.html_mailbody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.html_mailbody.EnterToBR = false;
            this.html_mailbody.FontName = null;
            this.html_mailbody.FontSize = WinHtmlEditor.FontSize.NA;
            this.html_mailbody.Location = new System.Drawing.Point(0, 0);
            this.html_mailbody.Name = "html_mailbody";
            this.html_mailbody.ReadOnly = true;
            this.html_mailbody.ShowStatusBar = false;
            this.html_mailbody.ShowToolBar = false;
            this.html_mailbody.ShowWb = true;
            this.html_mailbody.Size = new System.Drawing.Size(800, 422);
            this.html_mailbody.TabIndex = 0;
            this.html_mailbody.TabStop = false;
            this.html_mailbody.WebBrowserShortcutsEnabled = false;
            // 
            // panel_head
            // 
            this.panel_head.Controls.Add(this.tableLayoutPanel1);
            this.panel_head.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_head.Location = new System.Drawing.Point(0, 0);
            this.panel_head.Name = "panel_head";
            this.panel_head.Size = new System.Drawing.Size(800, 140);
            this.panel_head.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(246)))), ((int)(((byte)(245)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbl_sender, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_receiver, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_cc, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl_bcc, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbl_mailtime, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 140);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "发件人";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(3, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "收件人";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(3, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 28);
            this.label3.TabIndex = 2;
            this.label3.Text = "抄送";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 28);
            this.label4.TabIndex = 3;
            this.label4.Text = "密送";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 28);
            this.label5.TabIndex = 4;
            this.label5.Text = "时间";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_sender
            // 
            this.lbl_sender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_sender.Location = new System.Drawing.Point(53, 0);
            this.lbl_sender.Name = "lbl_sender";
            this.lbl_sender.Size = new System.Drawing.Size(744, 28);
            this.lbl_sender.TabIndex = 5;
            this.lbl_sender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_receiver
            // 
            this.lbl_receiver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_receiver.Location = new System.Drawing.Point(53, 28);
            this.lbl_receiver.Name = "lbl_receiver";
            this.lbl_receiver.Size = new System.Drawing.Size(744, 28);
            this.lbl_receiver.TabIndex = 6;
            this.lbl_receiver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_cc
            // 
            this.lbl_cc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_cc.Location = new System.Drawing.Point(53, 56);
            this.lbl_cc.Name = "lbl_cc";
            this.lbl_cc.Size = new System.Drawing.Size(744, 28);
            this.lbl_cc.TabIndex = 7;
            this.lbl_cc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_bcc
            // 
            this.lbl_bcc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_bcc.Location = new System.Drawing.Point(53, 84);
            this.lbl_bcc.Name = "lbl_bcc";
            this.lbl_bcc.Size = new System.Drawing.Size(744, 28);
            this.lbl_bcc.TabIndex = 8;
            this.lbl_bcc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_mailtime
            // 
            this.lbl_mailtime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_mailtime.Location = new System.Drawing.Point(53, 112);
            this.lbl_mailtime.Name = "lbl_mailtime";
            this.lbl_mailtime.Size = new System.Drawing.Size(744, 28);
            this.lbl_mailtime.TabIndex = 9;
            this.lbl_mailtime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MailViewDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.panel_center);
            this.Controls.Add(this.panel_top);
            this.Name = "MailViewDetail";
            this.Text = "邮件查看窗体";
            this.Load += new System.EventHandler(this.MailViewDetail_Load);
            this.panel_top.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_more)).EndInit();
            this.panel_center.ResumeLayout(false);
            this.panel_body.ResumeLayout(false);
            this.panel_head.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_top;
        private System.Windows.Forms.Panel panel_center;
        private System.Windows.Forms.Panel panel_body;
        private System.Windows.Forms.Panel panel_head;
        private WinHtmlEditor.HtmlEditor html_mailbody;
        private System.Windows.Forms.Label lbl_subject;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picBox_more;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_sender;
        private System.Windows.Forms.Label lbl_receiver;
        private System.Windows.Forms.Label lbl_cc;
        private System.Windows.Forms.Label lbl_bcc;
        private System.Windows.Forms.Label lbl_mailtime;
    }
}