using ATS.Domain.Entities;
using ATS.Domain.Interfaces.Repositories;
using Moq;

namespace ATS.UnitTests.Common.Builders.Repositories;

public class ICandidateRepositoryBuilder
{
    private readonly Mock<ICandidateRepository> _repository;

    public ICandidateRepositoryBuilder() => _repository = new Mock<ICandidateRepository>();

    public ICandidateRepositoryBuilder WithCreateAsync()
    {
        _repository
            .Setup(repo => repo.CreateAsync(It.IsAny<Candidate>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Candidate c, CancellationToken _) => c);
        return this;
    }

    public ICandidateRepositoryBuilder WithGetByEmail(string email, Candidate? candidate)
    {
        _repository.Setup(r => r.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
           .ReturnsAsync(candidate);
        return this;
    }

    public ICandidateRepositoryBuilder WithGetById(Guid id, Candidate? candidate)
    {
        _repository.Setup(repo => repo.GetByIdAsync(id, It.IsAny<CancellationToken>()))
           .ReturnsAsync(candidate);
        return this;
    }

    public ICandidateRepositoryBuilder WithGetPaged(int page, int size, IEnumerable<ATS.Domain.Entities.Candidate> candidates)
    {
        _repository.Setup(repo => repo.GetPagedAsync(page, size, It.IsAny<CancellationToken>()))
            .ReturnsAsync(candidates);
        return this;
    }

    public ICandidateRepositoryBuilder WithCountTotal(int total)
    {
        _repository.Setup(repo => repo.CountTotalAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(total);
        return this;
    }

    public ICandidateRepository Build() => _repository.Object;

    public Mock<ICandidateRepository> GetMock() => _repository;
}
