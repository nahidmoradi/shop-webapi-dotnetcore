using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); 
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled exception occurred while processing request {Path}", context.Request.Path);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred. Please try again later."
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
