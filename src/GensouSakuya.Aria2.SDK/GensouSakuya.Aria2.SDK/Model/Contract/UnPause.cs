using System;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class UnpauseRequest : BaseRequest
    {
        public string GID { get; set; }

        protected override string MethodName => "aria2.unpause";
        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(GID))
            {
                throw new Exception();
            }

            AddParam(GID);
        }
    }

    internal class UnpauseResponse : BaseResponse
    {
        public UnpauseResponse(BaseResponse res) : base(res)
        {
            GID = res.Result as string;
        }

        public string GID { get; private set; }
    }
}
