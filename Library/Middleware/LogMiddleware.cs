using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Infrastructure.Data.Repositories.Contracts;
using Domain;
using Common.Enum;
using Common.Constants;

namespace Library.Middleware
{
    public class LogMiddleware
    {
        private ILogInformationRepository _informationRepository;
        private ILogErrorRepository _errorRepository;
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, 
            ILogInformationRepository informationRepository,
            ILogErrorRepository errorRepository)
        {
            _informationRepository = informationRepository;
            _errorRepository = errorRepository;

            try
            {
                if (httpContext.Request.Path.Value.StartsWith(ApiRoutes.Swagger_Path))
                {
                    await _next(httpContext);
                }
                else
                {
                    await LogRequest(httpContext);
                    await LogResponse(httpContext);
                }
            }
            catch (Exception ex)
            {
                await LogError(httpContext, ex);
                var response = httpContext.Response;
                response.ContentType = "application/json";

                switch (ex)
                {
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = System.Text.Json.JsonSerializer.Serialize(new { message = ex?.Message });
                await response.WriteAsync(result);
            }
        }

        private async Task LogRequest(HttpContext httpContext)
        {
            Information information = new Information()
            {
                Method = httpContext.Request?.Method,
                Type = ContextType.Request,
                Path = httpContext.Request?.Path.Value,
                QueryString = httpContext.Request?.QueryString.Value,
                Data = ReadBodyFromRequest(httpContext.Request),
                ClientIp = httpContext.Request?.HttpContext.Connection.RemoteIpAddress.ToString()
            };
            await _informationRepository.LogInformation(information);
        }

        private async Task LogResponse(HttpContext httpContext)
        {
            Information information = new Information()
            {
                Method = httpContext.Request?.Method,
                Type = ContextType.Response,
                Path = httpContext.Request?.Path.Value,
                QueryString = httpContext.Request?.QueryString.Value,
                StatusCode = httpContext.Response?.StatusCode,
                Data = ReadBodyFromResponse(httpContext).Result,
                ClientIp = httpContext.Request?.HttpContext.Connection.RemoteIpAddress.ToString()
            };
            await _informationRepository.LogInformation(information);
        }

        private static string ReadBodyFromRequest(HttpRequest request)
        {
            request.EnableBuffering();

            using var streamReader = new StreamReader(request.Body, leaveOpen: true);
            var requestBody = streamReader.ReadToEndAsync();

            request.Body.Position = 0;
            return requestBody.Result;
        }

        private async Task<string> ReadBodyFromResponse(HttpContext httpContext)
        {
            Stream originalBody = httpContext.Response.Body;
            using (var memStream = new MemoryStream())
            {
                httpContext.Response.Body = memStream;

                await _next(httpContext);

                memStream.Position = 0;
                string responseBody = new StreamReader(memStream).ReadToEnd();

                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);
                httpContext.Response.Body = originalBody;
                return responseBody;
            }
        }

        private async Task LogError(HttpContext httpContext, Exception ex)
        {
            Error error = new Error()
            {
                Method = httpContext.Request?.Method,
                Path = httpContext.Request?.Path.Value,
                StatusCode = httpContext.Response?.StatusCode,
                Data = ReadBodyFromRequest(httpContext.Request),
                ErrorMessage = JsonConvert.SerializeObject(ex, Formatting.Indented),
                ClientIp = httpContext.Request?.HttpContext.Connection.RemoteIpAddress.ToString()
            };
            await _errorRepository.LogError(error);
        }
    }
}
