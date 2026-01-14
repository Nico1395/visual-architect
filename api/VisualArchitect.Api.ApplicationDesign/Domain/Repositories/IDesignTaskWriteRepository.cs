namespace VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

public interface IDesignTaskWriteRepository
{
    Task AddAsync(DesignTask designTask, CancellationToken cancellationToken);
}
