using AutoMapper;
using Contactos.Application.Contracts.Persistance;
using Contactos.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contactos.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostHandler : IRequestHandler<CreatePostCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreatePostHandler> _logger;
        private readonly IMapper _mapper;

        public CreatePostHandler(IUnitOfWork unitOfWork, ILogger<CreatePostHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var postEntity = _mapper.Map<Post>(request);
            _unitOfWork.Repository<Post>().AddEntity(postEntity);
            var result = await _unitOfWork.Complete();


            if (result <= 0)
            {
                _logger.LogError("No se inserto el record de post");
                throw new Exception("No se pudo insertar el record del post");
            }
            return postEntity.Id;
        }
    }
}
