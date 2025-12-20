using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using VisualArchitect.Api.Orchestration.Abstractions.Configuration.Options;
using VisualArchitect.Api.Orchestration.Abstractions.Exceptions;

namespace VisualArchitect.Api.Orchestration.Infrastructure.Context;

public sealed class OrchestrationDbContext(IOptions<ConnectionStringsOptions> _connectionStrings, DbContextConfiguration dbContextConfiguration, IEnumerable<IInterceptor> _interceptors) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionStrings.Value.Default ?? throw new MissingConfigurationException("ConnectionStrings:Default"));
        optionsBuilder.AddInterceptors(_interceptors);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var assembly in dbContextConfiguration.AssembliesToScan)
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}
