using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model.Contract
{
    internal class GetVersionRequest : BaseRequest
    {
        //https://aria2.github.io/manual/en/html/aria2c.html#aria2.getGlobalStat

        protected override string MethodName => "aria2.getVersion";

        protected override void PrepareParam()
        {
        }
    }

    internal class GetVersionResponse : BaseResponse
    {
        public GetVersionResponse(BaseResponse res) : base(res)
        {
        }
    }
}
