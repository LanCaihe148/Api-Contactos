using AutoMapper;
using Contactos.Application.Contracts.Persistance;
using Contactos.Application.Features.DTOs;
using MediatR;

namespace Contactos.Application.Features.Users.Queries.GetAllUsersQuery
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, PaginatedResult<UserByidDto>>
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<UserByidDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var query = await _unitOfWork.UserRepository.GetPagedAsync(
                request.PageIndex,
                request.PageSize,
                request.SearchTerm,
                cancellationToken);

            var mappedItems = _mapper.Map<List<UserByidDto>>(query.Items);

            return new PaginatedResult<UserByidDto>(
                mappedItems,
                query.TotalCount,
                query.PageIndex,
                query.PageSize);
            
            //query = query.OrderBy(u => u.Id);

            //var totalCount = await query.CountAsync();
            ////var UserList = await _unitOfWork.UserRepository.GetAllAsync();

            //return _mapper.Map<PaginatedResult<UserByidDto>>(UserList);
        }
    }
}
