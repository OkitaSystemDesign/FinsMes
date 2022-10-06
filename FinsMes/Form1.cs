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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbSrcIP.Text = Properties.Settings.Default.SrcIP;
            txtTargetIP.Text = Properties.Settings.Default.TargetIP;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.SrcIP = cmbSrcIP.Text;
            Properties.Settings.Default.TargetIP = txtTargetIP.Text;
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
    }
}
