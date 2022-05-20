namespace Posts.BusinessLogic.DTO.Responses;

public class CompletePostResponse : PostResponse
{
    public string Content { get; set; }

    public double Rating { get; set; }

    public IEnumerable<string> Tags { get; set; }
}
