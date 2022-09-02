using System;
using System.Collections.Generic;
using TuyenSinh_api.Domain.Enum;

namespace TuyenSinh_api.Domain.Common
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public ResultCodeEnum ResultCode { get; set; }
        public object Message { get; set; }
        public List<string> ValidationErrors { get; set; }
        public T Data { get; set; }
        public BaseResponse()
        {
            Success = true;
            ResultCode = ResultCodeEnum.GET;
        }

        public BaseResponse(string message)
        {
            Success = true;
            Message = message;
            ResultCode = ResultCodeEnum.GET;
        }

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }

        public BaseResponse(string message, bool success, ResultCodeEnum resultCode)
        {
            Success = success;
            Message = message;
            ResultCode = resultCode;
        }

        public BaseResponse(T data, string message = null)
        {
            Success = true;
            Message = message;
            ResultCode = ResultCodeEnum.GET;
            Data = data;
        }

        public BaseResponse(T data, ResultCodeEnum resultCode, string message = null)
        {
            Success = true;
            Message = message;
            ResultCode = resultCode;
            Data = data;
        }
    }
}
