using GensouSakuya.Aria2.SDK;
using System.Threading.Tasks;
using GensouSakuya.Aria2.SDK.Model;

namespace demo
{
    public class Demo
    {
        public static async Task<string> UriDownload(Aria2Client client)
        {
            return await client.AddUri("https://speed.hetzner.de/1GB.bin");
        }

        public static async Task<DownloadStatusModel> GetStatus(Aria2Client client,string gid)
        {
            return await client.TellStatus(gid);
        }
    }
}
