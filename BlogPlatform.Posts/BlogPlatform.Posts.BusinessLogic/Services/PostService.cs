using AutoMapper;
using BlogPlatform.Posts.BusinessLogic.DTO.Requests;
using BlogPlatform.Posts.BusinessLogic.DTO.Responses;
using BlogPlatform.Posts.BusinessLogic.Extensions;
using BlogPlatform.Posts.BusinessLogic.Helpers;
using BlogPlatform.Posts.BusinessLogic.Services.Contracts;
using BlogPlatform.Posts.DataAccess.Context.Contracts;
using BlogPlatform.Posts.DataAccess.Entities;
using BlogPlatform.Posts.DataAccess.Extensions;
using BlogPlatform.Posts.DataAccess.Filters;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BlogPlatform.Posts.BusinessLogic.Services;

public class PostService : IPostService
{
    private readonly IBloggingUnitOfWork _unitOfWork;
    private readonly IUriService _uriService;
    private readonly ITimeService _timeService;
    private readonly IMapper _mapper;

    public PostService(IBloggingUnitOfWork unitOfWork,
                       IUriService uriService,
                       ITimeService timeService,
                       IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _uriService = uriService;
        _timeService = timeService;
        _mapper = mapper;
    }

    public async Task<Page<PostResponse>> GetPageOfPostsAsync(PostFilter filter = null)
    {
        Uri GetPageUri(int pageNumber)
        {
            PostFilter anotherFilter = filter.CopyWithDifferentPage(pageNumber);
            return _uriService.GetPostsPageUri(anotherFilter);
        }

        var filteredPosts = await _unitOfWork.Posts.GetFilteredPostsAsync(filter);
        var pagedPosts = await filteredPosts.Paginate(filter).ToListAsync();
        var responseList = _mapper.Map<List<Post>, List<PostResponse>>(pagedPosts);
        responseList.ForEach(this.AddRelativeTimeToResponse);

        Uri previousPageUri = GetPageUri(filter.PageNumber - 1);
        Uri nextPageUri = GetPageUri(filter.PageNumber + 1);

        return new(responseList, filteredPosts.Count(), filter, previousPageUri, nextPageUri);
    }

    public async Task<IEnumerable<PostResponse>> GetTrendingPostsAsync(int count)
    {
        var posts = await _unitOfWork.Posts.GetTopRatedPostsWithAuthorsAsync(count);
        var postList = await posts.ToListAsync();
        return _mapper.Map<List<Post>, List<PostResponse>>(postList);
    }

    public async Task<PostResponse> FindPostAsync(Guid id)
    {
        var post = await _unitOfWork.Posts.GetByIdAsync(id);
        var response = _mapper.Map<PostResponse>(post);

        this.AddRelativeTimeToResponse(response);
        return response;
    }

    public async Task<CompletePostResponse> GetCompletePostAsync(string titleIdentifier)
    {
        var completePost = await _unitOfWork.Posts.GetCompletePostAsync(titleIdentifier);
        var response = _mapper.Map<CompletePostResponse>(completePost);
        response.Rating = await _unitOfWork.Posts.CalculatePostRatingAsync(completePost.Id);

        this.AddRelativeTimeToResponse(response);
        return response;
    }

    public async Task<PostResponse> PublishPostAsync(PostRequest postDto)
    {
        Post post = _mapper.Map<Post>(postDto);
        post.ContentEntity = _mapper.Map<PostContent>(postDto);

        string cutTitle = post.Title.Length switch
        {
            > 100 => post.Title[..100],
            _ => post.Title
        };

        string cutLowerTitle = cutTitle.ToLowerInvariant();
        string wordOnlyString = Regex.Replace(cutLowerTitle, @"[^\w\s]", string.Empty);
        post.TitleIdentifier = Regex.Replace(wordOnlyString, @"\s+", "-");

        await _unitOfWork.Posts.CreateAsync(post);
        await _unitOfWork.CommitAsync();

        var response = _mapper.Map<PostResponse>(post);
        this.AddRelativeTimeToResponse(response);

        return response;
    }

    public async Task EditPostAsync(Guid id, PostRequest postDto)
    {
        var post = await _unitOfWork.Posts.GetPostWithContentAsync(id);
        _mapper.Map(postDto, post);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeletePostAsync(Guid id)
    {
        await _unitOfWork.Posts.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task SetTagsOfPostAsync(Guid id, params string[] tagNames)
    {
        var post = await _unitOfWork.Posts.GetPostWithTagsAsync(id);
        post.Tags = new List<Tag>();

        foreach (string tagName in tagNames)
        {
            Tag tag;

            try
            {
                tag = await _unitOfWork.Tags.GetTagByNameAsync(tagName);
            }
            catch (EntityNotFoundException)
            {
                tag = new() { TagName = tagName };
            }

            if (!post.Tags.Contains(tag))
            {
                post.Tags.Add(tag);
            }
        }

        await _unitOfWork.CommitAsync();
    }

    public async Task<bool> CheckIsPostAuthorAsync(Guid id, Guid userId)
    {
        var post = await _unitOfWork.Posts.GetByIdAsync(id);
        return post.AuthorId == userId;
    }

    private void AddRelativeTimeToResponse(PostResponse response)
    {
        response.RelativePublishTime = _timeService.ConvertToLocalRelativeString(response.PublishedOn);
    }
}
