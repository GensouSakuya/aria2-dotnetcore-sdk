﻿using System.Collections.Generic;

namespace GensouSakuya.Aria2.SDK.Model
{
    public class BitTorrentModel
    {
        public List<string> AnnounceList { get; set; }
        public string Comment { get; set; }
        public long CreatonDate { get; set; }
        public string Mode { get; set; }

        public List<InfoModel> Info { get; set; }

        public class InfoModel
        {
            public string Name { get; set; }
        }
    }
}
