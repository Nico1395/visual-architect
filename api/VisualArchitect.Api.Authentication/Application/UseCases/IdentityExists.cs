using VisualArchitect.Api.Authentication.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Application.UseCases;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

namespace VisualArchitect.Api.Authentication.Application.UseCases;

public static class IdentityExists
{
    private sealed class IdentityExistsQueryHandler(IIdentityReadRepository _identityReadRepository) : IQueryHandler<IdentityExistsQuery, bool>
    {
        public async Task<IQueryResponse<bool>> HandleAsync(IdentityExistsQuery request, CancellationToken cancellationToken)
        {
            var exists = await _identityReadRepository.ExistsAsync(request.IdentityId, cancellationToken);
            return QueryResponseFactory.OK_200(exists).Build();
        }
    }
}
