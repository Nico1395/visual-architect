namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

public interface ICommandResponse : ICqrsResponse
{
}

public interface ICommandResponse<TData> : ICommandResponse, ICqrsResponse<TData>
{
}
