using AutoMapper;
using Contactos.Application.Contracts.Persistance;
using Contactos.Application.Features.DTOs;
using Contactos.Domain;
using MediatR;


namespace Contactos.Application.Features.Posts.Queries.GetAllPost
{
    public class GetAllPostHandler : IRequestHandler<GetAllPostQuery, List<PostDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPostHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PostDto>> Handle(GetAllPostQuery request, CancellationToken cancellationToken)
        {
            var query = await _unitOfWork.Repository<Post>()
                .GetAllAsync();

            return _mapper.Map<List<PostDto>>(query);
        }
    }
}
