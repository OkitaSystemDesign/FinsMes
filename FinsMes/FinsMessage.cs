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
        private NetworkStream ns;

        private Int32 TargetPort = 9600;

        StringBuilder sbMes = new StringBuilder();

        public string Message
        {
            get
            {
                return sbMes.ToString();
            }
        }

        public FinsMessage(string IPAddress = "")
        {
            if(IPAddress!="")
            {
                Connect(IPAddress);
            }
        }

        ~FinsMessage()
        {

        }

        void IDisposable.Dispose()
        {
            if(tcp.Connected )
            {
                Close();
            }
        }


        public void Connect(string TargetIp)
        {
            MemoryStream ms = new MemoryStream();

            //TCPClient作成と接続
            tcp = new TcpClient();

            var task = tcp.ConnectAsync(TargetIp, TargetPort);
            if (!task.Wait(1000))
            {
                throw new SocketException(10060);
            }
        }

        public void Close()
        {
            sbMes.Clear();

            ns.Close();
            tcp.Close();

        }

    }
}
