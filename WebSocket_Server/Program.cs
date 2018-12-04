using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebSocket_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildWebHost(args).Run();

        }
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseUrls(@"http://127.0.0.1:5999")
                .UseStartup<Startup>()
                .Build();
        }
    }
}
