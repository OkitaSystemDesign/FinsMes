using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinsMes
{
    public partial class Form1 : Form
    {
        private bool connected = false;
        private FinsMessage fins = null;
        int DumpColMax = 16;

        private class ItemSet
        {
            public string Name { get; set; }
            public int Value { get; set; }

            public ItemSet(int v, string s)
            {
                Name = s;
                Value = v;
            }
        }

        private class MemType
        {
            public string Name { get; set; }
            public int Value { get;set; }
            public MemType(int v, string s)
            {
                Name = s;
                Value = v;
            }
        }

        public Form1()
        {
            InitializeComponent();

            List<ItemSet> src = new List<ItemSet>();
            src.Add(new ItemSet(0x0101, "0101 変数読出"));
            src.Add(new ItemSet(0x0102, "0102 変数書込"));
            src.Add(new ItemSet(0x0103, "0103 一括書込"));
            src.Add(new ItemSet(0x0104, "0104 複合読出"));
            src.Add(new ItemSet(0x0401, "0401 運転開始"));
            src.Add(new ItemSet(0x0402, "0402 運転停止"));
            src.Add(new ItemSet(0x0501, "0501 CPUユニット情報"));
            src.Add(new ItemSet(0x0601, "0601 CPUステータス"));
            src.Add(new ItemSet(0x0620, "0620 サイクルタイム"));
            src.Add(new ItemSet(0x0701, "0701 時間情報読出"));
            src.Add(new ItemSet(0x0702, "0702 時間情報書込"));
            src.Add(new ItemSet(0x2101, "2101 異常解除"));
            src.Add(new ItemSet(0x2102, "2102 異常履歴"));
            src.Add(new ItemSet(0x2103, "2103 異常履歴クリア"));
            cmbCmd.DataSource = src;
            cmbCmd.DisplayMember = "Name";
            cmbCmd.ValueMember = "Value";
            cmbCmd.SelectedIndex = 0;


            List<MemType> dst = new List<MemType>();
            dst.Add(new MemType(0xB0, "CIO"));
            dst.Add(new MemType(0xB1, "WR"));
            dst.Add(new MemType(0xB2, "HR"));
            dst.Add(new MemType(0xB3, "AR"));
            dst.Add(new MemType(0x89, "TIM"));
            dst.Add(new MemType(0x82, "DM"));
            dst.Add(new MemType(0xA0, "E0"));
            dst.Add(new MemType(0xA1, "E1"));
            dst.Add(new MemType(0xA2, "E2"));
            dst.Add(new MemType(0xA3, "E3"));
            dst.Add(new MemType(0xA4, "E4"));
            dst.Add(new MemType(0xA5, "E5"));
            dst.Add(new MemType(0xA6, "E6"));
            dst.Add(new MemType(0xA7, "E7"));
            dst.Add(new MemType(0xA8, "E8"));
            dst.Add(new MemType(0xA9, "E9"));
            dst.Add(new MemType(0xAA, "EA"));
            dst.Add(new MemType(0xAB, "EB"));
            dst.Add(new MemType(0xAC, "EC"));
            dst.Add(new MemType(0xAD, "ED"));
            dst.Add(new MemType(0xAE, "EE"));
            dst.Add(new MemType(0xAF, "EF"));
            dst.Add(new MemType(0x60, "E10"));
            dst.Add(new MemType(0x61, "E11"));
            dst.Add(new MemType(0x62, "E12"));
            dst.Add(new MemType(0x63, "E13"));
            dst.Add(new MemType(0x64, "E14"));
            dst.Add(new MemType(0x65, "E15"));
            dst.Add(new MemType(0x66, "E16"));
            dst.Add(new MemType(0x67, "E17"));
            dst.Add(new MemType(0x68, "E18"));
            cmbMemType.DataSource = dst;
            cmbMemType.DisplayMember = "Name";
            cmbMemType.ValueMember = "Value";
            cmbMemType.SelectedIndex = 0;

            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SelectedIndex = 1;
            lblWriteData.Visible = false;
            txtWriteData.Visible = false;

            fins = new FinsMessage();

            cmbCommType.SelectedIndex = 0;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            cmbSrcIP.Text = Properties.Settings.Default.SrcIP;
            txtTargetIP.Text = Properties.Settings.Default.TargetIP;
            txtFinsSrcAdr.Text = Properties.Settings.Default.SrcFins;
            txtFinsTargetAdr.Text = Properties.Settings.Default.TargetFins;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.SrcIP = cmbSrcIP.Text;
            Properties.Settings.Default.TargetIP = txtTargetIP.Text;
            Properties.Settings.Default.SrcFins = txtFinsSrcAdr.Text;
            Properties.Settings.Default.TargetFins = txtFinsTargetAdr.Text;
            Properties.Settings.Default.Save();
        }

        private void cmbSrcIP_Click(object sender, EventArgs e)
        {
            cmbSrcIP.Items.Clear();

            byte[] ip = new byte[4];
            IPHostEntry IPHstEnt = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress targetip in IPHstEnt.AddressList)
            {
                ip = targetip.GetAddressBytes();
                if (ip.Length == 4)
                {
                    string IpString = "";
                    for (int i = 0; i < 3; i++)
                    {
                        IpString += ip[i].ToString() + '.';
                    }
                    IpString += ip[3].ToString();
                    cmbSrcIP.Items.Add(IpString);
                }
            }

        }

        private byte[] CreateFinsCmd()
        {
            byte[] finscmd = null;
            byte[] cmdcode = BitConverter.GetBytes((short)(int)cmbCmd.SelectedValue).Reverse().ToArray();
            byte[] address = new byte[3];
            byte[] memsize = new byte[2];

            switch (cmbCmd.SelectedValue)
            {
                case 0x0101:
                    finscmd = new byte[8];
                    Array.Copy(cmdcode, 0, finscmd, 0, 2);
                    finscmd[2] = Convert.ToByte(cmbMemType.SelectedValue);
                    address = AddressArray(txtMemAddress.Text);
                    Array.Copy(address, 0, finscmd, 3, 3);
                    memsize = BitConverter.GetBytes(short.Parse(txtSize.Text)).Reverse().ToArray();
                    Array.Copy(memsize, 0, finscmd, 6, memsize.Length);

                    break;

                case 0x0102:
                case 0x0103:
                    string[] subs = txtWriteData.Text.Split(',');
                    finscmd = new byte[8 + subs.Length * 2];

                    Array.Copy(cmdcode, 0, finscmd, 0, 2);
                    finscmd[2] = Convert.ToByte(cmbMemType.SelectedValue);
                    address = AddressArray(txtMemAddress.Text);
                    Array.Copy(address, 0, finscmd, 3, 3);
                    memsize = BitConverter.GetBytes(short.Parse(txtSize.Text)).Reverse().ToArray();
                    Array.Copy(memsize, 0, finscmd, 6, memsize.Length);

                    int offset = 0;
                    foreach(string sub in subs)
                    {
                        byte[] wdata = BitConverter.GetBytes(Convert.ToInt16(sub.Trim(' '),16)).Reverse().ToArray();
                        Array.Copy(wdata, 0, finscmd, 8 + offset , 2);
                        offset += 2;
                    }

                    break;

                case 0x0104:
                    subs = txtMultiRead.Text.Split(',');
                    finscmd = new byte[2 + subs.Length * 4];

                    Array.Copy(cmdcode, 0, finscmd, 0, 2);

                    offset = 0;
                    foreach(string sub in subs)
                    {
                        byte[] adrary = fins.MemOffset(sub.Trim(' '), 0);
                        Array.Copy(adrary, 0, finscmd, 2 + offset, 4);
                        offset += 4;
                    }
                    
                    break;

                case 0x0401:
                    finscmd = new byte[5];
                    Array.Copy(cmdcode, 0, finscmd, 0, 2);
                    finscmd[2] = 0xFF;
                    finscmd[3] = 0xFF;
                    finscmd[4] = rdoMonitor.Checked ? (byte)0x02 : (byte)0x04;

                    break;

                case 0x0402:
                    finscmd = new byte[4];
                    Array.Copy(cmdcode, 0, finscmd, 0, 2);
                    finscmd[2] = 0xFF;
                    finscmd[3] = 0xFF;

                    break;

                case 0x0501:
                case 0x0601:
                case 0x0701:
                case 0x2103:
                    finscmd = new byte[2];
                    Array.Copy(cmdcode, 0, finscmd, 0, 2);

                    break;

                case 0x0620:
                    finscmd = new byte[3];
                    Array.Copy(cmdcode, 0, finscmd, 0, 2);
                    finscmd[2] = rdoIni.Checked ? (byte)0x00 : (byte)0x01;

                    break;

                case 0x0702:
                    finscmd = new byte[9];
                    DateTime dt = DateTime.Now;

                    Array.Copy(cmdcode, 0, finscmd, 0, 2);
                    finscmd[2] = Convert.ToByte(dt.ToString("yy"), 16);
                    finscmd[3] = Convert.ToByte(dt.ToString("MM"), 16);
                    finscmd[4] = Convert.ToByte(dt.ToString("dd"), 16);
                    finscmd[5] = Convert.ToByte(dt.ToString("HH"), 16);
                    finscmd[6] = Convert.ToByte(dt.ToString("mm"), 16);
                    finscmd[7] = Convert.ToByte(dt.ToString("ss"), 16);
                    finscmd[8] = Convert.ToByte(dt.DayOfWeek);
                    
                    break;

                case 0x2101:
                    finscmd = new byte[4];
                    Array.Copy(cmdcode, 0, finscmd, 0, 2);
                    string ErrCode = txtErrCode.Text.PadLeft(4, '0');

                    finscmd[2] = Convert.ToByte(ErrCode.Substring(0, 2),16);
                    finscmd[3] = Convert.ToByte(ErrCode.Substring(2, 2),16);
                    break;

                case 0x2102:
                    finscmd = new byte[6];
                    Array.Copy(cmdcode, 0, finscmd, 0, 2);

                    byte[] StartRec = BitConverter.GetBytes(short.Parse(txtStartRec.Text)).Reverse().ToArray();
                    Array.Copy(StartRec, 0, finscmd, 2, 2);
                    byte[] RecSize = BitConverter.GetBytes(short.Parse(txtRecSize.Text)).Reverse().ToArray();
                    Array.Copy(RecSize, 0, finscmd, 4, 2);

                    break;

                default:
                    break;
            }

            return finscmd;
        }

        private void btnCreateSendMes_Click(object sender, EventArgs e)
        {
            byte[] finscmd = CreateFinsCmd();
            byte[] cmd = fins.CreateFinsFrame(finscmd);

            /*
            byte[] finsheader = fins.GetFinsHeader(txtFinsTargetAdr.Text, txtFinsSrcAdr.Text);

            byte[] cmd = null;
            int pos = 0;
            if (cmbCommType.SelectedItem.ToString() == "TCP")
            {
                byte[] finsTcpheader = fins.GetFinsTcpHeader(finsheader.Length + finscmd.Length);
                cmd = new byte[finsTcpheader.Length + finsheader.Length + finscmd.Length];
                Array.Copy(finsTcpheader, 0, cmd, 0, finsTcpheader.Length);
                pos = finsTcpheader.Length;
            }
            else
            {
                cmd = new byte[finsheader.Length + finscmd.Length];
            }

            Array.Copy(finsheader, 0, cmd, pos, finsheader.Length);
            pos += finsheader.Length;
            Array.Copy(finscmd, 0, cmd, pos, finscmd.Length);
            */
            txtCmd.Text = BitConverter.ToString(cmd);
        }

        private void cmbCmd_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbCmd.SelectedValue)
            {
                case 0x0101:
                    tabControl1.SelectedIndex = 1;
                    lblWriteData.Visible = false;
                    txtWriteData.Visible = false;
                    break;

                case 0x0102:
                    tabControl1.SelectedIndex = 1;
                    lblWriteData.Visible = true;
                    txtWriteData.Visible = true;
                    break;

                case 0x0103:
                    tabControl1.SelectedIndex = 1;
                    lblWriteData.Visible = true;
                    txtWriteData.Visible = true;

                    string[] subs = txtWriteData.Text.Split(',');
                    if (subs.Length > 1)
                        txtWriteData.Text = subs[0];

                    break;

                case 0x0104:
                    tabControl1.SelectedIndex = 2;
                    break;

                case 0x0401:
                    tabControl1.SelectedIndex = 3;
                    break;

                case 0x0402:
                    tabControl1.SelectedIndex = 0;
                    break;

                case 0x0501:
                    tabControl1.SelectedIndex = 0;
                    break;

                case 0x0601:
                    tabControl1.SelectedIndex = 0;
                    break;

                case 0x0620:
                    tabControl1.SelectedIndex = 4;
                    break;

                case 0x0701:
                    tabControl1.SelectedIndex = 0;
                    break;

                case 0x0702:
                    tabControl1.SelectedIndex = 5;
                    break;

                case 0x2101:
                    tabControl1.SelectedIndex = 6;
                    break;

                case 0x2102:
                    tabControl1.SelectedIndex = 7;
                    break;

                case 0x2103:
                    tabControl1.SelectedIndex = 0;
                    break;
            }
        }

        private byte[] AddressArray(string adr)
        {
            byte[] bytes = new byte[3];
            if (adr.IndexOf('.') == -1)
            {
                byte[] address = BitConverter.GetBytes(short.Parse(adr)).Reverse().ToArray();
                Array.Copy(address, 0, bytes, 0, address.Length);
                bytes[2] = 0x00;
            }
            else
            {
                byte[] address = BitConverter.GetBytes(short.Parse(adr.Split('.')[0])).Reverse().ToArray();
                Array.Copy(address, 0, bytes, 0, address.Length);
                bytes[2] = Convert.ToByte(adr.Split('.')[1]);

            }

            return bytes;
        }


        private void connect(bool value)
        {
            connected = value;

            cmbCommType.Enabled = !value;
            cmbSrcIP.Enabled = !value;
            txtTargetIP.Enabled = !value;
            txtFinsSrcAdr.Enabled = !value;
            txtFinsTargetAdr.Enabled   = !value;

            btnSend.Enabled = value;

            if (value)
            {
                btnConnect.BackColor = Color.Yellow;
            }
            else
            {
                btnConnect.BackColor = SystemColors.Control;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (connected)
                {
                    connect(false);
                    fins.Close();

                }
                else
                {
                    bool TcpConnect;
                    if (cmbCommType.SelectedIndex == 0)
                        TcpConnect = false;
                    else
                        TcpConnect = true;

                    fins.Connect(txtTargetIP.Text, txtFinsTargetAdr.Text, txtFinsSrcAdr.Text, TcpConnect);

                    txtLineMonitor.AppendText(fins.MessageLog);

                    connect(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string[] str = txtCmd.Text.Split('-');
                byte[] senddata = new byte[str.Length];
                for (int i = 0; i < str.Length; i++)
                {
                    senddata[i] = Convert.ToByte(str[i], 16);
                }
                Console.WriteLine(BitConverter.ToString(senddata));

                DumpColMax = int.Parse(txtDumpColMax.Text);
                txtRes.AppendText("-->> Send\r\n" + Dump.Execute(senddata, DumpColMax) + "\r\n\r\n");

                byte[] res = fins.SendCommand(senddata);

                txtRes.AppendText("<<-- Recive\r\n" + Dump.Execute(res, DumpColMax) + "\r\n\r\n");
                Console.WriteLine(BitConverter.ToString(senddata));

                txtLineMonitor.AppendText(fins.MessageLog);

            }
            catch (System.Net.Sockets.SocketException ex)
            {
                MessageBox.Show(ex.Message, "Socket", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connect(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connect(false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DumpColMax = int.Parse(txtDumpColMax.Text);

            // Read 0101
            byte[] res = fins.read("D0", 1000);
            txtRes.AppendText("<Read Command>\r\n" + Dump.Execute(res, DumpColMax) + "\r\n\r\n");
            txtLineMonitor.AppendText(fins.MessageLog);

            // Write 0102
            byte[] writedata = new byte[2000];
            for(int cnt = 0; cnt<1000; cnt++)
            {
                byte[] data = BitConverter.GetBytes((short)cnt).Reverse().ToArray();
                Array.Copy(data, 0, writedata, cnt * 2, 2);
            }
            fins.write("D1000", writedata);
            txtRes.AppendText("<Write Command>\r\n" + "\r\n\r\n");
            txtLineMonitor.AppendText(fins.MessageLog);

            // Fill 0103
            byte[] filldata = BitConverter.GetBytes((short)100).Reverse().ToArray();
            fins.fill("D2000", 100, filldata);
            txtRes.AppendText("<Fill Command>\r\n" + "\r\n\r\n");
            txtLineMonitor.AppendText(fins.MessageLog);

            // MultiRead 0x0104
            res = fins.MultiRead("D0,D10,D50");
            txtRes.AppendText("<MultiRead Command>\r\n" + Dump.Execute(res, DumpColMax) + "\r\n\r\n");
            txtLineMonitor.AppendText(fins.MessageLog);

            // ReadUnitData 0x0501
            res =fins.ReadUnitData();
            txtRes.AppendText("<ReadUnitData Command>\r\n" + Dump.Execute(res, DumpColMax) + "\r\n\r\n");
            txtLineMonitor.AppendText(fins.MessageLog);

            // ReadUnitStatus 0x0601
            res = fins.ReadUnitStatus();
            txtRes.AppendText("<ReadUnitStatus Command>\r\n" + Dump.Execute(res, DumpColMax) + "\r\n\r\n");
            txtLineMonitor.AppendText(fins.MessageLog);

            // ReadCycleTime 0x0620
            res = fins.ReadUnitStatus();
            txtRes.AppendText("<ReadCycleTime Command>\r\n" + Dump.Execute(res, DumpColMax) + "\r\n\r\n");
            txtLineMonitor.AppendText(fins.MessageLog);

            // Clock 0x0701
            res = fins.Clock();
            txtRes.AppendText("<Clock Command>\r\n" + Dump.Execute(res, DumpColMax) + "\r\n\r\n");
            txtLineMonitor.AppendText(fins.MessageLog);

            // SetClock 0x0702
            fins.SetClock();
            txtRes.AppendText("<SetClock Command>\r\n" + "\r\n\r\n");
            txtLineMonitor.AppendText(fins.MessageLog);

            // stop 0x0402
            fins.stop();
            txtRes.AppendText("<stop Command>\r\n" + "\r\n\r\n");
            txtLineMonitor.AppendText(fins.MessageLog);

            // ErrorClear 0x2101
            fins.ErrorClear();
            txtRes.AppendText("<ErrorClear Command>\r\n" + "\r\n\r\n");
            txtLineMonitor.AppendText(fins.MessageLog);

            // ErrorLogRead 0x2102
            res = fins.ErrorLogRead();
            txtRes.AppendText("<ErrorLogRead Command>\r\n" + Dump.Execute(res, DumpColMax) + "\r\n\r\n");
            txtLineMonitor.AppendText(fins.MessageLog);

            // ErrorLogClear 0x2103
            fins.ErrorLogClear();
            txtRes.AppendText("<ErrorLogClear Command>\r\n" + "\r\n\r\n");
            txtLineMonitor.AppendText(fins.MessageLog);

            // run 0x0401
            fins.run(0x02);
            txtRes.AppendText("<run Command>\r\n" + "\r\n\r\n");
            txtLineMonitor.AppendText(fins.MessageLog);

        }


    }
}
