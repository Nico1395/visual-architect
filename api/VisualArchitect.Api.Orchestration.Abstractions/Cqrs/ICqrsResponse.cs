namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs;

public interface ICqrsResponse
{
    CqrsResponseStatus Status { get; }
    IReadOnlyDictionary<string, object> Metadata { get; }
    string? Message { get; }
}

public interface ICqrsResponse<TData> : ICqrsResponse
{
    TData? Data { get; }
}
