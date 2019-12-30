using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model.Contract
{
    internal class SaveSessionRequest : BaseRequest
    {
        //https://aria2.github.io/manual/en/html/aria2c.html#aria2.saveSession

        protected override string MethodName => "aria2.saveSession";

        protected override void PrepareParam()
        {
        }
    }

    internal class SaveSessionResponse : BaseResponse
    {
        public SaveSessionResponse(BaseResponse res) : base(res)
        {
        }
    }
}
