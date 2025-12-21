namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

public static class CommandResponseFactory
{
    public static ICommandResponse OK_200()
    {
        return new CommandResponse()
        {
            Status = CqrsResponseStatus.OK_200,
        };
    }

    public static ICommandResponse Created_201()
    {
        return new CommandResponse()
        {
            Status = CqrsResponseStatus.Created_201,
        };
    }

    public static ICommandResponse Accepted_202()
    {
        return new CommandResponse()
        {
            Status = CqrsResponseStatus.Accepted_202,
        };
    }

    public static ICommandResponse NoContent_204()
    {
        return new CommandResponse()
        {
            Status = CqrsResponseStatus.NoContent_204,
        };
    }

    public static ICommandResponse BadRequest_400()
    {
        return new CommandResponse()
        {
            Status = CqrsResponseStatus.BadRequest_400,
        };
    }
}
