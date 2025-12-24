using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

namespace VisualArchitect.Api.Orchestration.Abstractions.Application.UseCases;

public sealed record IdentityExistsQuery(Guid IdentityId) : IQuery<bool>;
