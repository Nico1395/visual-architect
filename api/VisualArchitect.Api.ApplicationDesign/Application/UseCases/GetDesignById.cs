using VisualArchitect.Api.ApplicationDesign.Domain;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

namespace VisualArchitect.Api.ApplicationDesign.Application.UseCases;

public sealed class GetDesignById
{
    public sealed record GetDesignByIdQuery(Guid DesignId) : IQuery<Design>;

    private sealed class GetDesignByIdQueryHandler(IDesignReadRepository _designReadRepository) : IQueryHandler<GetDesignByIdQuery, Design>
    {
        public async Task<IQueryResponse<Design>> HandleAsync(GetDesignByIdQuery request, CancellationToken cancellationToken)
        {
            var design = await _designReadRepository.GetByIdAsync(request.DesignId, cancellationToken);
            if (design == null)
                return QueryResponseFactory.NotFound_404<Design>().Build();

            return QueryResponseFactory.OK_200(design).Build();
        }
    }
}
