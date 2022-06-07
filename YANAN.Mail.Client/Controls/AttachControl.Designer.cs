namespace YANAN.Mail.Client.Controls
{
    partial class AttachControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.skinPictureBox1 = new CCWin.SkinControl.SkinPictureBox();
            this.lbl_filename = new System.Windows.Forms.Label();
            this.lbl_filesize = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.skinPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // skinPictureBox1
            // 
            this.skinPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.skinPictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.skinPictureBox1.Location = new System.Drawing.Point(0, 0);
            this.skinPictureBox1.Name = "skinPictureBox1";
            this.skinPictureBox1.Padding = new System.Windows.Forms.Padding(3);
            this.skinPictureBox1.Size = new System.Drawing.Size(25, 25);
            this.skinPictureBox1.TabIndex = 0;
            this.skinPictureBox1.TabStop = false;
            // 
            // lbl_filename
            // 
            this.lbl_filename.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_filename.Location = new System.Drawing.Point(25, 0);
            this.lbl_filename.Name = "lbl_filename";
            this.lbl_filename.Padding = new System.Windows.Forms.Padding(3);
            this.lbl_filename.Size = new System.Drawing.Size(215, 25);
            this.lbl_filename.TabIndex = 1;
            this.lbl_filename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_filename.MouseLeave += new System.EventHandler(this.lbl_filename_MouseLeave);
            this.lbl_filename.MouseHover += new System.EventHandler(this.lbl_filename_MouseHover);
            // 
            // lbl_filesize
            // 
            this.lbl_filesize.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_filesize.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_filesize.Location = new System.Drawing.Point(185, 0);
            this.lbl_filesize.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_filesize.Name = "lbl_filesize";
            this.lbl_filesize.Size = new System.Drawing.Size(55, 25);
            this.lbl_filesize.TabIndex = 2;
            this.lbl_filesize.Text = "0字节";
            this.lbl_filesize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_filesize.MouseLeave += new System.EventHandler(this.lbl_filesize_MouseLeave);
            this.lbl_filesize.MouseHover += new System.EventHandler(this.lbl_filesize_MouseHover);
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.White;
            // 
            // AttachControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbl_filesize);
            this.Controls.Add(this.lbl_filename);
            this.Controls.Add(this.skinPictureBox1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AttachControl";
            this.Size = new System.Drawing.Size(240, 25);
            ((System.ComponentModel.ISupportInitialize)(this.skinPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinPictureBox skinPictureBox1;
        private System.Windows.Forms.Label lbl_filename;
        private System.Windows.Forms.Label lbl_filesize;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
