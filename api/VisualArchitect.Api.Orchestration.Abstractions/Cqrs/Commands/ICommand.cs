namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

public interface ICommand : ICqrsRequest<ICommandResponse>
{
}

public interface ICommand<TData> : ICqrsRequest<ICommandResponse<TData>>
{
}
