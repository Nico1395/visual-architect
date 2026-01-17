namespace VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

public interface IDesignTaskWriteRepository
{
    Task AddAsync(DesignTask designTask, CancellationToken cancellationToken);
    Task UpdateAsync(DesignTask task, CancellationToken cancellationToken);
    Task DeletAsync(DesignTask task, CancellationToken cancellationToken);
}
