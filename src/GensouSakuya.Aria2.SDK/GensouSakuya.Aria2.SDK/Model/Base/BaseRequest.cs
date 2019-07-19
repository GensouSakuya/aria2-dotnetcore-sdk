using EdjCase.JsonRpc.Core;
using System;
using System.Collections.Generic;

namespace GensouSakuya.Aria2.SDK.Model
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
}
