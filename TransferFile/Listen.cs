﻿using System.Net;
using System.Net.Sockets;


namespace TransferFile
{
    public class Listen
    {
        private static Socket socket;
        private static bool IsStop;
        public static string AddressIP;
        //public delegate void ContainerItemEvent(UserFileSend userItem);
        //public static ContainerItemEvent AddContainerItem { get; set; }

        //Socket Functions
        public static void Start(int port)
        {
            if (socket != null)
                return;

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(new IPEndPoint(IPAddress.Parse(AddressIP), port));
                socket.Listen(1);
            }
            catch (Exception ex)
            {
                Stop();
                throw ex;
            }

            IsStop = false;

            new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        var clients = socket.Accept();

                        new Thread(() =>
                        {
                            var fileInfo = ReceiverEngine.GetFileInfo(clients);
                            //UserItem(fileInfo);
                            //AddContainerItem(temp);
                            fileInfo.LoadData();
                        }).Start();
                    }
                    catch (Exception ex)
                    {
                        if (!IsStop)
                            throw ex;
                        break;
                    }
                }
            }).Start();
        }

        //static UserFileSend item = new UserFileSend();
        //private static void UserItem(ReceiveFileInfo fileInfo)
        //{
        //    var dispatcher = Dispatcher.CurrentDispatcher;
        //    if (Application.Current != null)
        //        dispatcher = Application.Current.Dispatcher;
        //    if (dispatcher != null)
        //    {
        //        dispatcher.Invoke(() =>
        //        {
        //            var mainPanel = new MainPanel();
        //            item.Username = fileInfo.FileName;
        //            item.Address = System.AppDomain.CurrentDomain.BaseDirectory + @"Downloads\" + fileInfo.FileName;
        //            mainPanel.AddMessageToUi(item);
        //        });
        //    }
        //}

        //Stop the Socket
        public static void Stop()
        {
            IsStop = true;
            if (socket != null)
            {
                if (socket.Connected)
                    socket.Disconnect(false);

                socket.Close();
                socket.Dispose();
                socket = null;
            }
        }
    }
}
