using ATS.Application.Mapping;
using ATS.Application.UseCases.Candidate.Register;
using ATS.Domain.Exceptions;
using ATS.Domain.Exceptions.Base;
using ATS.Domain.Interfaces.Repositories;
using ATS.UnitTests.Common.Builders.Repositories;
using ATS.UnitTests.Common.Builders.Requests;
using Moq;

namespace ATS.UnitTests.Application.UseCases.Candidate;

public class RegisterCandidateUseCaseTests
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly RegisterCandidateUseCase _useCase;

    public RegisterCandidateUseCaseTests()
    {
        _candidateRepository = new ICandidateRepositoryBuilder().Build();

        _useCase = new RegisterCandidateUseCase(_candidateRepository);
    }

    [Fact]
    public async Task Execute_ShouldReturnResponse_WhenRequestIsValid()
    {
        var request = RegisterCandidateRequestBuilder.Init().Build();
        var candidate = request.MapToCandidate();

        var builder = new ICandidateRepositoryBuilder()
            .WithCreateAsync(candidate)
            .WithGetByEmail(request.Email, null);
        var candidateRepository = builder.Build();
        var useCase = new RegisterCandidateUseCase(candidateRepository);

        var result = await useCase.Execute(request);

        Assert.NotNull(result);
        Assert.Equal(request.FullName, result.FullName);

        builder.GetMock().Verify(repo => repo.CreateAsync(
            It.Is<ATS.Domain.Entities.Candidate>(c => c.Email == request.Email && c.FullName == request.FullName)),
            Times.Once);
    }

    [Fact]
    public async Task Execute_ShouldThrowValidationException_WhenEmailAlreadyExists()
    {
        var request = RegisterCandidateRequestBuilder.Init().Build();
        var candidate = request.MapToCandidate();

        var builder = new ICandidateRepositoryBuilder()
            .WithCreateAsync(candidate)
            .WithGetByEmail(request.Email, candidate);
        var candidateRepository = builder.Build();

        var useCase = new RegisterCandidateUseCase(candidateRepository);

        var act = () => useCase.Execute(request);

        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
        Assert.Contains(ErrorMessages.Validation.EmailAlreadyRegistered, exception.Messages);

        builder.GetMock().Verify(r => r.CreateAsync(It.IsAny<ATS.Domain.Entities.Candidate>()), Times.Never);
    }

    [Fact]
    public async Task Execute_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        var request = RegisterCandidateRequestBuilder.Init().WithFullName("Ab").WithEmail("invalid").Build();
        var candidate = request.MapToCandidate();

        var builder = new ICandidateRepositoryBuilder()
            .WithGetByEmail(request.Email, null);
        var candidateRepository = builder.Build();

        var useCase = new RegisterCandidateUseCase(candidateRepository);

        var act = () => useCase.Execute(request);

        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);

        Assert.Contains(ErrorMessages.Validation.FullNameTooShort, exception.Messages);
        Assert.Contains(ErrorMessages.Validation.InvalidEmailFormat, exception.Messages);

        builder.GetMock().Verify(r => r.CreateAsync(It.IsAny<ATS.Domain.Entities.Candidate>()), Times.Never);
    }
}
