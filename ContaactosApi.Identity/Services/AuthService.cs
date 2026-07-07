
using Contactos.Application.Constants;
using Contactos.Application.Contracts.Identity;
using Contactos.Application.Contracts.Persistance;
using Contactos.Application.Models.Identity;
using Contactos.Domain;
using ContactsApi.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ContactsApi.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (string.IsNullOrEmpty(request.Email))
            {
                user = await _userManager.FindByNameAsync(request.Username);
            }
            if (user == null)
            {
                throw new Exception($"El usuario con email {request.Email} no existe");
            }
            var resultado = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!resultado.Succeeded)
            {
                throw new Exception("Las credenciales son incorrectas.");
            }

            var token = await GenerateToken(user);
            var authresponse = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = user.Email,
                Username = user.UserName

            };

            return authresponse;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.Username);

            if (existingUser != null)
            {
                throw new Exception($"El username {request.Username} ya esta en uso");
            }
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail != null)
            {
                throw new Exception($"Este email ya ha sido registrado por un usuario");
            }

            var appuser = new ApplicationUser
            {
                Email = request.Email,

                Nombre = request.Name,

                Apellidos = request.Apellidos,

                UserName = request.Username,

                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(appuser, request.Password);


            await _userManager.AddToRoleAsync(appuser, "Operator");

            var user = new User(
                name: request.Name,
                userName: request.Username,
                address: null,
                email: new Email(request.Email),
                phone: null,
                webSite: string.Empty,
                company: null
                );

            user.SetApplicationUserId(appuser.Id);
            _unitOfWork.UserRepository.AddEntity(user);
            await _unitOfWork.Complete();

            appuser.UserId = user.Id;
            
            var token = await GenerateToken(appuser);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"{result.Errors}");

            }


            return new RegistrationResponse
            {
                Email = appuser.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = appuser.Id,
                Username = user.UserName
            };
            

            //var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            //throw new Exception($"Error al registrar usuario: {errors}");
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach(var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurity, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims = claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
