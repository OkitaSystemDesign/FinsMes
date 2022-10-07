namespace FinsMes
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSrcIP = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTargetIP = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCreateSendmes = new System.Windows.Forms.Button();
            this.cmbCmd1 = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtFinsSrcAdr = new System.Windows.Forms.TextBox();
            this.txtFinsTargetAdr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbMemType = new System.Windows.Forms.ComboBox();
            this.txtMemAddress = new System.Windows.Forms.TextBox();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "送信元";
            // 
            // cmbSrcIP
            // 
            this.cmbSrcIP.FormattingEnabled = true;
            this.cmbSrcIP.Location = new System.Drawing.Point(84, 6);
            this.cmbSrcIP.Name = "cmbSrcIP";
            this.cmbSrcIP.Size = new System.Drawing.Size(121, 20);
            this.cmbSrcIP.TabIndex = 1;
            this.cmbSrcIP.Click += new System.EventHandler(this.cmbSrcIP_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "送信先";
            // 
            // txtTargetIP
            // 
            this.txtTargetIP.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTargetIP.Location = new System.Drawing.Point(306, 6);
            this.txtTargetIP.Name = "txtTargetIP";
            this.txtTargetIP.Size = new System.Drawing.Size(100, 19);
            this.txtTargetIP.TabIndex = 3;
            this.txtTargetIP.Text = "192.168.250.1";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(451, 4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 71);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1.Controls.Add(this.btnCreateSendmes);
            this.splitContainer1.Panel1.Controls.Add(this.cmbCmd1);
            this.splitContainer1.Panel1.Controls.Add(this.label15);
            this.splitContainer1.Size = new System.Drawing.Size(636, 367);
            this.splitContainer1.SplitterDistance = 238;
            this.splitContainer1.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(24, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(568, 118);
            this.tabControl1.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.txtSize);
            this.tabPage1.Controls.Add(this.txtMemAddress);
            this.tabPage1.Controls.Add(this.cmbMemType);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(560, 92);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(560, 92);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCreateSendmes
            // 
            this.btnCreateSendmes.Location = new System.Drawing.Point(132, 153);
            this.btnCreateSendmes.Name = "btnCreateSendmes";
            this.btnCreateSendmes.Size = new System.Drawing.Size(195, 36);
            this.btnCreateSendmes.TabIndex = 24;
            this.btnCreateSendmes.Text = "Convert to Send Data";
            this.btnCreateSendmes.UseVisualStyleBackColor = true;
            this.btnCreateSendmes.Click += new System.EventHandler(this.btnCreateSendmes_Click);
            // 
            // cmbCmd1
            // 
            this.cmbCmd1.FormattingEnabled = true;
            this.cmbCmd1.Location = new System.Drawing.Point(51, 3);
            this.cmbCmd1.Name = "cmbCmd1";
            this.cmbCmd1.Size = new System.Drawing.Size(121, 20);
            this.cmbCmd1.TabIndex = 23;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 12);
            this.label15.TabIndex = 21;
            this.label15.Text = "コマンド";
            // 
            // txtFinsSrcAdr
            // 
            this.txtFinsSrcAdr.Location = new System.Drawing.Point(84, 32);
            this.txtFinsSrcAdr.Name = "txtFinsSrcAdr";
            this.txtFinsSrcAdr.Size = new System.Drawing.Size(100, 19);
            this.txtFinsSrcAdr.TabIndex = 6;
            // 
            // txtFinsTargetAdr
            // 
            this.txtFinsTargetAdr.Location = new System.Drawing.Point(306, 33);
            this.txtFinsTargetAdr.Name = "txtFinsTargetAdr";
            this.txtFinsTargetAdr.Size = new System.Drawing.Size(100, 19);
            this.txtFinsTargetAdr.TabIndex = 7;
            this.txtFinsTargetAdr.Text = "0.1.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "FINSアドレス";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "FINSアドレス";
            // 
            // cmbMemType
            // 
            this.cmbMemType.FormattingEnabled = true;
            this.cmbMemType.Location = new System.Drawing.Point(6, 6);
            this.cmbMemType.Name = "cmbMemType";
            this.cmbMemType.Size = new System.Drawing.Size(54, 20);
            this.cmbMemType.TabIndex = 0;
            // 
            // txtMemAddress
            // 
            this.txtMemAddress.Location = new System.Drawing.Point(66, 6);
            this.txtMemAddress.Name = "txtMemAddress";
            this.txtMemAddress.Size = new System.Drawing.Size(100, 19);
            this.txtMemAddress.TabIndex = 1;
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(241, 7);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(58, 19);
            this.txtSize.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(194, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "要素数";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(44, 41);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(420, 19);
            this.textBox1.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "データ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFinsTargetAdr);
            this.Controls.Add(this.txtFinsSrcAdr);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtTargetIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbSrcIP);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSrcIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTargetIP;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtFinsSrcAdr;
        private System.Windows.Forms.TextBox txtFinsTargetAdr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbCmd1;
        private System.Windows.Forms.Button btnCreateSendmes;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cmbMemType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.TextBox txtMemAddress;
    }
}

