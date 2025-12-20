using VisualArchitect.Api.Authentication.Presentation;
using VisualArchitect.Api.Authentication.Presentation.Middleware;

namespace VisualArchitect.Api;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddVisualArchitect(builder.Configuration);
        builder.Configuration.AddEnvironmentVariables();

        var app = builder.Build();

        app.UseCors("virtual-architect-client");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<CsrfMiddleware>();

        app.MapAuthentication();

        app.Run();
    }
}
