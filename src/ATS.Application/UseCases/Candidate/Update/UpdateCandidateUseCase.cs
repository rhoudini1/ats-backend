using ATS.Application.Mapping;
using ATS.Application.Validators.Candidate;
using ATS.Contracts.Requests;
using ATS.Contracts.Responses;
using ATS.Domain.Exceptions;
using ATS.Domain.Exceptions.Base;
using ATS.Domain.Interfaces.Repositories;
using FluentValidation.Results;

namespace ATS.Application.UseCases.Candidate.Update;

public class UpdateCandidateUseCase : IUpdateCandidateUseCase
{
    private readonly ICandidateRepository _candidateRepository;

    public UpdateCandidateUseCase(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<CandidateResponse> Execute(Guid id, UpdateCandidateRequest request, CancellationToken token)
    {
        var candidate = await Validate(id, request, token);

        candidate.FullName = request.FullName;
        candidate.UpdatedAt = DateTime.UtcNow;

        var result = await _candidateRepository.UpdateAsync(candidate, token);

        return result.MapToResponse();
    }

    private async Task<Domain.Entities.Candidate> Validate(Guid id, UpdateCandidateRequest request, CancellationToken token)
    {
        var validator = new UpdateCandidateValidator();
        var result = validator.Validate(request);

        var candidate = await _candidateRepository.GetByIdAsync(id, token);
        if (candidate is null)
            result.Errors.Add(new ValidationFailure(string.Empty, ErrorMessages.Errors.CandidateNotFound));
        else if (candidate.Email != request.Email)
            result.Errors.Add(new ValidationFailure(string.Empty, ErrorMessages.Validation.EmailChangeNotAllowed));

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }

        return candidate!;
    }
}
