using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Signalr_Server.Hubs
{
    /// <summary>
    /// 集线器
    /// </summary>
    public class ChatHub : Hub
    {
        /// <summary>
        /// 发送到所有
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendMessage(string user, string msg)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, msg);
        }
        /// <summary>
        /// 发送到分组
        /// </summary>
        /// <param name="group"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Task SendMessageToGroup(string group, string msg)
        {
            return Clients.Group(group).SendAsync("ReceiveMessage", msg);
        }
        public Task OnlineCount()
        {
            return Task.Run(() =>
            {
                var readom = new Random();
                while (true)
                {
                    Thread.Sleep(1000);
                    Clients.Caller.SendAsync("OnlineCount", readom.Next(100, 200));
                }
            });
        }
        public Task SendMessageToClient(string userCode, string msg)
        {
            return Clients.Client(userCode).SendAsync("ReceiveMessage", msg);
        }
    }
}
