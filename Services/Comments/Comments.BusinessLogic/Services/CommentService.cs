using AutoMapper;
using BlogPlatform.Shared.Common.Extensions;
using BlogPlatform.Shared.Common.Filters;
using BlogPlatform.Shared.Common.Pagination;
using BlogPlatform.Shared.Services.Contracts;
using Comments.BusinessLogic.DTO.Requests;
using Comments.BusinessLogic.DTO.Responses;
using Comments.BusinessLogic.Services.Contracts;
using Comments.DataAccess.Entities;
using Comments.DataAccess.Repositories.Contracts;

namespace Comments.BusinessLogic.Services;

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
        var pagedComments = filteredComments.Paginate(filter).ToList();
        var responseList = _mapper.Map<List<Comment>, List<CommentResponse>>(pagedComments);
        responseList.ForEach(AddRelativeTimeToResponse);

        Uri previousPageUri = GetPageUri(filter.PageNumber - 1);
        Uri nextPageUri = GetPageUri(filter.PageNumber + 1);

        return new(responseList, filteredComments.Count(), filter, previousPageUri, nextPageUri);
    }

    public async Task<CommentResponse> GetCommentByIdAsync(Guid id)
    {
        var comment = await _commentRepository.GetCommentWithAuthorAsync(id);
        var response = _mapper.Map<CommentResponse>(comment);
        AddRelativeTimeToResponse(response);

        return response;
    }

    public async Task<CommentResponse> PublishCommentAsync(CommentRequest commentDto, Guid userId, string username)
    {
        var comment = _mapper.Map<Comment>(commentDto);
        comment.AuthorId = userId;
        comment.Author = username;

        var id = await _commentRepository.CreateAsync(comment);
        var createdComment = await _commentRepository.GetCommentWithAuthorAsync(id);
        var response = _mapper.Map<CommentResponse>(createdComment);

        AddRelativeTimeToResponse(response);
        return response;
    }

    public async Task EditCommentAsync(Guid id, CommentContentRequest commentDto)
    {
        var comment = await _commentRepository.GetAsync(id);
        _mapper.Map(commentDto, comment);
        await _commentRepository.UpdateAsync(id, comment);
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
        await _commentRepository.UpdateAsync(id, comment);
    }

    public async Task<bool> CheckIsCommentAuthorAsync(Guid id, Guid userId)
    {
        var comment = await _commentRepository.GetAsync(id);
        return comment.AuthorId == userId;
    }

    private void AddRelativeTimeToResponse(CommentResponse response)
    {
        response.RelativePublishTime = _timeService.ConvertToLocalRelativeString(response.PublishedOn);
    }
}
