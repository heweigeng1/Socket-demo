using System.Threading.Tasks;

namespace Signalr_Server.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string msg);
        Task ReceiveMessage(string msg);

    }
}
