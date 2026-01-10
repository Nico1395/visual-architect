namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

public interface IQuery<TData> : ICqrsRequest<IQueryResponse<TData>>
{
}
