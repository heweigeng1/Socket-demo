using System.Net;
using System.Net.Sockets;

namespace WebSocket_Server
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public IPEndPoint IP { get; set; }
        public Socket ClientSocket { get; set; }
    }
}
