namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

public interface IQueryResponseBuilder<TData>
{
    IQueryResponseBuilder<TData> WithMetadata(string key, object value);
    IQueryResponseBuilder<TData> WithMessage(string message);
    IQueryResponse<TData> Build();
}
