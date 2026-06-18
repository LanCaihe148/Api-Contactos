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
        [ProducesResponseType(typeof(IEnumerable<PostDto>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllAsync()
        {
            var query = new GetAllPostQuery();
            var post = await _mediator.Send(query);

            return Ok(post);
        }
    }
}
