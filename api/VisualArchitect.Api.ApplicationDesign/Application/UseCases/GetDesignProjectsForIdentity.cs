using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

namespace VisualArchitect.Api.ApplicationDesign.Application.UseCases;

public static class GetDesignProjectsForIdentity
{
    public sealed record GetDesignProjectsForIdentityQuery(
        Guid IdentityId,
        bool IncludeDesignTasks,
        bool IncludeDesigns) : IQuery<List<DesignProject>>;

    private sealed class GetDesignProjectsForIdentityQueryHandler(
        IDesignProjectReadRepository _designProjectReadRepository) : IQueryHandler<GetDesignProjectsForIdentityQuery, List<DesignProject>>
    {
        public async Task<IQueryResponse<List<DesignProject>>> HandleAsync(GetDesignProjectsForIdentityQuery request, CancellationToken cancellationToken)
        {
            var projects = await _designProjectReadRepository.GetAllForIdentityAsync(request.IdentityId, request.IncludeDesignTasks, request.IncludeDesigns, cancellationToken);
            return QueryResponseFactory.OK_200(projects).Build();
        }
    }
}
