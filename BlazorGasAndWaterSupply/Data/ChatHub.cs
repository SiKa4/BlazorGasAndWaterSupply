using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;

namespace BlazorGasAndWaterSupply.Data
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string _idReceiver, string _idSending)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, _idReceiver, _idSending);
        }
    }
}
