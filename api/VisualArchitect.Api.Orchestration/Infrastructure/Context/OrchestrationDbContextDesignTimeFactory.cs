using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using VisualArchitect.Api.Orchestration.Abstractions.Configuration.Options;
using VisualArchitect.Api.Orchestration.Abstractions.Exceptions;

namespace VisualArchitect.Api.Orchestration.Infrastructure.Context;

public sealed class OrchestrationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<OrchestrationDbContext>
{
    public OrchestrationDbContext CreateDbContext(string[] args)
    {
        // Create a configuration to access appsettings
        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables();
        var configuration = configurationBuilder.Build();

        //  Create connection strings
        var connectionStrings = new ConnectionStringsOptions(configuration.GetConnectionString("Default") ?? throw new MissingConfigurationException("ConnectionStrings:Default"));
        var options = Options.Create(connectionStrings);

        // Create the DbContext
        return new OrchestrationDbContext(options, []);
    }
}
