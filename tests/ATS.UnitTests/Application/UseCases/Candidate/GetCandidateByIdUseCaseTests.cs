using ATS.Application.UseCases.Candidate.GetById;
using ATS.UnitTests.Common.Builders.Entities;
using ATS.UnitTests.Common.Builders.Repositories;
using Moq;

namespace ATS.UnitTests.Application.UseCases.Candidate;

public class GetCandidateByIdUseCaseTests
{
    [Fact]
    public async Task Execute_ShouldReturnResponse_WhenCandidateExists()
    {
        var candidate = CandidateBuilder.Init().Build();
        var candidateId = candidate.Id;
        var builder = new ICandidateRepositoryBuilder()
            .WithGetById(candidateId, candidate);

        var useCase = new GetCandidateByIdUseCase(builder.Build());
        var result = await useCase.Execute(candidateId, It.IsAny<CancellationToken>());

        Assert.NotNull(result);
        Assert.Equal(candidate.FullName, result.FullName);
        Assert.Equal(candidate.Email, result.Email);

        builder.GetMock().Verify(repo => repo.GetByIdAsync(candidateId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Execute_ShouldReturnNull_WhenCandidateDoesNotExist()
    {
        var candidateId = Guid.NewGuid();
        var builder = new ICandidateRepositoryBuilder()
            .WithGetById(candidateId, null);

        var useCase = new GetCandidateByIdUseCase(builder.Build());
        var result = await useCase.Execute(candidateId, It.IsAny<CancellationToken>());

        Assert.Null(result);

        builder.GetMock().Verify(repo => repo.GetByIdAsync(candidateId, It.IsAny<CancellationToken>()), Times.Once);
    }
}
