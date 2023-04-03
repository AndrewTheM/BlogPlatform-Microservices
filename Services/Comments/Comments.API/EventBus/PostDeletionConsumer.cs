using BlogPlatform.Shared.Events;
using Comments.DataAccess.Repositories.Contracts;
using MassTransit;

namespace Comments.API.EventBus;

public class PostDeletionConsumer : IConsumer<PostDeletionEvent>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ILogger<PostDeletionConsumer> _logger;

    public PostDeletionConsumer(ICommentRepository commentRepository, ILogger<PostDeletionConsumer> logger)
    {
        _commentRepository = commentRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PostDeletionEvent> context)
    {
        Guid postId = context.Message.PostId;
        var postComments = await _commentRepository.GetFilteredCommentsOfPostAsync(postId, null);
        foreach (var comment in postComments)
        {
            await _commentRepository.DeleteAsync(comment.Id);
        }

        _logger.LogInformation("Deleted comments of post {PostId}", postId);
    }
}
