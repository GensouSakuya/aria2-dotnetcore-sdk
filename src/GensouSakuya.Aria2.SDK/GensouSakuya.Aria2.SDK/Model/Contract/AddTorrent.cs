using System;
using System.Collections.Generic;
using System.Linq;
using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class AddTorrentRequest : BaseRequest
    {
        public string torrent { get; set; }
        public Options Options { get; set; }
        public int? Position { get; set; }
        public List<string> Uris { get; set; }

        protected override string MethodName => "aria2.addTorrent";

        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(torrent))
            {
                throw new Exception();
            }
            
            AddParam(torrent);
            if (Uris != null && Uris.Any())
            {
                AddParam(Uris);
            }

            if (Options != null)
            {
                AddParam(Options.ToString());
                if (Position.HasValue)
                {
                    AddParam(Position);
                }
            }
        }
    }

    internal class AddTorrentResponse : BaseResponse
    {
        public AddTorrentResponse(BaseResponse res) : base(res)
        {
            if (!IsSuccess)
            {
                return;
            }
            GID = res.Result as string;
        }

        public string GID { get; private set; }
    }
}
