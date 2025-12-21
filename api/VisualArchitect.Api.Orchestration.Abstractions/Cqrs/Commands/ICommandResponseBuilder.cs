namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;

public interface ICommandResponseBuilder
{
    ICommandResponseBuilder WithMetadata(string key, object value);
    ICommandResponseBuilder WithMessage(string message);
    ICommandResponse Build();
}

