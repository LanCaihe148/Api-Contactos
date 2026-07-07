using System.Net;

namespace Contactos.Application.Exceptions
{
    public abstract class AuthException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected AuthException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }

    // ✅ Email no encontrado
    public class EmailNotFoundException : AuthException
    {
        public EmailNotFoundException(string email)
            : base($"El usuario con email '{email}' no existe", HttpStatusCode.NotFound)
        {
        }
    }

    // ✅ Credenciales incorrectas
    public class InvalidCredentialsException : AuthException
    {
        public InvalidCredentialsException()
            : base("Las credenciales son incorrectas", HttpStatusCode.Unauthorized)
        {
        }
    }

    // ✅ Username ya existe
    public class UserAlreadyExistsException : AuthException
    {
        public UserAlreadyExistsException(string username)
            : base($"El username '{username}' ya está en uso", HttpStatusCode.Conflict)
        {
        }
    }

    // ✅ Email ya existe
    public class EmailAlreadyExistsException : AuthException
    {
        public EmailAlreadyExistsException(string email)
            : base($"El email '{email}' ya está registrado", HttpStatusCode.Conflict)
        {
        }
    }

    // ✅ Campos requeridos faltantes
    public class AuthValidationException : AuthException
    {
        public AuthValidationException(string field)
            : base($"El campo '{field}' es requerido", HttpStatusCode.BadRequest)
        {
        }
    }

    // ✅ Contraseña débil
    public class WeakPasswordException : AuthException
    {
        public WeakPasswordException()
            : base("La contraseña debe tener al menos 6 caracteres", HttpStatusCode.BadRequest)
        {
        }
    }

}
