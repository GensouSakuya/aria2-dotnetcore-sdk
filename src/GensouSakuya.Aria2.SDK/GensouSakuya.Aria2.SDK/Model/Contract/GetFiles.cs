using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class GetFilesRequest : BaseRequest
    {
        public string GID { get; set; }

        protected override string MethodName => "aria2.getFiles";

        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(GID))
            {
                throw new Exception();
            }

            AddParam(GID);
        }
    }

    internal class GetFilesResponse : BaseResponse
    {
        public GetFilesResponse(BaseResponse res) : base(res)
        {
            if (!IsSuccess)
            {
                return;
            }
            Info = JsonConvert.DeserializeObject<List<FileModel>>(res.Result as string);
        }

        public List<FileModel> Info { get; private set; }
    }
}
