using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Signalr_Server
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
                .UseUrls(@"http://127.0.0.1:5998")
                .UseStartup<Startup>()
                .Build();
        }
    }
}
