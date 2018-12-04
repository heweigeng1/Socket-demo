using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace WebSocket_Server
{
    public class Utils
    {
        private static Dictionary<string, WebClient> _webSocket = new Dictionary<string, WebClient>();
        public static async Task Acceptor(HttpContext  httpContext, Func<Task> next)
        {
            Console.WriteLine("轻轻的我来了");
        }
    }
}
