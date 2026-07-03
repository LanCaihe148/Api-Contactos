using Contactos.Application.Contracts.Persistance;
using Contactos.Application.Exceptions;
using Contactos.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contactos.Application.Features.Posts.Commands.DeletePost
{
    public class DeletePostHandler : IRequestHandler<DeletePostCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeletePostHandler> _logger;
        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var PostToDelete = await _unitOfWork.PostRepository.GetByIdAsync(request.Id);

            if(PostToDelete == null) 
            {
                throw new NotFoundException(nameof(Post), request.Id);
            }

            _unitOfWork.PostRepository.DeleteEntity(PostToDelete);
            await _unitOfWork.Complete();

            _logger.LogInformation($"Post {request.Id} fue eliminado exitosamente");

            return Unit.Value;
        }
    }
}
