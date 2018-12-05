using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocket_Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var webSocket = new ClientWebSocket();
            Console.WriteLine("my websocket!!!");
            var socket = ConnectAsync("ws://127.0.0.1:5999/ws").Result;

            //webSocket.ConnectAsync(new Uri(@"ws://127.0.0.1:5999/ws"), new CancellationToken()).Wait();
            Task.Run(() =>
            {
                var buffer = new byte[1024 * 4];
                var result = socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None).Result;
                string txt = Encoding.Unicode.GetString(buffer, 0, result.Count);
                Console.WriteLine(txt);
            });
            while (true)
            {
                string msg = Console.ReadLine();
                //var bytes = Encoding.Unicode.GetBytes(msg);
                socket.SendAsync(new ArraySegment<byte>(Encoding.Unicode.GetBytes(msg)), WebSocketMessageType.Text, true, CancellationToken.None).Wait();
                //var bytes = Encoding.UTF8.GetBytes(@"hi boy");//发送远程调用 log方法
            }
        }

        private static async Task<ClientWebSocket> ConnectAsync(string BaseUrl)

        {

            ClientWebSocket client = new ClientWebSocket();
            client.Options.AddSubProtocol("protocol1");
            client.Options.SetRequestHeader("test", "aaaaaaa");
            await client.ConnectAsync(new Uri(BaseUrl), CancellationToken.None);

            Console.WriteLine("Connect success");
            await client.SendAsync(new ArraySegment<byte>(Encoding.Unicode.GetBytes(@"{""protocol"":""json"", ""version"":1}")), WebSocketMessageType.Text, true, CancellationToken.None);//发送握手包

            Console.WriteLine("Send success");

            return client;
            //var buffer = new ArraySegment<byte>(new byte[1024 * 4]);
            //client.ReceiveAsync(buffer, CancellationToken.None).Wait();
            //Console.WriteLine(Encoding.Unicode.GetString(RemoveSeparator(buffer.ToArray())));
            //while (true)
            //{
            //    string msg = Console.ReadLine();
            //    var bytes = Encoding.Unicode.GetBytes(msg);
            //    await client.SendAsync(new ArraySegment<byte>(AddSeparator(bytes)), WebSocketMessageType.Text, true, CancellationToken.None);
            //    //var bytes = Encoding.UTF8.GetBytes(@"hi boy");//发送远程调用 log方法
            //}


            //while (true)

            //{

            //    await client.ReceiveAsync(buffer, CancellationToken.None);

            //    Console.WriteLine(Encoding.UTF8.GetString(RemoveSeparator(buffer.ToArray())));

            //}

        }
    }
}
