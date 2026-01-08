using ATS.Application.UseCases.Candidate.Update;
using ATS.Domain.Exceptions;
using ATS.Domain.Exceptions.Base;
using ATS.UnitTests.Common.Builders.Entities;
using ATS.UnitTests.Common.Builders.Repositories;
using ATS.UnitTests.Common.Builders.Requests;
using Moq;

namespace ATS.UnitTests.Application.UseCases.Candidate;

public class UpdateCandidateUseCaseTests
{
    [Fact]
    public async Task Execute_ShouldReturnResponse_WhenUpdateIsValid()
    {
        var token = CancellationToken.None;
        var existingCandidate = CandidateBuilder.Init().Build();
        var id = existingCandidate.Id;

        var request = UpdateCandidateRequestBuilder.Init()
            .WithEmail(existingCandidate.Email)
            .Build();

        var builder = new ICandidateRepositoryBuilder()
            .WithGetById(id, existingCandidate)
            .WithUpdate();
        var candidateRepository = builder.Build();

        var useCase = new UpdateCandidateUseCase(candidateRepository);

        var result = await useCase.Execute(id, request, token);

        Assert.NotNull(result);
        Assert.Equal(request.FullName, result.FullName);

        builder.GetMock().Verify(repo => repo.UpdateAsync(
            It.Is<Domain.Entities.Candidate>(c => c.FullName == request.FullName), token), Times.Once);
    }

    [Fact]
    public async Task Execute_ShouldThrowException_WhenCandidateNotFound()
    {
        var id = Guid.NewGuid();
        var request = UpdateCandidateRequestBuilder.Init().Build();

        var builder = new ICandidateRepositoryBuilder()
            .WithGetById(id, null);

        var useCase = new UpdateCandidateUseCase(builder.Build());

        var act = () => useCase.Execute(id, request, CancellationToken.None);

        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
        Assert.Contains(ErrorMessages.Errors.CandidateNotFound, exception.Messages);

        builder.GetMock()
            .Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Candidate>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Execute_ShouldThrowException_WhenEmailIsDifferent()
    {
        var existingCandidate = CandidateBuilder.Init().Build();
        var id = existingCandidate.Id;

        var request = UpdateCandidateRequestBuilder.Init().WithEmail("any@mail.com").Build();

        var builder = new ICandidateRepositoryBuilder()
            .WithGetById(id, existingCandidate);

        var useCase = new UpdateCandidateUseCase(builder.Build());

        var act = () => useCase.Execute(id, request, CancellationToken.None);

        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
        Assert.Contains(ErrorMessages.Validation.EmailChangeNotAllowed, exception.Messages);

        builder.GetMock()
            .Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Candidate>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Execute_ShouldThrowException_WhenFullNameIsInvalid()
    {
        var existingCandidate = CandidateBuilder.Init().Build();
        var id = existingCandidate.Id;

        var request = UpdateCandidateRequestBuilder.Init()
            .WithFullName("Ab")
            .WithEmail(existingCandidate.Email)
            .Build();

        var builder = new ICandidateRepositoryBuilder()
            .WithGetById(id, existingCandidate);

        var useCase = new UpdateCandidateUseCase(builder.Build());

        var act = () => useCase.Execute(id, request, CancellationToken.None);

        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
        Assert.NotEmpty(exception.Messages);
        Assert.Contains(ErrorMessages.Validation.FullNameTooShort, exception.Messages);
    }
}
