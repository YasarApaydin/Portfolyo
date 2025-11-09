using FluentValidation;

namespace Portfolyo.Business.Features.Profiles.RemoveProfile
{
    public sealed class RemoveProfileCommandValidator:AbstractValidator<RemoveProfileCommand>
    {

        public RemoveProfileCommandValidator()
        {

            RuleFor(p => p.Id).NotEmpty().WithMessage("Id Boş Olamaz.");
            RuleFor(p => p.Id).NotNull().WithMessage("Id Boş Olamaz.");
            
        }
    }
}
