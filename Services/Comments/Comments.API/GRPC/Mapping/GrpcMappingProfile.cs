using AutoMapper;
using BlogPlatform.Shared.Common.Filters;
using BlogPlatform.Shared.Common.Pagination;
using Comments.BusinessLogic.DTO.Responses;
using Google.Protobuf.WellKnownTypes;
using Protos = BlogPlatform.Shared.GRPC.Protos;

namespace Comments.API.GRPC.Mapping;

public class GrpcMappingProfile : Profile
{
    public GrpcMappingProfile()
    {
        CreateMap<Protos.Guid, Guid>()
            .ConvertUsing(src => System.Guid.Parse(src.Value));

        CreateMap<Timestamp, DateTime>()
            .ConvertUsing(src => src.ToDateTime());

        CreateMap<Protos.CommentPageRequest, CommentFilter>();

        CreateMap<CommentResponse, Protos.CommentModel>();
        CreateMap<Page<CommentResponse>, Protos.CommentPageResponse>();
    }
}
