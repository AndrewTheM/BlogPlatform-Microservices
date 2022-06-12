using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Posts.BusinessLogic.DTO.Responses;
using Protos = BlogPlatform.Shared.GRPC.Protos;

namespace Posts.API.GRPC.Mapping;

public class GrpcMappingProfile : Profile
{
    public GrpcMappingProfile()
    {
        CreateMap<Guid, Protos.Guid>()
            .ConvertUsing(src => new Protos.Guid { Value = src.ToString() });

        CreateMap<DateTime, Timestamp>()
            .ConvertUsing(src => Timestamp.FromDateTime(src));

        CreateMap<CompletePostResponse, Protos.CompletePostResponse>();
    }
}
