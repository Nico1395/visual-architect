namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

public sealed class QueryResponse<TData> : CqrsResponse<TData>, IQueryResponse<TData>
{
    public QueryResponse(CqrsResponseStatus status)
        : base(status)
    {
    }

    public QueryResponse(CqrsResponseStatus status, IReadOnlyDictionary<string, object>? metadata, string? message, TData data)
        : base(status, metadata, message, data)
    {
    }
}
