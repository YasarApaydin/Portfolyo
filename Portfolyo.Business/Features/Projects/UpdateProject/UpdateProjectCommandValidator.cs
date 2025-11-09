using FluentValidation;

namespace Portfolyo.Business.Features.Projects.UpdateProject
{
    public sealed class UpdateProjectCommandValidator:AbstractValidator<UpdateProjectCommand>
    {

        public UpdateProjectCommandValidator()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage("Başlık Boş Olamaz.");
            RuleFor(p => p.Title).NotNull().WithMessage("Başlık Boş Olamaz.");
            RuleFor(p => p.Title).MinimumLength(3).WithMessage("Başlık En Az 3 Karakter Olmalıdır.");

            RuleFor(p => p.Id).NotEmpty().WithMessage("Id Boş Olamaz.");
            RuleFor(p => p.Id).NotNull().WithMessage("Id Boş Olamaz.");



            RuleFor(p => p.Description).NotEmpty().WithMessage("Acıklama Boş Olamaz.");
            RuleFor(p => p.Description).NotNull().WithMessage("Acıklama Boş Olamaz.");
            RuleFor(p => p.Description).MinimumLength(3).WithMessage("Acıklama En Az 3 Karakter Olmalıdır.");


            RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("Image Boş Olamaz.");
            RuleFor(p => p.ImageUrl).NotNull().WithMessage("Image Boş Olamaz.");


        
            RuleFor(p => p.Technologies).NotEmpty().WithMessage("Kullanılan Tekonolojiler Boş Olamaz.");
            RuleFor(p => p.Technologies).NotNull().WithMessage("Kullanılan Tekonolojiler Boş Olamaz.");
            RuleFor(p => p.Technologies).MinimumLength(1).WithMessage("Kullanılan Tekonolojiler En Az 1 Karakter Olmalıdır.");

        }
    }
}
