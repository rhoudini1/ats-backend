using ATS.Application.UseCases.Candidate.List;
using ATS.Contracts.Requests;
using ATS.Domain.Exceptions;
using ATS.Domain.Exceptions.Base;
using ATS.UnitTests.Common.Builders.Entities;
using ATS.UnitTests.Common.Builders.Repositories;
using Moq;

namespace ATS.UnitTests.Application.UseCases.Candidate;

public class ListCandidatesUseCaseTests
{
    [Fact]
    public async Task Execute_ShouldReturnPagedResponse_WhenRequestIsValid()
    {
        var request = new ListRequest { Page = 1, Size = 10 };
        var candidates = new List<ATS.Domain.Entities.Candidate>
        {
            CandidateBuilder.Init().Build(),
            CandidateBuilder.Init().Build()
        };

        var builder = new ICandidateRepositoryBuilder()
            .WithGetPaged(request.Page, request.Size, candidates)
            .WithCountTotal(10);

        var useCase = new ListCandidatesUseCase(builder.Build());

        var result = await useCase.Execute(request);

        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count());
        Assert.Equal(10, result.TotalCount);

        builder.GetMock().Verify(repo => repo.GetPagedAsync(request.Page, request.Size), Times.Once);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Execute_ShouldThrowValidationException_WhenPageIsInvalid(int invalidPage)
    {
        var request = new ListRequest { Page = invalidPage, Size = 10 };

        var builder = new ICandidateRepositoryBuilder();
        var useCase = new ListCandidatesUseCase(builder.Build());

        var act = () => useCase.Execute(request);

        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
        Assert.Contains(ErrorMessages.Validation.PageNumberNegative, exception.Messages);

        builder.GetMock().Verify(repo => repo.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        builder.GetMock().Verify(repo => repo.CountTotalAsync(), Times.Never);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    public async Task Execute_ShouldThrowValidationException_WhenSizeIsInvalid(int invalidSize)
    {
        var request = new ListRequest { Page = 1, Size = invalidSize };

        var builder = new ICandidateRepositoryBuilder();
        var useCase = new ListCandidatesUseCase(builder.Build());

        var act = () => useCase.Execute(request);

        await Assert.ThrowsAsync<ErrorOnValidationException>(act);

        builder.GetMock().Verify(repo => repo.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }
}
