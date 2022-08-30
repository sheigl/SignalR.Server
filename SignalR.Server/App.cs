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

        public App Build()
        {
            Configure(_builder.Configuration);
            Configure(_builder.Logging);
            Configure(_builder.Services);

            _app = _builder.Build();

            Configure(_app);

            return this;
        }

        public async Task RunAsync(CancellationToken token = default) =>
            await _app.RunAsync(token);

        private void Configure(WebApplication builder)
        {
            builder.UseCors("CorsPolicy");

            builder
                .MapHub<ImportStatusHub>("/import-status");
        }
            

        private void Configure(IServiceCollection services)
        {
            services
                .AddSignalR();

            services.AddCors(options =>
                {
                options.AddPolicy("CorsPolicy", builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }
            

        private void Configure(IConfigurationBuilder builder)
        {
            builder
                .AddCommandLine(_args)
                .AddEnvironmentVariables("NET_")
                .AddJsonFile("appsettings.json", true);
        }

        private void Configure(ILoggingBuilder builder) 
        {
            builder.ClearProviders();
            builder.AddConsole();
        }
    }
}
