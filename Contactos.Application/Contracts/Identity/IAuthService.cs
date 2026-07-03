using Contactos.Application.Models.Identity;

namespace Contactos.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);

        Task<RegistrationResponse> Register(RegistrationRequest request);
    }
}
