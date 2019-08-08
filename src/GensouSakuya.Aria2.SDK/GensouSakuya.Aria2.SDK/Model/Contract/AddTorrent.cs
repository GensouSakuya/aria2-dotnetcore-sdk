using System;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class AddTorrentRequest : BaseRequest
    {
        public string torrent { get; set; }
        public Options Options { get; set; }
        public int? Position { get; set; }

        protected override string MethodName => "aria2.addTorrent";

        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(torrent))
            {
                throw new Exception();
            }
            
            AddParam(torrent);

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
