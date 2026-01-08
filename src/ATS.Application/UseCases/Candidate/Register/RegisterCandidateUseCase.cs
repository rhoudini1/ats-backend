using ATS.Application.Mapping;
using ATS.Application.Validators.Candidate;
using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using ATS.Domain.Exceptions;
using ATS.Domain.Exceptions.Base;
using ATS.Domain.Interfaces.Repositories;
using FluentValidation.Results;

namespace ATS.Application.UseCases.Candidate.Register;

public class RegisterCandidateUseCase : IRegisterCandidateUseCase
{
    private readonly ICandidateRepository _candidateRepository;

    public RegisterCandidateUseCase(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<CandidateResponse> Execute(RegisterCandidateRequest request, CancellationToken token)
    {
        await Validate(request, token);

        var candidate = request.MapToCandidate();

        var result = await _candidateRepository.CreateAsync(candidate, token);

        return result.MapToResponse();
    }

    private async Task Validate(RegisterCandidateRequest request, CancellationToken token)
    {
        var validator = new RegisterCandidateValidator();
        var result = validator.Validate(request);

        var possibleRegisteredUser = await _candidateRepository.GetByEmailAsync(request.Email, token);
        if (possibleRegisteredUser is not null)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ErrorMessages.Validation.EmailAlreadyRegistered));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
