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
        CreateMap<Guid, Protos.Guid>()
            .ConvertUsing(src => new Protos.Guid { Value = src.ToString() });
        
        CreateMap<Protos.Guid, Guid>()
            .ConvertUsing(src => Guid.Parse(src.Value));

        CreateMap<DateTime, Timestamp>()
            .ConvertUsing(src => Timestamp.FromDateTime(DateTime.SpecifyKind(src, DateTimeKind.Utc)));

        CreateMap<Protos.CommentPageRequest, CommentFilter>();

        CreateMap<CommentResponse, Protos.CommentModel>()
            .ForMember(dest => dest.Author, opts => opts.NullSubstitute(string.Empty));

        CreateMap<Page<CommentResponse>, Protos.CommentPageResponse>()
            .ForMember(dest => dest.PreviousPage, opts => opts.NullSubstitute(string.Empty))
            .ForMember(dest => dest.NextPage, opts => opts.NullSubstitute(string.Empty));
    }
}
