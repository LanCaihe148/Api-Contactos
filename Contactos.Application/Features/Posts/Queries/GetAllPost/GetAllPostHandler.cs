using AutoMapper;
using Contactos.Application.Contracts.Persistance;
using Contactos.Application.Features.DTOs;
using Contactos.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Contactos.Application.Features.Posts.Queries.GetAllPost
{
    public class GetAllPostHandler : IRequestHandler<GetAllPostQuery, PaginatedResult<PostDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPostHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<PostDto>> Handle(GetAllPostQuery request, CancellationToken cancellationToken)
        {
            var postRepo = _unitOfWork.Repository<Post>();

            var query = postRepo.GetQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(p => p.Title.Contains(request.SearchTerm) ||
                                          p.Body.Contains(request.SearchTerm));                          
            }

            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                query = request.SortBy.ToLower() switch
                {
                    "title" => request.SortDescending ? query.OrderByDescending(p => p.Title) : query.OrderBy(p => p.Title),
                    "userid" => request.SortDescending ? query.OrderByDescending(p => p.UserId) : query.OrderBy(p => p.UserId),
                    _ => query.OrderBy(p => p.Id)
                };
            }else
            {
                query = query.OrderBy(p => p.Id);
            }
            var totalCount = await query.CountAsync();

            var items = await query.Skip((request.PageIndex - 1) * request.PageSize)
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
