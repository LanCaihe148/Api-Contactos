using AutoMapper;
using Contactos.Application.Contracts.Persistance;
using Contactos.Application.Features.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Features.Users.Queries.GetUsersByString
{
    public class GetUserByStringHandler : IRequestHandler<GetUserByStringQuery, PaginatedResult<UserByidDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByStringHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<UserByidDto>> Handle(GetUserByStringQuery request, CancellationToken cancellationToken)
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

        }
    }
}
