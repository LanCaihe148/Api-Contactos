using MediatR;

namespace Contactos.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<int>
    {
        public string Title { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}
