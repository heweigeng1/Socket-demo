using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Signalr_Server.Hubs
{
    /// <summary>
    /// 强类型集线器
    /// </summary>
    public class StronglyTypedChatHub : Hub<IChatClient>
    {
        public async Task ReceiveMessage(string user, string msg)
        {
            await Clients.All.ReceiveMessage(user, msg);
        }

        public async Task ReceiveMessage(string msg)
        {
            await Clients.Caller.ReceiveMessage(msg);
        }
    }
}
