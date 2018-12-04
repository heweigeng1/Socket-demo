using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocket_Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //var staticfile = new StaticFileOptions();
            //staticfile.FileProvider = new PhysicalFileProvider(@"D:\");
            //app.UseStaticFiles(staticfile);
            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        await Echo(context, webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }

            });

            //app.Use(Utils.Acceptor);
            //app.Run(new RequestDelegate(HttpRequestHandler));
        }

        private async Task HttpRequestHandler(HttpContext context)
        {
            await context.Response.WriteAsync("hello  here is songxingzhu prov.");
        }
        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            Console.InputEncoding = UnicodeEncoding.Unicode;
            Console.OutputEncoding = UnicodeEncoding.Unicode;
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                //await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
                var msgBuffer = new byte[1024 * 4];
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(msgBuffer), CancellationToken.None);
                string txt = Encoding.Unicode.GetString(msgBuffer, 0, result.Count);
                Console.WriteLine(txt);
                await webSocket.SendAsync(new ArraySegment<byte>(Encoding.Unicode.GetBytes($"收到:{txt}")), WebSocketMessageType.Text, true, CancellationToken.None);
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

    }
}
