using GensouSakuya.Aria2.SDK;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace demo
{
    class Program
    {
        static Process _aria2Process;
        static Aria2Client _client;
        private const int _aria2Port = 7777;

        static async Task Main(string[] args)
        {
            RunAria2();
            _client = new Aria2Client("localhost", _aria2Port);

            var gid = await Demo.UriDownload(_client);

            await Task.Delay(3000);
            //wait 3 sec
            var downloadStatus = await Demo.GetStatus(_client,gid);
            Console.WriteLine($@"
Dir:{downloadStatus.Dir}
Speed:{downloadStatus.DownloadSpeed}
Length:{downloadStatus.TotalLength}
");

            await Task.Delay(1000);
            downloadStatus = await Demo.GetStatus(_client, gid);
            Console.WriteLine($@"
Dir:{downloadStatus.Dir}
Speed:{downloadStatus.DownloadSpeed}
Length:{downloadStatus.TotalLength}
");

            Console.ReadKey();
            _client.Shutdown();
        }

        private static void RunAria2()
        {
            _aria2Process = Process.Start(new ProcessStartInfo
            {
                Arguments = $"--enable-rpc=true --rpc-listen-all=true --rpc-listen-port={_aria2Port} --rpc-allow-origin-all=true",
                FileName = "Aria2 1.35.0\\x64\\aria2c.exe"
            });
            _aria2Process.Start();
        }
    }
}
