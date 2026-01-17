namespace VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

public interface IDesignReadRepository
{
    Task<Design?> GetByIdAsync(Guid designId, CancellationToken cancellationToken);
}
