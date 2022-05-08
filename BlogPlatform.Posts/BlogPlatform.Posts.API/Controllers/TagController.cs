using AutoMapper;
using BlogPlatform.Posts.BusinessLogic.DTO.Requests;
using BlogPlatform.Posts.BusinessLogic.DTO.Responses;
using BlogPlatform.Posts.DataAccess.Context.Contracts;
using BlogPlatform.Posts.DataAccess.Entities;
using BlogPlatform.Posts.DataAccess.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Posts.API.Controllers
{
    [Route("api/tags")]
    [ApiController]
    [Authorize]
    public class TagController : ControllerBase
    {
        private readonly IBloggingUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TagController(IBloggingUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<string>> GetTags()
        {
            var tags = await _unitOfWork.Tags.GetRelevantTagsAsync();
            return tags.Select(t => t.TagName);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TagResponse>> GetTagById([FromRoute] int id)
        {
            try
            {
                var tag = await _unitOfWork.Tags.GetByIdAsync(id);
                return _mapper.Map<TagResponse>(tag);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Author")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> CreateTag([FromBody] TagRequest tagDto)
        {
            var tag = _mapper.Map<Tag>(tagDto);
            await _unitOfWork.Tags.CreateAsync(tag);
            await _unitOfWork.CommitAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Author")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateTag([FromRoute] int id,
                                                  [FromBody] TagRequest tagDto)
        {
            try
            {
                var tag = await _unitOfWork.Tags.GetByIdAsync(id);
                _mapper.Map(tagDto, tag);
                await _unitOfWork.CommitAsync();
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Author")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTag([FromRoute] int id)
        {
            try
            {
                await _unitOfWork.Tags.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
