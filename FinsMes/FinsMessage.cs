using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace FinsMes
{
    internal class FinsMessage : IDisposable
    {
        private TcpClient tcp;
        private UdpClient udp;
        private NetworkStream ns;
        private Int32 TargetPort = 9600;
        static byte sid = 0;

        private byte[] ClientNodeNo = new byte[] { 0, 0, 0 };
        private byte[] ServerNodeNo = new byte[] { 0, 0, 0 };

        public bool useTcp = false;

        StringBuilder sbMes = new StringBuilder();


        public string MessageLog
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


        public void Connect(string TargetIp, string ServerNode, string ClientNode, bool TcpConnect=false)
        {
            MemoryStream ms = new MemoryStream();
            useTcp = TcpConnect;
            string[] snode = ServerNode.Split('.');
            string[] cnode = ClientNode.Split('.');
            for(int i = 0; i < 3; i++)
            {
                ServerNodeNo[i] = Convert.ToByte(snode[i]);
                ClientNodeNo[i] = Convert.ToByte(cnode[i]);
            }

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
                ClientNodeNo[1] = node[0];
                ServerNodeNo[1] = node[1];

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

        public byte[] GetFinsHeader()
        {
            byte[] cmd = new byte[10];

            // ----------------- FINSヘッダ
            cmd[0] = 0x80;          // ICF
            cmd[1] = 0x00;          // RSV
            cmd[2] = 0x02;          // GCT
            cmd[3] = ServerNodeNo[0];      // DNA  相手先ネットワークアドレス
            cmd[4] = ServerNodeNo[1];      // DA1  相手先ノードアドレス
            cmd[5] = ServerNodeNo[2];      // DA2  相手先号機アドレス
            cmd[6] = ClientNodeNo[0];      // SNA  発信元ネットワークアドレス
            cmd[7] = ClientNodeNo[1];      // SA1  発信元ノードアドレス
            cmd[8] = ClientNodeNo[2];      // SA2  発信元号機アドレス
            cmd[9] = (byte)++sid;        // SID  識別子 00-FFの任意の数値

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

        public byte[] CreateFinsFrame(byte[] command)
        {
            byte[] finsheader = GetFinsHeader();

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
            Array.Copy(buf, 0, res, 0, rcvsize);
            sbMes.Append("[UDP]<- " + BitConverter.ToString(res) + "\r\n");

            byte[] endcode = new byte[2];
            Array.Copy(res, 12, endcode, 0, 2);
            if (!(endcode[0] == 0x00 && endcode[1] == 0x00))
                throw new Exception($"FINS Error : {BitConverter.ToString(endcode)}");

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

            byte[] endcode = new byte[2];
            Array.Copy(res, 28, endcode, 0, 2);
            if (!(endcode[0] == 0x00 && endcode[1] == 0x00))
                throw new Exception($"FINS Error : {BitConverter.ToString(endcode)}");

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

        public byte[] MemOffset(string memstring, short offset)
        {
            string memtype = memstring.Substring(0, 1);
            short memadr = 0;
            byte[] adr = new byte[4];

            switch (memtype.ToUpper())
            {
                case "D":
                    adr[0] = 0x82;
                    memadr = (short)(int.Parse(memstring.Substring(1)) + offset);
                    Array.Copy(BitConverter.GetBytes(memadr).Reverse().ToArray(), 0, adr, 1, 2);
                    break;

                case "E":
                    string[] strs = memstring.Split('_');
                    memadr = (short)(int.Parse(strs[1]) + offset);
                    int bank = Convert.ToInt32(strs[0].Substring(1), 16);
                    if (bank < 16)
                        adr[0] = (byte)(0xA0 + Convert.ToByte(bank));
                    else
                        adr[0] = (byte)(0x60 + Convert.ToByte(bank - 16));

                    Array.Copy(BitConverter.GetBytes(memadr).Reverse().ToArray(), 0, adr, 1, 2);
                    break;

                case "W":
                    adr[0] = 0xB1;
                    memadr = (short)(int.Parse(memstring.Substring(1)) + offset);
                    Array.Copy(BitConverter.GetBytes(memadr).Reverse().ToArray(), 0, adr, 1, 2);
                    break;

                case "H":
                    adr[0] = 0xB2;
                    memadr = (short)(int.Parse(memstring.Substring(1)) + offset);
                    Array.Copy(BitConverter.GetBytes(memadr).Reverse().ToArray(), 0, adr, 1, 2);
                    break;

                default:
                    int cioadr;
                    if (int.TryParse(memstring, out cioadr))
                    {
                        adr[0] = 0xB0;
                        Array.Copy(BitConverter.GetBytes((short)(cioadr + offset)).Reverse().ToArray(), 0, adr, 1, 2);
                    }
                    break;
            }

            return adr;
        }

        public byte[] read(string memaddr, int readsize)
        {
            byte[] finscmd = new byte[8];

            int readnum = readsize / 990;
            int remainder = readsize % 990;
            byte[] data = new byte[readsize * 2];

            for (int cnt = 0; cnt < readnum + 1; cnt++)
            {
                byte[] MemAddressArray = MemOffset(memaddr, (short)(cnt * 990));
                short size = 0;
                if (cnt == readnum)
                    size = (short)remainder;
                else
                    size = (short)990;

                byte[] wordsize = BitConverter.GetBytes(size).Reverse().ToArray();

                finscmd[0] = 0x01;
                finscmd[1] = 0x01;
                finscmd[2] = MemAddressArray[0];
                finscmd[3] = MemAddressArray[1];
                finscmd[4] = MemAddressArray[2];
                finscmd[5] = MemAddressArray[3];
                finscmd[6] = wordsize[0];
                finscmd[7] = wordsize[1];

                byte[] cmd = CreateFinsFrame(finscmd);

                if (useTcp)
                {
                    byte[] res = TcpSend(cmd);
                    Array.Copy(res, 30, data, cnt * 990 * 2, size * 2);
                }
                else
                {
                    byte[] res = UdpSend(cmd);
                    Array.Copy(res, 14, data, cnt * 990 * 2, size * 2);
                }
            }

            return data;
        }

        public void write(string memaddr, byte[] data)
        {
            if (data.Length % 2 == 1)
                throw new Exception($"Write data must be an even number of bytes");

            int WriteWordSize = data.Length / 2;

            int writenum = WriteWordSize / 990;
            int remainder = WriteWordSize % 990;

            for (int cnt = 0; cnt < writenum + 1; cnt++)
            {
                byte[] MemAddressArray = MemOffset(memaddr, (short)(cnt * 990));
                short size = 0;
                if (cnt == writenum)
                    size = (short)remainder;
                else
                    size = 990;

                byte[] wordsize = BitConverter.GetBytes(size).Reverse().ToArray();

                byte[] finscmd = new byte[8 + size * 2];

                finscmd[0] = 0x01;
                finscmd[1] = 0x02;
                finscmd[2] = MemAddressArray[0];
                finscmd[3] = MemAddressArray[1];
                finscmd[4] = MemAddressArray[2];
                finscmd[5] = MemAddressArray[3];
                finscmd[6] = wordsize[0];
                finscmd[7] = wordsize[1];
                Array.Copy(data, cnt * 990 * 2, finscmd, 8, size * 2);

                byte[] cmd = CreateFinsFrame(finscmd);

                if (useTcp)
                {
                    byte[] res = TcpSend(cmd);
                }
                else
                {
                    byte[] res = UdpSend(cmd);
                }
            }
        }

        public void fill(string memaddr, int size, byte[] data)
        {
            byte[] MemAddressArray = MemOffset(memaddr, 0);
            byte[] wordsize = BitConverter.GetBytes((short)size).Reverse().ToArray();

            byte[] finscmd = new byte[10];

            finscmd[0] = 0x01;
            finscmd[1] = 0x03;
            finscmd[2] = MemAddressArray[0];
            finscmd[3] = MemAddressArray[1];
            finscmd[4] = MemAddressArray[2];
            finscmd[5] = MemAddressArray[3];
            finscmd[6] = wordsize[0];
            finscmd[7] = wordsize[1];
            Array.Copy(data, 0, finscmd, 8, 2);

            byte[] cmd = CreateFinsFrame(finscmd);

            byte[] res;
            if (useTcp)
                res = TcpSend(cmd);
            else
                res = UdpSend(cmd);

        }

        public byte[] MultiRead(string memaddresses)
        {
            string[] adrs = memaddresses.Split(',');
            byte[] adrary = new byte[4 * adrs.Length];

            if (adrs.Length == 0)
                throw new Exception($"Address Format Error");

            int pos = 0;
            foreach (string adr in adrs)
            {
                byte[] MemAddressArray = MemOffset(adr.Trim(' '), 0);
                Array.Copy(MemAddressArray, 0, adrary, pos, 4);
                pos += 4;
            }

            byte[] finscmd = new byte[2 + adrary.Length];

            finscmd[0] = 0x01;
            finscmd[1] = 0x04;
            Array.Copy(adrary, 0, finscmd, 2, adrary.Length);

            byte[] cmd = CreateFinsFrame(finscmd);
            byte[] data = new byte[adrs.Length * 3];

            if (useTcp)
            {
                byte[] res = TcpSend(cmd);
                Array.Copy(res, 30, data, 0, data.Length);
            }
            else
            {
                byte[] res = UdpSend(cmd);
                Array.Copy(res, 14, data, 0, data.Length);
            }

            return data;
        }

        public void run(byte Mode)
        {
            byte[] finscmd = new byte[5];
            finscmd[0] = 0x04;
            finscmd[1] = 0x01;
            finscmd[2] = 0xFF;
            finscmd[3] = 0xFF;
            finscmd[4] = Mode;

            byte[] cmd = CreateFinsFrame(finscmd);

            byte[] res;
            if (useTcp)
                res = TcpSend(cmd);
            else
                res = UdpSend(cmd);
        }

        public void stop()
        {
            byte[] finscmd = new byte[4];
            finscmd[0] = 0x04;
            finscmd[1] = 0x02;
            finscmd[2] = 0xFF;
            finscmd[3] = 0xFF;

            byte[] cmd = CreateFinsFrame(finscmd);

            byte[] res;
            if (useTcp)
                res = TcpSend(cmd);
            else
                res = UdpSend(cmd);
        }

        public byte[] ReadUnitData()
        {
            byte[] finscmd = new byte[3];
            finscmd[0] = 0x05;
            finscmd[1] = 0x01;
            finscmd[2] = 0x00;

            byte[] cmd = CreateFinsFrame(finscmd);
            byte[] data;

            if (useTcp)
            {
                byte[] res = TcpSend(cmd);
                data = new byte[res.Length - 30];
                Array.Copy(res, 30, data, 0, data.Length);
            }
            else
            {
                byte[] res = UdpSend(cmd);
                data = new byte[res.Length - 14];
                Array.Copy(res, 14, data, 0, data.Length);
            }
            return data;
        }

        public byte[] ReadUnitStatus()
        {
            byte[] finscmd = new byte[2];
            finscmd[0] = 0x06;
            finscmd[1] = 0x01;

            byte[] cmd = CreateFinsFrame(finscmd);
            byte[] data;

            if (useTcp)
            {
                byte[] res = TcpSend(cmd);
                data = new byte[res.Length - 30];
                Array.Copy(res, 30, data, 0, data.Length);
            }
            else
            {
                byte[] res = UdpSend(cmd);
                data = new byte[res.Length - 14];
                Array.Copy(res, 14, data, 0, data.Length);
            }
            return data;
        }

        public byte[] ReadCycleTime()
        {
            byte[] finscmd = new byte[3];
            finscmd[0] = 0x06;
            finscmd[1] = 0x20;
            finscmd[2] = 0x01;

            byte[] cmd = CreateFinsFrame(finscmd);
            byte[] data;

            if (useTcp)
            {
                byte[] res = TcpSend(cmd);
                data = new byte[res.Length - 30];
                Array.Copy(res, 30, data, 0, data.Length);
            }
            else
            {
                byte[] res = UdpSend(cmd);
                data = new byte[res.Length - 14];
                Array.Copy(res, 14, data, 0, data.Length);
            }
            return data;
        }

        public byte[] Clock()
        {
            byte[] finscmd = new byte[2];
            finscmd[0] = 0x07;
            finscmd[1] = 0x01;

            byte[] cmd = CreateFinsFrame(finscmd);
            byte[] data;

            if (useTcp)
            {
                byte[] res = TcpSend(cmd);
                data = new byte[res.Length - 30];
                Array.Copy(res, 30, data, 0, data.Length);
            }
            else
            {
                byte[] res = UdpSend(cmd);
                data = new byte[res.Length - 14];
                Array.Copy(res, 14, data, 0, data.Length);
            }
            return data;
        }

        public void SetClock()
        {
            DateTime dt = DateTime.Now;

            byte[] finscmd = new byte[9];
            finscmd[0] = 0x07;
            finscmd[1] = 0x02;
            finscmd[2] = Convert.ToByte(dt.ToString("yy"), 16);
            finscmd[3] = Convert.ToByte(dt.ToString("MM"), 16);
            finscmd[4] = Convert.ToByte(dt.ToString("dd"), 16);
            finscmd[5] = Convert.ToByte(dt.ToString("HH"), 16);
            finscmd[6] = Convert.ToByte(dt.ToString("mm"), 16);
            finscmd[7] = Convert.ToByte(dt.ToString("ss"), 16);
            finscmd[8] = Convert.ToByte(dt.DayOfWeek);

            byte[] cmd = CreateFinsFrame(finscmd);

            if (useTcp)
            {
                byte[] res = TcpSend(cmd);
            }
            else
            {
                byte[] res = UdpSend(cmd);
            }
        }

        public void ErrorClear()
        {
            byte[] finscmd = new byte[4];
            finscmd[0] = 0x21;
            finscmd[1] = 0x01;
            finscmd[2] = 0xFF;
            finscmd[3] = 0xFF;

            byte[] cmd = CreateFinsFrame(finscmd);

            if (useTcp)
            {
                byte[] res = TcpSend(cmd);
            }
            else
            {
                byte[] res = UdpSend(cmd);
            }
        }

        public byte[] ErrorLogRead()
        {
            byte[] finscmd = new byte[6];
            finscmd[0] = 0x21;
            finscmd[1] = 0x02;
            finscmd[2] = 0x00;
            finscmd[3] = 0x00;
            finscmd[4] = 0x00;
            finscmd[5] = 0x0A;

            byte[] cmd = CreateFinsFrame(finscmd);
            byte[] data;

            if (useTcp)
            {
                byte[] res = TcpSend(cmd);
                data = new byte[res.Length - 30];
                Array.Copy(res, 30, data, 0, data.Length);
            }
            else
            {
                byte[] res = UdpSend(cmd);
                data = new byte[res.Length - 14];
                Array.Copy(res, 14, data, 0, data.Length);
            }
            return data;
        }

        public void ErrorLogClear()
        {
            DateTime dt = DateTime.Now;

            byte[] finscmd = new byte[2];
            finscmd[0] = 0x21;
            finscmd[1] = 0x03;

            byte[] cmd = CreateFinsFrame(finscmd);

            if (useTcp)
            {
                byte[] res = TcpSend(cmd);
            }
            else
            {
                byte[] res = UdpSend(cmd);
            }
        }

    }
}
