using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Diagnostics;

namespace DataCollator
{
    public struct UdpState
    {
        public UdpClient u;
        public IPEndPoint e;
        public int id;
    }

    public class UDPListener
    {
        //private UdpClient _udpClient = null;
        public delegate void DataReceivedEventHandler(object sender, string data);
        public static event DataReceivedEventHandler DataReceived;
        public static List<UdpClient> _udpClients = new List<UdpClient>();

        public static void ReceiveCallback(IAsyncResult ar)
        {
            UdpState s = (UdpState)(ar.AsyncState);
            byte[] receiveBytes = null;
            try
            {
                receiveBytes = s.u.EndReceive(ar, ref s.e);
            }
            catch { }
            try
            {
                s.u.BeginReceive(new AsyncCallback(ReceiveCallback), s);
            }
            catch { }
            if (receiveBytes == null)
                return;

            DataReceived?.Invoke(s.id, Encoding.UTF8.GetString(receiveBytes));
        }

        public static void StartListening(int ListenPort)
        {
            // We create 10 UDP listeners
            for (int i=0; i<10; i++)
                CreateUdpClient(ListenPort, i);
        }

        private static void CreateUdpClient(int ListenPort, int id)
        {
            UdpState s = new UdpState();
            s.e = new IPEndPoint(IPAddress.Any, ListenPort);
            s.u = new UdpClient();
            s.id = id;
            s.u.Client.ExclusiveAddressUse = false;
            s.u.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            s.u.Client.Bind(new IPEndPoint(IPAddress.Any, ListenPort));

            Debug.WriteLine($"UDP{id}: Listening for messages on port {ListenPort}");
            s.u.BeginReceive(new AsyncCallback(ReceiveCallback), s);
            _udpClients.Add(s.u);
        }

        public static void StopListening()
        {
            while (_udpClients.Count>0)
            {
                try
                {
                    _udpClients[0].Close();
                    _udpClients[0].Dispose();
                }
                catch { }
                _udpClients.RemoveAt(0);
            }
        }
    }
}
