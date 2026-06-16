using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contactos.Application.Features.Users.Commands.CreateUsersCommand
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator() 
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} no puede estar en blanco")
                .NotNull()
                .MaximumLength(50).WithMessage("{Name} no puede exceder los 50 caracteres maistro");
            RuleFor(e => e.UserName)
                .NotEmpty().WithMessage("{UserName} no puede estar vacio")
                .NotNull()
                .MaximumLength(30).WithMessage("{UserName} no puede exceder los 30 caracteres");
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("{Email} no puede estar vacio")
                .NotNull()
                .MaximumLength(64).WithMessage("{Email} no puede exceder los 64 caracteres");
            RuleFor(e => e.Phone)
                .NotEmpty().WithMessage("{Phone} no puede estar vacio")
                .NotNull()
                .MaximumLength(12).WithMessage("{Phone} no puede exceder los 12 caracteres");

        }
    }
}
