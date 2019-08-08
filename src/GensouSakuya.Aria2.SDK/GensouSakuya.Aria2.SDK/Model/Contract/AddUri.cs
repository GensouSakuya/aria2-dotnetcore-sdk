using System;
using System.Collections.Generic;
using System.Linq;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class AddUriRequest:BaseRequest
    {
        public List<string> Uris { get; set; }
        public Options Options { get; set; }
        public int? Position { get; set; }

        protected override string MethodName => "aria2.addUri";

        protected override void PrepareParam()
        {
            if (Uris == null || !Uris.Any())
            {
                throw new Exception();
            }
            
            AddParam(Uris);

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

    internal class AddUriResponse : BaseResponse
    {
        public AddUriResponse(BaseResponse res) : base(res)
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
