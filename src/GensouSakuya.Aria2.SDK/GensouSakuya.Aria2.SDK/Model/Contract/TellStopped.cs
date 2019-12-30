using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class TellStoppedRequest:BaseRequest
    {
        public int Offset { get; set; } = 0;
        public int Num { get; set; } = int.MaxValue;

        public List<string> Keys { get; set; }

        protected override string MethodName => "aria2.tellStopped";
        protected override void PrepareParam()
        {
            AddParam(Offset);
            AddParam(Num);

            if (Keys != null && Keys.Any())
            {
                AddParam(Keys);
            }
        }
    }

    internal class TellStoppedResponse : BaseResponse
    {
        public TellStoppedResponse(BaseResponse res) : base(res)
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
