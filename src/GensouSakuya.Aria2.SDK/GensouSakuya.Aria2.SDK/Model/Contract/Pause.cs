using System;
using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class PauseRequest : BaseRequest
    {
        public string GID { get; set; }

        protected override string MethodName => "aria2.pause";
        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(GID))
            {
                throw new Exception();
            }

            AddParam(GID);
        }
    }

    internal class PauseResponse : BaseResponse
    {
        public PauseResponse(BaseResponse res) : base(res)
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
