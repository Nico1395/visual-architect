using Microsoft.EntityFrameworkCore;

namespace VisualArchitect.Api.Authentication.Tests.Authentication;

internal sealed class AuthenticationTestDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assemblies = AuthenticationTestFixture.YieldAssemblies();
        foreach (var assembly in assemblies)
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}
