using ATS.Contracts.Requests;
using ATS.Domain.Exceptions.Base;
using FluentValidation;

namespace ATS.Application.Validators.Candidate;

public class RegisterCandidateValidator : AbstractValidator<RegisterCandidateRequest>
{
    public RegisterCandidateValidator()
    {
        RuleFor(candidate => candidate.FullName)
            .NotEmpty().WithMessage(ErrorMessages.Validation.FullNameRequired)
            .MinimumLength(3).WithMessage(ErrorMessages.Validation.FullNameTooShort)
            .MaximumLength(100).WithMessage(ErrorMessages.Validation.FullNameTooLong);

        RuleFor(candidate => candidate.Email)
            .NotEmpty().WithMessage(ErrorMessages.Validation.EmailRequired)
            .EmailAddress().WithMessage(ErrorMessages.Validation.InvalidEmailFormat);
    }
}
