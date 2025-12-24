using VisualArchitect.Api.Orchestration.Abstractions.Application.UseCases;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Application.UseCases;

public static class GetPreferences
{
    private sealed class GetPreferencesQueryHandler(
        IIdentityPreferenceReadRepository _identityPreferenceReadRepository,
        IPreferenceReadRepository _preferenceReadRepository,
        IMediator _mediator) : IQueryHandler<GetPreferencesQuery, IReadOnlyList<GetPreferencesQuery.Prefence>>
    {
        public async Task<IQueryResponse<IReadOnlyList<GetPreferencesQuery.Prefence>>> HandleAsync(GetPreferencesQuery request, CancellationToken cancellationToken)
        {
            var keys = request.Keys ?? [];

            // Does the identity even exists?
            var identityExistsResponse = await _mediator.SendAsync<IdentityExistsQuery, bool>(new IdentityExistsQuery(request.IdentityId), cancellationToken);
            if (identityExistsResponse.ResultedInFalse())
                return QueryResponseFactory.BadRequest_400<IReadOnlyList<GetPreferencesQuery.Prefence>>().Build();

            // Get all preferences and if none even exists, we wont have to search any further -> thats a faulty request.
            var preferences = await _preferenceReadRepository.GetByKeysOrAllAsync(keys, cancellationToken);
            if (preferences.Count == 0)
                return QueryResponseFactory.BadRequest_400<IReadOnlyList<GetPreferencesQuery.Prefence>>().Build();

            var identityPreferences = await _identityPreferenceReadRepository.GetByKeysOrAllAsync(request.IdentityId, keys, cancellationToken);
            var totalPreferences = new List<GetPreferencesQuery.Prefence>();

            // Basically:
            // 1. Matchmake identity and key
            // 2. Get the identity preference's value and if not set yet, get the default value from the preference
            // 3. If it has never set, UpdatedAt should be null
            foreach (var preference in preferences)
            {
                var identityPreference = identityPreferences.SingleOrDefault(i => i.PreferenceId == preference.Id);
                totalPreferences.Add(new GetPreferencesQuery.Prefence(
                    request.IdentityId,
                    preference.Key,
                    identityPreference != null ? identityPreference.Value : preference.DefaultValue,
                    preference.DefaultValue,
                    identityPreference?.UpdatedAt
                ));
            }

            return QueryResponseFactory.OK_200(totalPreferences as IReadOnlyList<GetPreferencesQuery.Prefence>).Build();
        }
    }
}
