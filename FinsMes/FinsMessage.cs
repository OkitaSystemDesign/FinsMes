using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinsMes
{
    internal class FinsMessage : IDisposable
    {
        private TcpClient tcp;
        private UdpClient udp;
        private NetworkStream ns;
        private Int32 TargetPort = 9600;
        static byte sid = 0;

        private byte ClientNodeNo = 0;
        private byte ServerNodeNo = 0;

        public bool useTcp = false;

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


        public void Connect(string TargetIp)
        {
            MemoryStream ms = new MemoryStream();
            //useTcp = TcpConnect;

            if (useTcp)
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
            else
            {
                udp = new UdpClient();
                udp.Client.ReceiveTimeout = 1000;

                udp.Connect(TargetIp, TargetPort);
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

        public byte[] GetFinsHeader(string ServerNode, string ClientNode)
        {
            byte[] cmd = new byte[10];

            string[] snode = ServerNode.Split('.');
            string[] cnode = ClientNode.Split('.');

            // ----------------- FINSヘッダ
            cmd[0] = 0x80;          // ICF
            cmd[1] = 0x00;          // RSV
            cmd[2] = 0x02;          // GCT
            cmd[3] = Convert.ToByte(snode[0]);      // DNA  相手先ネットワークアドレス
            cmd[4] = Convert.ToByte(snode[1]);      // DA1  相手先ノードアドレス
            cmd[5] = Convert.ToByte(snode[2]);      // DA2  相手先号機アドレス
            cmd[6] = Convert.ToByte(cnode[0]);      // SNA  発信元ネットワークアドレス
            cmd[7] = Convert.ToByte(cnode[1]);      // SA1  発信元ノードアドレス
            cmd[8] = Convert.ToByte(cnode[2]);      // SA2  発信元号機アドレス
            cmd[9] = (byte)++sid;        // SID  識別子 00-FFの任意の数値

            if(useTcp)
            {
                cmd[4] = ServerNodeNo;
                cmd[7] = ClientNodeNo;
            }

            return cmd;
        }

        public byte[] GetFinsTcpHeader(int CommandLength)
        {
            byte[] cmd = new byte[16];

            // ----------------- FINS-TCPヘッダ
            cmd[0] = 0x46;          // "F"
            cmd[1] = 0x49;          // "I"
            cmd[2] = 0x4E;          // "N"
            cmd[3] = 0x53;          // "S"

            //BitConverter.GetBytes(CommandLength + 8).CopyTo(cmd, 4);    // Length (Command以降のバイト数)
            byte[] len = BitConverter.GetBytes(CommandLength + 8).Reverse().ToArray();
            Array.Copy(len, 0, cmd, 4, len.Length);

            cmd[8] = 0x00;          // Command
            cmd[9] = 0x00;
            cmd[10] = 0x00;
            cmd[11] = 0x02;
            cmd[12] = 0x00;         // ErrorCode
            cmd[13] = 0x00;
            cmd[14] = 0x00;
            cmd[15] = 0x00;

            return cmd;

        }

        public byte[] CreateFinsFrame(string ServerNode, string ClientNode, byte[] command)
        {
            byte[] finsheader = GetFinsHeader(ServerNode, ClientNode);

            byte[] cmd = null;
            int pos = 0;
            if (useTcp)
            {
                byte[] finsTcpheader = GetFinsTcpHeader(finsheader.Length + command.Length);
                cmd = new byte[finsTcpheader.Length + finsheader.Length + command.Length];
                Array.Copy(finsTcpheader, 0, cmd, 0, finsTcpheader.Length);
                pos = finsTcpheader.Length;
            }
            else
            {
                cmd = new byte[finsheader.Length + command.Length];
            }

            Array.Copy(finsheader, 0, cmd, pos, finsheader.Length);
            pos += finsheader.Length;
            Array.Copy(command, 0, cmd, pos, command.Length);

            return cmd;

        }

        private byte[] UdpSend(byte[] cmd)
        {
            byte[] buf = new byte[4096];

            sbMes.Clear(); 
            sbMes.Append("[UDP]-> " + BitConverter.ToString(cmd) + "\r\n");

            udp.Send(cmd, cmd.Length);

            udp.Client.ReceiveTimeout = 1000;
            int rcvsize = udp.Client.Receive(buf);

            byte[] res = new byte[rcvsize];
            Array.Copy(buf,0, res, 0, rcvsize);
            sbMes.Append("[UDP]<- " + BitConverter.ToString(res) + "\r\n");

            return res;
        }

        private byte[] TcpSend(byte[] cmd)
        {
            byte[] buf = new byte[4096];
            byte[] res = null;
            MemoryStream ms = new MemoryStream();

            sbMes.Clear();
            sbMes.Append("[TCP]-> " + BitConverter.ToString(cmd) + "\r\n");

            ns.Write(cmd, 0, cmd.Length);

            ns.ReadTimeout = 1000;
            do
            {
                int readsize = ns.Read(buf, 0, buf.Length);
                if (readsize == 0)
                    break;
                ms.Write(buf, 0, readsize);

            } while (ns.DataAvailable);

            if (ms.Length > 0)
            {
                res = ms.ToArray();
            }

            ms.Close();

            sbMes.Append("[TCP]<- " + BitConverter.ToString(res) + "\r\n");
            Console.WriteLine(res.Length);
            return res;
        }


        public byte[] SendCommand(byte[] command)
        {
            //byte[] buf = null;
            byte[] res = null;
            MemoryStream ms = new MemoryStream();

            sbMes.Clear();

            if (useTcp)
            {
                res = TcpSend(command);
            }
            else
            {
                res = UdpSend(command);

            }

            return res;
        }

        public byte[] MemAddress(string memstring)
        {
            string memtype = memstring.Substring(0, 1);
            string memadr = null;
            byte[] adr = new byte[3];

            switch (memtype)
            {
                case "D":
                    adr[0] = 0x82;
                    memadr = memstring.Substring(1);
                    Array.Copy(BitConverter.GetBytes(short.Parse(memadr)).Reverse().ToArray(), 0, adr, 1, 2);
                    break;

                case "E":
                    string[] strs = memstring.Split('_');
                    int bank = Convert.ToInt32(strs[0].Substring(1), 16);
                    if (bank < 16)
                        adr[0] = (byte)(0xA0 + Convert.ToByte(bank));
                    else
                        adr[0] = (byte)(0x60 + Convert.ToByte(bank - 16));

                    Array.Copy(BitConverter.GetBytes(short.Parse(strs[1])).Reverse().ToArray(), 0, adr, 1, 2);
                    break;

                case "W":
                    adr[0] = 0xB1;
                    memadr = memstring.Substring(1);
                    Array.Copy(BitConverter.GetBytes(short.Parse(memadr)).Reverse().ToArray(), 0, adr, 1, 2);
                    break;

                case "H":
                    adr[0] = 0xB2;
                    memadr = memstring.Substring(1);
                    Array.Copy(BitConverter.GetBytes(short.Parse(memadr)).Reverse().ToArray(), 0, adr, 1, 2);
                    break;

                default:
                    int cioadr;
                    if (int.TryParse(memstring, out cioadr))
                    {
                        adr[0] = 0xB0;
                        Array.Copy(BitConverter.GetBytes((short)cioadr).Reverse().ToArray(), 0, adr, 1, 2);
                    }
                    break;
            }

            return adr;
        }

    }
}
