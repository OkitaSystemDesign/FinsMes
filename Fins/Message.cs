using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Fins
{
    [Serializable]
    public class FinsException : Exception
    {
        public FinsException() { }

        public FinsException(string message)
            : base(message) { }

        public FinsException(string message, Exception inner)
            : base(message, inner) { }

    }

    public class Message : IDisposable
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

        public Message()
        {

        }

        ~Message()
        {

        }

        void IDisposable.Dispose()
        {
            if (tcp.Connected)
            {
                Close();
            }
        }


        private string FinsExceptionMessage(byte[] EndCode)
        {
            string Message = "";
            short Code = (short)(EndCode[0] * 0x100 + EndCode[1]);

            if (Code == 0x0101)
                Message = Convert.ToString(Code, 16) + ": Local node not in network (自ノード ネットワーク未加入)";
            else if (Code == 0x0102)
                Message = Convert.ToString(Code, 16) + ": Token timeout (トークン タイムアウト)";
            else if (Code == 0x0103)
                Message = Convert.ToString(Code, 16) + ": Retries failed (再送オーバー)";
            else if (Code == 0x0104)
                Message = Convert.ToString(Code, 16) + ": Too many send frames (送信許可フレーム数オーバー)";
            else if (Code == 0x0105)
                Message = Convert.ToString(Code, 16) + ": Node address range error (ノードアドレス設定範囲エラー)";
            else if (Code == 0x0106)
                Message = Convert.ToString(Code, 16) + ": Node address duplication (ノードアドレス二重設定エラー)";
            else if (Code == 0x0201)
                Message = Convert.ToString(Code, 16) + ": Destination node not in network (相手ノード ネットワーク未加入)";
            else if (Code == 0x0202)
                Message = Convert.ToString(Code, 16) + ": Unit missing (該当ユニットなし)";
            else if (Code == 0x0203)
                Message = Convert.ToString(Code, 16) + ": Third node missing (第三ノード ネットワーク未加入)";
            else if (Code == 0x0204)
                Message = Convert.ToString(Code, 16) + ": Destination node busy (相手ノード ビジー)";
            else if (Code == 0x0205)
                Message = Convert.ToString(Code, 16) + ": Response timeout (レスポンス タイムアウト)";
            else if (Code == 0x0301)
                Message = Convert.ToString(Code, 16) + ": Communications controller error (通信コントローラ異常)";
            else if (Code == 0x0302)
                Message = Convert.ToString(Code, 16) + ": CPU Unit error (CPUユニット異常)";
            else if (Code == 0x0303)
                Message = Convert.ToString(Code, 16) + ": Controller error (該当コントローラ異常)";
            else if (Code == 0x0304)
                Message = Convert.ToString(Code, 16) + ": Unit number error (ユニット番号設定異常)";
            else if (Code == 0x0401)
                Message = Convert.ToString(Code, 16) + ": Undefined command (未定義コマンド)";
            else if (Code == 0x0402)
                Message = Convert.ToString(Code, 16) + ": Not supported by model/version (サポート外機種/バージョン)";
            else if (Code == 0x0501)
                Message = Convert.ToString(Code, 16) + ": Destination address setting error (相手アドレス設定エラー)";
            else if (Code == 0x0502)
                Message = Convert.ToString(Code, 16) + ": No routing tables (ルーチングテーブル未登録)";
            else if (Code == 0x0503)
                Message = Convert.ToString(Code, 16) + ": Routing table error (ルーチングテーブル異常)";
            else if (Code == 0x0504)
                Message = Convert.ToString(Code, 16) + ": oo many relays (中継回数オーバー)";
            else if (Code == 0x1001)
                Message = Convert.ToString(Code, 16) + ": Command too long (コマンド長オーバー)";
            else if (Code == 0x1002)
                Message = Convert.ToString(Code, 16) + ": Command too short (コマンド長不足)";
            else if (Code == 0x1003)
                Message = Convert.ToString(Code, 16) + ": Elements/data don’t match (要素数/データ数不一致)";
            else if (Code == 0x1004)
                Message = Convert.ToString(Code, 16) + ": Command format error (コマンドフォーマットエラー)";
            else if (Code == 0x1005)
                Message = Convert.ToString(Code, 16) + ": Header error (ヘッダ異常)";
            else if (Code == 0x1101)
                Message = Convert.ToString(Code, 16) + ": Area classification missing (エリア種別なし)";
            else if (Code == 0x1102)
                Message = Convert.ToString(Code, 16) + ": Access size error (アクセスサイズエラー)";
            else if (Code == 0x1103)
                Message = Convert.ToString(Code, 16) + ": Address range error (アドレス範囲外指定エラー)";
            else if (Code == 0x1104)
                Message = Convert.ToString(Code, 16) + ": Address range exceeded (アドレス範囲オーバー)";
            else if (Code == 0x1106)
                Message = Convert.ToString(Code, 16) + ": Program missing (該当プログラム番号なし)";
            else if (Code == 0x1109)
                Message = Convert.ToString(Code, 16) + ": Relational error (相関関係エラー)";
            else if (Code == 0x110A)
                Message = Convert.ToString(Code, 16) + ": Duplicate data access (データ重複エラー)";
            else if (Code == 0x110B)
                Message = Convert.ToString(Code, 16) + ": Response too long (レスポンス長オーバー)";
            else if (Code == 0x110C)
                Message = Convert.ToString(Code, 16) + ": Parameter error (パラメータエラー)";
            else if (Code == 0x2002)
                Message = Convert.ToString(Code, 16) + ": Protected (プロテクト中)";
            else if (Code == 0x2003)
                Message = Convert.ToString(Code, 16) + ": Table missing (登録テーブルなし)";
            else if (Code == 0x2004)
                Message = Convert.ToString(Code, 16) + ": Data missing (検索データなし)";
            else if (Code == 0x2005)
                Message = Convert.ToString(Code, 16) + ": Program missing (該当プログラム番号なし)";
            else if (Code == 0x2006)
                Message = Convert.ToString(Code, 16) + ": File missing (該当ファイルなし)";
            else if (Code == 0x2007)
                Message = Convert.ToString(Code, 16) + ": Data mismatch (照合異常)";
            else if (Code == 0x2101)
                Message = Convert.ToString(Code, 16) + ": Read-only (リードオンリー)";
            else if (Code == 0x2102)
                Message = Convert.ToString(Code, 16) + ": Protected (プロテクト中)";
            else if (Code == 0x2103)
                Message = Convert.ToString(Code, 16) + ": Cannot register (登録不可)";
            else if (Code == 0x2105)
                Message = Convert.ToString(Code, 16) + ": Program missing (該当プログラム番号なし)";
            else if (Code == 0x2106)
                Message = Convert.ToString(Code, 16) + ": File missing (該当ファイルなし)";
            else if (Code == 0x2107)
                Message = Convert.ToString(Code, 16) + ": File name already exists (同一ファイル名あり)";
            else if (Code == 0x2108)
                Message = Convert.ToString(Code, 16) + ": Cannot change (変更不可)";
            else if (Code == 0x2201)
                Message = Convert.ToString(Code, 16) + ": Not possible during execution (運転中のため動作不可)";
            else if (Code == 0x2202)
                Message = Convert.ToString(Code, 16) + ": Not possible while running (停止中)";
            else if (Code == 0x2203)
                Message = Convert.ToString(Code, 16) + ": Wrong PLC mode, PROGRAM mode (本体モードが違う プログラムモード)";
            else if (Code == 0x2204)
                Message = Convert.ToString(Code, 16) + ": Wrong PLC mode, DEBUG mode (本体モードが違う デバッグモード)";
            else if (Code == 0x2205)
                Message = Convert.ToString(Code, 16) + ": Wrong PLC mode, MONITOR mode (本体モードが違う モニタモード)";
            else if (Code == 0x2206)
                Message = Convert.ToString(Code, 16) + ": Wrong PLC mode, RUN mode (本体モードが違う 運転モード)";
            else if (Code == 0x2207)
                Message = Convert.ToString(Code, 16) + ": Specified node not polling node (指定ノードが管理局でない)";
            else if (Code == 0x2208)
                Message = Convert.ToString(Code, 16) + ": Step cannot be executed (ステップが実行不可)";
            else if (Code == 0x2301)
                Message = Convert.ToString(Code, 16) + ": File device missing (ファイル装置なし)";
            else if (Code == 0x2302)
                Message = Convert.ToString(Code, 16) + ": Memory missing (該当メモリなし)";
            else if (Code == 0x2303)
                Message = Convert.ToString(Code, 16) + ": Clock missing (時計なし)";
            else if (Code == 0x2401)
                Message = Convert.ToString(Code, 16) + ": Table missing (登録テーブルなし)";
            else
                Message = "FINS END CODE = " + Convert.ToString(Code, 16);


            return Message;
        }

        public void SetFinsNode(string ServerNode, string ClientNode)
        {
            string[] snode = ServerNode.Split('.');
            string[] cnode = ClientNode.Split('.');

            if (snode.Length == 3 && cnode.Length == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    ServerNodeNo[i] = Convert.ToByte(snode[i]);
                    ClientNodeNo[i] = Convert.ToByte(cnode[i]);
                }
            }
        }

        public byte GetFinsClientNode()
        {
            return ClientNodeNo[1];

        }

        public void Connect(string TargetIp, bool TcpConnect = false)
        {
            MemoryStream ms = new MemoryStream();
            useTcp = TcpConnect;

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

                byte[] node = GetServerFinsNodeCmd();
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

        public byte[] GetServerFinsNodeCmd()
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

            //sbMes.Clear(); 
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
            {
                string mes = FinsExceptionMessage(endcode);
                throw new FinsException(mes);
            }

            return res;
        }

        private byte[] TcpSend(byte[] cmd)
        {
            byte[] buf = new byte[4096];
            byte[] res = null;
            MemoryStream ms = new MemoryStream();

            //sbMes.Clear();
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
            {
                string mes = FinsExceptionMessage(endcode);
                throw new FinsException(mes);
            }

            return res;
        }


        public byte[] SendCommand(byte[] command)
        {
            byte[] res = null;
            //MemoryStream ms = new MemoryStream();

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

            sbMes.Clear();

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

            sbMes.Clear();

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

            sbMes.Clear();

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

            sbMes.Clear();

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
            sbMes.Clear();

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
            sbMes.Clear();

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
            sbMes.Clear();

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
            sbMes.Clear();

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
            sbMes.Clear();

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
            sbMes.Clear();

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
            sbMes.Clear();

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
            sbMes.Clear();

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
            sbMes.Clear();

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
            sbMes.Clear();

            //DateTime dt = DateTime.Now;

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

        public BitArray toBitArray(byte[] data)
        {
            BitArray bits = new BitArray(data);
            return bits;
        }

        public bool[] toBoolArray(byte[] data)
        {
            bool[] Bools = new bool[data.Length * 8];

            byte[] word = new byte[2];
            for (int i = 0; i < data.Length / 2; i++)
            {
                word[0] = data[i * 2 + 1];
                word[1] = data[i * 2];
                BitArray bits = new BitArray(word);
                for (int j = 0; j < bits.Length; j++)
                {
                    Bools[i * 16 + j] = bits[15 - j];
                }
            }
            return Bools;
        }

        public string WordToBin(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length / 2; i++)
            {
                byte[] word = new byte[2];
                Array.Copy(data, i * 2, word, 0, 2);
                sb.Append(Convert.ToString(BitConverter.ToInt16(word.Reverse().ToArray(), 0), 2).PadLeft(16, '0'));
            }
            return sb.ToString();
        }

        public short[] toInt16(byte[] data)
        {
            short[] ret = new short[data.Length / 2];
            byte[] word = new byte[2];

            for (int i = 0; i < ret.Length; i++)
            {
                Array.Copy(data, i * 2, word, 0, 2);
                ret[i] = BitConverter.ToInt16(word.Reverse().ToArray(), 0);
            }
            return ret;
        }

        public int[] toInt32(byte[] data)
        {
            int[] ret = new int[data.Length / 4];
            byte[] word = new byte[4];

            for (int i = 0; i < ret.Length; i++)
            {
                Array.Copy(data, i * 4, word, 0, 4);
                ret[i] = BitConverter.ToInt32(word.Reverse().ToArray(), 0);
            }
            return ret;
        }

        public long[] toInt64(byte[] data)
        {
            long[] ret = new long[data.Length / 8];
            byte[] word = new byte[8];

            for (int i = 0; i < ret.Length; i++)
            {
                Array.Copy(data, i * 8, word, 0, 8);
                ret[i] = BitConverter.ToInt64(word.Reverse().ToArray(), 0);
            }
            return ret;
        }

        public ushort[] toUInt16(byte[] data)
        {
            ushort[] ret = new ushort[data.Length / 2];
            byte[] word = new byte[2];

            for (int i = 0; i < ret.Length; i++)
            {
                Array.Copy(data, i * 2, word, 0, 2);
                ret[i] = BitConverter.ToUInt16(word.Reverse().ToArray(), 0);
            }
            return ret;
        }

        public uint[] toUInt32(byte[] data)
        {
            uint[] ret = new uint[data.Length / 4];
            byte[] word = new byte[4];

            for (int i = 0; i < ret.Length; i++)
            {
                Array.Copy(data, i * 4, word, 0, 4);
                ret[i] = BitConverter.ToUInt32(word.Reverse().ToArray(), 0);
            }
            return ret;
        }

        public ulong[] toUInt64(byte[] data)
        {
            ulong[] ret = new ulong[data.Length / 8];
            byte[] word = new byte[8];

            for (int i = 0; i < ret.Length; i++)
            {
                Array.Copy(data, i * 8, word, 0, 8);
                ret[i] = BitConverter.ToUInt64(word.Reverse().ToArray(), 0);
            }
            return ret;
        }

        public float[] toFloat(byte[] data)
        {
            float[] ret = new float[data.Length / 4];
            byte[] word = new byte[4];

            for (int i = 0; i < ret.Length; i++)
            {
                Array.Copy(data, i * 4, word, 0, 4);
                ret[i] = BitConverter.ToSingle(word.Reverse().ToArray(), 0);
            }
            return ret;
        }

        public double[] toDoublet(byte[] data)
        {
            double[] ret = new double[data.Length / 8];
            byte[] word = new byte[8];

            for (int i = 0; i < ret.Length; i++)
            {
                Array.Copy(data, i * 8, word, 0, 8);
                ret[i] = BitConverter.ToDouble(word.Reverse().ToArray(), 0);
            }
            return ret;
        }

        public string toString(byte[] data)
        {
            string ret = Encoding.UTF8.GetString(data);

            return ret;
        }

    }
}
