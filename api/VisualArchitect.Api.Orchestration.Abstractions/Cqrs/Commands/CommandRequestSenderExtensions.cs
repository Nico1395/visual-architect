using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

public static class CommandRequestSenderExtensions
{
    public static Task<ICommandResponse> SendAsync<TCommand>(this IRequestSender sender, TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        return sender.SendAsync<TCommand, ICommandResponse>(command, cancellationToken);
    }
}
