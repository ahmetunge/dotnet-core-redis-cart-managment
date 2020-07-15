using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }

        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            if (ex.GetType() == typeof(ValidationException))
            {
                message = "";
                var exception = (ValidationException)ex;
                if (exception != null && exception.Errors.Count() > 0)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (var error in exception.Errors)
                    {
                        stringBuilder.Append(error.ErrorMessage);
                    }
                    message = stringBuilder.ToString();
                }



                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                statusCode = HttpStatusCode.UnprocessableEntity;
            }
            var errorResult = new
            {
                Message = message,
                StatusCode = statusCode,
                MessageDetail = ex.Message
            };
            return httpContext.Response.WriteAsync(GetStringError(errorResult));
        }

        private string GetStringError(object entity)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            return JsonSerializer.Serialize(entity, options);
        }
    }
}
