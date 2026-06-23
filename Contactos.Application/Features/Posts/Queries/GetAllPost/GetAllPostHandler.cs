using AutoMapper;
using Contactos.Application.Contracts.Persistance;
using Contactos.Application.Features.DTOs;
using MediatR;



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
            
            var pagedResult = await _unitOfWork.PostRepository.GetPagedAsync(
                request.PageIndex,
                request.PageSize,
                request.SearchTerm,
                cancellationToken);

            var mappedItems = _mapper.Map<List<PostDto>>(pagedResult.Items);

            return new PaginatedResult<PostDto>(
                mappedItems,
                pagedResult.TotalCount,
                pagedResult.PageIndex,
                pagedResult.PageSize);
        }
    }
}
