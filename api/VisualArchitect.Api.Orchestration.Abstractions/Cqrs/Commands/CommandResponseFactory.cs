namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

public static class CommandResponseFactory
{
    public static ICommandResponseBuilder OK_200() => new CommandResponseBuilder(CqrsResponseStatus.OK_200);
    public static ICommandResponseBuilder Created_201() => new CommandResponseBuilder(CqrsResponseStatus.Created_201);
    public static ICommandResponseBuilder Accepted_202() => new CommandResponseBuilder(CqrsResponseStatus.Accepted_202);
    public static ICommandResponseBuilder NoContent_204() => new CommandResponseBuilder(CqrsResponseStatus.NoContent_204);
    public static ICommandResponseBuilder BadRequest_400() => new CommandResponseBuilder(CqrsResponseStatus.BadRequest_400);
    public static ICommandResponseBuilder Unauthorized_401() => new CommandResponseBuilder(CqrsResponseStatus.Unauthorized_401);
    public static ICommandResponseBuilder Forbidden_403() => new CommandResponseBuilder(CqrsResponseStatus.Forbidden_403);
    public static ICommandResponseBuilder NotFound_404() => new CommandResponseBuilder(CqrsResponseStatus.NotFound_404);
    public static ICommandResponseBuilder NotAcceptable_406() => new CommandResponseBuilder(CqrsResponseStatus.NotAcceptable_406);
    public static ICommandResponseBuilder Conflict_409() => new CommandResponseBuilder(CqrsResponseStatus.Conflict_409);
    public static ICommandResponseBuilder InternalServerError_500() => new CommandResponseBuilder(CqrsResponseStatus.InternalServerError_500);
    public static ICommandResponseBuilder NotImplemented_501() => new CommandResponseBuilder(CqrsResponseStatus.NotImplemented_501);
    public static ICommandResponseBuilder ServiceUnavailable_503() => new CommandResponseBuilder(CqrsResponseStatus.ServiceUnavailable_503);
}
