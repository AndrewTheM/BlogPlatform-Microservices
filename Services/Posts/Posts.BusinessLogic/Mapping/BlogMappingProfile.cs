using AutoMapper;
using Posts.BusinessLogic.DTO.Requests;
using Posts.BusinessLogic.DTO.Responses;
using Posts.DataAccess.Entities;

namespace Posts.BusinessLogic.Mapping;

public class BlogMappingProfile : Profile
{
    public BlogMappingProfile()
    {
        ConfigurePost();
        ConfigureRating();
        ConfigureTag();
    }

    private static void SkipNullValues<TSource, TDestination, TMember>(
        IMemberConfigurationExpression<TSource, TDestination, TMember> opt)
    {
        opt.Condition((src, dest, prop) => prop is not null);
    }

    private void ConfigurePost()
    {
        CreateMap<PostRequest, Post>()
            .AfterMap((req, p) =>
            {
                if (p.ContentEntity is not null && req.Content is not null)
                {
                    p.ContentEntity.Content = req.Content;
                }
            })
            .ForAllMembers(SkipNullValues);

        CreateMap<PostRequest, PostContent>()
            .ForAllMembers(SkipNullValues);

        CreateMap<Post, PostResponse>()
            .ForMember(res => res.PublishedOn, opt => opt.MapFrom(p => p.CreatedOn))
            .ForMember(res => res.IsEdited, opt => opt.MapFrom(p => p.UpdatedOn > p.CreatedOn));

        CreateMap<Post, CompletePostResponse>()
            .IncludeBase<Post, PostResponse>()
            .ForMember(res => res.Content, opt => opt.MapFrom(p => p.ContentEntity.Content))
            .ForMember(res => res.Tags, opt => opt.MapFrom(p => p.Tags.Select(t => t.TagName)));
    }

    private void ConfigureRating()
    {
        CreateMap<RatingRequest, Rating>();

        CreateMap<Rating, RatingResponse>();
    }

    private void ConfigureTag()
    {
        CreateMap<TagRequest, Tag>();

        CreateMap<Tag, TagResponse>();
    }
}
