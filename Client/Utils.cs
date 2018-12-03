using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public static class Utils
    {
        /// <summary>
        /// 连接服务器
        /// </summary>
        public static void Connect(this Socket socket,IPEndPoint iPEndPoint)
        {
            socket.Connect(iPEndPoint);
        }
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="msg"></param>
        public static void Send(this Socket socket, string msg)
        {
            socket.Send(Encoding.Unicode.GetBytes(msg));
        }
        /// <summary>
        /// 读取信息
        /// </summary>
        public static void Read()
        {

        }
        /// <summary>
        /// 结束连接
        /// </summary>
        public static void Close()
        {

        }
    }
}
