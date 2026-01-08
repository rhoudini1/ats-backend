using ATS.Contracts.Requests;
using ATS.Domain.Exceptions.Base;
using FluentValidation;

namespace ATS.Application.Validators.Job;

public class RegisterJobValidator : AbstractValidator<RegisterJobRequest>
{
    public RegisterJobValidator()
    {
        RuleFor(job => job.Title)
            .NotEmpty().WithMessage(ErrorMessages.Validation.JobTitleRequired)
            .MinimumLength(3).WithMessage(ErrorMessages.Validation.JobTitleTooShort)
            .MaximumLength(100).WithMessage(ErrorMessages.Validation.JobTitleTooLong);

        RuleFor(job => job.Description)
            .NotEmpty().WithMessage(ErrorMessages.Validation.JobDescriptionRequired)
            .MinimumLength(10).WithMessage(ErrorMessages.Validation.JobDescriptionTooShort)
            .MaximumLength(2000).WithMessage(ErrorMessages.Validation.JobDescriptionTooLong);
    }
}