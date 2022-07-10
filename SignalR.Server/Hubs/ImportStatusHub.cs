using Microsoft.AspNetCore.SignalR;

namespace SignalR.Server.Hubs
{
    internal class ImportStatusHub : Hub
    {
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await base.OnDisconnectedAsync(ex);
            throw ex;
        }

        public async Task SendMessage(string id, string message)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(message))
                await OnDisconnectedAsync(new Exception("Invalid parameters!"));
            else
                await ReceiveMessage(id, message);
        }

        public async Task ReceiveMessage(string id, string message) => await Clients.All.SendAsync(id, message);
    }
}
