using FluentValidation;

namespace Portfolyo.Business.Features.Skills.RemoveSkill
{
    public sealed class RemoveSkillCommandValidator:AbstractValidator<RemoveSkillCommand>
    {

        public RemoveSkillCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Id Boş Olamaz.");

            RuleFor(p => p.Id).NotNull().WithMessage("Id Boş Olamaz.");
        }
    }
}
