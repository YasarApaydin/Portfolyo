using FluentValidation;

namespace Portfolyo.Business.Features.Skills.UpdateSkill
{
    public sealed class UpdateSkillCommandValidator:AbstractValidator<UpdateSkillCommand>
    {
        public UpdateSkillCommandValidator()
        {

            RuleFor(p => p.Id).NotEmpty().WithMessage("Id Boş Olamaz.");
            RuleFor(p => p.Id).NotNull().WithMessage("Id Boş Olamaz.");

            RuleFor(p => p.Name).NotEmpty().WithMessage("Ad Yeri Boş Olamaz.");
            RuleFor(p => p.Name).NotNull().WithMessage("Ad Yeri Boş Olamaz.");
            RuleFor(p => p.Name).MinimumLength(3).WithMessage("Ad Yeri En Az 3 Karakter Olmalıdır.");


            RuleFor(p => p.Level).NotEmpty().WithMessage("Seviye Yeri Boş Olamaz.");

            RuleFor(p => p.Level).NotNull().WithMessage("Seviye alanı boş olamaz.").InclusiveBetween(0, 100).WithMessage("Seviye en az 0, en fazla 100 olmalıdır.");


            RuleFor(p => p.IconClass).NotEmpty().WithMessage("İkon Yeri Boş Olamaz.");
            RuleFor(p => p.IconClass).NotNull().WithMessage("İkon Yeri Boş Olamaz.");

        }
    }
}
