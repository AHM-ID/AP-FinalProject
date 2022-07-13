﻿using System.Net;
using System.Net.Sockets;

namespace Receiver
{
    public class CurrentSocket
    {
        public Socket Current { get; private set; }
        public IPAddress Address { get; private set; }
        public int Port { get; private set; }
        public IPEndPoint EndPoint { get; private set; }
        public CurrentSocket(Socket socket)
        {
            Current = socket;
            Address = ((IPEndPoint)Current.RemoteEndPoint).Address;
            Port = ((IPEndPoint)Current.RemoteEndPoint).Port;
            EndPoint = (IPEndPoint)Current.RemoteEndPoint;
        }
        public override string ToString()
        {
            return $"IPAddress : {Address}\nPort : {Port}";
        }
    }
}
