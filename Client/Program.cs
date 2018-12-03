using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
           
            Console.InputEncoding = Encoding.Unicode;
            int port = 5999;
            string host = "127.0.0.1";
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint iPEndPoint = new IPEndPoint(ip, port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Utils.Connect(socket, iPEndPoint);
            Utils.Send(socket, "我是好哥哥");
            Task.Run(() =>
            {
                while (true)
                {
                    string serverMsg = "";
                    byte[] bytes = new byte[4096];
                    int length = socket.Receive(bytes, bytes.Length, 0);
                    serverMsg += Encoding.Unicode.GetString(bytes, 0, length);
                    Console.WriteLine($"server:{serverMsg}");
                }
            });
            while (true)
            {
                string msg = Console.ReadLine();
                Utils.Send(socket, msg);
            }
        }
    }
}
