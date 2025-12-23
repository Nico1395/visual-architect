using VisualArchitect.Api.Orchestration.Abstractions.Application.UseCases;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Application.UseCases;

public static class GetPreferences
{
    private sealed class GetPreferencesQueryHandler(
        IIdentitySettingReadRepository _identitySettingReadRepository,
        IMediator _mediator) : IQueryHandler<GetPreferencesQuery, IReadOnlyList<GetPreferencesQuery.Prefence>>
    {
        public async Task<IQueryResponse<IReadOnlyList<GetPreferencesQuery.Prefence>>> HandleAsync(GetPreferencesQuery request, CancellationToken cancellationToken)
        {
            var identityExistsResponse = await _mediator.SendAsync<IdentityExistsQuery, bool>(new IdentityExistsQuery(request.IdentityId), cancellationToken);
            if (identityExistsResponse.ResultedInFalse())
                return QueryResponseFactory.Unauthorized_401<IReadOnlyList<GetPreferencesQuery.Prefence>>().Build();

            var identitySettings = await _identitySettingReadRepository.GetForIdentityByKeysAsync(request.IdentityId, request.Keys, cancellationToken);
            var preferences = identitySettings
                .Select(i => new GetPreferencesQuery.Prefence(i.Value.IdentityId, i.Key, i.Value.Value, i.Value.UpdatedAt))
                .ToList();

            return QueryResponseFactory.OK_200(preferences as IReadOnlyList<GetPreferencesQuery.Prefence>).Build();
        }
    }
}
