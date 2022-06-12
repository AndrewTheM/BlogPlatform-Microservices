using Aggregator.DTO;
using Aggregator.Services.Contracts;
using AutoMapper;
using BlogPlatform.Shared.Common.Pagination;
using CommentGrpcClient = BlogPlatform.Shared.GRPC.Protos.CommentGrpc.CommentGrpcClient;
using Protos = BlogPlatform.Shared.GRPC.Protos;

namespace Aggregator.Services;

public class CommentService : ICommentService
{
    private readonly CommentGrpcClient _client;
    private readonly IMapper _mapper;

    public CommentService(CommentGrpcClient client, IMapper mapper)
    {
        _client = client;
        _mapper = mapper;
    }

    public async Task<Page<CommentDto>> GetPageOfPostCommentsAsync(Guid postId)
    {
        var request = new Protos.CommentPageRequest
        {
            PostId = _mapper.Map<Protos.Guid>(postId),
            PageNumber = 1,
            PageSize = 10
        };

        var response = await _client.GetPageOfCommentsForPostAsync(request);
        var page = _mapper.Map<Page<CommentDto>>(response);
        return page;
    }
}
