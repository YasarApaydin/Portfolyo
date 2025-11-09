using FluentValidation;

namespace Portfolyo.Business.Features.Users.UpdateUser
{
    public sealed class UpdateUserCommandValidator:AbstractValidator<UpdateUserCommand>
    {

        public UpdateUserCommandValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("Kullanıcı Ad Alanı Boş Olamaz.");
            RuleFor(p => p.UserName).NotNull().WithMessage("Kullanıcı Ad Alanı Boş Olamaz.");
            RuleFor(p => p.UserName).MinimumLength(3).WithMessage("Kullanıcı Ad Alanı En Az 3 Karakter Olmalıdır.");

            RuleFor(p => p.Name).NotEmpty().WithMessage("Ad Alanı Boş Olamaz.");
            RuleFor(p => p.Name).NotNull().WithMessage("Ad Alanı Boş Olamaz.");
            RuleFor(p => p.Name).MinimumLength(3).WithMessage("Ad Alanı En Az 3 Karakter Olmalıdır.");

            RuleFor(p => p.LastName).NotEmpty().WithMessage("Soyad Alanı Boş Olamaz.");
            RuleFor(p => p.LastName).NotNull().WithMessage("Soyad Alanı Boş Olamaz.");
            RuleFor(p => p.LastName).MinimumLength(3).WithMessage("Soyad Alanı En Az 3 Karakter Olmalıdır.");

            RuleFor(p => p.Email).NotEmpty().WithMessage("Mail Adresi Boş Olamaz.");
            RuleFor(p => p.Email).NotNull().WithMessage("Mail Adresi Boş Olamaz.");
            RuleFor(p => p.Email).MinimumLength(3).WithMessage("Mail Adresi En Az 3 Karakter Olmalıdır.");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Gecerli Bir Email Adresi Girin.");

            RuleFor(p => p.NewPassword).NotEmpty().WithMessage("Şifre Alanı Boş Olamaz.");
            RuleFor(p => p.NewPassword).NotNull().WithMessage("Şifre Alanı Boş Olamaz.");
            RuleFor(p => p.NewPassword).MinimumLength(8).WithMessage("Şifre Alanı En Az 8 Karakter İcermelidir.");
            RuleFor(p => p.NewPassword).Matches("[A-Z]").WithMessage("Şifre Alanı En Az 1 Adet Büyük Harf İcermelidir.");
            RuleFor(p => p.NewPassword).Matches("[a-z]").WithMessage("Şifre Alanı En Az 1 Adet Kücük Harf İcermelidir.");
            RuleFor(p => p.NewPassword).Matches("[0-9]").WithMessage("Şifre Alanı En Az 1 Adet Rakam İcermelidir.");
            RuleFor(p => p.NewPassword).Matches("[^a-zA-Z0-9]").WithMessage("Şifre Alanı En Az 1 Adet Özel Karakter İcermelidir.");



            RuleFor(p => p.CurrentPassword).NotEmpty().WithMessage("Şifre Alanı Boş Olamaz.");
            RuleFor(p => p.CurrentPassword).NotNull().WithMessage("Şifre Alanı Boş Olamaz.");
            RuleFor(p => p.CurrentPassword).MinimumLength(8).WithMessage("Şifre Alanı En Az 8 Karakter İcermelidir.");
            RuleFor(p => p.CurrentPassword).Matches("[A-Z]").WithMessage("Şifre Alanı En Az 1 Adet Büyük Harf İcermelidir.");
            RuleFor(p => p.CurrentPassword).Matches("[a-z]").WithMessage("Şifre Alanı En Az 1 Adet Kücük Harf İcermelidir.");
            RuleFor(p => p.CurrentPassword).Matches("[0-9]").WithMessage("Şifre Alanı En Az 1 Adet Rakam İcermelidir.");
            RuleFor(p => p.CurrentPassword).Matches("[^a-zA-Z0-9]").WithMessage("Şifre Alanı En Az 1 Adet Özel Karakter İcermelidir.");

            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("Telefon Numarası Boş Olamaz.")
                .NotNull().WithMessage("Telefon Numarası Boş Olamaz.")
                .MinimumLength(10).WithMessage("Telefon Numarası En Az 10 Karakter Olmalıdır.")
                .MaximumLength(15).WithMessage("Telefon Numarası En Fazla 15 Karakter Olabilir.");

        }

    }
}
