using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

public interface IQuery<TData> : IRequest<IQueryResponse<TData>>
{
}
