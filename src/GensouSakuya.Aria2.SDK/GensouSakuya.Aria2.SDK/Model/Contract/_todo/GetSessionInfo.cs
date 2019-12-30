using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model.Contract
{
    internal class GetSessionInfoRequest : BaseRequest
    {
        //https://aria2.github.io/manual/en/html/aria2c.html#aria2.getGlobalStat

        protected override string MethodName => "aria2.getSessionInfo";

        protected override void PrepareParam()
        {
        }
    }

    internal class GetSessionInfoResponse : BaseResponse
    {
        public GetSessionInfoResponse(BaseResponse res) : base(res)
        {
        }
    }
}
