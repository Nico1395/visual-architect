using System.Reflection;

namespace VisualArchitect.Api.Orchestration.Infrastructure.Context;

public sealed class DbContextConfiguration
{
    public required IReadOnlyList<Assembly> AssembliesToScan { get; init; }
}
