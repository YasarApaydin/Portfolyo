using FluentValidation;

namespace Portfolyo.Business.Features.Roles.CreateRole
{
    public sealed class CreateRoleCommandValidator:AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(p =>p.Name).NotEmpty().NotNull().WithMessage("Boş Olamaz");
            RuleFor(p => p.Name).MaximumLength(3).WithMessage("En Az 3 Karakter Olmalıdır.");

        }
    }
}
