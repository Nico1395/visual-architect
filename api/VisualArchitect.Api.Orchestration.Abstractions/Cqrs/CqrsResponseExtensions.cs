using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace VisualArchitect.Api.Orchestration.Abstractions.Cqrs;

public static class CqrsResponseExtensions
{
    public static bool IsSuccess_2xx(this ICqrsResponse response) => response.Status <= CqrsResponseStatus.NoContent_204;
    public static bool IsClientSide_4xx(this ICqrsResponse response) => response.Status >= CqrsResponseStatus.BadRequest_400 && response.Status <= CqrsResponseStatus.Conflict_409;
    public static bool IsServerSide_5xx(this ICqrsResponse response) => response.Status >= CqrsResponseStatus.InternalServerError_500;
    public static bool IsStatus(this ICqrsResponse response, CqrsResponseStatus status) => response.Status == status;
    public static bool IsOK_200(this ICqrsResponse response) => response.IsStatus(CqrsResponseStatus.OK_200);
    public static bool IsCreated_201(this ICqrsResponse response) => response.IsStatus(CqrsResponseStatus.Created_201);
    public static bool IsAccepted_202(this ICqrsResponse response) => response.IsStatus(CqrsResponseStatus.Accepted_202);
    public static bool IsNoContent_204(this ICqrsResponse response) => response.IsStatus(CqrsResponseStatus.NoContent_204);
    public static bool IsBadRequest_400(this ICqrsResponse response) => response.IsStatus(CqrsResponseStatus.BadRequest_400);
    public static bool IsUnauthorized_401(this ICqrsResponse response) => response.IsStatus(CqrsResponseStatus.Unauthorized_401);
    public static bool IsForbidden_403(this ICqrsResponse response) => response.IsStatus(CqrsResponseStatus.Forbidden_403);
    public static bool IsNotFound_404(this ICqrsResponse response) => response.IsStatus(CqrsResponseStatus.NotFound_404);
    public static bool IsNotAcceptable_406(this ICqrsResponse response) => response.IsStatus(CqrsResponseStatus.NotAcceptable_406);
    public static bool IsConflict_409(this ICqrsResponse response) => response.IsStatus(CqrsResponseStatus.Conflict_409);
    public static bool IsInternalServerError_500(this ICqrsResponse response) => response.IsStatus(CqrsResponseStatus.InternalServerError_500);
    public static bool IsNotImplemented_501(this ICqrsResponse response) => response.IsStatus(CqrsResponseStatus.NotImplemented_501);
    public static bool IsServiceUnavailable_503(this ICqrsResponse response) => response.IsStatus(CqrsResponseStatus.ServiceUnavailable_503);

    public static IResult ToResult(this ICqrsResponse response)
    {
        if (response.Status == CqrsResponseStatus.NoContent_204)
            return Results.NoContent();

        var statusCode = (int)(HttpStatusCode)response.Status;
        if (statusCode >= 400)
            return CreateProblemResult(response, statusCode);

        return Results.StatusCode(statusCode);
    }

    public static IResult ToResult<T>(this ICqrsResponse<T> response)
    {
        if (response.Status == CqrsResponseStatus.NoContent_204)
            return Results.NoContent();

        var statusCode = (int)(HttpStatusCode)response.Status;
        if (statusCode >= 400)
            return CreateProblemResult(response, statusCode);

        return response.Status switch
        {
            CqrsResponseStatus.OK_200 => Results.Ok(response.Data),
            CqrsResponseStatus.Created_201 => Results.Created(GetLocation(response), response.Data),
            CqrsResponseStatus.Accepted_202 => Results.Accepted(GetLocation(response), response.Data),
            _ => Results.Json(response.Data, statusCode: statusCode)
        };
    }

    private static IResult CreateProblemResult(ICqrsResponse response, int statusCode)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = response.Metadata.TryGetValue("title", out var title) ? title?.ToString() : ReasonPhrases.GetReasonPhrase(statusCode),
            Detail = response.Metadata.TryGetValue("detail", out var detail) ? detail?.ToString() : null
        };

        foreach (var kvp in response.Metadata)
        {
            if (kvp.Key is "title" or "detail")
                continue;

            problemDetails.Extensions[kvp.Key] = kvp.Value;
        }

        return Results.Problem(problemDetails);
    }

    private static string? GetLocation(ICqrsResponse response)
        => response.Metadata.TryGetValue("location", out var location) ? location?.ToString() : null;
}
