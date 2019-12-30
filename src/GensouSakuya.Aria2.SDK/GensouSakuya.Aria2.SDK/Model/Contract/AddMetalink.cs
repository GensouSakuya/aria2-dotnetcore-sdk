using System;
using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class AddMetalinkRequest : BaseRequest
    {
        public string Metalink { get; set; }
        public Options Options { get; set; }
        public int? Position { get; set; }

        protected override string MethodName => "aria2.addMetalink";

        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(Metalink))
            {
                throw new Exception();
            }
            
            AddParam(Metalink);

            if (Options != null)
            {
                AddParam(Options.ToString());
                if (Position.HasValue)
                {
                    AddParam(Position);
                }
            }
        }
    }

    internal class AddMetalinkResponse : BaseResponse
    {
        public AddMetalinkResponse(BaseResponse res) : base(res)
        {
            if (!IsSuccess)
            {
                return;
            }
            GID = res.Result as string;
        }

        public string GID { get; private set; }
    }
}
