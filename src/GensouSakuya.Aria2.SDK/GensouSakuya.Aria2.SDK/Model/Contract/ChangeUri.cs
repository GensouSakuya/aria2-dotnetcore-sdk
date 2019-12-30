using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class ChangeUriRequest : BaseRequest
    {
        public string GID { get; set; }
        public int FileIndex { get; set; }
        public List<string> DelUris { get; set; }
        public List<string> AddUris { get; set; }
        public int? Position { get; set; }

        protected override string MethodName => "aria2.changeUri";
        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(GID))
            {
                throw new Exception();
            }

            AddParam(GID);

            AddParam(FileIndex);

            AddParam(DelUris);

            AddParam(AddUris);

            if (Position.HasValue)
            {
                AddParam(Position.Value);
            }
        }
    }
    
    internal class ChangeUriResponse : BaseResponse
    {
        public ChangeUriResponse(BaseResponse res) : base(res)
        {
            if (!IsSuccess)
            {
                return;
            }
            UriResult = JsonConvert.DeserializeObject<List<int>>(res.Result as string);
        }

        public List<int> UriResult { get; private set; }
    }
}
