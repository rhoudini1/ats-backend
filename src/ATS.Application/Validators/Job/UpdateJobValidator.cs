using ATS.Contracts.Requests;
using ATS.Domain.Enums;
using ATS.Domain.Exceptions.Base;
using FluentValidation;

namespace ATS.Application.Validators.Job;

public class UpdateJobValidator : AbstractValidator<UpdateJobRequest>
{
    public UpdateJobValidator()
    {
        RuleFor(job => job.Title)
            .NotEmpty().WithMessage(ErrorMessages.Validation.JobTitleRequired)
            .MinimumLength(3).WithMessage(ErrorMessages.Validation.JobTitleTooShort);

        RuleFor(job => job.Description)
            .NotEmpty().WithMessage(ErrorMessages.Validation.JobDescriptionRequired)
            .MinimumLength(10).WithMessage(ErrorMessages.Validation.JobDescriptionTooShort);

        RuleFor(job => job.Status)
            .IsEnumName(typeof(EJobStatus), caseSensitive: false)
            .WithMessage(ErrorMessages.Validation.InvalidJobStatus);
    }
}
