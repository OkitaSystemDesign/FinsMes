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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSrcIP = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTargetIP = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDumpColMax = new System.Windows.Forms.TextBox();
            this.txtRes = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCmd = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabNone = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtWriteData = new System.Windows.Forms.TextBox();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.txtMemAddress = new System.Windows.Forms.TextBox();
            this.lblWriteData = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbMemType = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtMultiRead = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoMonitor = new System.Windows.Forms.RadioButton();
            this.rdoRun = new System.Windows.Forms.RadioButton();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoCycleTime = new System.Windows.Forms.RadioButton();
            this.rdoIni = new System.Windows.Forms.RadioButton();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.txtErrCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.txtRecSize = new System.Windows.Forms.TextBox();
            this.txtStartRec = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnCreateSendMes = new System.Windows.Forms.Button();
            this.cmbCmd = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtLineMonitor = new System.Windows.Forms.TextBox();
            this.txtFinsSrcAdr = new System.Windows.Forms.TextBox();
            this.txtFinsTargetAdr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCommType = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnDirectCommand = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "送信元";
            // 
            // cmbSrcIP
            // 
            this.cmbSrcIP.FormattingEnabled = true;
            this.cmbSrcIP.Location = new System.Drawing.Point(163, 12);
            this.cmbSrcIP.Name = "cmbSrcIP";
            this.cmbSrcIP.Size = new System.Drawing.Size(121, 20);
            this.cmbSrcIP.TabIndex = 1;
            this.cmbSrcIP.Click += new System.EventHandler(this.cmbSrcIP_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(318, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "送信先";
            // 
            // txtTargetIP
            // 
            this.txtTargetIP.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTargetIP.Location = new System.Drawing.Point(365, 12);
            this.txtTargetIP.Name = "txtTargetIP";
            this.txtTargetIP.Size = new System.Drawing.Size(100, 19);
            this.txtTargetIP.TabIndex = 3;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(498, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(91, 23);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.txtDumpColMax);
            this.splitContainer1.Panel1.Controls.Add(this.txtRes);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.txtCmd);
            this.splitContainer1.Panel1.Controls.Add(this.btnSend);
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1.Controls.Add(this.btnCreateSendMes);
            this.splitContainer1.Panel1.Controls.Add(this.cmbCmd);
            this.splitContainer1.Panel1.Controls.Add(this.label15);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtLineMonitor);
            this.splitContainer1.Size = new System.Drawing.Size(636, 535);
            this.splitContainer1.SplitterDistance = 416;
            this.splitContainer1.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(484, 123);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 35;
            this.label10.Text = "DUMP 表示列数";
            // 
            // txtDumpColMax
            // 
            this.txtDumpColMax.Location = new System.Drawing.Point(581, 120);
            this.txtDumpColMax.Name = "txtDumpColMax";
            this.txtDumpColMax.Size = new System.Drawing.Size(32, 19);
            this.txtDumpColMax.TabIndex = 34;
            this.txtDumpColMax.Text = "10";
            // 
            // txtRes
            // 
            this.txtRes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRes.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtRes.Location = new System.Drawing.Point(9, 199);
            this.txtRes.Multiline = true;
            this.txtRes.Name = "txtRes";
            this.txtRes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRes.Size = new System.Drawing.Size(616, 214);
            this.txtRes.TabIndex = 33;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 32;
            this.label8.Text = "送信FINSコマンド";
            // 
            // txtCmd
            // 
            this.txtCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCmd.Location = new System.Drawing.Point(9, 153);
            this.txtCmd.Multiline = true;
            this.txtCmd.Name = "txtCmd";
            this.txtCmd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCmd.Size = new System.Drawing.Size(520, 40);
            this.txtCmd.TabIndex = 30;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(549, 153);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(76, 40);
            this.btnSend.TabIndex = 31;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabNone);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(5, 29);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(624, 76);
            this.tabControl1.TabIndex = 29;
            // 
            // tabNone
            // 
            this.tabNone.Location = new System.Drawing.Point(4, 22);
            this.tabNone.Name = "tabNone";
            this.tabNone.Padding = new System.Windows.Forms.Padding(3);
            this.tabNone.Size = new System.Drawing.Size(616, 50);
            this.tabNone.TabIndex = 4;
            this.tabNone.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.CausesValidation = false;
            this.tabPage1.Controls.Add(this.txtWriteData);
            this.tabPage1.Controls.Add(this.txtSize);
            this.tabPage1.Controls.Add(this.txtMemAddress);
            this.tabPage1.Controls.Add(this.lblWriteData);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.cmbMemType);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(616, 50);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtWriteData
            // 
            this.txtWriteData.Location = new System.Drawing.Point(66, 36);
            this.txtWriteData.Name = "txtWriteData";
            this.txtWriteData.Size = new System.Drawing.Size(394, 19);
            this.txtWriteData.TabIndex = 4;
            this.txtWriteData.Text = "1234,12AB";
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(285, 6);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(58, 19);
            this.txtSize.TabIndex = 2;
            this.txtSize.Text = "2";
            // 
            // txtMemAddress
            // 
            this.txtMemAddress.Location = new System.Drawing.Point(110, 6);
            this.txtMemAddress.Name = "txtMemAddress";
            this.txtMemAddress.Size = new System.Drawing.Size(100, 19);
            this.txtMemAddress.TabIndex = 1;
            this.txtMemAddress.Text = "0";
            // 
            // lblWriteData
            // 
            this.lblWriteData.AutoSize = true;
            this.lblWriteData.Location = new System.Drawing.Point(3, 39);
            this.lblWriteData.Name = "lblWriteData";
            this.lblWriteData.Size = new System.Drawing.Size(57, 12);
            this.lblWriteData.TabIndex = 5;
            this.lblWriteData.Text = "書込データ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(238, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "要素数";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "アドレス";
            // 
            // cmbMemType
            // 
            this.cmbMemType.FormattingEnabled = true;
            this.cmbMemType.Location = new System.Drawing.Point(50, 6);
            this.cmbMemType.Name = "cmbMemType";
            this.cmbMemType.Size = new System.Drawing.Size(54, 20);
            this.cmbMemType.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtMultiRead);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(616, 50);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtMultiRead
            // 
            this.txtMultiRead.Location = new System.Drawing.Point(93, 6);
            this.txtMultiRead.Name = "txtMultiRead";
            this.txtMultiRead.Size = new System.Drawing.Size(377, 19);
            this.txtMultiRead.TabIndex = 0;
            this.txtMultiRead.Text = "D0,E0_0,W10";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "複合読出データ";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(616, 50);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoMonitor);
            this.groupBox1.Controls.Add(this.rdoRun);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(437, 38);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // rdoMonitor
            // 
            this.rdoMonitor.AutoSize = true;
            this.rdoMonitor.Location = new System.Drawing.Point(11, 16);
            this.rdoMonitor.Name = "rdoMonitor";
            this.rdoMonitor.Size = new System.Drawing.Size(49, 16);
            this.rdoMonitor.TabIndex = 0;
            this.rdoMonitor.TabStop = true;
            this.rdoMonitor.Text = "モニタ";
            this.rdoMonitor.UseVisualStyleBackColor = true;
            // 
            // rdoRun
            // 
            this.rdoRun.AutoSize = true;
            this.rdoRun.Checked = true;
            this.rdoRun.Location = new System.Drawing.Point(118, 16);
            this.rdoRun.Name = "rdoRun";
            this.rdoRun.Size = new System.Drawing.Size(47, 16);
            this.rdoRun.TabIndex = 1;
            this.rdoRun.TabStop = true;
            this.rdoRun.Text = "運転";
            this.rdoRun.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox2);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(616, 50);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoCycleTime);
            this.groupBox2.Controls.Add(this.rdoIni);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(328, 35);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // rdoCycleTime
            // 
            this.rdoCycleTime.AutoSize = true;
            this.rdoCycleTime.Checked = true;
            this.rdoCycleTime.Location = new System.Drawing.Point(116, 13);
            this.rdoCycleTime.Name = "rdoCycleTime";
            this.rdoCycleTime.Size = new System.Drawing.Size(56, 16);
            this.rdoCycleTime.TabIndex = 1;
            this.rdoCycleTime.TabStop = true;
            this.rdoCycleTime.Text = "読出し";
            this.rdoCycleTime.UseVisualStyleBackColor = true;
            // 
            // rdoIni
            // 
            this.rdoIni.AutoSize = true;
            this.rdoIni.Location = new System.Drawing.Point(12, 13);
            this.rdoIni.Name = "rdoIni";
            this.rdoIni.Size = new System.Drawing.Size(86, 16);
            this.rdoIni.TabIndex = 0;
            this.rdoIni.Text = "イニシャライズ";
            this.rdoIni.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.label19);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(616, 50);
            this.tabPage6.TabIndex = 6;
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(33, 15);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(150, 12);
            this.label19.TabIndex = 0;
            this.label19.Text = "PCの現在時刻を書き込みます";
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.txtErrCode);
            this.tabPage8.Controls.Add(this.label6);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(616, 50);
            this.tabPage8.TabIndex = 8;
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // txtErrCode
            // 
            this.txtErrCode.Location = new System.Drawing.Point(68, 7);
            this.txtErrCode.Name = "txtErrCode";
            this.txtErrCode.Size = new System.Drawing.Size(43, 19);
            this.txtErrCode.TabIndex = 1;
            this.txtErrCode.Text = "FFFF";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "故障コード";
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.txtRecSize);
            this.tabPage9.Controls.Add(this.txtStartRec);
            this.tabPage9.Controls.Add(this.label18);
            this.tabPage9.Controls.Add(this.label17);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(616, 50);
            this.tabPage9.TabIndex = 9;
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // txtRecSize
            // 
            this.txtRecSize.Location = new System.Drawing.Point(216, 6);
            this.txtRecSize.Name = "txtRecSize";
            this.txtRecSize.Size = new System.Drawing.Size(44, 19);
            this.txtRecSize.TabIndex = 3;
            this.txtRecSize.Text = "20";
            // 
            // txtStartRec
            // 
            this.txtStartRec.Location = new System.Drawing.Point(91, 6);
            this.txtStartRec.Name = "txtStartRec";
            this.txtStartRec.Size = new System.Drawing.Size(41, 19);
            this.txtStartRec.TabIndex = 1;
            this.txtStartRec.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(157, 9);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 2;
            this.label18.Text = "レコード数";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 9);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 12);
            this.label17.TabIndex = 0;
            this.label17.Text = "開始レコードNo";
            // 
            // btnCreateSendMes
            // 
            this.btnCreateSendMes.Location = new System.Drawing.Point(199, 111);
            this.btnCreateSendMes.Name = "btnCreateSendMes";
            this.btnCreateSendMes.Size = new System.Drawing.Size(195, 36);
            this.btnCreateSendMes.TabIndex = 24;
            this.btnCreateSendMes.Text = "Create FINS Command";
            this.btnCreateSendMes.UseVisualStyleBackColor = true;
            this.btnCreateSendMes.Click += new System.EventHandler(this.btnCreateFinsCommand_Click);
            // 
            // cmbCmd
            // 
            this.cmbCmd.FormattingEnabled = true;
            this.cmbCmd.Location = new System.Drawing.Point(59, 3);
            this.cmbCmd.Name = "cmbCmd";
            this.cmbCmd.Size = new System.Drawing.Size(160, 20);
            this.cmbCmd.TabIndex = 23;
            this.cmbCmd.SelectedIndexChanged += new System.EventHandler(this.cmbCmd_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 12);
            this.label15.TabIndex = 21;
            this.label15.Text = "コマンド";
            // 
            // txtLineMonitor
            // 
            this.txtLineMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLineMonitor.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtLineMonitor.Location = new System.Drawing.Point(9, 3);
            this.txtLineMonitor.Multiline = true;
            this.txtLineMonitor.Name = "txtLineMonitor";
            this.txtLineMonitor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLineMonitor.Size = new System.Drawing.Size(620, 109);
            this.txtLineMonitor.TabIndex = 20;
            this.txtLineMonitor.WordWrap = false;
            // 
            // txtFinsSrcAdr
            // 
            this.txtFinsSrcAdr.Location = new System.Drawing.Point(163, 38);
            this.txtFinsSrcAdr.Name = "txtFinsSrcAdr";
            this.txtFinsSrcAdr.Size = new System.Drawing.Size(100, 19);
            this.txtFinsSrcAdr.TabIndex = 6;
            // 
            // txtFinsTargetAdr
            // 
            this.txtFinsTargetAdr.Location = new System.Drawing.Point(365, 39);
            this.txtFinsTargetAdr.Name = "txtFinsTargetAdr";
            this.txtFinsTargetAdr.Size = new System.Drawing.Size(100, 19);
            this.txtFinsTargetAdr.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(91, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "FINSアドレス";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(293, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "FINSアドレス";
            // 
            // cmbCommType
            // 
            this.cmbCommType.FormattingEnabled = true;
            this.cmbCommType.Items.AddRange(new object[] {
            "UDP",
            "TCP"});
            this.cmbCommType.Location = new System.Drawing.Point(12, 12);
            this.cmbCommType.Name = "cmbCommType";
            this.cmbCommType.Size = new System.Drawing.Size(66, 20);
            this.cmbCommType.TabIndex = 10;
            this.cmbCommType.SelectedIndexChanged += new System.EventHandler(this.cmbCommType_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.btnDirectCommand);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(616, 50);
            this.tabPage3.TabIndex = 10;
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnDirectCommand
            // 
            this.btnDirectCommand.Enabled = false;
            this.btnDirectCommand.Location = new System.Drawing.Point(492, 6);
            this.btnDirectCommand.Name = "btnDirectCommand";
            this.btnDirectCommand.Size = new System.Drawing.Size(112, 30);
            this.btnDirectCommand.TabIndex = 0;
            this.btnDirectCommand.Text = "連続メソッドテスト";
            this.btnDirectCommand.UseVisualStyleBackColor = true;
            this.btnDirectCommand.Click += new System.EventHandler(this.btnDirectCommand_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(30, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(287, 12);
            this.label11.TabIndex = 1;
            this.label11.Text = "FinsMessageクラスのメソッドを連続して送信してテストします";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 611);
            this.Controls.Add(this.cmbCommType);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "FinsMes";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.tabPage9.ResumeLayout(false);
            this.tabPage9.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
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
        private System.Windows.Forms.ComboBox cmbCmd;
        private System.Windows.Forms.Button btnCreateSendMes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCmd;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtRes;
        private System.Windows.Forms.TextBox txtLineMonitor;
        private System.Windows.Forms.ComboBox cmbCommType;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabNone;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtWriteData;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.TextBox txtMemAddress;
        private System.Windows.Forms.Label lblWriteData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbMemType;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtMultiRead;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoMonitor;
        private System.Windows.Forms.RadioButton rdoRun;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoCycleTime;
        private System.Windows.Forms.RadioButton rdoIni;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TextBox txtErrCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TextBox txtRecSize;
        private System.Windows.Forms.TextBox txtStartRec;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtDumpColMax;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnDirectCommand;
        private System.Windows.Forms.Label label11;
    }
}

