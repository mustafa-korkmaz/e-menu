using Application.Constants;
using Application.Exceptions;
using System.Net;

namespace Presentation.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
            catch (Exception exception)
            {
                var response = context.Response;

                response.ContentType = "application/json";

                var message = exception.Message;

                switch (exception)
                {
                    case ValidationException _:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case RecordNotFoundException _:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        message = ErrorMessages.RecordNotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        message = ErrorMessages.InternalServerError;
                        break;
                }

                _logger.LogError(exception, message);

                await response.WriteAsync(message);
            }
        }
    }
}
