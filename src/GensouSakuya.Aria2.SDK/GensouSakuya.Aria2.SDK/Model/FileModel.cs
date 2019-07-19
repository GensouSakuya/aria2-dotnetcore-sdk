using System.Collections.Generic;

namespace GensouSakuya.Aria2.SDK.Model
{
    public class FileModel
    {
        public int Index { get; set; }
        public string Path { get; set; }
        public long Length { get; set; }
        public long CompletedLength { get; set; }
        public bool Selected { get; set; }
        public List<UriModel> Uris { get; set; }
    }
}
