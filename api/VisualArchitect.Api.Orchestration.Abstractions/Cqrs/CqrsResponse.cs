namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs;

public class CqrsResponse : ICqrsResponse
{
    public CqrsResponse(CqrsResponseStatus status)
    {
        Status = status;
    }

    public CqrsResponse(CqrsResponseStatus status, IReadOnlyDictionary<string, object>? metadata, string? message)
    {
        Status = status;
        Metadata = metadata ?? new Dictionary<string, object>();
        Message = message;
    }

    public CqrsResponseStatus Status { get; }
    public IReadOnlyDictionary<string, object> Metadata { get; init; } = new Dictionary<string, object>();
    public string? Message { get; init; }
}

public class CqrsResponse<TData> : CqrsResponse, ICqrsResponse<TData>
{
    public CqrsResponse(CqrsResponseStatus status)
        : base(status)
    {
    }

    public CqrsResponse(CqrsResponseStatus status, IReadOnlyDictionary<string, object>? metadata, string? message, TData? data)
        : base(status, metadata, message)
    {
        Data = data;
    }

    public TData? Data { get; init; }
}
