using System;

namespace VisualArchitect.Api.Orchestration.Abstractions.Domain.Extensions;

public static class StringExtensions
{
    public static string Truncate(this string value, int maxLength)
    {
        return value.Length <= maxLength ? value : value[..maxLength];
    }
}
