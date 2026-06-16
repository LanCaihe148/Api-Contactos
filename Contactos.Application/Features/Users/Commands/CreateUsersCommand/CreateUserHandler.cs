using AutoMapper;
using Contactos.Application.Contracts.Persistance;
using Contactos.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Features.Users.Commands.CreateUsersCommand
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateUserHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userEntity = _mapper.Map<User>(request);

            _unitOfWork.UserRepository.AddEntity(userEntity);


            var result = await _unitOfWork.Complete();

            if(result <= 0)
            {
                throw new Exception("No se puede insertar el record de user");
            }

            _logger.LogInformation($"User {userEntity} fue creado exitosamente");

            return userEntity.Id;

        }
    }
}
