using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model.Contract
{
    internal class GetGlobalOptionRequest : BaseRequest
    {
        //https://aria2.github.io/manual/en/html/aria2c.html#aria2.getGlobalOption

        protected override string MethodName => "aria2.getGlobalOption";

        protected override void PrepareParam()
        {
        }
    }

    internal class GetGlobalOptionResponse : BaseResponse
    {
        public GetGlobalOptionResponse(BaseResponse res) : base(res)
        {
        }
    }
}
