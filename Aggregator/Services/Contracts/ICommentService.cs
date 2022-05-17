using Aggregator.DTO;

namespace Aggregator.Services.Contracts;

public interface ICommentService
{
    Task<Page<CommentDto>> GetPageOfPostCommentsAsync(Guid postId);
}
