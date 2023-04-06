using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace w_escolas.Endpoints;

public static class ProblemDetailsExtension
{
    public static Dictionary<string, string[]> ConvertToProblemDetails(this List<ValidationFailure> failures)
    {
        return failures
            .GroupBy(g => g.PropertyName)
            .ToDictionary(d => d.Key, g => g.Select(x => x.ErrorMessage).ToArray());
    }
    public static Dictionary<string, string[]> ConvertToProblemDetails(this IEnumerable<IdentityError> errors)
    {
        var dictionary = new Dictionary<string, string[]>
        {
            { "details", errors.Select(e => e.Description).ToArray() }
        };
        return dictionary;
    }
    public static Dictionary<string, string[]> ConvertToProblemDetails(this List<string> errors)
    {
        var dictionary = new Dictionary<string, string[]>
        {
            { "details", errors.ToArray() }
        };
        return dictionary;
    }
    public static Dictionary<string, string[]> ConvertToProblemDetails(this string errorMessage)
    {
        var dictionary = new Dictionary<string, string[]>();
        var errors = new List<string>{ errorMessage };
        dictionary.Add("details", errors.ToArray());
        return dictionary;
    }
}
