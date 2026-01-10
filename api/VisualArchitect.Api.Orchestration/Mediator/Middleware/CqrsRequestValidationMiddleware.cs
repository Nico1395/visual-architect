using VisualArchitect.Api.Orchestration.Abstractions.Cqrs;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;

namespace VisualArchitect.Api.Orchestration.Mediator.Middleware;

internal sealed class CqrsRequestValidationMiddleware<TRequest, TResponse> : CqrsRequestValidationMiddlewareBase, IRequestMiddleware<TRequest, TResponse>
    where TRequest : ICqrsRequest<TResponse>
    where TResponse : ICqrsResponse
{
    public async Task<TResponse> InterceptAsync(TRequest request, RequestHandlerDelegate<TResponse> nextStep, CancellationToken cancellationToken)
    {
        var result = ValidateRequest(request);
        if (result == null)
            return await nextStep.Invoke();

        return CqrsValidationResponseFactory.BadRequest_400<TResponse>(result.Errors);
    }
}
