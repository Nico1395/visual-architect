namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

internal sealed class CommandResponseBuilder(CqrsResponseStatus status) : ICommandResponseBuilder
{
    private string? _message;
    private Dictionary<string, object> _metadata = [];

    public ICommandResponseBuilder WithMessage(string message)
    {
        _message = message;
        return this;
    }

    public ICommandResponseBuilder WithMetadata(string key, object value)
    {
        _metadata[key] = value;
        return this;
    }
    
    public ICommandResponse Build()
    {
        return new CommandResponse()
        {
            Status = status,
            Message = _message,
            Metadata = _metadata
        };
    }
}
