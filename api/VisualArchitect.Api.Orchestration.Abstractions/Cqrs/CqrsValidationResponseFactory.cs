using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;

namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs;

public static class CqrsValidationResponseFactory
{
    public static TResponse BadRequest_400<TResponse>(IReadOnlyDictionary<string, string[]> errors)
        where TResponse : ICqrsResponse
    {
        var metadata = new Dictionary<string, object>
        {
            ["title"] = "Validation failed",
            ["errors"] = errors
        };

        var responseType = typeof(TResponse);
        var implementationType = MapResponseInterfaceTypeToImplementationType(responseType);

        if (typeof(ICqrsResponse).IsAssignableFrom(responseType) && responseType.IsGenericType)
        {
            return (TResponse)Activator.CreateInstance(
                implementationType,
                CqrsResponseStatus.BadRequest_400,
                metadata,
                null,   // message
                null    // data
            )!;
        }

        return (TResponse)Activator.CreateInstance(
            implementationType,
            CqrsResponseStatus.BadRequest_400,
            metadata,
            null    // message
        )!;
    }

    private static Type MapResponseInterfaceTypeToImplementationType(Type interfaceType)
    {
        // Non-generic
        if (interfaceType == typeof(ICqrsResponse))
            return typeof(CqrsResponse);

        if (interfaceType == typeof(ICommandResponse))
            return typeof(CommandResponse);

        // Generic interfaces
        if (interfaceType.IsGenericType)
        {
            var genericDef = interfaceType.GetGenericTypeDefinition();
            var genericArgs = interfaceType.GetGenericArguments();

            if (genericDef == typeof(ICqrsResponse<>))
                return typeof(CqrsResponse<>).MakeGenericType(genericArgs);

            if (genericDef == typeof(IQueryResponse<>))
                return typeof(QueryResponse<>).MakeGenericType(genericArgs);

            if (genericDef == typeof(ICommandResponse<>))
                return typeof(CommandResponse<>).MakeGenericType(genericArgs);
        }

        throw new NotSupportedException($"Response interface type '{interfaceType}' is not supported.");
    }
}
