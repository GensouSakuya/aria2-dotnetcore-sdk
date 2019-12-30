using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model.Contract
{
    internal class RemoveDownloadResultRequest : BaseRequest
    {
        //https://aria2.github.io/manual/en/html/aria2c.html#aria2.removeDownloadResult

        protected override string MethodName => "aria2.removeDownloadResult";

        protected override void PrepareParam()
        {
        }
    }

    internal class RemoveDownloadResultResponse : BaseResponse
    {
        public RemoveDownloadResultResponse(BaseResponse res) : base(res)
        {
        }
    }
}
