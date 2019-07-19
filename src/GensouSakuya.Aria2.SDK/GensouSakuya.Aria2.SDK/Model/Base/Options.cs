namespace GensouSakuya.Aria2.SDK.Model
{
    internal class Options
    {
        public Options(int? split, string proxy)
        {
            Split = split ?? 1;
            Http_Proxy = proxy ?? "";
        }

        public int Split { get; set; }

        public string Http_Proxy { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
