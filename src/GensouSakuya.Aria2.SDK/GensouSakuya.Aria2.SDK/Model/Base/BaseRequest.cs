using System;
using System.Collections.Generic;
using EdjCase.JsonRpc.Client;
using EdjCase.JsonRpc.Core;
using Microsoft.Extensions.Logging;

namespace GensouSakuya.Aria2.SDK.Model.Base
{
    internal abstract class BaseRequest
    {
        protected Guid Guid = Guid.NewGuid();

        protected abstract string MethodName { get; }

        private string _secret = null;

        private List<object> _parameters = new List<object>();

        protected void AddParam(object obj)
        {
            _parameters.Add(obj);
        }

        protected abstract void PrepareParam();

        public virtual RpcRequest ToRpcRequest()
        {
            if (!string.IsNullOrWhiteSpace(_secret))
            {
                AddParam(_secret);
            }
            PrepareParam();
            var id = new RpcId(Guid.ToString());
            return new RpcRequest(id, MethodName, _parameters);
        }
    }

    internal class BaseRequestConverter : IRequestSerializer
    {
        private ILogger logger;
        private DefaultRequestJsonSerializer _serializer = new DefaultRequestJsonSerializer();
        public string SerializeBulk(IList<RpcRequest> requests)
        {
            return _serializer.SerializeBulk(requests);
        }

        public string Serialize(RpcRequest request)
        {
            var jsonReq = _serializer.Serialize(request);
            //just for debugging
            return _serializer.Serialize(request);
        }

        public RpcResponse Deserialize(string json, Type resultType = null)
        {
            return _serializer.Deserialize(json,resultType);
        }

        public List<RpcResponse> DeserializeBulk(string json, Func<RpcId, Type> resultTypeResolver = null)
        {
            return _serializer.DeserializeBulk(json,resultTypeResolver);
        }
    }
}
