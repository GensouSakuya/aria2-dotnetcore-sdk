using System.Collections.Generic;

namespace GensouSakuya.Aria2.SDK.Model
{
    public class DownloadStatusModel
    {
        public string GID { get; set; }
        public string Status { get; set; }
        public long TotalLength { get; set; }
        public long CompletedLength { get; set; }
        public long UploadLength { get; set; }
        public string BitField { get; set; }
        public decimal DownloadSpeed { get; set; }
        public decimal UploadSpeed { get; set; }
        public string InfoHash { get; set; }
        public int NumSeeders { get; set; }
        public long PieceLength { get; set; }
        public int NumPieces { get; set; }
        public int Connections { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> FollowedBy { get; set; }
        public string Following { get; set; }
        public string BelongsTo { get; set; }
        public string Dir { get; set; }
        public List<FileModel> Files { get; set; }
        public BitTorrentModel BitTorrent { get; set; }
        public int VerifiedLength { get; set; }
        public bool VerifyIntegrityPending { get; set; }
    }
}
