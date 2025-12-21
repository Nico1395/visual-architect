namespace VisualArchitect.Api.Orchestration.Abstractions.Application.Persistence;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}
