using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FinsMes
{
    internal class FinsMessage : IDisposable
    {
        private TcpClient tcp;
        private UdpClient udp;
        private NetworkStream ns;
        private Int32 TargetPort = 9600;

        private byte ClientNodeNo = 0;
        private byte ServerNodeNo = 0;

        StringBuilder sbMes = new StringBuilder();


        public string Message
        {
            get
            {
                return sbMes.ToString();
            }
        }

        public FinsMessage()
        {
            /*
            if(IPAddress!="")
            {
                Connect(IPAddress);
            }
            */
        }

        ~FinsMessage()
        {

        }

        void IDisposable.Dispose()
        {
            if (tcp.Connected)
            {
                Close();
            }
        }


        public void Connect(string TargetIp, bool useUDP)
        {
            MemoryStream ms = new MemoryStream();

            if (useUDP)
            {
                udp = new UdpClient();
                udp.Client.ReceiveTimeout = 1000;
            }
            else
            {
                //TCPClient作成と接続
                tcp = new TcpClient();

                var task = tcp.ConnectAsync(TargetIp, TargetPort);
                if (!task.Wait(1000))
                {
                    throw new SocketException(10060);
                }

                ns = tcp.GetStream();
                ns.ReadTimeout = 1000;
                ns.WriteTimeout = 1000;

                byte[] node = GetFinsNodeCmd();
                ClientNodeNo = node[0];
                ServerNodeNo = node[1];

            }

        }

        public void Close()
        {
            sbMes.Clear();

            if (udp != null)
            {
                udp.Close();
            }

            if (tcp != null)
            {
                ns.Close();
                tcp.Close();
            }
        }

        public byte[] GetFinsNodeCmd()
        {
            byte[] node = new byte[2];

            // FINS ノードアドレス情報送信コマンド
            byte[] FinsTcpHeader = new byte[20];
            FinsTcpHeader[0] = 0x46;       // "F"
            FinsTcpHeader[1] = 0x49;       // "I"
            FinsTcpHeader[2] = 0x4E;       // "N"
            FinsTcpHeader[3] = 0x53;       // "S"
            FinsTcpHeader[4] = 0x00;
            FinsTcpHeader[5] = 0x00;
            FinsTcpHeader[6] = 0x00;
            FinsTcpHeader[7] = 0x0C;
            FinsTcpHeader[8] = 0x00;
            FinsTcpHeader[9] = 0x00;
            FinsTcpHeader[10] = 0x00;
            FinsTcpHeader[11] = 0x00;
            FinsTcpHeader[12] = 0x00;
            FinsTcpHeader[13] = 0x00;
            FinsTcpHeader[14] = 0x00;
            FinsTcpHeader[15] = 0x00;
            FinsTcpHeader[16] = 0x00;
            FinsTcpHeader[17] = 0x00;
            FinsTcpHeader[18] = 0x00;
            FinsTcpHeader[19] = 0x00;

            ns.Write(FinsTcpHeader, 0, FinsTcpHeader.Length);
            sbMes.Append("[GetFinsNode]-> " + BitConverter.ToString(FinsTcpHeader) + "\r\n");

            byte[] res = new byte[256];
            int resSize = ns.Read(res, 0, res.Length);
            byte[] resData = new byte[resSize];
            Array.Copy(res, 0, resData, 0, resSize);


            if ((res[8] == 0x00 && res[9] == 0x00) &&
               (res[10] == 0x00 && res[11] == 0x01))
            {

                node[0] = res[19];  // ClientNodeNo
                node[1] = res[23];  // ServerNodeNo
            }
            sbMes.Append("[GetFinsNode]<- " + BitConverter.ToString(resData) + "\r\n");

            return node;
        }
    }
}
