using Aggregator.DTO;
using Aggregator.Services.Contracts;
using AutoMapper;
using PostGrpcClient = BlogPlatform.Shared.GRPC.Protos.PostGrpc.PostGrpcClient;
using Protos = BlogPlatform.Shared.GRPC.Protos;

namespace Aggregator.Services;

public class PostService : IPostService
{
    private readonly PostGrpcClient _client;
    private readonly IMapper _mapper;

    public PostService(PostGrpcClient client, IMapper mapper)
    {
        _client = client;
        _mapper = mapper;
    }

    public async Task<CompletePostDto> GetCompletePostAsync(string titleIdentifier)
    {
        var request = new Protos.CompletePostRequest { TitleIdentifier = titleIdentifier };
        var response = await _client.GetCompletePostAsync(request);
        var post = _mapper.Map<CompletePostDto>(response);
        return post;
    }
}
