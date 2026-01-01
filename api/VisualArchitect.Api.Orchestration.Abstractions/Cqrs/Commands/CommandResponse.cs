namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

public sealed class CommandResponse : CqrsResponse, ICommandResponse
{
}

public sealed class CommandResponse<TData> : CqrsResponse<TData>, ICommandResponse<TData>
{
}
