using Newtonsoft.Json;

namespace GensouSakuya.Aria2.SDK.Model.Base
{
    internal class Options
    {
        [JsonProperty("split")]
        public int? Split { get; set; }

        [JsonProperty("http-proxy")]
        public string HttpProxy { get; set; }

        [JsonProperty("dir")]
        public string Directory { get; set; }

        [JsonProperty("max-download-limit")]
        public ulong? MaxDownloadSpeed { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore, StringEscapeHandling = StringEscapeHandling.EscapeHtml
                });
        }
    }
}
