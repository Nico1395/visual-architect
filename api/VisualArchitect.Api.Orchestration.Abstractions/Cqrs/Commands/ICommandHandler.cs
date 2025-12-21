using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, ICommandResponse>
    where TCommand : ICommand
{
}
