using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class ShutdownRequest : BaseRequest
    {
        protected override string MethodName => "aria2.shutdown";

        protected override void PrepareParam()
        {
        }
    }

    internal class ShutdownResponse : BaseResponse
    {
        public ShutdownResponse(BaseResponse res) : base(res)
        {
            if (!IsSuccess)
            {
                return;
            }
        }
    }
}
