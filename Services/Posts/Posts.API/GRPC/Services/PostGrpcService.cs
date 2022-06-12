using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
using Posts.API.Extensions;
using Posts.BusinessLogic.DTO.Responses;
using Posts.BusinessLogic.Services.Contracts;
using Protos = BlogPlatform.Shared.GRPC.Protos;

namespace Posts.API.GRPC.Services;

public class PostGrpcService : Protos.PostGrpc.PostGrpcBase
{
    private readonly IPostService _postService;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;

    public PostGrpcService(IPostService postService, IMapper mapper, IDistributedCache cache)
    {
        _postService = postService;
        _mapper = mapper;
        _cache = cache;
    }

    public async override Task<Protos.CompletePostResponse> GetCompletePost(
        Protos.CompletePostRequest request, ServerCallContext context)
    {
        var post = await _cache.GetAsync<CompletePostResponse>(request.TitleIdentifier);

        if (post is null)
        {
            post = await _postService.GetCompletePostAsync(request.TitleIdentifier);
            await _cache.SetAsync(request.TitleIdentifier, post, options: new()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                SlidingExpiration = TimeSpan.FromSeconds(30),
            });
        }

        var response = _mapper.Map<Protos.CompletePostResponse>(post);
        return response;
    }
}
