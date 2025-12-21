using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using VisualArchitect.Api.Orchestration.Abstractions.Configuration.Options;

namespace VisualArchitect.Api.Orchestration.Infrastructure.Context;

public sealed class OrchestrationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<OrchestrationDbContext>
{
    public OrchestrationDbContext CreateDbContext(string[] args)
    {
        //  Create connection strings
        var connectionStrings = new ConnectionStringsOptions("Host=localhost;Password=dev;Persist Security Info=True;Username=dev;Database=va_dev");
        var options = Options.Create(connectionStrings);

        // Configure assemblies
        var dbContextConfiguration = new DbContextConfiguration() { AssembliesToScan = DependencyInjection.YieldAssemblies().ToList() };

        // Create the DbContext
        return new OrchestrationDbContext(
            options,
            dbContextConfiguration,
            _interceptors: []);
    }
}
