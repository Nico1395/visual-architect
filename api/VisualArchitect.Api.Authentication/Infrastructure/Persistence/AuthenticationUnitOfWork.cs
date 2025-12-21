using VisualArchitect.Api.Authentication.Application.Persistence;
using VisualArchitect.Api.Orchestration.Abstractions.Application.Persistence;

namespace VisualArchitect.Api.Authentication.Infrastructure.Persistence;

internal sealed class AuthenticationUnitOfWork(IUnitOfWork _unitOfWork) : IAuthenticationUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.CommitAsync(cancellationToken);
    }
}
