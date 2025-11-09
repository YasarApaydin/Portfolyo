using FluentValidation;

namespace Portfolyo.Business.Features.Profiles.UpdateProfile
{
    public sealed class UpdateProfileCommandValidator:AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(p => p.FullName).NotNull().WithMessage("Ad Soyad Boş Olamaz");
            RuleFor(p => p.FullName).NotEmpty().WithMessage("Ad Soyad Boş Olamaz");
            RuleFor(p => p.FullName).MinimumLength(5).WithMessage("Ad Soyad En Az 5 Karakter Olmalıdır.");

            RuleFor(p => p.Id).NotNull().WithMessage("Id Boş Olamaz");
            RuleFor(p => p.Id).NotEmpty().WithMessage("Id Boş Olamaz");


            RuleFor(p => p.Title).NotNull().WithMessage("Başlık Boş Olamaz");
            RuleFor(p => p.Title).NotEmpty().WithMessage("Başlık Boş Olamaz");
            RuleFor(p => p.Title).MinimumLength(5).WithMessage("Başlık En Az 5 Karakter Olmalıdır.");


            RuleFor(p => p.Biography).NotNull().WithMessage("Biografi Boş Olamaz");
            RuleFor(p => p.Biography).NotEmpty().WithMessage("Biografi Boş Olamaz");
            RuleFor(p => p.Biography).MinimumLength(10).WithMessage("Biografi En Az 10 Karakter Olmalıdır.");

            RuleFor(p => p.Email).NotNull().WithMessage("Email Boş Olamaz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email Boş Olamaz");
            RuleFor(p => p.Email).MinimumLength(5).WithMessage("Email En Az 5 Karakter Olmalıdır.");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Gecerli Bir Email Adres Girin");



            RuleFor(p => p.GithubUrl).NotNull().WithMessage("Github Boş Olamaz");
            RuleFor(p => p.GithubUrl).NotEmpty().WithMessage("Github Boş Olamaz");
            RuleFor(p => p.GithubUrl).MinimumLength(3).WithMessage("Github En Az 3 Karakter Olmalıdır.");

            RuleFor(p => p.LinkedInUrl).NotNull().WithMessage("LinkedIn Boş Olamaz");
            RuleFor(p => p.LinkedInUrl).NotEmpty().WithMessage("LinkedIn Boş Olamaz");
            RuleFor(p => p.LinkedInUrl).MinimumLength(3).WithMessage("LinkedIn En Az 3 Karakter Olmalıdır.");

        }
    }
}
