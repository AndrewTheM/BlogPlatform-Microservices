using Intelligence.API.Models;

namespace Intelligence.API.Constants;

internal static class Responses
{
    public static ContentResponse NoProblemsFound =>
        new() { ModerationResult = "No problems found.", Passed = true };

    public static ContentResponse ModerationProblem(string message) =>
        new() { ModerationResult = message, Passed = false };
}
