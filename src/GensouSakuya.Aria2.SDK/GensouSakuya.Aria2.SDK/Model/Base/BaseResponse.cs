using EdjCase.JsonRpc.Core;
using System;

namespace GensouSakuya.Aria2.SDK.Model
{
    internal class BaseResponse
    {
        private BaseResponse()
        { }

        public BaseResponse(BaseResponse res)
        {
            Guid = res.Guid;
            IsSuccess = res.IsSuccess;
            Result = res.Result;
            ErrorMessage = res.ErrorMessage;
        }

        public BaseResponse(RpcResponse rpcRes)
        {
            Guid = new Guid(rpcRes.Id.StringValue);
            IsSuccess = !rpcRes.HasError;
            Result = rpcRes.Result;
            ErrorMessage = rpcRes.Error?.Message;
        }

        public BaseResponse(RpcErrorResponse error)
        {
            Guid = new Guid(error.Id.StringValue);
            IsSuccess = false;
            Result = error.Error;
            ErrorMessage = error.Error?.Message;
        }

        public Guid Guid { get; set; }

        public bool IsSuccess { get; private set; }

        public object Result { get; private set; }

        public string ErrorMessage { get; private set; }
    }

    internal class RpcErrorResponse
    {
        public RpcId Id { get; set; }
        public Aria2Error Error { get; set; }
    }

    internal class Aria2Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
