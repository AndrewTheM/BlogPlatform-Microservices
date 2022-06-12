using AutoMapper;
using BlogPlatform.Shared.Common.Filters;
using BlogPlatform.Shared.GRPC.Protos;
using Comments.BusinessLogic.Services.Contracts;
using Grpc.Core;

namespace Comments.API.GRPC.Services;

public class CommentGrpcService : CommentGrpc.CommentGrpcBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentGrpcService(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    public async override Task<CommentPageResponse> GetPageOfCommentsForPost(
        CommentPageRequest request, ServerCallContext context)
    {
        var postId = _mapper.Map<System.Guid>(request.PostId);
        var commentFilter = _mapper.Map<CommentFilter>(request);
        var page = await _commentService.GetPageOfCommentsForPostAsync(postId, commentFilter);
        var response = _mapper.Map<CommentPageResponse>(page);
        return response;
    }
}
