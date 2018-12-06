# Signalr 控制台客户端

在本demo将简单做一个连接signalr服务的客户端，可以发送信息到服务器，或者取在线人数（随机数）的方法，简单的学习一下signalr client的使用。

## 引用c# Signalr client包

新添一个控制台应用程序后，在nuget包管理工具，安装
```c#
Microsoft.AapNetCore.SignalR.Client
```

因为这只是一个简单的demo 所以直接在Main函数里写了

```c#
        private static void Main(string[] args)
        {
            Console.WriteLine("退出请按Ctrl+C");
            Console.WriteLine("开始连接服务器");
            //构筑服务器集线器连接
            var connection = new HubConnectionBuilder().WithUrl(@"http://127.0.0.1:5998/chathub").Build();
            //连接到服务器
            connection.StartAsync().Wait();
            Console.WriteLine("连接服务器成功");
            //绑定客户端代理函数,让服务器调用
            BuildClientFunction(connection);
            Console.WriteLine("绑定客户端代理方法");
            //控制台的命令
            while (true)
            {
                Console.Write("请输入命令：");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "在线人数":
                        connection.InvokeAsync("OnlineCount").Wait();//调用服务器集线器中的在线人数方法
                        break;
                    case "群发":
                        Console.Write("请输入信息：");
                        string msg = Console.ReadLine();
                        connection.InvokeAsync("SendMessage", "客户a：", msg).Wait();//调用集线器中发送信息方法
                        break;
                    default:
                        Console.WriteLine("命令不存在");
                        break;
                }
            }
        }
        public static void BuildClientFunction(HubConnection hubConnection)
        {
            hubConnection.On("ReceiveMessage", (string user, string msg) =>
            {
                System.Console.WriteLine(user + ":" + msg);
            });
            hubConnection.On("ReceiveMessageA", (string msg) =>
            {
                System.Console.WriteLine(msg);
            });
            hubConnection.On("OnlineCount", (int count) =>
            {
                Console.WriteLine(count);
            });
        }
    }
```

当然你必须先执行 Signalr_Server 服务器端，再执行Signalr_Client。