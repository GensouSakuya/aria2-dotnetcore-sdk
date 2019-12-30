using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class TellActiveRequest:BaseRequest
    {
        public List<string> Keys { get; set; }

        protected override string MethodName => "aria2.tellActive";
        protected override void PrepareParam()
        {
            if (Keys != null && Keys.Any())
            {
                AddParam(Keys);
            }
        }
    }

    internal class TellActiveResponse : BaseResponse
    {
        public TellActiveResponse(BaseResponse res) : base(res)
        {
            if (!IsSuccess)
            {
                return;
            }
            Info = JsonConvert.DeserializeObject<List<DownloadStatusModel>>(res.Result as string);
        }
        public List<DownloadStatusModel> Info { get; private set; }
    }
}
