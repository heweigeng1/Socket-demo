using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using Microsoft.AspNet.SignalR.Client;

namespace AbpSignalr_Client
{
    public partial class Form1 : Form
    {
        //定义代理,广播服务连接相关
        private static IHubProxy HubProxy { get; set; }
        //private static readonly string ServerUrl = ConfigurationManager.AppSettings["signalrServer"];
        //private static string _serverUrl ="";
        //定义一个连接对象
        public static HubConnection Connection { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            var url = this.UrlTxt.Text;
            var tenant = this.tenantCodeTxt.Text.Split(',');
            Console.WriteLine("连接服务器成功");
            if (!string.IsNullOrEmpty(url))
            {
                var Connection = new HubConnection(url);
                HubProxy = Connection.CreateHubProxy("orderpaycall");
                try
                {
                    Connection.Start().Wait();
                    var sound = new SoundPlayer
                    {
                        SoundLocation = @"C:\Windows\media\Alarm06.wav"
                    };
                    sound.Play();
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
    }
}
