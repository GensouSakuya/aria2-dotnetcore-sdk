using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model.Contract
{
    internal class ChangeOptionRequest : BaseRequest
    {
        //https://aria2.github.io/manual/en/html/aria2c.html#aria2.changeOption

        protected override string MethodName => "aria2.changeOption";

        protected override void PrepareParam()
        {
        }
    }

    internal class ChangeOptionResponse : BaseResponse
    {
        public ChangeOptionResponse(BaseResponse res) : base(res)
        {
        }
    }
}
