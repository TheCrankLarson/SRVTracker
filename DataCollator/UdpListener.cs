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
    }

    public class UDPListener
    {
        //private UdpClient _udpClient = null;
        public delegate void DataReceivedEventHandler(object sender, string data);
        public static event DataReceivedEventHandler DataReceived;

        public static void ReceiveCallback(IAsyncResult ar)
        {
            UdpState s = (UdpState)(ar.AsyncState);

            try
            {
                byte[] receiveBytes = s.u.EndReceive(ar, ref s.e);
                string receiveString = Encoding.ASCII.GetString(receiveBytes);
                DataReceived?.Invoke(null, receiveString);
            }
            catch { }
            s.u.BeginReceive(new AsyncCallback(ReceiveCallback), s);
        }

        public static void StartListening(int ListenPort = 11938)
        {
            // Receive a message and write it to the console.
            UdpState s = new UdpState();
            s.e = new IPEndPoint(IPAddress.Any, 0);
            s.u = new UdpClient();
            s.u.Client.ExclusiveAddressUse = false;
            s.u.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            s.u.Client.Bind(new IPEndPoint(IPAddress.Any, ListenPort));

            Debug.WriteLine($"Listening for messages on port {ListenPort}");
            s.u.BeginReceive(new AsyncCallback(ReceiveCallback), s);
        }

    }
}
