using AutoMapper;
using Contactos.Application.Contracts.Persistance;
using Contactos.Application.Features.DTOs;
using MediatR;

namespace Contactos.Application.Features.Users.Queries.GetUsersByIdQuery
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserByidDto>
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserByidDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var userEntity = await _unitOfWork.UserRepository.GetByIdAsync(request._Id);

            return _mapper.Map<UserByidDto>(userEntity);

        }
    }
}
