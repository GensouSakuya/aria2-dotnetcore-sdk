using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class ForceShutdownRequest : BaseRequest
    {
        protected override string MethodName => "aria2.forceShutdown";

        protected override void PrepareParam()
        {
        }
    }

    internal class ForceShutdownResponse : BaseResponse
    {
        public ForceShutdownResponse(BaseResponse res) : base(res)
        {
            if (!IsSuccess)
            {
                return;
            }
        }
    }
}
