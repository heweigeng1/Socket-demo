using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Signalr_Server.Hubs
{
    /// <summary>
    /// 强类型集线器
    /// </summary>
    public class StronglyTypedChatHub : Hub<IChatClient>
    {
        public async Task SendMessage(string user, string msg)
        {
            await Clients.All.StrongReceiveMessage(user, msg);
        }
        
        public async Task SendMessageToCaller(string msg)
        {
            await Clients.Caller.StrongReceiveMessage(msg);
        }
    }
}
