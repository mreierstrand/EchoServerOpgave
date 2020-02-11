using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace EchoServer
{
    static class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("192.168.24.205");
            TcpListener serverSocket = new TcpListener(ip, 6789);

            serverSocket.Start();
            Console.WriteLine("Server Started");

            TcpClient connectionSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine("Server Activated");

            DoClient(connectionSocket);

            serverSocket.Stop();

        }


        public static void DoClient(TcpClient socket)
        {
            Stream ns = socket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;
            string message = sr.ReadLine();
            string answer = "";

            while (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine("Client:" + message);
                answer = message.ToUpper();
                sw.WriteLine(answer);
                message = sr.ReadLine();
            }

            ns.Close();
            socket.Close();

        }



    }
}
