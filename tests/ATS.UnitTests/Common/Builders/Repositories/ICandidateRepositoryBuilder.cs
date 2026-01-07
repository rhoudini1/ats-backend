using ATS.Domain.Entities;
using ATS.Domain.Interfaces.Repositories;
using Moq;

namespace ATS.UnitTests.Common.Builders.Repositories;

public class ICandidateRepositoryBuilder
{
    private readonly Mock<ICandidateRepository> _repository;

    public ICandidateRepositoryBuilder() => _repository = new Mock<ICandidateRepository>();

    public ICandidateRepositoryBuilder WithCreateAsync(Candidate candidate)
    {
        _repository
            .Setup(repo => repo.CreateAsync(It.IsAny<Candidate>()))
            .ReturnsAsync((Candidate c) => c);
        return this;
    }

    public ICandidateRepositoryBuilder WithGetByEmail(string email, Candidate? candidate)
    {
        _repository.Setup(r => r.GetByEmailAsync(email))
           .ReturnsAsync(candidate);
        return this;
    }

    public ICandidateRepositoryBuilder WithGetById(Guid id, Candidate? candidate)
    {
        _repository.Setup(repo => repo.GetByIdAsync(id))
           .ReturnsAsync(candidate);
        return this;
    }

    public ICandidateRepository Build() => _repository.Object;

    public Mock<ICandidateRepository> GetMock() => _repository;
}
