using VisualArchitect.Api.ApplicationDesign.Application.Persistence;
using VisualArchitect.Api.Orchestration.Abstractions.Application.Persistence;

namespace VisualArchitect.Api.ApplicationDesign.Infrastructure.Persistence;

internal sealed class ApplicationDesignUnitOfWork(IUnitOfWork _unitOfWork) : IApplicationDesignUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.CommitAsync(cancellationToken);
    }
}
