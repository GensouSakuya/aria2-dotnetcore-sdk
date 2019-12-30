using System;
using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class ForcePauseAllRequest : BaseRequest
    {
        public string GID { get; set; }

        protected override string MethodName => "aria2.forceForcePauseAll";
        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(GID))
            {
                throw new Exception();
            }

            AddParam(GID);
        }
    }

    internal class ForcePauseAllResponse : BaseResponse
    {
        public ForcePauseAllResponse(BaseResponse res) : base(res)
        {
            if (!IsSuccess)
            {
                return;
            }
        }
    }
}
