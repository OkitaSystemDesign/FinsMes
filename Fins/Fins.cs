using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Osd.Omron
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

    public class CpuUnitInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public byte DipSwitch { get; set; }
        public ushort EMBankNum { get; set; }
        public ushort ProgramSize { get; set; }
        public ushort IOMSize { get; set; }
        public ushort DMSize { get; set; }
        public ushort TimSize { get; set; }
        public ushort EMSize { get; set; }
        public byte MemCardType { get; set; }
        public ushort MemCardSize { get; set; }
        public ushort[] UnitCode { get; set; }
        public ushort RemortIONum { get; set; }
        public ushort RackNum { get; set; }

        public CpuUnitInfo()
        {
            UnitCode = new ushort[16];
        }
    }

    public class Model
    {
        public string[] Name { get; set; }

        public Model()
        {
            Name = new string[25];
        }
    }

    public class CpuUnitStatus
    {
        public bool Run { get; set; }
        public bool FlashMemoryAccess { get; set; }
        public bool BatteryStatus { get; set; }
        public bool CpuStatus { get; set; }

        public byte Mode { get; set; }

        public bool FalsError { get; set; }
        public bool CycleTimeOver { get; set; }
        public byte CpuTimeOver { get; set; }
        public bool ProgramError { get; set; }
        public bool IOSettingError { get; set; }
        public bool IOPointOverflow { get; set; }
        public bool FatalInnerBoardError { get; set; }
        public bool DuplicationError { get; set; }
        public bool IOBusError { get; set; }
        public bool MemoryError { get; set; }

        public bool OtherNonFatalErrors { get; set; }
        public bool SpecialIOUnitSettingError { get; set; }
        public bool CpuBusUnitSettingError { get; set; }
        public bool BatteryError { get; set; }
        public bool SysbusError { get; set; }
        public bool SpecialIOUnitError { get; set; }
        public bool CpuBusUnitError { get; set; }
        public bool InnerBoardError { get; set; }
        public bool IOVerificationError { get; set; }
        public bool PlcSetupError { get; set; }
        public bool BasicIOUnitError { get; set; }
        public bool InterruptTaskError { get; set; }
        public bool DuplexError { get; set; }
        public bool FalError { get; set; }

        public ushort ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

    }

    public class CycleTime
    {
        public double Ave { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }
    }

    public class ErrorLogData
    {
        public ushort ErrorCode { get; set; }
        public ushort ErrorMessage { get; set; }
        public DateTime DT { get; set; }

    }

    public class ErrorLog
    {
        public ushort MaxRecordNo { get; set; }
        public ushort StoredRecordNo { get; set; }
        public ushort ReadRecordNum { get; set; }
        public string WriteRecordNum { get; set; }

        public ErrorLogData[] Data = new ErrorLogData[20];

    }

    public class Fins : IDisposable
    {
        private TcpClient tcp;
        private UdpClient udp;
        private NetworkStream ns;
        private Int32 TargetPort = 9600;
        static byte sid = 0;

        public byte[] ClientFinsAddress = new byte[] { 0, 0, 0 };
        public byte[] ServerFinsAddress = new byte[] { 0, 0, 0 };
        public bool useTcp = false;

        StringBuilder sbMes = new StringBuilder();


        public string MessageLog
        {
            get
            {
                return sbMes.ToString();
            }
        }

        public Fins()
        {

        }

        ~Fins()
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
            string Message = "FinsEndCode=";
            short Code = (short)(EndCode[0] * 0x100 + EndCode[1]);


            if (Code == 0x0101)
                Message += Code.ToString("X4") + ": Local node not in network (自ノード ネットワーク未加入)";
            else if (Code == 0x0102)
                Message += Code.ToString("X4") + ": Token timeout (トークン タイムアウト)";
            else if (Code == 0x0103)
                Message += Code.ToString("X4") + ": Retries failed (再送オーバー)";
            else if (Code == 0x0104)
                Message += Code.ToString("X4") + ": Too many send frames (送信許可フレーム数オーバー)";
            else if (Code == 0x0105)
                Message += Code.ToString("X4") + ": Node address range error (ノードアドレス設定範囲エラー)";
            else if (Code == 0x0106)
                Message += Code.ToString("X4") + ": Node address duplication (ノードアドレス二重設定エラー)";
            else if (Code == 0x0201)
                Message += Code.ToString("X4") + ": Destination node not in network (相手ノード ネットワーク未加入)";
            else if (Code == 0x0202)
                Message += Code.ToString("X4") + ": Unit missing (該当ユニットなし)";
            else if (Code == 0x0203)
                Message += Code.ToString("X4") + ": Third node missing (第三ノード ネットワーク未加入)";
            else if (Code == 0x0204)
                Message += Code.ToString("X4") + ": Destination node busy (相手ノード ビジー)";
            else if (Code == 0x0205)
                Message += Code.ToString("X4") + ": Response timeout (レスポンス タイムアウト)";
            else if (Code == 0x0301)
                Message += Code.ToString("X4") + ": Communications controller error (通信コントローラ異常)";
            else if (Code == 0x0302)
                Message += Code.ToString("X4") + ": CPU Unit error (CPUユニット異常)";
            else if (Code == 0x0303)
                Message += Code.ToString("X4") + ": Controller error (該当コントローラ異常)";
            else if (Code == 0x0304)
                Message += Code.ToString("X4") + ": Unit number error (ユニット番号設定異常)";
            else if (Code == 0x0401)
                Message += Code.ToString("X4") + ": Undefined command (未定義コマンド)";
            else if (Code == 0x0402)
                Message += Code.ToString("X4") + ": Not supported by model/version (サポート外機種/バージョン)";
            else if (Code == 0x0501)
                Message += Code.ToString("X4") + ": Destination address setting error (相手アドレス設定エラー)";
            else if (Code == 0x0502)
                Message += Code.ToString("X4") + ": No routing tables (ルーチングテーブル未登録)";
            else if (Code == 0x0503)
                Message += Code.ToString("X4") + ": Routing table error (ルーチングテーブル異常)";
            else if (Code == 0x0504)
                Message += Code.ToString("X4") + ": oo many relays (中継回数オーバー)";
            else if (Code == 0x1001)
                Message += Code.ToString("X4") + ": Command too long (コマンド長オーバー)";
            else if (Code == 0x1002)
                Message += Code.ToString("X4") + ": Command too short (コマンド長不足)";
            else if (Code == 0x1003)
                Message += Code.ToString("X4") + ": Elements/data don’t match (要素数/データ数不一致)";
            else if (Code == 0x1004)
                Message += Code.ToString("X4") + ": Command format error (コマンドフォーマットエラー)";
            else if (Code == 0x1005)
                Message += Code.ToString("X4") + ": Header error (ヘッダ異常)";
            else if (Code == 0x1101)
                Message += Code.ToString("X4") + ": Area classification missing (エリア種別なし)";
            else if (Code == 0x1102)
                Message += Code.ToString("X4") + ": Access size error (アクセスサイズエラー)";
            else if (Code == 0x1103)
                Message += Code.ToString("X4") + ": Address range error (アドレス範囲外指定エラー)";
            else if (Code == 0x1104)
                Message += Code.ToString("X4") + ": Address range exceeded (アドレス範囲オーバー)";
            else if (Code == 0x1106)
                Message += Code.ToString("X4") + ": Program missing (該当プログラム番号なし)";
            else if (Code == 0x1109)
                Message += Code.ToString("X4") + ": Relational error (相関関係エラー)";
            else if (Code == 0x110A)
                Message += Code.ToString("X4") + ": Duplicate data access (データ重複エラー)";
            else if (Code == 0x110B)
                Message += Code.ToString("X4") + ": Response too long (レスポンス長オーバー)";
            else if (Code == 0x110C)
                Message += Code.ToString("X4") + ": Parameter error (パラメータエラー)";
            else if (Code == 0x2002)
                Message += Code.ToString("X4") + ": Protected (プロテクト中)";
            else if (Code == 0x2003)
                Message += Code.ToString("X4") + ": Table missing (登録テーブルなし)";
            else if (Code == 0x2004)
                Message += Code.ToString("X4") + ": Data missing (検索データなし)";
            else if (Code == 0x2005)
                Message += Code.ToString("X4") + ": Program missing (該当プログラム番号なし)";
            else if (Code == 0x2006)
                Message += Code.ToString("X4") + ": File missing (該当ファイルなし)";
            else if (Code == 0x2007)
                Message += Code.ToString("X4") + ": Data mismatch (照合異常)";
            else if (Code == 0x2101)
                Message += Code.ToString("X4") + ": Read-only (リードオンリー)";
            else if (Code == 0x2102)
                Message += Code.ToString("X4") + ": Protected (プロテクト中)";
            else if (Code == 0x2103)
                Message += Code.ToString("X4") + ": Cannot register (登録不可)";
            else if (Code == 0x2105)
                Message += Code.ToString("X4") + ": Program missing (該当プログラム番号なし)";
            else if (Code == 0x2106)
                Message += Code.ToString("X4") + ": File missing (該当ファイルなし)";
            else if (Code == 0x2107)
                Message += Code.ToString("X4") + ": File name already exists (同一ファイル名あり)";
            else if (Code == 0x2108)
                Message += Code.ToString("X4") + ": Cannot change (変更不可)";
            else if (Code == 0x2201)
                Message += Code.ToString("X4") + ": Not possible during execution (運転中のため動作不可)";
            else if (Code == 0x2202)
                Message += Code.ToString("X4") + ": Not possible while running (停止中)";
            else if (Code == 0x2203)
                Message += Code.ToString("X4") + ": Wrong PLC mode, PROGRAM mode (本体モードが違う プログラムモード)";
            else if (Code == 0x2204)
                Message += Code.ToString("X4") + ": Wrong PLC mode, DEBUG mode (本体モードが違う デバッグモード)";
            else if (Code == 0x2205)
                Message += Code.ToString("X4") + ": Wrong PLC mode, MONITOR mode (本体モードが違う モニタモード)";
            else if (Code == 0x2206)
                Message += Code.ToString("X4") + ": Wrong PLC mode, RUN mode (本体モードが違う 運転モード)";
            else if (Code == 0x2207)
                Message += Code.ToString("X4") + ": Specified node not polling node (指定ノードが管理局でない)";
            else if (Code == 0x2208)
                Message += Code.ToString("X4") + ": Step cannot be executed (ステップが実行不可)";
            else if (Code == 0x2301)
                Message += Code.ToString("X4") + ": File device missing (ファイル装置なし)";
            else if (Code == 0x2302)
                Message += Code.ToString("X4") + ": Memory missing (該当メモリなし)";
            else if (Code == 0x2303)
                Message += Code.ToString("X4") + ": Clock missing (時計なし)";
            else if (Code == 0x2401)
                Message += Code.ToString("X4") + ": Table missing (登録テーブルなし)";
            else if (Code == 0x2502)
                Message += Code.ToString("X4") + ": Memory error (メモリ異常)";
            else if (Code == 0x2503)
                Message += Code.ToString("X4") + ": I/O setting nerror (I/O 設定異常)";
            else if (Code == 0x2504)
                Message += Code.ToString("X4") + ": Too many I/O points (I/O 点数オーバー)";
            else if (Code == 0x2505)
                Message += Code.ToString("X4") + ": CPU bus error (CPU バス異常)";
            else if (Code == 0x2506)
                Message += Code.ToString("X4") + ": I/O duplication (I/O 二重エラー)";
            else if (Code == 0x2507)
                Message += Code.ToString("X4") + ": I/O bus error (I/O バス異常)";
            else if (Code == 0x2509)
                Message += Code.ToString("X4") + ": SYSMAC BUS/2 error (SYSMAC BUS/2 異常)";
            else if (Code == 0x250A)
                Message += Code.ToString("X4") + ": CPU Bus Unit error (高機能ユニット異常)";
            else if (Code == 0x250D)
                Message += Code.ToString("X4") + ": SYSMAC BUS No. duplication (SYSBUS No. 二重使用)";
            else if (Code == 0x250F)
                Message += Code.ToString("X4") + ": Memory error (メモリ異常)";
            else if (Code == 0x2510)
                Message += Code.ToString("X4") + ": SYSMAC BUS terminator missing (SYSBUS エンド局なし)";
            else if (Code == 0x2601)
                Message += Code.ToString("X4") + ": No protection (プロテクト解除中)";
            else if (Code == 0x2602)
                Message += Code.ToString("X4") + ": Incorrect password (パスワード不一致)";
            else if (Code == 0x2604)
                Message += Code.ToString("X4") + ": Protected (プロテクト中)";
            else if (Code == 0x2605)
                Message += Code.ToString("X4") + ": Service already executing (サービス実行中)";
            else if (Code == 0x2606)
                Message += Code.ToString("X4") + ": Service stopped (サービス停止中)";
            else if (Code == 0x2607)
                Message += Code.ToString("X4") + ": No execution right (実行権なし)";
            else if (Code == 0x2608)
                Message += Code.ToString("X4") + ": Settings not complete (環境未設定)";
            else if (Code == 0x2609)
                Message += Code.ToString("X4") + ": Necessary items not set (必要項目未設定)";
            else if (Code == 0x260A)
                Message += Code.ToString("X4") + ": Number already defined (指定番号定義済み)";
            else if (Code == 0x260B)
                Message += Code.ToString("X4") + ": No protection (異常解除不可)";
            else if (Code == 0x3001)
                Message += Code.ToString("X4") + ": No access right (アクセス権なし)";
            else if (Code == 0x4001)
                Message += Code.ToString("X4") + ": Service aborted (サービス中断)";
            else
                Message += Code.ToString("X4");

            return Message;
        }

        public byte[] Connect(string TargetIp, bool TcpConnect = false)
        {
            MemoryStream ms = new MemoryStream();
            useTcp = TcpConnect;
            byte[] node = new byte[] { ClientFinsAddress[1], ServerFinsAddress[1] };

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

                node = GetServerFinsNodeCmd();
                ClientFinsAddress[1] = node[0];
                ServerFinsAddress[1] = node[1];

            }
            else
            {
                udp = new UdpClient();
                udp.Client.ReceiveTimeout = 1000;

                udp.Connect(TargetIp, TargetPort);
            }

            return node;

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

        private byte[] GetServerFinsNodeCmd()
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

        private byte[] GetFinsHeader()
        {
            byte[] cmd = new byte[10];

            // ----------------- FINSヘッダ
            cmd[0] = 0x80;          // ICF
            cmd[1] = 0x00;          // RSV
            cmd[2] = 0x02;          // GCT
            cmd[3] = ServerFinsAddress[0];      // DNA  相手先ネットワークアドレス
            cmd[4] = ServerFinsAddress[1];      // DA1  相手先ノードアドレス
            cmd[5] = ServerFinsAddress[2];      // DA2  相手先号機アドレス
            cmd[6] = ClientFinsAddress[0];      // SNA  発信元ネットワークアドレス
            cmd[7] = ClientFinsAddress[1];      // SA1  発信元ノードアドレス
            cmd[8] = ClientFinsAddress[2];      // SA2  発信元号機アドレス
            cmd[9] = (byte)++sid;        // SID  識別子 00-FFの任意の数値

            return cmd;
        }

        private byte[] GetFinsTcpHeader(int CommandLength)
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

        public byte[] MemOffset(string memadrstr, short offset)
        {
            string memtype = memadrstr.Substring(0, 1);
            short memadr = 0;
            byte[] adr = new byte[4];

            switch (memtype.ToUpper())
            {
                case "D":
                    adr[0] = 0x82;
                    memadr = (short)(int.Parse(memadrstr.Substring(1)) + offset);
                    Array.Copy(BitConverter.GetBytes(memadr).Reverse().ToArray(), 0, adr, 1, 2);
                    break;

                case "E":
                    string[] strs = memadrstr.Split('_');
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
                    memadr = (short)(int.Parse(memadrstr.Substring(1)) + offset);
                    Array.Copy(BitConverter.GetBytes(memadr).Reverse().ToArray(), 0, adr, 1, 2);
                    break;

                case "H":
                    adr[0] = 0xB2;
                    memadr = (short)(int.Parse(memadrstr.Substring(1)) + offset);
                    Array.Copy(BitConverter.GetBytes(memadr).Reverse().ToArray(), 0, adr, 1, 2);
                    break;

                case "A":
                    adr[0] = 0xB3;
                    memadr = (short)(int.Parse(memadrstr.Substring(1)) + offset);
                    Array.Copy(BitConverter.GetBytes(memadr).Reverse().ToArray(), 0, adr, 1, 2);
                    break;

                default:
                    int cioadr;
                    if (int.TryParse(memadrstr, out cioadr))
                    {
                        adr[0] = 0xB0;
                        Array.Copy(BitConverter.GetBytes((short)(cioadr + offset)).Reverse().ToArray(), 0, adr, 1, 2);
                    }
                    break;
            }

            return adr;
        }

        public byte[] read(string memadrstr, int readsize)
        {
            byte[] finscmd = new byte[8];

            int readnum = readsize / 990;
            int remainder = readsize % 990;
            byte[] data = new byte[readsize * 2];

            sbMes.Clear();

            for (int cnt = 0; cnt < readnum + 1; cnt++)
            {
                byte[] MemAddressArray = MemOffset(memadrstr, (short)(cnt * 990));
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

        public void write(string memadrstr, byte[] data)
        {
            if (data.Length % 2 == 1)
                throw new Exception($"Write data must be an even number of bytes");

            int WriteWordSize = data.Length / 2;

            int writenum = WriteWordSize / 990;
            int remainder = WriteWordSize % 990;

            sbMes.Clear();

            for (int cnt = 0; cnt < writenum + 1; cnt++)
            {
                byte[] MemAddressArray = MemOffset(memadrstr, (short)(cnt * 990));
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

        public void fill(string memadrstr, int size, byte[] data)
        {
            byte[] MemAddressArray = MemOffset(memadrstr, 0);
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

            byte[] finscmd = new byte[2];
            finscmd[0] = 0x05;
            finscmd[1] = 0x01;
            //finscmd[2] = 0x00;

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

        public byte[] ReadConnectionData(byte address, byte size)
        {
            sbMes.Clear();

            byte[] finscmd = new byte[4];
            finscmd[0] = 0x05;
            finscmd[1] = 0x02;
            finscmd[2] = address;
            finscmd[3] = size;

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

        public void ErrorClear(byte[] code)
        {
            sbMes.Clear();

            byte[] finscmd = new byte[4];
            finscmd[0] = 0x21;
            finscmd[1] = 0x01;
            finscmd[2] = code[0];
            finscmd[3] = code[1];

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

        public byte[] ErrorLogRead(int startIndex, int count)
        {
            sbMes.Clear();
            byte[] startidx = BitConverter.GetBytes((short)startIndex).Reverse().ToArray();
            byte[] cnt = BitConverter.GetBytes((short)count).Reverse().ToArray();

            byte[] finscmd = new byte[6];
            finscmd[0] = 0x21;
            finscmd[1] = 0x02;
            finscmd[2] = startidx[0];
            finscmd[3] = startidx[1];
            finscmd[4] = cnt[0];
            finscmd[5] = cnt[1];

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

        public BitArray toBitArray(byte[] data)
        {
            BitArray bits = new BitArray(data.Reverse().ToArray());
            return bits;
        }

        public bool[] toBoolArray(byte[] data)
        {
            bool[] Bools = new bool[data.Length * 8];
            BitArray bits = new BitArray(data.Reverse().ToArray());

            int bytesize = data.Length * 8;
            for (int i = 0; i < bytesize; i++)
            {
                Bools[i] = bits[(bytesize - 1) - i];
                ;
            }
            return Bools;
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
                Array.Copy(data, i * 4 + 2, word, 0, 2);
                Array.Copy(data, i * 4, word, 2, 2);
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
                Array.Copy(data, i * 8 + 6, word, 0, 2);
                Array.Copy(data, i * 8 + 4, word, 2, 2);
                Array.Copy(data, i * 8 + 2, word, 4, 2);
                Array.Copy(data, i * 8, word, 6, 2);
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
                Array.Copy(data, i * 4 + 2, word, 0, 2);
                Array.Copy(data, i * 4, word, 2, 2);
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
                Array.Copy(data, i * 8 + 6, word, 0, 2);
                Array.Copy(data, i * 8 + 4, word, 2, 2);
                Array.Copy(data, i * 8 + 2, word, 4, 2);
                Array.Copy(data, i * 8, word, 6, 2);
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
                Array.Copy(data, i * 4 + 2, word, 0, 2);
                Array.Copy(data, i * 4, word, 2, 2);
                ret[i] = BitConverter.ToSingle(word.Reverse().ToArray(), 0);
            }
            return ret;
        }

        public double[] toDouble(byte[] data)
        {
            double[] ret = new double[data.Length / 8];
            byte[] word = new byte[8];

            for (int i = 0; i < ret.Length; i++)
            {
                Array.Copy(data, i * 8 + 6, word, 0, 2);
                Array.Copy(data, i * 8 + 4, word, 2, 2);
                Array.Copy(data, i * 8 + 2, word, 4, 2);
                Array.Copy(data, i * 8, word, 6, 2);
                ret[i] = BitConverter.ToDouble(word.Reverse().ToArray(), 0);
            }
            return ret;
        }

        public string toString(byte[] data)
        {
            string ret = Encoding.UTF8.GetString(data);

            return ret;
        }


        public CpuUnitInfo SetCpuUnitInfo(byte[] res)
        {
            CpuUnitInfo info = new CpuUnitInfo();


            byte[] name = new byte[20];
            Array.Copy(res, name, 20);
            info.Name = Encoding.UTF8.GetString(name).Trim();

            byte[] ver = new byte[20];
            Array.Copy(res, 20, ver, 0, 20);
            info.Version = Encoding.UTF8.GetString(ver).Trim();

            info.DipSwitch = res[40];                           // DipSwitch

            info.EMBankNum = BitConverter.ToUInt16(res, 41);    // EM Bank

            byte[] tmpEndian = new byte[2];
            tmpEndian[0] = res[81];
            tmpEndian[1] = res[80];
            info.ProgramSize = BitConverter.ToUInt16(tmpEndian, 0); // ProgramSize

            info.IOMSize = (ushort)res[82];                     // IOM

            tmpEndian[0] = res[84];
            tmpEndian[1] = res[83];
            info.DMSize = BitConverter.ToUInt16(tmpEndian, 0);  // DM CH Size

            info.TimSize = (ushort)res[85];                     // Timer Size
            info.EMSize = (ushort)res[86];                      // EM Size
            info.MemCardType = res[89];                         // MemCard Type

            tmpEndian[0] = res[91];
            tmpEndian[1] = res[90];
            info.MemCardSize = BitConverter.ToUInt16(tmpEndian, 0);     // MemCard Size

            for (int i = 0; i < 16; i++)
            {
                tmpEndian[0] = res[92 + i + 1];
                tmpEndian[1] = res[92 + i];
                info.UnitCode[i] = BitConverter.ToUInt16(tmpEndian, 0);     // 0-15号機 ユニットID
            }

            info.RemortIONum = (ushort)res[156];
            info.RackNum = (ushort)res[157];

            return info;
        }

        public Model SetConnectionData(byte[] res)
        {
            Model model = new Model();

            // 要素数
            int UnitNum = (int)(res[0] & 0x7F);

            // 形式
            byte[] UnitNameArray = new byte[20];

            for (int i = 0; i < UnitNum; i++)
            {
                int UnitNo = (int)(res[1 + i * 21] - 16);
                Array.Copy(res, 1 + i * 21 + 1, UnitNameArray, 0, 20);
                model.Name[UnitNo] = Encoding.UTF8.GetString(UnitNameArray).Trim();
            }

            return model;
        }

        public CpuUnitStatus SetCpuUnitStatus(byte[] res)
        {
            CpuUnitStatus status = new CpuUnitStatus();

            status.Run = (res[0] & 0b00000001) != 0;
            status.FlashMemoryAccess = (res[0] & 0b00000010) != 0;
            status.BatteryStatus = (res[0] & 0b00000100) != 0;
            status.CpuStatus = (res[0] & 0b10000000) != 0;

            status.Mode = res[1];

            status.FalsError = (res[3] & 0b01000000) != 0;

            status.CycleTimeOver = (res[2] & 0b00000001) != 0;
            status.ProgramError = (res[2] & 0b00000010) != 0;
            status.IOSettingError = (res[2] & 0b00000100) != 0;
            status.IOPointOverflow = (res[2] & 0b00001000) != 0;
            status.FatalInnerBoardError = (res[2] & 0b00010000) != 0;
            status.DuplicationError = (res[2] & 0b00100000) != 0;
            status.IOBusError = (res[2] & 0b01000000) != 0;
            status.MemoryError = (res[2] & 0b10000000) != 0;

            status.OtherNonFatalErrors = (res[5] & 0b00000001) != 0;
            status.SpecialIOUnitSettingError = (res[5] & 0b00000100) != 0;
            status.CpuBusUnitSettingError = (res[5] & 0b00001000) != 0;
            status.BatteryError = (res[5] & 0b00010000) != 0;
            status.SysbusError = (res[5] & 0b00100000) != 0;
            status.SpecialIOUnitError = (res[5] & 0b01000000) != 0;
            status.CpuBusUnitError = (res[5] & 0b10000000) != 0;

            status.InnerBoardError = (res[4] & 0b00000001) != 0;
            status.IOVerificationError = (res[4] & 0b00000010) != 0;
            status.PlcSetupError = (res[4] & 0b00000100) != 0;
            status.BasicIOUnitError = (res[4] & 0b00010000) != 0;
            status.InterruptTaskError = (res[4] & 0b00100000) != 0;
            status.DuplexError = (res[4] & 0b01000000) != 0;
            status.FalError = (res[4] & 0b10000000) != 0;

            status.ErrorCode = BitConverter.ToUInt16(res, 8);

            byte[] msg = new byte[16];
            Array.Copy(res, 10, msg, 0, 16);
            status.ErrorMessage = Encoding.UTF8.GetString(msg).Trim();

            return status;
        }

        public CycleTime SetCycleTime(byte[] res)
        {
            CycleTime ct = new CycleTime();

            byte[] tmpArray = new byte[4];

            Array.Copy(res, 0, tmpArray, 0, 4);
            Array.Reverse(tmpArray);
            ct.Ave = (double)BitConverter.ToUInt32(tmpArray, 0) * 0.1;

            Array.Copy(res, 4, tmpArray, 0, 4);
            Array.Reverse(tmpArray);
            ct.Max = (double)BitConverter.ToUInt32(tmpArray, 0) * 0.1;

            Array.Copy(res, 8, tmpArray, 0, 4);
            Array.Reverse(tmpArray);
            ct.Min = (double)BitConverter.ToUInt32(tmpArray, 0) * 0.1;

            return ct;
        }

        public ErrorLog SetErrorLog(byte[] res)
        {
            ErrorLog log = new ErrorLog();

            byte[] tmpArray = new byte[2];
            Array.Copy(res, 0, tmpArray, 0, 2);
            Array.Reverse(tmpArray);
            log.MaxRecordNo = BitConverter.ToUInt16(tmpArray, 0);

            Array.Copy(res, 2, tmpArray, 0, 2);
            Array.Reverse(tmpArray);
            log.StoredRecordNo = BitConverter.ToUInt16(tmpArray, 0);

            Array.Copy(res, 4, tmpArray, 0, 2);
            Array.Reverse(tmpArray);
            log.ReadRecordNum = BitConverter.ToUInt16(tmpArray, 0);

            for (int i = 0; i < 20; i++)
            {
                log.Data[i] = new ErrorLogData();
            }

            if (log.ReadRecordNum > 0)
            {
                for (int i = 0; i < log.ReadRecordNum; i++)
                {
                    byte[] tmpData = new byte[10];
                    Array.Copy(res, 6 + i * 10, tmpData, 0, 10);

                    Array.Copy(tmpData, 0, tmpArray, 0, 2);
                    Array.Reverse(tmpArray);
                    log.Data[i].ErrorCode = BitConverter.ToUInt16(tmpArray, 0);

                    Array.Copy(tmpData, 2, tmpArray, 0, 2);
                    Array.Reverse(tmpArray);
                    log.Data[i].ErrorMessage = BitConverter.ToUInt16(tmpArray, 0);

                    string dt = tmpData[8].ToString("X2") + "/"
                                + tmpData[9].ToString("X2") + "/"
                                + tmpData[6].ToString("X2") + " "
                                + tmpData[7].ToString("X2") + ":"
                                + tmpData[4].ToString("X2") + ":"
                                + tmpData[5].ToString("X2");

                    log.Data[i].DT = DateTime.Parse(dt);

                }
            }

            return log;
        }
    }
}
