using FluentValidation;

namespace Portfolyo.Business.Features.Experience.UpdateExperience
{
    public sealed class UpdateExperienceCommandValidator:AbstractValidator<UpdateExperienceCommand>
    {

        public UpdateExperienceCommandValidator()
        {

            RuleFor(p => p.Id).NotEmpty().WithMessage("Id Boş Olamaz.");
            RuleFor(p => p.Id).NotNull().WithMessage("Id Boş Olamaz.");


            RuleFor(p => p.CompanyName).NotEmpty().NotNull().WithMessage("Firma Adı Boş Olamaz.");
            RuleFor(p => p.CompanyName).MinimumLength(3).WithMessage("Firma Adı Karakteri En Az 3 Karakter Olmalıdır.");



            RuleFor(p => p.Position).NotEmpty().NotNull().WithMessage("Pozisyon Adı Boş Olamaz.");
            RuleFor(p => p.Position).MinimumLength(3).WithMessage("Pozisyon Adı Karakteri En Az 3 Karakter Olmalıdır.");

            RuleFor(p => p.Sector).NotEmpty().NotNull().WithMessage("Sektör Adı Boş Olamaz.");
            RuleFor(p => p.Sector).MinimumLength(3).WithMessage("Sektör Adı Karakteri En Az 3 Karakter Olmalıdır.");
            RuleFor(p => p.WorkingMethod).NotEmpty().NotNull().WithMessage("Çalışma Yöntemi Adı Boş Olamaz.");
            RuleFor(p => p.WorkingMethod).MinimumLength(3).WithMessage("Çalışma Yöntemi Adı Karakteri En Az 3 Karakter Olmalıdır.");
            RuleFor(p => p.Explanation).NotEmpty().NotNull().WithMessage("Açıklama Adı Boş Olamaz.");
            RuleFor(p => p.Explanation).MinimumLength(3).WithMessage("Açıklama Adı Karakteri En Az 3 Karakter Olmalıdır.");
            RuleFor(p => p.TechnologyUsed).NotEmpty().NotNull().WithMessage("Kullanılan Teknoloji Adı Boş Olamaz.");
            RuleFor(p => p.TechnologyUsed).MinimumLength(3).WithMessage("Kullanılan Teknoloji Adı Karakteri En Az 3 Karakter Olmalıdır.");



            RuleFor(p => p.StartDate)
                .NotEmpty().WithMessage("Başlangıç tarihi boş olamaz.")
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Başlangıç tarihi bugünden ileri olamaz.");

            RuleFor(p => p.EndDate)
                .NotEmpty().WithMessage("Bitiş tarihi boş olamaz.")
                .GreaterThanOrEqualTo(p => p.StartDate)
                .WithMessage("Bitiş tarihi, başlangıç tarihinden önce olamaz.")
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Bitiş tarihi bugünden ileri olamaz.");
        }

    }
    }

