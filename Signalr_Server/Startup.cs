using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Signalr_Server.Hubs;

namespace Signalr_Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSignalR(route =>
            {
                route.MapHub<ChatHub>("/chathub");
                route.MapHub<StronglyTypedChatHub>("/stronglyhub");
            });
        }
    }
}
