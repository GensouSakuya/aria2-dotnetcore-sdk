using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class GetPeersRequest : BaseRequest
    {
        public string GID { get; set; }

        protected override string MethodName => "aria2.getPeers";

        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(GID))
            {
                throw new Exception();
            }

            AddParam(GID);
        }
    }

    internal class GetPeersResponse : BaseResponse
    {
        public GetPeersResponse(BaseResponse res) : base(res)
        {
            if (!IsSuccess)
            {
                return;
            }
            Info = JsonConvert.DeserializeObject<List<PeerModel>>(res.Result as string);
        }

        public List<PeerModel> Info { get; private set; }
    }
}
