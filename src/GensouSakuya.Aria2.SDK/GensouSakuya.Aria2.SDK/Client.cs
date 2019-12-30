using System;
using System.Threading.Tasks;
using EdjCase.JsonRpc.Client;
using GensouSakuya.Aria2.SDK.Model;
using GensouSakuya.Aria2.SDK.Model.Base;
using Newtonsoft.Json;

namespace GensouSakuya.Aria2.SDK
{
    internal class Client
    {        
        private RpcClient _rpcClient = null;
        private readonly string _secret = null;

        public Client(string baseUri,string secret)
        {
            var uri = new Uri(baseUri);
            _rpcClient = new RpcClient(uri, new BaseRequestConverter());
            _secret = secret;
        }

        public BaseResponse SendRequest(BaseRequest req)
        {
            BaseResponse response = null;
            Task.WaitAll(Task.Run(async () =>
            {
                try
                {
                    response = new BaseResponse(await _rpcClient.SendRequestAsync(req.ToRpcRequest()));
                }
                catch (Exception e)
                {
                    response = new BaseResponse(GetResByException(e));
                }
            }));
            return response;
        }

        public void SendRequestWithoutResponse(BaseRequest req)
        {
            _rpcClient.SendRequestAsync(req.ToRpcRequest()).Wait(new TimeSpan(0, 0, 3));
        }

        public async Task<BaseResponse> SendRequestAsync(BaseRequest req)
        {
            BaseResponse response = null;
            try
            {
                response = new BaseResponse(await _rpcClient.SendRequestAsync(req.ToRpcRequest()));
            }
            catch (Exception e)
            {
                response = new BaseResponse(GetResByException(e));
            }
            return response;
        }

        private RpcErrorResponse GetResByException(Exception e)
        {
            RpcClientInvalidStatusCodeException invalidStatusCodeException = null;
            if (e is RpcClientInvalidStatusCodeException)
            {
                invalidStatusCodeException = e as RpcClientInvalidStatusCodeException;
            }
            else if (e.InnerException is RpcClientInvalidStatusCodeException)
            {
                invalidStatusCodeException = e.InnerException as RpcClientInvalidStatusCodeException;
            }

            if (invalidStatusCodeException != null)
            {
                return JsonConvert.DeserializeObject<RpcErrorResponse>(invalidStatusCodeException.Content);
            }
            else
            {
                return new RpcErrorResponse
                {
                    Id = null,
                    Error = new Aria2Error
                    {
                        Code = -1, Message = "Unknown Exception"
                    }
                };
            }
        }
    }
}
