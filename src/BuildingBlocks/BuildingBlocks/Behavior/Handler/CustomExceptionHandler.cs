using BuildingBlocks.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Behavior.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, exception.Message, DateTime.UtcNow);

            (string Detail, string Title, int StatusCode) problemDetails = exception switch
            {
                InternalServerException =>
                  (
                  exception.Message,
                  exception.GetType().Name,
                  context.Response.StatusCode = StatusCodes.Status500InternalServerError
                  ),
                ValidationException =>
                  (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status400BadRequest
                  ),
                BadRequestException =>
                  (
                    exception.Message,
                    exception.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status400BadRequest
                  ),
                NotFoundException =>
                (
                  exception.Message,
                  exception.GetType().Name,
                  context.Response.StatusCode = StatusCodes.Status404NotFound
                ),
                _ =>
                (
                  exception.Message,
                  exception.GetType().Name,
                  context.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };

            var problemDet = new ProblemDetails
            {
                Title = problemDetails.Title,
                Status = problemDetails.StatusCode,
                Detail = problemDetails.Detail
            };

            problemDet.Extensions.Add("traceId", context.TraceIdentifier);

            if(exception is InternalServerException internalServerException )
            {
                problemDet.Extensions.Add("details", internalServerException.Details);
            }            

            await context.Response.WriteAsJsonAsync(problemDet, cancellationToken);
            return true;
        }
    }
}
