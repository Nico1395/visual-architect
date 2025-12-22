using System.Transactions;
using VisualArchitect.Api.Orchestration.Abstractions.Infrastructure.Transactions;

namespace VisualArchitect.Api.Orchestration.Infrastructure.Transactions;

public sealed class TransactionFactory : ITransactionFactory
{
    public ITransaction Create()
    {
        return new Transaction();
    }
}

public sealed class Transaction : ITransaction
{
    private readonly TransactionScope _scope = new(TransactionScopeAsyncFlowOption.Enabled);

    public Task CommitAsync(CancellationToken cancellationToken)
    {
        _scope.Complete();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
