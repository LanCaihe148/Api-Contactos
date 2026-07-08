using Contactos.Application.Features.DTOs;
using Contactos.Application.Features.Users.Commands.CreateUsersCommand;
using Contactos.Application.Features.Users.Commands.UpdateUsersCommand;
using Contactos.Application.Features.Users.Queries.GetAllUsersQuery;
using Contactos.Application.Features.Users.Queries.GetUsersByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Contacts.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetUsers")]
        [Authorize(Roles="Operator")]
        [ProducesResponseType(typeof(PaginatedResult<UserDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedResult<UserDto>>> GetAllAsync(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool sortDescending = false)
        {
            var query = new GetAllUserQuery
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                SortBy = sortBy,
                SortDescending = sortDescending
            };

            var users = await _mediator.Send(query);
            return Ok(users);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        [Authorize(Roles = "Operator")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDto>> GetByIdAsync(int id)
        {
            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query);
            return Ok(user);
        }

        [HttpPost(Name = "CreateUser")]
        [Authorize(Roles = "Operator")]
        [ProducesResponseType((int)HttpStatusCode.OK)]

        public async Task<ActionResult<int>> CreateUser([FromBody] CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
