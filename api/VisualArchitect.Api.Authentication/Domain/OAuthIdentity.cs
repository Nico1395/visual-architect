using VisualArchitect.Api.Orchestration.Abstractions.Domain;

namespace VisualArchitect.Api.Authentication.Domain;

internal sealed class OAuthIdentity : ICreated
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string ExternalId { get; init; }
    public required int ProviderId { get; init; }
    public OAuthProvider? Provider { get; init; }
    public required Guid IdentityId { get; init; }
    public Identity? Identity { get; init; }
    public DateTime CreatedAt { get; set; }
}
