using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using TaskManagement.Application.Exceptions;

namespace TaskManagement.API.ExceptionHandler
{
    public sealed class GlobalExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "Resource not found");
                await WriteProblem(context, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized");
                await WriteProblem(context, StatusCodes.Status401Unauthorized, "Unauthorized");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await WriteProblem(context, StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

        private static async Task WriteProblem(HttpContext ctx, int status, string detail, object? errors = null)
        {
            ctx.Response.ContentType = "application/problem+json";
            ctx.Response.StatusCode = status;
            var problem = new ProblemDetails
            {
                Status = status,
                Title = ReasonPhrases.GetReasonPhrase(status),
                Detail = detail,
                Instance = ctx.TraceIdentifier
            };

            if (errors != null)
            {
                problem.Extensions["errors"] = errors;
            }

            await ctx.Response.WriteAsJsonAsync(problem);
        }
    }
}
