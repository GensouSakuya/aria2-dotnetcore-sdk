namespace GensouSakuya.Aria2.SDK.Model
{
    public class PeerModel
    {
        public string PeerID { get; set; }
        public string IP { get; set; }
        public int port { get; set; }
        public string BitField { get; set; }
        public bool AmChoking { get; set; }
        public bool PeerChoking { get; set; }
        public long DownloadSpeed { get; set; }
        public long UploadSpeed { get; set; }
        public bool Seeder { get; set; }
    }
}
