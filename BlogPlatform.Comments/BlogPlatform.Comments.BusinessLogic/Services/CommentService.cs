using AutoMapper;
using BlogPlatform.Comments.BusinessLogic.DTO.Requests;
using BlogPlatform.Comments.BusinessLogic.DTO.Responses;
using BlogPlatform.Comments.BusinessLogic.Extensions;
using BlogPlatform.Comments.BusinessLogic.Helpers;
using BlogPlatform.Comments.BusinessLogic.Services.Contracts;
using BlogPlatform.Comments.DataAccess.Context.Contracts;
using BlogPlatform.Comments.DataAccess.Entities;
using BlogPlatform.Comments.DataAccess.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Comments.BusinessLogic.Services
{
    public class CommentService : ICommentService
    {
        private readonly IBloggingUnitOfWork _unitOfWork;
        private readonly IUriService _uriService;
        private readonly ITimeService _timeService;
        private readonly IMapper _mapper;

        public CommentService(IBloggingUnitOfWork unitOfWork,
                              IUriService uriService,
                              ITimeService timeService,
                              IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _uriService = uriService;
            _timeService = timeService;
            _mapper = mapper;
        }

        public async Task<Page<CommentResponse>> GetPageOfCommentsForPostAsync(int postId, CommentFilter filter = null)
        {
            Uri GetPageUri(int pageNumber)
            {
                CommentFilter anotherFilter = filter.CopyWithDifferentPage(pageNumber);
                return _uriService.GetCommentsPageUri(postId, anotherFilter);
            }

            var filteredComments = await _unitOfWork.Comments.GetFilteredCommentsOfPostAsync(postId, filter);
            var pagedComments = await filteredComments.Paginate(filter).ToListAsync();
            var responseList = _mapper.Map<List<Comment>, List<CommentResponse>>(pagedComments);
            responseList.ForEach(this.AddRelativeTimeToResponse);

            Uri previousPageUri = GetPageUri(filter.PageNumber - 1);
            Uri nextPageUri = GetPageUri(filter.PageNumber + 1);

            return new(responseList, filteredComments.Count(), filter, previousPageUri, nextPageUri);
        }

        public async Task<CommentResponse> GetCommentByIdAsync(int id)
        {
            var comment = await _unitOfWork.Comments.GetCommentWithAuthorAsync(id);
            var response = _mapper.Map<CommentResponse>(comment);
            this.AddRelativeTimeToResponse(response);

            return response;
        }

        public async Task<CommentResponse> PublishCommentAsync(CommentRequest commentDto, string authorId)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            comment.AuthorId = authorId;

            await _unitOfWork.Comments.CreateAsync(comment);
            await _unitOfWork.CommitAsync();

            var createdComment = await _unitOfWork.Comments.GetCommentWithAuthorAsync(comment.Id);
            var response = _mapper.Map<CommentResponse>(createdComment);
            this.AddRelativeTimeToResponse(response);

            return response;
        }

        public async Task EditCommentAsync(int id, CommentRequest commentDto)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(id);
            _mapper.Map(commentDto, comment);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _unitOfWork.Comments.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddVoteToCommentAsync(int id, int voteValue)
        {
            if (voteValue != 1 && voteValue != -1)
                throw new ArgumentOutOfRangeException(nameof(voteValue));

            var comment = await _unitOfWork.Comments.GetByIdAsync(id);
            comment.UpvoteCount += voteValue;
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> CheckIsCommentAuthorAsync(int id, string userId)
        {
            Comment comment = await _unitOfWork.Comments.GetByIdAsync(id);
            return comment.AuthorId == userId;
        }

        private void AddRelativeTimeToResponse(CommentResponse response)
        {
            response.RelativePublishTime = _timeService.ConvertToLocalRelativeString(response.PublishedOn);
        }
    }
}
