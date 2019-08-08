using System;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class UnpauseAllRequest : BaseRequest
    {
        public string GID { get; set; }

        protected override string MethodName => "aria2.unpauseAll";
        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(GID))
            {
                throw new Exception();
            }

            AddParam(GID);
        }
    }

    internal class UnpauseAllResponse : BaseResponse
    {
        public UnpauseAllResponse(BaseResponse res) : base(res)
        {
            if (!IsSuccess)
            {
                return;
            }
        }
    }
}
