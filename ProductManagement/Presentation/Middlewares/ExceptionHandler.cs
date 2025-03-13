using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using AutoMapper;

namespace ProductManagement.Presentation.Middlewares;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    
    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var exceptionDetails = new ExceptionDetails()
            {
                Message = error.Message, // сообщение исключения
                Type = error.GetType().Name, // название исключения
            };
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = error switch
            {
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                ValidationException => (int)HttpStatusCode.BadRequest,
                AutoMapperMappingException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };
            var result = JsonSerializer.Serialize(exceptionDetails);
            await response.WriteAsync(result);
        }
    }
}