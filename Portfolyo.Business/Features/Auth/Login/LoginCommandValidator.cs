using FluentValidation;

namespace Portfolyo.Business.Features.Auth.Login
{
    public sealed class LoginCommandValidator:AbstractValidator<LoginCommand>
    {

        public LoginCommandValidator()
        {
            RuleFor(p => p.UserNameOrEmail).NotEmpty().WithMessage("Kullanıcı Ad Alanı Boş Olamaz.");
            RuleFor(p => p.UserNameOrEmail).NotNull().WithMessage("Kullanıcı Ad Alanı Boş Olamaz.");
            RuleFor(p => p.UserNameOrEmail).MinimumLength(3).WithMessage("Kullanıcı Ad Alanı En Az 3 Karakter Olmalıdır.");


            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre Alanı Boş Olamaz.");
            RuleFor(p => p.Password).NotNull().WithMessage("Şifre Alanı Boş Olamaz.");
            RuleFor(p => p.Password).MinimumLength(8).WithMessage("Şifre Alanı En Az 8 Karakter İcermelidir.");
            RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Şifre Alanı En Az 1 Adet Büyük Harf İcermelidir.");
            RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Şifre Alanı En Az 1 Adet Kücük Harf İcermelidir.");
            RuleFor(p => p.Password).Matches("[0-9]").WithMessage("Şifre Alanı En Az 1 Adet Rakam İcermelidir.");
            RuleFor(p => p.Password).Matches("[^a-zA-Z0-9]").WithMessage("Şifre Alanı En Az 1 Adet Özel Karakter İcermelidir.");
        }
    }
}
