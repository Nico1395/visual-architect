using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

namespace VisualArchitect.Api.ApplicationDesign.Application.UseCases;

public sealed class GetDesignTaskByProjectIdAndNumber
{
    public sealed record GetDesignTaskByProjectIdAndNumberQuery(Guid ProjectId, long TaskNumber, bool IncludeDesigns) : IQuery<DesignTask>;

    private sealed class GetDesignTaskByProjectIdAndNumberQueryHandler(IDesignTaskReadRepository _designTaskReadRepository) : IQueryHandler<GetDesignTaskByProjectIdAndNumberQuery, DesignTask>
    {
        public async Task<IQueryResponse<DesignTask>> HandleAsync(GetDesignTaskByProjectIdAndNumberQuery request, CancellationToken cancellationToken)
        {
            var task = await _designTaskReadRepository.GetByProjectIdAndNumberAsync(request.ProjectId, request.TaskNumber, request.IncludeDesigns, cancellationToken);
            if (task == null)
                return QueryResponseFactory.BadRequest_400<DesignTask>().Build();

            return QueryResponseFactory.OK_200(task).Build();
        }
    }
}
