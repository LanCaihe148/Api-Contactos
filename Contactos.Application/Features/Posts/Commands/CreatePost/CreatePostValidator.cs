using FluentValidation;


namespace Contactos.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostValidator()
        {
            RuleFor(b => b.Title)
                .NotNull().WithMessage("{Title} no puede ser nulo");
            RuleFor(a => a.Body)
                .NotNull().WithMessage("{Body} no puede ser nulo");
        }
    }
}
