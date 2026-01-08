using ATS.Contracts.Requests;
using ATS.Domain.Exceptions.Base;
using FluentValidation;

namespace ATS.Application.Validators.Common;

public class ListRequestValidator : AbstractValidator<ListRequest>
{
    private const int MaxPageSize = 100;

    public ListRequestValidator()
    {
        RuleFor(req => req.Page)
            .GreaterThan(0).WithMessage(ErrorMessages.Validation.PageNumberNegative);

        RuleFor(req => req.Size)
            .GreaterThan(0).WithMessage(ErrorMessages.Validation.PageSizeNegative)
            .LessThanOrEqualTo(MaxPageSize).WithMessage(ErrorMessages.Validation.PageSizeExceeded);
    }
}
