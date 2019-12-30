using System;
using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class RemoveRequest : BaseRequest
    {
        public string GID { get; set; }

        protected override string MethodName => "aria2.remove";
        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(GID))
            {
                throw new Exception();
            }

            AddParam(GID);
        }
    }

    internal class RemoveResponse : BaseResponse
    {
        public RemoveResponse(BaseResponse res) : base(res)
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
