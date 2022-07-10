using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalR.Server.Hubs;

namespace SignalR.Server
{
    internal class App
    {
        private readonly string[] _args;
        private WebApplicationBuilder _builder;
        private WebApplication _app;

        public App(string[] args)
        {
            _args = args;
            _builder = WebApplication.CreateBuilder(args);
        }

        public void Build()
        {
            Configure(_builder.Configuration);
            Configure(_builder.Logging);
            Configure(_builder.Services);

            _app = _builder.Build();

            Configure(_app);
        }

        public async Task RunAsync(CancellationToken token = default) =>
            await _app.RunAsync(token);

        private void Configure(WebApplication builder) =>
            builder.MapHub<ImportStatusHub>("");

        private void Configure(IServiceCollection services) =>
            services.AddSignalRCore();

        private void Configure(IConfigurationBuilder builder)
        {
            builder
                .AddCommandLine(_args)
                .AddEnvironmentVariables("NET_")
                .AddJsonFile("appsettings.json", true);
        }

        private void Configure(ILoggingBuilder builder) { }
    }
}
