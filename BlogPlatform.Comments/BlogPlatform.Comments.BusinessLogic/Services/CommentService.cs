using AutoMapper;
using BlogPlatform.Comments.BusinessLogic.DTO.Requests;
using BlogPlatform.Comments.BusinessLogic.DTO.Responses;
using BlogPlatform.Comments.BusinessLogic.Extensions;
using BlogPlatform.Comments.BusinessLogic.Helpers;
using BlogPlatform.Comments.BusinessLogic.Services.Contracts;
using BlogPlatform.Comments.DataAccess.Entities;
using BlogPlatform.Comments.DataAccess.Filters;
using BlogPlatform.Comments.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Comments.BusinessLogic.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUriService _uriService;
    private readonly ITimeService _timeService;
    private readonly IMapper _mapper;

    public CommentService(
        ICommentRepository commentRepository,
        IUriService uriService,
        ITimeService timeService,
        IMapper mapper)
    {
        _commentRepository = commentRepository;
        _uriService = uriService;
        _timeService = timeService;
        _mapper = mapper;
    }

    public async Task<Page<CommentResponse>> GetPageOfCommentsForPostAsync(Guid postId, CommentFilter filter = null)
    {
        Uri GetPageUri(int pageNumber)
        {
            CommentFilter anotherFilter = filter.CopyWithDifferentPage(pageNumber);
            return _uriService.GetCommentsPageUri(postId, anotherFilter);
        }

        var filteredComments = await _commentRepository.GetFilteredCommentsOfPostAsync(postId, filter);
        var pagedComments = await filteredComments.Paginate(filter).ToListAsync();
        var responseList = _mapper.Map<List<Comment>, List<CommentResponse>>(pagedComments);
        responseList.ForEach(this.AddRelativeTimeToResponse);

        Uri previousPageUri = GetPageUri(filter.PageNumber - 1);
        Uri nextPageUri = GetPageUri(filter.PageNumber + 1);

        return new(responseList, filteredComments.Count(), filter, previousPageUri, nextPageUri);
    }

    public async Task<CommentResponse> GetCommentByIdAsync(Guid id)
    {
        var comment = await _commentRepository.GetCommentWithAuthorAsync(id);
        var response = _mapper.Map<CommentResponse>(comment);
        this.AddRelativeTimeToResponse(response);

        return response;
    }

    public async Task<CommentResponse> PublishCommentAsync(CommentRequest commentDto, string authorId)
    {
        var comment = _mapper.Map<Comment>(commentDto);
        comment.AuthorId = authorId;

        await _commentRepository.CreateAsync(comment);

        var createdComment = await _commentRepository.GetCommentWithAuthorAsync(comment.Id);
        var response = _mapper.Map<CommentResponse>(createdComment);
        this.AddRelativeTimeToResponse(response);

        return response;
    }

    public async Task EditCommentAsync(Guid id, CommentRequest commentDto)
    {
        var comment = await _commentRepository.GetAsync(id);
        _mapper.Map(commentDto, comment);
        // TODO: use update method here
    }

    public async Task DeleteCommentAsync(Guid id)
    {
        await _commentRepository.DeleteAsync(id);
    }

    public async Task AddVoteToCommentAsync(Guid id, int voteValue)
    {
        if (voteValue != 1 && voteValue != -1)
            throw new ArgumentOutOfRangeException(nameof(voteValue));

        var comment = await _commentRepository.GetAsync(id);
        comment.UpvoteCount += voteValue;
        // TODO: use update method here
    }

    public async Task<bool> CheckIsCommentAuthorAsync(Guid id, string userId)
    {
        Comment comment = await _commentRepository.GetAsync(id);
        return comment.AuthorId == userId;
    }

    private void AddRelativeTimeToResponse(CommentResponse response)
    {
        response.RelativePublishTime = _timeService.ConvertToLocalRelativeString(response.PublishedOn);
    }
}
