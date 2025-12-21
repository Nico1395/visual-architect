using VisualArchitect.Api.Authentication.Domain;
using VisualArchitect.Api.Authentication.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

namespace VisualArchitect.Api.Authentication.Application.UseCases;

internal static class GetIdentity
{
    public sealed record GetIdentityQuery(Guid IdentityId) : IQuery<Identity>;

    private sealed class GetIdentityQueryHandler(IIdentityReadRepository _identityReadRepository) : IQueryHandler<GetIdentityQuery, Identity>
    {
        public async Task<IQueryResponse<Identity>> HandleAsync(GetIdentityQuery request, CancellationToken cancellationToken)
        {
            var identity = await _identityReadRepository.GetByIdAsync(request.IdentityId, cancellationToken);
            return QueryResponseFactory.FromData(identity);
        }
    }
}
