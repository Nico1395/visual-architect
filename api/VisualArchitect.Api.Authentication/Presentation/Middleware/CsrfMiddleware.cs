using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace VisualArchitect.Api.Authentication.Presentation.Middleware;

public sealed class CsrfMiddleware(IAntiforgery _antiforgery) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Prüfe nur für Methoden, die CSRF-relevant sind
        if (HttpMethods.IsPost(context.Request.Method) ||
            HttpMethods.IsPut(context.Request.Method) ||
            HttpMethods.IsDelete(context.Request.Method) ||
            HttpMethods.IsPatch(context.Request.Method))
        {
            try
            {
                await _antiforgery.ValidateRequestAsync(context);
            }
            catch (AntiforgeryValidationException)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Invalid CSRF token");
                return;
            }
        }

        await next(context);
    }
}
