using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs;

public interface ICqrsRequest<TResponse> : IRequest<TResponse>
    where TResponse : ICqrsResponse
{
}
