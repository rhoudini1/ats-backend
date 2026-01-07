using ATS.Application.Validators.Candidate;
using ATS.Contracts.Requests;
using ATS.Domain.Exceptions.Base;
using FluentValidation.TestHelper;

namespace ATS.UnitTests.Application.Validators.Candidate;

public class RegisterCandidateValidatorTests
{
    private readonly RegisterCandidateValidator _validator;

    public RegisterCandidateValidatorTests()
    {
        _validator = new RegisterCandidateValidator();
    }

    [Fact]
    public void Validate_ShouldSucceed_WhenRequestIsValid()
    {
        var request = new RegisterCandidateRequest
        {
            FullName = "Bruce Wayne",
            Email = "bruce.wayne@batman.com"
        };

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenFullNameIsEmpty()
    {
        var request = new RegisterCandidateRequest { FullName = string.Empty, Email = "bruce.wayne@batman.com" };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.FullName)
              .WithErrorMessage(ErrorMessages.Validation.FullNameRequired);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenFullNameIsTooShort()
    {
        var request = new RegisterCandidateRequest { FullName = "Br", Email = "bruce.wayne@batman.com" };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.FullName)
            .WithErrorMessage(ErrorMessages.Validation.FullNameTooShort);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenFullNameIsTooLong()
    {
        var request = new RegisterCandidateRequest { FullName = new string('a', 101), Email = "test@test.com" };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.FullName)
            .WithErrorMessage(ErrorMessages.Validation.FullNameTooLong);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenEmailIsEmpty()
    {
        var request = new RegisterCandidateRequest { FullName = "Bruce Wayne", Email = "" };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage(ErrorMessages.Validation.EmailRequired);
    }

    [Theory]
    [InlineData("invalid-email")]
    [InlineData("user@")]
    [InlineData("@domain.com")]
    public void Validate_ShouldHaveError_WhenEmailIsInvalid(string invalidEmail)
    {
        var request = new RegisterCandidateRequest { FullName = "Bruce Wayne", Email = invalidEmail };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage(ErrorMessages.Validation.InvalidEmailFormat);
    }
}
