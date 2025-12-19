namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs;

public abstract class CqrsResponse : ICqrsResponse
{
    public required CqrsResponseStatus Status { get; init; }
    public IReadOnlyDictionary<string, object> Metadata { get; init; } = new Dictionary<string, object>();
}

public abstract class CqrsResponse<TData> : CqrsResponse, ICqrsResponse<TData>
{
    public TData? Data { get; init; }
}
