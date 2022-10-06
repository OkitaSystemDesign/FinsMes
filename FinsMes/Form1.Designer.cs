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
            this.txtFinsSrcAdr = new System.Windows.Forms.TextBox();
            this.txtFinsTargetAdr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(636, 367);
            this.splitContainer1.SplitterDistance = 238;
            this.splitContainer1.TabIndex = 5;
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
    }
}

