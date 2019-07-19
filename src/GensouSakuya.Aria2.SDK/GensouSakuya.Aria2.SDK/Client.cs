using System;
using System.Threading.Tasks;
using EdjCase.JsonRpc.Client;
using GensouSakuya.Aria2.SDK.Model;

namespace GensouSakuya.Aria2.SDK
{
    internal class Client
    {        
        private RpcClient _rpcClient = null;
        private string _secret = null;

        public Client(string baseUri,string secret)
        {
            var uri = new Uri(baseUri);
            _rpcClient = new RpcClient(uri);
            _secret = secret;
        }

        public BaseResponse SendRequest(BaseRequest req)
        {
            return new BaseResponse(_rpcClient.SendRequestAsync(req.ToRpcRequest()).Result);
        }

        public void SendRequestWithoutResponse(BaseRequest req)
        {
            _rpcClient.SendRequestAsync(req.ToRpcRequest()).Wait(new TimeSpan(0, 0, 3));
        }

        public async Task<BaseResponse> SendRequestAsync(BaseRequest req)
        {
            return new BaseResponse(await _rpcClient.SendRequestAsync(req.ToRpcRequest()));
        }
    }
}
