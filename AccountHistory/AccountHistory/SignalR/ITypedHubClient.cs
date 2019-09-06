using System.Threading.Tasks;

namespace AccountHistory.Api.SignalR
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(string message);
    }
}
