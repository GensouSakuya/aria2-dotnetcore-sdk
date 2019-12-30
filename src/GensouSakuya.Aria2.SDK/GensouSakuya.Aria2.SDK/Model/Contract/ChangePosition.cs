using System;
using GensouSakuya.Aria2.SDK.Model.Base;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class ChangePositionRequest : BaseRequest
    {
        public string GID { get; set; }
        public int Position { get; set; }
        public EnumHowChangePosition How { get; set; }

        protected override string MethodName => "aria2.changePosition";
        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(GID))
            {
                throw new Exception();
            }

            AddParam(GID);

            AddParam(Position);

            var howStr = "";

            switch (How)
            {
                case EnumHowChangePosition.Set:
                    howStr = "POS_SET";
                    break;
                case EnumHowChangePosition.AddCur:
                    howStr = "POS_CUR";
                    break;
                case EnumHowChangePosition.End:
                    howStr = "PSO_END";
                    break;
            }

            AddParam(howStr);
        }
    }

    public enum EnumHowChangePosition
    {
        Set = 0,
        AddCur = 1,
        End = 2
    }

    internal class ChangePositionResponse : BaseResponse
    {
        public ChangePositionResponse(BaseResponse res) : base(res)
        {
            if (!IsSuccess)
            {
                return;
            }
            int.TryParse(res.Result as string, out int pos);
            Position = pos;
        }

        public int Position { get; private set; }
    }
}
