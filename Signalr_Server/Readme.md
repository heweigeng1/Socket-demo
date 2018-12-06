# 控制台 Signalr 服务器端 demo(Console Signalr Server Demo)

## 安装与构筑

1. 首先当然是创建一个控制台程序,然后在nuget安装,下面的包

```c#
Microsoft.AspNetCore
Microsoft.AspNetCore.SignalR
```

2. 新增集线器,将消息发送到客户端

新建一个集线器类(继承于Hub的类)
```c#
    /// <summary>
    /// 集线器
    /// </summary>
    public class ChatHub : Hub
    {
        /// <summary>
        /// 发送到所有
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendMessage(string user, string msg)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, msg);
        }
        /// <summary>
        /// 发送到分组
        /// </summary>
        /// <param name="group"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Task SendMessageToGroup(string group, string msg)
        {
            return Clients.Group(group).SendAsync("ReceiveMessage", msg);
        }
        public Task OnlineCount()
        {
            return Task.Run(() =>
            {
                var readom = new Random();
                while (true)
                {
                    Thread.Sleep(1000);
                    Clients.Caller.SendAsync("OnlineCount", readom.Next(100, 200));
                }
            });
        }
        public Task SendMessageToClient(string userCode, string msg)
        {
            return Clients.Client(userCode).SendAsync("ReceiveMessage", msg);
        }
    }
}
```

* SendMessage 将消息发送到所有已连接客户端，使用Clients.All。
* SendMessageToClient 将消息发送回调用方，使用Clients.Caller。
* SendMessageToGroup 将消息发送到中的所有客户端分组。

这些方法都是调用客户端代理的函数ReceiveMessage。

除了上面的方法,我们还可以使用强类型集线器,先添加一个集线器接口 IChatClient

```c#
    public interface IChatClient
    {
        Task StrongReceiveMessage(string user, string msg);
        Task StrongReceiveMessage(string msg);
    }
```
再创建集线器类继承泛型Hub\<T>
```c#
    /// <summary>
    /// 强类型集线器
    /// </summary>
    public class StronglyTypedChatHub : Hub<IChatClient>
    {
        public async Task SendMessage(string user, string msg)
        {
            await Clients.All.StrongReceiveMessage(user, msg);
        }

        public async Task SendMessageToCaller(string msg)
        {
            await Clients.Caller.StrongReceiveMessage(msg);
        }
    }
```
使用强类型集线器(Hub\<T>)后就不能直接使用 SendAsync 了,而是使用接口 IChantClient 中定义的方法。

3. 新添一个Startup类

```c#
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
```

这里添加了SignalR的服务与中间件与两个集线器的路由。

4. 在系统入口修改Main函数。

```c#
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
```

到这一步，SignalR的服务器demo就完成了，后面我们接着去弄客户端。