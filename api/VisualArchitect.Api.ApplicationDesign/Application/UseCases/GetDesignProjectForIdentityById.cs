using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

namespace VisualArchitect.Api.ApplicationDesign.Application.UseCases;

public static class GetDesignProjectForIdentityById
{
    public sealed record GetDesignProjectForIdentityByIdQuery(Guid IdentityId, Guid ProjectId, bool IncludeDesignTasks, bool IncludeDesigns) : IQuery<DesignProject>;

    private sealed class GetDesignProjectForIdentityByIdQueryHandler(
        IDesignProjectReadRepository _designProjectReadRepository) : IQueryHandler<GetDesignProjectForIdentityByIdQuery, DesignProject>
    {
        public async Task<IQueryResponse<DesignProject>> HandleAsync(GetDesignProjectForIdentityByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _designProjectReadRepository.GetForIdentityByIdAsync(
                request.IdentityId,
                request.ProjectId,
                request.IncludeDesignTasks,
                request.IncludeDesigns,
                cancellationToken);

            if (project == null)
                return QueryResponseFactory.NotFound_404<DesignProject>().Build();      // This means if a user accesses the project of another user for any reason, he will see it as "not found, it doesnt exist".

            return QueryResponseFactory.OK_200(project).Build();
        }
    }
}
