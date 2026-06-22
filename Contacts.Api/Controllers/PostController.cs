using Contactos.Application.Features.DTOs;
using Contactos.Application.Features.Posts.Commands.CreatePost;
using Contactos.Application.Features.Posts.Queries.GetAllPost;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Contacts.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostController : ControllerBase
    {
        private IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost(Name = "CreatePost")]
        [ProducesResponseType((int) HttpStatusCode.OK)]

        public async Task<ActionResult<int>> CreatePost([FromBody] CreatePostCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PostDto>), (int)HttpStatusCode.OK)]
        
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsByUserIdAsync([FromQuery]int userId)
        {
            var query = new GetAllPostByIdQuery(userId);
            var posts = await _mediator.Send(query);
            return Ok(posts);
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(PaginatedResult<PostDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedResult<PostDto>>> GetAllAsync(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool sortDescending = false)
        {
            var query = new GetAllPostQuery
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                SortBy = sortBy,
                SortDescending = sortDescending
            };

            var posts = await _mediator.Send(query);
            return Ok(posts);
        }


        [HttpGet("{id}", Name = "GetPostByUserId")]
        [ProducesResponseType(typeof(PostDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PostDto>> GetByIdAsync(int id)
        {
            var query = new GetAllPostByIdQuery(id);
            var post = await _mediator.Send(query);

            if (post == null)
                return NotFound();

            return Ok(post);
        }
    }
}
