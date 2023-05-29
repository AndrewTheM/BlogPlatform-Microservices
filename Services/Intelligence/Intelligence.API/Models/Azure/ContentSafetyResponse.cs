namespace Intelligence.API.Models.Azure;

internal class ContentSafetyResponse
{
    public CategoryResult HateResult { get; set; }

    public CategoryResult SelfHarmResult { get; set; }

    public CategoryResult SexualResult { get; set; }

    public CategoryResult ViolenceResult { get; set; }

    public CategoryResult[] CategoryResults => new[]
    {
        HateResult,
        SelfHarmResult,
        SexualResult,
        ViolenceResult
    };

    internal class CategoryResult
    {
        public string Category { get; set; }

        public int Severity { get; set; }
    }
}
