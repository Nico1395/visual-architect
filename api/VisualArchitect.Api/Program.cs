using VisualArchitect.Api.Authentication.Domain.Constants;
using VisualArchitect.Api.Authentication.Presentation.Middleware;
using VisualArchitect.Api.Orchestration.Infrastructure.Context;
using Scalar.AspNetCore;

namespace VisualArchitect.Api;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddVisualArchitect(builder.Configuration);
        builder.Configuration.AddEnvironmentVariables();

        builder.Services.AddOpenApi();

        var app = builder.Build();

        app.UseCors(AuthenticationConstants.Cors.VirtualArchitectClient.PolicyName);
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<CsrfMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference("/docs", (options, httpContext) =>
            {
                options.AddDocument("v1", "Visual Architect Public API Documentation");
            });
        }

        app.MapVisualArchitect();

        app.RunMigrations();
        app.Run();
    }
}
