using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

public interface ICommand : IRequest<ICommandResponse>
{
}
