using AutoMapper;
using Contactos.Application.Contracts.Persistance;
using Contactos.Application.Features.DTOs;
using Contactos.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Contactos.Application.Features.Posts.Queries.GetAllPost
{
    public class GetAllPostByIdHandler : IRequestHandler<GetAllPostByIdQuery, PaginatedResult<PostDto>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPostByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<PostDto>> Handle(GetAllPostByIdQuery request, CancellationToken cancellationToken)
        {

            var postRepo = _unitOfWork.Repository<Post>();

            var query = postRepo.GetQueryable()
                .Where(p => p.UserId == request.UserId)
                .OrderBy(p => p.Id); 

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var mappedItems = _mapper.Map<List<PostDto>>(items);

            return new PaginatedResult<PostDto>(
                mappedItems,
                totalCount,
                request.PageIndex,
                request.PageSize);
        }
    }
}
