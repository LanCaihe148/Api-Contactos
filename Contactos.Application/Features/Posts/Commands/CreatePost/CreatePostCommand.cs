using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<int>
    {
        public string Title { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}
