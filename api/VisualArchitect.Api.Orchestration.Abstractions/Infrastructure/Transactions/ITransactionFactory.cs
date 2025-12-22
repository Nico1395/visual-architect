namespace VisualArchitect.Api.Orchestration.Abstractions.Infrastructure.Transactions;

public interface ITransactionFactory
{
    ITransaction Create();
}
