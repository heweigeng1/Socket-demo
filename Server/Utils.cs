using System.Net;
using System.Net.Sockets;

namespace Server
{
    public static class Utils
    {
        public static void Bind(this Socket socket, IPEndPoint iPEndPoint)
        {
            socket.Bind(iPEndPoint);
        }
        public static void Listen(this Socket socket)
        {
            socket.Listen(0);
            System.Console.WriteLine("开始监听");
        }
        public static Socket Accept(this Socket socket)
        {
            return socket.Accept();
        }
        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="tcpListener"></param>
        /// <param name="iPEndPoint"></param>
        public static TcpListener TcpListener(IPEndPoint iPEndPoint)
        {
            return new TcpListener(iPEndPoint);
        }
    }
}
