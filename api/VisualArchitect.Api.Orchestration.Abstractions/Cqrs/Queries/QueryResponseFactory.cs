namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

public static class QueryResponseFactory
{
    public static IQueryResponseBuilder<TData> OK_200<TData>(TData data) => new QueryResponseBuilder<TData>(CqrsResponseStatus.OK_200, data);
    public static IQueryResponseBuilder<TData> Created_201<TData>() => new QueryResponseBuilder<TData>(CqrsResponseStatus.Created_201);
    public static IQueryResponseBuilder<TData> Accepted_202<TData>() => new QueryResponseBuilder<TData>(CqrsResponseStatus.Accepted_202);
    public static IQueryResponseBuilder<TData> NoContent_204<TData>() => new QueryResponseBuilder<TData>(CqrsResponseStatus.NoContent_204);
    public static IQueryResponseBuilder<TData> BadRequest_400<TData>() => new QueryResponseBuilder<TData>(CqrsResponseStatus.BadRequest_400);
    public static IQueryResponseBuilder<TData> Unauthorized_401<TData>() => new QueryResponseBuilder<TData>(CqrsResponseStatus.Unauthorized_401);
    public static IQueryResponseBuilder<TData> Forbidden_403<TData>() => new QueryResponseBuilder<TData>(CqrsResponseStatus.Forbidden_403);
    public static IQueryResponseBuilder<TData> NotFound_404<TData>() => new QueryResponseBuilder<TData>(CqrsResponseStatus.NotFound_404);
    public static IQueryResponseBuilder<TData> NotAcceptable_406<TData>() => new QueryResponseBuilder<TData>(CqrsResponseStatus.NotAcceptable_406);
    public static IQueryResponseBuilder<TData> Conflict_409<TData>() => new QueryResponseBuilder<TData>(CqrsResponseStatus.Conflict_409);
    public static IQueryResponseBuilder<TData> InternalServerError_500<TData>() => new QueryResponseBuilder<TData>(CqrsResponseStatus.InternalServerError_500);
    public static IQueryResponseBuilder<TData> NotImplemented_501<TData>() => new QueryResponseBuilder<TData>(CqrsResponseStatus.NotImplemented_501);
    public static IQueryResponseBuilder<TData> ServiceUnavailable_503<TData>() => new QueryResponseBuilder<TData>(CqrsResponseStatus.ServiceUnavailable_503);
}
