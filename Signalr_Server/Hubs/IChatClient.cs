using System.Threading.Tasks;

namespace Signalr_Server.Hubs
{
    public interface IChatClient
    {
        Task StrongReceiveMessage(string user, string msg);
        Task StrongReceiveMessage(string msg);

    }
}
