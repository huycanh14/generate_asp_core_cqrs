using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TuyenSinh_api.Application.Exceptions;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Enum;

namespace TuyenSinh_api.Application.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            int httpStatusCode;
            var result = exception.Message;
            var response = new BaseResponse<string>();
            response.Success = false;

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = (int)HttpStatusCode.BadRequest;
                    response.ResultCode = ResultCodeEnum.BADREQUEST;
                    response.Message = exception.Message;
                    response.ValidationErrors = validationException.ValdationErrors;
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = (int)HttpStatusCode.BadRequest;
                    response.ResultCode = ResultCodeEnum.BADREQUEST;
                    response.Message = badRequestException.Message;
                    break;
                case NotFoundException _:
                    httpStatusCode = (int)HttpStatusCode.NotFound;
                    response.ResultCode = ResultCodeEnum.NOTFOUND;
                    response.Message = exception.Message;
                    break;
                case ApiException apiException:
                    httpStatusCode = (int)HttpStatusCode.InternalServerError;
                    response.ResultCode = ResultCodeEnum.INTERNALSERVERERROR;
                    response.Message = apiException.Message;
                    break;
                default:
                    httpStatusCode = (int)HttpStatusCode.InternalServerError;
                    response.ResultCode = ResultCodeEnum.INTERNALSERVERERROR;
                    response.Message = exception.Message;
                    break;
            }


            _logger.LogError(result);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = httpStatusCode;

            if (result == string.Empty)
            {
                response.Message = exception.Message;
            }
            JsonConvert.DefaultSettings = () =>
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                    Formatting = Formatting.None
                };

                return settings;
            };
            result = JsonConvert.SerializeObject(response);

            return context.Response.WriteAsync(result);
        }
    }
}
