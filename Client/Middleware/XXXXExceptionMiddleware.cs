using Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.Middleware
{
    public class XXXXExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<XXXXExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public XXXXExceptionMiddleware(RequestDelegate next, ILogger<XXXXExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //oyr logging system is the concole
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new XXXXExcecptionError((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new XXXXExcecptionError((int)HttpStatusCode.InternalServerError);

                context.Response.Redirect("Errors/");
                
                //var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                //var json = JsonSerializer.Serialize(response, options);
                //await context.Response.WriteAsync(json);

            }
        }
    }
}
