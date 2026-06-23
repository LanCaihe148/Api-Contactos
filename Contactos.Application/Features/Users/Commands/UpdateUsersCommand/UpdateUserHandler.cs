using AutoMapper;
using Contactos.Application.Contracts.Persistance;
using Contactos.Application.Exceptions;
using Contactos.Domain;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Contactos.Application.Features.Users.Commands.UpdateUsersCommand
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserHandler> _logger;

        public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateUserHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userUpdate = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);

            if(userUpdate == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            //var updatedUser = new User(
            //    request.Name,
            //    request.UserName,
            //    request.Address is null ? null : new Address(
            //        request.Address.Street!,
            //        request.Address.Suite!,
            //        request.Address.City!,
            //        request.Address.ZipCode!,
            //        request.Address.Geo is null ? null : new Geo(
            //            request.Address.Geo.Lat!,
            //            request.Address.Geo.Lng!
            //            )
            //        ),
            //    request.Email is null ? null : new Email(request.Email),
            //    request.Phone is null ? null : new Phone(request.Phone),
            //    request.WebSite!,
            //    request.Company is null ? null : new Company(
            //        request.Company.Name,
            //        request.Company.CatchPhrase,
            //        request.Company.Bs
            //        ));

            var updatedUser = _mapper.Map<User>(request);

            updatedUser.Id = request.Id;

            _unitOfWork.UserRepository.UpdateEntity(updatedUser);
            

            await _unitOfWork.Complete();

            _logger.LogInformation($"User {request.Id} fue actualizado exitosamente");

            return Unit.Value;
        }
    }
}
