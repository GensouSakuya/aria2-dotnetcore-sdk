using System;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class GetOptionRequest : BaseRequest
    {
        public string GID { get; set; }

        protected override string MethodName => "aria2.getOption";
        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(GID))
            {
                throw new Exception();
            }

            AddParam(GID);
        }
    }

    internal class GetOptionResponse : BaseResponse
    {
        public GetOptionResponse(BaseResponse res) : base(res)
        {
            Option = res.Result;
        }

        public object Option { get; private set; }
    }
}
