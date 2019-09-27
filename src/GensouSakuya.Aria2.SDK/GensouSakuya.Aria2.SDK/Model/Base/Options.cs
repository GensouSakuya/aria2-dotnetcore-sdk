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
            //  Reduce the size of JSON string
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore, StringEscapeHandling = StringEscapeHandling.EscapeHtml
                });
        }
    }
}
