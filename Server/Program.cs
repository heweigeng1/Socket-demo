using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            var port = 5999;
            var ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(ip, port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Utils.Bind(socket, iPEndPoint);
            Utils.Listen(socket);
            Socket serverSocket = Utils.Accept(socket);
            Console.WriteLine("服务器连接成功");
            Task.Run(() =>
            {
                while (true)
                {
                    string clientMsg = "";
                    byte[] bytes = new byte[4096];
                    int length = serverSocket.Receive(bytes, bytes.Length, 0);
                    clientMsg += Encoding.Unicode.GetString(bytes, 0, length);
                    //在控制台输出ip@端口:msg
                    Console.WriteLine($"{((IPEndPoint)serverSocket.RemoteEndPoint).Address}@{((IPEndPoint)serverSocket.RemoteEndPoint).Port}:{clientMsg}");
                }
            });
            while (true)
            {
                string msg = Console.ReadLine();
                serverSocket.Send(Encoding.Unicode.GetBytes(msg));
            }
        }
    }
}
