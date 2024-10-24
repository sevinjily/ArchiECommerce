
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;
using System.Text.Json;

namespace WebAPI.Middlewares
{
    public class GlobalHandlingExceptionMiddleware : IMiddleware
    {
		private readonly ILogger<GlobalHandlingExceptionMiddleware> _logger;

        public GlobalHandlingExceptionMiddleware(ILogger<GlobalHandlingExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            //catch (ValidationException ex) // Handling FluentValidation errors
            //{
            //    Log.Warning(ex, ex.Message);
            //    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            //    var validationErrors = ex.Errors.Select(err => new
            //    {
            //        Field = err.PropertyName,
            //        Error = err.ErrorMessage
            //    });

            //    var problemDetails = new
            //    {
            //        Status = (int)HttpStatusCode.BadRequest,
            //        Title = "Validation Error",
            //        Errors = validationErrors
            //    };

            //    var json = JsonSerializer.Serialize(problemDetails);
            //    context.Response.ContentType = "application/json";
            //    await context.Response.WriteAsync(json);
            //}

            catch (HttpRequestException ex)
            {
                //_logger.LogError(ex, ex.Message);
                Log.Error(ex, ex.Message);
                context.Response.StatusCode = (int)ex.StatusCode;

                ProblemDetails details = new()
                {
                    Status = (int)ex.StatusCode,
                    Title = ex.HttpRequestError.ToString(),
                    Type = ex.InnerException.Message,
                    Detail = ex.Message
                };

                var json = JsonSerializer.Serialize(details);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex,ex.Message);
                Log.Error(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ProblemDetails details = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Server error",
                    Type = "https://httpstatuses.com/500",
                    Detail = ex.Message

                };
                var json = JsonSerializer.Serialize(details);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
        }
    }
}
