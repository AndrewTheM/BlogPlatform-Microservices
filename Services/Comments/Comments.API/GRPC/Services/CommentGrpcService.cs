using AutoMapper;
using BlogPlatform.Shared.Common.Filters;
using Comments.BusinessLogic.Services.Contracts;
using Grpc.Core;
using Protos = BlogPlatform.Shared.GRPC.Protos;

namespace Comments.API.GRPC.Services;

public class CommentGrpcService : Protos.CommentGrpc.CommentGrpcBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentGrpcService(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    public async override Task<Protos.CommentPageResponse> GetPageOfCommentsForPost(
        Protos.CommentPageRequest request, ServerCallContext context)
    {
        var postId = _mapper.Map<Guid>(request.PostId);
        var commentFilter = _mapper.Map<CommentFilter>(request);
        var page = await _commentService.GetPageOfCommentsForPostAsync(postId, commentFilter);
        var response = _mapper.Map<Protos.CommentPageResponse>(page);
        return response;
    }
}
