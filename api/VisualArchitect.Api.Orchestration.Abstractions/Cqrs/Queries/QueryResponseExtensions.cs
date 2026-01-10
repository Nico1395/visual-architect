namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

public static class QueryResponseExtensions
{
    public static IQueryResponse<TDestination> Map<TSource, TDestination>(this IQueryResponse<TSource> response, Func<TSource, TDestination> map)
    {
        CqrsResponseStatus status = response.Status;
        TDestination? data = default;

        // Only map the data if its code 200 and data is actually present
        if (response.IsOK_200() && response.Data != null)
            data = map(response.Data);
        else if (response.IsOK_200() && response.Data == null)
            status = CqrsResponseStatus.BadRequest_400;

        return new QueryResponse<TDestination>(status)
        {
            Data = data,
            Metadata = response.Metadata,
            Message = response.Message,
        };
    }
}
