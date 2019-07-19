using System.Collections.Generic;

namespace GensouSakuya.Aria2.SDK.Model
{
    public class ServerModel
    {
        public int Index { get; set; }

        public List<ServerItem> Servers { get; set; }

        public class ServerItem
        {
            public string Uri { get; set; }
            public string CurrentUri { get; set; }
            public long DownloadSpeed { get; set; }
        }
    }
}
