using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace WebSocket_Server
{
    public static class Utils
    {
        /// <summary>
        /// 用于发送到指定会员
        /// </summary>
        public static Dictionary<string, WebSocket> _webSocket = new Dictionary<string, WebSocket>();
        /// <summary>
        /// 分组(群发)多对多聊天
        /// </summary>
        public static Dictionary<string, List<WebSocket>> _groupSocket = new Dictionary<string, List<WebSocket>>();
        public static WebSocket Find(string key)
        {
            if (_webSocket.TryGetValue(key, out WebSocket webSocket))
            {
                return webSocket;
            }
            throw new System.Exception("no sigin");
        }
        public static List<WebSocket>FindGroup(string key)
        {
            if (_groupSocket.TryGetValue(key, out List<WebSocket> group))
            {
                return group;
            }
            throw new System.Exception("no group");
        }
    }
}
