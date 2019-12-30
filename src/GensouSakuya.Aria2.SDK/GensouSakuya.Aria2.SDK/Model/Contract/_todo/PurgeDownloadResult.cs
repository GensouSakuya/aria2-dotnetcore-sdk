using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model.Contract
{
    internal class PurgeDownloadResultRequest : BaseRequest
    {
        //https://aria2.github.io/manual/en/html/aria2c.html#aria2.purgeDownloadResult

        protected override string MethodName => "aria2.purgeDownloadResult";

        protected override void PrepareParam()
        {
        }
    }

    internal class PurgeDownloadResultResponse : BaseResponse
    {
        public PurgeDownloadResultResponse(BaseResponse res) : base(res)
        {
        }
    }
}
