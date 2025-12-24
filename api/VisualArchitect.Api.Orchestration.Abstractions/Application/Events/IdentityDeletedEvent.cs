using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Orchestration.Abstractions.Application.Events;

public sealed record IdentityDeletedEvent(Guid IdentityId) : INotification;
