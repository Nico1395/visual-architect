namespace VisualArchitect.Api.Orchestration.Abstractions.Infrastructure.Transactions;

public interface ITransaction : IDisposable
{
    Task CommitAsync(CancellationToken cancellationToken);
}
