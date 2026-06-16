using AutoMapper;
using Contactos.Application.Contracts.Persistance;
using Contactos.Application.Features.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Features.Users.Queries.GetAllUsersQuery
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, List<UserDto>>
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var UserList = await _unitOfWork.UserRepository.GetAllAsync();

            return _mapper.Map<List<UserDto>>(UserList);
        }
    }
}
