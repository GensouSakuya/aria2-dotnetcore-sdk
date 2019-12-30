using System;
using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class ForceRemoveRequest : BaseRequest
    {
        public string GID { get; set; }

        protected override string MethodName => "aria2.forceRemove";
        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(GID))
            {
                throw new Exception();
            }

            AddParam(GID);
        }
    }

    internal class ForceRemoveResponse : BaseResponse
    {
        public ForceRemoveResponse(BaseResponse res) : base(res)
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
