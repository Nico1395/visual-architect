namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

internal sealed class QueryResponseBuilder<TData>(CqrsResponseStatus status, TData? data = default) : IQueryResponseBuilder<TData>
{
    private string? _message;
    private Dictionary<string, object> _metadata = [];

    public IQueryResponseBuilder<TData> WithMessage(string message)
    {
        _message = message;
        return this;
    }

    public IQueryResponseBuilder<TData> WithMetadata(string key, object value)
    {
        _metadata[key] = value;
        return this;
    }
    
    public IQueryResponse<TData> Build()
    {
        return new QueryResponse<TData>()
        {
            Status = status,
            Data = data,
            Message = _message,
            Metadata = _metadata
        };
    }
}
