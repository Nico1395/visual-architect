namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

public sealed class CommandResponse : CqrsResponse, ICommandResponse
{
    public CommandResponse(CqrsResponseStatus status)
        : base(status)
    {
    }

    public CommandResponse(CqrsResponseStatus status, IReadOnlyDictionary<string, object>? metadata, string? message)
        : base(status, metadata, message)
    {
    }
}

public sealed class CommandResponse<TData> : CqrsResponse<TData>, ICommandResponse<TData>
{
    public CommandResponse(CqrsResponseStatus status)
        : base(status)
    {
    }

    public CommandResponse(CqrsResponseStatus status, IReadOnlyDictionary<string, object>? metadata, string? message, TData data)
        : base(status, metadata, message, data)
    {
    }
}
