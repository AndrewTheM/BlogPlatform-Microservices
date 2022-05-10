using AutoMapper;
using BlogPlatform.Comments.BusinessLogic.DTO.Requests;
using BlogPlatform.Comments.BusinessLogic.DTO.Responses;
using BlogPlatform.Comments.DataAccess.Entities;

namespace BlogPlatform.Comments.BusinessLogic.Mapping;

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
