using BlogPlatform.Shared.Common.Pagination;

namespace Aggregator.DTO;

public class CompletePostDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string TitleIdentifier { get; set; }

    public string Author { get; set; }

    public DateTime PublishedOn { get; set; }

    public string RelativePublishTime { get; set; }

    public bool IsEdited { get; set; }

    public string ThumbnailPath { get; set; }

    public string Content { get; set; }

    public double Rating { get; set; }

    public IEnumerable<string> Tags { get; set; }

    public Page<CommentDto> CommentPage { get; set; }
}
