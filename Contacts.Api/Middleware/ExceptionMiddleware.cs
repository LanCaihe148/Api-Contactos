using Contactos.Application.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Contacts.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            // ✅ Estructura base de respuesta de error
            var errorResponse = new
            {
                success = false,
                statusCode = 500,
                error = "Error interno del servidor",
                details = "Ocurrió un error inesperado. Por favor, intente más tarde.",
                timestamp = DateTime.UtcNow
            };

            // ✅ Manejar excepciones personalizadas
            switch (exception)
            {
                case AuthException authEx:
                    response.StatusCode = (int)authEx.StatusCode;
                    errorResponse = new
                    {
                        success = false,
                        statusCode = (int)authEx.StatusCode,
                        error = GetErrorTitle(authEx),
                        details = authEx.Message,
                        timestamp = DateTime.UtcNow
                    };
                    break;

                // ✅ Si estás usando FluentValidation.ValidationException
                case FluentValidation.ValidationException fluentEx:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var fluentErrors = string.Join("; ", fluentEx.Errors.Select(e => e.ErrorMessage));
                    errorResponse = new
                    {
                        success = false,
                        statusCode = 400,
                        error = "Error de validación",
                        details = fluentErrors,
                        timestamp = DateTime.UtcNow
                    };
                    break;

                // ✅ Caso por defecto para errores no manejados
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    _logger.LogError(exception, "Error no manejado: {Message}", exception.Message);
                    errorResponse = new
                    {
                        success = false,
                        statusCode = 500,
                        error = "Error interno del servidor",
                        details = exception.Message, // En desarrollo, mostrar más detalles
                        timestamp = DateTime.UtcNow
                    };
                    break;
            }

            var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await response.WriteAsync(jsonResponse);
        }

        // ✅ Método auxiliar para obtener el título del error
        private static string GetErrorTitle(AuthException exception)
        {
            return exception switch
            {
                EmailNotFoundException => "Usuario no encontrado",
                InvalidCredentialsException => "Credenciales inválidas",
                UserAlreadyExistsException => "Usuario ya existe",
                EmailAlreadyExistsException => "Email ya registrado",
                AuthValidationException => "Error de validación",
                WeakPasswordException => "Contraseña débil",
                _ => "Error de autenticación"
            };
        }
    }
}
