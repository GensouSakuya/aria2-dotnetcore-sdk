using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class GetServersRequest : BaseRequest
    {
        public string GID { get; set; }

        protected override string MethodName => "aria2.getServers";

        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(GID))
            {
                throw new Exception();
            }

            AddParam(GID);
        }
    }

    internal class GetServersResponse : BaseResponse
    {
        public GetServersResponse(BaseResponse res) : base(res)
        {
            if (!IsSuccess)
            {
                return;
            }
            Info = JsonConvert.DeserializeObject<List<ServerModel>>(res.Result as string);
        }

        public List<ServerModel> Info { get; private set; }
    }
}
