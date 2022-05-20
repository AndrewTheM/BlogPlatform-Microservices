using AutoMapper;
using Comments.BusinessLogic.DTO.Requests;
using Comments.BusinessLogic.DTO.Responses;
using Comments.DataAccess.Entities;

namespace Comments.BusinessLogic.Mapping;

public class CommentMappingProfile : Profile
{
    public CommentMappingProfile()
    {
        CreateMap<CommentRequest, Comment>();

        CreateMap<CommentContentRequest, Comment>();

        CreateMap<Comment, CommentResponse>()
            .ForMember(res => res.PublishedOn, opt => opt.MapFrom(c => c.CreatedOn))
            .ForMember(res => res.IsEdited, opt => opt.MapFrom(c => c.UpdatedOn > c.CreatedOn));
    }
}
