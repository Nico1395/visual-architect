using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, ICommandResponse>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TData> : IRequestHandler<TCommand, ICommandResponse<TData>>
    where TCommand : ICommand<TData>
{
}
