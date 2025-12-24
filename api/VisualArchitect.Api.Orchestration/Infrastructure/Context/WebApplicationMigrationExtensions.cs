using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VisualArchitect.Api.Orchestration.Infrastructure.Context;

public static class WebApplicationMigrationExtensions
{
    public static void RunMigrations(this WebApplication app)
    {
        var runOnStartup = app.Configuration.GetValue<bool>("RunMigrationsOnStartup");
        if (!runOnStartup)
            return;

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<DbContext>();

        context.Database.Migrate();
    }
}
