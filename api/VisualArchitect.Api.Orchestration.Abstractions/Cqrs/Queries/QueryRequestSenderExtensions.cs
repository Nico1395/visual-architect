using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

public static class QueryRequestSenderExtensions
{
    public static Task<IQueryResponse<TData>> SendAsync<TQuery, TData>(this IRequestSender sender, TQuery query, CancellationToken cancellationToken = default)
        where TQuery : IQuery<TData>
    {
        return sender.SendAsync<TQuery, IQueryResponse<TData>>(query, cancellationToken);
    }
}
