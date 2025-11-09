using FluentValidation;

namespace Portfolyo.Business.Features.Projects.RemoveProject
{
    public sealed class RemoveProjectCommandValidator:AbstractValidator<RemoveProjectCommand>
    {
        public RemoveProjectCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Id Boş Olamaz");
            RuleFor(p => p.Id).NotNull().WithMessage("Id Boş Olamaz");
        }
    }
}
