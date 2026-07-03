using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Features.Posts.Commands.DeletePost
{
    public class DeletePostCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        
    }
}
