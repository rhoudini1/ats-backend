using ATS.Application.UseCases.Candidate.Delete;
using ATS.UnitTests.Common.Builders.Entities;
using ATS.UnitTests.Common.Builders.Repositories;
using Moq;

namespace ATS.UnitTests.Application.UseCases.Candidate;

public class DeleteCandidateByIdUseCaseTests
{
    [Fact]
    public async Task Execute_ShouldReturnTrue_WhenCandidateExists()
    {
        var candidate = CandidateBuilder.Init().Build();
        var candidateId = candidate.Id;
        var cancellationToken = CancellationToken.None;

        var builder = new ICandidateRepositoryBuilder()
            .WithGetById(candidateId, candidate)
            .WithDelete(candidateId);
        var useCase = new DeleteCandidateByIdUseCase(builder.Build());

        var result = await useCase.Execute(candidateId, cancellationToken);

        Assert.True(result);

        builder.GetMock().Verify(repo => repo.GetByIdAsync(candidateId, cancellationToken), Times.Once);
        builder.GetMock().Verify(repo => repo.DeleteAsync(candidateId, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Execute_ShouldReturnFalse_WhenCandidateDoesNotExist()
    {
        var candidateId = Guid.NewGuid();
        var cancellationToken = CancellationToken.None;

        var builder = new ICandidateRepositoryBuilder()
            .WithGetById(candidateId, null);
        var useCase = new DeleteCandidateByIdUseCase(builder.Build());

        var result = await useCase.Execute(candidateId, cancellationToken);

        Assert.False(result);

        builder.GetMock().Verify(repo => repo.GetByIdAsync(candidateId, cancellationToken), Times.Once);
        builder.GetMock().Verify(repo => repo.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
