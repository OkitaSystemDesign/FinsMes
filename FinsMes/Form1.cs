using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinsMes
{
    public partial class Form1 : Form
    {
        public class ItemSet
        {
            public string Name { get; set; }
            public int Value { get; set; }

            public ItemSet(int v, string s)
            {
                Name = s;
                Value = v;
            }
        }

        public class MemType
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
            src.Add(new ItemSet(0x0105, "0105 メモリ転送"));
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
            cmbCmd1.DataSource = src;
            cmbCmd1.DisplayMember = "Name";
            cmbCmd1.ValueMember = "Value";
            cmbCmd1.SelectedIndex = 0;


            List<MemType> dst = new List<MemType>();
            dst.Add(new MemType(0xB0, "CIO"));
            dst.Add(new MemType(0xB1, "WR"));
            dst.Add(new MemType(0xB2, "HR"));
            dst.Add(new MemType(0xB3, "AR"));
            dst.Add(new MemType(0x89, "TIM"));
            dst.Add(new MemType(0x82, "DM"));
            dst.Add(new MemType(0xA0, "EM"));
            cmbMemType.DataSource = dst;
            cmbMemType.DisplayMember = "Name";
            cmbMemType.ValueMember = "Value";
            cmbMemType.SelectedIndex = 0;

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

        private void btnCreateSendmes_Click(object sender, EventArgs e)
        {
            Console.WriteLine(cmbCmd1.SelectedValue);
        }
    }
}
