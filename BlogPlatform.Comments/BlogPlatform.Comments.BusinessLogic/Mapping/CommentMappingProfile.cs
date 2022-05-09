using AutoMapper;
using BlogPlatform.Comments.BusinessLogic.DTO.Requests;
using BlogPlatform.Comments.BusinessLogic.DTO.Responses;
using BlogPlatform.Comments.DataAccess.Entities;

namespace BlogPlatform.Comments.BusinessLogic.Mapping;

public class CommentMappingProfile : Profile
{
    // TODO: work with other microservices
    public CommentMappingProfile()
    {
        CreateMap<CommentRequest, Comment>()
            .ForMember(c => c.PostId, opt => opt.Condition(req => req.PostId != Guid.Empty));

        CreateMap<Comment, CommentResponse>()
            //.ForMember(res => res.Author, opt => opt.MapFrom(c => c.Author.UserName))
            .ForMember(res => res.PublishedOn, opt => opt.MapFrom(c => c.CreatedOn))
            .ForMember(res => res.IsEdited, opt => opt.MapFrom(c => c.UpdatedOn > c.CreatedOn));
    }
}
