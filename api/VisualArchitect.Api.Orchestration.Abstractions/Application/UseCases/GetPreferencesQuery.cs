using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

namespace VisualArchitect.Api.Orchestration.Abstractions.Application.UseCases;

public sealed record GetPreferencesQuery(Guid IdentityId, List<string> Keys) : IQuery<IReadOnlyList<GetPreferencesQuery.Prefence>>
{
    public sealed record Prefence(
        Guid IdentityId,
        string Key,
        string? Value,
        DateTime UpdatedAt);
}
