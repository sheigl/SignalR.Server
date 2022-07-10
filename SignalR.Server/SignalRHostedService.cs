using Microsoft.Extensions.Hosting;

namespace SignalR.Server
{
    internal class SignalRHostedService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
        }
    }
}
