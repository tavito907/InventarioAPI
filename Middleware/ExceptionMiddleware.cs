using InventarioAPI.DTOs.Error;
using InventarioAPI.Exceptions;

namespace InventarioAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                string error = "InternalServerError";

                if (ex is ApiException apiException)
                {
                    context.Response.StatusCode = apiException.StatusCode;
                    error = apiException.Error;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                _logger.LogError(ex, ex.Message);

                await context.Response.WriteAsJsonAsync(new ErrorResponseDto
                {
                    Status = context.Response.StatusCode,
                    Error = error,
                    Mensaje = ex.Message,
                    Timestamp = DateTime.UtcNow
                });
            }
        }
    }
}
