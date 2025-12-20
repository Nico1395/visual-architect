using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

public interface IQueryHandler<TQuery, TData> : IRequestHandler<TQuery, IQueryResponse<TData>>
    where TQuery : IQuery<TData>
{
}
