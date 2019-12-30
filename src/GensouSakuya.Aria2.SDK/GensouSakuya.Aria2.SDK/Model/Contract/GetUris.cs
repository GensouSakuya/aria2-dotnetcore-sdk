using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class GetUrisRequest : BaseRequest
    {
        public string GID { get; set; }

        protected override string MethodName => "aria2.getUris";

        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(GID))
            {
                throw new Exception();
            }

            AddParam(GID);
        }
    }

    internal class GetUrisResponse : BaseResponse
    {
        public GetUrisResponse(BaseResponse res) : base(res)
        {
            if (!IsSuccess)
            {
                return;
            }
            Info = JsonConvert.DeserializeObject<List<UriModel>>(res.Result as string);
        }

        public List<UriModel> Info { get; private set; }
    }
}
