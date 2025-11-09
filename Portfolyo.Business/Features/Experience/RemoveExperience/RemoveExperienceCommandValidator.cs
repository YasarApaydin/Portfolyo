using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolyo.Business.Features.Experience.RemoveExperience
{
    public sealed class RemoveExperienceCommandValidator:AbstractValidator<RemoveExperienceCommand>
    {
        public RemoveExperienceCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Id Boş Olamaz.");
            RuleFor(p => p.Id).NotNull().WithMessage("Id Boş Olamaz.");
        }
    }
}
