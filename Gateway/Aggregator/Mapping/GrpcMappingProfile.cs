using Aggregator.DTO;
using AutoMapper;
using BlogPlatform.Shared.Common.Pagination;
using Google.Protobuf.WellKnownTypes;
using Protos = BlogPlatform.Shared.GRPC.Protos;

namespace Aggregator.Mapping;

public class GrpcMappingProfile : Profile
{
    public GrpcMappingProfile()
    {
        CreateMap<Guid, Protos.Guid>()
            .ConvertUsing(src => new Protos.Guid { Value = src.ToString() });

        CreateMap<DateTime, Timestamp>()
            .ConvertUsing(src => Timestamp.FromDateTime(src));

        CreateMap<Protos.CommentModel, CommentDto>();
        CreateMap<Protos.CommentPageResponse, Page<CommentDto>>();
    }
}
