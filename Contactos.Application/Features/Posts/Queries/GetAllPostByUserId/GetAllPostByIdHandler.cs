using AutoMapper;
using Contactos.Application.Contracts.Persistance;
using Contactos.Application.Features.DTOs;
using Contactos.Domain;
using MediatR;


namespace Contactos.Application.Features.Posts.Queries.GetAllPost
{
    public class GetAllPostByIdHandler : IRequestHandler<GetAllPostByIdQuery, List<PostDto>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPostByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PostDto>> Handle(GetAllPostByIdQuery request, CancellationToken cancellationToken)
        {
            //return await _unitOfWork.Repository<Post>().GetByIdAsync(request.UserId);

            var posts = await _unitOfWork.Repository<Post>()
                .GetAsync(a => a.UserId == request.UserId);

            return _mapper.Map<List<PostDto>>(posts);
        }
    }
}
