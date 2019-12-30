using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model.Contract
{
    internal class GetGlobalStatRequest : BaseRequest
    {
        //https://aria2.github.io/manual/en/html/aria2c.html#aria2.getGlobalStat

        protected override string MethodName => "aria2.getGlobalStat";

        protected override void PrepareParam()
        {
        }
    }

    internal class GetGlobalStatResponse : BaseResponse
    {
        public GetGlobalStatResponse(BaseResponse res) : base(res)
        {
        }
    }
}
