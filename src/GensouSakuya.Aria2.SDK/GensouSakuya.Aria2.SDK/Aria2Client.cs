using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GensouSakuya.Aria2.SDK.Model;

namespace GensouSakuya.Aria2.SDK
{
    public class Aria2Client
    {
        public Aria2Client(string host, int port)
        {
            if (!host.StartsWith("http://") && !host.StartsWith("https://"))
            {
                host = "http://" + host;
            }
            var url = $"{host}:{port}/jsonrpc";
            _client = new Client(url, null);
        }

        private Client _client = null;

        /// <summary>
        /// 新增下载
        /// </summary>
        /// <param name="uri">HTTP/FTP/SFTP/Magnet下载链接</param>
        /// <param name="split">下载连接数</param>
        /// <param name="proxy">代理地址</param>
        /// <param name="position">下载队列位置，超过队列长度则排到队尾</param>
        /// <returns>下载请求的GID</returns>
        public async Task<string> AddUri(string uri, int? split = null, string proxy = null, int? position = null)
        {
            return await AddUri(new List<string>
            {
                uri
            }, split, proxy, position);
        }

        /// <summary>
        /// 新增同资源下多链接下载
        /// </summary>
        /// <param name="uris">相同资源下的HTTP/FTP/SFTP/Magnet下载链接集合，资源不相同则会失败</param>
        /// <param name="split">下载所有链接的总连接数</param>
        /// <param name="proxy">代理地址</param>
        /// <param name="position">下载队列位置，超过队列长度则排到队尾</param>
        /// <returns>下载请求的GID</returns>
        public async Task<string> AddUri(IEnumerable<string> uris, int? split = null, string proxy = null,int? position = null)
        {
            var option = split.HasValue || !string.IsNullOrWhiteSpace(proxy) ? new Options(split, proxy) : null;
            var res = new AddUriResponse(await _client.SendRequestAsync(new AddUriRequest
            {
                Uris = uris.ToList(),
                Options = option,
                Position = position
            }));
            return res?.GID;
        }

        /// <summary>
        /// 新增BT下载
        /// </summary>
        /// <param name="torrentBase64">Base64编码的Torrent文件</param>
        /// <param name="split">下载连接数</param>
        /// <param name="proxy">代理地址</param>
        /// <param name="position">下载队列位置，超过队列长度则排到队尾</param>
        /// <returns>下载请求的GID</returns>
        public async Task<string> AddTorrentBase64(string torrentBase64, int? split = null, string proxy = null, int? position = null)
        {
            var option = split.HasValue || !string.IsNullOrWhiteSpace(proxy) ? new Options(split, proxy) : null;
            return new AddTorrentResponse(await _client.SendRequestAsync(new AddTorrentRequest
            {
                torrent = torrentBase64,
                Options = option,
                Position = position
            }))?.GID;
        }

        /// <summary>
        /// 新增BT下载
        /// </summary>
        /// <param name="torrentFilePath">Torrent文件路径</param>
        /// <param name="split">下载连接数</param>
        /// <param name="proxy">代理地址</param>
        /// <param name="position">下载队列位置，超过队列长度则排到队尾</param>
        /// <returns>下载请求的GID</returns>
        public async Task<string> AddTorrentFile(string torrentFilePath, int? split = null, string proxy = null, int? position = null)
        {
            if (!File.Exists(torrentFilePath))
            {
                throw new System.Exception("Torrent文件不存在");
            }

            FileInfo fi = new FileInfo(torrentFilePath);
            return await AddTorrentFile(fi, split, proxy, position);
        }

        /// <summary>
        /// 新增BT下载
        /// </summary>
        /// <param name="torrentFile">Torrent文件对象</param>
        /// <param name="split">下载连接数</param>
        /// <param name="proxy">代理地址</param>
        /// <param name="position">下载队列位置，超过队列长度则排到队尾</param>
        /// <returns>下载请求的GID</returns>
        public async Task<string> AddTorrentFile(FileInfo torrentFile, int? split = null, string proxy = null, int? position = null)
        {
            byte[] buff = new byte[torrentFile.Length];

            using (var fs = torrentFile.OpenRead())
            {
                fs.Read(buff, 0, Convert.ToInt32(fs.Length));
            }

            var base64Torrent = Convert.ToBase64String(buff);
            return await AddTorrentBase64(base64Torrent, split, proxy, position);
        }

        /// <summary>
        /// 新增Metalink下载
        /// </summary>
        /// <param name="uris"></param>
        /// <param name="split"></param>
        /// <param name="proxy"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public async Task<string> AddMetalink(string metalink, int? split = 0, string proxy = null, int? position = null)
        {
            var option = split.HasValue || !string.IsNullOrWhiteSpace(proxy) ? new Options(split, proxy) : null;
            var res = new AddMetalinkResponse(await _client.SendRequestAsync(new AddMetalinkRequest
            {
                Metalink = metalink,
                Options = option,
                Position = position
            }));
            return res?.GID;
        }

        /// <summary>
        /// 获取下载任务状态
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="keys">需要获取的属性表达式集合（不传则返回全部属性）</param>
        public async Task<DownloadStatusModel> TellStatus(string gid, List<Expression<Func<DownloadStatusModel, object>>> keys = null)
        {
            var strKeys = new List<string>();
            if (keys != null && keys.Any())
            {
                keys.ForEach(key =>
                {
                    MemberInitExpression init = key.Body as MemberInitExpression;
                    strKeys.AddRange(init.Bindings.Select(p => p.Member.Name));
                });
            }
            var res = new TellStatusResponse(await _client.SendRequestAsync(new TellStatusRequest
            {
                GID = gid,
                Keys = strKeys
            }));
            return res?.Info;
        }

        /// <summary>
        /// 移除下载任务
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public async Task<string> Remove(string gid)
        {
            var res = new RemoveResponse(await _client.SendRequestAsync(new RemoveRequest
            {
                GID = gid
            }));
            return res?.GID;
        }

        /// <summary>
        /// 强制移除下载任务
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public async Task<string> ForceRemove(string gid)
        {
            var res = new ForceRemoveResponse(await _client.SendRequestAsync(new ForceRemoveRequest
            {
                GID = gid
            }));
            return res?.GID;
        }

        /// <summary>
        /// 暂停下载任务
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public async Task<string> Pause(string gid)
        {
            var res = new PauseResponse(await _client.SendRequestAsync(new PauseRequest
            {
                GID = gid
            }));
            return res?.GID;
        }

        /// <summary>
        /// 强制暂停下载任务
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public async Task<string> ForcePause(string gid)
        {
            var res = new ForcePauseResponse(await _client.SendRequestAsync(new ForcePauseRequest
            {
                GID = gid
            }));
            return res?.GID;
        }

        /// <summary>
        /// 暂停全部下载任务
        /// </summary>
        public async void PauseAll()
        {
            var res = new PauseAllResponse(await _client.SendRequestAsync(new PauseAllRequest()));
        }

        /// <summary>
        /// 强制暂停全部下载任务
        /// </summary>
        public async void ForcePauseAll()
        {
            var res = new ForcePauseAllResponse(await _client.SendRequestAsync(new ForcePauseAllRequest()));
        }
        
        /// <summary>
        /// 取消暂停下载任务
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public async Task<string> Unpause(string gid)
        {
            var res = new UnpauseResponse(await _client.SendRequestAsync(new UnpauseRequest
            {
                GID = gid
            }));
            return res?.GID;
        }

        /// <summary>
        /// 取消暂停全部下载任务
        /// </summary>
        public async void UnpauseAll()
        {
            var res = new UnpauseAllResponse(await _client.SendRequestAsync(new UnpauseAllRequest()));
        }

        /// <summary>
        /// 获取下载任务的Uri
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public async Task<List<UriModel>> GetUris(string gid)
        {
            var res = new GetUrisResponse(await _client.SendRequestAsync(new GetUrisRequest
            {
                GID = gid
            }));
            return res?.Info;
        }

        /// <summary>
        /// 获取下载任务的文件列表
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public async Task<List<FileModel>> GetFiles(string gid)
        {
            var res = new GetFilesResponse(await _client.SendRequestAsync(new GetFilesRequest
            {
                GID = gid
            }));
            return res?.Info;
        }

        /// <summary>
        /// 获取下载任务的Peer列表
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public async Task<List<PeerModel>> GetPeers(string gid)
        {
            var res = new GetPeersResponse(await _client.SendRequestAsync(new GetPeersRequest
            {
                GID = gid
            }));
            return res?.Info;
        }

        /// <summary>
        /// 获取下载任务的服务器列表
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public async Task<List<ServerModel>> GetServers(string gid)
        {
            var res = new GetServersResponse(await _client.SendRequestAsync(new GetServersRequest
            {
                GID = gid
            }));
            return res?.Info;
        }
        
        /// <summary>
        /// 获取所有下载中任务
        /// </summary>
        /// <param name="keys"></param>
        public async Task<List<DownloadStatusModel>> TellActive(List<Expression<Func<DownloadStatusModel, object>>> keys = null)
        {
            var strKeys = new List<string>();
            if (keys != null)
            {
                keys.ForEach(key =>
                {
                    MemberInitExpression init = key.Body as MemberInitExpression;
                    strKeys.AddRange(init.Bindings.Select(p => p.Member.Name));
                });
            }
            var res = new TellActiveResponse(await _client.SendRequestAsync(new TellActiveRequest
            {
                Keys = strKeys
            }));
            return res?.Info;
        }

        /// <summary>
        /// 获取所有暂停任务
        /// </summary>
        /// <param name="keys"></param>
        public async Task<List<DownloadStatusModel>> TellWaiting(List<Expression<Func<DownloadStatusModel, object>>> keys = null)
        {
            var strKeys = new List<string>();
            if (keys != null)
            {
                keys.ForEach(key =>
                {
                    MemberInitExpression init = key.Body as MemberInitExpression;
                    strKeys.AddRange(init.Bindings.Select(p => p.Member.Name));
                });
            }
            var res = new TellWaitingResponse(await _client.SendRequestAsync(new TellWaitingRequest
            {
                Keys = strKeys
            }));
            return res?.Info;
        }

        /// <summary>
        /// 获取指定范围的暂停任务
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="takeCount"></param>
        /// <param name="keys"></param>
        public async Task<List<DownloadStatusModel>> TellWaiting(int startIndex, int takeCount,
            Expression<Func<DownloadStatusModel, DownloadStatusModel>> keys = null)
        {
            var strKeys = new List<string>();
            if (keys != null)
            {
                MemberInitExpression init = keys.Body as MemberInitExpression;
                strKeys.AddRange(init.Bindings.Select(p => p.Member.Name));
            }

            var res = new TellWaitingResponse(await _client.SendRequestAsync(new TellWaitingRequest
            {
                Offset = startIndex,
                Num = takeCount,
                Keys = strKeys
            }));
            return res?.Info;
        }
        
        /// <summary>
        /// 获取所有停止任务
        /// </summary>
        /// <param name="keys"></param>
        public async Task<List<DownloadStatusModel>> TellStopped(List<Expression<Func<DownloadStatusModel, object>>> keys = null)
        {
            var strKeys = new List<string>();
            if (keys != null)
            {
                keys.ForEach(key =>
                {
                    MemberInitExpression init = key.Body as MemberInitExpression;
                    strKeys.AddRange(init.Bindings.Select(p => p.Member.Name));
                });
            }
            var res = new TellStoppedResponse(await _client.SendRequestAsync(new TellStoppedRequest
            {
                Keys = strKeys
            }));
            return res?.Info;
        }

        /// <summary>
        /// 获取指定范围的暂停任务
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="takeCount"></param>
        /// <param name="keys"></param>
        public async Task<List<DownloadStatusModel>> TellStopped(int startIndex, int takeCount,
            Expression<Func<DownloadStatusModel, DownloadStatusModel>> keys = null)
        {
            var strKeys = new List<string>();
            if (keys != null)
            {
                MemberInitExpression init = keys.Body as MemberInitExpression;
                strKeys.AddRange(init.Bindings.Select(p => p.Member.Name));
            }

            var res = new TellStoppedResponse(await _client.SendRequestAsync(new TellStoppedRequest
            {
                Offset = startIndex,
                Num = takeCount,
                Keys = strKeys
            }));
            return res?.Info;
        }

        /// <summary>
        /// 变更任务在队列中的位置
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="position"></param>
        /// <param name="how"></param>
        /// <returns></returns>
        public async Task<int> ChangePosition(string gid, int position, EnumHowChangePosition how)
        {
            var res = new ChangePositionResponse(await _client.SendRequestAsync(new ChangePositionRequest
            {
                GID = gid,
                How = how,
                Position = position
            }));
            return res?.Position ?? 0;
        }

        /// <summary>
        /// 变更任务到队首
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="position"></param>
        /// <param name="how"></param>
        /// <returns></returns>
        public async Task<int> ChangePositionToFirst(string gid)
        {
            var res = new ChangePositionResponse(await _client.SendRequestAsync(new ChangePositionRequest
            {
                GID = gid,
                How = EnumHowChangePosition.Set,
                Position = 0
            }));
            return res?.Position ?? 0;
        }

        /// <summary>
        /// 变更任务的uri
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="fileIndex"></param>
        /// <param name="delUris"></param>
        /// <param name="addUris"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public async Task<List<int>> ChangeUri(string gid,int fileIndex,List<string> delUris = null,List<string> addUris = null,int? position = null)
        {
            var res = new ChangeUriResponse(await _client.SendRequestAsync(new ChangeUriRequest
            {
                GID = gid,
                FileIndex = fileIndex,
                AddUris = addUris,
                DelUris = delUris,
                Position = position
            }));
            return res?.UriResult ?? new List<int>();
        }

        /// <summary>
        /// 获取下载任务的设置
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public async Task<object> GetOption(string gid)
        {
            var res = new GetOptionResponse(await _client.SendRequestAsync(new GetOptionRequest
            {
                GID = gid
            }));
            return res?.Option;
        }

        public void Shutdown()
        {
            //TODO:如果等待响应则会锁住，但文档中说调用后是有响应的
            _client.SendRequestWithoutResponse(new ShutdownRequest { });
        }

        public void ForceShutdown()
        {
            //TODO:如果等待响应则会锁住，但文档中说调用后是有响应的
            _client.SendRequestWithoutResponse(new ForceShutdownRequest { });
        }
    }
}
