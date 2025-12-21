namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

public sealed class QueryResponse<TData> : CqrsResponse<TData>, IQueryResponse<TData>
{
}
